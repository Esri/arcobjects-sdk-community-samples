// Copyright 2015 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at <your ArcGIS install location>/DeveloperKit10.4/userestrictions.txt.
// 



// LogoMarkerSymbol.cpp : Implementation of CLogoMarkerSymbol
#include "stdafx.h"
#include "LogoMarkerSymbolVC.h"
#include "LogoMarkerSymbol.h"

#include <math.h>

/////////////////////////////////////////////////////////////////////////////
//	Helper Functions - Generic

double Radians(double dDegrees)
{
	double dPi = 4 * atan(1.0);
	return dDegrees * dPi / 180;
}

bool qaDoubleCompare(double TestValue, double ExpectedValue, int precision  = 8)
{
    double v1,v2,vx,tx;
		tx = pow(10.0, precision);
    v1 = (int) (fabs(TestValue) * tx) / tx;
    v2 = (int)(fabs(ExpectedValue) * tx) / tx;
    vx = (v1 - v2) * tx;
    return (abs((int)vx) + 1)  <=  2 ;
}

void CreatePoint(IPointPtr* ippPoint, double XVal, double YVal, 
								 double ZVal = 0, double MVal = 0, long IDVal = 0)
{
	ippPoint->CreateInstance(CLSID_Point);
	(*ippPoint)->PutCoords(XVal, YVal);
	(*ippPoint)->put_M(MVal);
	(*ippPoint)->put_Z(ZVal);
	(*ippPoint)->put_ID(IDVal);
  return;
}

void CreateCArcCenterFromTo(ICircularArcPtr* ippCArc, IPointPtr ipCPt, 
														IPointPtr ipFPt, IPointPtr ipTPt)
{
	ippCArc->CreateInstance(CLSID_CircularArc);
	(*ippCArc)->PutCoords(ipCPt, ipFPt, ipTPt, esriArcClockwise);
	return;
}

void ToMapPoint(IDisplayTransformationPtr ipDTrans, double dX, double dY, IPoint** ippPoint)
{
	if (ipDTrans == NULL)
	{
		::CoCreateInstance(CLSID_Point, NULL, 0, IID_IPoint, (void**)ippPoint);
		(*ippPoint)->PutCoords(dX, dY);
	}
	else
		ipDTrans->ToMapPoint(lround(dX), lround(dY), ippPoint);
}

void FromMapPoint(IDisplayTransformationPtr ipDTrans, IPointPtr ipPoint, double* pdX, double* pdY)
{
	if (ipDTrans == NULL)
		ipPoint->QueryCoords(pdX, pdY);
	else
	{
		long lX, lY;
		ipDTrans->FromMapPoint(ipPoint, &lX, &lY);
		*pdX = lX;
		*pdY = lY;
	}
}

/////////////////////////////////////////////////////////////////////////////
// CLogoMarkerSymbol

/////////////////////////////////////////////////////////////////////////////
CLogoMarkerSymbol::CLogoMarkerSymbol() : m_dPi(3.141592653), m_lhDC(0),
	m_lPen(0), m_lOldPen(0), m_lBrushTop(0), m_lBrushLeft(0),
	m_lBrushRight(0), m_lOldBrush(0), /*m_lROP2Old;*/
	m_dDeviceRatio(0.0), m_dDeviceXOffset(0.0),
	m_dDeviceYOffset(0.0), /*m_Coords[6]*/
	m_dDeviceRadius(0.0),
	m_ipTopColor(CLSID_RgbColor),
	m_ipLeftColor(CLSID_RgbColor),
	m_ipRightColor(CLSID_RgbColor),
	m_ipBorderColor(CLSID_RgbColor),
	m_lROP2(esriROPCopyPen),
	m_dSize(40.0), m_dXOffset(0.0),
	m_dYOffset(0.0), m_dAngle(0.0),
	m_bsDisplayName(L"Logo Marker Symbol (C++)"),
	m_bRotWithTrans(VARIANT_TRUE),
	m_dMapRotation(0.0),
	m_lMapLevel(0)
{
	m_ipTopColor->put_RGB(RGB(255,0,0));
	m_ipLeftColor->put_RGB(RGB(170,0,0));
	m_ipRightColor->put_RGB(RGB(128,0,0));
	m_ipBorderColor->put_RGB(RGB(0,0,0));
}

CLogoMarkerSymbol::~CLogoMarkerSymbol()
{
	//m_lhDC = 0;
}

//	Helper Functions - Class
double CLogoMarkerSymbol::MapToPoints(ITransformationPtr ipTrans, double dMapSize)
{
	if (ipTrans == NULL)
	{
		return dMapSize / m_dDeviceRatio;
	}
	else
	{
		IDisplayTransformationPtr ipDTrans(ipTrans);
		double dResult;
		ipDTrans->ToPoints(dMapSize, &dResult);
		return dResult;
	}
}

double CLogoMarkerSymbol::PointsToMap(ITransformationPtr ipTrans, double dPointSizeSize)
{
	if (ipTrans == NULL)
		return dPointSizeSize * m_dDeviceRatio;
	else
	{
		IDisplayTransformationPtr ipDTrans(ipTrans);
		double dResult;
		ipDTrans->FromPoints(dPointSizeSize, &dResult);
		return dResult;
	}
}


void CLogoMarkerSymbol::SetupDeviceRatio(HDC hdc, IDisplayTransformationPtr ipDTrans)
{
	if (ipDTrans == NULL)
	{
		if (hdc)
			m_dDeviceRatio = ::GetDeviceCaps(hdc, LOGPIXELSX) / 72;
		else
			m_dDeviceRatio = ::GetDeviceCaps(::GetDC(NULL), LOGPIXELSX) / 72;  //use DC of the screen
	}
	else
	{
		double dDpi;
		ipDTrans->get_Resolution(&dDpi);
		if (dDpi)
		{
			m_dDeviceRatio = dDpi / 72;
			double dScale;
			ipDTrans->get_ReferenceScale(&dScale);
			if (dScale)
			{
				double dScaleRatio;
				ipDTrans->get_ScaleRatio(&dScaleRatio);
				m_dDeviceRatio = m_dDeviceRatio * dScale / dScaleRatio;
			}
		}
	}
}

void CLogoMarkerSymbol::RotateCoords()
{
	//Correct for anticlockwise rotation
	double dAngle;
	dAngle = 360 - m_dAngle + m_dMapRotation;
	if (qaDoubleCompare(dAngle, 360.0))
		return;
  double dRadians = Radians(dAngle);
  
	IAffineTransformation2DPtr ipA2D(CLSID_AffineTransformation2D);
	ipA2D->Move(-m_ptCoords[1].x, -m_ptCoords[1].y);
	ipA2D->Rotate(dRadians);
	ipA2D->Move(m_ptCoords[1].x, m_ptCoords[1].y);
	
	ITransform2DPtr ipT2D;
	double dX, dY, dCenX, dCenY, dCosRad, dSinRad;
	dCenX = m_ptCoords[1].x;
	dCenY = m_ptCoords[1].y;
	dCosRad = cos(dRadians);
	dSinRad = sin(dRadians);
	for (int i = 0; i < 4; ++i)
	{
		if (i != 1)
		{
			/*
			dX = m_ptCoords[i].x;
			dY = m_ptCoords[i].y;
			dX = dCenX + ((dX - dCenX) * dCosRad) - 
				((dY - dCenY) * dSinRad);
			dY = dCenY + ((dX - dCenX) * dSinRad) + 
				((dY - dCenY) * dCosRad);
			/**/
			IPointPtr ipPt(CLSID_Point);
			ipPt->PutCoords((double)m_ptCoords[i].x, (double)m_ptCoords[i].y);
			ipT2D = ipPt;
			ipT2D->Transform(esriTransformForward, ipA2D);
			ipPt->get_X(&dX);
			ipPt->get_Y(&dY);
			/**/

			m_ptCoords[i].x = lround(dX);
			m_ptCoords[i].y = lround(dY);
		}
	}
}

void CLogoMarkerSymbol::CalcCoords(double dX, double dY)
{
	//This function calculates the required coordinates for each symbol
	//based on the feature's geometry.  These coordinates are in Device units
	double dVal;
	dVal = sqrt(pow(m_dDeviceRadius, 2) / 2.0);
	//We account for the offset in the calculation of the center point. All other
  //points are then calculated from this.  Y coordinates are top to bottom in Y axis.
  m_ptCoords[1].x = lround(dX + m_dDeviceXOffset);
  m_ptCoords[1].y = lround(dY - m_dDeviceYOffset);

  m_ptCoords[0].x = lround(m_ptCoords[1].x - dVal);
  m_ptCoords[0].y = lround(m_ptCoords[1].y - dVal);
  m_ptCoords[3].x = lround(m_ptCoords[1].x + dVal);
  m_ptCoords[3].y = lround(m_ptCoords[1].y + dVal);
  m_ptCoords[2].x = m_ptCoords[0].x;
  m_ptCoords[2].y = m_ptCoords[3].y;
  m_ptCoords[4].x = lround(m_ptCoords[1].x - m_dDeviceRadius);
  m_ptCoords[4].y = lround(m_ptCoords[1].y - m_dDeviceRadius);
  m_ptCoords[5].x = lround(m_ptCoords[1].x + m_dDeviceRadius);
  m_ptCoords[5].y = lround(m_ptCoords[1].y + m_dDeviceRadius);

  RotateCoords();

}

void CLogoMarkerSymbol::QueryBoundsFromGeom(OLE_HANDLE hdc, IDisplayTransformationPtr ipTransform, 
	IPolygonPtr ipBoundary, IPointPtr ipPoint)
{
  //Calculate Size, XOffset and YOffset of the shape in Map units.
	double dMapSize, dMapXOffset, dMapYOffset;
	dMapSize = PointsToMap(ipTransform, m_dSize);
  
	if (qaDoubleCompare(m_dXOffset, 0.0))
		dMapXOffset = PointsToMap(ipTransform, m_dXOffset);

  if (qaDoubleCompare(m_dYOffset, 0.0))
		dMapYOffset = PointsToMap(ipTransform, m_dYOffset);
	
	double dX, dY;
	ipPoint->get_X(&dX);
	ipPoint->get_Y(&dY);
	ipPoint->PutCoords(dX + dMapXOffset, dY + dMapYOffset);

  //Set up the device ratio.
  SetupDeviceRatio((HDC)hdc, (IDisplayTransformationPtr)ipTransform);
	IPointCollectionPtr ipPtColl(ipBoundary);
	ISegmentCollectionPtr ipSegColl(ipBoundary);
	double dVal, dRad; //dVal is the measurement of the short side of a Triangles are based on.
  dRad = dMapSize / 2.0;
  dVal = sqrt((dRad * dRad / 2.0));
	ipPoint->get_X(&dX);
	ipPoint->get_Y(&dY);
	IPointPtr ipPt;
	CreatePoint((IPointPtr*)&ipPt, dX + dVal, dY - dVal);
  ipPtColl->AddPoint(ipPt);
	CreatePoint((IPointPtr*)&ipPt, dX - dVal, dY - dVal);
  ipPtColl->AddPoint(ipPt);
	CreatePoint((IPointPtr*)&ipPt, dX - dVal, dY + dVal);
  ipPtColl->AddPoint(ipPt);
	ICircularArcPtr ipCArc;
	IPointPtr ipFPt, ipTPt;
	ipPtColl->get_Point(2, &ipFPt);
	ipPtColl->get_Point(0, &ipTPt);
	CreateCArcCenterFromTo((ICircularArcPtr*)&ipCArc, ipPoint, ipFPt, ipTPt);
  ipSegColl->AddSegment((ISegmentSamplePtr)ipCArc);

  //Account for rotation also.
  if (qaDoubleCompare(m_dAngle, 0.0))
	{
		ITransform2DPtr ipTrans2D(ipBoundary);
    ipTrans2D->Rotate(ipPoint, Radians(m_dAngle));
  }
}

/////////////////////////////////////////////////////////////////////////////
// IClone
STDMETHODIMP CLogoMarkerSymbol::Clone(IClone **Clone)
{
	if (Clone == NULL)
		return E_POINTER;
	::CoCreateInstance(CLSID_LogoMarkerSymbol, NULL,CLSCTX_INPROC_SERVER  ,IID_IClone, (void**) Clone);
	IClonePtr ipSrc;
	this->QueryInterface(IID_IClone, (void**)&ipSrc);
	HRESULT hr = (*Clone)->Assign(ipSrc);
	return hr;
}

STDMETHODIMP CLogoMarkerSymbol::Assign(IClone *src)
{
	HRESULT hr;
	
	//ILogoMarkerSymbol
	ILogoMarkerSymbolPtr ipLogo;
	if (FAILED(hr = src->QueryInterface(IID_ILogoMarkerSymbol, (void**)&ipLogo)))
		return hr;
	ipLogo->get_ColorBorder(&m_ipBorderColor);
	ipLogo->get_ColorLeft(&m_ipLeftColor);
	ipLogo->get_ColorRight(&m_ipRightColor);
	ipLogo->get_ColorTop(&m_ipTopColor);

	//IMarkerSymbol
	IMarkerSymbolPtr ipMarkerSym;
	if (FAILED(hr = src->QueryInterface(IID_IMarkerSymbol, (void**)&ipMarkerSym)))
		return hr;
	ipMarkerSym->get_Angle(&m_dAngle);
	ipMarkerSym->get_Size(&m_dSize);
	ipMarkerSym->get_XOffset(&m_dXOffset);
	ipMarkerSym->get_YOffset(&m_dYOffset);

	//ISymbol
	ISymbolPtr ipSymbol;
	if (FAILED(hr = src->QueryInterface(IID_ISymbol, (void**)&ipSymbol)))
		return hr;
	ipSymbol->get_ROP2(&m_lROP2);

	//ISymbolRotation
	ISymbolRotationPtr ipSymbolRot;
	if (FAILED(hr = src->QueryInterface(IID_ISymbolRotation, (void**)&ipSymbolRot)))
		return hr;	
	ipSymbolRot->get_RotateWithTransform(&m_bRotWithTrans);
	
	//IMapLevel
	IMapLevelPtr ipMapLevel;
	if (FAILED(hr = src->QueryInterface(IID_IMapLevel, (void**)&ipMapLevel)))
		return hr;
	ipMapLevel->get_MapLevel(&m_lMapLevel);

	//IDisplayName - does not have any writable properties
	//IMarkerMask does not have any properties
	
	return S_OK;
}

STDMETHODIMP CLogoMarkerSymbol::IsEqual(IClone *other, VARIANT_BOOL *equal)
{
	if (equal == NULL)
		return E_POINTER;

	HRESULT hr;
	
	//ILogoMarkerSymbol
	ILogoMarkerSymbolPtr ipLogo;
	if (FAILED(hr = other->QueryInterface(IID_ILogoMarkerSymbol, (void**)&ipLogo)))
		return hr;
	OLE_COLOR lRGB, lOtherRGB;
	IColorPtr ipColor;
	//border
	ipLogo->get_ColorBorder(&ipColor);
	ipColor->get_RGB(&lOtherRGB);
	m_ipBorderColor->get_RGB(&lRGB);
	lRGB == lOtherRGB ? *equal = VARIANT_TRUE : VARIANT_FALSE;
	if (*equal == VARIANT_FALSE) return S_OK;
	//left
	ipLogo->get_ColorLeft(&ipColor);
	ipColor->get_RGB(&lOtherRGB);
	m_ipLeftColor->get_RGB(&lRGB);
	lRGB == lOtherRGB ? *equal = VARIANT_TRUE : VARIANT_FALSE;
	if (*equal == VARIANT_FALSE) return S_OK;
	//right
	ipLogo->get_ColorRight(&ipColor);
	ipColor->get_RGB(&lOtherRGB);
	m_ipRightColor->get_RGB(&lRGB);
	lRGB == lOtherRGB ? *equal = VARIANT_TRUE : VARIANT_FALSE;
	if (*equal == VARIANT_FALSE) return S_OK;
	//top
	ipLogo->get_ColorTop(&ipColor);
	ipColor->get_RGB(&lOtherRGB);
	m_ipTopColor->get_RGB(&lRGB);
	lRGB == lOtherRGB ? *equal = VARIANT_TRUE : VARIANT_FALSE;
	if (*equal == VARIANT_FALSE) return S_OK;

	//IMarkerSymbol
	IMarkerSymbolPtr ipMarkerSym;
	if (FAILED(hr = other->QueryInterface(IID_IMarkerSymbol, (void**)&ipMarkerSym)))
		return hr;
	double dOther;
	//angle
	ipMarkerSym->get_Angle(&dOther);
	qaDoubleCompare(m_dAngle, dOther) ? *equal = VARIANT_TRUE : VARIANT_FALSE;
	if (*equal == VARIANT_FALSE) return S_OK;
	//size
	ipMarkerSym->get_Size(&dOther);
	qaDoubleCompare(m_dSize, dOther) ? *equal = VARIANT_TRUE : VARIANT_FALSE;
	if (*equal == VARIANT_FALSE) return S_OK;
	//xoffset
	ipMarkerSym->get_XOffset(&dOther);
	qaDoubleCompare(m_dXOffset, dOther) ? *equal = VARIANT_TRUE : VARIANT_FALSE;
	if (*equal == VARIANT_FALSE) return S_OK;
	//yoffset
	ipMarkerSym->get_YOffset(&dOther);
	qaDoubleCompare(m_dYOffset, dOther) ? *equal = VARIANT_TRUE : VARIANT_FALSE;
	if (*equal == VARIANT_FALSE) return S_OK;

	//ISymbol
	ISymbolPtr ipSymbol;
	esriRasterOpCode lROPOther;
	if (FAILED(hr = other->QueryInterface(IID_ISymbol, (void**)&ipSymbol)))
		return hr;
	ipSymbol->get_ROP2(&lROPOther);
	m_lROP2 == lROPOther ? *equal = VARIANT_TRUE : VARIANT_FALSE;
	if (*equal == VARIANT_FALSE) return S_OK;

	//ISymbolRotation
	ISymbolRotationPtr ipSymbolRot;
	VARIANT_BOOL bOther;
	if (FAILED(hr = other->QueryInterface(IID_ISymbolRotation, (void**)&ipSymbolRot)))
		return hr;		
	ipSymbolRot->get_RotateWithTransform(&bOther);
	m_bRotWithTrans == bOther ? *equal = VARIANT_TRUE : VARIANT_FALSE;
	if (*equal == VARIANT_FALSE) return S_OK;
	
	//IMapLevel
	IMapLevelPtr ipMapLevel;
	long lOther;
	if (FAILED(hr = other->QueryInterface(IID_IMapLevel, (void**)&ipMapLevel)))
		return hr;
	ipMapLevel->get_MapLevel(&lOther);
	m_lMapLevel == lOther ? *equal = VARIANT_TRUE : VARIANT_FALSE;
	if (*equal == VARIANT_FALSE) return S_OK;

	//IDisplayName
	IDisplayNamePtr ipDisplayName;
	CComBSTR bsOther;
	if (FAILED(hr = other->QueryInterface(IID_IDisplayName, (void**)&ipDisplayName)))
		return hr;
	ipDisplayName->get_NameString(&bsOther);
	m_bsDisplayName  == bsOther ? *equal = VARIANT_TRUE : VARIANT_FALSE;
	if (*equal == VARIANT_FALSE) return S_OK;

	//IMarkerMask does not have any properties
	return S_OK;
}

STDMETHODIMP CLogoMarkerSymbol::IsIdentical(IClone *other, VARIANT_BOOL *identical)
{
	if (identical == NULL)
		return E_POINTER;
	*identical = VARIANT_FALSE;
	if (other == NULL)
		return S_OK;
	IUnknownPtr ipUnk, ipUnkOther;
	HRESULT hr;
	if (FAILED(hr = other->QueryInterface(IID_IUnknown, (void**)&ipUnkOther)))
		return hr;
	if (FAILED(hr = this->QueryInterface(IID_IUnknown, (void**)&ipUnk)))
		return hr;
	if (ipUnk == ipUnkOther)
		*identical = VARIANT_TRUE;

	return S_OK;
}

/////////////////////////////////////////////////////////////////////////////
// IDisplayName
STDMETHODIMP CLogoMarkerSymbol::get_NameString(BSTR *DisplayName)
{
	if (DisplayName == NULL) return E_POINTER;
	*DisplayName = ::SysAllocString(m_bsDisplayName);
	return S_OK;
}

/////////////////////////////////////////////////////////////////////////////
// IMarkerSymbol
STDMETHODIMP CLogoMarkerSymbol::get_Size(double *Size)
{
	if (! Size) return E_POINTER;
	*Size = m_dSize;
	return S_OK;
}

STDMETHODIMP CLogoMarkerSymbol::put_Size(double Size)
{
	m_dSize = Size;
	return S_OK;
}

STDMETHODIMP CLogoMarkerSymbol::get_Color(IColor **Color)
{
	if (! Color) return E_POINTER;
	IClonePtr ipClone(m_ipTopColor), ipCloned;
	ipClone->Clone(&ipCloned);
	return ipCloned.QueryInterface(IID_IColor, (void**)Color);
}

STDMETHODIMP CLogoMarkerSymbol::put_Color(IColor *Color)
{
	IClonePtr ipClone(Color), ipCloned;
	HRESULT hr = ipClone->Clone(&ipCloned);
	m_ipTopColor = ipCloned;
	return hr;
}

STDMETHODIMP CLogoMarkerSymbol::get_Angle(double *Angle)
{
	if (!Angle) return E_POINTER;
	*Angle = m_dAngle;
	return S_OK;
}

STDMETHODIMP CLogoMarkerSymbol::put_Angle(double Angle)
{
	if (Angle > 360)
		m_dAngle = Angle - (((int)(Angle / 360)) * 360);
	else
		m_dAngle = Angle;
	return S_OK;
}

STDMETHODIMP CLogoMarkerSymbol::get_XOffset(double *XOffset)
{
	if (!XOffset) return E_POINTER;
	*XOffset = m_dXOffset;
	return S_OK;
}

STDMETHODIMP CLogoMarkerSymbol::put_XOffset(double XOffset)
{
	m_dXOffset = XOffset;
	return S_OK;
}

STDMETHODIMP CLogoMarkerSymbol::get_YOffset(double *YOffset)
{
	if (! YOffset) return E_POINTER;
	*YOffset = m_dYOffset;
	return S_OK;
}

STDMETHODIMP CLogoMarkerSymbol::put_YOffset(double YOffset)
{
	m_dYOffset = YOffset;
	return S_OK;
}

/////////////////////////////////////////////////////////////////////////////
// IPersistVariant
STDMETHODIMP CLogoMarkerSymbol::get_ID(IUID **objectID)
{
	if (! objectID) return E_POINTER;
	LPOLESTR lpTmp;
	StringFromCLSID(CLSID_LogoMarkerSymbol, &lpTmp);
	CComBSTR bstTmp(lpTmp);
	CComVariant vVariant(bstTmp);

	IUIDPtr ipUID(CLSID_UID);
	HRESULT hr = ipUID->put_Value(vVariant);
	if (FAILED(hr)) return hr;

	*objectID = ipUID;
	if (*objectID != 0)
		(*objectID)->AddRef();
	
	return S_OK;
}

STDMETHODIMP CLogoMarkerSymbol::Load(IVariantStream *Stream)
{
	CComVariant vLoad;
	HRESULT hr;

	//load ISymbol properties 
	if (FAILED(hr = Stream->Read(&vLoad))) return hr;
	if (vLoad.vt == VT_I4)
		m_lROP2 = (esriRasterOpCode)vLoad.lVal;
	else
		return E_FAIL;
	vLoad.Clear();

	//load IMarkerSymbol properties
	if (FAILED(hr = Stream->Read(&vLoad))) return hr;
	if (vLoad.vt == VT_R8)
		m_dSize = vLoad.dblVal;
	else
		return E_FAIL;
	vLoad.Clear();

	if (FAILED(hr = Stream->Read(&vLoad))) return hr;
	if (vLoad.vt == VT_R8)
		m_dXOffset = vLoad.dblVal;
	else
		return E_FAIL;
	vLoad.Clear();
	
	if (FAILED(hr = Stream->Read(&vLoad))) return hr;
	if (vLoad.vt == VT_R8)
		m_dYOffset = vLoad.dblVal;
	else
		return E_FAIL;
	vLoad.Clear();
	
	if (FAILED(hr = Stream->Read(&vLoad))) return hr;
	if (vLoad.vt == VT_R8)
		m_dAngle = vLoad.dblVal;
	else
		return E_FAIL;
	vLoad.Clear();

	//load ISymbolRotation properties
	if (FAILED(hr = Stream->Read(&vLoad))) return hr;
	if (vLoad.vt == VT_BOOL)
		m_bRotWithTrans = vLoad.boolVal;
	else
		return E_FAIL;
	vLoad.Clear();

	//load IMapLevel properties
	if (FAILED(hr = Stream->Read(&vLoad))) return hr;
	if (vLoad.vt == VT_I4)
		m_lMapLevel = vLoad.lVal;
	else
		return E_FAIL;
	vLoad.Clear();

	//load custom properties
	if (FAILED(hr = Stream->Read(&vLoad))) return hr;
	if (vLoad.vt == VT_UNKNOWN)
		m_ipTopColor = (IColorPtr) vLoad.punkVal;
	else
		return E_FAIL;
	vLoad.Clear();
	
	if (FAILED(hr = Stream->Read(&vLoad))) return hr;
	if (vLoad.vt == VT_UNKNOWN)
		m_ipLeftColor = (IColorPtr) vLoad.punkVal;
	else
		return E_FAIL;
	vLoad.Clear();

	if (FAILED(hr = Stream->Read(&vLoad))) return hr;
	if (vLoad.vt == VT_UNKNOWN)
		m_ipRightColor = (IColorPtr) vLoad.punkVal;
	else
		return E_FAIL;
	vLoad.Clear();

	if (FAILED(hr = Stream->Read(&vLoad))) return hr;
	if (vLoad.vt == VT_UNKNOWN)
		m_ipBorderColor = (IColorPtr) vLoad.punkVal;
	else
		return E_FAIL;

	return S_OK;
}

STDMETHODIMP CLogoMarkerSymbol::Save(IVariantStream *Stream)
{
	CComVariant vSave;
	HRESULT hr;
	
	//persist ISymbol properties 
	vSave = (long)m_lROP2;
	if (FAILED(hr = Stream->Write(vSave))) return hr;

	//persist IMarkerSymbol properties
	vSave = m_dSize;
	if (FAILED(hr = Stream->Write(vSave))) return hr;
	vSave = m_dXOffset;
	if (FAILED(hr = Stream->Write(vSave))) return hr;
	vSave = m_dYOffset;
	if (FAILED(hr = Stream->Write(vSave))) return hr;
	vSave = m_dAngle;
	if (FAILED(hr = Stream->Write(vSave))) return hr;
	
	//persist ISymbolRotation properties
	// CComVariant does not have assignment operator
	// for VARIANT_BOOL, so do this as for the raw datatype
	vSave.Clear();
	vSave.vt = VT_BOOL;
	vSave.boolVal = m_bRotWithTrans;
	if (FAILED(hr = Stream->Write(vSave))) return hr;

	//persist IMapLevel properties
	vSave = m_lMapLevel;
	if (FAILED(hr = Stream->Write(vSave))) return hr;

	//persist custom properties
	vSave = (IUnknown*)m_ipTopColor;
	if (FAILED(hr = Stream->Write(vSave))) return hr;
	vSave = (IUnknown*)m_ipLeftColor;
	if (FAILED(hr = Stream->Write(vSave))) return hr;
	vSave = (IUnknown*)m_ipRightColor;
	if (FAILED(hr = Stream->Write(vSave))) return hr;
	vSave = (IUnknown*)m_ipBorderColor;
	if (FAILED(hr = Stream->Write(vSave))) return hr;

	return S_OK;
}

/////////////////////////////////////////////////////////////////////////////
// ISymbol
STDMETHODIMP CLogoMarkerSymbol::SetupDC(OLE_HANDLE hDC, ITransformation *Transformation)
{
	//Store the DisplayTransformation and display handle for use by Draw and ResetDC.
  m_ipTrans = Transformation;
  m_lhDC = (HDC)hDC;

  //Set up the device ratio for use by Draw and the rest of SetupDC.
	IDisplayTransformationPtr ipDTrans(Transformation);
  SetupDeviceRatio(m_lhDC, m_ipTrans);

  //Calculate the new Radius for the symbol from the Size (width) overall.
  m_dDeviceRadius = (m_dSize / 2) * m_dDeviceRatio;
  m_dDeviceXOffset = m_dXOffset * m_dDeviceRatio;
  m_dDeviceYOffset = m_dYOffset * m_dDeviceRatio;

  //Check if we need to rotate the symbol based on the ISymbolRotation interface.
  if (m_bRotWithTrans == VARIANT_TRUE)
    m_ipTrans->get_Rotation(&m_dMapRotation);
	else
    m_dMapRotation = 0;

  //Setup the Pen which is used to outline the shapes.
	OLE_COLOR lRGB;
	m_ipBorderColor->get_RGB(&lRGB);
  m_lPen = ::CreatePen(PS_SOLID, (int)m_dDeviceRatio, lRGB);  // * m_dDeviceRatio allows the pen size to scale

  //Set the appropriate raster operation code for this draw, according to the
  //ISymbol interface.
  m_lROP2Old = (esriRasterOpCode)::SetROP2(m_lhDC, (int)m_lROP2);

  //Set up three solid brushes to fill in the shapes with the different color fills.
	m_ipTopColor->get_RGB(&lRGB);
  m_lBrushTop = CreateSolidBrush(lRGB);
 	m_ipLeftColor->get_RGB(&lRGB);
	m_lBrushLeft = CreateSolidBrush(lRGB);
	m_ipRightColor->get_RGB(&lRGB);
  m_lBrushRight = CreateSolidBrush(lRGB);

  //Select in the new pen and store the old pen - essential for use during cleanup.
  m_lOldPen = (HPEN)::SelectObject(m_lhDC, m_lPen);

	return S_OK;
}

STDMETHODIMP CLogoMarkerSymbol::ResetDC()
{
	//Select back the old GDI pen and ROP code, and release other GDI resources
  m_lROP2 = (esriRasterOpCode)::SetROP2(m_lhDC, (int)m_lROP2Old);
  SelectObject(m_lhDC, m_lOldPen);
  DeleteObject(m_lPen);
  SelectObject(m_lhDC, m_lOldBrush);
  DeleteObject (m_lBrushTop);
  DeleteObject (m_lBrushLeft);
  DeleteObject (m_lBrushRight);
  m_ipTrans = 0;
  m_lhDC = 0;
	return S_OK;
}

STDMETHODIMP CLogoMarkerSymbol::Draw(IGeometry *Geometry)
{
  if (!Geometry)
    return E_POINTER;

  if (!m_lhDC)
    return E_FAIL;

  if (m_ipTopColor == NULL ||
			m_ipLeftColor == NULL ||
			m_ipRightColor == NULL ||
			m_ipBorderColor == NULL)
    return E_FAIL;

	IPointPtr ipPt(Geometry);
	if (ipPt == NULL)
		return E_FAIL;

  VARIANT_BOOL dummy;
  if (Geometry->get_IsEmpty(&dummy) == S_OK)
    return S_OK;

	//Transform the Point coords to device coords, accounting for rotation, offset etc.
	double dCenterX, dCenterY;
	FromMapPoint((IDisplayTransformation*)m_ipTrans, ipPt, &dCenterX, &dCenterY); 
  CalcCoords(dCenterX, dCenterY);

  //Draw the chord, and two polygons, and flood fill them.
  m_lOldBrush = (HBRUSH)::SelectObject(m_lhDC, m_lBrushTop);
	::Chord(m_lhDC, m_ptCoords[4].x, m_ptCoords[4].y, m_ptCoords[5].x, 
		m_ptCoords[5].y, m_ptCoords[3].x, m_ptCoords[3].y, 
		m_ptCoords[0].x, m_ptCoords[0].y);

  ::SelectObject(m_lhDC, m_lBrushLeft);
  ::Polygon(m_lhDC, &m_ptCoords[0], 3);


  ::SelectObject(m_lhDC, m_lBrushRight);
  ::Polygon(m_lhDC, &m_ptCoords[1], 3);

  ::SelectObject(m_lhDC, m_lOldBrush);

	return S_OK;
}

STDMETHODIMP CLogoMarkerSymbol::QueryBoundary(OLE_HANDLE hDC, 
	ITransformation *displayTransform, IGeometry *Geometry, IPolygon *Boundary)
{
  //Check input parameters. Boundary may be a preexisting Polygon, so
  //make sure it's geometry is cleared.
  if (Geometry == NULL || Boundary == NULL)
		return E_POINTER;
	
	IPointPtr ipPt(Geometry);
	if (ipPt == NULL)
		return E_FAIL;
	Boundary->SetEmpty();
    
	IDisplayTransformationPtr ipDTrans(displayTransform);
  QueryBoundsFromGeom(hDC, ipDTrans, Boundary, ipPt);
	return S_OK;
}

STDMETHODIMP CLogoMarkerSymbol::get_ROP2(esriRasterOpCode *DrawMode)
{
	if (!DrawMode) return E_POINTER;
	*DrawMode = m_lROP2;

	return S_OK;
}

STDMETHODIMP CLogoMarkerSymbol::put_ROP2(esriRasterOpCode DrawMode)
{
	if (DrawMode < 1)
		m_lROP2 = esriROPCopyPen;
  else
    m_lROP2 = DrawMode;

	return S_OK;
}

/////////////////////////////////////////////////////////////////////////////
// IMapLevel
STDMETHODIMP CLogoMarkerSymbol::get_MapLevel(long *MapLevel)
{
	if (!MapLevel) return E_POINTER;
	*MapLevel = m_lMapLevel;

	return S_OK;
}

STDMETHODIMP CLogoMarkerSymbol::put_MapLevel(long MapLevel)
{
	m_lMapLevel = MapLevel;
	return S_OK;
}

/////////////////////////////////////////////////////////////////////////////
// IMarkerMask
STDMETHODIMP CLogoMarkerSymbol::QueryMarkerMask(OLE_HANDLE hDC, 
	ITransformation *Transform, IGeometry *Geometry, IPolygon *Boundary)
{

  //Code QueryBoundary using same steps as Draw. But add a step where
  //Points are converted to Map units, and then build an appropriate Polygon.
	if (Geometry == NULL || Boundary == NULL)
		return E_FAIL;
	
	IDisplayTransformationPtr ipDTrans(Transform);
	if (ipDTrans == NULL)
		return E_FAIL;
	IPointPtr ipPoint(Geometry);
	if (ipPoint == NULL)
		return E_FAIL;
  Boundary->SetEmpty();
  QueryBoundsFromGeom(hDC, ipDTrans, Boundary, ipPoint);

  //Unlike ISymbol_QueryBoundary, QueryMarkerMask requires a Simple geometry.
	ITopologicalOperatorPtr ipTopo(Boundary);
	VARIANT_BOOL bSimple;

	ipTopo->get_IsKnownSimple(&bSimple);
  if (bSimple == VARIANT_FALSE)
	{
		ipTopo->get_IsSimple(&bSimple);
		if (bSimple == VARIANT_FALSE)
		{
      ipTopo->Simplify();
		}
	}
	return S_OK;
}

/////////////////////////////////////////////////////////////////////////////
// IPropertySupport
STDMETHODIMP CLogoMarkerSymbol::Applies(LPUNKNOWN pUnk, VARIANT_BOOL *Applies)
{
  if (!Applies)
    return E_POINTER;

  *Applies = VARIANT_FALSE;

  IColorPtr ipColor(pUnk);
  ILogoMarkerSymbolPtr ipLogo(pUnk);
  if (ipColor != NULL && ipLogo != NULL)
    *Applies = VARIANT_TRUE;

  return S_OK;
}

STDMETHODIMP CLogoMarkerSymbol::CanApply(LPUNKNOWN pUnk, VARIANT_BOOL *CanApply)
{
  return Applies(pUnk, CanApply);
}

STDMETHODIMP CLogoMarkerSymbol::get_Current(LPUNKNOWN pUnk, LPUNKNOWN *currentObject)
{
  IColorPtr ipColor(pUnk);
  if (ipColor)
  {
    IColorPtr ipCurrentColor;
    get_Color(&ipCurrentColor);
    ipCurrentColor.QueryInterface(IID_IUnknown, (void**)currentObject);
    return S_OK;
  }

  ILogoMarkerSymbolPtr ipSymbol(pUnk);
  if (ipSymbol)
  {
    IClonePtr ipClone;
    Clone(&ipClone);
    ipClone.QueryInterface(IID_IUnknown, (void**)currentObject);
    return S_OK;
  }
  return E_FAIL;
}

STDMETHODIMP CLogoMarkerSymbol::Apply(LPUNKNOWN NewObject, LPUNKNOWN *oldObject)
{
  IColorPtr ipColor(NewObject);
  if (ipColor)
  {
    get_Current(NewObject, oldObject);
    put_Color(ipColor);
    return S_OK;
  }  

  ILogoMarkerSymbolPtr ipSymbol(NewObject);
  if (ipSymbol)
  {
    get_Current(NewObject, oldObject);
    IClonePtr ipClone(NewObject);
    Assign(ipClone);
    return S_OK;
  }

  return E_FAIL;
}

/////////////////////////////////////////////////////////////////////////////
// ISymbolRotation
STDMETHODIMP CLogoMarkerSymbol::get_RotateWithTransform(VARIANT_BOOL *Flag)
{
	if (! Flag) return E_POINTER;
	*Flag = m_bRotWithTrans;
	return S_OK;
}

STDMETHODIMP CLogoMarkerSymbol::put_RotateWithTransform(VARIANT_BOOL Flag)
{
	m_bRotWithTrans = Flag;
	return S_OK;
}

/////////////////////////////////////////////////////////////////////////////
// ILogoMarkerSymbol
STDMETHODIMP CLogoMarkerSymbol::get_ColorBorder(IColor **ppColor)
{
	IClonePtr ipSrc(m_ipBorderColor), ipClone;
	HRESULT hr = ipSrc->Clone(&ipClone);
	return ipClone.QueryInterface(IID_IColor, (void**)ppColor);
}

STDMETHODIMP CLogoMarkerSymbol::put_ColorBorder(IColor *pColor)
{
	IClonePtr ipClone(pColor), ipCloned;
	HRESULT hr = ipClone->Clone(&ipCloned);
	m_ipBorderColor = ipCloned;
	return hr;
}

STDMETHODIMP CLogoMarkerSymbol::get_ColorLeft(IColor **ppColor)
{
	IClonePtr ipClone(m_ipLeftColor), ipCloned;
	ipClone->Clone(&ipCloned);
	return ipCloned.QueryInterface(IID_IColor, (void**)ppColor);
}

STDMETHODIMP CLogoMarkerSymbol::put_ColorLeft(IColor *pColor)
{
	IClonePtr ipClone(pColor), ipCloned;
	HRESULT hr = ipClone->Clone(&ipCloned);
	m_ipLeftColor = ipCloned;
	return hr;
}

STDMETHODIMP CLogoMarkerSymbol::get_ColorRight(IColor **ppColor)
{
	IClonePtr ipClone(m_ipRightColor), ipCloned;
	ipClone->Clone(&ipCloned);
	return ipCloned.QueryInterface(IID_IColor, (void**)ppColor);
}

STDMETHODIMP CLogoMarkerSymbol::put_ColorRight(IColor *pColor)
{
	IClonePtr ipClone(pColor), ipCloned;
	HRESULT hr = ipClone->Clone(&ipCloned);
	m_ipRightColor = ipCloned;
	return hr;
}

STDMETHODIMP CLogoMarkerSymbol::get_ColorTop(IColor **ppColor)
{
	IClonePtr ipClone(m_ipTopColor), ipCloned;
	ipClone->Clone(&ipCloned);
	return ipCloned.QueryInterface(IID_IColor, (void**)ppColor);
}

STDMETHODIMP CLogoMarkerSymbol::put_ColorTop(IColor *pColor)
{
	IClonePtr ipClone(pColor), ipCloned;
	HRESULT hr = ipClone->Clone(&ipCloned);
	m_ipTopColor = ipCloned;
	return hr;
}


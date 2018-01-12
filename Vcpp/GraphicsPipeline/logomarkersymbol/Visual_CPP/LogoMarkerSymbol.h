/*

   Copyright 2016 Esri

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

   See the License for the specific language governing permissions and
   limitations under the License.

*/
// Copyright 2015 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at <your ArcGIS install location>/DeveloperKit10.6/userestrictions.txt.
// 


// LogoMarkerSymbol.h : Declaration of the CLogoMarkerSymbol

#ifndef __LOGOMARKERSYMBOL_H_
#define __LOGOMARKERSYMBOL_H_

#include "resource.h"       // main symbols
_COM_SMARTPTR_TYPEDEF(ILogoMarkerSymbol, __uuidof(ILogoMarkerSymbol));
/////////////////////////////////////////////////////////////////////////////
// CLogoMarkerSymbol
class ATL_NO_VTABLE CLogoMarkerSymbol : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CLogoMarkerSymbol, &CLSID_LogoMarkerSymbol>,
	public ILogoMarkerSymbol,
	//required
	public IClone,
	public IDisplayName,
	public IMarkerSymbol,
	public IPersistVariant,
	public ISymbol,
	//optional
	public IMapLevel,
	public IMarkerMask,
	public IPropertySupport,
	public ISymbolRotation
{
public:
	CLogoMarkerSymbol();
	~CLogoMarkerSymbol();

DECLARE_REGISTRY_RESOURCEID(IDR_LOGOMARKERSYMBOL)

DECLARE_PROTECT_FINAL_CONSTRUCT()

BEGIN_COM_MAP(CLogoMarkerSymbol)
	COM_INTERFACE_ENTRY(ILogoMarkerSymbol)
	COM_INTERFACE_ENTRY(IClone)
	COM_INTERFACE_ENTRY(IDisplayName)
	COM_INTERFACE_ENTRY(IMarkerSymbol)
	COM_INTERFACE_ENTRY(IPersistVariant)
	COM_INTERFACE_ENTRY(ISymbol)
	COM_INTERFACE_ENTRY(IMapLevel)
	COM_INTERFACE_ENTRY(IMarkerMask)
	COM_INTERFACE_ENTRY(IPropertySupport)
	COM_INTERFACE_ENTRY(ISymbolRotation)
END_COM_MAP()

BEGIN_CATEGORY_MAP(CLogoMarkerPropertyPage)
	IMPLEMENTED_CATEGORY(__uuidof(CATID_MarkerSymbol))
END_CATEGORY_MAP()

// ILogoMarkerSymbol
public:
	STDMETHOD(get_ColorTop)(/*[out, retval]*/ IColor* *ppColor);
	STDMETHOD(put_ColorTop)(/*[in]*/ IColor* pColor);
	STDMETHOD(get_ColorRight)(/*[out, retval]*/ IColor* *ppColor);
	STDMETHOD(put_ColorRight)(/*[in]*/ IColor* pColor);
	STDMETHOD(get_ColorLeft)(/*[out, retval]*/ IColor* *ppColor);
	STDMETHOD(put_ColorLeft)(/*[in]*/ IColor* pColor);
	STDMETHOD(get_ColorBorder)(/*[out, retval]*/ IColor* *ppColor);
	STDMETHOD(put_ColorBorder)(/*[in]*/ IColor* pColor);
// IClone
	STDMETHOD(Clone)(IClone **Clone);
	STDMETHOD(Assign)(IClone *src);
	STDMETHOD(IsEqual)(IClone *other, VARIANT_BOOL *equal);
	STDMETHOD(IsIdentical)(IClone *other, VARIANT_BOOL *identical);

// IDisplayName
	STDMETHOD(get_NameString)(BSTR *DisplayName);

// IMarkerSymbol
	STDMETHOD(get_Size)(double *Size);
	STDMETHOD(put_Size)(double Size);
	STDMETHOD(get_Color)(IColor **Color);
	STDMETHOD(put_Color)(IColor *Color);
	STDMETHOD(get_Angle)(double *Angle);
	STDMETHOD(put_Angle)(double Angle);
	STDMETHOD(get_XOffset)(double *XOffset);
	STDMETHOD(put_XOffset)(double XOffset);
	STDMETHOD(get_YOffset)(double *YOffset);
	STDMETHOD(put_YOffset)(double YOffset);

// IPersistVariant
	STDMETHOD(get_ID)(IUID **objectID);
	STDMETHOD(Load)(IVariantStream *Stream);
	STDMETHOD(Save)(IVariantStream *Stream);

// ISymbol
	STDMETHOD(SetupDC)(OLE_HANDLE hDC, ITransformation *Transformation);
	STDMETHOD(ResetDC)();
	STDMETHOD(Draw)(IGeometry *Geometry);
	STDMETHOD(QueryBoundary)(OLE_HANDLE hDC, ITransformation *displayTransform, IGeometry *Geometry, IPolygon *Boundary);
	STDMETHOD(get_ROP2)(esriRasterOpCode *DrawMode);
	STDMETHOD(put_ROP2)(esriRasterOpCode DrawMode);

// IMapLevel
	STDMETHOD(get_MapLevel)(long *MapLevel);
	STDMETHOD(put_MapLevel)(long MapLevel);

// IMarkerMask
	STDMETHOD(QueryMarkerMask)(OLE_HANDLE hDC, ITransformation *Transform, IGeometry *Geometry, IPolygon *Boundary);

// IPropertySupport
	STDMETHOD(Applies)(LPUNKNOWN pUnk, VARIANT_BOOL *Applies);
	STDMETHOD(CanApply)(LPUNKNOWN pUnk, VARIANT_BOOL *CanApply);
	STDMETHOD(get_Current)(LPUNKNOWN pUnk, LPUNKNOWN *currentObject);
	STDMETHOD(Apply)(LPUNKNOWN NewObject, LPUNKNOWN *oldObject);

// ISymbolRotation
	STDMETHOD(get_RotateWithTransform)(VARIANT_BOOL *Flag);
	STDMETHOD(put_RotateWithTransform)(VARIANT_BOOL Flag);

private:

	void SetupDeviceRatio(HDC hdc, IDisplayTransformationPtr ipDTrans);
	double MapToPoints(ITransformationPtr ipTrans, double dMapSize);
	double PointsToMap(ITransformationPtr ipTrans, double dPointSizeSize);
	void RotateCoords();
	void CalcCoords(double dX, double dY);
	void QueryBoundsFromGeom(OLE_HANDLE hdc, IDisplayTransformationPtr ipTransform, 
		IPolygonPtr ipBoundary, IPointPtr ipPoint);

	const double m_dPi;

  //These variables store the device context, transformation and pens used for
  //drawing the different sections of the Marker. The transformation is set in
  //ISymbol_SetupDC, and used in ISymbol_Draw.
	IDisplayTransformationPtr	m_ipTrans;
	
	HDC										m_lhDC;
  HPEN                  m_lPen;
  HPEN                  m_lOldPen;
  HBRUSH                m_lBrushTop;
  HBRUSH                m_lBrushLeft;
  HBRUSH                m_lBrushRight;
  HBRUSH                m_lOldBrush;
	esriRasterOpCode			m_lROP2Old;


  //The device ratio is the ratio of screen resolution to Points.  Note that the screen resolution
  //is not strictly dependent on the output device, but also on the resolution in the DisplayTransformation
  //(which is driven in part by the zoom level in layout mode). Also
  //offset values in device units.
	double								m_dDeviceRatio;
	double								m_dDeviceXOffset;
	double								m_dDeviceYOffset;

  //This variable stores the device coordinates for each of the control points
  //for the logo, for use in the Chord and Polygon functions. Also stored points
  //of the interior of each shape for the FloodFill function to use.
	POINT                 m_ptCoords[6];


  //The r value calculated from the Size (width) of the symbol, in PrintPoints.
	double								m_dDeviceRadius;

  //These members relate directly to the LogoMarkerSymbol
  IColorPtr             m_ipTopColor;
  IColorPtr             m_ipLeftColor;
  IColorPtr             m_ipRightColor;
  IColorPtr             m_ipBorderColor;

  //These members hold properties of the ISymbol interface.
	esriRasterOpCode			m_lROP2;

  //These members hold properties of the IMarkerSymbol interface. The Color property on
  //this interface points to m_pColorTop.
	double								m_dSize;
	double								m_dXOffset;
	double								m_dYOffset;
	double								m_dAngle;

  //These members hold properties of the IDisplayName interface.
	const _bstr_t					m_bsDisplayName;

  //These members hold properties used by the ISymbolRotation interface.
	VARIANT_BOOL					m_bRotWithTrans;
	double								m_dMapRotation;

  //These members hold properties of the IMapLevel interface.
	long									m_lMapLevel;

/*  double                m_angle;  // Counter clockwise angle w.r.t X-Axis
  IEnvelopePtr          m_ipBoundaryEnvelope;
  bool                  m_nullColor;
*/
};

#endif //__LOGOMARKERSYMBOL_H_



// AddGeographicPoint.cpp : Implementation of CAddGeographicPoint
#include "stdafx.h"
#include "GlobeDigitizePoint.h"
#include "AddGeographicPoint.h"
#include "CoordInputDlg.h"

/////////////////////////////////////////////////////////////////////////////
// CAddGeographicPoint

CAddGeographicPoint::CAddGeographicPoint() : m_ipGlobeHookHelper(0),
																						 m_ipGlobeViewUtil(0),
                                             m_ipUnitConverter(0),   
                                             m_dwGlobeDisplayEventsCookie(0),
                                             m_dwActiveViewEventsCookie(0),
                                             m_bDrawPoint(false)
{
  m_hBitmap = ::LoadBitmap(_Module.m_hInst, MAKEINTRESOURCE(IDB_BITMAP2));
}

CAddGeographicPoint::~CAddGeographicPoint()
{
  TurnOffGlobeDisplayEvents();
  
  ::DeleteObject(m_hBitmap);
}

HRESULT CAddGeographicPoint::FinalConstruct()
{
	HRESULT hr = S_OK;

  return hr;
}

void CAddGeographicPoint::FinalRelease()
{
  m_ipUnitConverter   = NULL;
}

STDMETHODIMP CAddGeographicPoint::InterfaceSupportsErrorInfo(REFIID riid)
{
	static const IID* arr[] = 
	{
		&IID_IAddGeographicPoint
	};
	for (int i=0; i < sizeof(arr) / sizeof(arr[0]); i++)
	{
		if (InlineIsEqualGUID(*arr[i],riid))
			return S_OK;
	}
	return S_FALSE;
}

//ICommand
STDMETHODIMP CAddGeographicPoint::get_Enabled(VARIANT_BOOL * Enabled)
{
	if (Enabled == NULL)
		return E_POINTER;
	
  * Enabled = VARIANT_TRUE;

	return S_OK;
}
STDMETHODIMP CAddGeographicPoint::get_Checked(VARIANT_BOOL * Checked)
{
	if (Checked == NULL)
		return E_POINTER;

  *Checked = VARIANT_FALSE;
		
	return E_NOTIMPL;
}
STDMETHODIMP CAddGeographicPoint::get_Name(BSTR * Name)
{
	if (Name == NULL)
		return E_POINTER;
	
  *Name = ::SysAllocString(L"AddGeographicPoint");

	return S_OK;
}
STDMETHODIMP CAddGeographicPoint::get_Caption(BSTR * Caption)
{
	if (Caption == NULL)
		return E_POINTER;
	
  *Caption = ::SysAllocString(L"Add Geographic Point");
	
  return S_OK;
}
STDMETHODIMP CAddGeographicPoint::get_Tooltip(BSTR * Tooltip)
{
	if (Tooltip == NULL)
		return E_POINTER;
		
	*Tooltip = ::SysAllocString(L"Add Geographic Point");
  
  return S_OK;
}
STDMETHODIMP CAddGeographicPoint::get_Message(BSTR * Message)
{
	if (Message == NULL)
		return E_POINTER;
		
	*Message = ::SysAllocString(L"Add Geographic Point");
  
  return S_OK;
}
STDMETHODIMP CAddGeographicPoint::get_HelpFile(BSTR * HelpFile)
{
	if (HelpFile == NULL)
		return E_POINTER;
		
	return E_NOTIMPL;
}
STDMETHODIMP CAddGeographicPoint::get_HelpContextID(LONG * helpID)
{
	if (helpID == NULL)
		return E_POINTER;
		
	return E_NOTIMPL;
}
STDMETHODIMP CAddGeographicPoint::get_Bitmap(OLE_HANDLE * Bitmap)
{
	if (Bitmap == NULL)
		return E_POINTER;
		
  *Bitmap = (OLE_HANDLE) m_hBitmap;
  
  return S_OK;
}
STDMETHODIMP CAddGeographicPoint::get_Category(BSTR * categoryName)
{
	if (categoryName == NULL)
		return E_POINTER;
		
	*categoryName = ::SysAllocString(L"C++ Samples");
  
  return S_OK;
}
STDMETHODIMP CAddGeographicPoint::OnCreate(IDispatch * hook)
{
  HRESULT hr = S_OK;

  hr = m_ipGlobeHookHelper.CreateInstance(CLSID_GlobeHookHelper);
	if(FAILED(hr))
		ATLASSERT(_T("Error initializing hook helper class."));

	//set the hook
	m_ipGlobeHookHelper->putref_Hook(hook);
		

   hr = m_ipGlobeHookHelper->get_Globe(&m_ipGlobe);
  if(FAILED(hr))
		ATLASSERT(_T("Erorr getting globe"));

  hr = m_ipGlobeHookHelper->get_GlobeDisplay(&m_ipGlobeDisplay);
  if(FAILED(hr))
		ATLASSERT(_T("Erorr getting GlobeDisplay"));

  //start listening to GlobeDisplayEvents
  hr = TurnOnGlobeDisplayEvents();

  return hr;
}
STDMETHODIMP CAddGeographicPoint::OnClick()
{
  HRESULT hr = S_OK;
 

  hr = m_ipGlobeDisplay->get_ActiveViewer(&m_ipViewer);
  if(FAILED(hr))
		ATLASSERT(_T("Error getting Active Viewer"));

	ICameraPtr ipCamera;
  m_ipViewer->get_Camera(&ipCamera);
	m_ipGlobeViewUtil = ipCamera;
  
  CCoordInputDlg dlg;
  if(dlg.DoModal() == IDCANCEL) //THE USER HAS CLICKED CANCEL
	{
		return S_OK;
	}
  
  //get the coordinate from the dialog 
  USES_CONVERSION;
  double dLon = 0;
  if (dlg.m_bstrLon.Length()!=0)
    dLon = atof(W2A(dlg.m_bstrLon));  
  double dLat = 0;
  if (dlg.m_bstrLat.Length()!=0)
    dLat = atof(W2A(dlg.m_bstrLat));
    

  //convert the point to geocentric coordinate system and add it to the points vector
  //In this sample, the elevation abouve the earth is assumed to be 1KM
  WKSPointZ wksPoint;
  m_ipGlobeViewUtil->GeographicToGeocentric(dLon, dLat, 1.0, &wksPoint.X, &wksPoint.Y, &wksPoint.Z);

  //Add the new point to the points vector
  
  m_OGLPoints.push_back(wksPoint);
  
  //refresh the screen in order to draw the point
  
  m_ipViewer->Redraw(VARIANT_FALSE);

  return hr;
}

// IGlobeDisplayEvents
STDMETHODIMP CAddGeographicPoint::ActiveViewerChanged(ISceneViewer * pViewer)
{
	return E_NOTIMPL;
}
STDMETHODIMP CAddGeographicPoint::ViewerAdded(ISceneViewer * pViewer)
{
	return E_NOTIMPL;
}
STDMETHODIMP CAddGeographicPoint::ViewerRemoved(ISceneViewer * pViewer)
{
	return E_NOTIMPL;
}
STDMETHODIMP CAddGeographicPoint::InteractionStopped()
{
	return E_NOTIMPL;
}
STDMETHODIMP CAddGeographicPoint::BatchTileGenerationStarted(BSTR Name)
{
	return E_NOTIMPL;
}
STDMETHODIMP CAddGeographicPoint::BatchTileGenerationStopped()
{
	return E_NOTIMPL;
}
STDMETHODIMP CAddGeographicPoint::BeforeDraw(ISceneViewer * pViewer, VARIANT_BOOL * pbHandled)
{
	if (pbHandled == NULL)
		return E_POINTER;
		
	return E_NOTIMPL;
}
STDMETHODIMP CAddGeographicPoint::AfterDraw(ISceneViewer * pViewer)
{
  ATLTRACE(_T("CAddGeographicPoint::AfterDraw"));

  //draw the points on the globe
  WKSPointZ OGLPoint;
	glPushAttrib(GL_ALL_ATTRIB_BITS);
	glDisable(GL_DEPTH_TEST);
	for(OGLPoints::iterator it = m_OGLPoints.begin(); it != m_OGLPoints.end(); it++)
	{
      OGLPoint = (WKSPointZ)(*it); 		
      glPointSize(20.0);
      glColor3ub(255,0,0);
      glBegin(GL_POINTS);
        glVertex3f((GLfloat)OGLPoint.X, (GLfloat)OGLPoint.Y, (GLfloat)OGLPoint.Z);
      glEnd();
	} 
  glPopAttrib();


	return S_OK;
}
STDMETHODIMP CAddGeographicPoint::VectorOverflow(ILayer * pLayer)
{
	return E_NOTIMPL;
}
STDMETHODIMP CAddGeographicPoint::TileOverflow(ILayer * pLayer)
{
	return E_NOTIMPL;
}

////////////////////////////////////////////////////////////////////
//private helper methods

HRESULT CAddGeographicPoint::TurnOnGlobeDisplayEvents()
{
    ATLTRACE(_T("CAddGeographicPoint::TurnOnGlobeDisplayEvents()\n"));


    HRESULT hr = S_OK;

    if (m_dwGlobeDisplayEventsCookie)
        return hr;

	  hr = AtlAdvise(m_ipGlobeDisplay, IUnknownPtr(this), __uuidof(IGlobeDisplayEvents), &m_dwGlobeDisplayEventsCookie);
	
    return hr;

}

HRESULT CAddGeographicPoint::TurnOffGlobeDisplayEvents()
{
    ATLTRACE(_T("CAddGeographicPoint::TurnOnGlobeDisplayEvents()\n"));


    HRESULT hr = S_OK;

    if (m_dwGlobeDisplayEventsCookie)
    {
		  hr = AtlUnadvise(m_ipGlobeDisplay, __uuidof(IGlobeDisplayEvents), m_dwGlobeDisplayEventsCookie);
    }

    return hr;
}
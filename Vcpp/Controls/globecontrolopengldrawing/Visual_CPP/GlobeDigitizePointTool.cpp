

// GlobeDigitizePointTool.cpp : Implementation of CGlobeDigitizePointTool
#include "stdafx.h"
#include "GlobeDigitizePoint.h"
#include "GlobeDigitizePointTool.h"


/////////////////////////////////////////////////////////////////////////////
// CGlobeDigitizePointTool

CGlobeDigitizePointTool::CGlobeDigitizePointTool() : m_ipGlobeHookHelper(0),
m_ipGlobeViewUtil(0),
m_dwGlobeDisplayEventsCookie(0),
m_dwActiveViewEventsCookie(0),
m_bDrawPoint(false)
{
  m_hCursor = ::LoadCursor(_Module.m_hInst, MAKEINTRESOURCE(IDC_CURSOR));
  m_hBitmap = ::LoadBitmap(_Module.m_hInst, MAKEINTRESOURCE(IDB_BITMAP));
}

CGlobeDigitizePointTool::~CGlobeDigitizePointTool()
{
  TurnOffGlobeDisplayEvents();

  ::DeleteObject(m_hBitmap);
  ::DeleteObject(m_hCursor);
}

HRESULT CGlobeDigitizePointTool::FinalConstruct()
{
  HRESULT hr = S_OK;

  return hr;
}

void CGlobeDigitizePointTool::FinalRelease()
{
}

// ICommand
STDMETHODIMP CGlobeDigitizePointTool::get_Enabled(VARIANT_BOOL * Enabled)
{
  if (Enabled == NULL)
    return E_POINTER;

  *Enabled = VARIANT_TRUE;

  return S_OK;
}
STDMETHODIMP CGlobeDigitizePointTool::get_Checked(VARIANT_BOOL * Checked)
{
  if (Checked == NULL)
    return E_POINTER;

  *Checked = VARIANT_FALSE;

  return E_NOTIMPL;
}
STDMETHODIMP CGlobeDigitizePointTool::get_Name(BSTR * Name)
{
  if (Name == NULL)
    return E_POINTER;

  *Name = ::SysAllocString(L"GlobeDigitizePoint");

  return S_OK;
}
STDMETHODIMP CGlobeDigitizePointTool::get_Caption(BSTR * Caption)
{
  if (Caption == NULL)
    return E_POINTER;

  *Caption = ::SysAllocString(L"Globe Digitize Point Tool");

  return S_OK;
}
STDMETHODIMP CGlobeDigitizePointTool::get_Tooltip(BSTR * Tooltip)
{
  if (Tooltip == NULL)
    return E_POINTER;

  *Tooltip = ::SysAllocString(L"Globe Digitize Point Tool");

  return S_OK;
}
STDMETHODIMP CGlobeDigitizePointTool::get_Message(BSTR * Message)
{
  if (Message == NULL)
    return E_POINTER;

  *Message = ::SysAllocString(L"Globe Digitize Point Tool");

  return S_OK;
}
STDMETHODIMP CGlobeDigitizePointTool::get_HelpFile(BSTR * HelpFile)
{
  if (HelpFile == NULL)
    return E_POINTER;

  return E_NOTIMPL;
}
STDMETHODIMP CGlobeDigitizePointTool::get_HelpContextID(LONG * helpID)
{
  if (helpID == NULL)
    return E_POINTER;

  return E_NOTIMPL;
}
STDMETHODIMP CGlobeDigitizePointTool::get_Bitmap(OLE_HANDLE * Bitmap)
{
  if (Bitmap == NULL)
    return E_POINTER;

  *Bitmap = (OLE_HANDLE)m_hBitmap;

  return S_OK;
}
STDMETHODIMP CGlobeDigitizePointTool::get_Category(BSTR * categoryName)
{
  if (categoryName == NULL)
    return E_POINTER;

  *categoryName = ::SysAllocString(L"C++ Samples");

  return S_OK;
}
STDMETHODIMP CGlobeDigitizePointTool::OnCreate(IDispatch * hook)
{
  HRESULT hr = S_OK;

  hr = m_ipGlobeHookHelper.CreateInstance(CLSID_GlobeHookHelper);
  if (FAILED(hr))
    ATLASSERT(_T("Error initializing hook helper class."));

  //set the hook
  m_ipGlobeHookHelper->putref_Hook(hook);

  hr = m_ipGlobeHookHelper->get_Globe(&m_ipGlobe);
  if (FAILED(hr))
    ATLASSERT(_T("Erorr getting globe"));

  hr = m_ipGlobeHookHelper->get_GlobeDisplay(&m_ipGlobeDisplay);
  if (FAILED(hr))
    ATLASSERT(_T("Erorr getting GlobeDisplay"));

  //start listening to GlobeDisplayEvents
  hr = TurnOnGlobeDisplayEvents();

  return hr;
}
STDMETHODIMP CGlobeDigitizePointTool::OnClick()
{
  HRESULT hr = S_OK;

  hr = m_ipGlobeDisplay->get_ActiveViewer(&m_ipViewer);
  if (FAILED(hr))
    ATLASSERT(_T("Erorr getting Active Viewer"));

  ICameraPtr ipCamera;
  m_ipViewer->get_Camera(&ipCamera);
  m_ipGlobeViewUtil = ipCamera;

  return hr;
}

//ITool
STDMETHODIMP CGlobeDigitizePointTool::get_Cursor(OLE_HANDLE * Cursor)
{
  if (Cursor == NULL)
    return E_POINTER;

  *Cursor = (OLE_HANDLE)m_hCursor;

  return S_OK;
}
STDMETHODIMP CGlobeDigitizePointTool::OnMouseDown(LONG button, LONG shift, LONG x, LONG y)
{

  //cache the coordinate since we need it for the AfterDraw
  m_scrX = (UINT)x;
  m_scrY = (UINT)y;

  m_bDrawPoint = true;

  //Refresh the display so that the AfterDraw will get called
  m_ipViewer->Redraw(VARIANT_FALSE);

  return S_OK;
}
STDMETHODIMP CGlobeDigitizePointTool::OnMouseMove(LONG button, LONG shift, LONG x, LONG y)
{
  if (false == m_bDrawPoint)
    return S_OK;

  //cache the coordinate since we need it for the AfterDraw
  m_scrX = (UINT)x;
  m_scrY = (UINT)y;

  //Refresh the display so that the AfterDraw will get called
  m_ipViewer->Redraw(VARIANT_FALSE);

  return S_OK;
}
STDMETHODIMP CGlobeDigitizePointTool::OnMouseUp(LONG button, LONG shift, LONG x, LONG y)
{
  m_bDrawPoint = false;

  return S_OK;
}
STDMETHODIMP CGlobeDigitizePointTool::OnDblClick()
{
  return E_NOTIMPL;
}
STDMETHODIMP CGlobeDigitizePointTool::OnKeyDown(LONG keyCode, LONG shift)
{
  return E_NOTIMPL;
}
STDMETHODIMP CGlobeDigitizePointTool::OnKeyUp(LONG keyCode, LONG shift)
{
  return E_NOTIMPL;
}
STDMETHODIMP CGlobeDigitizePointTool::OnContextMenu(LONG x, LONG y, VARIANT_BOOL * handled)
{
  if (handled == NULL)
    return E_POINTER;

  return E_NOTIMPL;
}
STDMETHODIMP CGlobeDigitizePointTool::Refresh(OLE_HANDLE hdc)
{
  return E_NOTIMPL;
}
STDMETHODIMP CGlobeDigitizePointTool::Deactivate(VARIANT_BOOL * complete)
{
  if (complete == NULL)
    return E_POINTER;

  return E_NOTIMPL;
}

// IGlobeDisplayEvents
STDMETHODIMP CGlobeDigitizePointTool::ActiveViewerChanged(ISceneViewer * pViewer)
{
  return E_NOTIMPL;
}
STDMETHODIMP CGlobeDigitizePointTool::ViewerAdded(ISceneViewer * pViewer)
{
  return E_NOTIMPL;
}
STDMETHODIMP CGlobeDigitizePointTool::ViewerRemoved(ISceneViewer * pViewer)
{
  return E_NOTIMPL;
}
STDMETHODIMP CGlobeDigitizePointTool::InteractionStopped()
{
  return E_NOTIMPL;
}
STDMETHODIMP CGlobeDigitizePointTool::BatchTileGenerationStarted(BSTR Name)
{
  return E_NOTIMPL;
}
STDMETHODIMP CGlobeDigitizePointTool::BatchTileGenerationStopped()
{
  return E_NOTIMPL;
}
STDMETHODIMP CGlobeDigitizePointTool::BeforeDraw(ISceneViewer * pViewer, VARIANT_BOOL * pbHandled)
{
  if (pbHandled == NULL)
    return E_POINTER;

  return E_NOTIMPL;
}
STDMETHODIMP CGlobeDigitizePointTool::AfterDraw(ISceneViewer * pViewer)
{
  ATLTRACE(_T("CGlobeDigitizePointTool::AfterDraw"));

  if (false == m_bDrawPoint)
    return S_OK;

  //convert the windows coordinated into OGL coordinates (geocentric coordinates)
  double glX, glY, glZ;
  m_ipGlobeViewUtil->WindowToGeocentric(m_ipGlobeDisplay, m_ipViewer, (long)m_scrX, (long)m_scrY, VARIANT_TRUE, &glX, &glY, &glZ);

  //draw the converted point on the surface of the globe
  glPushAttrib(GL_ALL_ATTRIB_BITS);
  glDisable(GL_DEPTH_TEST);
  glPointSize(20.0);
  glColor3ub(255, 0, 0);
  glBegin(GL_POINTS);
  glVertex3f((GLfloat)glX, (GLfloat)glY, (GLfloat)glZ);
  glEnd();
  glPopAttrib();

  return S_OK;
}
STDMETHODIMP CGlobeDigitizePointTool::VectorOverflow(ILayer * pLayer)
{
  return E_NOTIMPL;
}
STDMETHODIMP CGlobeDigitizePointTool::TileOverflow(ILayer * pLayer)
{
  return E_NOTIMPL;
}

////////////////////////////////////////////////////////////////////////////////////
//Private helper methods

HRESULT CGlobeDigitizePointTool::TurnOnGlobeDisplayEvents()
{
  ATLTRACE(_T("CGlobeDigitizePointTool::TurnOnGlobeDisplayEvents()\n"));


  HRESULT hr = S_OK;

  if (m_dwGlobeDisplayEventsCookie)
    return hr;

  hr = AtlAdvise(m_ipGlobeDisplay, IUnknownPtr(this), __uuidof(IGlobeDisplayEvents), &m_dwGlobeDisplayEventsCookie);

  return hr;

}

HRESULT CGlobeDigitizePointTool::TurnOffGlobeDisplayEvents()
{
  ATLTRACE(_T("CGlobeDigitizePointTool::TurnOnGlobeDisplayEvents()\n"));


  HRESULT hr = S_OK;

  if (m_dwGlobeDisplayEventsCookie)
  {
    hr = AtlUnadvise(m_ipGlobeDisplay, __uuidof(IGlobeDisplayEvents), m_dwGlobeDisplayEventsCookie);
  }

  return hr;
}


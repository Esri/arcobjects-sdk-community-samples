// Copyright 2015 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at <your ArcGIS install location>/DeveloperKit10.3/userestrictions.txt.
// 


#include <map>


template<class T> 
class SingletonClassFactory : public CComClassFactory, 
															public ISingletonRelease
{
public:

	BEGIN_COM_MAP(SingletonClassFactory)
		COM_INTERFACE_ENTRY(IClassFactory)
		COM_INTERFACE_ENTRY(ISingletonRelease)
	END_COM_MAP()

  SingletonClassFactory() 
  {
  }
  
  virtual ~SingletonClassFactory()
  {
  }

  STDMETHODIMP CreateInstance(LPUNKNOWN pUnkOuter, REFIID riid, void** ppv)
  {
    *ppv = 0;
    if (pUnkOuter != 0)
      return CLASS_E_NOAGGREGATION;
    
    HRESULT hr = S_OK;

    // Serialize access to this function.
    m_sec.Lock();

    DWORD tid = ::GetCurrentThreadId();
    
    IUnknown* pData = 0;

    // Lookup the singleton for the calling thread.
		ThreadInstanceLUT::iterator p;
		p = m_threadInstanceLUT.find(tid);
	  if (p != m_threadInstanceLUT.end())
		{
			// If its found, hand out a reference
			pData = p->second;
			hr = pData->QueryInterface(riid, ppv);
		}
		else 
		{
      // If not found, co-create the object

			hr = CComClassFactory::CreateInstance(NULL, IID_IUnknown,(void **)&pData);			
			hr = pData->QueryInterface(riid, ppv);

			// Artificially decrement ref count on the object, so that when
			// all clients have released their references, the reference
			// count goes to zero and the object is destroyed
			// The FinalRelease on the object must tidy up the entry in the 
			// LUT
			pData->Release();

			// add it to the cache
      m_threadInstanceLUT.insert(ThreadInstanceLUT::value_type(tid, pData));
		}

    m_sec.Unlock();

    return hr;
  }

  // Allows the singleton object to free its entry in the lookup table
	// when it shuts down
	// Assumes that the reference count of the object is already zero
  STDMETHODIMP ReleaseInstance()
  {
    // Serialize access to this function.
    m_sec.Lock();

    DWORD tid = ::GetCurrentThreadId();
    
		ThreadInstanceLUT::iterator p;
		p = m_threadInstanceLUT.find(tid);
	
	  if (p != m_threadInstanceLUT.end())
    {
			m_threadInstanceLUT.erase(p);
    }

		m_sec.Unlock();
    return S_OK;
  }

private:

  // Use standard map template class to provide data structure
  // for a set of <thread id, object instance> pairs.
  typedef std::map<DWORD, IUnknown *> ThreadInstanceLUT;

	CComAutoCriticalSection m_sec;
  ThreadInstanceLUT m_threadInstanceLUT;
};



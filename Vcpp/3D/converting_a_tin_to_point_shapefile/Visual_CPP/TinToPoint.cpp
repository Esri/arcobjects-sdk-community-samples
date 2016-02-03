#include "StdAfx.h"
#include "TinToPoint.h"

int main(int argc, char** argv)
{
	
	CoInitialize(0);
	
	ESRI_SET_VERSION(esriArcGISEngine);

  std::cerr << "Tin to Point: ArcGIS Engine Developer Sample" << std::endl;
  if (argc != 3) 
	{
		std::cerr << "Usage: TinToPoint "
							<< "[Input Tin] "
							<< "[Output Shapefile]"
							<< std::endl;
    return 0;
	}
  
  char* tinFullPath = argv[1];
  char* outputFile = argv[2];
  
  // data location variables
  CComBSTR shapePath;
  CComBSTR shapeFile;
  // Parse the input and output paths
  HRESULT hr = GetParentDirFromFullPath(outputFile, &shapePath);
  if (FAILED(hr) || shapePath.Length() <= 0)
  {
    std::cerr << "Couldn't get output path." << std::endl;
  }
  hr = GetFileFromFullPath(outputFile, &shapeFile);
  if (FAILED(hr) || shapeFile.Length() <= 0)
  {
    std::cerr << "Couldn't get output file name." << std::endl;
  }

  if (!InitializeApp(esriLicenseExtensionCode3DAnalyst))
  {
    return 0;
  }

  hr = ConvertTin2Point(tinFullPath, shapePath, shapeFile);
  if (FAILED(hr))
    std::cerr << "Failed!" << std::endl;
  else
    std::cerr << "Tin to point - Done" << std::endl;
  
  ShutdownApp();
	CoUninitialize();
  return 0;
}

HRESULT ConvertTin2Point(char* tinFullPath, BSTR shapePath, BSTR shapeFile)
{
  // Get tin from tin file
  ITinPtr ipTin(CLSID_Tin);
  ITinAdvancedPtr ipTinAdv(ipTin);
  ipTinAdv->Init(CComBSTR(tinFullPath));
  printf("Calculating...\n");

  ISpatialReferencePtr ipTinSpatialRef;
  ((IGeoDatasetPtr) ipTin)->get_SpatialReference(&ipTinSpatialRef);

  IFieldsPtr ipFields;
  CreateBasicFields(esriGeometryPoint, VARIANT_FALSE, VARIANT_TRUE, 
                    ipTinSpatialRef, &ipFields);

  // Create output shapefile
  IWorkspaceFactoryPtr ipWkspFac(CLSID_ShapefileWorkspaceFactory);
  IWorkspacePtr ipWksp;
  HRESULT hr = ipWkspFac->OpenFromFile(shapePath, 0, &ipWksp);  
  if (FAILED(hr) || ipWksp == 0)
  {
    std::cerr << "Couldn't open workspace." << std::endl;
    return hr;
  }
  IFeatureWorkspacePtr ipFeatureWksp(ipWksp);
  IFeatureClassPtr ipOutFeatureClass;
  ipFeatureWksp->CreateFeatureClass(shapeFile, ipFields, NULL, NULL, 
                                    esriFTSimple, CComBSTR(L"Shape"), 
                                    CComBSTR(L""), &ipOutFeatureClass);
  
  // Get Tin node enum
  IEnumTinNodePtr ipTinEnum;
  IEnvelopePtr ipTinEnv;
  ipTin->get_Extent(&ipTinEnv);
  ipTinAdv->MakeNodeEnumerator(ipTinEnv, esriTinInsideDataArea, NULL, 
			       &ipTinEnum);

  // Store node to shapefile
  IFeatureCursorPtr ipOutCursor;
  ipOutFeatureClass->Insert(VARIANT_TRUE, &ipOutCursor);
  IFeatureBufferPtr ipOutBuffer;
  ipOutFeatureClass->CreateFeatureBuffer(&ipOutBuffer);
  IPointPtr ipPoint(CLSID_Point);
  ITinNodePtr ipTinNode;
  ipTinEnum->Next(&ipTinNode);
  while (ipTinNode != 0)
    {
      ipTinNode->QueryAsPoint(ipPoint);
      ipOutBuffer->putref_Shape((IGeometryPtr) ipPoint);
      CComVariant id;
      ipOutCursor->InsertFeature(ipOutBuffer, &id);
      ipTinEnum->Next(&ipTinNode);
    }
  std::wcerr << "Generated shapefile is named " << shapeFile << " and in " 
             << shapePath << std::endl;

  return S_OK;
}

HRESULT CreateBasicFields(esriGeometryType shapeType, VARIANT_BOOL hasM, VARIANT_BOOL hasZ, ISpatialReference* pSpatialRef, IFields** ppFields)
{
  HRESULT hr;

  VARIANT_BOOL vbXYPres;
  pSpatialRef->HasXYPrecision(&vbXYPres);
  
  double dGridSize;
  if (vbXYPres)
  {
    double xmin; 
    double ymin; 
    double xmax; 
    double ymax; 
    pSpatialRef->GetDomain(&xmin, &xmax, &ymin, &ymax);
    double dArea = (xmax - xmin) * (ymax - ymin);
    dGridSize = sqrt(dArea);
  }
  else
    dGridSize = 1000;
  
  IGeometryDefPtr ipGeomDef(CLSID_GeometryDef);
  IGeometryDefEditPtr ipGeomDefEdit(ipGeomDef);
  ipGeomDefEdit->put_GeometryType(shapeType);
  ipGeomDefEdit->put_HasM(hasM);
  ipGeomDefEdit->put_HasZ(hasZ);
  ipGeomDefEdit->putref_SpatialReference(pSpatialRef);
  ipGeomDefEdit->put_GridCount(1);
  ipGeomDefEdit->put_GridSize(0, dGridSize);
  
  // Add OID field (must come before geometry field)
  IFieldsPtr ipFields(CLSID_Fields);
  *ppFields = ipFields;
  if (*ppFields)
    (*ppFields)->AddRef();
  IFieldsEditPtr ipFieldsEdit(ipFields);
  IFieldPtr ipField(CLSID_Field);
  IFieldEditPtr ipFieldEdit(ipField);
  ipFieldEdit->put_Name(CComBSTR(L"OBJECTID"));
  ipFieldEdit->put_AliasName(CComBSTR(L"OBJECTID"));
  ipFieldEdit->put_Type(esriFieldTypeOID);
  hr = ipFieldsEdit->AddField(ipField);

  // Add geometry field
  ipField = IFieldPtr(CLSID_Field);
  ipFieldEdit = ipField;
  ipFieldEdit->put_Name(CComBSTR(L"SHAPE"));
  ipFieldEdit->put_IsNullable(VARIANT_TRUE);
  ipFieldEdit->put_Type(esriFieldTypeGeometry);
  ipFieldEdit->putref_GeometryDef(ipGeomDef);
  ipFieldEdit->put_Required(true);
  ipFieldsEdit->AddField(ipField);

  return S_OK;
}


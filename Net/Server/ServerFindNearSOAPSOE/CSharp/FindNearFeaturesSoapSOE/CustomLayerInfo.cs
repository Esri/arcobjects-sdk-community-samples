// Copyright 2015 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at <your ArcGIS Developer Kit install location>/userestrictions.txt.
// 

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SOESupport;

namespace NetFindNearFeaturesSoapSOE
{
    [ComVisible(true)]
    [Guid("EE82B345-23E7-43A2-94EE-867ABAF0A8AC")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("NetFindNearFeatures.CustomLayerInfo")]
    [ArcGISCoCreatable]
    public class CustomLayerInfo : IXMLSerialize
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public IEnvelope Extent { get; set; }
        
        #region IXMLSerialize Members

        public void Serialize(IXMLSerializeData data)
        {
            data.TypeName = GetType().Name;
            data.TypeNamespaceURI = Constants.SOENamespaceURI;

            data.AddString("Name", Name);
            data.AddInteger("ID", ID);            
            data.AddObject("Extent", Extent);
        }

        public void Deserialize(IXMLSerializeData data)
        {
            int idx = FindMandatoryParam("Name", data);
            this.Name = data.GetString(idx);

            idx = FindMandatoryParam("ID", data);
            this.ID = data.GetInteger(idx);

            idx = FindMandatoryParam("Extent", data);
            this.Extent = (IEnvelope)data.GetObject(idx, Constants.ESRINamespaceURI, "Envelope");
        }

        #endregion

        private int FindMandatoryParam(string fieldName, IXMLSerializeData data)
        {
            int idx = data.Find(fieldName);
            if (idx == -1)
                throw new MissingMandatoryFieldException(fieldName);
            return idx;
        }

        internal class MissingMandatoryFieldException : Exception
        {
            internal MissingMandatoryFieldException(string fieldName) : base("Missing mandatory field: " + fieldName) { }
        }
        
        //empty constructor is required by ArcGISCoCreatable
        public CustomLayerInfo() 
        {

        }

    } //class CustomLayerInfo


    [ComVisible(true)]
    [Guid("8D7CF4B1-D915-4B31-B6BF-FB06FB9A3580")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("NetFindNearFeatures.CustomLayerInfos")]
    [ArcGISCoCreatable]
    public class CustomLayerInfos : SerializableList<CustomLayerInfo> 
    {
        public CustomLayerInfos(string namespaceURI)
            :base(namespaceURI)
        {
        }

        //empty constructor is required by ArcGISCoCreatable. NamespaceURI must be there!
        public CustomLayerInfos()
            : base(Constants.SOENamespaceURI)
        {

        }

    } //class CustomLayerInfos

}

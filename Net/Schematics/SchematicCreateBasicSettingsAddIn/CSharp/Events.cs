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
// Copyright 2010 ESRI
// 
// All rights reserved under the copyright laws of the United States
// and applicable international laws, treaties, and conventions.
// 
// You may freely redistribute and use this sample code, with or
// without modification, provided you include the original copyright
// notice and use restrictions.
// 
// See the use restrictions at &lt;your ArcGIS install location&gt;/DeveloperKit10.0/userestrictions.txt.

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace SchematicCreateBasicSettingsAddIn 
{
    public class ReduceEvents : EventArgs
    {
        private string[] selectedObjects;
        public ReduceEvents(string[] items)
        {
            this.selectedObjects = items;
        }
        public string[] SelectedObjects
        {
            get { return selectedObjects; }
            set { selectedObjects = value; }
        }
    }
    
    public class NameEvents : EventArgs
    {
        private bool blnNewDataset;
        private string strDatasetName;
        private string strTemplateName;
        private bool blnUseVertices;
        private bool blnAutoCreate;
        
        public NameEvents(bool blnNewDataset, string strDatasetName, string strTemplateName, bool blnUseVertices)
        {
            this.blnNewDataset = blnNewDataset;
            this.strDatasetName = strDatasetName;
            this.strTemplateName = strTemplateName;
            this.blnUseVertices = blnUseVertices;
        }

        public bool NewDataset
        {
            get { return blnNewDataset; }
            set { blnNewDataset = value; }
        }
        public string DatasetName
        {
            get { return strDatasetName; }
            set { strDatasetName = value; }
        }
        public string TemplateName
        {
            get { return strTemplateName; }
            set { strTemplateName = value; }
        }
        public bool UseVertices
        {
            get { return blnUseVertices; }
            set { blnUseVertices = value; }
        }
        public bool AutoCreate
        {
            get { return blnAutoCreate; }
            set { blnAutoCreate = value; }
        }
    }

    public class AdvancedEvents : EventArgs
    {
        private string strAlgorithmName;
        private Dictionary<string,string> dicAlgorithmParams;
        private string strRootClass;
        private NameValueCollection fieldsToCreate;

        public AdvancedEvents(string AlgorithmName, Dictionary<string,string> AlgorithmParams,string RootClass,NameValueCollection FieldsToCreate)
        {
            strAlgorithmName = AlgorithmName;
            dicAlgorithmParams = AlgorithmParams;
            strRootClass = RootClass;
            fieldsToCreate = FieldsToCreate;
        }

        public string AlgorithmName
        {
            get { return strAlgorithmName; }
            set { strAlgorithmName = value; }
        }
        public string RootClass
        {
            get { return strRootClass; }
            set { strRootClass = value; }
        }
        public Dictionary<string,string> AlgorithmParams
        {
            get { return dicAlgorithmParams; }
            set { dicAlgorithmParams = value; }
        }
        public NameValueCollection FieldsToCreate
        {
            get { return fieldsToCreate; }
            set { FieldsToCreate = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Schematic;
using ApplicativeAlgorithmsCS;

namespace ApplicativeAlgorithmsPageCS
{
    [ClassInterface(ClassInterfaceType.None)]
    [Guid(TranslateTreePropPage.GUID)]
    [ProgId(TranslateTreePropPage.PROGID)]
    [ComVisible(true)]
    public partial class TranslateTreePropPage : PropertyPage
    {
        public const string GUID = "FCEB5D3A-AB6E-42eb-ABE7-074067DC0202";
        private const string PROGID = "ApplicativeAlgorithmsPage.TranslateTreePropPage";


        #region "Component Category Registration"
        [ComRegisterFunction(), ComVisibleAttribute(true)]
        public static void Reg(string sKey)
        {
            SchematicAlgorithmPages.Register(sKey);
        }

        [ComUnregisterFunction(), ComVisibleAttribute(true)]
        public static void Unreg(string sKey)
        {
            SchematicAlgorithmPages.Unregister(sKey);
        }
        #endregion

        #region internal methods
        public TranslateTreePropPage()
        {
            InitializeComponent();
        }

        private void ChangedTexte(object sender, EventArgs e)
        {
            PageIsDirty = true;
        }

        private void TexteEnter(object sender, EventArgs e)
        {
            TextBox texteBox = (TextBox)sender;
            texteBox.SelectAll();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {

            // Get the saved algorithm parameters from the diagram class
            TranslateTree newAlgo = GetSavedAlgo();
            if (newAlgo == null)
                newAlgo = new TranslateTree(); // otherwise revert to default algorithm parameters

            // get the values and set the edit boxes
            txtXTrans.Text = newAlgo.TranslationFactorX.ToString();
            txtYTrans.Text = newAlgo.TranslationFactorY.ToString();
        }


        private TranslateTree GetSavedAlgo()
        {
            TranslateTree myAlgo = FindOurAlgo();
            if (myAlgo == null)
                return null;

            ISchematicDiagramClassName diagramClassName = myAlgo.SchematicDiagramClassName;
            IName pName = diagramClassName as IName;
            if (pName == null)
                return null;

            object unk = pName.Open();
            ISchematicDiagramClass diagramClass = unk as ISchematicDiagramClass;
            if (diagramClass == null)
                return null;

            // get the default algorithms for this diagram class
            IEnumSchematicAlgorithm enumAlgorithms = diagramClass.SchematicAlgorithms;
            if (enumAlgorithms == null)
                return null;

            TranslateTree savedAlgo = null;

            enumAlgorithms.Reset();
            ISchematicAlgorithm algorithm;
            while ((algorithm = enumAlgorithms.Next()) != null)
            {
                    savedAlgo = algorithm as TranslateTree;

                if (savedAlgo != null)
                    break;
            }

            return savedAlgo;
        }


        private TranslateTree FindOurAlgo()
        {
            TranslateTree myAlgo = null;

            // loop through the objects until the algorithm is found or not
            System.Collections.IEnumerator enumCollection = Objects.GetEnumerator();
            enumCollection.Reset();

            while (enumCollection.MoveNext())
            {
                myAlgo = enumCollection.Current as TranslateTree;
                if (myAlgo != null)
                    return myAlgo; // found it
            }

            return null;
        }



        #endregion


        #region " PropertyPage "

        protected override void OnPageDeactivate()
        {
            base.OnPageDeactivate();
        }

        protected override void OnActivated(System.EventArgs e)
        {
            base.OnActivated(e);

            TranslateTree myAlgo = FindOurAlgo();
            if (myAlgo != null)
            {
                this.txtXTrans.Text = myAlgo.TranslationFactorX.ToString();
                this.txtYTrans.Text = myAlgo.TranslationFactorY.ToString();
            }

            PageIsDirty = false;
        }

        protected override void OnPageApply()
        {
            //OnPageApply is launched two times when you click on Apply 
            //and one time if you have more one page and change page
            //So I use a flag to authorize or not the application of algorithm
            //The timer reset the flag, in case of multi-pages
            timApply.Enabled = false;
            base.OnPageApply();

            TranslateTree myAlgo = FindOurAlgo();
            if (myAlgo != null)
            {
                try
                {
                    myAlgo.TranslationFactorX = System.Convert.ToDouble(this.txtXTrans.Text);
                }
                finally { }

                try
                {
                    myAlgo.TranslationFactorY = System.Convert.ToDouble(this.txtYTrans.Text);
                }
                finally { }
            }
            timApply.Enabled = true;

            PageIsDirty = false;
        }


        public override void SetPageSite(IPropertyPageSite pPageSite)
        {
            if (pPageSite == null)
                return;

            base.SetPageSite(pPageSite);
        }


        // make sure our algorithm is in the input array of IUnknown
        // otherwise throw an exception
        public override void SetObjects(uint cObjects, object[] ppUnk)
        {
            if (ppUnk == null || cObjects < 1)
                throw new ArgumentNullException();

            // remove previously stored IUnkown objects
            Objects = null;

            TranslateTree processedAlgo = null;

            // browse input collection 
            System.Collections.IEnumerator enumCollection = ppUnk.GetEnumerator();
            enumCollection.Reset();
            while (enumCollection.MoveNext())
            {
                processedAlgo = enumCollection.Current as TranslateTree;

                if (processedAlgo != null)
                {
                    // assumes only one object is managed by this property page
                    Objects = new object[1];
                    Objects[0] = enumCollection.Current;
                    break;
                }
            }

            if (Objects == null)
                throw new ArgumentNullException();
        }
            


        #endregion
    }
}


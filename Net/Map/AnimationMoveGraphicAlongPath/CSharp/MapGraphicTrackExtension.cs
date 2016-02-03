using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.Animation;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Framework;

namespace AnimationDeveloperSamples
{
    [Guid("A18921C8-275B-4CDC-B830-C7F8DB1E92EC")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("CustomMapAnimation1.MapGraphicTrackExtension")]
    public class MapGraphicTrackExtension:IMapGraphicTrackExtension,IPersistStream
    {
        private IPropertySet propSet;
        private IPersistStream persist;

        #region constructor
        public MapGraphicTrackExtension()
        {
            ILineElement traceElement = new LineElementClass();
            SetDefaultSymbol(traceElement);
            //add a tag to the trace line
            IElementProperties elemProps = (IElementProperties)traceElement;
            elemProps.Name = "{E63706E1-B13C-4184-8AB8-97F67FA052D4}";
            bool showTrace = true;
            propSet = new PropertySetClass();
            propSet.SetProperty("Line Element", traceElement);
            propSet.SetProperty("Show Trace", showTrace);
            persist = (IPersistStream)propSet;
        }
        #endregion

        #region IMapGraphicTrackExtension members
        public ILineElement TraceElement
        {
            get {
                return (ILineElement)propSet.GetProperty("Line Element");
            }
            set {
                ILineElement temp = value;
                propSet.SetProperty("Line Element", temp);
            }
        }

        public bool ShowTrace
        {
            get {
                return (bool)propSet.GetProperty("Show Trace");
            }
            set {
                bool temp = value;
                propSet.SetProperty("Show Trace", temp);
            }
        }

        public void ClearTrace()
        {
            IElement elem = (IElement)propSet.GetProperty("Line Element");
            elem.Geometry.SetEmpty();
        }
        #endregion

        #region IPersistStream Members
        public void GetSizeMax(out _ULARGE_INTEGER pcbSize)
        {
            persist.GetSizeMax(out pcbSize);
        }

        public void GetClassID(out Guid pClassID)
        {
            pClassID = this.GetType().GUID;
        }

        public void Load(IStream pstm)
        {
            persist.Load(pstm);
        }

        public void IsDirty()
        {
            persist.IsDirty();
        }

        public void Save(IStream pstm, int fClearDirty)
        {
            persist.Save(pstm, fClearDirty);
        }
        #endregion

        #region private methods
        private void SetDefaultSymbol(ILineElement elem)
        {
            ILineSymbol defaultLineSym = null;
            String esriStylePath;
            IStyleGallery styleGallery = new StyleGalleryClass();
            IStyleGalleryStorage styleStor = (IStyleGalleryStorage)styleGallery;
            esriStylePath = styleStor.DefaultStylePath + "ESRI.style";

            IEnumStyleGalleryItem styleItems = styleGallery.get_Items("Line Symbols",esriStylePath,"Dashed");
            styleItems.Reset();
            IStyleGalleryItem styleGalleryItem = styleItems.Next();
            while (!(styleGalleryItem == null))
            {
                if (styleGalleryItem.Name == "Dashed 4:4")
                {
                    defaultLineSym = (ILineSymbol)styleGalleryItem.Item;
                    defaultLineSym.Width = 1.50;
                    IRgbColor rgbColor = new RgbColorClass();
                    rgbColor.Red = 255;
                    rgbColor.Blue = 0;
                    rgbColor.Green = 0;
                    rgbColor.Transparency = 50;
                    defaultLineSym.Color = rgbColor;
                    break;
                }
                else
                {
                    styleGalleryItem = styleItems.Next();
                }
            }
            elem.Symbol = defaultLineSym;
        }
        #endregion
    }

    public interface IMapGraphicTrackExtension
    {
        ILineElement TraceElement
        {
            get;
            set;
        }

        bool ShowTrace
        {
            get;
            set;
        }

        void ClearTrace();
        
    }
}

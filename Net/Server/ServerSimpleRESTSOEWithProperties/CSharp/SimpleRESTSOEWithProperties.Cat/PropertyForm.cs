using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetSimpleRESTSOEWithProperties.Cat
{
    public partial class PropertyForm : Form
    {   
        private string m_layerType;
        private string m_maxNumFeatures;
        private string m_returnFormat;
        private string m_isEditable; 
        private System.Collections.Generic.Dictionary<string, string[]> m_fieldsDictionary = new
            System.Collections.Generic.Dictionary<string, string[]>();

        public int getHWnd()
        {
            return this.Handle.ToInt32();
        }

        internal ESRI.ArcGIS.Framework.IComPropertyPageSite PageSite
        {
            private get;
            set;
        }

        internal string LayerType
        {
            get
            {
                return m_layerType;
            }
            set
            {
                m_layerType = value;
                SetLayerType(m_layerType);
            }
        }

        private void SetLayerType(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(SetLayerType), new object[] { value });
                return;
            }
            LayerTypeBox.Text = value;
        }

        internal string MaxNumFeatures
        {
            get
            {
                return m_maxNumFeatures;
            }
            set
            {
                m_maxNumFeatures = value;
                SetMaxNumFeatures(m_maxNumFeatures);
            }
        }

        private void SetMaxNumFeatures(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(SetMaxNumFeatures), new object[] { value });
                return;
            }
            MaxNumFeaturesBox.Text = value;
        }

        internal string ReturnFormat
        {
            get
            {
                return m_returnFormat; 
            }
            set
            {
                m_returnFormat = value;
                SetReturnFormat(m_returnFormat);
            }
        }

        private void SetReturnFormat(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(SetReturnFormat), new object[] { value });
                return;
            }
            ReturnFormatBox.Text = value;
        }

        internal string IsEditable
        {
            get
            {
                return m_isEditable;
            }
            set
            {
                m_isEditable = value;
                SetIsEditable(m_isEditable);
            }
        }

        private void SetIsEditable(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(SetIsEditable), new object[] { value });
                return;
            }
            IsEditableBox.Text = value;
        }

        public PropertyForm()
        {
            InitializeComponent();
        }

        private void LayerTypeBox_TextChanged(object sender, EventArgs e)
        {
            // Update the current SOE field.
            m_layerType = LayerTypeBox.Text;
            // Notify ArcCatalog that the properties have changed.
            if(PageSite != null)
                PageSite.PageChanged();
        }

        private void MaxNumFeaturesBox_TextChanged(object sender, EventArgs e)
        {
            // Update the current SOE field.
            m_maxNumFeatures = MaxNumFeaturesBox.Text;
            // Notify ArcCatalog that the properties have changed.
            if(PageSite != null)
                PageSite.PageChanged();
        }

        private void ReturnFormatBox_TextChanged(object sender, EventArgs e)
        {
            // Update the current SOE field.
            m_returnFormat = ReturnFormatBox.Text;
            // Notify ArcCatalog that the properties have changed.
            if (PageSite != null)
                PageSite.PageChanged();
        }

        private void IsEditableBox_TextChanged(object sender, EventArgs e)
        {
            // Update the current SOE field.
            m_isEditable = IsEditableBox.Text;
            // Notify ArcCatalog that the properties have changed.
            if (PageSite != null)
                PageSite.PageChanged();
        }
        
    }
}

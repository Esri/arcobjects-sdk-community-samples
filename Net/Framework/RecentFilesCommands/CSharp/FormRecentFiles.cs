using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace RecentFilesCommandsCS
{
    internal partial class FormRecentFiles : Form
    {
        public FormRecentFiles()
        {
            InitializeComponent();
        }

        public void PopulateFileList(string[] files)
        {
            lstFiles.Items.Clear();
            lstFiles.Items.AddRange(files);
        }
    }
}
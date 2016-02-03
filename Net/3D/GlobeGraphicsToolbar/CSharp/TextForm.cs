using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GlobeGraphicsToolbar
{
    public partial class TextForm : Form
    {
        private string _inputText;

        public TextForm()
        {
            InitializeComponent();
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _inputText = this.textBox1.Text;
                this.Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        public string InputText
        {
            get
            {
                return _inputText;
            }
        }
    }
}
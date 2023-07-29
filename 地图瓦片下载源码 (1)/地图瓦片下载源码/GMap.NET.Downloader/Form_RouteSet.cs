using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GMap.NET.Downloader
{
    public partial class Form_RouteSet : Form
    {
        public Form_RouteSet()
        {
            InitializeComponent();
        }

        public bool FirstVisible
        {
            get
            { 
                return this.checkBox_FirstVisible.Checked;
            }
            set 
            {
                this.checkBox_FirstVisible.Checked = value;
            }
        }

        public bool ShowTotal
        {
            get
            {
                return this.checkBox_ShowTotal.Checked;
            }
            set
            {
                this.checkBox_ShowTotal.Checked = value;
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}

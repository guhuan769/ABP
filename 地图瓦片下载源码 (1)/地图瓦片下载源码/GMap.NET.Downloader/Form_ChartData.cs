using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GMap.NET.Mng;

namespace GMap.NET.Downloader
{
    public partial class Form_ChartData : Form
    {
        public Form_ChartData(ChartMng mng)
        {
            _mng = mng;
            InitializeComponent();
        }

        private ChartMng _mng;
        private DataTable _table;

        private void Form_ChartData_Load(object sender, EventArgs e)
        {
            try
            {
                _table = _mng.DataSource.Copy();
                this.dataGridView1.DataSource = _table.DefaultView;
                this.textBox1.Text = _mng.TitleName;

                foreach (Color item in _mng.WearColor)
                {
                    this.comboBox_颜色.Items.Add(item.Name);
                }

                this.comboBox_颜色.SelectedItem = _mng.ChartColor.Name;

                foreach (CharMode item in Enum.GetValues(typeof(CharMode)))
                {
                    this.comboBox_类型.Items.Add(item.ToString());
                }

                this.comboBox_类型.SelectedItem = _mng.ChartStyle.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            try
            {
                _mng.DataSource = _table;
                _mng.TitleName = this.textBox1.Text;
                _mng.ChartColor = Color.FromName(this.comboBox_颜色.SelectedItem.ToString()); 
                _mng.ChartStyle = (CharMode)Enum.Parse(typeof(CharMode), this.comboBox_类型.SelectedItem.ToString());

                if (_mng.ChartStyle == CharMode.Area)
                {
                    _mng.LabelSay = true;
                }
                else
                {
                    _mng.LabelSay = false;
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}

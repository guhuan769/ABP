using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace GMap.NET.Downloader
{
    public partial class Form_Config : Form
    {
        public Form_Config()
        {
            InitializeComponent();
        }

        private void Form_Config_Load(object sender, EventArgs e)
        {
            this.textBox_普通地图.Text = ConfigurationManager.AppSettings["GaoDe"].ToString();
            this.textBox_卫星地图.Text = ConfigurationManager.AppSettings["GaoDeWX"].ToString();
            this.textBox_公路地图.Text = ConfigurationManager.AppSettings["GaoDeGL"].ToString();
            this.textBox_路网地图.Text = ConfigurationManager.AppSettings["GaoDeLW"].ToString();
            this.textBox_URL.Text = ConfigurationManager.AppSettings["URL"].ToString();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            var cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None); //打开配置文件
            cfg.AppSettings.Settings["GaoDe"].Value = this.textBox_普通地图.Text; //修改配置节
            cfg.AppSettings.Settings["GaoDeWX"].Value = this.textBox_卫星地图.Text; //修改配置节
            cfg.AppSettings.Settings["GaoDeGL"].Value = this.textBox_公路地图.Text; //修改配置节
            cfg.AppSettings.Settings["GaoDeLW"].Value = this.textBox_路网地图.Text; //修改配置节
            cfg.AppSettings.Settings["URL"].Value = this.textBox_URL.Text; 
            cfg.Save(); //保存

            ConfigurationManager.RefreshSection("appSettings"); //更新缓存
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}

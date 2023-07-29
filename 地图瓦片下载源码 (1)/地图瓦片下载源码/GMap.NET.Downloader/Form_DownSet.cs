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
    public partial class Form_DownSet : Form
    {
        public Form_DownSet(Dictionary<int, long> roomData)
        {
            _roomData = roomData;

            InitializeComponent();
        }

        private DataTable _table;
        private Dictionary<int, long> _roomData;
        private List<int> _room = new List<int>();

        public List<int> Room
        {
            get { return _room; }
        }

        private void Form_DownSet_Load(object sender, EventArgs e)
        {
            _table = CreateTable();
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.DataSource = _table.DefaultView;
        }

        private DataTable CreateTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("选取", typeof(bool));
            table.Columns.Add("层级", typeof(int));
            table.Columns.Add("瓦片数量", typeof(long));
            table.Columns.Add("大小", typeof(string));

            foreach (int key in _roomData.Keys)
            {
                DataRow newRow = table.NewRow();
                newRow["选取"] = false;
                newRow["层级"] = key;
                newRow["瓦片数量"] = _roomData[key];
                newRow["大小"] = GetSize(_roomData[key] * 20);
                table.Rows.Add(newRow);
            }

            return table;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            _room.Clear();
            foreach (DataRow row in _table.Rows)
            {
                if ((bool)row["选取"])
                {
                    _room.Add((int)row["层级"]);
                }
            }

            if (_room.Count > 0)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                MessageBox.Show("请选取要下载的范围！");
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }


        /// <summary>
        /// 单位换算
        /// </summary>
        /// <param name="fileKB"></param>
        /// <returns></returns>
        public string GetSize(long fileKB)
        {
            string SizeText = string.Empty;

            if (fileKB < 1000)
            {
                SizeText = string.Format("{0} KB", fileKB.ToString());
            }
            else
            {
                long title = fileKB / 1000;
                if (title < 1000)
                {
                    long tail = fileKB % 1000;

                    if (tail > 0)
                    {
                        tail = tail * 10 / 1000;
                    }

                    if (tail > 0)
                    {
                        SizeText = string.Format("{0}.{1} MB", title.ToString(), tail.ToString());
                    }
                    else
                    {
                        SizeText = string.Format("{0} MB", title.ToString());
                    }
                }
                else
                {
                    long titleM = title / 1000;
                    long tail = title % 1000;

                    if (tail > 0)
                    {
                        tail = tail * 10 / 1000;
                    }

                    if (tail > 0)
                    {
                        SizeText = string.Format("{0}.{1} G", titleM.ToString(), tail.ToString());
                    }
                    else
                    {
                        SizeText = string.Format("{0} G", titleM.ToString());
                    }
                }
            }

            return SizeText;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRow row in _table.Rows)
            {
                row["选取"] = this.checkBox1.Checked;
            }
        }
    }
}

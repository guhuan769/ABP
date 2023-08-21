using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using GMap.NET.WindowsForms;
using GMap.NET.Mng;

namespace GMap.NET.Downloader
{
    public partial class Form_Downloader : Form
    {
        public Form_Downloader()
        {
            InitializeComponent();
        }

        private GMapMng _mng;
        private readonly Guid _id_普通 = new Guid("608748FC-5FDD-4d3a-9027-356F24A755E5");
        private readonly Guid _id_卫星 = new Guid("5D95D60D-EAC3-4A44-8539-EB0CD012A83D");
        private readonly Guid _id_公路 = new Guid("84BA63CB-4916-4B83-BC4B-820410F54F9A");
        private readonly Guid _id_路网 = new Guid("9CDFA795-CB22-40E9-A531-58D2C5DB8025");
        private readonly Guid _id_自定义 = new Guid("9121659A-F466-4B7D-99E4-786432C0915B");

        private void Form_Downloader_Load(object sender, EventArgs e)
        {
            try
            {
                _dbConnectionStr = string.Format("Data Source={0}\\GMap.db;Version=3;", System.Windows.Forms.Application.StartupPath);
                Init();

                gMapControl1.Manager.Mode = AccessMode.ServerAndCache;
                gMapControl1.MapProvider = MapProviderHelper.GetProvider(EMapType.高德, ConfigurationManager.AppSettings["GaoDe"].ToString());
                //gMapControl1.MapProvider = MapProviderHelper.GetProvider(EMapType.本地, ConfigurationManager.AppSettings["URL"].ToString());
                //gMapControl1.MapProvider.UrlFormat = ConfigurationManager.AppSettings["URL"].ToString();
                gMapControl1.Position = new PointLatLng(30.6639034374516, 104.0625);
                //gMapControl1.Position = new PointLatLng(21.5053134635657, 67.8822576999664);
                gMapControl1.DragButton = System.Windows.Forms.MouseButtons.Left;
                gMapControl1.MinZoom = 0;
                gMapControl1.MaxZoom = 18;
                //gMapControl1.Zoom = 10;
                gMapControl1.Zoom = 8;

                gMapControl1.OnMapZoomChanged += gMapControl_OnMapZoomChanged;

                _mng = new GMapMng(this.gMapControl1);
                _mng.OverlayCollection.AddOverlayMng("第二层");
                _mng.OverlayCollection.AddOverlayMng("第一层");
                _mng.IsFixedCenter = false;

                this.comboBox_存储方式.SelectedIndex = 0;
                this.comboBox_取数模式.SelectedIndex = 0;
                this.comboBox_图层.SelectedIndex = 0;

                _mng.MapMouseDownPositionEvent += mng_MapMouseDownPositionEvent;
                _mng.MapMouseUpPositionEvent += mng_MapMouseUpPositionEvent;
                _mng.MapCtrlDoubleClickEvent += mng_MapCtrlDoubleClickEvent;
                _mng.MapMouseMoveEvent += mng_MapMouseMoveEvent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string _dbConnectionStr = string.Empty;

        private void Init()
        {
            SQLiteDBBase db = new SQLiteDBBase(_dbConnectionStr);
            db.Db.CommandType = CommandType.Text;

            db.Db.CommandText = "select * from (Select PID as CID,'' as UPID,PName as CName From [Province] union select CID,PID as UPID,CName from [City]) as T order by T.CName";
            db.Db.Parameters.Clear();
            DataSet dsC = db.Db.DBSelect();

            Bind_Tv(dsC.Tables[0], this.treeView_Area.Nodes, null, "CID", "UPID", "CName");
        }

        #region 绑定TreeView

        /// <summary>
        /// 绑定TreeView（利用TreeNode）
        /// </summary>
        /// <param name="p_Node">TreeNode（TreeView的一个节点）</param>
        /// <param name="pid_val">父id的值</param>
        /// <param name="id">数据库 id 字段名</param>
        /// <param name="pid">数据库 父id 字段名</param>
        /// <param name="text">数据库 文本 字段值</param>
        protected void Bind_Tv(DataTable dt, TreeNode p_Node, string pid_val, string id, string pid, string text)
        {
            DataView dv = new DataView(dt);//将DataTable存到DataView中，以便于筛选数据
            TreeNode tn;//建立TreeView的节点（TreeNode），以便将取出的数据添加到节点中
            //以下为三元运算符，如果父id为空，则为构建“父id字段 is null”的查询条件，否则构建“父id字段=父id字段值”的查询条件
            string filter = string.IsNullOrEmpty(pid_val) ? pid + "=''" : string.Format(pid + "='{0}'", pid_val);
            dv.RowFilter = filter;//利用DataView将数据进行筛选，选出相同 父id值 的数据
            foreach (DataRowView row in dv)
            {
                tn = new TreeNode();//建立一个新节点（学名叫：一个实例）
                if (p_Node == null)//如果为根节点
                {
                    tn.Name = row[id].ToString();//节点的Value值，一般为数据库的id值
                    tn.Text = row[text].ToString();//节点的Text，节点的文本显示
                    treeView_Area.Nodes.Add(tn);//将该节点加入到TreeView中
                    Bind_Tv(dt, tn, tn.Name, id, pid, text);//递归（反复调用这个方法，直到把数据取完为止）
                }
                else//如果不是根节点
                {
                    tn.Name = row[id].ToString();//节点Value值
                    tn.Text = row[text].ToString();//节点Text值
                    p_Node.Nodes.Add(tn);//该节点加入到上级节点中
                    Bind_Tv(dt, tn, tn.Name, id, pid, text);//递归
                }
            }
        }

        /// <summary>
        /// 绑定TreeView（利用TreeNodeCollection）
        /// </summary>
        /// <param name="tnc">TreeNodeCollection（TreeView的节点集合）</param>
        /// <param name="pid_val">父id的值</param>
        /// <param name="id">数据库 id 字段名</param>
        /// <param name="pid">数据库 父id 字段名</param>
        /// <param name="text">数据库 文本 字段值</param>
        private void Bind_Tv(DataTable dt, TreeNodeCollection tnc, string pid_val, string id, string pid, string text)
        {
            DataView dv = new DataView(dt);//将DataTable存到DataView中，以便于筛选数据
            TreeNode tn;//建立TreeView的节点（TreeNode），以便将取出的数据添加到节点中
            //以下为三元运算符，如果父id为空，则为构建“父id字段 is null”的查询条件，否则构建“父id字段=父id字段值”的查询条件
            string filter = string.IsNullOrEmpty(pid_val) ? pid + "=''" : string.Format(pid + "='{0}'", pid_val);
            dv.RowFilter = filter;//利用DataView将数据进行筛选，选出相同 父id值 的数据
            foreach (DataRowView drv in dv)
            {
                tn = new TreeNode();//建立一个新节点（学名叫：一个实例）
                tn.Name = drv[id].ToString();//节点的Value值，一般为数据库的id值
                tn.Text = drv[text].ToString();//节点的Text，节点的文本显示
                tnc.Add(tn);//将该节点加入到TreeNodeCollection（节点集合）中
                Bind_Tv(dt, tn.Nodes, tn.Name, id, pid, text);//递归（反复调用这个方法，直到把数据取完为止）
            }
        }

        #endregion

        private MapPolygon _polygon;
        private void treeView_Area_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                SQLiteDBBase db = new SQLiteDBBase(_dbConnectionStr);
                db.Db.CommandType = CommandType.Text;

                db.Db.CommandText = " select Lng,Lat,Sort from [CITYAREA] where CID=@CID order by Sort";
                db.Db.Parameters.Clear();
                db.Db.Parameters.AddWithValue("@CID", e.Node.Name);
                DataSet ds = db.Db.DBSelect();

                List<PointLatLng> points = new List<PointLatLng>();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    points.Add(new PointLatLng(Convert.ToDouble(row["Lat"]), Convert.ToDouble(row["Lng"])));
                }

                if (points.Count == 0) return;
                gMapControl1.Zoom = 8;
                ICtrlBase ctrl = _mng.GetCtrl(_mng.OverlayCollection.CurrentOverlayID);
                if (ctrl == null)
                {
                    _polygon = new MapPolygon(points, _mng.OverlayCollection.CurrentOverlayID, e.Node.Text);
                    _polygon.IsEdit = false;
                    _polygon.IsMove = false;
                    _mng.AddCtrl(_polygon);
                    gMapControl1.Position = _polygon.Center;
                    _polygon.IsShowText = true;
                }
                else
                {
                    ((MapPolygon)ctrl).Points.Clear();
                    ((MapPolygon)ctrl).Points.AddRange(points);
                    gMapControl1.UpdatePolygonLocalPosition((MapPolygon)ctrl);
                    gMapControl1.Position = ((MapPolygon)ctrl).Center;
                    ((MapPolygon)ctrl).ShowText = e.Node.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void toolStripMenuItem_下载区域_Click_1(object sender, EventArgs e)
        {
            _mng.CreateType = ECreateType.DownRectangle;
        }
        private void toolStripMenuItem_矩形_Click(object sender, EventArgs e)
        {
            _mng.CreateType = ECreateType.Rectangle;
        }

        private void toolStripMenuItem_圆形_Click(object sender, EventArgs e)
        {
            _mng.CreateType = ECreateType.Circle;
        }
        private void toolStripMenuItem_多边形_Click(object sender, EventArgs e)
        {
            _mng.CreateType = ECreateType.Polygon;
        }

        private void toolStripMenuItem_图标_Click(object sender, EventArgs e)
        {
            _mng.CreateType = ECreateType.Marker;
        }
        private void toolStripMenuItem_报警图标_Click(object sender, EventArgs e)
        {
            _mng.CreateType = ECreateType.AlarmMarker;
        }
        private void 图表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mng.CreateType = ECreateType.Chart;
        }
        private void ToolStripMenuItem_测距_Click(object sender, EventArgs e)
        {
            _mng.CreateType = ECreateType.Route;
        }
        private void comboBox_图层_SelectedIndexChanged(object sender, EventArgs e)
        {
            _mng.OverlayCollection.SetCurrentOverlayID(this.comboBox_图层.SelectedItem.ToString());
            _mng.RestCtrl();
        }

        private void checkBox_图标报警_CheckedChanged(object sender, EventArgs e)
        {
            this._mng.AlarmMng.IsAlarm = this.checkBox_图标报警.Checked;
        }

        private void checkBox_网格_CheckedChanged(object sender, EventArgs e)
        {
            gMapControl1.ShowTileGridLines = this.checkBox_网格.Checked;
        }

        private void checkBox_hide_CheckedChanged(object sender, EventArgs e)
        {
            gMapControl1.Manager.HideMap = this.checkBox_hide.Checked;
            gMapControl1.ReloadMap();
        }

        private void toolStripMenuItem_选择删除_Click(object sender, EventArgs e)
        {
            try
            {
                _mng.DeleteCtrl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripMenuItem_全部删除_Click(object sender, EventArgs e)
        {
            try
            {
                _mng.DeleteAllCtrl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripMenuItem_重新加载_Click(object sender, EventArgs e)
        {
            gMapControl1.Position = new PointLatLng(30.6639034374516, 104.0625);
            gMapControl1.Zoom = 10;
            gMapControl1.ReloadMap();
        }

        private void mng_MapMouseDownPositionEvent(PointLatLng point)
        {
            this.textBox_lat.Text = point.Lat.ToString();
            this.textBox_lng.Text = point.Lng.ToString();
        }

        private void mng_MapMouseUpPositionEvent(PointLatLng point)
        {
            if (_mng.CurrentCtrl != null && _mng.CurrentCtrl is MapMarkerPositionDot)
            {
                ToolStripMenuItem_删除.Enabled = true;
            }
            else
            {
                ToolStripMenuItem_删除.Enabled = false;
            }
            if (_mng.FocusCtrl != null && _mng.FocusCtrl.IsEdit)
            {
                ToolStripMenuItem_删除焦点.Enabled = true;
                if (_mng.FocusCtrl.IsShowText)
                {
                    ToolStripMenuItem_显示文本.Enabled = true;
                }
                else
                {
                    ToolStripMenuItem_显示文本.Enabled = false;
                }
                if (_mng.FocusCtrl is GMapMarker)
                {
                    ToolStripMenuItem_提示信息.Enabled = true;
                }
                else
                {
                    ToolStripMenuItem_提示信息.Enabled = false;
                }
                if (_mng.FocusCtrl is MapMarkerRect && _mng.FocusCtrl.IsDoubleClick)
                {
                    ToolStripMenuItem_地图下载.Enabled = true;
                }
                else
                {
                    ToolStripMenuItem_地图下载.Enabled = false;
                }
                if (_mng.FocusCtrl is MapPolygon)
                {
                    if (_mng.CreateType == ECreateType.Polygon)
                    {
                        ToolStripMenuItem_开始编辑.Enabled = false;
                        结束编辑ToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        ToolStripMenuItem_开始编辑.Enabled = true;
                        结束编辑ToolStripMenuItem.Enabled = false;
                    }
                }
                else
                {
                    ToolStripMenuItem_开始编辑.Enabled = false;
                    结束编辑ToolStripMenuItem.Enabled = false;
                }
                if (_mng.FocusCtrl is MapRouteDistance)
                {
                    ToolStripMenuItem_属性测距.Enabled = true;

                    if (_mng.CreateType == ECreateType.Route)
                    {
                        ToolStripMenuItem_开始编辑测距.Enabled = false;
                        ToolStripMenuItem_结束编辑测距.Enabled = true;
                    }
                    else
                    {
                        ToolStripMenuItem_开始编辑测距.Enabled = true;
                        ToolStripMenuItem_结束编辑测距.Enabled = false;
                    }
                    if (_mng.CurrentCtrl != null
                        && _mng.CurrentCtrl.CPType == ECPType.Child
                        && _mng.OverlayCollection.CurrentOverlayMng.GetCtrl(_mng.CurrentCtrl.MainID) == _mng.FocusCtrl)
                    {
                        ToolStripMenuItem_删除测距点.Enabled = true;
                    }
                    else
                    {
                        ToolStripMenuItem_删除测距点.Enabled = false;
                    }
                }
                else
                {
                    ToolStripMenuItem_开始编辑测距.Enabled = false;
                    ToolStripMenuItem_结束编辑测距.Enabled = false;
                    ToolStripMenuItem_删除测距点.Enabled = false;
                    ToolStripMenuItem_属性测距.Enabled = false;
                }
                if (_mng.FocusCtrl is MapMarkerChart)
                {
                    ToolStripMenuItem_图表数据.Enabled = true;
                }
                else
                {
                    ToolStripMenuItem_图表数据.Enabled = false;
                }
            }
            else
            {
                ToolStripMenuItem_删除焦点.Enabled = false;
                ToolStripMenuItem_显示文本.Enabled = false;
                ToolStripMenuItem_提示信息.Enabled = false;
                ToolStripMenuItem_图表数据.Enabled = false;
                ToolStripMenuItem_开始编辑.Enabled = false;
                结束编辑ToolStripMenuItem.Enabled = false;
                ToolStripMenuItem_开始编辑测距.Enabled = false;
                ToolStripMenuItem_结束编辑测距.Enabled = false;
                ToolStripMenuItem_删除测距点.Enabled = false;
                ToolStripMenuItem_属性测距.Enabled = false;
                ToolStripMenuItem_地图下载.Enabled = false;
            }
        }

        private void mng_MapCtrlDoubleClickEvent(GMapMarker item, MouseEventArgs e)
        {
            GPoint p = gMapControl1.FromLatLngToLocal(item.Position);
            GMap.NET.PointLatLng p1 = gMapControl1.FromLocalToLatLng((int)(p.X - item.Size.Width / 2), (int)(p.Y - item.Size.Height / 2));
            GMap.NET.PointLatLng p2 = gMapControl1.FromLocalToLatLng((int)(p.X + item.Size.Width / 2), (int)(p.Y + item.Size.Height / 2));
            double x1 = Math.Min(p1.Lng, p2.Lng);
            double y1 = Math.Max(p1.Lat, p2.Lat);
            double x2 = Math.Max(p1.Lng, p2.Lng);
            double y2 = Math.Min(p1.Lat, p2.Lat);

            RectLatLng area = new RectLatLng(y1, x1, x2 - x1, y1 - y2);

            if (!area.IsEmpty)
            {
                long count = 1;

                Dictionary<int, long> roomData = new Dictionary<int, long>();
                for (int i = (int)gMapControl1.Zoom; i <= 18; i++)
                {
                    count = gMapControl1.MapProvider.Projection.GetAreaTileCount(area, i, 0);
                    roomData.Add(i, count);
                }

                Form_DownSet form = new Form_DownSet(roomData);
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        gMapControl1.ImageSavePath = folderBrowserDialog1.SelectedPath;
                        List<int> roomList = form.Room;
                        gMapControl1.IsDownLoad = true; ;
                        for (int i = (int)gMapControl1.Zoom; i <= 18; i++)
                        {
                            if (roomList.Contains(i))
                            {
                                using (TilePrefetcher obj = new TilePrefetcher())
                                {
                                    obj.Shuffle = gMapControl1.Manager.Mode != AccessMode.CacheOnly;

                                    obj.Owner = this;
                                    obj.Start(area, i, gMapControl1.MapProvider, gMapControl1.Manager.Mode == AccessMode.CacheOnly ? 0 : 100, gMapControl1.Manager.Mode == AccessMode.CacheOnly ? 0 : 1);
                                }
                            }
                        }

                        MessageBox.Show("下载完成！");

                    }
                }
            }
            else
            {
                MessageBox.Show("请设置一个有效的下载区域！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gMapControl_OnMapZoomChanged()
        {
            this.label_级别.Text = gMapControl1.Zoom.ToString();
        }

        private void mng_MapMouseMoveEvent(object sender, MouseEventArgs e)
        {
            PointLatLng pnew = gMapControl1.FromLocalToLatLng(e.X, e.Y);
            this.label_坐标.Text = pnew.Lng.ToString() + "," + pnew.Lat.ToString();
        }

        private void toolStripMenuItem_高德_Click(object sender, EventArgs e)
        {

        }
        private void toolStripMenuItem_普通地图_Click(object sender, EventArgs e)
        {
            try
            {
                //DeleteCache();

                this.toolStripMenuItem_普通地图.Checked = true;
                this.toolStripMenuItem_卫星地图.Checked = false;
                this.toolStripMenuItem_公路地图.Checked = false;
                this.toolStripMenuItem_路网地图.Checked = false;
                this.toolStripMenuItem_URL.Checked = false;
                gMapControl1.MapProvider.UrlFormat = ConfigurationManager.AppSettings["GaoDe"].ToString();
                gMapControl1.MapProvider.SetDbId(_id_普通);
                gMapControl1.ReloadMap();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void toolStripMenuItem_卫星地图_Click(object sender, EventArgs e)
        {
            try
            {
                //DeleteCache();

                this.toolStripMenuItem_普通地图.Checked = false;
                this.toolStripMenuItem_卫星地图.Checked = true;
                this.toolStripMenuItem_公路地图.Checked = false;
                this.toolStripMenuItem_路网地图.Checked = false;
                this.toolStripMenuItem_URL.Checked = false;
                gMapControl1.MapProvider.UrlFormat = ConfigurationManager.AppSettings["GaoDeWX"].ToString();
                gMapControl1.MapProvider.SetDbId(_id_卫星);
                gMapControl1.ReloadMap();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void toolStripMenuItem_公路地图_Click(object sender, EventArgs e)
        {
            try
            {
                //DeleteCache();

                this.toolStripMenuItem_普通地图.Checked = false;
                this.toolStripMenuItem_卫星地图.Checked = false;
                this.toolStripMenuItem_公路地图.Checked = true;
                this.toolStripMenuItem_路网地图.Checked = false;
                this.toolStripMenuItem_URL.Checked = false;
                gMapControl1.MapProvider.UrlFormat = ConfigurationManager.AppSettings["GaoDeGL"].ToString();
                gMapControl1.MapProvider.SetDbId(_id_公路);
                gMapControl1.ReloadMap();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void toolStripMenuItem_路网地图_Click(object sender, EventArgs e)
        {
            try
            {
                //DeleteCache();

                this.toolStripMenuItem_普通地图.Checked = false;
                this.toolStripMenuItem_卫星地图.Checked = false;
                this.toolStripMenuItem_公路地图.Checked = false;
                this.toolStripMenuItem_路网地图.Checked = true;
                this.toolStripMenuItem_URL.Checked = false;
                gMapControl1.MapProvider.UrlFormat = ConfigurationManager.AppSettings["GaoDeLW"].ToString();
                gMapControl1.MapProvider.SetDbId(_id_路网);
                gMapControl1.ReloadMap();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void toolStripMenuItem_URL_Click(object sender, EventArgs e)
        {
            try
            {
                //DeleteCache();

                this.toolStripMenuItem_普通地图.Checked = false;
                this.toolStripMenuItem_卫星地图.Checked = false;
                this.toolStripMenuItem_公路地图.Checked = false;
                this.toolStripMenuItem_路网地图.Checked = false;
                this.toolStripMenuItem_URL.Checked = true;
                gMapControl1.MapProvider.UrlFormat = ConfigurationManager.AppSettings["URL"].ToString();
                gMapControl1.MapProvider.SetDbId(_id_自定义);
                gMapControl1.Position = new PointLatLng(30.6639034374516, 104.0625);
                gMapControl1.Zoom = 10;
                gMapControl1.ReloadMap();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void toolStripMenuItem_地址配置_Click(object sender, EventArgs e)
        {
            try
            {
                Form_Config form = new Form_Config();
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    DeleteCache();

                    if (this.toolStripMenuItem_普通地图.Checked)
                    {
                        gMapControl1.MapProvider.UrlFormat = ConfigurationManager.AppSettings["GaoDe"].ToString();
                    }
                    else if (this.toolStripMenuItem_卫星地图.Checked)
                    {
                        gMapControl1.MapProvider.UrlFormat = ConfigurationManager.AppSettings["GaoDeWX"].ToString();
                    }
                    else if (this.toolStripMenuItem_公路地图.Checked)
                    {
                        gMapControl1.MapProvider.UrlFormat = ConfigurationManager.AppSettings["GaoDeGL"].ToString();
                    }
                    else if (this.toolStripMenuItem_路网地图.Checked)
                    {
                        gMapControl1.MapProvider.UrlFormat = ConfigurationManager.AppSettings["GaoDeLW"].ToString();
                    }
                    else
                    {
                        gMapControl1.MapProvider.UrlFormat = ConfigurationManager.AppSettings["URL"].ToString();
                        gMapControl1.Position = new PointLatLng(30.6639034374516, 104.0625);
                        gMapControl1.Zoom = 10;
                    }

                    gMapControl1.ReloadMap();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void comboBox_取数模式_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (this.comboBox_取数模式.SelectedItem.ToString())
                {
                    case "在线服务":
                        gMapControl1.Manager.Mode = AccessMode.ServerOnly;
                        break;
                    case "服务和缓存":
                        gMapControl1.Manager.Mode = AccessMode.ServerAndCache;
                        break;
                    case "本地缓存":
                        gMapControl1.Manager.Mode = AccessMode.CacheOnly;
                        break;
                    default:
                        break;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DeleteCache()
        {
            if (this.gMapControl1.Manager.Mode == AccessMode.ServerAndCache)
            {
                GMap.NET.CacheProviders.SQLitePureImageCache.DeleteData();
            }
        }

        private void toolStripMenuItem_软件说明_Click(object sender, EventArgs e)
        {
            Form_Info form = new Form_Info();
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                GeoCoderStatusCode status = gMapControl1.SetPositionByKeywords(this.textBox_地名.Text);
                if (status != GeoCoderStatusCode.G_GEO_SUCCESS)
                {
                    MessageBox.Show("Can't find: '" + textBox_地名.Text + "', reason: " + status.ToString(), "GMap.NET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ToolStripMenuItem_结束编辑_Click(object sender, EventArgs e)
        {
            try
            {
                _mng.RestCtrl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ToolStripMenuItem_删除焦点_Click(object sender, EventArgs e)
        {
            try
            {
                _mng.DeleteCtrl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ToolStripMenuItem_删除_Click(object sender, EventArgs e)
        {
            try
            {
                _mng.DeleteDot();
                gMapControl1.CanDragMap = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ToolStripMenuItem_显示文本_Click(object sender, EventArgs e)
        {
            try
            {
                _mng.SetShowText();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ToolStripMenuItem_提示信息_Click(object sender, EventArgs e)
        {
            try
            {
                _mng.SetTipText();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ToolStripMenuItem_图表数据_Click(object sender, EventArgs e)
        {
            try
            {
                Form_ChartData form = new Form_ChartData(((MapMarkerChart)_mng.FocusCtrl).Chart);
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    gMapControl1.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ToolStripMenuItem_开始编辑_Click(object sender, EventArgs e)
        {
            _mng.CreateType = ECreateType.Polygon;
        }

        private void 结束编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _mng.RestCtrl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ToolStripMenuItem_删除测距点_Click(object sender, EventArgs e)
        {
            try
            {
                _mng.DeleteRouteChild();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ToolStripMenuItem_开始编辑测距_Click(object sender, EventArgs e)
        {
            try
            {
                _mng.CreateType = ECreateType.Route;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ToolStripMenuItem_结束编辑测距_Click(object sender, EventArgs e)
        {
            try
            {
                _mng.RestCtrl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ToolStripMenuItem_属性测距_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mng.FocusCtrl != null && _mng.FocusCtrl is MapRouteDistance)
                {
                    Form_RouteSet form = new Form_RouteSet();
                    form.ShowTotal = ((MapRouteDistance)_mng.FocusCtrl).ShowTotal;
                    form.FirstVisible = ((MapRouteDistance)_mng.FocusCtrl).First_Visible;
                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        ((MapRouteDistance)_mng.FocusCtrl).ShowTotal = form.ShowTotal;
                        ((MapRouteDistance)_mng.FocusCtrl).First_Visible = form.FirstVisible;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ToolStripMenuItem_地图下载_Click(object sender, EventArgs e)
        {
            try
            {
                if (_mng.FocusCtrl != null
                    && _mng.FocusCtrl is MapMarkerRect
                    && _mng.FocusCtrl.IsDoubleClick)
                {
                    mng_MapCtrlDoubleClickEvent((GMapMarker)_mng.FocusCtrl, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

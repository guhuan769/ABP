using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET;

namespace GMap.NET.Mng
{
    public class ChartMng
    {
        public ChartMng(GMapMarker marker)
        {
            _marker = marker;
            ChartFirst();
        }

        private GMapMarker _marker;

        #region 添加属性
        private DataTable _dataSource = null;
        /// <summary>
        /// 连接数据表
        /// </summary>
        [Browsable(true), Category("图表属性设置"), Description("连接数据表")]   //在“属性”窗口中显示DataSource属性
        public DataTable DataSource
        {
            get { return _dataSource; }
            set
            {
                _dataSource = value;
                ChartFirst();
                //Invalidate();
            }
        }

        public enum AxesStyle
        {
            Null = 0,//无
            X = 1,//对X轴进行汇总
            Y = 2,//对Y轴进行汇总
            XY = 3,//对XY轴进行汇总            
        }

        private AxesStyle _sumAxes = AxesStyle.Null;
        /// <summary>
        /// 设置进行汇总的轴
        /// </summary>
        [Browsable(true), Category("图表属性设置"), Description("设置进行汇总的轴")] //在“属性”窗口中显示SumAxes属性
        public AxesStyle SumAxes
        {
            get { return _sumAxes; }
            set
            {
                _sumAxes = value;
                if (_sumAxes != AxesStyle.Null)
                {
                    this.SumYAxis = "";
                }
            }
        }

        private string _sumXAxis = "";
        /// <summary>
        /// 设置统计字段
        /// </summary>
        [Browsable(true), Category("图表属性设置"), Description("设置统计字段")]   //在“属性”窗口中显示SumXAxis属性
        public string SumXAxis
        {
            get { return _sumXAxis; }
            set
            {
                _sumXAxis = value;
            }
        }

        private string _sumYAxis = "";
        /// <summary>
        /// 设置统计的数据字段
        /// </summary>
        [Browsable(true), Category("图表属性设置"), Description("设置统计的数据字段")]   //在“属性”窗口中显示SumYAxis属性
        public string SumYAxis
        {
            get { return _sumYAxis; }
            set
            {
                _sumYAxis = value;
                if (_sumYAxis != "")
                    this.SumAxes = AxesStyle.Null;
            }
        }

        private CharMode _chartStyle = CharMode.Bar;
        /// <summary>
        /// 图表的类型
        /// </summary>
        [Browsable(true), Category("图表属性设置"), Description("图表的类型")]   //在“属性”窗口中显示ChartStyle属性
        public CharMode ChartStyle
        {
            get
            {
                return _chartStyle;
            }
            set
            {
                _chartStyle = value;
                if (this.ChartStyle == CharMode.Bar || this.ChartStyle == CharMode.Mark)
                {
                    if (this.RowWeave == CharRowWeaveStyle.Stavked)
                    {
                        if (this.RowList == 1)
                            RowSideMax();
                        else
                            RowStackedSum();
                    }

                    if (this.RowWeave == CharRowWeaveStyle.Side)
                        RowSideMax();
                }
                if (this.ChartStyle == CharMode.Line)
                    RowSideMax();
                if (this.ChartStyle == CharMode.Area)
                {
                    AreaPercent();
                }
                //Invalidate();
            }
        }

        private ChartTitleStyle _titleStyle = ChartTitleStyle.TopCenter;
        /// <summary>
        /// 图表的标题位置
        /// </summary>
        [Browsable(true), Category("图表属性设置"), Description("图表的标题位置")]   //在“属性”窗口中显示TitleStyle属性

        public ChartTitleStyle TitleStyle
        {
            get { return _titleStyle; }
            set
            {
                _titleStyle = value;
                //Invalidate();
            }
        }

        private Color _titleColor = Color.Black;
        /// <summary>
        /// 图表的标题颜色
        /// </summary>
        [Browsable(true), Category("图表属性设置"), Description("图表的标题颜色")]   //在“属性”窗口中显示TitleColor属性

        public Color TitleColor
        {
            get { return _titleColor; }
            set
            {
                _titleColor = value;
                //Invalidate();
            }
        }

        private Color _chartColor = Color.Red;
        /// <summary>
        /// 图表的图形颜色
        /// </summary>
        [Browsable(true), Category("图表属性设置"), Description("图表的图形颜色")]   //在“属性”窗口中显示ChartColor属性
        public Color ChartColor
        {
            get { return _chartColor; }
            set
            {
                _chartColor = value;
                //Invalidate();
            }
        }

        private bool _chartWearColor = false;
        /// <summary>
        /// 图表的图形多个颜色
        /// </summary>
        [Browsable(true), Category("图表属性设置"), Description("图表的图形多个颜色")]   //在“属性”窗口中显示ChartWearColor属性
        public bool ChartWearColor
        {
            get { return _chartWearColor; }
            set
            {
                _chartWearColor = value;
                //if (this.RowList == 1)
                //    if (this.ChartStyle == CharMode.Bar)
                //Invalidate();
            }
        }

        private bool _chartmark = true;
        /// <summary>
        /// 是否显示数据标签
        /// </summary>
        [Browsable(true), Category("图表属性设置"), Description("是否显示数据标签")]   //在“属性”窗口中显示Chartmark属性
        public bool Chartmark
        {
            get { return _chartmark; }
            set
            {
                _chartmark = value;
                //Invalidate();
            }
        }

        private string _titleName = "Title";
        /// <summary>
        /// 图表的标题
        /// </summary>
        [Browsable(true), Category("图表属性设置"), Description("图表的标题")]   //在“属性”窗口中显示TitleName属性
        public string TitleName
        {
            get { return _titleName; }
            set
            {
                _titleName = value;
                //Invalidate();
            }
        }

        private Font _titleFont = new Font("宋体", 9);
        /// <summary>
        /// 图表的标题文字样式
        /// </summary>
        [Browsable(true), Category("图表属性设置"), Description("图表的标题文字样式")]   //在“属性”窗口中显示TitleFont属性
        public Font TitleFont
        {
            get { return _titleFont; }
            set
            {
                _titleFont = value;
                //Invalidate();
            }
        }

        private int _foulCalcar = 10;
        /// <summary>
        /// 图表的边距
        /// </summary>
        [Browsable(true), Category("图表属性设置"), Description("图表的边距")]   //在“属性”窗口中显示FoulCalcar属性
        public int FoulCalcar
        {
            get { return _foulCalcar; }
            set
            {
                _foulCalcar = value;
                if (_foulCalcar < 0)
                    _foulCalcar = 0;
                //Invalidate();
            }
        }

        private Color _foulLineColor = Color.Black;
        /// <summary>
        /// 图表的边线颜色
        /// </summary>
        [Browsable(true), Category("图表属性设置"), Description("图表的边线颜色")]   //在“属性”窗口中显示FoulLineColor属性
        public Color FoulLineColor
        {
            get { return _foulLineColor; }
            set
            {
                _foulLineColor = value;
                //Invalidate();
            }
        }

        private Font _XYFont = new Font("宋体", 9);
        /// <summary>
        /// 图表X、Y轴的文字样式
        /// </summary>
        [Browsable(true), Category("图表属性设置"), Description("图表X、Y轴的文字样式")]   //在“属性”窗口中显示XYFont属性
        public Font XYFont
        {
            get { return _XYFont; }
            set
            {
                _XYFont = value;
                //Invalidate();
            }
        }

        private Color _XYColor = Color.Black;
        /// <summary>
        /// 图表中XY轴标识文字的颜色
        /// </summary>
        [Browsable(true), Category("图表属性设置"), Description("图表中XY轴标识文字的颜色")]   //在“属性”窗口中显示XYColor属性
        public Color XYColor
        {
            get { return _XYColor; }
            set
            {
                _XYColor = value;
                //Invalidate();
            }
        }

        private int _pageList = 10;
        /// <summary>
        /// 设置图表中每页的列数
        /// </summary>
        [Browsable(true), Category("图表属性设置"), Description("设置图表中每页的列数")]   //在“属性”窗口中显示PageList属性
        public int PageList
        {
            get { return _pageList; }
            set
            {
                _pageList = value;
                if (_pageList < 1)
                    _pageList = 1;
                if (this.DataSource == null)
                    ChartFirst();
                if (this.ChartStyle == CharMode.Bar || this.ChartStyle == CharMode.Mark)
                {
                    if (this.RowWeave == CharRowWeaveStyle.Stavked)
                    {
                        if (this.RowList == 1)
                            RowSideMax();
                        else
                            RowStackedSum();
                    }
                    if (this.RowWeave == CharRowWeaveStyle.Side)
                        RowSideMax();
                }
                if (this.ChartStyle == CharMode.Area)
                {
                    AreaPercent();
                }
                //Invalidate();
            }
        }

        private int _rowList = 1;
        /// <summary>
        /// 设置每列中数据个数
        /// </summary>
        [Browsable(true), Category("图表属性设置"), Description("设置每列中数据个数")]   //在“属性”窗口中显示RowList属性
        public int RowList
        {
            get { return _rowList; }
            set
            {
                _rowList = value;
                if (_rowList < 1)
                    _rowList = 1;
                if (this.DataSource == null)
                    ChartFirst();
                if (this.ChartStyle == CharMode.Bar || this.ChartStyle == CharMode.Mark)
                {
                    if (this.RowWeave == CharRowWeaveStyle.Stavked)
                    {
                        if (this.RowList == 1)
                            RowSideMax();
                        else
                            RowStackedSum();
                    }
                    if (this.RowWeave == CharRowWeaveStyle.Side)
                        RowSideMax();
                }
                if (this.ChartStyle == CharMode.Area)
                {
                    AreaPercent();
                }
                //Invalidate();
            }
        }

        private bool _labelSay = false;
        /// <summary>
        /// 设置图表的标签说明
        /// </summary>
        [Browsable(true), Category("图表属性设置"), Description("设置图表的标签说明")]   //在“属性”窗口中显示LabelSay属性
        public bool LabelSay
        {
            get { return _labelSay; }
            set
            {
                _labelSay = value;
                if (this.ShowData == false)
                    _labelSay = false;
                //Invalidate();
            }
        }

        public enum CharRowWeaveStyle
        {
            Side = 0,//侧面显示
            Stavked = 1,//单列组合显示
        }

        private CharRowWeaveStyle _rowWeave = CharRowWeaveStyle.Side;
        /// <summary>
        /// 多列图表的组合样式
        /// </summary>
        [Browsable(true), Category("图表属性设置"), Description("多列图表的组合样式")]   //在“属性”窗口中显示RowWeave属性
        public CharRowWeaveStyle RowWeave
        {
            get { return _rowWeave; }
            set
            {
                _rowWeave = value;
                if (this.RowList > 1)
                {
                    if (this.ChartStyle == CharMode.Bar || this.ChartStyle == CharMode.Mark)
                    {
                        if (_rowWeave == CharRowWeaveStyle.Stavked)
                        {
                            if (this.RowList == 1)
                                RowSideMax();
                            else
                                RowStackedSum();
                        }
                        if (_rowWeave == CharRowWeaveStyle.Side)
                            RowSideMax();

                        //Invalidate();
                    }
                }
            }
        }

        private bool _showData = true;
        /// <summary>
        /// 是否显示图表中的数据
        /// </summary>
        [Browsable(true), Category("图表属性设置"), Description("是否显示图表中的数据")]   //在“属性”窗口中显示ShowData属性
        public bool ShowData
        {
            get { return _showData; }
            set
            {
                _showData = value;
                if (_showData == false)
                    this.ChartStyle = CharMode.none;
                else
                    this.ChartStyle = CharMode.Bar;
                ChartFirst();
                //Invalidate();
            }
        }

        private int _areaAngle = 0;
        /// <summary>
        /// 设置饼形图的旋转角度
        /// </summary>
        [Browsable(true), Category("图表属性设置"), Description("设置饼形图的旋转角度")]   //在“属性”窗口中显示AreaAngle属性
        public int AreaAngle
        {
            get { return _areaAngle; }
            set
            {
                _areaAngle = value;
                if (_areaAngle < 0)
                    _areaAngle = 360;
                if (_areaAngle > 360)
                    _areaAngle = 0;
                //Invalidate();
            }
        }
        #endregion

        #region 公共变量
        public Color[] WearColor = new Color[] { Color.OrangeRed,Color.ForestGreen,Color.Maroon,Color.CornflowerBlue,Color.Fuchsia,Color.YellowGreen,Color.DarkOrange,
            Color.Purple,Color.SlateBlue,Color.Red,Color.DarkBlue,Color.Crimson,Color.DarkSlateGray, Color.Thistle,Color.Blue,Color.LimeGreen,Color.DarkRed,Color.SlateGray};
        public int _initialize = 0;
        public float[,] _zData;//记录数据的值
        public float _yMax = 0;//数值的最大值
        public float _loadS = 0;
        public float _yHeightM = 0;
        public int[] _xData;//记录X轴的标识数
        public string[] _xText;//记录X轴的标识数
        public string[] _zText;//记录X轴的标识数
        public float[] _szData;//获取每列的和
        public float _aSum;//记录饼形的总和
        public float _xSize = 0;//X轴的大小
        public float _ySize = 0;//Y轴的大小
        public float _temXSize = 0;//X轴的临时大小
        public float _xLeft = 0;//X轴的左端点
        public float _xRight = 0;//X轴的右端点
        public float _yUp = 0;//Y轴的上端点
        public float _yDown = 0;//Y轴的下端点
        public float _yUnit = 0;//纵向的单元格宽度
        public float _xUnit = 0;//横向的单元格宽度
        public float _temXLeft = 0;//X轴的左端点初始化
        public Pen _mypen = new Pen(Color.Black, 1);//设置线的颜色及粗细
        public Pen _penvoid = new Pen(Color.DarkGray, 1);//设置线的颜色及粗细
        public SolidBrush _mybrush = new SolidBrush(Color.Red);
        public Brush _bXYColor = new SolidBrush(Color.Black);
        public float _titHeight = 0;//记录标题的高度
        public float _chartStyle_sign = 0;//图表样式的标识
        public float _rowL = 0;//记录每Y列中的个数
        public float _pageL = 0;//记录X轴Y列的个数
        public int _xcou = 0;
        public bool _is3DBar = true;
        #endregion

        #region 数据的初始化
        /// <summary>
        /// 数据的初始化
        /// </summary>
        public void ChartFirst()
        {
            Random rand = new Random();//随机获取100以内的数据
            _rowL = this.RowList;//记录每列中的记录个数
            _pageL = this.PageList;//记录Y轴的列数

            if (this.DataSource == null)
            {
                DataTable _table = new DataTable();
                _table.Columns.Add("统计列");
                _table.Columns.Add("统计值");

                DataRow newRow1 = _table.NewRow();
                newRow1["统计列"] = "1月";
                newRow1["统计值"] = rand.Next(1, 1000);
                _table.Rows.Add(newRow1);
                DataRow newRow2 = _table.NewRow();
                newRow2["统计列"] = "2月";
                newRow2["统计值"] = rand.Next(1, 1000);
                _table.Rows.Add(newRow2);
                DataRow newRow3 = _table.NewRow();
                newRow3["统计列"] = "3月";
                newRow3["统计值"] = rand.Next(1, 1000);
                _table.Rows.Add(newRow3);
                DataRow newRow4 = _table.NewRow();
                newRow4["统计列"] = "4月";
                newRow4["统计值"] = rand.Next(1, 1000);
                _table.Rows.Add(newRow4);
                DataRow newRow5 = _table.NewRow();
                newRow5["统计列"] = "5月";
                newRow5["统计值"] = rand.Next(1, 1000);
                _table.Rows.Add(newRow5);
                DataRow newRow6 = _table.NewRow();
                newRow6["统计列"] = "6月";
                newRow6["统计值"] = rand.Next(1, 1000);
                _table.Rows.Add(newRow6);

                this.DataSource = _table;
                this.SumXAxis = "统计列";
                this.SumYAxis = "统计值";
                this.TitleName = "月数据统计";
            }

            int rowC = 0;
            int ColumnC = 0;

            if (this.SumAxes != AxesStyle.Null && this.SumXAxis != "")
            {
                rowC = DataSource.Rows.Count;
                ColumnC = DataSource.Columns.Count;
                if (this.SumAxes == AxesStyle.X)
                    ColumnC = ColumnC - 1;
                if (this.SumAxes == AxesStyle.Y)
                    rowC = rowC - 1;
                if (this.SumAxes == AxesStyle.XY)
                {
                    rowC = rowC - 1;
                    ColumnC = ColumnC - 1;

                }

                _zData = new float[rowC, ColumnC];//记录随机数据一维数组
                _xData = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };//记录随机数据一维数组
                _xText = new string[rowC];
                _zText = new string[ColumnC - 1];//记录X轴的标识数
                int temCloumn = 0;
                for (int i = 0; i < ColumnC; i++)
                {
                    if (DataSource.Columns[i].Caption.Trim() == this.SumXAxis.Trim())
                    {
                        for (int j = 0; j < rowC; j++)
                        {
                            _xText[j] = DataSource.Rows[j][i].ToString();
                        }
                        temCloumn = temCloumn + 1;
                    }
                    else
                    {
                        _zText[i - temCloumn] = DataSource.Columns[i].Caption.Trim();
                        for (int j = 0; j < rowC; j++)
                        {
                            _zData[j, i - temCloumn] = Convert.ToSingle(DataSource.Rows[j][i]);
                        }
                    }
                }
                this.PageList = _xText.Length;
                this.RowList = _zText.Length;
                _pageL = this.PageList;
                _rowL = this.RowList;
                if (this.RowWeave == CharRowWeaveStyle.Side)
                    RowStackedSum();
                else
                    RowSideMax();
                //RowSideMax();
            }
            if (this.SumXAxis != "" && this.SumYAxis != "")
            {
                rowC = DataSource.Rows.Count;
                ColumnC = DataSource.Columns.Count;
                _zData = new float[rowC, ColumnC];//记录随机数据一维数组
                _xData = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };//记录随机数据一维数组
                _xText = new string[rowC];
                _zText = new string[ColumnC - 1];//记录X轴的标识数
                int temCloumn = 0;
                for (int i = 0; i < ColumnC; i++)
                {
                    if (DataSource.Columns[i].Caption.Trim() == this.SumXAxis.Trim())
                    {
                        for (int j = 0; j < rowC; j++)
                        {
                            _xText[j] = DataSource.Rows[j][i].ToString();
                        }
                        temCloumn = temCloumn + 1;
                    }
                    else
                    {
                        _zText[i - temCloumn] = DataSource.Columns[i].Caption.Trim();
                        for (int j = 0; j < rowC; j++)
                        {
                            _zData[j, i - temCloumn] = Convert.ToSingle(DataSource.Rows[j][i]);
                        }
                    }
                }
                this.PageList = _xText.Length;
                this.RowList = _zText.Length;
                _pageL = this.PageList;
                _rowL = this.RowList;
                if (this.RowWeave == CharRowWeaveStyle.Side)
                    RowStackedSum();
                else
                    RowSideMax();
            }

        }
        #endregion

        #region 获取饼形图的值
        /// <summary>
        /// 获取饼形图的值
        /// </summary>
        public void AreaPercent()
        {
            RowStackedSum();
            _aSum = 0;
            for (int i = 0; i < _szData.Length; i++)
                _aSum = _aSum + _szData[i];
            if (_aSum == 0)
            {
                this.ChartStyle = CharMode.none;
                _yMax = 10;
                _yHeightM = 100;
                //Invalidate();
                return;
            }
        }

        #endregion

        #region 单列多数据最大值
        /// <summary>
        /// 单列多数据最大值
        /// </summary>
        public void RowSideMax()
        {
            _yMax = 0;
            _szData = new float[_xText.Length];
            for (int j = 0; j < _xText.Length; j++)
            {
                for (int i = 0; i < _zText.Length; i++)
                {
                    _szData[j] = _zData[j, i];
                    if (_yMax < _zData[j, i])
                        _yMax = _zData[j, i];
                }
            }

            double nY = _yMax + _yMax * 0.3;
            if (nY == 0)
            {
                this.ChartStyle = CharMode.none;
                _yMax = 10;
                _yHeightM = 100;
                //Invalidate();
                return;
            }

            if (nY < 11)//设置X轴的刻度
            {
                _yHeightM = (float)Math.Ceiling(nY);//Y轴的最大高度
                _xcou = (int)(Math.Ceiling(nY + 1));
                _xData = new int[_xcou];//记录随机数据一维数组
                for (int i = 0; i < _xData.Length; i++)
                    _xData[i] = 0;
            }
            else
            {
                _xData = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                _yMax = (int)(Math.Ceiling(nY / 10));
                _yHeightM = _yMax * 10;//Y轴的最大高度
            }
        }
        #endregion

        #region 单列数据求和
        /// <summary>
        /// 单列数据求和
        /// </summary>
        public void RowStackedSum()
        {
            float Szsum = 0;
            _yMax = 0;
            _szData = new float[_xText.Length];
            for (int i = 0; i < _xText.Length; i++)
            {
                Szsum = 0;
                for (int j = 0; j < _zText.Length; j++)
                    Szsum = Szsum + _zData[i, j];
                _szData[i] = Szsum;
                if (_yMax < _szData[i])
                    _yMax = _szData[i];
            }

            double nY = _yMax + _yMax * 0.2;

            if (nY == 0)
            {
                this.ChartStyle = CharMode.none;
                _yMax = 10;
                _yHeightM = 100;
                //Invalidate();
                return;
            }
            if (nY < 11)//设置X轴的刻度
            {
                _yHeightM = (float)Math.Ceiling(nY);//Y轴的最大高度
                _xcou = (int)Math.Ceiling(nY + 1);
                _xData = new int[_xcou];//记录随机数据一维数组
                for (int i = 0; i < _xData.Length; i++)
                    _xData[i] = 0;
            }
            else
            {
                _xData = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                _yMax = (int)(Math.Ceiling(nY / 10));
                _yHeightM = _yMax * 10;//Y轴的最大高度
            }
        }
        #endregion

        #region 绘制图表的标题
        /// <summary>
        /// 绘制图表的标题
        /// </summary>
        /// <param g="Graphics">封装一个绘图的类对象</param>
        /// <returns>返回int对象</returns>
        private int ProtractTitle(Graphics g, int x, int y)
        {
            Brush BTitColor = new SolidBrush(this.TitleColor);
            string TitS = this.TitleName;//获取图表标题的名称
            SizeF TitSize = g.MeasureString(TitS, this.TitleFont);//将绘制的字符串进行格式化
            float TitWidth = TitSize.Width;//获取字符串的宽度
            _titHeight = TitSize.Height;//获取字符串的高度

            float TitX = 0;//标题的横向坐标
            float TitY = 0;//标题的纵向坐标
            int TitPlace = 0;//标题的位置
            switch (TitleStyle.ToString())
            {
                case "TopLeft":
                    {
                        TitY = this.FoulCalcar;
                        TitX = this.FoulCalcar;
                        TitPlace = 1;
                        break;
                    }
                case "TopCenter":
                    {
                        TitY = this.FoulCalcar;
                        TitX = (int)((_marker.Size.Width - TitWidth) / 2);
                        TitPlace = 1;
                        break;
                    }
                case "TopRight":
                    {
                        TitY = this.FoulCalcar;
                        TitX = _marker.Size.Width - TitWidth - this.FoulCalcar;
                        TitPlace = 1;
                        break;
                    }
                case "BottomLeft":
                    {
                        TitY = _marker.Size.Height - _titHeight - this.FoulCalcar;
                        TitX = this.FoulCalcar;
                        TitPlace = 2;
                        break;
                    }
                case "BottomCenter":
                    {
                        TitY = _marker.Size.Height - _titHeight - this.FoulCalcar;
                        TitX = (int)((_marker.Size.Width - TitWidth) / 2);
                        TitPlace = 2;
                        break;
                    }
                case "BottomRight":
                    {
                        TitY = _marker.Size.Height - _titHeight - this.FoulCalcar;
                        TitX = _marker.Size.Width - TitWidth - this.FoulCalcar;
                        TitPlace = 2;
                        break;
                    }
            }

            //给制图表的标题
            if (this.TitleName == "")
                TitPlace = 0;
            else
                g.DrawString(this.TitleName, this.TitleFont, BTitColor, new PointF(TitX + x, TitY + y));
            return TitPlace;
        }
        #endregion

        #region 绘制图表的网格
        /// <summary>
        /// 获取绘制网格的基础信息
        /// </summary>
        /// <param g="Graphics">封装一个绘图的类对象</param>
        /// <param TitPlace="int">标题的位置</param>
        private void ProtractXY(Graphics g, int TitPlace, int x, int y)
        {
            _mypen = new Pen(this.FoulLineColor, 1);//设置线的颜色及粗细
            _mybrush = new SolidBrush(this.ChartColor);
            //获取X轴上最大标识符的高度和宽度
            //Graphics TitG = this.CreateGraphics();//创建Graphics类对象
            SizeF XMaxSize = g.MeasureString(_xText[0], this._XYFont);//将绘制的字符串进行格式化
            int XMaxWidth = (int)(XMaxSize.Width);//获取字符串的宽度
            int XMaxHeight = (int)(XMaxSize.Height);//获取字符串的高度

            switch (TitPlace)//获取Y轴的高度
            {
                case 0:
                    {
                        _ySize = _marker.Size.Height - this.FoulCalcar * 2;
                        _yUp = _marker.Size.Height - (_marker.Size.Height - this.FoulCalcar);
                        break;
                    }
                case 1:
                    {
                        _ySize = _marker.Size.Height - this.FoulCalcar * 3 - _titHeight;
                        _yUp = _marker.Size.Height - (_marker.Size.Height - this.FoulCalcar * 2 - _titHeight);
                        break;
                    }
                case 2:
                    {
                        _ySize = _marker.Size.Height - this.FoulCalcar * 3 - _titHeight;
                        _yUp = _marker.Size.Height - (_marker.Size.Height - this.FoulCalcar);
                        break;
                    }

            }

            if (this.TitleName == "")
            {
                _ySize = _ySize - XMaxHeight;
                _yUp = _yUp + XMaxHeight;
            }

            switch (this.ChartStyle)
            {
                case CharMode.Bar:
                case CharMode.Line:
                case CharMode.Mark:
                case CharMode.none:
                    {
                        ProtractBLMXY(g, XMaxWidth, XMaxHeight, x, y);
                        break;
                    }
                case CharMode.Area:
                    {
                        ProtractArea(g, x, y);
                        break;
                    }
            }

        }

        /// <summary>
        /// 绘制条形、线形、面形图表的网格
        /// </summary>
        /// <param g="Graphics">封装一个绘图的类对象</param>
        /// <param TitPlace="int">标题的位置</param>
        /// <param image="Bitmap">图片</param>
        private void ProtractBLMXY(Graphics g, int XMaxWidth, int XMaxHeight, int x, int y)
        {
            //Graphics TitG = this.CreateGraphics();//创建Graphics类对象
            SizeF XMaxSize = g.MeasureString(_xText[0], this._XYFont);//将绘制的字符串进行格式化
            float nyh = 0;
            if (_xData.Length < 10)//设置X轴的刻度
                nyh = _yHeightM;
            else
                nyh = _yMax * 10;

            //MessageBox.Show(_yHeightM.ToString());

            SizeF YMaxSize = g.MeasureString(nyh.ToString(), this._XYFont);//将绘制的字符串进行格式化
            float YMaxWidth = YMaxSize.Width;//获取字符串的宽度
            float YMaxHeight = YMaxSize.Height;//获取字符串的高度
            _yUnit = (_ySize - XMaxHeight - 5) / (_xData.Length - 1);//纵向的单元格宽度

            if (this.ChartStyle == CharMode.Line || this.ChartStyle == CharMode.Mark)
            {
                if (this.LabelSay)
                    _xSize = _marker.Size.Width - this.FoulCalcar * 2 - YMaxWidth - 5;
                else
                {
                    _xSize = _marker.Size.Width - this.FoulCalcar * 2 - YMaxWidth - 5 - XMaxWidth / 2;
                }
            }
            else
                _xSize = _marker.Size.Width - this.FoulCalcar * 2 - YMaxWidth - 5;

            float YHeight = _yUp + _yUnit * (_xData.Length - 1);
            _xLeft = _marker.Size.Width - (_marker.Size.Width - this.FoulCalcar - YMaxWidth - 5);

            //当X轴的标识文字超出边线时，进行设置
            if (XMaxWidth / 2 > (YMaxWidth + 5))
            {
                _xLeft = _xLeft + (XMaxWidth / 2 - (YMaxWidth + 5));
                if (this.LabelSay == false)
                {
                    if (this.ChartStyle == CharMode.Bar)
                        _xSize = _xSize - (XMaxWidth / 2 - (YMaxWidth + 5)) * 2;
                    if (this.ChartStyle == CharMode.Line || this.ChartStyle == CharMode.Mark)
                        _xSize = _xSize - (Convert.ToSingle(XMaxWidth) / 2 - (YMaxWidth + 5));
                }
                else
                    _xSize = _xSize - (XMaxWidth / 2 - (YMaxWidth + 5));
            }
            //-----------------------------------

            _temXLeft = _xLeft;//获取图表的左边线的X坐标点
            if (LabelSay)//是否显示标签
            {
                ProtractLabelSay(g, YHeight - _yUp, x, y);
            }

            if (this.ChartStyle == CharMode.Line || this.ChartStyle == CharMode.Mark)//设置X轴的单元大小
            {
                if (_pageL == 1)
                    _xUnit = _xSize / 1;//如果条形图或面形图为一个值时
                else
                    _xUnit = _xSize / (_pageL - 1);
            }
            else
                _xUnit = _xSize / _pageL;
            float XWidth = 0;//设置X轴的长度
            if (this.ChartStyle == CharMode.Line || this.ChartStyle == CharMode.Mark)//获取X轴的长度
            {
                if (_pageL == 1)//如果条形图或面形图为一个值时
                    XWidth = _xLeft + _xUnit;
                else
                    XWidth = _xLeft + _xUnit * (_pageL - 1);
            }
            else
                XWidth = _xLeft + _xUnit * _pageL;

            int fontAmong = 1;//当文字的宽度大于X轴上的单元格时，设置文本显示的间隔数
            long nn = 0;//获取两数相除的余数
            if (XMaxWidth > _xUnit)//判断X轴上的文字宽度是否大于单元格大小
            {
                fontAmong = (int)(Math.Ceiling(XMaxWidth / _xUnit));//获取间隔数
            }

            _yDown = YHeight;
            Pen temPen = new Pen(Color.Black, 1);//定义临时画笔
            _penvoid.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;//设置虚线
            int LMcount = 0;//设置X轴的列数
            if (this.ChartStyle == CharMode.Line || this.ChartStyle == CharMode.Mark)//获取X轴的列数
            {
                if (_xText.Length == 1)
                    LMcount = 1;
                else
                    LMcount = _xText.Length - 1;
            }
            else
                LMcount = _xText.Length;
            float StrX = 0;//设置X轴文字的X坐标点
            float StrY = 0;//设置X轴文字的Y坐标点
            //_mypen = new Pen(Color.Black, 1);
            //绘制线条
            //绘制纵向线条
            for (int i = 0; i <= LMcount; i++)
            {
                if (i == 0 || i == LMcount)
                    temPen = _mypen;
                else
                    temPen = _penvoid;
                g.DrawLine(temPen, _xLeft + x, _yUp + y, _xLeft + x, YHeight + y);
                if (i != _xText.Length)
                {
                    YMaxSize = g.MeasureString(_xText[i], this._XYFont);//将绘制的字符串进行格式化
                    YMaxWidth = (int)(YMaxSize.Width);//获取字符串的宽度
                    YMaxHeight = (int)(YMaxSize.Height);//获取字符串的高度
                    if (this.ChartStyle == CharMode.Line || this.ChartStyle == CharMode.Mark)
                    {
                        StrX = _xLeft - YMaxWidth / 2;
                        StrY = YHeight + 5;
                    }
                    else
                    {
                        StrX = _xLeft + (_xUnit - YMaxWidth) / 2;
                        StrY = YHeight + 5;
                    }
                    if (i == 0)
                        g.DrawString(_xText[i], this._XYFont, _bXYColor, new PointF(StrX + x, StrY + y));//绘制X轴的标识字体
                    else
                    {
                        Math.DivRem((long)i, (long)fontAmong, out nn);
                        if (nn == 0)
                            g.DrawString(_xText[i], this._XYFont, _bXYColor, new PointF(StrX + x, StrY + y));//绘制X轴的标识字体
                    }

                }
                _xLeft = _xLeft + _xUnit;
            }

            _xLeft = _temXLeft;
            //----------------------------------
            fontAmong = 1;//当文字的高度大于Y轴上的单元格时，设置文本显示的间隔数
            nn = 0;//获取两数相除的余数
            if (YMaxHeight > _yUnit)//判断Y轴上的文字宽度是否大于单元高度
            {
                fontAmong = (int)(Math.Ceiling(YMaxHeight / _yUnit));//获取间隔数
            }

            //绘制横向线条
            for (int i = 0; i < _xData.Length; i++)//设置X轴的横向刻度
            {
                if (i == 0 || i == (_xData.Length - 1))////10
                    temPen = _mypen;
                else
                    temPen = _penvoid;
                g.DrawLine(temPen, _xLeft + x, _yUp + y, XWidth + x, _yUp + y);//绘制横向线条
                if (_xData.Length < 11)//设置X轴的横向刻度
                    nyh = (_xData.Length - 1) - i;
                else
                    nyh = _yHeightM - (_yMax * i);
                YMaxSize = g.MeasureString(nyh.ToString(), this._XYFont);//将绘制的字符串进行格式化
                YMaxWidth = YMaxSize.Width;//获取字符串的宽度
                YMaxHeight = YMaxSize.Height;//获取字符串的高度

                if (i == (_xData.Length - 1))//绘制Y轴的标识字体
                {
                    if (this.ChartStyle == CharMode.Bar)
                    {
                        if (i == 0)
                            g.DrawString(nyh.ToString(), this._XYFont, _bXYColor, new PointF(_xLeft - 5 - YMaxWidth + x, _yUp - YMaxHeight / 2 + y));//绘制Y轴的标识字体
                        else
                        {
                            Math.DivRem((long)i, (long)fontAmong, out nn);
                            if (nn == 0)
                                g.DrawString(nyh.ToString(), this._XYFont, _bXYColor, new PointF(_xLeft - 5 - YMaxWidth + x, _yUp - YMaxHeight / 2 + y));//绘制Y轴的标识字体
                        }
                    }
                }
                else
                {
                    if (i == 0)
                        g.DrawString(nyh.ToString(), this._XYFont, _bXYColor, new PointF(_xLeft - 5 - YMaxWidth + x, _yUp - YMaxHeight / 2 + y));//绘制Y轴的标识字体
                    else
                    {
                        Math.DivRem((long)i, (long)fontAmong, out nn);
                        if (nn == 0)
                            g.DrawString(nyh.ToString(), this._XYFont, _bXYColor, new PointF(_xLeft - 5 - YMaxWidth + x, _yUp - YMaxHeight / 2 + y));//绘制Y轴的标识字体
                    }
                }
                _xData[i] = Convert.ToInt32(nyh);
                _yUp = _yUp + _yUnit;//设置横向线条的间距
            }
        }
        #endregion

        #region 显示标签说明文字
        /// <summary>
        /// 显示标签说明文字
        /// </summary>
        private void ProtractLabelSay(Graphics g, float YHeight, int x, int y)
        {
            string[] temText;//临时存储数据的名称数组
            string temTextSize = "";//存储最长的名称
            Font LSfont = new System.Drawing.Font("宋体", 8);
            if (this.ChartStyle != CharMode.Area)
            {
                if (_zText.Length > 1)
                    temText = _zText;
                else
                    temText = _xText;
                for (int i = 0; i < temText.Length; i++)
                {
                    if (temText[i].Length > temTextSize.Length)
                        temTextSize = temText[i];
                }
            }
            else
            {
                temText = _xText;
                float AresF = 0;

                for (int i = 0; i < temText.Length; i++)
                {
                    AresF = (_szData[i] / _aSum) * 100;
                    AresF = (float)Math.Round(AresF, 3);
                    temTextSize = temText[i] + " " + AresF.ToString() + "%";
                    if (temText[i].Length > temTextSize.Length)
                        temTextSize = temText[i];
                }
            }

            //Graphics TitG = this.CreateGraphics();//创建Graphics类对象
            SizeF XMaxSize = g.MeasureString(temTextSize, LSfont);//将绘制的字符串进行格式化
            float XMaxWidth = XMaxSize.Width;//获取字符串的宽度
            float XMaxHeight = XMaxSize.Height;//获取字符串的高度

            LabelSayPlace(g, YHeight, temText, XMaxWidth, XMaxHeight, LSfont, 0, 0, 0, 0, 0, false, x, y);//绘制标签
        }

        /// <summary>
        /// 显示标签说明文字
        /// </summary>
        /// <param g="Graphics">封装一个绘图的类对象</param>
        /// <param YHeight="float">图表的高度</param>
        /// <param temText="string[]">标签名称的数组</param>
        /// <param XMaxWidth="float">最大字符串的宽度</param>
        /// <param XMaxHeight="float">最大字符串的高度</param>
        /// <param LSfont="Font">标签的字体样式</param>
        /// <param LSRcount="int">标签列的个数</param>
        /// <param TempL="float">字符串的X坐标</param>
        /// <param TempU="float">字符串的Y坐标</param>
        /// <param k="int">循环的起始点</param>
        /// <param p="int">循环的结束点</param>
        /// <param paint="bool">是否绘制</param>
        private void LabelSayPlace(Graphics g, float YHeight, string[] temText, float XMaxWidth, float XMaxHeight, Font LSfont, int Cp, float TempL, float TempU, int k, int p, bool paint, int x, int y)
        {
            float ClumpH = 12;//小方块的高度
            float ClumpW = 6;//小方块的宽度
            float remW = 6;//方块与边界的左右空隙
            float remH = 6;//方块与边界的上下空隙
            float bosom = 4;//标签之间的间隔
            float Wma = 7;//图表和标签之间的间隔
            float LSHeight = 0;//标签的高度
            float LSWidth = 0;//标签的宽度
            float LSLeft = 0;//设置标签左边线的位置
            float LSCount = 0;//标签的最大个数
            int CycCount = 0;//循环个数
            int LSRcount = 0;//标签的列数
            float AreaF = 0;//记录百分比
            string AreaS = "";//记录标识文字

            if (!paint)
            {
                LSHeight = temText.Length * ClumpH + (temText.Length - 1) * bosom + remH * 2;//获取标签的高度

                if (LSHeight >= YHeight)
                {
                    double mm = (YHeight - remH - (remH - bosom)) / (ClumpH + bosom);
                    LSCount = (float)(Math.Floor(mm));
                    LSHeight = LSCount * ClumpH + (LSCount - 1) * bosom + remH * 2;
                    LSCount = (float)(Math.Floor((LSHeight - remH - (remH - bosom)) / (ClumpH + bosom)));
                    if (LSHeight < (remH * 2 + ClumpH))
                        return;
                    CycCount = (int)LSCount;
                    LSRcount = (int)(Math.Ceiling(Convert.ToDouble(temText.Length) / (Convert.ToDouble(CycCount))));
                    LSWidth = (ClumpW + remW + XMaxWidth) * LSRcount + remW * 2;
                    if (LSWidth > (_marker.Size.Width - this.FoulCalcar * 2))
                        return;
                    _xSize = Convert.ToInt32(_xSize - Wma - LSWidth) - 2;//获取图表的宽度
                    k = 0;
                    p = CycCount;
                }
                else
                {
                    LSWidth = ClumpW + remW * 3 + XMaxWidth;//获取标签的宽度
                    _xSize = Convert.ToInt32(_xSize - Wma - LSWidth) - 2;//获取图表的宽度
                    CycCount = temText.Length;
                    k = 0;
                    p = CycCount;
                }

                LSLeft = _temXLeft + _xSize + Wma - 2;//获取标签左边线的位置
                if (this.ChartStyle == CharMode.Area)
                    LSLeft = _temXLeft + _xSize + Wma;

                //g.FillRectangle(Brushes.Gainsboro, LSLeft + x, _yUp + y, LSWidth, LSHeight);//绘制内矩形
                g.DrawRectangle(new Pen(Color.Black, 1), LSLeft + x, _yUp + y, LSWidth, LSHeight);//绘制矩形
                TempU = _yUp + remH;//小方块的Y坐标
                TempL = LSLeft + remW;//小方块的X坐标

                LabelSayPlace(g, YHeight, temText, XMaxWidth, XMaxHeight, LSfont, CycCount, TempL, TempU, k, p, true, x, y);//调用本方法，绘制标签文字
            }
            else
            {
                int t = 0;
                for (int i = k; i < p; i++)//循环绘制每列的标签文字
                {
                    if (this.ChartStyle == CharMode.Area)
                    {
                        if (i >= WearColor.Length)
                        {
                            _mybrush = new SolidBrush(WearColor[i - WearColor.Length]);
                        }
                        else
                        {
                            _mybrush = new SolidBrush(WearColor[i]);
                        }
                    }
                    else
                    {
                        if (_zText.Length > 1)//如果每列有多个数据，设置小方模块的颜色为不同的颜色
                        {
                            if (i >= WearColor.Length)
                            {
                                if (this.ChartStyle == CharMode.Line)
                                    _mypen = new Pen(WearColor[i - WearColor.Length], 1);
                                else
                                    _mybrush = new SolidBrush(WearColor[i - WearColor.Length]);
                            }
                            else
                            {
                                if (this.ChartStyle == CharMode.Line)
                                    _mypen = new Pen(WearColor[i], 1);
                                else
                                    _mybrush = new SolidBrush(WearColor[i]);
                            }
                        }
                        else
                        {
                            if (this.ChartStyle == CharMode.Line || this.ChartStyle == CharMode.Mark)
                            {
                                if (this.ChartStyle == CharMode.Line)
                                    _mypen = new Pen(this.ChartColor, 1);
                                else
                                    _mybrush = new SolidBrush(this.ChartColor);
                            }
                            else
                            {
                                if (this.ChartWearColor)//当用多颜色显示不同数据时，设置小方模块的颜色为不同的颜色
                                {
                                    if (i >= WearColor.Length)
                                    {
                                        _mybrush = new SolidBrush(WearColor[i - WearColor.Length]);
                                    }
                                    else
                                    {
                                        _mybrush = new SolidBrush(WearColor[i]);
                                    }
                                }
                                else//以默认颜色来显示数据条
                                {
                                    _mybrush = new SolidBrush(this.ChartColor);
                                }
                            }
                        }
                    }

                    //Graphics TitG = this.CreateGraphics();//创建Graphics类对象
                    if (this.ChartStyle == CharMode.Line)
                        g.DrawLine(_mypen, TempL + x, TempU + ClumpH / 2 + y, TempL + ClumpW + x, TempU + ClumpH / 2 + y);//绘制线形
                    else
                        g.FillRectangle(_mybrush, TempL + x, TempU + y, ClumpW, ClumpH);//绘制小方块
                    if (this.ChartStyle == CharMode.Area)
                    {
                        AreaF = (_szData[i] / _aSum) * 100;
                        AreaF = (float)Math.Round(AreaF, 2);
                        AreaS = temText[i] + "  " + AreaF.ToString() + "%";
                    }
                    else
                        AreaS = temText[i];

                    SizeF XMaxSize = g.MeasureString(AreaS, LSfont);//将绘制的字符串进行格式化
                    XMaxHeight = XMaxSize.Height;//获取字符串的高度
                    g.DrawString(AreaS, new Font("宋体", 8), new SolidBrush(Color.Black), new PointF(TempL + remW * 2 + x, TempU + (ClumpH - XMaxHeight) / 2 + y));
                    TempU = TempU + ClumpH + bosom;
                }
                if (p != temText.Length)//如果没有绘制到标签尾
                {
                    t = p;
                    if ((p + Cp) > temText.Length)//如果下一次循环到尾部
                        p = temText.Length;
                    else
                        p = p + Cp;
                    k = t;

                    TempU = _yUp + remH;//小方块的Y坐标
                    TempL = TempL + remW + XMaxWidth + ClumpW;//小方块的X坐标
                    LabelSayPlace(g, YHeight, temText, XMaxWidth, XMaxHeight, LSfont, Cp, TempL, TempU, k, p, true, x, y);
                }
            }
        }
        #endregion

        #region 绘制条形图(Bar)
        /// <summary>
        /// 绘制条形图
        /// </summary>
        /// <param g="Graphics">封装一个绘图的类对象</param>
        public void ProtractBar(Graphics g, int x, int y)
        {
            //显示柱状效果
            Font Zfont = new System.Drawing.Font("Arial", 8);
            SolidBrush Zbrush = new SolidBrush(Color.Black);
            //Graphics TitG = this.CreateGraphics();//创建Graphics类对象
            //SizeF YMaxSize = TitG.MeasureString("", this._XYFont);//将绘制的字符串进行格式化
            _xLeft = _temXLeft;
            float UnitSp = (float)(_xUnit * 0.2);//设置矩形与纵线的间隔
            float A1 = 0;//记录矩形的X点
            float A2 = 0;//记录矩形的Y点
            float A3 = 0;//记录矩形的宽度
            float A4 = 0;//记录矩形的高度
            int XLinage = 0;//记录数值在最大水平线的行数
            float XValue = 0;//记录数值高于最大水平线的值
            bool XParity = false;//是否有与X轴的标识相等的值
            double XDegree = 0.0;//当窗体的坐标点小于X轴的刻度时，记录每刻度的坐标点大小
            int rowcon = 1;//记录每列的个数
            bool bl = true;//记录数据是否为0
            //Side属性的设置
            if ((_rowL > 1 && this.RowWeave == CharRowWeaveStyle.Side) || _rowL == 1)////-----////
            {

                for (int k = 0; k < _xText.Length; k++)//循环X轴上的数据
                {
                    if (_zText.Length > 1)//当列数据的个数大于1时
                    {
                        UnitSp = (float)(_xUnit * 0.1);//设置矩形与纵线的间隔
                        rowcon = 0;
                        for (int i = 0; i < _zText.Length; i++)//获取列数据不为0的个数
                        {
                            if (_zData[k, i] != 0)
                                rowcon = rowcon + 1;
                        }
                    }
                    A1 = _xLeft + UnitSp;

                    for (int i = 0; i < _zText.Length; i++)//循环列中的数据
                    {
                        bl = true;
                        for (int j = 0; j < _xData.Length; j++)
                        {
                            if (_zText.Length > 1 && _zData[k, i] == 0)
                            {
                                bl = false;
                                break;
                            }
                            if (_zData[k, i] >= _xData[j])
                            {
                                if (_zData[k, i] == _xData[j])
                                {
                                    XParity = true;
                                    XLinage = _xData.Length - j - 1;
                                }
                                else
                                {
                                    XParity = false;
                                    XLinage = _xData.Length - j;
                                }
                                XValue = _xData[j];
                                break;
                            }
                        }

                        XValue = _zData[k, i] - XValue;

                        if (XParity)
                            A4 = _yUnit * XLinage;
                        else
                        {
                            if (_yUnit >= _yMax)
                                A4 = _yUnit * (XLinage - 1) + (_yUnit / _yMax * XValue);
                            else
                            {
                                XDegree = Convert.ToDouble(_yUnit) / Convert.ToDouble(_yMax);
                                XDegree = XDegree * XValue;
                                A4 = _yUnit * (XLinage - 1) + (float)XDegree;
                            }
                        }
                        A2 = _yDown - A4;
                        A3 = (_xUnit - UnitSp * 2) / (float)rowcon;
                        if (_zText.Length > 1)
                        {
                            if (i >= WearColor.Length)
                                _mybrush = new SolidBrush(WearColor[i - WearColor.Length]);
                            else
                                _mybrush = new SolidBrush(WearColor[i]);
                        }
                        else
                        {
                            if (this.ChartWearColor)
                            {
                                if (i >= WearColor.Length)
                                    _mybrush = new SolidBrush(WearColor[k - WearColor.Length]);
                                else
                                    _mybrush = new SolidBrush(WearColor[k]);
                            }
                            else
                            {
                                _mybrush = new SolidBrush(this.ChartColor);
                            }
                        }
                        if (bl == true)
                        {
                            if (_is3DBar)
                            {
                                RectangleF myRectT = new RectangleF(A1 + x, A2 + y, A3 / 2 + 0.5F, A4);
                                RectangleF myRectD = new RectangleF(A1 + x + A3 / 2 - 0.5F, A2 + y, A3 / 2 + 0.5F, A4);

                                LinearGradientBrush myBrushT = new LinearGradientBrush(myRectT, _chartColor, Color.White, LinearGradientMode.Horizontal);
                                LinearGradientBrush myBrushD = new LinearGradientBrush(myRectD, Color.White, _chartColor, LinearGradientMode.Horizontal);

                                /////画图例
                                g.FillRectangle(myBrushT, myRectT);
                                g.FillRectangle(myBrushD, myRectD);

                                g.DrawRectangle(Pens.Black, A1 + x, A2 + y, A3, A4);
                            }
                            else
                            {
                                g.FillRectangle(_mybrush, A1 + x, A2 + y, A3, A4);//显示柱状效果
                            }

                            A1 = A1 + A3;
                        }

                    }
                    _xLeft = _xLeft + _xUnit;
                }
                if (this.Chartmark)
                    ProtractBarFont(g, x, y);
            }
            //Stacked属性的设置
            float temSum = 0;
            if ((_rowL > 1 && this.RowWeave == CharRowWeaveStyle.Stavked))
            {
                for (int k = 0; k < _xText.Length; k++)//循环X轴上的数据
                {
                    A1 = _xLeft + UnitSp;
                    temSum = 0;
                    for (int i = 0; i < _zText.Length; i++)//循环列中的数据
                    {
                        bl = true;
                        for (int j = 0; j < _xData.Length; j++)
                        {
                            if (_zData[k, i] == 0)
                                bl = false;
                            if (_zData[k, i] >= _xData[j])
                            {
                                if (_zData[k, i] == _xData[j])
                                {
                                    XParity = true;
                                    XLinage = _xData.Length - j - 1;
                                }
                                else
                                {
                                    XParity = false;
                                    XLinage = _xData.Length - j;
                                }
                                XValue = _xData[j];
                                break;
                            }
                        }
                        XValue = _zData[k, i] - XValue;

                        if (XParity)
                            A4 = _yUnit * XLinage;
                        else
                        {
                            if (_yUnit >= _yMax)
                                A4 = _yUnit * (XLinage - 1) + (_yUnit / _yMax * XValue);
                            else
                            {
                                XDegree = Convert.ToDouble(_yUnit) / Convert.ToDouble(_yMax);
                                XDegree = XDegree * XValue;
                                A4 = _yUnit * (XLinage - 1) + (float)XDegree;
                            }
                        }
                        A2 = _yDown - A4;
                        A3 = _xUnit - UnitSp * 2;

                        if (i >= WearColor.Length)
                            _mybrush = new SolidBrush(WearColor[i - WearColor.Length]);
                        else
                            _mybrush = new SolidBrush(WearColor[i]);
                        if (i == 0)
                            temSum = A2;
                        else
                            temSum = temSum - A4;
                        if (bl == true)
                        {
                            if (_is3DBar)
                            {
                                RectangleF myRectT = new RectangleF(A1 + x, temSum + y, A3 / 2 + 0.5F, A4);
                                RectangleF myRectD = new RectangleF(A1 + x + A3 / 2 - 0.5F, temSum + y, A3 / 2 + 0.5F, A4);

                                LinearGradientBrush myBrushT = new LinearGradientBrush(myRectT, _chartColor, Color.White, LinearGradientMode.Horizontal);
                                LinearGradientBrush myBrushD = new LinearGradientBrush(myRectD, Color.White, _chartColor, LinearGradientMode.Horizontal);

                                /////画图例
                                g.FillRectangle(myBrushT, myRectT);
                                g.FillRectangle(myBrushD, myRectD);

                                g.DrawRectangle(Pens.Black, A1 + x, temSum + y, A3, A4);
                            }
                            else
                            {
                                g.FillRectangle(_mybrush, A1 + x, temSum + y, A3, A4);//显示柱状效果
                            }
                        }
                    }
                    temSum = 0;
                    _xLeft = _xLeft + _xUnit;
                }
                if (this.Chartmark)
                    ProtractBarFont(g, x, y);

            }

        }
        #endregion

        #region 绘制条形图标识文字(Bar Sign)
        /// <summary>
        /// 绘制条形图
        /// </summary>
        /// <param g="Graphics">封装一个绘图的类对象</param>
        private void ProtractBarFont(Graphics g, int x, int y)
        {
            //显示柱状效果
            Font Zfont = new System.Drawing.Font("Arial", 8);
            SolidBrush Zbrush = new SolidBrush(Color.Black);
            //Graphics TitG = this.CreateGraphics();//创建Graphics类对象
            SizeF YMaxSize = g.MeasureString("", this._XYFont);//将绘制的字符串进行格式化
            float YMaxWidth = 0;//获取字符串的宽度
            float YMaxHeight = 0;//获取字符串的高度
            _xLeft = _temXLeft;
            float UnitSp = (float)(_xUnit * 0.2);//设置矩形与纵线的间隔
            float A1 = 0;//记录矩形的X点
            float A2 = 0;//记录矩形的Y点
            float A3 = 0;//记录矩形的宽度
            float A4 = 0;//记录矩形的高度
            int XLinage = 0;//记录数值在最大水平线的行数
            float XValue = 0;//记录数值高于最大水平线的值
            bool XParity = false;//是否有与X轴的标识相等的值
            double XDegree = 0.0;//当窗体的坐标点小于X轴的刻度时，记录每刻度的坐标点大小
            int rowcon = 1;//记录每列的个数
            float Fsign = 0;//记录数据标识文字的X轴位置
            bool bl = true;//记录数据是否为0

            //Side样式的标识文字
            if ((_rowL > 1 && this.RowWeave == CharRowWeaveStyle.Side) || _rowL == 1)////////------/////
            {
                for (int k = 0; k < _xText.Length; k++)//循环X轴上的数据
                {
                    if (_zText.Length > 1)//当列数据的个数大于1时
                    {
                        UnitSp = (float)(_xUnit * 0.1);//设置矩形与纵线的间隔
                        rowcon = 0;
                        for (int i = 0; i < _zText.Length; i++)//获取列数据不为0的个数
                        {
                            if (_zData[k, i] != 0)
                                rowcon = rowcon + 1;
                        }
                    }
                    A1 = _xLeft + UnitSp;
                    for (int i = 0; i < _zText.Length; i++)//循环列中的数据
                    {
                        bl = true;
                        for (int j = 0; j < _xData.Length; j++)
                        {
                            if (_zText.Length > 1 && _zData[k, i] == 0)
                                bl = false;
                            if (_zData[k, i] >= _xData[j])
                            {
                                if (_zData[k, i] == _xData[j])
                                {
                                    XParity = true;
                                    XLinage = _xData.Length - j - 1;
                                }
                                else
                                {
                                    XParity = false;
                                    XLinage = _xData.Length - j;
                                }
                                XValue = _xData[j];
                                break;
                            }
                        }
                        XValue = _zData[k, i] - XValue;

                        if (XParity)
                            A4 = _yUnit * XLinage;
                        else
                        {
                            if (_yUnit >= _yMax)
                                A4 = _yUnit * (XLinage - 1) + (_yUnit / _yMax * XValue);
                            else
                            {
                                XDegree = Convert.ToDouble(_yUnit) / Convert.ToDouble(_yMax);
                                XDegree = XDegree * XValue;
                                A4 = _yUnit * (XLinage - 1) + (float)XDegree;
                            }
                        }
                        A2 = _yDown - A4;
                        A3 = (_xUnit - UnitSp * 2) / (float)rowcon;

                        YMaxSize = g.MeasureString(_zData[k, i].ToString(), Zfont);//将绘制的字符串进行格式化
                        YMaxWidth = YMaxSize.Width;//获取字符串的宽度
                        YMaxHeight = YMaxSize.Height;//获取字符串的高度
                        if (YMaxWidth < A3)
                            Fsign = (float)(A1 + (A3 - YMaxWidth) / 2);
                        else
                        {
                            if ((YMaxWidth - A3) != 0)
                                Fsign = (float)(A1 - (YMaxWidth - A3) / 2);
                            else
                                Fsign = (float)A1;
                        }
                        if (bl == true)
                        {
                            g.DrawString(_zData[k, i].ToString(), Zfont, Zbrush, new PointF(Fsign + x, A2 - 2 - YMaxHeight + y));
                            A1 = A1 + A3;
                        }
                    }
                    _xLeft = _xLeft + _xUnit;
                }
            }

            //Stacked样式的标识文字
            float temSum = 0;
            if ((_rowL > 1 && this.RowWeave == CharRowWeaveStyle.Stavked))// || this.RowList == 1)
            {
                for (int k = 0; k < _xText.Length; k++)//循环X轴上的数据
                {
                    A1 = _xLeft + UnitSp;
                    temSum = 0;
                    for (int i = 0; i < _zText.Length; i++)//循环列中的数据
                    {
                        bl = true;
                        for (int j = 0; j < _xData.Length; j++)
                        {
                            if (_zData[k, i] == 0)
                                bl = false;
                            if (_zData[k, i] >= _xData[j])
                            {
                                if (_zData[k, i] == _xData[j])
                                {
                                    XParity = true;
                                    XLinage = _xData.Length - j - 1;
                                }
                                else
                                {
                                    XParity = false;
                                    XLinage = _xData.Length - j;
                                }
                                XValue = _xData[j];
                                break;
                            }
                        }
                        XValue = _zData[k, i] - XValue;

                        if (XParity)
                            A4 = _yUnit * XLinage;
                        else
                        {
                            if (_yUnit >= _yMax)
                                A4 = _yUnit * (XLinage - 1) + (_yUnit / _yMax * XValue);
                            else
                            {
                                XDegree = Convert.ToDouble(_yUnit) / Convert.ToDouble(_yMax);
                                XDegree = XDegree * XValue;
                                A4 = _yUnit * (XLinage - 1) + (float)XDegree;
                            }
                        }
                        A2 = _yDown - A4;
                        A3 = _xUnit - UnitSp * 2;

                        if (i == 0)
                            temSum = A2;
                        else
                            temSum = temSum - A4;

                        YMaxSize = g.MeasureString(_zData[k, i].ToString(), Zfont);//将绘制的字符串进行格式化
                        YMaxWidth = YMaxSize.Width;//获取字符串的宽度
                        YMaxHeight = YMaxSize.Height;//获取字符串的高度
                        if (YMaxWidth < A3)
                            Fsign = (float)(A1 + (A3 - YMaxWidth) / 2);
                        else
                        {
                            if ((YMaxWidth - A3) != 0)
                                Fsign = (float)(A1 - (YMaxWidth - A3) / 2);
                            else
                                Fsign = (float)A1;
                        }
                        if (bl == true)
                            g.DrawString(_zData[k, i].ToString(), Zfont, Zbrush, new PointF(Fsign + x, temSum - 2 - YMaxHeight + y));

                    }
                    temSum = 0;
                    _xLeft = _xLeft + _xUnit;
                }
            }
        }
        #endregion

        #region 绘制线形或面形图(Line、Mark)
        /// <summary>
        /// 绘制线形或面形图(Line、Mark)
        /// </summary>
        /// <param g="Graphics">封装一个绘图的类对象</param>
        private void ProtractLM(Graphics g, int x, int y)
        {
            //显示线形效果
            Font Zfont = new System.Drawing.Font("Arial", 8);
            SolidBrush Zbrush = new SolidBrush(Color.Black);
            //Graphics TitG = this.CreateGraphics();//创建Graphics类对象
            SizeF YMaxSize = g.MeasureString("", this._XYFont);//将绘制的字符串进行格式化
            _xLeft = _temXLeft;
            float UnitSp = (float)(_xUnit * 0.2);//设置矩形与纵线的间隔
            float A1 = 0;//记录矩形的X点
            float A3 = 0;
            float A4 = 0;//记录矩形的高度
            //记录面形图表的各点坐标
            PointF PF1 = new PointF(0, 0);
            PointF PF2 = new PointF(0, 0);
            PointF PF3 = new PointF(0, 0);
            PointF PF4 = new PointF(0, 0);

            int XLinage = 0;//记录数值在最大水平线的行数
            float XValue = 0;//记录数值高于最大水平线的值
            bool XParity = false;//是否有与X轴的标识相等的值
            double XDegree = 0.0;//当窗体的坐标点小于X轴的刻度时，记录每刻度的坐标点大小
            float Linker = 0;//记录连接点的数值
            PointF[] curvePoints;//记录面形图表的坐标数组
            Pen Lmypen = new Pen(Color.Black, 1);
            float[] Xsum = new float[_xText.Length];

            for (int i = 0; i < Xsum.Length; i++)
                Xsum[i] = 0;

            for (int k = 0; k < _zText.Length; k++)//遍历X轴上每列的个数
            {
                A1 = _xLeft;

                for (int i = 0; i < _xText.Length; i++)//遍历X轴上的列数
                {
                    for (int j = 0; j < _xData.Length; j++)//遍历Y轴上的刻度值
                    {
                        if (_zData[i, k] >= _xData[j])//如果当前值大于等于X轴上的刻度值
                        {
                            if (_zData[i, k] == _xData[j])//如果当前值等于X轴上的刻度值
                            {
                                XParity = true;//进行标识
                                XLinage = _xData.Length - j - 1;//因为X轴上的刻度值是从大到小排序的，所以取反
                            }
                            else//如果当前值小于X轴上的刻度值
                            {
                                XParity = false;//进行标识
                                XLinage = _xData.Length - j;//获取当前值大于相应的X轴刻度值
                            }
                            XValue = _xData[j];//获取当前值与X轴刻度相比的最小刻度值
                            break;
                        }
                    }
                    XValue = _zData[i, k] - XValue;//获取当前值与X轴刻度的差

                    if (XParity)//如果当前值等于X轴的刻度
                        A4 = _yUnit * XLinage;//获取当前值的高度
                    else
                    {
                        if (_yUnit >= _yMax)//如果X轴每单元的刻度值大于每单元的实际刻度
                            A4 = _yUnit * (XLinage - 1) + (_yUnit / _yMax * XValue);
                        else
                        {
                            XDegree = Convert.ToDouble(_yUnit) / Convert.ToDouble(_yMax);
                            XDegree = XDegree * XValue;
                            A4 = _yUnit * (XLinage - 1) + (float)XDegree;
                        }
                    }

                    if (_pageL == 1)//如果条形图或面形图为一个值时
                    {
                        if (_zText.Length > 1)
                        {
                            if (k >= WearColor.Length)
                                _mybrush = new SolidBrush(WearColor[k - WearColor.Length]);
                            else
                                _mybrush = new SolidBrush(WearColor[k]);
                        }
                        else
                        {
                            _mybrush = new SolidBrush(this.ChartColor);
                        }

                        g.FillEllipse(_mybrush, A1 - 1 + x, _yDown - A4 - 1 + y, 3, 3);
                    }
                    else
                    {
                        if (i != 0)//当前值不在X轴的0刻度上
                        {
                            if (_zText.Length > 1)//如果每列中的个数大1
                            {
                                if (this.ChartStyle == CharMode.Line)//如果是线形图
                                {
                                    if (k >= WearColor.Length)//如果列中的个数大于自定义的颜色数组个数
                                        Lmypen = new Pen(WearColor[k - WearColor.Length], 1);//设置线的颜色及粗细
                                    else
                                        Lmypen = new Pen(WearColor[k], 1);
                                }
                                if (this.ChartStyle == CharMode.Mark)//如果是面形图
                                {
                                    if (k >= WearColor.Length)
                                        _mybrush = new SolidBrush(WearColor[k - WearColor.Length]);
                                    else
                                        _mybrush = new SolidBrush(WearColor[k]);
                                }
                            }
                            else
                            {
                                if (this.ChartStyle == CharMode.Line)//如果是线形图
                                {
                                    Lmypen = new Pen(this.ChartColor, 1);
                                }
                                if (this.ChartStyle == CharMode.Mark)//如果是面形图
                                {
                                    _mybrush = new SolidBrush(this.ChartColor);
                                }
                            }

                            if (this.ChartStyle == CharMode.Line)//如果是线形图
                            {
                                g.DrawLine(Lmypen, A1 + x, Linker + y, A1 + _xUnit + x, _yDown - A4 + y);//绘制线条
                            }

                            if (this.ChartStyle == CharMode.Mark)//如果是面形图
                            {

                                //设置绘制面形图的4个点
                                if (i == 1)//如要绘制的是第一个平面，使平面不覆盖Y轴
                                {
                                    if (this.RowWeave == CharRowWeaveStyle.Side)
                                    {
                                        PF1 = new PointF(A1 + 1 + x, _yDown + y);
                                        PF2 = new PointF(A1 + 1 + x, Linker + y);
                                    }
                                    if (this.RowWeave == CharRowWeaveStyle.Stavked)
                                    {
                                        if (k == 0)
                                        {
                                            PF1 = new PointF(A1 + 1 + x, _yDown + y);
                                            PF2 = new PointF(A1 + 1 + x, Linker + y);
                                        }
                                        else
                                        {
                                            PF1 = new PointF(A1 + 1 + x, _yDown - Xsum[i - 1] + A3 + y);
                                            PF2 = new PointF(A1 + 1 + x, _yDown - Xsum[i - 1] + y);
                                        }
                                    }
                                }
                                else
                                {
                                    if (this.RowWeave == CharRowWeaveStyle.Side)
                                    {
                                        PF1 = new PointF(A1 + x, _yDown + y);
                                        PF2 = new PointF(A1 + x, Linker + y);
                                    }
                                    if (this.RowWeave == CharRowWeaveStyle.Stavked)
                                    {
                                        if (k == 0)
                                        {
                                            PF1 = new PointF(A1 + x, _yDown + y);
                                            PF2 = new PointF(A1 + x, Linker + y);
                                        }
                                        else
                                        {
                                            PF1 = new PointF(A1 + x, _yDown - Xsum[i - 1] + A3 + y);
                                            PF2 = new PointF(A1 + x, _yDown - Xsum[i - 1] + y);
                                        }
                                    }
                                }
                                if (this.RowWeave == CharRowWeaveStyle.Side)
                                {
                                    PF3 = new PointF(A1 + _xUnit + x, _yDown - A4 + y);
                                    PF4 = new PointF(A1 + _xUnit + x, _yDown + y);
                                }
                                if (this.RowWeave == CharRowWeaveStyle.Stavked)
                                {
                                    if (k == 0)
                                    {
                                        PF3 = new PointF(A1 + _xUnit + x, _yDown - A4 + y);
                                        PF4 = new PointF(A1 + _xUnit + x, _yDown + y);
                                    }
                                    else
                                    {
                                        PF3 = new PointF(A1 + _xUnit + x, _yDown - A4 - Xsum[i] + y);
                                        PF4 = new PointF(A1 + _xUnit + x, _yDown - Xsum[i] + y);
                                    }
                                }
                                curvePoints = new PointF[] { PF1, PF2, PF3, PF4 };//定义成点数组
                                g.FillPolygon(_mybrush, curvePoints);//绘制面形图
                                g.DrawPolygon(new Pen(Color.Black, (float)(0.2)), curvePoints);
                                if (this.RowWeave == CharRowWeaveStyle.Stavked)
                                    Xsum[i] = A4 + Xsum[i];
                            }
                            Linker = _yDown - A4;
                            A3 = A4;
                            A1 = A1 + _xUnit;
                        }
                        else
                        {
                            if (this.ChartStyle == CharMode.Mark)//如果是面形图
                            {

                                A3 = A4;
                                if (this.RowWeave == CharRowWeaveStyle.Stavked)
                                {
                                    if (k != 0)
                                        Xsum[i] = A3 + Xsum[i];
                                    else
                                        Xsum[i] = A4 + Xsum[i];
                                }

                            }

                            Linker = _yDown - A4;

                        }
                    }
                }


            }
            ProtractLMSign(g, x, y);
        }


        #endregion

        #region 绘制线形或面形图(Line、Mark)的标识文字
        /// <summary>
        /// 绘制线形或面形图(Line、Mark)
        /// </summary>
        /// <param g="Graphics">封装一个绘图的类对象</param>
        private void ProtractLMSign(Graphics g, int x, int y)
        {

            //显示线形效果
            Font Zfont = new System.Drawing.Font("Arial", 8);
            SolidBrush Zbrush = new SolidBrush(Color.Black);
            //Graphics TitG = this.CreateGraphics();//创建Graphics类对象
            SizeF YMaxSize = g.MeasureString("", this._XYFont);//将绘制的字符串进行格式化
            float YMaxWidth = 0;//获取字符串的宽度
            float YMaxHeight = 0;//获取字符串的高度

            _xLeft = _temXLeft;
            float UnitSp = (float)(_xUnit * 0.2);//设置矩形与纵线的间隔
            float A1 = 0;//记录矩形的X点
            float A4 = 0;//记录矩形的高度

            int XLinage = 0;//记录数值在最大水平线的行数
            float XValue = 0;//记录数值高于最大水平线的值
            bool XParity = false;//是否有与X轴的标识相等的值
            double XDegree = 0.0;//当窗体的坐标点小于X轴的刻度时，记录每刻度的坐标点大小

            float[] Xsum = new float[_xText.Length];

            for (int i = 0; i < Xsum.Length; i++)
                Xsum[i] = 0;

            if (this.Chartmark)
            {

                for (int k = 0; k < _zText.Length; k++)//遍历X轴上每列的个数
                {
                    A1 = _xLeft;
                    for (int i = 0; i < _xText.Length; i++)//遍历X轴上的列数
                    {
                        for (int j = 0; j < _xData.Length; j++)//遍历Y轴上的刻度值
                        {
                            if (_zData[i, k] >= _xData[j])//如果当前值大于等于X轴上的刻度值
                            {
                                if (_zData[i, k] == _xData[j])//如果当前值等于X轴上的刻度值
                                {
                                    XParity = true;//进行标识
                                    XLinage = _xData.Length - j - 1;//因为X轴上的刻度值是从大到小排序的，所以取反
                                }
                                else//如果当前值小于X轴上的刻度值
                                {
                                    XParity = false;//进行标识
                                    XLinage = _xData.Length - j;//获取当前值大于相应的X轴刻度值
                                }
                                XValue = _xData[j];//获取当前值与X轴刻度相比的最小刻度值
                                break;
                            }
                        }
                        XValue = _zData[i, k] - XValue;//获取当前值与X轴刻度的差

                        if (XParity)//如果当前值等于X轴的刻度
                            A4 = _yUnit * XLinage;//获取当前值的高度
                        else
                        {
                            if (_yUnit >= _yMax)//如果X轴每单元的刻度值大于每单元的实际刻度
                                A4 = _yUnit * (XLinage - 1) + (_yUnit / _yMax * XValue);
                            else
                            {
                                XDegree = Convert.ToDouble(_yUnit) / Convert.ToDouble(_yMax);
                                XDegree = XDegree * XValue;
                                A4 = _yUnit * (XLinage - 1) + (float)XDegree;
                            }
                        }

                        YMaxSize = g.MeasureString(_zData[i, k].ToString(), Zfont);//将绘制的字符串进行格式化
                        YMaxWidth = YMaxSize.Width;//获取字符串的宽度
                        YMaxHeight = YMaxSize.Height;//获取字符串的高度

                        if (this.ChartStyle == CharMode.Mark)
                        {
                            if (this.RowWeave == CharRowWeaveStyle.Stavked)
                            {
                                Xsum[i] = Xsum[i] + A4;
                                g.DrawString(_zData[i, k].ToString(), Zfont, Zbrush, new PointF(A1 - YMaxWidth / 2 + x, (_yDown - Xsum[i]) - 2 - YMaxHeight + y));
                            }
                            if (this.RowWeave == CharRowWeaveStyle.Side)
                            {
                                g.DrawString(_zData[i, k].ToString(), Zfont, Zbrush, new PointF(A1 - YMaxWidth / 2 + x, (_yDown - A4) - 2 - YMaxHeight + y));
                            }
                        }
                        if (this.ChartStyle == CharMode.Line)
                        {
                            g.DrawString(_zData[i, k].ToString(), Zfont, Zbrush, new PointF(A1 - YMaxWidth / 2 + x, (_yDown - A4) - 2 - YMaxHeight + y));
                        }

                        A1 = A1 + _xUnit;
                    }
                }
            }
        }
        #endregion

        #region 绘制饼形图(Area)


        public string[] AreaText;//临时存储数据的名称数组
        public float AreaXMaxWidth = 0;//获取字符串的宽度
        public float AreaXMaxHeight = 0;//获取字符串的高度
        public float Aline = 20;//标识文字的前端线长
        public float Asash = 3;//标识文本名边框的宽度
        //获取饼形图的标识文字
        public void AreaValue(Graphics g)
        {
            string temTextSize = "";//存储最长的名称
            Font LSfont = new System.Drawing.Font("宋体", 8);
            AreaText = new string[_xText.Length];
            for (int i = 0; i < _xText.Length; i++)
            {
                AreaText[i] = _xText[i];
            }
            float AresF = 0;

            for (int i = 0; i < AreaText.Length; i++)
            {
                AresF = (_szData[i] / _aSum) * 100;
                AresF = (float)Math.Round(AresF, 3);

                AreaText[i] = AreaText[i] + " " + AresF.ToString() + "%";
                if (AreaText[i].Length > temTextSize.Length)
                {
                    temTextSize = AreaText[i];
                }
            }
            //Graphics TitG = this.CreateGraphics();//创建Graphics类对象
            SizeF XMaxSize = g.MeasureString(temTextSize + Asash * 2, LSfont);//将绘制的字符串进行格式化
            AreaXMaxWidth = XMaxSize.Width;//获取字符串的宽度
            AreaXMaxHeight = XMaxSize.Height;//获取字符串的高度
        }

        //绘制饼形图表
        public void ProtractArea(Graphics g, int x, int y)
        {
            AreaValue(g);
            _mypen = new Pen(Color.Black, 1);
            float f = 0;
            float TimeNum = 0;
            float AXLeft = 0;
            float AYUp = 0;
            float AXSize = 0;
            float AYSize = 0;
            float Atop = 0;
            float Aleft = 0;
            TimeNum = this.AreaAngle;//-------
            _xLeft = _marker.Size.Width - (_marker.Size.Width - this.FoulCalcar);
            _xSize = _marker.Size.Width - this.FoulCalcar * 2;
            AYUp = _yUp;
            AYSize = _ySize;
            _temXLeft = AXLeft;
            if (this.LabelSay)//是否显示标签
            {
                ProtractLabelSay(g, _marker.Size.Height - this.FoulCalcar * 3 - _titHeight, x, y);
            }
            AXLeft = _xLeft;
            AXSize = _xSize;
            AYUp = _yUp;
            AYSize = _ySize;

            if (this.Chartmark == true)
            {
                AXLeft = AXLeft + AreaXMaxWidth + Aline;
                AYUp = AYUp + AreaXMaxHeight;
                AXSize = _xSize - AreaXMaxWidth * 2 - Aline * 2;
                AYSize = _ySize - AreaXMaxHeight * 2;

                if (AXSize >= AYSize)
                {
                    Aleft = AXSize - AYSize;
                    AXSize = AYSize;
                }
                else
                {
                    Atop = AYSize - AXSize;
                    AYSize = AXSize;
                }
                if (Aleft != 0)
                {
                    AXLeft = AXLeft + Aleft / 2;
                }
                if (Atop != 0)
                {
                    AYUp = AYUp + Atop / 2;
                }
            }
            _temXLeft = _xLeft;

            if (AXSize > 0 && AYSize > 0)
            {
                for (int i = 0; i < _szData.Length; i++)
                {
                    f = _szData[i] / _aSum;
                    if (i >= WearColor.Length)
                        _mybrush = new SolidBrush(WearColor[i - WearColor.Length]);
                    else
                        _mybrush = new SolidBrush(WearColor[i]);
                    g.FillPie(_mybrush, AXLeft + x, AYUp + y, AXSize, AYSize, TimeNum, f * 360);
                    TimeNum += f * 360;
                }

                if (this.Chartmark == true)
                {
                    ProAreaSign(g, x, y);
                }
            }
            else
                return;
        }
        #endregion

        #region 绘制饼形图标识(Area)
        public void ProAreaSign(Graphics g, int x, int y)
        {
            AreaValue(g);
            _mypen = new Pen(Color.Black, 1);
            Font LSfont = new System.Drawing.Font("宋体", 8);
            SolidBrush Zbrush = new SolidBrush(Color.Black);
            SolidBrush ATbrush = new SolidBrush(Color.Khaki);
            float f = 0;
            float TimeNum = 0;
            float AXLeft = 0;
            float AYUp = 0;
            float AXSize = 0;
            float AYSize = 0;
            float Atop = 0;
            float Aleft = 0;
            //Graphics TitG = this.CreateGraphics();//创建Graphics类对象
            SizeF XMaxSize = g.MeasureString("", LSfont);//将绘制的字符串进行格式化
            float SWidth = 0;//获取字符串的宽度
            float SHeight = 0;//获取字符串的高度

            _xLeft = _marker.Size.Width - (_marker.Size.Width - this.FoulCalcar);
            _xSize = _marker.Size.Width - this.FoulCalcar * 2;
            AYUp = _yUp;
            AYSize = _ySize;
            _temXLeft = AXLeft;
            if (this.LabelSay)//是否显示标签
            {
                ProtractLabelSay(g, _marker.Size.Height - this.FoulCalcar * 3 - _titHeight, x, y);
            }
            AXLeft = _xLeft;
            AXSize = _xSize;
            AYUp = _yUp;
            AYSize = _ySize;

            if (this.Chartmark == true)
            {
                AXLeft = AXLeft + AreaXMaxWidth + Aline;
                AYUp = AYUp + AreaXMaxHeight;
                AXSize = _xSize - AreaXMaxWidth * 2 - Aline * 2;
                AYSize = _ySize - AreaXMaxHeight * 2;

                if (AXSize >= AYSize)
                {
                    Aleft = AXSize - AYSize;
                    AXSize = AYSize;
                }
                else
                {
                    Atop = AYSize - AXSize;
                    AYSize = AXSize;
                }
                if (Aleft != 0)
                {
                    AXLeft = AXLeft + Aleft / 2;
                }
                if (Atop != 0)
                {
                    AYUp = AYUp + Atop / 2;
                }
            }
            _temXLeft = _xLeft;
            float X1 = 0;
            float Y1 = 0;
            float X2 = 0;
            float Y2 = 0;
            float TX1 = 0;
            float TY1 = 0;
            float TX2 = 0;
            float TY2 = 0;
            float temf = 0;
            double radians = 0;
            temf = (this.AreaAngle * (_aSum / 360) / _aSum);
            TimeNum = this.AreaAngle;
            if (AXSize > 0 && AYSize > 0)
            {
                for (int i = 0; i < _szData.Length; i++)
                {
                    f = _szData[i] / _aSum;
                    if (i >= WearColor.Length)
                        _mybrush = new SolidBrush(WearColor[i - WearColor.Length]);
                    else
                        _mybrush = new SolidBrush(WearColor[i]);
                    if (f == 0)
                        continue;
                    radians = ((double)((temf + f / 2) * 360) * Math.PI) / (double)180;
                    X1 = Convert.ToSingle(AXLeft + (AXSize / 2.0 + (int)((float)(AXSize / 2.0) * Math.Cos(radians))));
                    Y1 = Convert.ToSingle(AYUp + (AYSize / 2.0 + (int)((float)(AYSize / 2.0) * Math.Sin(radians))));

                    XMaxSize = g.MeasureString(AreaText[i].Trim(), LSfont);//将绘制的字符串进行格式化
                    SWidth = XMaxSize.Width;//获取字符串的宽度
                    SHeight = XMaxSize.Height;//获取字符串的高度
                    if ((temf + f / 2) * 360 > 90 && (temf + f / 2) * 360 <= 270)
                    {
                        X2 = X1 - Aline;

                        TX1 = X2 - 1 - SWidth;
                        TY1 = Y1 - SHeight / 2 - Asash;
                        TX2 = SWidth;
                        TY2 = SHeight + Asash * 2;
                        //g.FillRectangle(ATbrush, TX1 + x, TY1 + y, TX2, TY2);//绘制内矩形
                        //g.DrawRectangle(new Pen(Color.Black, 1), TX1 + x, TY1 + y, TX2, TY2);//绘制矩形
                        g.DrawString(AreaText[i].Trim(), LSfont, Zbrush, new PointF(X2 - SWidth + Asash - 1 + x, Y1 - SHeight / 2 + y));
                    }
                    else
                    {
                        X2 = X1 + Aline;

                        TX1 = X2 + 1;
                        TY1 = Y1 - SHeight / 2 - Asash;
                        TX2 = SWidth;
                        TY2 = SHeight + Asash * 2;
                        //g.FillRectangle(ATbrush, TX1 + x, TY1 + y, TX2, TY2);//绘制内矩形
                        //g.DrawRectangle(new Pen(Color.Black, 1), TX1 + x, TY1 + y, TX2, TY2);//绘制矩形
                        g.DrawString(AreaText[i].Trim(), LSfont, Zbrush, new PointF(X2 + Asash + 1 + x, Y1 - SHeight / 2 + y));
                    }
                    Y2 = Y1;

                    g.DrawLine(new Pen(new SolidBrush(Color.Black), 1), X1 + x, Y1 + y, X2 + x, Y2 + y);
                    TimeNum += f * 360;
                    temf = temf + f;
                }
            }
            else
                return;
        }
        #endregion

        #region 绘制图表
        /// <summary>
        /// 绘制图表
        /// </summary>
        /// <param Variable="int">判断图表的绘样式</param>
        public void DrawText(Graphics g, int x, int y)
        {
            int height = _marker.Size.Height, width = _marker.Size.Width;
            try
            {
                _bXYColor = new SolidBrush(this.XYColor);
                int TitPlace = ProtractTitle(g, x, y);//绘制标题

                ProtractXY(g, TitPlace, x, y);
                switch (this.ChartStyle)
                {
                    case CharMode.Bar:
                        {
                            ProtractBar(g, x, y);//绘制条形图
                            break;
                        }
                    case CharMode.Line:
                    case CharMode.Mark:
                        {
                            ProtractLM(g, x, y);//绘制线形图和面形图
                            break;
                        }
                    case CharMode.Area:
                        {
                            ProtractArea(g, x, y);//绘制饼形图
                            break;
                        }
                    case CharMode.none: break;

                }
            }
            catch (Exception ey)
            {
                MessageBox.Show(ey.Message);
            }
        }
        #endregion

    }

    /// <summary>
    /// 图表类型
    /// </summary>
    public enum CharMode
    {
        Bar = 0,//条形
        Mark = 1,//面形
        Line = 2,//线形
        Area = 3,//饼形
        none = 4,//空
    }

    public enum ChartTitleStyle
    {
        TopLeft = 0,//置顶居左
        TopCenter = 1,//置顶居中
        TopRight = 2,//置顶居右
        BottomLeft = 3,//置底居左
        BottomCenter = 4,//置底居中
        BottomRight = 5,//置底居右
    }
}

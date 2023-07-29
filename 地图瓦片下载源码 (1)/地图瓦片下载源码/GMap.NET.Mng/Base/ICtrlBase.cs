using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GMap.NET.WindowsForms;

namespace GMap.NET.Mng
{
    public interface ICtrlBase
    {
        string CtrlID { get; }
        string MainID { get; set; }
        ECPType CPType { get; set; }
        bool IsFocus { get; set; }
        bool IsEnter { get; set; }
        bool IsFilled { get; set; }
        bool IsClick { get; set; }
        bool IsDoubleClick { get; set; }
        bool IsEdit { get; set; }
        bool IsMove { get; set; }
        bool IsShowText { get; set; }
        string ShowText { get; set; }
        Color CtrlColor { get; set; }
        void UpdateUI(ICtrlBase childCtrl);
        void UpdateUI();
        void DeleteCtrl();
    }
}

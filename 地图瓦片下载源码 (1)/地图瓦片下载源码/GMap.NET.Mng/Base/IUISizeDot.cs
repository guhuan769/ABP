using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMap.NET.Mng
{
    public interface IUISizeDot : ICtrlBase
    {
        DotType Type { get; set; }
        System.Windows.Forms.Cursor Cursor { get; set; }
    }
}

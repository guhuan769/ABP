using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GMap.NET.Mng
{
    public interface ICtrlDraw : ICtrlBase
    {
        void SetSize(int width, int height);

        Pen Pen { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace GMap.NET.Mng
{
    public class OverlayMng
    {
        public OverlayMng(string mid)
        {
            _overlay = new GMapOverlay(mid);
            _ctrlList = new Dictionary<string, ICtrlBase>();
        }

        private GMapOverlay _overlay;

        public GMapOverlay Overlay
        {
            get { return _overlay; }
        }

        private Dictionary<string, ICtrlBase> _ctrlList;

        public string Mid
        {
            get { return _overlay.Id; }
        }

        public ICtrlBase GetCtrl(string mid)
        {
            if (_ctrlList.ContainsKey(mid))
            {
                return _ctrlList[mid];
            }
            else
            {
                return null;
            }
        }

        public void AddCtrl(ICtrlBase ctrl)
        {
            if (ctrl is GMapMarker)
            {
                _overlay.Markers.Add((GMapMarker)ctrl);
                _ctrlList.Add(ctrl.CtrlID, ctrl);
            }
            else if (ctrl is GMapPolygon)
            {
                _overlay.Polygons.Add((GMapPolygon)ctrl);
                _ctrlList.Add(ctrl.CtrlID, ctrl);
            }
            else if (ctrl is GMapRoute)
            {
                _overlay.Routes.Add((GMapRoute)ctrl);
                _ctrlList.Add(ctrl.CtrlID, ctrl);
            }

        }

        public void DeleteAllCtrl()
        {
            foreach (ICtrlBase ctrl in _ctrlList.Values)
            {
                ctrl.DeleteCtrl();
            }

            _overlay.Markers.Clear();
            _overlay.Polygons.Clear();
            _overlay.Routes.Clear();
            _ctrlList.Clear();
        }

        public void DeleteCtrl(string ctrlID)
        {
            if (_ctrlList.ContainsKey(ctrlID))
            {
                _ctrlList[ctrlID].DeleteCtrl();

                if (_ctrlList[ctrlID] is GMapMarker)
                {
                    _overlay.Markers.Remove((GMapMarker)_ctrlList[ctrlID]);
                }
                else if (_ctrlList[ctrlID] is MapPolygon)
                {
                    _overlay.Polygons.Remove((MapPolygon)_ctrlList[ctrlID]);
                }
                else if (_ctrlList[ctrlID] is GMapRoute)
                {
                    _overlay.Routes.Remove((GMapRoute)_ctrlList[ctrlID]);
                }

                _ctrlList.Remove(ctrlID);
            }
        }

        public void OnMapPositionChanged()
        {
            foreach (string id in _ctrlList.Keys)
            {
                _ctrlList[id].UpdateUI();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace GMap.NET.Mng
{
    public class OverlayMngCollection
    {
        public OverlayMngCollection(GMapMng mng)
        {
            _mng = mng;
        }

        private GMapMng _mng;
        private Dictionary<string, OverlayMng> _list = new Dictionary<string, OverlayMng>();

        private string _currentOverlayID = string.Empty;
        public string CurrentOverlayID
        {
            get { return _currentOverlayID; }
        }

        public void AddOverlayMng(string mid)
        {
            _currentOverlayID = mid;
            _list.Add(mid, new OverlayMng(mid));
            _mng.GMapControl.Overlays.Add(_list[mid].Overlay);
        }

        public void DeleteAllCtrl()
        {
            foreach (string key in _list.Keys)
            {
                _list[key].DeleteAllCtrl();
            }
        }

        public void SetCurrentOverlayID(string id)
        {
            if (!string.IsNullOrEmpty(id) && _list.ContainsKey(id))
            {
                _currentOverlayID = id;
            }
        }

        public OverlayMng this[string mid]
        {
            get
            {
                if (string.IsNullOrEmpty(mid))
                {
                    return null;
                }
                else
                {
                    return _list[mid];
                }
            }
        }

        public OverlayMng CurrentOverlayMng
        {
            get
            {
                return _list[_currentOverlayID];
            }
        }

        public List<OverlayMng> OverlayMngList
        {
            get
            {
                return _list.Values.ToList<OverlayMng>();
            }
        }

        public void OnMapPositionChanged()
        {
            foreach (string id in _list.Keys)
            {
                _list[id].OnMapPositionChanged();
            }
        }
    }
}

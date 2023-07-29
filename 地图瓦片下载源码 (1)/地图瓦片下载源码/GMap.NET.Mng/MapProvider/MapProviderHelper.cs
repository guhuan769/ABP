using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GMap.NET.MapProviders;

namespace GMap.NET.Mng
{
    public static class MapProviderHelper
    {
        public static GMapProvider GetProvider(EMapType mapType, string url)
        {
            switch (mapType)
            {
                case EMapType.高德:
                    return new GaodeMapProvider(url);
                case EMapType.百度:
                    return BaiduMapProvider.Instance;
                default:
                    return null;
            }
        }
    }
}

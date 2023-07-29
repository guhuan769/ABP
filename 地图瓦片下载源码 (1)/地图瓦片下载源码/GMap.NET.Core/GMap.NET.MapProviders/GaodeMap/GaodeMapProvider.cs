using System;
using GMap.NET.Projections;

namespace GMap.NET.MapProviders
{
    public class GaodeMapProvider : GaodeMapProviderBase
    {
        public static readonly GaodeMapProvider Instance;

        private readonly Guid id = new Guid("608748FC-5FDD-4d3a-9027-356F24A755E5");
        public override Guid Id
        {
            get
            {
                return id;
            }
        }

        private readonly string name = "GaoDe";
        public override string Name
        {
            get
            {
                return name;
            }
        }

        static GaodeMapProvider()
        {
            Instance = new GaodeMapProvider();
        }

        public override PureImage GetTileImage(GPoint pos, int zoom)
        {
            try
            {
                string url = MakeTileImageUrl(pos, zoom, LanguageStr);

                return GetTileImageUsingHttp(url);
            }
            catch
            {
                return null;
            }
        }

        private string MakeTileImageUrl(GPoint pos, int zoom, string language)
        {
            //var num = (pos.X + pos.Y) % 4 + 1;
            //string url = string.Format(UrlFormat, num, pos.X, pos.Y, zoom);
            string url = string.Format(UrlFormat, pos.X, pos.Y, zoom);
            return url;
        }

        //private static readonly string UrlFormat = "http://webrd01.is.autonavi.com/appmaptile?lang=zh_cn&size=1&scale=1&style=7&x={0}&y={1}&z={2}";
        //private static readonly string UrlFormat = "http://localhost/ditu/{2}/{0}_{1}.png";
    }

    public abstract class GaodeMapProviderBase : GMapProvider
    {
        //private string ClientKey = "1308e84a0e8a1fc2115263a4b3cf87f1";

        public GaodeMapProviderBase()
        {
            MaxZoom = null;
            RefererUrl = "http://www.amap.com/";
            //Copyright = string.Format("©{0} Corporation, ©{0} NAVTEQ, ©{0}", DateTime.Today.Year);
        }

        public override PureProjection Projection
        {
            get
            {
                return MercatorProjection.Instance;
            }
        }

        private GMapProvider[] overlays;
        public override GMapProvider[] Overlays
        {
            get
            {
                if (overlays == null)
                {
                    overlays = new GMapProvider[] { this };
                }
                return overlays;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace GMap.NET.Mng
{
    public class GaodeMapProvider : MapProviderBase
    {
        public GaodeMapProvider(string url)
        {
            RefererUrl = "http://www.amap.com/";
            UrlFormat = url;
        }

        //public static readonly GaodeMapProvider Instance;

        private Guid id = new Guid("608748FC-5FDD-4d3a-9027-356F24A755E5");
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

        //static GaodeMapProvider()
        //{
        //    Instance = new GaodeMapProvider();
        //}

        public override void SetDbId(Guid providerID)
        {
            id = providerID;
            using (var HashProvider = new SHA1CryptoServiceProvider())
            {
                DbId = Math.Abs(BitConverter.ToInt32(HashProvider.ComputeHash(providerID.ToByteArray()), 0));
            }
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
    }
}

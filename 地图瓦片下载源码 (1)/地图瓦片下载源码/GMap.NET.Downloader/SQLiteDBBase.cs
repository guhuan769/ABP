using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GMap.NET.Downloader
{
    public class SQLiteDBBase
    {
        private SQLiteAccess _db = null;

        public SQLiteAccess Db
        {
            get { return _db; }
        }

        public SQLiteDBBase(string connectionString) : this(new SQLiteAccess(connectionString)) { }

        public SQLiteDBBase(SQLiteAccess db)
        {
            _db = db;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace GMap.NET.Downloader
{
    public sealed class SQLiteAccess
    {
        private SQLiteConnection _conn = new SQLiteConnection();
        private SQLiteCommand _comm = new SQLiteCommand();
        private SQLiteTransaction _trans = null;
        private string _connectionString = string.Empty;

        public SQLiteConnection Conn
        {
            get { return _conn; }
            set { _conn = value; }
        }

        public SQLiteCommand Comm
        {
            get { return _comm; }
            set { _comm = value; }
        }

        public SQLiteTransaction Trans
        {
            get { return _trans; }
            set { _trans = value; }
        }

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        static SQLiteAccess()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(GMap.NET.CacheProviders.SQLitePureImageCache.CurrentDomain_AssemblyResolve);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SQLiteAccess(string connectionString)
        {
            _connectionString = connectionString;
            _comm.Connection = _conn;
            _comm.CommandTimeout = 300;
            Parameters = new DBParameterCollection(_comm);
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void ConnClose()
        {
            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }
        }

        private bool IsOpened()
        {
            if (_conn.State != ConnectionState.Open)
            {
                if (string.IsNullOrEmpty(_conn.ConnectionString))
                {
                    // 获取配置ConnectionString
                    _conn.ConnectionString = _connectionString;
                }
                _conn.Open();
            }

            return _conn.State == ConnectionState.Open;
        }

        public CommandType CommandType
        {
            get
            {
                return _comm.CommandType;
            }
            set
            {
                _comm.CommandType = value;
            }
        }

        public string CommandText
        {
            get
            {
                return _comm.CommandText;
            }
            set
            {
                _comm.CommandText = value;
            }
        }

        public int CommandTimeout
        {
            get
            {
                return _comm.CommandTimeout;
            }
            set
            {
                _comm.CommandTimeout = value;
            }
        }

        public DataSet DBSelect()
        {
            if (IsOpened())
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter(_comm);
                DataSet ds = new DataSet();
                //da.MissingSchemaAction = MissingSchemaAction.Add;
                da.Fill(ds);
                return ds;

            }
            else
            {
                return null;
            }
        }

        public DataTable DBSelect(string tableName)
        {
            if (IsOpened())
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter(_comm);
                DataTable dt = new DataTable(tableName);
                //da.MissingSchemaAction = MissingSchemaAction.Add;
                da.Fill(dt);
                return dt;

            }
            else
            {
                return null;
            }
        }

        public void Fill(DataSet dataSet)
        {
            if (IsOpened())
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter(_comm);
                da.Fill(dataSet);
            }
        }

        public void Fill(DataSet dataSet, string srcTable)
        {
            if (IsOpened())
            {
                SQLiteDataAdapter da = new SQLiteDataAdapter(_comm);
                da.Fill(dataSet, srcTable);
            }
        }

        public int ExecuteNonQuery()
        {
            if (IsOpened())
            {
                return _comm.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public object ExecuteScalar()
        {
            if (IsOpened())
            {
                return _comm.ExecuteScalar();
            }
            else
            {
                return -1;
            }
        }

        public void BeginTransaction()
        {
            if (IsOpened())
            {
                // _trans = _conn.BeginTransaction();
                _comm.Transaction = _conn.BeginTransaction();
            }
        }

        public void Commit()
        {
            //if (_trans != null)
            //{
            //    _trans.Commit();
            //}

            if (_comm.Transaction != null)
            {
                _comm.Transaction.Commit();
            }
        }
        public void Rollback()
        {
            //if (_trans != null)
            //{
            //    _trans.Rollback();
            //}

            if (_comm.Transaction != null)
            {
                _comm.Transaction.Rollback();
            }
        }



        public DBParameterCollection Parameters;
    }

    public class DBParameterCollection
    {
        private SQLiteParameterCollection _parameters;

        public DBParameterCollection(SQLiteCommand comm)
        {
            _parameters = comm.Parameters;
        }

        public SQLiteParameterCollection SQLParameters
        {
            get
            {
                return _parameters;
            }
        }

        public void AddWithValue(string parameterName, object value)
        {
            _parameters.AddWithValue(parameterName, value);
        }

        public void Clear()
        {
            _parameters.Clear();
        }

        public DBSQLiteParameter Add(string parameterName, DbType dbType, int size)
        {
            return new DBSQLiteParameter(_parameters.Add(parameterName, dbType, size));
        }

        public DBSQLiteParameter Add(string parameterName, DbType dbType)
        {
            return new DBSQLiteParameter(_parameters.Add(parameterName, dbType));
        }

        public DBSQLiteParameter this[int index]
        {
            get
            {
                return new DBSQLiteParameter(_parameters[index]);
            }
            set
            {
                _parameters[index] = value.SqlParameter;
            }
        }

        public DBSQLiteParameter this[string parameterName]
        {
            get
            {
                return new DBSQLiteParameter(_parameters[parameterName]);
            }
            set
            {
                _parameters[parameterName] = value.SqlParameter;
            }
        }

        public bool Contain(string parameterName)
        {
            return _parameters.Contains(parameterName);
        }
    }

    public class DBSQLiteParameter
    {
        private SQLiteParameter _parameter;

        public DBSQLiteParameter(SQLiteParameter parameter)
        {
            _parameter = parameter;
        }

        public SQLiteParameter SqlParameter
        {
            get
            {
                return _parameter;
            }
        }

        public object Value
        {
            get
            {
                return _parameter.Value;
            }
            set
            {
                _parameter.Value = value;
            }
        }

        public ParameterDirection Direction
        {
            get
            {
                return _parameter.Direction;
            }
            set
            {
                _parameter.Direction = value;
            }
        }
    }
}

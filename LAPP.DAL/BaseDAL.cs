using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace LAPP.DAL
{
    public class BaseDAL
    {
    }
    public class DBHelper
    {
        #region Class Variable
        private MySql.Data.MySqlClient.MySqlConnection _conn;
        private static DBHelper instance;
        private static string connString = ConfigurationManager.ConnectionStrings["LappDB"].ConnectionString;
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());
        private static readonly string RETURNVALUE = "RETURNVALUE";
        #endregion
        public DBHelper()
        {
            _conn = new MySqlConnection(connString);
        }
        //public static DBHelper Instance
        //{
        // get
        // {
        // if (instance == null)
        // {
        // lock (typeof(DBHelper))
        // {
        // if (instance == null)
        // {
        // instance = new DBHelper();
        // }
        // }
        // }
        // return instance;
        // }
        //}
        #region ExecuteReader : Return a single DataReader
        public MySqlDataReader ExecuteReader(string commandText)
        {
            return ExecuteReader(CommandType.Text, commandText, (MySqlParameter[])null);
        }
        public MySqlDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            return ExecuteReader(commandType, commandText, (MySqlParameter[])null);
        }
        public MySqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params MySqlParameter[] cmdParms)
        {
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                PrepareCommand(cmd, null, cmdType, cmdText, cmdParms);
                MySqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch (Exception ex)
            {
                _conn.Close();
                throw ex;
            }
            finally
            {
                _conn.Close();
            }
        }
        public MySqlDataReader ExecuteReader(CommandType cmdType, string cmdText, MySqlParameter[] cmdParms, bool useTrans)
        {
            MySql.Data.MySqlClient.MySqlCommand cmd = _conn.CreateCommand();
            MySql.Data.MySqlClient.MySqlTransaction trans = null;
            if (useTrans)
            {
                trans = _conn.BeginTransaction();
            }
            try
            {
                PrepareCommand(cmd, trans, cmdType, cmdText, cmdParms);
                MySqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                if (useTrans)
                {
                    trans.Commit();
                }
                return rdr;
            }
            catch (Exception ex)
            {
                if (useTrans)
                {
                    trans.Rollback();
                }
                Close();
                throw ex;
            }
        }
        #endregion
        #region ExecuteScalar : Return a single value(object)
        public object ExecuteScalar(string cmdText)
        {
            return ExecuteScalar(CommandType.Text, cmdText, null);
        }
        public object ExecuteScalar(CommandType cmdType, string cmdText)
        {
            return ExecuteScalar(cmdType, cmdText, null);
        }
        public object ExecuteScalar(CommandType cmdType, string cmdText, params MySqlParameter[] cmdParms)
        {
            MySqlCommand cmd = _conn.CreateCommand();
            try
            {
                PrepareCommand(cmd, null, cmdType, cmdText, cmdParms);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
            catch (Exception ex)
            {
                Close();
                throw ex;
            }
            finally
            {
                Close();
            }
        }
        public object ExecuteScalar(CommandType cmdType, string cmdText, MySqlParameter[] cmdParms, bool useTrans)
        {
            MySqlCommand cmd = _conn.CreateCommand();
            MySqlTransaction trans = null;
            if (useTrans)
            {
                trans = _conn.BeginTransaction();
            }
            try
            {
                PrepareCommand(cmd, trans, cmdType, cmdText, cmdParms);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                if (useTrans)
                {
                    trans.Commit();
                }
                return val;
            }
            catch (Exception ex)
            {
                if (useTrans)
                {
                    trans.Rollback();
                }
                Close();
                throw ex;
            }
            finally
            {
                Close();
            }
        }
        #endregion
        #region ExecuteNonQuery : Return the number of lines affected!
        public int ExecuteNonQuery(string cmdText)
        {
            return this.ExecuteNonQuery(CommandType.Text, cmdText, (MySqlParameter[])null);
        }
        public int ExecuteNonQuery(CommandType cmdType, string cmdText)
        {
            return this.ExecuteNonQuery(cmdType, cmdText, (MySqlParameter[])null);
        }
        public int ExecuteNonQuery(CommandType cmdType, string cmdText, params MySqlParameter[] cmdParms)
        {
            MySqlCommand cmd = _conn.CreateCommand();
            try
            {
                PrepareCommand(cmd, null, cmdType, cmdText, cmdParms);
                cmd.ExecuteNonQuery();
                //int retunvalue = (Int32)cmd.Parameters["ReturnParam"].Value;
                cmd.Parameters.Clear();
                return 0;
            }
            catch (Exception ex)
            {
                Close();
                throw ex;
            }
            finally
            {
                Close();
            }
        }
        public int ExecuteNonQuery(CommandType cmdType, string cmdText, bool IsReturnParam, params MySqlParameter[] cmdParms)
        {
            MySqlCommand cmd = _conn.CreateCommand();
            try
            {
                PrepareCommand(cmd, null, cmdType, cmdText, cmdParms);
                cmd.ExecuteNonQuery();
                int retunvalue = (Int32)cmd.Parameters["ReturnParam"].Value;
                cmd.Parameters.Clear();
                return retunvalue;
            }
            catch (Exception ex)
            {
                Close();
                throw ex;
            }
            finally
            {
                Close();
            }
        }
        public int ExecuteNonQuery(CommandType cmdType, string cmdText, MySqlParameter[] cmdParms, bool useTrans)
        {
            MySqlCommand cmd = _conn.CreateCommand();
            MySqlTransaction trans = null;
            if (useTrans)
            {
                trans = _conn.BeginTransaction();
            }
            try
            {
                PrepareCommand(cmd, trans, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                if (useTrans)
                {
                    trans.Commit();
                }
                return val;
            }
            catch (Exception ex)
            {
                if (useTrans)
                {
                    trans.Rollback();
                }
                Close();
                throw ex;
            }
            finally
            {
                Close();
            }
        }
        #endregion
        #region ExecuteDataSet : Return a DataSet

        public DataSet ExecuteDataSet(string cmdText)
        {
            return this.ExecuteDataSet(CommandType.Text, cmdText, (MySqlParameter[])null);
        }

        public DataSet ExecuteDataSet(CommandType cmdType, string cmdText)
        {
            return this.ExecuteDataSet(cmdType, cmdText, (MySqlParameter[])null);
        }

        public DataSet ExecuteDataSet(CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();

            try
            {
                PrepareCommand(cmd, (MySqlTransaction)null, commandType, commandText, commandParameters);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
            catch (Exception ex)
            {
                Close();
                throw ex;
            }
            finally
            {
                Close();
            }
        }

        public DataSet ExecuteDataSet(CommandType commandType, string commandText, MySqlParameter[] commandParameters, bool useTrans)
        {
            MySqlCommand cmd = _conn.CreateCommand();
            MySqlTransaction trans = null;
            if (useTrans)
            {
                trans = _conn.BeginTransaction();
            }
            try
            {
                PrepareCommand(cmd, trans, commandType, commandText, commandParameters);

                //´´½¨MySqlDataAdapter¶ÔÏóÒÔ¼°DataSet 
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                //Ìî³äds 
                da.Fill(ds);

                // Çå³ýcmdµÄ²ÎÊý¼¯ºÏ	
                cmd.Parameters.Clear();

                if (useTrans)
                {
                    trans.Commit();
                }
                //·µ»Øds 
                return ds;
            }
            catch (Exception ex)
            {
                if (useTrans)
                {
                    trans.Rollback();
                }

                Close();
                throw ex;
            }
            finally
            {
                Close();
            }
        }


        #endregion
        #region TODO: ExecuteXmlReader
        #endregion ExecuteXmlReader
        #region TODO: ExecProc : Execute a Stored Procedure
        public int ExecProc(string procName)
        {
            return 0;
        }
        public int ExecProc(string procName, MySqlParameter[] paras)
        {
            return 0;
        }
        public MySqlDataReader ExecProcReader(string procName)
        {
            return this.ExecuteReader(CommandType.StoredProcedure, procName);
        }
        public MySqlDataReader ExecProcReader(string procName, MySqlParameter[] paras)
        {
            return this.ExecuteReader(CommandType.StoredProcedure, procName, paras);
        }
        public DataSet ExecProcDataSet(string procName)
        {
            return this.ExecuteDataSet(CommandType.StoredProcedure, procName);
        }
        public DataSet ExecProcDataSet(string procName, MySqlParameter[] paras)
        {
            return this.ExecuteDataSet(CommandType.StoredProcedure, procName, paras);
        }
        #endregion ExecProc : Execute a Stored Procedure
        #region Public Properties
        public string DBName
        {
            get
            {
                return _conn.Database;
            }
        }
        public string ConnString
        {
            get
            {
                return _conn.ConnectionString;
            }
        }
        public string DataSource
        {
            get
            {
                return _conn.DataSource;
            }
        }
        public string ServerVersion
        {
            get
            {
                return _conn.ServerVersion;
            }
        }
        #endregion Public Properties
        #region Public Methods
        public void Close()
        {
            if (_conn != null)
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
        }
        public void Dispose()
        {
            if (_conn != null)
            {
                _conn.Dispose();
                _conn = null;
            }
        }
        public void Refresh()
        {
            this.Close();
            this.Dispose();
            DBHelper.instance = null;
        }
        #endregion Public Methods
        #region Private Methods
        private void PrepareCommand(MySqlCommand cmd, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        {
            if (_conn.State != ConnectionState.Open)
                _conn.Open();
            cmd.Connection = _conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            cmd.CommandTimeout = 300;
            if (cmdParms != null)
            {
                foreach (MySqlParameter parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
            }
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
        }
        #endregion Private Methods
        #region Static Methods
        public static void CacheParameters(string cacheKey, params MySqlParameter[] cmdParms)
        {
            parmCache[cacheKey] = cmdParms;
        }
        public static MySqlParameter[] GetCachedParameters(string cacheKey)
        {
            MySqlParameter[] cachedParms = (MySqlParameter[])parmCache[cacheKey];
            if (cachedParms == null)
                return null;
            MySqlParameter[] clonedParms = new MySqlParameter[cachedParms.Length];
            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (MySqlParameter)((ICloneable)cachedParms[i]).Clone();
            return clonedParms;
        }
        #endregion
    }
}

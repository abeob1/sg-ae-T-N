using System;
using System.Data.Common;
using System.Configuration;
using System.Data;
namespace SAP.Admin.DAO
{
    public class Data
    {
        /// <summary>
        /// "System.Data.SqlClient"
        /// </summary>
        public static string dataProvider = ConfigurationManager.ConnectionStrings["dbconnection"].ProviderName;
        private static readonly DbProviderFactory factory = DbProviderFactories.GetFactory(dataProvider);
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        #region parameters

        public static DbParameter CreateParameter(string ParameterName, DbType ParameterType, int ParameterSize)
        {
            DbParameter p = factory.CreateParameter();
            p.ParameterName = ParameterName;
            p.DbType = ParameterType;
            p.Size = ParameterSize;
            return p;
        }

        public static DbParameter CreateParameter(string ParameterName, object ParameterValue)
        {
            DbParameter p = factory.CreateParameter();
            p.ParameterName = ParameterName;
            p.Value = ParameterValue;
            return p;
        }

        public static DbParameter CreateParameter(string ParameterName)
        {
            DbParameter p = factory.CreateParameter();
            p.ParameterName = ParameterName;
            return p;
        }

        #endregion
    }
}

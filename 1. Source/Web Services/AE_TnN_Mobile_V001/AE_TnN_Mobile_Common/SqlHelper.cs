using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Transactions;
using System.Data.Common;


namespace SAP.Admin.DAO
{

    public enum DBCompareType
    {
        Equal = 0, NotEqual = 1, Larger = 2, Smaller = 3, LargerNEqual = 4, SmallerNEqual = 5, IS = 6, ISNOT = 7, LIKE = 8
    }

    public enum DBDataFormula
    {
        GETDATE = 0, GETID = 1, NULL = 2
    }

    public enum DBDataType
    {
        String = 0, Integer = 1, Date = 2, Formula = 3, DBColumn = 4, SQLFormula = 5
    }

    public enum DBLinkage
    {
        AND = 0, OR = 1
    }

    public enum TypeOfDatabase
    {
        MSSQL = 0, OleDB = 1, ODBC = 2
    }

    public class DatabaseParameter
    {
        #region Fields
        public bool ASC;
        public DBCompareType DataCompare;
        private string[] DataCompareStr;
        public DBDataFormula DataFormula;
        private string[] DataFormulaStr;
        public DBLinkage DataLinkage;
        private string[] DataLinkageStr;
        public DBDataType DataType;
        private string Field;
        public bool IsNullable;
        public bool IsUnicode;
        public bool SortBy;
        private string StringPattern;
        private string Value;
        #endregion

        #region Properties
        public string FieldName
        {
            get { return this.Field; }
        }

        public string FieldValue
        {
            get { return ((this.DataType == DBDataType.Formula) ? this.DataFormulaStr[(int)this.DataFormula] : ((this.Value.Length > 0) ? this.Value : this.StringPattern)); }
        }

        public string ParamCompare
        {
            get { return (" " + this.DataCompareStr[(int)this.DataCompare] + " "); }
        }

        public string ParamLinkage
        {
            get { return (" " + this.DataLinkageStr[(int)this.DataLinkage] + " "); }
        }

        public DBDataType ParamType
        {
            get { return this.DataType; }
        }
        #endregion

        #region Methods
        public DatabaseParameter(string Field, DBDataFormula DataFormula)
        {
            this.Field = string.Empty;
            this.Value = string.Empty;
            this.StringPattern = string.Empty;
            this.DataCompareStr = new string[] { " = ", " <> ", " > ", " < ", " >= ", " <= ", " IS ", " IS NOT ", " LIKE " };
            this.DataLinkageStr = new string[] { " AND ", " OR " };
            this.DataFormulaStr = new string[] { " GETDATE() ", " GETID() ", " NULL " };
            this.IsUnicode = false;
            this.IsNullable = false;
            this.DataType = DBDataType.String;
            this.DataCompare = DBCompareType.Equal;
            this.DataLinkage = DBLinkage.AND;
            this.SortBy = false;
            this.ASC = true;
            this.Field = Field;
            this.DataFormula = DataFormula;
            this.DataType = DBDataType.Formula;
        }

        public DatabaseParameter(string Field, DateTime TargetDateTime)
        {
            this.Field = string.Empty;
            this.Value = string.Empty;
            this.StringPattern = string.Empty;
            this.DataCompareStr = new string[] { " = ", " <> ", " > ", " < ", " >= ", " <= ", " IS ", " IS NOT ", " LIKE " };
            this.DataLinkageStr = new string[] { " AND ", " OR " };
            this.DataFormulaStr = new string[] { " GETDATE() ", " GETID() ", " NULL " };
            this.IsUnicode = false;
            this.IsNullable = false;
            this.DataType = DBDataType.String;
            this.DataCompare = DBCompareType.Equal;
            this.DataLinkage = DBLinkage.AND;
            this.SortBy = false;
            this.ASC = true;
            this.Field = Field;
            this.Value = TargetDateTime.ToString();
        }

        public DatabaseParameter(string Field, string Value)
        {
            this.Field = string.Empty;
            this.Value = string.Empty;
            this.StringPattern = string.Empty;
            this.DataCompareStr = new string[] { " = ", " <> ", " > ", " < ", " >= ", " <= ", " IS ", " IS NOT ", " LIKE " };
            this.DataLinkageStr = new string[] { " AND ", " OR " };
            this.DataFormulaStr = new string[] { " GETDATE() ", " GETID() ", " NULL " };
            this.IsUnicode = false;
            this.IsNullable = false;
            this.DataType = DBDataType.String;
            this.DataCompare = DBCompareType.Equal;
            this.DataLinkage = DBLinkage.AND;
            this.SortBy = false;
            this.ASC = true;
            this.Field = Field;
            this.Value = Value;
        }

        public DatabaseParameter(string Field, DateTime TargetDateTime, bool IsNullable)
        {
            this.Field = string.Empty;
            this.Value = string.Empty;
            this.StringPattern = string.Empty;
            this.DataCompareStr = new string[] { " = ", " <> ", " > ", " < ", " >= ", " <= ", " IS ", " IS NOT ", " LIKE " };
            this.DataLinkageStr = new string[] { " AND ", " OR " };
            this.DataFormulaStr = new string[] { " GETDATE() ", " GETID() ", " NULL " };
            this.IsUnicode = false;
            this.IsNullable = false;
            this.DataType = DBDataType.String;
            this.DataCompare = DBCompareType.Equal;
            this.DataLinkage = DBLinkage.AND;
            this.SortBy = false;
            this.ASC = true;
            this.Field = Field;
            this.Value = TargetDateTime.ToString();
            this.IsNullable = IsNullable;
        }

        public DatabaseParameter(string Field, string Value, DBDataType DataType)
        {
            this.Field = string.Empty;
            this.Value = string.Empty;
            this.StringPattern = string.Empty;
            this.DataCompareStr = new string[] { " = ", " <> ", " > ", " < ", " >= ", " <= ", " IS ", " IS NOT ", " LIKE " };
            this.DataLinkageStr = new string[] { " AND ", " OR " };
            this.DataFormulaStr = new string[] { " GETDATE() ", " GETID() ", " NULL " };
            this.IsUnicode = false;
            this.IsNullable = false;
            this.DataType = DBDataType.String;
            this.DataCompare = DBCompareType.Equal;
            this.DataLinkage = DBLinkage.AND;
            this.SortBy = false;
            this.ASC = true;
            this.Field = Field;
            this.Value = Value;
            this.DataType = DataType;
        }

        public DatabaseParameter(string Field, string StringPattern, DBLinkage DataLinkage)
        {
            this.Field = string.Empty;
            this.Value = string.Empty;
            this.StringPattern = string.Empty;
            this.DataCompareStr = new string[] { " = ", " <> ", " > ", " < ", " >= ", " <= ", " IS ", " IS NOT ", " LIKE " };
            this.DataLinkageStr = new string[] { " AND ", " OR " };
            this.DataFormulaStr = new string[] { " GETDATE() ", " GETID() ", " NULL " };
            this.IsUnicode = false;
            this.IsNullable = false;
            this.DataType = DBDataType.String;
            this.DataCompare = DBCompareType.Equal;
            this.DataLinkage = DBLinkage.AND;
            this.SortBy = false;
            this.ASC = true;
            this.Field = Field;
            this.StringPattern = StringPattern;
            this.DataCompare = DBCompareType.LIKE;
            this.DataLinkage = DataLinkage;
        }

        public DatabaseParameter(string Field, string Value, bool IsUnicode)
        {
            this.Field = string.Empty;
            this.Value = string.Empty;
            this.StringPattern = string.Empty;
            this.DataCompareStr = new string[] { " = ", " <> ", " > ", " < ", " >= ", " <= ", " IS ", " IS NOT ", " LIKE " };
            this.DataLinkageStr = new string[] { " AND ", " OR " };
            this.DataFormulaStr = new string[] { " GETDATE() ", " GETID() ", " NULL " };
            this.IsUnicode = false;
            this.IsNullable = false;
            this.DataType = DBDataType.String;
            this.DataCompare = DBCompareType.Equal;
            this.DataLinkage = DBLinkage.AND;
            this.SortBy = false;
            this.ASC = true;
            this.Field = Field;
            this.Value = Value;
            this.IsUnicode = IsUnicode;
        }

        public DatabaseParameter(string Field, DBDataFormula DataFormula, DBLinkage DataLinkage, DBCompareType DataCompare)
        {
            this.Field = string.Empty;
            this.Value = string.Empty;
            this.StringPattern = string.Empty;
            this.DataCompareStr = new string[] { " = ", " <> ", " > ", " < ", " >= ", " <= ", " IS ", " IS NOT ", " LIKE " };
            this.DataLinkageStr = new string[] { " AND ", " OR " };
            this.DataFormulaStr = new string[] { " GETDATE() ", " GETID() ", " NULL " };
            this.IsUnicode = false;
            this.IsNullable = false;
            this.DataType = DBDataType.String;
            this.DataCompare = DBCompareType.Equal;
            this.DataLinkage = DBLinkage.AND;
            this.SortBy = false;
            this.ASC = true;
            this.Field = Field;
            this.DataFormula = DataFormula;
            this.DataType = DBDataType.Formula;
            this.DataCompare = DataCompare;
            this.DataLinkage = DataLinkage;
        }

        public DatabaseParameter(string Field, DateTime TargetDateTime, DBLinkage DataLinkage, DBCompareType DataCompare)
        {
            this.Field = string.Empty;
            this.Value = string.Empty;
            this.StringPattern = string.Empty;
            this.DataCompareStr = new string[] { " = ", " <> ", " > ", " < ", " >= ", " <= ", " IS ", " IS NOT ", " LIKE " };
            this.DataLinkageStr = new string[] { " AND ", " OR " };
            this.DataFormulaStr = new string[] { " GETDATE() ", " GETID() ", " NULL " };
            this.IsUnicode = false;
            this.IsNullable = false;
            this.DataType = DBDataType.String;
            this.DataCompare = DBCompareType.Equal;
            this.DataLinkage = DBLinkage.AND;
            this.SortBy = false;
            this.ASC = true;
            this.Field = Field;
            this.Value = TargetDateTime.ToString();
            this.DataType = DBDataType.Date;
            this.DataCompare = DataCompare;
            this.DataLinkage = DataLinkage;
        }

        public DatabaseParameter(string Field, string Value, DBDataType DataType, DBLinkage DataLinkage)
        {
            this.Field = string.Empty;
            this.Value = string.Empty;
            this.StringPattern = string.Empty;
            this.DataCompareStr = new string[] { " = ", " <> ", " > ", " < ", " >= ", " <= ", " IS ", " IS NOT ", " LIKE " };
            this.DataLinkageStr = new string[] { " AND ", " OR " };
            this.DataFormulaStr = new string[] { " GETDATE() ", " GETID() ", " NULL " };
            this.IsUnicode = false;
            this.IsNullable = false;
            this.DataType = DBDataType.String;
            this.DataCompare = DBCompareType.Equal;
            this.DataLinkage = DBLinkage.AND;
            this.SortBy = false;
            this.ASC = true;
            this.Field = Field;
            this.Value = Value;
            this.DataType = DataType;
            this.DataLinkage = DataLinkage;
        }

        public DatabaseParameter(string Field, string Value, DBDataType DataType, bool IsUnicode)
        {
            this.Field = string.Empty;
            this.Value = string.Empty;
            this.StringPattern = string.Empty;
            this.DataCompareStr = new string[] { " = ", " <> ", " > ", " < ", " >= ", " <= ", " IS ", " IS NOT ", " LIKE " };
            this.DataLinkageStr = new string[] { " AND ", " OR " };
            this.DataFormulaStr = new string[] { " GETDATE() ", " GETID() ", " NULL " };
            this.IsUnicode = false;
            this.IsNullable = false;
            this.DataType = DBDataType.String;
            this.DataCompare = DBCompareType.Equal;
            this.DataLinkage = DBLinkage.AND;
            this.SortBy = false;
            this.ASC = true;
            this.Field = Field;
            this.Value = Value;
            this.DataType = DataType;
            this.IsUnicode = IsUnicode;
        }

        public DatabaseParameter(string Field, string Value, bool IsUnicode, bool IsNullable)
        {
            this.Field = string.Empty;
            this.Value = string.Empty;
            this.StringPattern = string.Empty;
            this.DataCompareStr = new string[] { " = ", " <> ", " > ", " < ", " >= ", " <= ", " IS ", " IS NOT ", " LIKE " };
            this.DataLinkageStr = new string[] { " AND ", " OR " };
            this.DataFormulaStr = new string[] { " GETDATE() ", " GETID() ", " NULL " };
            this.IsUnicode = false;
            this.IsNullable = false;
            this.DataType = DBDataType.String;
            this.DataCompare = DBCompareType.Equal;
            this.DataLinkage = DBLinkage.AND;
            this.SortBy = false;
            this.ASC = true;
            this.Field = Field;
            this.Value = Value;
            this.IsUnicode = IsUnicode;
            this.IsNullable = IsNullable;
        }

        public DatabaseParameter(string Field, string Value, DBDataType DataType, DBLinkage DataLinkage, DBCompareType DataCompare)
        {
            this.Field = string.Empty;
            this.Value = string.Empty;
            this.StringPattern = string.Empty;
            this.DataCompareStr = new string[] { " = ", " <> ", " > ", " < ", " >= ", " <= ", " IS ", " IS NOT ", " LIKE " };
            this.DataLinkageStr = new string[] { " AND ", " OR " };
            this.DataFormulaStr = new string[] { " GETDATE() ", " GETID() ", " NULL " };
            this.IsUnicode = false;
            this.IsNullable = false;
            this.DataType = DBDataType.String;
            this.DataCompare = DBCompareType.Equal;
            this.DataLinkage = DBLinkage.AND;
            this.SortBy = false;
            this.ASC = true;
            this.Field = Field;
            this.Value = Value;
            this.DataType = DataType;
            this.DataCompare = DataCompare;
            this.DataLinkage = DataLinkage;
        }

        public DatabaseParameter(string Field, string Value, DBDataType DataType, DBLinkage DataLinkage, bool IsUnicode)
        {
            this.Field = string.Empty;
            this.Value = string.Empty;
            this.StringPattern = string.Empty;
            this.DataCompareStr = new string[] { " = ", " <> ", " > ", " < ", " >= ", " <= ", " IS ", " IS NOT ", " LIKE " };
            this.DataLinkageStr = new string[] { " AND ", " OR " };
            this.DataFormulaStr = new string[] { " GETDATE() ", " GETID() ", " NULL " };
            this.IsUnicode = false;
            this.IsNullable = false;
            this.DataType = DBDataType.String;
            this.DataCompare = DBCompareType.Equal;
            this.DataLinkage = DBLinkage.AND;
            this.SortBy = false;
            this.ASC = true;
            this.Field = Field;
            this.Value = Value;
            this.DataType = DataType;
            this.DataLinkage = DataLinkage;
            this.IsUnicode = IsUnicode;
        }

        public DatabaseParameter(string Field, string Value, DBDataType DBDataType, bool IsUnicode, bool IsNullable)
        {
            this.Field = string.Empty;
            this.Value = string.Empty;
            this.StringPattern = string.Empty;
            this.DataCompareStr = new string[] { " = ", " <> ", " > ", " < ", " >= ", " <= ", " IS ", " IS NOT ", " LIKE " };
            this.DataLinkageStr = new string[] { " AND ", " OR " };
            this.DataFormulaStr = new string[] { " GETDATE() ", " GETID() ", " NULL " };
            this.IsUnicode = false;
            this.IsNullable = false;
            this.DataType = DBDataType.String;
            this.DataCompare = DBCompareType.Equal;
            this.DataLinkage = DBLinkage.AND;
            this.SortBy = false;
            this.ASC = true;
            this.Field = Field;
            this.Value = Value;
            this.DataType = DBDataType;
            this.IsUnicode = IsUnicode;
            this.IsNullable = IsNullable;
        }

        public DatabaseParameter(string Field, string Value, DBDataType DataType, DBLinkage DataLinkage, bool IsUnicode, bool IsNullable)
        {
            this.Field = string.Empty;
            this.Value = string.Empty;
            this.StringPattern = string.Empty;
            this.DataCompareStr = new string[] { " = ", " <> ", " > ", " < ", " >= ", " <= ", " IS ", " IS NOT ", " LIKE " };
            this.DataLinkageStr = new string[] { " AND ", " OR " };
            this.DataFormulaStr = new string[] { " GETDATE() ", " GETID() ", " NULL " };
            this.IsUnicode = false;
            this.IsNullable = false;
            this.DataType = DBDataType.String;
            this.DataCompare = DBCompareType.Equal;
            this.DataLinkage = DBLinkage.AND;
            this.SortBy = false;
            this.ASC = true;
            this.Field = Field;
            this.Value = Value;
            this.DataType = DataType;
            this.DataLinkage = DataLinkage;
            this.IsUnicode = IsUnicode;
            this.IsNullable = IsNullable;
        }
        #endregion
    }

    public class DatabaseParameters : CollectionBase
    {
        #region Fields
        public DBLinkage DataLinkage;
        #endregion

        #region Properties
        public DatabaseParameter this[int index]
        {
            get { return (DatabaseParameter)base.List[index]; }
            set { base.List[index] = value; }
        }
        #endregion

        #region Methods
        public DatabaseParameters()
        {
            this.DataLinkage = DBLinkage.AND;
        }

        public DatabaseParameters(DBLinkage DataLinkage)
        {
            this.DataLinkage = DBLinkage.AND;
            this.DataLinkage = DataLinkage;
        }

        public int Add(DatabaseParameter value)
        {
            return base.List.Add(value);
        }

        public int IndexOf(DatabaseParameter value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, DatabaseParameter value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(DatabaseParameter value)
        {
            base.List.Remove(value);
        }
        #endregion
    }

    public class DatabaseParametersGroup : CollectionBase
    {
        #region Fields
        public DBLinkage DataLinkage;
        #endregion

        #region Properties
        public DatabaseParameters this[int index]
        {
            get { return (DatabaseParameters)base.List[index]; }
            set { base.List[index] = value; }
        }
        #endregion

        #region Methods
        public DatabaseParametersGroup()
        {
            this.DataLinkage = DBLinkage.AND;
        }

        public DatabaseParametersGroup(DBLinkage DataLinkage)
        {
            this.DataLinkage = DBLinkage.AND;
            this.DataLinkage = DataLinkage;
        }

        public int Add(DatabaseParameters value)
        {
            return base.List.Add(value);
        }

        public int IndexOf(DatabaseParameters value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, DatabaseParameters value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(DatabaseParameters value)
        {
            base.List.Remove(value);
        }
        #endregion
    }

    public class SqlHelper
    {
        // Fields
        private static string sql;
        private static string errmsg = string.Empty;

        // Properties
        public static string SQL
        {
            get { return sql; }
            set { sql = value; }
        }

        public static string ErrMsg
        {
            get { return errmsg; }
            set { errmsg = value; }
        }

        public SqlHelper()
        {
            sql = string.Empty;
        }

        #region private methods
        private static object CheckValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }
            return value;
        }

        private static SqlConnection GetTransactionSqlConnection(string connectionString)
        {
            LocalDataStoreSlot namedDataSlot = Thread.GetNamedDataSlot("ConnectionDictionary");
            Dictionary<string, SqlConnection> data = (Dictionary<string, SqlConnection>)Thread.GetData(namedDataSlot);
            if (data == null)
            {
                data = new Dictionary<string, SqlConnection>();
                Thread.SetData(namedDataSlot, data);
            }
            SqlConnection connection = null;
            if (data.ContainsKey(connectionString))
            {
                return data[connectionString];
            }
            connection = new SqlConnection(connectionString);
            data.Add(connectionString, connection);
            Transaction.Current.TransactionCompleted += new TransactionCompletedEventHandler(Current_TransactionCompleted);
            return connection;
        }

        private static void Current_TransactionCompleted(object sender, TransactionEventArgs e)
        {
            Dictionary<string, SqlConnection> data = (Dictionary<string, SqlConnection>)Thread.GetData(Thread.GetNamedDataSlot("ConnectionDictionary"));
            if (data != null)
            {
                foreach (SqlConnection connection in data.Values)
                {
                    if ((connection != null) && (connection.State != ConnectionState.Closed))
                    {
                        connection.Close();
                    }
                }
                data.Clear();
            }
            Thread.FreeNamedDataSlot("ConnectionDictionary");
        }

        private static SqlCommand CreateCommand(SqlConnection connection, CommandType commandType, string commandText)
        {
            if ((connection != null) && (connection.State == ConnectionState.Closed))
            {
                connection.Open();
            }
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = commandText;
                command.CommandType = commandType;
                return command;
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, CommandType commandType, string commandText, params object[] values)
        {
            if ((connection != null) && (connection.State == ConnectionState.Closed))
            {
                connection.Open();
            }
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = commandText;
                command.CommandType = commandType;
                if ((values == null) || (values.Length == 0))
                {
                    for (int j = 0; j < command.Parameters.Count; j++)
                    {
                        command.Parameters[j].Value = DBNull.Value;
                    }
                    return command;
                }
                command.Parameters.AddRange(values);
                return command;
            }
        }

        public static void InsertCommand(DatabaseParameters keys, string table)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();

            for (int i = 0; i < keys.Count; i++)
            {
                builder.Append(keys[i].FieldName + ",");
                builder2.Append((((keys[i].ParamType == DBDataType.Formula) || (keys[i].ParamType == DBDataType.SQLFormula)) ? keys[i].FieldValue : (((keys[i].FieldValue.Length > 0)) ? ("'" + keys[i].FieldValue + "'") : "NULL")) + ",");
            }
            builder.Remove(builder.Length - 1, 1);
            builder2.Remove(builder2.Length - 1, 1);
            sql = "INSERT INTO " + table + " ( " + builder.ToString() + " ) VALUES (" + builder2.ToString() + ")";
        }

        public static void DeleteCommand(DatabaseParameters keys, string table)
        {
            StringBuilder builder = new StringBuilder("DELETE FROM " + table + " ");
            if (keys != null)
            {
                for (int i = 0; i < keys.Count; i++)
                {
                    builder.Append((i == 0) ? " WHERE " : (" " + keys[i].ParamLinkage.ToString() + " "));
                    builder.Append(((keys[i].ParamType == DBDataType.Formula) || (keys[i].ParamType == DBDataType.SQLFormula)) ? (keys[i].FieldName + keys[i].ParamCompare + keys[i].FieldValue) : (keys[i].FieldName + keys[i].ParamCompare + "'" + keys[i].FieldValue + "'"));
                }
            }
            sql = builder.ToString();

        }

        private static void SelectCommand(DatabaseParameters Parameters, string TableNames)
        {
            DatabaseParametersGroup parametersGroup = new DatabaseParametersGroup();
            parametersGroup.Add(Parameters);
            string[] tableNames = new string[] { TableNames };
            SelectCommand(parametersGroup, tableNames);
        }

        private static void SelectCommand(DatabaseParametersGroup ParametersGroup, string[] TableNames)
        {
            int num;
            StringBuilder builder = new StringBuilder("SELECT * FROM ");
            StringBuilder builder2 = new StringBuilder();
            for (num = 0; num < TableNames.Length; num++)
            {
                builder.Append(TableNames[num] + ",");
            }
            builder.Remove(builder.Length - 1, 1);
            for (num = 0; num < ParametersGroup.Count; num++)
            {
                if (ParametersGroup[num].Count > 0)
                {
                    builder.Append((num == 0) ? " WHERE " : (" " + ParametersGroup[num].DataLinkage.ToString() + " "));
                    builder.Append(" (");
                    for (int i = 0; i < ParametersGroup[num].Count; i++)
                    {
                        builder.Append((i == 0) ? "" : (" " + ParametersGroup[num][i].ParamLinkage.ToString() + " "));
                        builder.Append(((ParametersGroup[num][i].ParamType == DBDataType.SQLFormula) || (ParametersGroup[num][i].ParamType == DBDataType.Formula)) ? (ParametersGroup[num][i].FieldName + ParametersGroup[num][i].ParamCompare + ParametersGroup[num][i].FieldValue) : (ParametersGroup[num][i].FieldName + " " + ParametersGroup[num][i].ParamCompare + " '" + ParametersGroup[num][i].FieldValue + "'"));
                        if (ParametersGroup[num][i].SortBy)
                        {
                            builder2.Append(ParametersGroup[num][i].FieldName + " " + (ParametersGroup[num][i].ASC ? "ASC" : "DESC") + ", ");
                        }
                    }
                    builder.Append(" )");
                }
            }
            if (builder2.Length > 0)
            {
                builder2.Remove(builder2.Length - 1, 1);
                builder.Append(" ORDER BY " + builder2.ToString());
            }
            sql = builder.ToString();
        }

        protected void UpdateCommand(DatabaseParameters keys, DatabaseParameters values, string table)
        {
            int num;
            StringBuilder builder = new StringBuilder("UPDATE " + table + " SET ");
            if (values != null)
            {
                for (num = 0; num < values.Count; num++)
                {
                    builder.Append(values[num].FieldName + " = " + (((values[num].ParamType == DBDataType.Formula) || (values[num].ParamType == DBDataType.SQLFormula)) ? values[num].FieldValue : ((values[num].FieldValue.Length > 0) ? ((values[num].IsUnicode ? "N" : "") + "'" + values[num].FieldValue + "'") : (values[num].IsNullable ? "NULL" : "''"))) + ",");
                }
            }
            builder.Remove(builder.Length - 1, 1);
            if (keys != null)
            {
                for (num = 0; num < keys.Count; num++)
                {
                    builder.Append((num == 0) ? " WHERE " : (" " + keys[num].ParamLinkage.ToString() + " "));
                    builder.Append(((keys[num].ParamType == DBDataType.Formula) || (keys[num].ParamType == DBDataType.SQLFormula)) ? (keys[num].FieldName + " = " + keys[num].FieldValue) : (keys[num].FieldName + " = '" + keys[num].FieldValue + "'"));
                }
            }
            sql = builder.ToString();
        }

        private static DataSet CreateDataSet(SqlCommand command)
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                return dataSet;
            }
        }

        private static DataTable CreateDataTable(SqlCommand command)
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
        #endregion

        #region public methods
        public static DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteDataSet(connectionString, commandType, commandText, (DbParameter[])null);
        }

        public static DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText, params DbParameter[] parameters)
        {
            if (Transaction.Current == null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = CreateCommand(connection, commandType, commandText, parameters))
                    {
                        return CreateDataSet(command);
                    }
                }
            }
            using (SqlCommand command2 = CreateCommand(GetTransactionSqlConnection(connectionString), commandType, commandText, parameters))
            {
                return CreateDataSet(command2);
            }
        }

        public static void ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
        {
            ExecuteNonQuery(connectionString, commandType, commandText, (DbParameter[])null);
        }

        public static void ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params DbParameter[] parameters)
        {
            if (Transaction.Current == null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = CreateCommand(connection, commandType, commandText, parameters))
                    {
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (SqlException sqlEx)
                        {
                            throw sqlEx;
                        }
                    }
                    return;
                }
            }
            using (SqlCommand command2 = CreateCommand(GetTransactionSqlConnection(connectionString), commandType, commandText, parameters))
            {
                command2.ExecuteNonQuery();
            }
        }

        public static IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteReader(connectionString, commandType, commandText, (DbParameter[])null);
        }

        public static IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params DbParameter[] parameters)
        {
            if (Transaction.Current == null)
            {
                SqlConnection connection = new SqlConnection(connectionString);
                using (SqlCommand command = CreateCommand(connection, commandType, commandText, parameters))
                {
                    return command.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
            using (SqlCommand command2 = CreateCommand(GetTransactionSqlConnection(connectionString), commandType, commandText, parameters))
            {
                return command2.ExecuteReader();
            }
        }

        public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params DbParameter[] parameters)
        {
            if (Transaction.Current == null)
            {
                SqlConnection connection = new SqlConnection(connectionString);
                using (SqlCommand command = CreateCommand(connection, commandType, commandText, parameters))
                {
                    return command.ExecuteScalar();
                }
            }
            using (SqlCommand command2 = CreateCommand(GetTransactionSqlConnection(connectionString), commandType, commandText, parameters))
            {
                return command2.ExecuteScalar();
            }
        }

        public static bool ExecuteQuery(ArrayList sqla, string connectionString)
        {
            bool flag = true;

            if (Transaction.Current == null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();
                    SqlCommand command = connection.CreateCommand();
                    command.Connection = connection;
                    command.Transaction = transaction;
                    try
                    {
                        // convert sql ArrayList to sql string array
                        string[] sql = new string[sqla.Count];
                        for (int i = 0; i < sqla.Count; i++)
                        {
                            sql[i] = sqla[i].ToString();
                        }
                        // ExcuteNonQuery
                        for (int i = 0; i < sql.Length; i++)
                        {
                            command.CommandText = sql[i];
                            command.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch (SqlException exception)
                    {
                        errmsg = exception.Message;
                        flag = false;
                        transaction.Rollback();
                    }
                    catch (Exception exception2)
                    {
                        errmsg = exception2.Message;
                        flag = false;
                        transaction.Rollback();
                    }
                    transaction = null;
                    command = null;
                }
            }
            return flag;
        }




        #endregion
    }
}

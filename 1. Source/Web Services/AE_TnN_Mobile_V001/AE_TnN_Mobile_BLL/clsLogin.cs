using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using AE_TnN_Mobile_Common;
using System.Data;
using SAP.Admin.DAO;

namespace AE_TnN_Mobile_BLL
{
    public class clsLogin
    {
        clsLog oLog = new clsLog();

        SAPbobsCOM.Company oDICompany;

        public Int16 p_iDebugMode = DEBUG_ON;

        public const Int16 RTN_SUCCESS = 1;
        public const Int16 RTN_ERROR = 0;
        public const Int16 DEBUG_ON = 1;
        public const Int16 DEBUG_OFF = 0;
        public string sErrDesc = string.Empty;

        public static string ConnectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public static string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

        public DataSet SPA_ValidateUser(string sUserName, string sPassword, string sCategory)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "SPA_ValidateUser()";
                sProcName = "AE_SPA001_Mobile_ValidateUser";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);


                oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName,
                    Data.CreateParameter("@UserName", sUserName), Data.CreateParameter("@Password", sPassword), Data.CreateParameter("@Category", sCategory));
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS  ", sFuncName);
                if (oDataset.Tables.Count > 0 && oDataset != null)
                {
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("There is a set of data from the SP :" + sProcName, sFuncName);
                    return oDataset;
                }
                else
                {
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("There is no data from the SP :" + sProcName, sFuncName);
                    return oDataset;
                }

            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw Ex;
            }
        }

        public SAPbobsCOM.Company ConnectToTargetCompany(string sCompanyDB)
        {
            string sFuncName = string.Empty;
            string sReturnValue = string.Empty;
            DataSet oDTCompanyList = new DataSet();
            DataSet oDSResult = new DataSet();
            //SAPbobsCOM.Company oDICompany = new SAPbobsCOM.Company();
            string sConnString = string.Empty;
            DataView oDTView = new DataView();
            string[] MyArr = new string[7];

            try
            {
                sFuncName = "ConnectToTargetCompany()";

                //oSessionCompany = oSessionCompany +  sSessionUserName;
                // SAPbobsCOM.Company Convert.ToString(Session["sLoginUserName"]);
                //SAPbobsCOM.Company = sSessionUserName + oSessionCompany;
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                // oDICompany = (SAPbobsCOM.Company)Session["SAPCompany"];

                MyArr = ConnString.Split(';');
                string DatabaseName, SQLUser, SQLPwd, SQLServer, LicenseServer, DbUserName, DbPassword = string.Empty;
                SQLServer = MyArr[0].ToString();
                DatabaseName = MyArr[1].ToString();
                SQLUser = MyArr[2].ToString();
                SQLPwd = MyArr[3].ToString();
                LicenseServer = MyArr[4].ToString();
                DbUserName = MyArr[5].ToString();
                DbPassword = MyArr[6].ToString();

                if (oDICompany != null)
                {
                    if (oDICompany.CompanyDB == sCompanyDB)
                    {
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("ODICompany Name " + oDICompany.CompanyDB, sFuncName);
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("SCompanyDB " + sCompanyDB, sFuncName);
                        return oDICompany;
                    }

                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling ConnectToTargetCompany() ", sFuncName);

                sConnString = ConnectionString;

                oDICompany = ConnectToTargetCompany(oDICompany, DbUserName, DbPassword
                                   , DatabaseName, SQLServer, LicenseServer
                                   , SQLUser, SQLPwd, sErrDesc);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS  ", sFuncName);

                //SAPCompany(oDIComapny, sConnString);

                return oDICompany;
            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw Ex;
            }

        }

        public SAPbobsCOM.Company ConnectToTargetCompany(SAPbobsCOM.Company oCompany, string sUserName, string sPassword, string sDBName,
                                                        string sServer, string sLicServerName, string sDBUserName
                                                       , string sDBPassword, string sErrDesc)
        {
            string sFuncName = string.Empty;
            //SAPbobsCOM.Company oCompany = new SAPbobsCOM.Company();
            long lRetCode;

            try
            {
                sFuncName = "ConnectToTargetCompany()";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (oCompany != null)
                {
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Disconnecting the Company object - Company Name " + oCompany.CompanyName, sFuncName);
                    oCompany.Disconnect();
                }
                oCompany = new SAPbobsCOM.Company();
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Initializing Company Connection ", sFuncName);
                oCompany.Server = sServer;
                oCompany.LicenseServer = sLicServerName;
                oCompany.DbUserName = sDBUserName;
                oCompany.DbPassword = sDBPassword;
                oCompany.language = SAPbobsCOM.BoSuppLangs.ln_English;
                oCompany.UseTrusted = false;
                oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012;


                oCompany.CompanyDB = sDBName;// sDataBaseName;
                oCompany.UserName = sUserName;
                oCompany.Password = sPassword;

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Connecting the Database...", sFuncName);

                lRetCode = oCompany.Connect();

                if (lRetCode != 0)
                {

                    throw new ArgumentException(oCompany.GetLastErrorDescription());
                }
                else
                {
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Company Connection Established", sFuncName);
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS", sFuncName);
                    return oCompany;
                }

            }
            catch (Exception Ex)
            {

                sErrDesc = Ex.Message.ToString();
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw Ex;
            }
        }

        public Int32 ChangePassword(DataSet oDTCompanyList, string sCompany, string sUserName, string sNewPassword)
        {
            int iReturnResult = 0;
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sQuery = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "ChangePassword()";
                sQuery = "UPDATE OUSR SET U_MobilePwd = @MobilePwd WHERE USER_CODE = @UserName ; Select @@ROWCOUNT as ReturnResult";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sQuery, sFuncName);

                if (oDTCompanyList != null && oDTCompanyList.Tables.Count > 0)
                {
                    oDTView = oDTCompanyList.Tables[0].DefaultView;

                    oDTView.RowFilter = "U_DBName= '" + sCompany + "'";
                    if (oDTView != null && oDTView.Count > 0)
                    {
                        oDataset = SqlHelper.ExecuteDataSet(oDTView[0]["U_ConnString"].ToString(), CommandType.Text, sQuery,
                            Data.CreateParameter("@MobilePwd", sNewPassword), Data.CreateParameter("@UserName", sUserName));
                        iReturnResult = Convert.ToInt32(oDataset.Tables[0].Rows[0][0]);
                        if (iReturnResult > 0)
                        {
                            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS  ", sFuncName);
                        }
                    }
                    else
                    {
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("No Data in OUSR table for the selected Company", sFuncName);
                        return iReturnResult;
                    }
                }
                else
                {
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("There is No Company List in the Holding Company ", sFuncName);
                    return iReturnResult;
                }

                return iReturnResult;
            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw Ex;
            }
        }
    }
}

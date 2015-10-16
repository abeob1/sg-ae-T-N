using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AE_TnN_Mobile_Common;
using System.Configuration;
using System.Data;
using SAP.Admin.DAO;
using System.Data.SqlClient;

namespace AE_TnN_Mobile_BLL
{
    public class clsDashboard
    {
        clsLog oLog = new clsLog();

        SAPbobsCOM.Company oDICompany;

        public Int16 p_iDebugMode = DEBUG_ON;

        public const Int16 RTN_SUCCESS = 1;
        public const Int16 RTN_ERROR = 0;
        public const Int16 DEBUG_ON = 1;
        public const Int16 DEBUG_OFF = 0;
        public string sErrDesc = string.Empty;
        clsLogin oLogin = new clsLogin();
        //SAPbobsCOM.Company oDICompany;

        public static string ConnectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public static string Category = ConfigurationManager.AppSettings["Category"].ToString();

        public DataSet SPA_DashboardInfo(string sAppType, string sUserCode)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "SPA_DashboardInfo()";
                sProcName = "AE_SPA002_Mobile_GetDashboardInfo";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);


                oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName,
                    Data.CreateParameter("@AppType", sAppType), Data.CreateParameter("@UserCode", sUserCode), Data.CreateParameter("@currentDate", DateTime.Now.Date));
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

        public DataSet SPA_CaseInfo(string sFiltertype, string sCasetype, string sCasestatus, string sUsercode, string sDateOpenFr, string sDateOpenTo, string sCaseFileNoFr, string sCaseFileNoTo, string
                    sClientName, string sCaseAmtFr, string sCaseAmtTo, string sDateClosedFr, string sDateClosedTo)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "SPA_CaseInfo()";
                sProcName = "AE_SPA003_Mobile_ListofCases";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);


                oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName,
                            Data.CreateParameter("@Filtertype", sFiltertype),
                            Data.CreateParameter("@Casetype", sCasetype),
                            Data.CreateParameter("@Casestatus", sCasestatus),
                            Data.CreateParameter("@Usercode", sUsercode),
                            Data.CreateParameter("@DateOpenFr", sDateOpenFr),
                            Data.CreateParameter("@DateOpenTo", sDateOpenTo),
                            Data.CreateParameter("@CaseFileNoFr", sCaseFileNoFr),
                            Data.CreateParameter("@CaseFileNoTo", sCaseFileNoTo),
                            Data.CreateParameter("@ClientName", sClientName),
                            Data.CreateParameter("@CaseAmtFr", sCaseAmtFr),
                            Data.CreateParameter("@CaseAmtTo", sCaseAmtTo),
                            Data.CreateParameter("@DateClosedFr", sDateClosedFr),
                            Data.CreateParameter("@DateClosedTo", sDateClosedTo));
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

        public DataSet SPA_ListofPropertyEnquiry(string sTITLETYPE, string sTITLENO, string sLOTTYPE, string sLOT_NO, string sFORMERLY_KNOWN_AS, string sBPM, string sSTATE, string sAREA)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "SPA_ListofPropertyEnquiry()";
                sProcName = "AE_SPA004_Mobile_ListofPropertyEnquiry";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);


                oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName,
                            Data.CreateParameter("@TITLETYPE", sTITLETYPE),
                            Data.CreateParameter("@TITLENO", sTITLENO),
                            Data.CreateParameter("@LOTTYPE", sLOTTYPE),
                            Data.CreateParameter("@LOT_NO", sLOT_NO),
                            Data.CreateParameter("@FORMERLY_KNOWN_AS", sFORMERLY_KNOWN_AS),
                            Data.CreateParameter("@BPM", sBPM),
                            Data.CreateParameter("@STATE", sSTATE),
                            Data.CreateParameter("@AREA", sAREA));
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

        public DataSet SPA_PropertyEnquiryDetails(string sCode, string sCategory)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "SPA_PropertyEnquiryDetails()";
                sProcName = "AE_SPA005_Mobile_PropertyEnquiryDetails";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);


                oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName,
                            Data.CreateParameter("@Code", sCode),
                            Data.CreateParameter("@Category", sCategory));

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

        public DataSet SPA_RelatedCases(string sPropertyCode, string sRelatedPartyCode, string sCallFrom, string sCategory)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "SPA_RelatedCases()";
                sProcName = "AE_SPA006_Mobile_RelatedCases";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);


                oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName,
                            Data.CreateParameter("@PropertyCode", sPropertyCode),
                            Data.CreateParameter("@RelatedPartyCode", sRelatedPartyCode),
                            Data.CreateParameter("@CallFrom", sCallFrom),
                            Data.CreateParameter("@Category", sCategory));

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

        public DataTable SPA_GetProject()
        {
            string sFuncName = string.Empty;
            string sReturnResult = string.Empty;
            DataTable results = new DataTable();
            try
            {
                sFuncName = "SPA_GetProject";
                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand command = con.CreateCommand();
                command.CommandText = "DECLARE @DATE date " +
                                      "SET @DATE = (SELECT GETDATE()) " +
                                      "SELECT '-- Select --' [Id], '-- Select --' [Name] UNION " +
                                      "SELECT PrjCode [Id],PrjName [Name] FROM OPRJ WHERE ISNULL(Locked,'N') != 'Y' AND Active = 'Y' " +
                                      "AND IsNull(ValidFrom,@DATE) <= @DATE and ISNULL(ValidTo,@DATE) >= @DATE ";
                con.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(results);
                con.Close();
            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw Ex;
            }

            return results;
        }

        public DataSet SPA_Property_GetDropdownValues()
        {
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            string sReturnResult = string.Empty;
            DataSet oDataset = new DataSet();
            try
            {
                sFuncName = "SPA_Property_GetDropdownValues";
                sProcName = "AE_SPA010_Mobile_Property_GetDropdownValues";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);


                oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName);

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

        public string SPA_AddPropertyEnquiry(DataTable dtDatatable)
        {
            string sFuncName = string.Empty;
            string sReturnResult = string.Empty;
            //int sCode = 1;
            string sCode = string.Empty;
            try
            {
                sFuncName = "SPA_AddPropertyEnquiry";

                DataTable results = new DataTable();
                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand command = con.CreateCommand();
                //command.CommandText = "select Count(CODE) from [@AE_PROPERTY]";
                command.CommandText = "SELECT REPLICATE('0', (12-LEN(ISNULL(MAX(SUBSTRING(Code,4,LEN(Code)))+1,1)))) " +
                                        " + CONVERT(VARCHAR, ISNULL(MAX(SUBSTRING(Code,4,LEN(Code)))+1,1)) [Code] FROM [@AE_PROPERTY]";
                con.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(results);

                if (results.Rows[0][0].ToString().Length > 0)
                {
                    //sCode = Convert.ToInt32(results.Rows[0][0].ToString()) + 1;
                    sCode = results.Rows[0][0].ToString();
                }

                con.Close();

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Connecting to target company ", sFuncName);
                oDICompany = oLogin.ConnectToTargetCompany(ConnectionString);

                SAPbobsCOM.GeneralService oGeneralService = null;
                SAPbobsCOM.GeneralData oGeneralData;
                SAPbobsCOM.CompanyService oCompanyService = oDICompany.GetCompanyService();
                oGeneralService = oCompanyService.GetGeneralService("PROPERTY");

                oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);

                //Adding the Informations
                oGeneralData.SetProperty("Code", sCode.ToString());
                oGeneralData.SetProperty("U_TITLETYPE", dtDatatable.Rows[0]["TITLETYPE"].ToString());
                oGeneralData.SetProperty("U_TITLENO", dtDatatable.Rows[0]["TITLENO"].ToString());
                oGeneralData.SetProperty("U_LOTTYPE", dtDatatable.Rows[0]["LOTTYPE"].ToString());
                oGeneralData.SetProperty("U_LOTNO", dtDatatable.Rows[0]["LOTNO"].ToString());
                oGeneralData.SetProperty("U_FORMERLY_KNOWN_AS", dtDatatable.Rows[0]["FORMERLY_KNOWN_AS"].ToString());
                oGeneralData.SetProperty("U_BPM", dtDatatable.Rows[0]["BPM"].ToString());
                oGeneralData.SetProperty("U_STATE", dtDatatable.Rows[0]["STATE"].ToString());
                oGeneralData.SetProperty("U_AREA", dtDatatable.Rows[0]["AREA"].ToString());
                oGeneralData.SetProperty("U_LOTAREA", dtDatatable.Rows[0]["LOTAREA"].ToString());
                //oGeneralData.SetProperty("U_LASTUPDATEDON", dtDatatable.Rows[0]["LASTUPDATEDON"].ToString());
                oGeneralData.SetProperty("U_DEVELOPER", dtDatatable.Rows[0]["DEVELOPER"].ToString());
                oGeneralData.SetProperty("U_DVLPR_CODE", dtDatatable.Rows[0]["DVLPR_CODE"].ToString());
                oGeneralData.SetProperty("U_PROJECT_CODE", dtDatatable.Rows[0]["PROJECT_CODE"].ToString());
                oGeneralData.SetProperty("U_PROJECTNAME", dtDatatable.Rows[0]["PROJECTNAME"].ToString());
                oGeneralData.SetProperty("U_DEVLICNO", dtDatatable.Rows[0]["DEVLICNO"].ToString());
                oGeneralData.SetProperty("U_DEVSOLICTOR", dtDatatable.Rows[0]["DEVSOLICTOR"].ToString());
                oGeneralData.SetProperty("U_DVLPR_SOL_CODE", dtDatatable.Rows[0]["DVLPR_SOL_CODE"].ToString());
                oGeneralData.SetProperty("U_DVLPR_LOC", dtDatatable.Rows[0]["DVLPR_LOC"].ToString());
                oGeneralData.SetProperty("U_LSTCHG_BANKCODE", dtDatatable.Rows[0]["LSTCHG_BANKCODE"].ToString());
                oGeneralData.SetProperty("U_LSTCHG_BANKNAME", dtDatatable.Rows[0]["LSTCHG_BANKNAME"].ToString());
                oGeneralData.SetProperty("U_LSTCHG_BRANCH", dtDatatable.Rows[0]["LSTCHG_BRANCH"].ToString());
                oGeneralData.SetProperty("U_LSTCHG_PANO", dtDatatable.Rows[0]["LSTCHG_PANO"].ToString());
                oGeneralData.SetProperty("U_LSTCHG_PRSTNO", dtDatatable.Rows[0]["LSTCHG_PRSTNO"].ToString());

                oGeneralService.Add(oGeneralData);

                sReturnResult = "SUCCESS";
            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw Ex;
            }

            return sReturnResult;
        }

        public string SPA_EditPropertyEnquiry(DataTable dtDatatable)
        {
            string sFuncName = string.Empty;
            string sReturnResult = string.Empty;
            try
            {
                sFuncName = "SPA_EditPropertyEnquiry";
                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand command = con.CreateCommand();

                string[] TimeSplit = DateTime.Now.TimeOfDay.ToString().Split(':');

                //Updating the Informations
                command.CommandText = "UPDATE [@AE_PROPERTY] SET U_TITLENO = '" + dtDatatable.Rows[0]["TITLENO"] + "',"
                                        + "U_TITLETYPE = '" + dtDatatable.Rows[0]["TITLETYPE"] + "',"
                                        + "U_LOTTYPE = '" + dtDatatable.Rows[0]["LOTTYPE"] + "',"
                                        + "U_LOTNO = '" + dtDatatable.Rows[0]["LOTNO"] + "',"
                                        + "U_FORMERLY_KNOWN_AS = '" + dtDatatable.Rows[0]["FORMERLY_KNOWN_AS"] + "',"
                                        + "U_BPM = '" + dtDatatable.Rows[0]["BPM"] + "',"
                                        + "U_STATE = '" + dtDatatable.Rows[0]["STATE"] + "',"
                                        + "U_AREA = '" + dtDatatable.Rows[0]["AREA"] + "',"
                                        + "U_LOTAREA = '" + dtDatatable.Rows[0]["LOTAREA"] + "',"
                                        + "UpdateDate = '" + DateTime.Now.Date + "',"
                                        + "Updatetime = '" + TimeSplit[0] + TimeSplit[1] + "',"
                                        + "U_DEVELOPER = '" + dtDatatable.Rows[0]["DEVELOPER"] + "',"
                                        + "U_DVLPR_CODE = '" + dtDatatable.Rows[0]["DVLPR_CODE"] + "',"
                                        + "U_PROJECT_CODE = '" + dtDatatable.Rows[0]["PROJECT_CODE"] + "',"
                                        + "U_PROJECTNAME = '" + dtDatatable.Rows[0]["PROJECTNAME"] + "',"
                                        + "U_DEVLICNO = '" + dtDatatable.Rows[0]["DEVLICNO"] + "',"
                                        + "U_DEVSOLICTOR = '" + dtDatatable.Rows[0]["DEVSOLICTOR"] + "',"
                                        + "U_DVLPR_SOL_CODE = '" + dtDatatable.Rows[0]["DVLPR_SOL_CODE"] + "',"
                                        + "U_DVLPR_LOC = '" + dtDatatable.Rows[0]["DVLPR_LOC"] + "',"
                                        + "U_LSTCHG_BANKCODE = '" + dtDatatable.Rows[0]["LSTCHG_BANKCODE"] + "',"
                                        + "U_LSTCHG_BANKNAME = '" + dtDatatable.Rows[0]["LSTCHG_BANKNAME"] + "',"
                                        + "U_LSTCHG_BRANCH = '" + dtDatatable.Rows[0]["LSTCHG_BRANCH"] + "',"
                                        + "U_LSTCHG_PANO = '" + dtDatatable.Rows[0]["LSTCHG_PANO"] + "',"
                                        + "U_LSTCHG_PRSTNO = '" + dtDatatable.Rows[0]["LSTCHG_PRSTNO"] + "'"
                                        + " WHERE Code = '" + dtDatatable.Rows[0]["Code"] + "'";
                con.Open();

                command.ExecuteNonQuery();
                sReturnResult = "SUCCESS";

                con.Close();
            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw Ex;
            }

            return sReturnResult;
        }

        public DataSet SPA_CaseEnquiry(string sInput1, string sInput2, string sInput3, string sInput4, string sInput5, string sInput6, string sInput7)
        {
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            string sReturnResult = string.Empty;
            DataSet oDataset = new DataSet();
            try
            {
                sFuncName = "SPA_CaseEnquiry()";
                sProcName = "AE_ListofCases";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);


                oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName,
                            Data.CreateParameter("@Filtertype", string.Empty),
                            Data.CreateParameter("@Casetype", sInput4),
                            Data.CreateParameter("@Casestatus", string.Empty),
                            Data.CreateParameter("@Usercode", string.Empty),
                            Data.CreateParameter("@DateOpenFr", sInput1),
                            Data.CreateParameter("@DateOpenTo", sInput2),
                            Data.CreateParameter("@CaseFileNoFr", sInput3),
                            Data.CreateParameter("@CaseFileNoTo", string.Empty),
                            Data.CreateParameter("@ClientName", sInput5),
                            Data.CreateParameter("@CaseAmtFr", sInput6),
                            Data.CreateParameter("@CaseAmtTo", sInput7),
                            Data.CreateParameter("@DateClosedFr", string.Empty),
                            Data.CreateParameter("@DateClosedTo", string.Empty));

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

        public DataSet SPA_DashboardCaseButtonInfo(string sFilterType, string sCaseStatus, string sUserCode)
        {
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            string sReturnResult = string.Empty;
            DataSet oDataset = new DataSet();
            try
            {
                sFuncName = "SPA_OpenCase()";
                sProcName = "AE_ListofCases";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);


                oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName,
                            Data.CreateParameter("@Filtertype", sFilterType),
                            Data.CreateParameter("@Casetype", Category),
                            Data.CreateParameter("@Casestatus", sCaseStatus),
                            Data.CreateParameter("@Usercode", sUserCode),
                            Data.CreateParameter("@DateOpenFr", string.Empty),
                            Data.CreateParameter("@DateOpenTo", string.Empty),
                            Data.CreateParameter("@CaseFileNoFr", string.Empty),
                            Data.CreateParameter("@CaseFileNoTo", string.Empty),
                            Data.CreateParameter("@ClientName", string.Empty),
                            Data.CreateParameter("@CaseAmtFr", string.Empty),
                            Data.CreateParameter("@CaseAmtTo", string.Empty),
                            Data.CreateParameter("@DateClosedFr", string.Empty),
                            Data.CreateParameter("@DateClosedTo", string.Empty));

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
    }
}

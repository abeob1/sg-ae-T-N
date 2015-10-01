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
    public class clsWalkin
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

        public DataSet SPA_GetValidValues(string sTableName, string sFieldName)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "SPA_GetValidValues()";
                sProcName = "AE_SPA008_Mobile_GetValidValues";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);


                oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName,
                    Data.CreateParameter("@TableName", sTableName), Data.CreateParameter("@FieldName", sFieldName));
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

        public DataSet SPA_GetValidValuesNames(string sTableName, string sFieldName)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "SPA_GetValidValuesNames()";
                sProcName = "AE_SPA009_Mobile_GetValidValueNames";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);


                oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName,
                    Data.CreateParameter("@TableName", sTableName), Data.CreateParameter("@FieldName", sFieldName));
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

        public DataSet SPA_IndividualSearch(string sFullName, string sMobileNum, string sIDNum, string sCategory)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "SPA_IndividualSearch()";
                sProcName = "AE_SPA007_Mobile_IndividualSearch";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);


                oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName,
                    Data.CreateParameter("@FullName", sFullName), Data.CreateParameter("@MobileNum", sMobileNum), Data.CreateParameter("@IDNum", sIDNum)
                    , Data.CreateParameter("@Category", sCategory));
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

        public DataSet SPA_CorporateSearch(string sCompanyName, string sRegNum, string sAddress, string sCategory)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "SPA_CorporateSearch()";
                sProcName = "AE_SPA011_Mobile_CorporateSearch";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);


                oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName,
                    Data.CreateParameter("@CompanyName", sCompanyName), Data.CreateParameter("@RegNum", sRegNum), Data.CreateParameter("@Address", sAddress)
                    , Data.CreateParameter("@Category", sCategory));
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

        public string SPA_AddIndividual(DataTable dtDatatable)
        {
            string sFuncName = string.Empty;
            string sReturnResult = string.Empty;
            string sCode = string.Empty;
            try
            {
                sFuncName = "SPA_AddIndividual";

                DataTable results = new DataTable();
                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand command = con.CreateCommand();
                command.CommandText = "SELECT REPLICATE('0', (12-LEN(ISNULL(MAX(SUBSTRING(Code,4,LEN(Code)))+1,1)))) " +
                                        " + CONVERT(VARCHAR, ISNULL(MAX(SUBSTRING(Code,4,LEN(Code)))+1,1)) [Code] FROM [@AE_RELATEDPARTY]";
                con.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(results);

                if (results.Rows[0][0].ToString().Length > 0)
                {
                    sCode = results.Rows[0][0].ToString();
                }

                con.Close();

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Connecting to target company ", sFuncName);
                oDICompany = oLogin.ConnectToTargetCompany(ConnectionString);

                SAPbobsCOM.GeneralService oGeneralService = null;
                SAPbobsCOM.GeneralData oGeneralData;
                SAPbobsCOM.CompanyService oCompanyService = oDICompany.GetCompanyService();
                oGeneralService = oCompanyService.GetGeneralService("RELATEDPARTY");

                oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);

                string[] sIDSplitUp = dtDatatable.Rows[0]["IDNo1"].ToString().Split('-');
                string sIDNo2 = dtDatatable.Rows[0]["IDNo1"].ToString();
                string sAddressId = dtDatatable.Rows[0]["IDAddress1"].ToString() + System.Environment.NewLine + dtDatatable.Rows[0]["IDAddress2"].ToString() + System.Environment.NewLine
                                    + dtDatatable.Rows[0]["IDAddress3"].ToString() + System.Environment.NewLine +
                                    dtDatatable.Rows[0]["IDAddress4"].ToString() + System.Environment.NewLine + dtDatatable.Rows[0]["IDAddress5"].ToString();
                string sCorrespondAddressId = dtDatatable.Rows[0]["CorresAddr1"].ToString() + System.Environment.NewLine + dtDatatable.Rows[0]["CorresAddr2"].ToString() + System.Environment.NewLine
                                    + dtDatatable.Rows[0]["CorresAddr3"].ToString() + System.Environment.NewLine +
                                    dtDatatable.Rows[0]["CorresAddr4"].ToString() + System.Environment.NewLine + dtDatatable.Rows[0]["CorresAddr5"].ToString();

                //Adding the Informations
                oGeneralData.SetProperty("Code", sCode);
                //oGeneralData.SetProperty("DocEntry", dtDatatable.Rows[0]["DocEntry"].ToString());
                oGeneralData.SetProperty("U_NAME", dtDatatable.Rows[0]["EmployeeName"].ToString());
                oGeneralData.SetProperty("U_INDIVIDUAL_TITLE", dtDatatable.Rows[0]["Title"].ToString());
                oGeneralData.SetProperty("U_GENDER", dtDatatable.Rows[0]["Gender"].ToString());
                oGeneralData.SetProperty("U_IDNO_F1", dtDatatable.Rows[0]["IDNo1"].ToString());
                oGeneralData.SetProperty("U_IDNO_F2", sIDNo2.Replace("-", ""));
                oGeneralData.SetProperty("U_IDNO_F3", dtDatatable.Rows[0]["IDNo3"].ToString());
                if (sIDSplitUp.Length > 0)
                {
                    if (sIDSplitUp.Length >= 1)
                    {
                        oGeneralData.SetProperty("U_IDSEC1", sIDSplitUp[0]);
                    }
                    if (sIDSplitUp.Length >= 2)
                    {
                        oGeneralData.SetProperty("U_IDSEC2", sIDSplitUp[1]);
                    }
                    if (sIDSplitUp.Length >= 3)
                    {
                        oGeneralData.SetProperty("U_IDSEC3", sIDSplitUp[2]);
                    }
                }
                oGeneralData.SetProperty("U_IDTYPE", "INDIVIDUAL");
                oGeneralData.SetProperty("U_TAXNOFORMAT1", dtDatatable.Rows[0]["TaxNo"].ToString());
                oGeneralData.SetProperty("U_CONTACT_MOBILE", dtDatatable.Rows[0]["MobileNo"].ToString());
                oGeneralData.SetProperty("U_CONTACT_TELEPHONE", dtDatatable.Rows[0]["Telephone"].ToString());
                oGeneralData.SetProperty("U_SYARIKATNO", dtDatatable.Rows[0]["OfficeNo"].ToString());
                oGeneralData.SetProperty("U_ADDSEG1", dtDatatable.Rows[0]["IDAddress1"].ToString());
                oGeneralData.SetProperty("U_ADDSEG2", dtDatatable.Rows[0]["IDAddress2"].ToString());
                oGeneralData.SetProperty("U_ADDSEG3", dtDatatable.Rows[0]["IDAddress3"].ToString());
                oGeneralData.SetProperty("U_ADDSEG4", dtDatatable.Rows[0]["IDAddress4"].ToString());
                oGeneralData.SetProperty("U_ADDSEG5", dtDatatable.Rows[0]["IDAddress5"].ToString());
                oGeneralData.SetProperty("U_ADDRESS_ID", sAddressId);
                oGeneralData.SetProperty("U_CADDSEG1", dtDatatable.Rows[0]["CorresAddr1"].ToString());
                oGeneralData.SetProperty("U_CADDSEG2", dtDatatable.Rows[0]["CorresAddr2"].ToString());
                oGeneralData.SetProperty("U_CADDSEG3", dtDatatable.Rows[0]["CorresAddr3"].ToString());
                oGeneralData.SetProperty("U_CADDSEG4", dtDatatable.Rows[0]["CorresAddr4"].ToString());
                oGeneralData.SetProperty("U_CADDSEG5", dtDatatable.Rows[0]["CorresAddr5"].ToString());
                oGeneralData.SetProperty("U_ADDRESS_CORRESPOND", sCorrespondAddressId);
                oGeneralData.SetProperty("U_ADDRESS_TOUSE", dtDatatable.Rows[0]["AddressToUse"].ToString());
                oGeneralData.SetProperty("U_BANK", "N");
                oGeneralData.SetProperty("U_DEVELOPER", "N");
                oGeneralData.SetProperty("U_SOLICITOR", "N");
                //oGeneralData.SetProperty("LastUpdatedOn", dtDatatable.Rows[0]["LastUpdatedOn"].ToString());

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

        public string SPA_EditIndividual(DataTable dtDatatable)
        {
            string sFuncName = string.Empty;
            string sReturnResult = string.Empty;
            string sCode = string.Empty;
            try
            {
                sFuncName = "SPA_EditIndividual";
                string sId1 = string.Empty, sId2 = string.Empty, sId3 = string.Empty;
                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand command = con.CreateCommand();

                string[] TimeSplit = DateTime.Now.TimeOfDay.ToString().Split(':');
                string[] sIDSplitUp = dtDatatable.Rows[0]["IDNo1"].ToString().Split('-');

                string sIDNo2 = dtDatatable.Rows[0]["IDNo1"].ToString();
                string sAddressId = dtDatatable.Rows[0]["IDAddress1"].ToString() + System.Environment.NewLine + dtDatatable.Rows[0]["IDAddress2"].ToString() + System.Environment.NewLine
                                    + dtDatatable.Rows[0]["IDAddress3"].ToString() + System.Environment.NewLine +
                                    dtDatatable.Rows[0]["IDAddress4"].ToString() + System.Environment.NewLine + dtDatatable.Rows[0]["IDAddress5"].ToString();
                string sCorrespondAddressId = dtDatatable.Rows[0]["CorresAddr1"].ToString() + System.Environment.NewLine + dtDatatable.Rows[0]["CorresAddr2"].ToString() + System.Environment.NewLine
                                    + dtDatatable.Rows[0]["CorresAddr3"].ToString() + System.Environment.NewLine +
                                    dtDatatable.Rows[0]["CorresAddr4"].ToString() + System.Environment.NewLine + dtDatatable.Rows[0]["CorresAddr5"].ToString();

                if (sIDSplitUp.Length > 0)
                {
                    if (sIDSplitUp.Length >= 1)
                    {
                        sId1 = sIDSplitUp[0].ToString();
                    }
                    if (sIDSplitUp.Length >= 2)
                    {
                        sId2 = sIDSplitUp[1].ToString();
                    }
                    if (sIDSplitUp.Length >= 3)
                    {
                        sId3 = sIDSplitUp[2].ToString();
                    }
                }

                //Updating the Informations

                command.CommandText = "UPDATE [@AE_RELATEDPARTY] SET U_NAME = '" + dtDatatable.Rows[0]["EmployeeName"] + "',"
                + "U_INDIVIDUAL_TITLE= '" + dtDatatable.Rows[0]["Title"] + "',"
                + "U_GENDER= '" + dtDatatable.Rows[0]["Gender"] + "',"
                + "U_IDNO_F1= '" + dtDatatable.Rows[0]["IDNo1"] + "',"
                + "U_IDNO_F2= '" + sIDNo2.Replace("-", "") + "',"
                + "U_IDNO_F3= '" + dtDatatable.Rows[0]["IDNo3"] + "',"
                + "U_IDSEC1= '" + sId1 + "',"
                + "U_IDSEC2= '" + sId2 + "',"
                + "U_IDSEC3= '" + sId3 + "',"
                + "U_IDTYPE= '" + "INDIVIDUAL" + "',"
                + "U_TAXNOFORMAT1= '" + dtDatatable.Rows[0]["TaxNo"] + "',"
                + "U_CONTACT_MOBILE= '" + dtDatatable.Rows[0]["MobileNo"] + "',"
                + "U_CONTACT_TELEPHONE= '" + dtDatatable.Rows[0]["Telephone"] + "',"
                + "U_SYARIKATNO= '" + dtDatatable.Rows[0]["OfficeNo"] + "',"
                + "U_ADDSEG1= '" + dtDatatable.Rows[0]["IDAddress1"] + "',"
                + "U_ADDSEG2= '" + dtDatatable.Rows[0]["IDAddress2"] + "',"
                + "U_ADDSEG3= '" + dtDatatable.Rows[0]["IDAddress3"] + "',"
                + "U_ADDSEG4= '" + dtDatatable.Rows[0]["IDAddress4"] + "',"
                + "U_ADDSEG5= '" + dtDatatable.Rows[0]["IDAddress5"] + "',"
                + "U_ADDRESS_ID= '" + sAddressId + "',"
                + "U_CADDSEG1= '" + dtDatatable.Rows[0]["CorresAddr1"] + "',"
                + "U_CADDSEG2= '" + dtDatatable.Rows[0]["CorresAddr2"] + "',"
                + "U_CADDSEG3= '" + dtDatatable.Rows[0]["CorresAddr3"] + "',"
                + "U_CADDSEG4= '" + dtDatatable.Rows[0]["CorresAddr4"] + "',"
                + "U_CADDSEG5= '" + dtDatatable.Rows[0]["CorresAddr5"] + "',"
                + "U_ADDRESS_CORRESPOND= '" + sCorrespondAddressId + "',"
                + "U_ADDRESS_TOUSE= '" + dtDatatable.Rows[0]["AddressToUse"] + "',"
                + "UpdateDate= '" + DateTime.Now.Date + "',"
                + "Updatetime = '" + TimeSplit[0] + TimeSplit[1] + "'"
                + " WHERE Code = '" + dtDatatable.Rows[0]["Code"] + "' AND DocEntry = '" + dtDatatable.Rows[0]["DocEntry"] + "'";

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

        public string SPA_NextLineAddressCheck(string sAddress1, string sAddress2, string sAddress3, string sAddress4, string sAddress5)
        {
            string sFuncName = string.Empty;
            string sReturnResult = string.Empty;
            string sCode = string.Empty;
            try
            {
                sFuncName = "SPA_TESTEDIT";
                string sId1 = string.Empty, sId2 = string.Empty, sId3 = string.Empty;
                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand command = con.CreateCommand();

                string sAddressEdit = sAddress1 + System.Environment.NewLine + sAddress2 + System.Environment.NewLine +
                    sAddress3 + System.Environment.NewLine + sAddress4 + System.Environment.NewLine + sAddress5;

                //Updating the Informations

                command.CommandText = "UPDATE [@AE_RELATEDPARTY] SET U_ADDRESS_ID = '" + sAddressEdit + "'"
                + " WHERE Code = '000000000001'";

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
    }
}

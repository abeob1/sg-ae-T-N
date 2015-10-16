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
    public class clsProcessCase
    {
        clsLog oLog = new clsLog();

        SAPbobsCOM.Company oDICompany;
        clsLogin oLogin = new clsLogin();

        public Int16 p_iDebugMode = DEBUG_ON;

        public const Int16 RTN_SUCCESS = 1;
        public const Int16 RTN_ERROR = 0;
        public const Int16 DEBUG_ON = 1;
        public const Int16 DEBUG_OFF = 0;
        public string sErrDesc = string.Empty;

        public static string ConnectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public static string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

        public DataSet SPA_ProcessCase_GetDataFromOCRD(string sCaseNo, string sUserName, string sUserRole)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "SPA_ProcessCase_GetDataFromOCRD()";
                sProcName = "AE_SPA023_Mobile_ProcessCase_GetDataFromOCRD";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);


                oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName,
                    Data.CreateParameter("@CaseNo", sCaseNo), Data.CreateParameter("@UserName", sUserName), Data.CreateParameter("@UserRole", sUserRole));
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

        public DataTable SPA_ProcessCase_GetIDType()
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            string sReturnResult = string.Empty;
            DataTable results = new DataTable();
            try
            {
                sFuncName = "SPA_ProcessCase_GetIDType";
                sProcName = "AE_SPA024_Mobile_ProcessCase_IDType";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);


                oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS  ", sFuncName);
                if (oDataset.Tables.Count > 0 && oDataset != null)
                {
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("There is a set of data from the SP :" + sProcName, sFuncName);
                    return oDataset.Tables[0];
                }
                else
                {
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("There is no data from the SP :" + sProcName, sFuncName);
                    return new DataTable();
                }
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

        public string SPA_ProcessCase_UpdateCaseTabDetails(string sTabId, DataTable dtHeader, DataTable dtDetails, DataTable dtPurchaser, DataTable dtVendor, DataTable dtProperty,
                                                            DataTable dtLoanPrinciple, DataTable dtLoanSubsidary)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();
            string sResult = string.Empty;
            string sCardCode = string.Empty;
            string sDocEntry = string.Empty;
            double lRetCode;
            try
            {
                sFuncName = "SPA_ProcessCase_UpdateCaseTabDetails()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                // Note : Based on the Header table TabId, need to Update the data's in SAP.
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Connecting to target company ", sFuncName);
                oDICompany = oLogin.ConnectToTargetCompany(ConnectionString);

                //DataRow drHeader = dtHeader.Rows[0];
                sCardCode = dtHeader.Rows[0]["Case"].ToString();
                SAPbobsCOM.BusinessPartners oBP = oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);

                if (sTabId == "2")// Update Purchaser tab details
                {
                    DataRow dr = dtPurchaser.Rows[0];
                    oBP.GetByKey(sCardCode);

                    oBP.UserFields.Fields.Item("U_SPADATE").Value = dr["PurSPADate"];
                    oBP.UserFields.Fields.Item("U_PRVTCAVEATENTRYDT").Value = dr["PurEntryOfPrivateCaveat"];
                    oBP.UserFields.Fields.Item("U_PRVTCAVEATWITHDWDT").Value = dr["PurWithOfPrivateCaveat"];
                    oBP.UserFields.Fields.Item("U_PURCH_RP_NAME1").Value = dr["PurFirstName"];
                    oBP.UserFields.Fields.Item("U_PURCH_RP_ID1").Value = dr["PurFirstID"];
                    oBP.UserFields.Fields.Item("U_PURCH_RP_TAX1").Value = dr["PurFirstTaxNo"];
                    oBP.UserFields.Fields.Item("U_PURCH_CONTACT1").Value = dr["PurFirstContactNo"];
                    oBP.UserFields.Fields.Item("U_PURCH_IDTYPE1").Value = dr["PurFirstType"];
                    oBP.UserFields.Fields.Item("U_PURCH_RP_NAME2").Value = dr["PurSecName"];
                    oBP.UserFields.Fields.Item("U_PURCH_RP_ID2").Value = dr["PurSecID"];
                    oBP.UserFields.Fields.Item("U_PURCH_RP_TAX2").Value = dr["PurSecTaxNo"];
                    oBP.UserFields.Fields.Item("U_PURCH_CONTACT2").Value = dr["PurSecContactNo"];
                    oBP.UserFields.Fields.Item("U_PURCH_IDTYPE2").Value = dr["PurSecType"];
                    oBP.UserFields.Fields.Item("U_PURCH_RP_NAME3").Value = dr["PurThirdName"];
                    oBP.UserFields.Fields.Item("U_PURCH_RP_ID3").Value = dr["PurThirdID"];
                    oBP.UserFields.Fields.Item("U_PURCH_RP_TAX3").Value = dr["PurThirdTaxNo"];
                    oBP.UserFields.Fields.Item("U_PURCH_CONTACT3").Value = dr["PurThirdContactNo"];
                    oBP.UserFields.Fields.Item("U_PURCH_IDTYPE3").Value = dr["PurThirdType"];
                    oBP.UserFields.Fields.Item("U_PURCH_RP_NAME4").Value = dr["PurFourthName"];
                    oBP.UserFields.Fields.Item("U_PURCH_RP_ID4").Value = dr["PurFourthID"];
                    oBP.UserFields.Fields.Item("U_PURCH_RP_TAX4").Value = dr["PurFourthTaxNo"];
                    oBP.UserFields.Fields.Item("U_PURCH_CONTACT4").Value = dr["PurFourthContactNo"];
                    oBP.UserFields.Fields.Item("U_PURCH_IDTYPE4").Value = dr["PurFourthType"];

                    lRetCode = oBP.Update();
                    if (lRetCode == 0)
                    {
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Purchaser tab details Updated Successfully ", sFuncName);
                    }
                    sResult = "SUCCESS";
                }
                else if (sTabId == "3")// Update Vendor tab details
                {
                    DataRow dr = dtVendor.Rows[0];
                    oBP.GetByKey(sCardCode);

                    oBP.UserFields.Fields.Item("U_REQ_DEV_CONSENT").Value = dr["VndrReqDevConsent"];
                    oBP.UserFields.Fields.Item("U_RECV_DEV_CONSENT").Value = dr["VndrReceiveDevConsent"];
                    oBP.UserFields.Fields.Item("U_VNDR_RP_NAME1").Value = dr["VndrFirstName"];
                    oBP.UserFields.Fields.Item("U_VNDR_RP_ID1").Value = dr["VndrFirstID"];
                    oBP.UserFields.Fields.Item("U_VNDR_RP_TAX1").Value = dr["VndrFirstTaxNo"];
                    oBP.UserFields.Fields.Item("U_VNDR_CONTACT1").Value = dr["VndrFirstContactNo"];
                    oBP.UserFields.Fields.Item("U_VNDR_IDTYPE1").Value = dr["VndrFirstType"];
                    oBP.UserFields.Fields.Item("U_VNDR_RP_NAME2").Value = dr["VndrSecName"];
                    oBP.UserFields.Fields.Item("U_VNDR_RP_ID2").Value = dr["VndrSecID"];
                    oBP.UserFields.Fields.Item("U_VNDR_RP_TAX2").Value = dr["VndrSecTaxNo"];
                    oBP.UserFields.Fields.Item("U_VNDR_CONTACT2").Value = dr["VndrSecContactNo"];
                    oBP.UserFields.Fields.Item("U_VNDR_IDTYPE2").Value = dr["VndrSecType"];
                    oBP.UserFields.Fields.Item("U_VNDR_RP_NAME3").Value = dr["VndrThirdName"];
                    oBP.UserFields.Fields.Item("U_VNDR_RP_ID3").Value = dr["VndrThirdID"];
                    oBP.UserFields.Fields.Item("U_VNDR_RP_TAX3").Value = dr["VndrThirdTaxNo"];
                    oBP.UserFields.Fields.Item("U_VNDR_CONTACT3").Value = dr["VndrThirdContactNo"];
                    oBP.UserFields.Fields.Item("U_VNDR_IDTYPE3").Value = dr["VndrThirdType"];
                    oBP.UserFields.Fields.Item("U_VNDR_RP_NAME4").Value = dr["VndrFourthName"];
                    oBP.UserFields.Fields.Item("U_VNDR_RP_ID4").Value = dr["VndrFourthID"];
                    oBP.UserFields.Fields.Item("U_VNDR_RP_TAX4").Value = dr["VndrFourthTaxNo"];
                    oBP.UserFields.Fields.Item("U_VNDR_CONTACT4").Value = dr["VndrFourthContactNo"];
                    oBP.UserFields.Fields.Item("U_VNDR_IDTYPE4").Value = dr["VndrFourthType"];

                    lRetCode = oBP.Update();
                    if (lRetCode == 0)
                    {
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Vendor tab details Updated Successfully ", sFuncName);
                    }
                    sResult = "SUCCESS";
                }
                else if (sTabId == "4")// Update Property tab details
                {
                    sResult = "SUCCESS";
                }
                else if (sTabId == "5") // Update Loan Principle tab details
                {
                    sResult = "SUCCESS";
                }
                else if (sTabId == "6")// Update Loan Subsidary tab details
                {
                    sResult = "SUCCESS";
                }

                // have to write the update statement for OCRD Using the DIAPI.
                return sResult;
            }
            catch (Exception Ex)
            {
                //if (oDICompany.InTransaction) oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                sResult = Ex.Message.ToString();
                throw Ex;
            }
            
        }

        public string SPA_ProcessCase_CreateBilling(string sDocType, DataTable dt)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();
            string sResult = string.Empty;
            string sCardCode = string.Empty;
            string sDocEntry = string.Empty;
            try
            {
                sFuncName = "SPA_ProcessCase_CreateBilling()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                // Note : Write the comments while doing the 

                sResult = "SUCCESS";
                return sResult;
            }
            catch (Exception Ex)
            {
                //if (oDICompany.InTransaction) oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                sResult = Ex.Message.ToString();
                throw Ex;
            }
        }

        public DataSet SPA_ProcessCase_GetNextSection(string sCaseNo)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "SPA_ProcessCase_GetNextSection()";
                sProcName = "getNextSection";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);


                oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName,
                    Data.CreateParameter("@caseNo", sCaseNo));
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

        public DataSet SPA_ProcessCase_GetOptionalItems(string sCaseNo)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "SPA_ProcessCase_GetNextSection()";
                sProcName = "getOptionalItems";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);


                oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName,
                    Data.CreateParameter("@caseNo", sCaseNo));
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

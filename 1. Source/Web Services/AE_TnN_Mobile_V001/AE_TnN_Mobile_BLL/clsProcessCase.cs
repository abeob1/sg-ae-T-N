using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using AE_TnN_Mobile_Common;
using System.Data;
using SAP.Admin.DAO;
using System.Data.SqlClient;

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
        public static string strItemCode = ConfigurationManager.AppSettings["ItemCode"].ToString();
        public static string strSelect = ConfigurationManager.AppSettings["Select"].ToString();

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
        }

        public DataTable SPA_ProcessCase_Status()
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            string sReturnResult = string.Empty;
            try
            {
                sFuncName = "SPA_ProcessCase_Status";
                sProcName = "AE_SPA028_Mobile_ProcessCase_Status";

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
        }

        public DataTable SPA_ProcessCase_TitleSubType()
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            string sReturnResult = string.Empty;
            try
            {
                sFuncName = "SPA_ProcessCase_Status";
                sProcName = "AE_SPA027_Mobile_ProcessCase_TitleSubType";

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
                SAPbobsCOM.BusinessPartners oBP = (SAPbobsCOM.BusinessPartners)(oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners));

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

                    if (dr["PurFirstName"].ToString() == string.Empty && dr["PurFirstType"].ToString() == strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_PURCH_IDTYPE1").Value = string.Empty;
                    }
                    else if (dr["PurFirstName"].ToString() == string.Empty && dr["PurFirstType"].ToString() != strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_PURCH_IDTYPE1").Value = string.Empty;
                    }
                    else if (dr["PurFirstName"].ToString() != string.Empty && dr["PurFirstType"].ToString() == strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_PURCH_IDTYPE1").Value = "INDIVIDUAL";
                    }
                    else
                    {
                        oBP.UserFields.Fields.Item("U_PURCH_IDTYPE1").Value = dr["PurFirstType"].ToString();
                    }

                    oBP.UserFields.Fields.Item("U_PURCH_RP_NAME2").Value = dr["PurSecName"];
                    oBP.UserFields.Fields.Item("U_PURCH_RP_ID2").Value = dr["PurSecID"];
                    oBP.UserFields.Fields.Item("U_PURCH_RP_TAX2").Value = dr["PurSecTaxNo"];
                    oBP.UserFields.Fields.Item("U_PURCH_CONTACT2").Value = dr["PurSecContactNo"];

                    if (dr["PurSecName"].ToString() == string.Empty && dr["PurSecType"].ToString() == strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_PURCH_IDTYPE2").Value = string.Empty;
                    }
                    else if (dr["PurSecName"].ToString() == string.Empty && dr["PurSecType"].ToString() != strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_PURCH_IDTYPE2").Value = string.Empty;
                    }
                    else if (dr["PurSecName"].ToString() != string.Empty && dr["PurSecType"].ToString() == strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_PURCH_IDTYPE2").Value = "INDIVIDUAL";
                    }
                    else
                    {
                        oBP.UserFields.Fields.Item("U_PURCH_IDTYPE2").Value = dr["PurSecType"].ToString();
                    }

                    oBP.UserFields.Fields.Item("U_PURCH_RP_NAME3").Value = dr["PurThirdName"];
                    oBP.UserFields.Fields.Item("U_PURCH_RP_ID3").Value = dr["PurThirdID"];
                    oBP.UserFields.Fields.Item("U_PURCH_RP_TAX3").Value = dr["PurThirdTaxNo"];
                    oBP.UserFields.Fields.Item("U_PURCH_CONTACT3").Value = dr["PurThirdContactNo"];

                    if (dr["PurThirdName"].ToString() == string.Empty && dr["PurThirdType"].ToString() == strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_PURCH_IDTYPE3").Value = string.Empty;
                    }
                    else if (dr["PurThirdName"].ToString() == string.Empty && dr["PurThirdType"].ToString() != strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_PURCH_IDTYPE3").Value = string.Empty;
                    }
                    else if (dr["PurThirdName"].ToString() != string.Empty && dr["PurThirdType"].ToString() == strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_PURCH_IDTYPE3").Value = "INDIVIDUAL";
                    }
                    else
                    {
                        oBP.UserFields.Fields.Item("U_PURCH_IDTYPE3").Value = dr["PurThirdType"].ToString();
                    }

                    oBP.UserFields.Fields.Item("U_PURCH_RP_NAME4").Value = dr["PurFourthName"];
                    oBP.UserFields.Fields.Item("U_PURCH_RP_ID4").Value = dr["PurFourthID"];
                    oBP.UserFields.Fields.Item("U_PURCH_RP_TAX4").Value = dr["PurFourthTaxNo"];
                    oBP.UserFields.Fields.Item("U_PURCH_CONTACT4").Value = dr["PurFourthContactNo"];

                    if (dr["PurFourthName"].ToString() == string.Empty && dr["PurFourthType"].ToString() == strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_PURCH_IDTYPE4").Value = string.Empty;
                    }
                    else if (dr["PurFourthName"].ToString() == string.Empty && dr["PurFourthType"].ToString() != strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_PURCH_IDTYPE4").Value = string.Empty;
                    }
                    else if (dr["PurFourthName"].ToString() != string.Empty && dr["PurFourthType"].ToString() == strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_PURCH_IDTYPE4").Value = "INDIVIDUAL";
                    }
                    else
                    {
                        oBP.UserFields.Fields.Item("U_PURCH_IDTYPE4").Value = dr["PurFourthType"].ToString();
                    }

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

                    if (dr["VndrFirstName"].ToString() == string.Empty && dr["VndrFirstType"].ToString() == strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_VNDR_IDTYPE1").Value = string.Empty;
                    }
                    else if (dr["VndrFirstName"].ToString() == string.Empty && dr["VndrFirstType"].ToString() != strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_VNDR_IDTYPE1").Value = string.Empty;
                    }
                    else if (dr["VndrFirstName"].ToString() != string.Empty && dr["VndrFirstType"].ToString() == strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_VNDR_IDTYPE1").Value = "INDIVIDUAL";
                    }
                    else
                    {
                        oBP.UserFields.Fields.Item("U_VNDR_IDTYPE1").Value = dr["VndrFirstType"].ToString();
                    }

                    oBP.UserFields.Fields.Item("U_VNDR_RP_NAME2").Value = dr["VndrSecName"];
                    oBP.UserFields.Fields.Item("U_VNDR_RP_ID2").Value = dr["VndrSecID"];
                    oBP.UserFields.Fields.Item("U_VNDR_RP_TAX2").Value = dr["VndrSecTaxNo"];
                    oBP.UserFields.Fields.Item("U_VNDR_CONTACT2").Value = dr["VndrSecContactNo"];

                    if (dr["VndrSecName"].ToString() == string.Empty && dr["VndrSecType"].ToString() == strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_VNDR_IDTYPE2").Value = string.Empty;
                    }
                    else if (dr["VndrSecName"].ToString() == string.Empty && dr["VndrSecType"].ToString() != strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_VNDR_IDTYPE2").Value = string.Empty;
                    }
                    else if (dr["VndrSecName"].ToString() != string.Empty && dr["VndrSecType"].ToString() == strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_VNDR_IDTYPE2").Value = "INDIVIDUAL";
                    }
                    else
                    {
                        oBP.UserFields.Fields.Item("U_VNDR_IDTYPE2").Value = dr["VndrSecType"].ToString();
                    }

                    oBP.UserFields.Fields.Item("U_VNDR_RP_NAME3").Value = dr["VndrThirdName"];
                    oBP.UserFields.Fields.Item("U_VNDR_RP_ID3").Value = dr["VndrThirdID"];
                    oBP.UserFields.Fields.Item("U_VNDR_RP_TAX3").Value = dr["VndrThirdTaxNo"];
                    oBP.UserFields.Fields.Item("U_VNDR_CONTACT3").Value = dr["VndrThirdContactNo"];

                    if (dr["VndrThirdName"].ToString() == string.Empty && dr["VndrThirdType"].ToString() == strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_VNDR_IDTYPE3").Value = string.Empty;
                    }
                    else if (dr["VndrThirdName"].ToString() == string.Empty && dr["VndrThirdType"].ToString() != strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_VNDR_IDTYPE3").Value = string.Empty;
                    }
                    else if (dr["VndrThirdName"].ToString() != string.Empty && dr["VndrThirdType"].ToString() == strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_VNDR_IDTYPE3").Value = "INDIVIDUAL";
                    }
                    else
                    {
                        oBP.UserFields.Fields.Item("U_VNDR_IDTYPE3").Value = dr["VndrThirdType"].ToString();
                    }

                    oBP.UserFields.Fields.Item("U_VNDR_RP_NAME4").Value = dr["VndrFourthName"];
                    oBP.UserFields.Fields.Item("U_VNDR_RP_ID4").Value = dr["VndrFourthID"];
                    oBP.UserFields.Fields.Item("U_VNDR_RP_TAX4").Value = dr["VndrFourthTaxNo"];
                    oBP.UserFields.Fields.Item("U_VNDR_CONTACT4").Value = dr["VndrFourthContactNo"];

                    if (dr["VndrFourthName"].ToString() == string.Empty && dr["VndrFourthType"].ToString() == strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_VNDR_IDTYPE4").Value = string.Empty;
                    }
                    else if (dr["VndrFourthName"].ToString() == string.Empty && dr["VndrFourthType"].ToString() != strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_VNDR_IDTYPE4").Value = string.Empty;
                    }
                    else if (dr["VndrFourthName"].ToString() != string.Empty && dr["VndrFourthType"].ToString() == strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_VNDR_IDTYPE4").Value = "INDIVIDUAL";
                    }
                    else
                    {
                        oBP.UserFields.Fields.Item("U_VNDR_IDTYPE4").Value = dr["VndrFourthType"].ToString();
                    }

                    lRetCode = oBP.Update();
                    if (lRetCode == 0)
                    {
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Vendor tab details Updated Successfully ", sFuncName);
                        sResult = "SUCCESS";
                    }
                    else
                    {
                        sResult = oDICompany.GetLastErrorDescription(); throw new ArgumentException(sResult);
                    }
                }
                else if (sTabId == "4")// Update Property tab details
                {
                    DataRow dr = dtProperty.Rows[0];
                    oBP.GetByKey(sCardCode);

                    oBP.UserFields.Fields.Item("U_TITLETYPE").Value = dr["TitleType"];

                    oBP.UserFields.Fields.Item("U_LOTNO").Value = dr["LotNo"];
                    oBP.UserFields.Fields.Item("U_FORMERLY_KNOWN_AS").Value = dr["PreviouslyKnownAs"];
                    oBP.UserFields.Fields.Item("U_STATE").Value = dr["State"];
                    oBP.UserFields.Fields.Item("U_AREA").Value = dr["Area"];
                    oBP.UserFields.Fields.Item("U_BPM").Value = dr["BPM"];

                    oBP.UserFields.Fields.Item("U_LOTAREA").Value = dr["LotArea"];
                    oBP.UserFields.Fields.Item("U_DEVELOPER_CODE").Value = dr["Developer"];
                    oBP.UserFields.Fields.Item("ProjectCod").Value = dr["Project"];
                    oBP.UserFields.Fields.Item("U_ADV_DEV_LIC_NO").Value = dr["DevLicenseNo"];
                    oBP.UserFields.Fields.Item("U_DEV_SOL_NAME").Value = dr["DevSolicitor"];
                    oBP.UserFields.Fields.Item("U_DEV_SOL_LOC").Value = dr["DevSoliLoc"];
                    oBP.UserFields.Fields.Item("U_TITLESEARCH_DT").Value = dr["TitleSearchDate"];
                    oBP.UserFields.Fields.Item("U_SBM_CNST_TRNSF_DT").Value = dr["DSCTransfer"];
                    oBP.UserFields.Fields.Item("U_RCV_CNST_TRNSF_DT").Value = dr["DRCTransfer"];
                    oBP.UserFields.Fields.Item("U_14A_DT").Value = dr["FourteenADate"];
                    oBP.UserFields.Fields.Item("U_TITLE_RTN_LNDRG_DT").Value = dr["DRTLRegistry"];
                    //if (dr["PropertyCharged"].ToString() == "Y")
                    //{
                    //    oBP.set_Properties(13, SAPbobsCOM.BoYesNoEnum.tYES);
                    //}
                    //else
                    //{
                    //    oBP.set_Properties(13, SAPbobsCOM.BoYesNoEnum.tNO);
                    //}
                    //oBP.UserFields.Fields.Item("U_PROPERTY_ISCHARGED").Value = dr["PropertyCharged"];

                    if (dr["PropertyCharged"].ToString() == "true")
                    {
                        oBP.set_Properties(12, SAPbobsCOM.BoYesNoEnum.tYES);
                        oBP.set_Properties(13, SAPbobsCOM.BoYesNoEnum.tNO);
                    }
                    else
                    {
                        oBP.set_Properties(12, SAPbobsCOM.BoYesNoEnum.tNO);
                        oBP.set_Properties(13, SAPbobsCOM.BoYesNoEnum.tYES);
                    }
                    oBP.UserFields.Fields.Item("U_PROPERTY_ISCHARGED").Value = dr["PropertyCharged"].ToString() == "true" ? "Y" : "N";

                    oBP.UserFields.Fields.Item("U_CHRG_BANK_CODE").Value = dr["BankName"];
                    oBP.UserFields.Fields.Item("U_CHRG_BANK_BRANCH").Value = dr["Branch"];
                    oBP.UserFields.Fields.Item("U_CHRG_BANK_PA_NAME").Value = dr["PAName"];
                    oBP.UserFields.Fields.Item("U_CHRG_BANK_PRSNTNO").Value = dr["PresentationNo"];
                    oBP.UserFields.Fields.Item("U_EXISTING_CHRG_REF").Value = dr["ExistChargeRef"];
                    if (dr["ReceiptType"].ToString() != strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_RECEIPT_TYPE").Value = dr["ReceiptType"];
                    }
                    oBP.UserFields.Fields.Item("U_RECEIPT_NO").Value = dr["ReceiptNo"];
                    oBP.UserFields.Fields.Item("U_RECEIPT_DT").Value = dr["ReceiptDate"];
                    oBP.UserFields.Fields.Item("U_PROPERTY_PURPRC").Value = dr["PurchasePrice"];
                    oBP.UserFields.Fields.Item("U_PROPERTY_ADJ_VALUE").Value = dr["AdjValue"];
                    oBP.UserFields.Fields.Item("U_VNDR_PRV_SPA_VALUE").Value = dr["VndrPrevSPAValue"];
                    oBP.UserFields.Fields.Item("U_PROPERTY_DEPOSIT").Value = dr["Deposit"];
                    oBP.UserFields.Fields.Item("U_PROPERTY_BALPURPRC").Value = dr["BalPurPrice"];
                    oBP.UserFields.Fields.Item("U_PROPERTY_LOAN_AMT").Value = dr["LoanAmount"];
                    oBP.UserFields.Fields.Item("U_PROPERTY_LOAN_CASE").Value = dr["LoanCaseNo"];
                    oBP.UserFields.Fields.Item("U_DIFFERENTIAL_SUM").Value = dr["DiffSum"];
                    oBP.UserFields.Fields.Item("U_REDEMPTION_AMT").Value = dr["RedAmt"];
                    oBP.UserFields.Fields.Item("U_REDEMPTION_DT").Value = dr["RedDate"];
                    oBP.UserFields.Fields.Item("U_DEFICIT_REDEMPTSUM").Value = dr["DefRdmptSum"];

                    if (dr["QryGroup14"].ToString() == "Y")
                    {
                        oBP.set_Properties(14, SAPbobsCOM.BoYesNoEnum.tYES);
                    }
                    else
                    {
                        oBP.set_Properties(14, SAPbobsCOM.BoYesNoEnum.tNO);
                    }

                    if (dr["QryGroup15"].ToString() == "Y")
                    {
                        oBP.set_Properties(15, SAPbobsCOM.BoYesNoEnum.tYES);
                    }
                    else
                    {
                        oBP.set_Properties(15, SAPbobsCOM.BoYesNoEnum.tNO);
                    }

                    if (dr["QryGroup16"].ToString() == "Y")
                    {
                        oBP.set_Properties(16, SAPbobsCOM.BoYesNoEnum.tYES);
                    }
                    else
                    {
                        oBP.set_Properties(16, SAPbobsCOM.BoYesNoEnum.tNO);
                    }

                    lRetCode = oBP.Update();
                    if (lRetCode == 0)
                    {
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Property tab details Updated Successfully ", sFuncName);
                        sResult = "SUCCESS";
                    }
                    else
                    {
                        sResult = oDICompany.GetLastErrorDescription(); throw new ArgumentException(sResult);
                    }

                    // Write a Query to update in the [@AE-PROPERTY] TABLE


                    SqlConnection con = new SqlConnection(ConnectionString);
                    SqlCommand command = con.CreateCommand();

                    string[] TimeSplit = DateTime.Now.TimeOfDay.ToString().Split(':');
                    string sCharged = string.Empty;
                    string sFree = string.Empty;

                    if (dr["PropertyCharged"].ToString() == "true")
                    {
                        sCharged = "Y";
                        sFree = "N";
                    }
                    else
                    {
                        sCharged = "N";
                        sFree = "Y";
                    }
                    //Updating the Informations
                    command.CommandText = "UPDATE [@AE_PROPERTY] SET U_LOTNO = '" + dr["LotNo"] + "',"
                                            + "U_FORMERLY_KNOWN_AS = '" + dr["PreviouslyKnownAs"] + "',"
                                            + "U_BPM = '" + dr["BPM"] + "',"
                                            + "U_STATE = '" + dr["State"] + "',"
                                            + "U_AREA = '" + dr["Area"] + "',"
                                            + "U_LOTAREA = '" + dr["LotArea"] + "',"
                                            + "UpdateDate = '" + DateTime.Now.Date + "',"
                                            + "Updatetime = '" + TimeSplit[0] + TimeSplit[1] + "',"
                                            + "U_DEVLICNO = '" + dr["DevLicenseNo"] + "',"
                                            + "U_DVLPR_LOC = '" + dr["DevSoliLoc"] + "',"
                                            + "U_LSTCHG_BRANCH = '" + dr["Branch"] + "',"
                                            + "U_LSTCHG_PANO = '" + dr["PAName"] + "',"
                                            + "U_LSTCHG_PRSTNO = '" + dr["PresentationNo"] + "',"
                                            + "U_PROPERTY_CHARGED = '" + sCharged.ToString() + "',"
                                            + "U_PROPERTY_FREE = '" + sFree.ToString() + "'"
                                            + " WHERE Code = (select U_PROPERTY_CODE  from OCRD  WITH (NOLOCK) where CardCode = '" + sCardCode + "')";
                    con.Open();

                    command.ExecuteNonQuery();
                    //sReturnResult = "SUCCESS";

                    con.Close();

                }
                else if (sTabId == "5") // Update Loan Principle tab details
                {
                    DataRow dr = dtLoanPrinciple.Rows[0];
                    oBP.GetByKey(sCardCode);

                    oBP.UserFields.Fields.Item("U_LOAN_MSTR_BANKCODE").Value = dr["MasterBankName"];
                    oBP.UserFields.Fields.Item("U_LOAN_BRANCH").Value = dr["BranchName"];
                    oBP.UserFields.Fields.Item("U_LOAN_BANK_ADD").Value = dr["Address"];
                    oBP.UserFields.Fields.Item("U_LOAN_BANK_PA_NAME").Value = dr["PAName"];
                    oBP.UserFields.Fields.Item("U_LOAN_BANK_REF").Value = dr["BankRef"];
                    oBP.UserFields.Fields.Item("U_LOAN_BANK_INSTR_DT").Value = dr["BankInsDate"];
                    oBP.UserFields.Fields.Item("U_LOAN_LETTEROFFR_DT").Value = dr["LOFDate"];
                    oBP.UserFields.Fields.Item("U_PROPERTY_LOAN_CASE").Value = dr["LoanCaseNo"];
                    oBP.UserFields.Fields.Item("U_LOAN_BANK_SOL_NAME").Value = dr["BankSolicitor"];
                    oBP.UserFields.Fields.Item("U_LOAN_BANK_SOL_LOC").Value = dr["SoliLoc"];
                    oBP.UserFields.Fields.Item("U_LOAN_BANK_SOL_REF").Value = dr["SoliRef"];
                    oBP.UserFields.Fields.Item("U_LOAN_RDMPSTMTREQDT").Value = dr["ReqRedStatement"];
                    oBP.UserFields.Fields.Item("U_LOAN_RDMPT_STMT_DT").Value = dr["RedStmtDate"];
                    oBP.UserFields.Fields.Item("U_LOAN_RDMPTPYMTDT").Value = dr["RedPayDate"];
                    oBP.UserFields.Fields.Item("U_PROJECT_NAME").Value = dr["Project"];
                    if (dr["TypeofLoan"].ToString() != strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_LOAN_PRNCP_TYPE").Value = dr["TypeofLoan"];
                    }
                    oBP.UserFields.Fields.Item("U_LOAN_PRNCP_FTYPE").Value = dr["TypeofFacility"];
                    oBP.UserFields.Fields.Item("U_LOAN_PRNCP_FAMT").Value = dr["FacilityAmt"];
                    oBP.UserFields.Fields.Item("U_LOAN_PRNCP_INT_RT").Value = dr["IntrstRate"];
                    oBP.UserFields.Fields.Item("U_LOAN_PRNCP_TRMLOAN").Value = dr["TermLoanAmt"];
                    oBP.UserFields.Fields.Item("U_LOAN_PRNCP_RPYMTPD").Value = dr["Repaymt"];
                    oBP.UserFields.Fields.Item("U_LOAN_PRNCP_MTHINST").Value = dr["MonthlyInstmt"];
                    oBP.UserFields.Fields.Item("U_LOAN_PRNCP_INT").Value = dr["Interest"];
                    oBP.UserFields.Fields.Item("U_LOAN_PRNCP_ODLOAN").Value = dr["ODLoan"];
                    oBP.UserFields.Fields.Item("U_LOAN_PRNCP_MRTA").Value = dr["MRTA"];
                    oBP.UserFields.Fields.Item("U_LOAN_PRNCP_BNKGNTE").Value = dr["BankGuarantee"];
                    oBP.UserFields.Fields.Item("U_LOAN_PRNCP_LC").Value = dr["LetterofCredit"];
                    oBP.UserFields.Fields.Item("U_LOAN_PRNCP_TRUSTRC").Value = dr["TrustReceipt"];
                    oBP.UserFields.Fields.Item("U_LOAN_PRNCP_OTHR").Value = dr["Others"];
                    oBP.UserFields.Fields.Item("U_BRWR_RP_FIRM").Value = dr["RepByFirm"].ToString() == "true" ? "Y" : "N";
                    oBP.UserFields.Fields.Item("U_LOAN_DET_1").Value = dr["LoanDet1"];
                    oBP.UserFields.Fields.Item("U_LOAN_DET_2").Value = dr["LoanDet2"];
                    oBP.UserFields.Fields.Item("U_LOAN_DET_3").Value = dr["LoanDet3"];
                    oBP.UserFields.Fields.Item("U_LOAN_DET_4").Value = dr["LoanDet4"];
                    oBP.UserFields.Fields.Item("U_LOAN_DET_5").Value = dr["LoanDet5"];

                    lRetCode = oBP.Update();
                    if (lRetCode == 0)
                    {
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Loan Principle tab details Updated Successfully ", sFuncName);
                        sResult = "SUCCESS";
                    }
                    else
                    {
                        sResult = oDICompany.GetLastErrorDescription(); throw new ArgumentException(sResult);
                    }
                }
                else if (sTabId == "6")// Update Loan Subsidary tab details
                {
                    DataRow dr = dtLoanSubsidary.Rows[0];
                    oBP.GetByKey(sCardCode);

                    oBP.UserFields.Fields.Item("U_LOAN_DOC_FWD_BK_DT").Value = dr["LoanDocForBankExe"];
                    oBP.UserFields.Fields.Item("U_FACILITY_AGMT_DT").Value = dr["FaciAgreeDate"];
                    oBP.UserFields.Fields.Item("U_LOAN_DOC_RTN_BK_DT").Value = dr["LoanDocRetFromBank"];
                    oBP.UserFields.Fields.Item("U_DISCHARGE_CHRG_DT").Value = dr["DischargeofCharge"];
                    if (dr["FirstTypeofFacility"].ToString() != strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_LOAN_SUB1_FTYPE").Value = dr["FirstTypeofFacility"];
                    }
                    oBP.UserFields.Fields.Item("U_LOAN_SUB1_FAMT").Value = dr["FirstFacilityAmt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB1_RPYMTPD").Value = dr["FirstRepaymt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB1_INT_RT").Value = dr["FirstIntrstRate"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB1_MTHINST").Value = dr["FirstMonthlyInstmt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB1_TRMLOAN").Value = dr["FirstTermLoanAmt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB1_INT").Value = dr["FirstInterest"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB1_ODLOAN").Value = dr["FirstODLoan"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB1_MRTA").Value = dr["FirstMRTA"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB1_BNKGNTE").Value = dr["FirstBankGuarantee"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB1_LC").Value = dr["FirstLetterofCredit"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB1_TRUSTRC").Value = dr["FirstTrustReceipt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB1_OTHR").Value = dr["FirstOthers"];

                    if (dr["SecTypeofFacility"].ToString() != strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_LOAN_SUB2_FTYPE").Value = dr["SecTypeofFacility"];
                    }
                    oBP.UserFields.Fields.Item("U_LOAN_SUB2_FAMT").Value = dr["SecFacilityAmt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB2_RPYMTPD").Value = dr["SecRepaymt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB2_INT_RT").Value = dr["SecIntrstRate"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB2_MTHINST").Value = dr["SecMonthlyInstmt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB2_TRMLOAN").Value = dr["SecTermLoanAmt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB2_INT").Value = dr["SecInterest"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB2_ODLOAN").Value = dr["SecODLoan"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB2_MRTA").Value = dr["SecMRTA"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB2_BNKGNTE").Value = dr["SecBankGuarantee"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB2_LC").Value = dr["SecLetterofCredit"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB2_TRUSTRC").Value = dr["SecTrustReceipt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB2_OTHR").Value = dr["SecOthers"];

                    if (dr["ThirdTypeofFacility"].ToString() != strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_LOAN_SUB3_FTYPE").Value = dr["ThirdTypeofFacility"];
                    }
                    oBP.UserFields.Fields.Item("U_LOAN_SUB3_FAMT").Value = dr["ThirdFacilityAmt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB3_RPYMTPD").Value = dr["ThirdRepaymt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB3_INT_RT").Value = dr["ThirdIntrstRate"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB3_MTHINST").Value = dr["ThirdMonthlyInstmt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB3_TRMLOAN").Value = dr["ThirdTermLoanAmt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB3_INT").Value = dr["ThirdInterest"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB3_ODLOAN").Value = dr["ThirdODLoan"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB3_MRTA").Value = dr["ThirdMRTA"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB3_BNKGNTE").Value = dr["ThirdBankGuarantee"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB3_LC").Value = dr["ThirdLetterofCredit"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB3_TRUSTRC").Value = dr["ThirdTrustReceipt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB3_OTHR").Value = dr["ThirdOthers"];

                    if (dr["FourthTypeofFacility"].ToString() != strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_LOAN_SUB4_FTYPE").Value = dr["FourthTypeofFacility"];
                    }
                    oBP.UserFields.Fields.Item("U_LOAN_SUB4_FAMT").Value = dr["FourthFacilityAmt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB4_RPYMTPD").Value = dr["FourthRepaymt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB4_INT_RT").Value = dr["FourthIntrstRate"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB4_MTHINST").Value = dr["FourthMonthlyInstmt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB4_TRMLOAN").Value = dr["FourthTermLoanAmt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB4_INT").Value = dr["FourthInterest"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB4_ODLOAN").Value = dr["FourthODLoan"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB4_MRTA").Value = dr["FourthMRTA"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB4_BNKGNTE").Value = dr["FourthBankGuarantee"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB4_LC").Value = dr["FourthLetterofCredit"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB4_TRUSTRC").Value = dr["FourthTrustReceipt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB4_OTHR").Value = dr["FourthOthers"];

                    if (dr["FifthTypeofFacility"].ToString() != strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_LOAN_SUB5_FTYPE").Value = dr["FifthTypeofFacility"];
                    }
                    oBP.UserFields.Fields.Item("U_LOAN_SUB5_FAMT").Value = dr["FifthFacilityAmt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB5_RPYMTPD").Value = dr["FifthRepaymt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB5_INT_RT").Value = dr["FifthIntrstRate"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB5_MTHINST").Value = dr["FifthMonthlyInstmt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB5_TRMLOAN").Value = dr["FifthTermLoanAmt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB5_INT").Value = dr["FifthInterest"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB5_ODLOAN").Value = dr["FifthODLoan"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB5_MRTA").Value = dr["FifthMRTA"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB5_BNKGNTE").Value = dr["FifthBankGuarantee"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB5_LC").Value = dr["FifthLetterofCredit"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB5_TRUSTRC").Value = dr["FifthTrustReceipt"];
                    oBP.UserFields.Fields.Item("U_LOAN_SUB5_OTHR").Value = dr["FifthOthers"];

                    lRetCode = oBP.Update();
                    if (lRetCode == 0)
                    {
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Loan Subsidary details Updated Successfully ", sFuncName);
                        sResult = "SUCCESS";
                    }
                    else
                    {
                        sResult = oDICompany.GetLastErrorDescription(); throw new ArgumentException(sResult);
                    }
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

        //public DataSet SPA_ProcessCase_GetNextSection(string sCaseNo)
        //{
        //    DataSet oDataset = new DataSet();
        //    DataSet oDataset1 = new DataSet();
        //    string sFuncName = string.Empty;
        //    string sProcName = string.Empty;
        //    string sProcName1 = string.Empty;
        //    DataView oDTView = new DataView();
        //    string sCanClose = string.Empty;

        //    try
        //    {
        //        sFuncName = "SPA_ProcessCase_GetNextSection()";
        //        sProcName = "getNextSection";
        //        sProcName1 = "canCloseCurrentSection";

        //        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

        //        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName1, sFuncName);
        //        oDataset1 = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName1,
        //            Data.CreateParameter("@caseNo", sCaseNo));
        //        if (oDataset1 != null && oDataset1.Tables.Count > 0)
        //        {
        //            if (oDataset1.Tables[0].Rows.Count > 0)
        //            {
        //                sCanClose = oDataset1.Tables[0].Rows[0][0].ToString();
        //            }
        //        }

        //        if (sCanClose == "Y")
        //        {

        //        }
        //        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);
        //        oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName,
        //            Data.CreateParameter("@caseNo", sCaseNo));

        //        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS  ", sFuncName);
        //        if (oDataset.Tables.Count > 0 && oDataset != null)
        //        {
        //            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("There is a set of data from the SP :" + sProcName, sFuncName);
        //            return oDataset;
        //        }
        //        else
        //        {
        //            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("There is no data from the SP :" + sProcName, sFuncName);
        //            return oDataset;
        //        }

        //    }
        //    catch (Exception Ex)
        //    {
        //        sErrDesc = Ex.Message.ToString();
        //        oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
        //        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
        //        throw Ex;
        //    }
        //}


        public DataSet SPA_ProcessCase_GetNextSection(string sCaseNo)
        {
            DataSet oDataset = new DataSet();
            DataSet oDataset1 = new DataSet();
            DataSet oDataset2 = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            string sProcName1 = string.Empty;
            DataView oDTView = new DataView();
            string sCanClose = string.Empty;
            string sProcName2 = string.Empty;

            try
            {
                sFuncName = "SPA_ProcessCase_GetNextSection()";
                sProcName = "getNextSection";
                sProcName1 = "canCloseCurrentSection";
                sProcName2 = "getOpenSection";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName1, sFuncName);
                oDataset1 = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName1,
                    Data.CreateParameter("@caseNo", sCaseNo));
                if (oDataset1 != null && oDataset1.Tables.Count > 0)
                {
                    if (oDataset1.Tables[0].Rows.Count > 0)
                    {
                        sCanClose = oDataset1.Tables[0].Rows[0][0].ToString();
                    }
                }
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Connecting to target company ", sFuncName);
                oDICompany = oLogin.ConnectToTargetCompany(ConnectionString);

                if (sCanClose == "Y")
                {
                    SAPbobsCOM.Documents oSalesQuote;
                    SAPbobsCOM.Recordset oRecSet;
                    string sSql;
                    int iSalQuoteEntry;
                    int iItemLine;

                    oSalesQuote = oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oQuotations);
                    oRecSet = oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                    oDICompany.StartTransaction();

                    sSql = "SELECT A.DocEntry,B.VisOrder FROM OQUT A WITH (NOLOCK) INNER JOIN QUT1 B WITH (NOLOCK) ON B.DocEntry = A.DocEntry WHERE CardCode = '" + sCaseNo + "' AND B.ItemCode <> '" + strItemCode + "' AND DocStatus = 'O'";
                    oRecSet.DoQuery(sSql);
                    if (oRecSet.RecordCount > 0)
                    {
                        while (!oRecSet.EoF)
                        {
                            iSalQuoteEntry = Convert.ToInt16(oRecSet.Fields.Item("DocEntry").Value);
                            iItemLine = Convert.ToInt16(oRecSet.Fields.Item("VisOrder").Value);

                            if (oSalesQuote.GetByKey(Convert.ToInt16(iSalQuoteEntry)))
                            {
                                oSalesQuote.Lines.SetCurrentLine(Convert.ToInt16(iItemLine));
                                oSalesQuote.Lines.LineStatus = SAPbobsCOM.BoStatus.bost_Close;
                                if (oSalesQuote.Update() != 0)
                                {
                                    if (oDICompany.InTransaction == true)
                                    {
                                        oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                                    }
                                    else
                                    {
                                        oDICompany.StartTransaction();
                                        oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                                    }
                                    sErrDesc = oDICompany.GetLastErrorDescription(); throw new ArgumentException(sErrDesc);
                                }
                            }
                            oRecSet.MoveNext();
                        }
                    }
                    oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);
                    oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName,
                        Data.CreateParameter("@caseNo", sCaseNo));

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS  ", sFuncName);
                    if (oDataset.Tables.Count > 0 && oDataset != null)
                    {
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("There is a set of data from the SP :" + sProcName, sFuncName);
                        if (CreateSalesQuote(oDataset.Tables[0], oDICompany) == "SUCCESS")
                        {
                            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName2, sFuncName);
                            oDataset2 = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName2,
                                Data.CreateParameter("@caseNo", sCaseNo));
                        }
                        oDataset = oDataset2;
                    }
                }
                else
                {
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("There is no data from the SP :" + sProcName, sFuncName);
                }
                return oDataset;
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
                sFuncName = "SPA_ProcessCase_GetOptionalItems()";
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

        public DataSet SPA_ProcessCase_GetOpenSection(string sCaseNo)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "SPA_ProcessCase_GetOpenSection()";
                sProcName = "getOpenSection";

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

        public DataSet SPA_ProcessCase_GetPastSection(string sCaseNo)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "SPA_ProcessCase_GetPastSection()";
                sProcName = "getPastSection";

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

        public DataSet SPA_ProcessCase_GetAlternative(string sItemCode)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "SPA_ProcessCase_GetAlternative()";
                sProcName = "getalternative";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);


                oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName,
                    Data.CreateParameter("@itemcode", sItemCode));
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

        public string SPA_ProcessCase_UpdateAlternativeItem(string sOldItemCode, string sNewItemCode, string sItemName, string sDocEntry, string LineNum)
        {
            string sFuncName = string.Empty;
            string sReturnResult = string.Empty;
            DataTable results = new DataTable();
            try
            {
                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand command = con.CreateCommand();
                command.CommandText = "Update [QUT1] set ItemCode ='" + sNewItemCode + "',Dscription ='" + sItemName + "'" +
                                      "WHERE LineNum = '" + LineNum + "' AND DocEntry = '" + sDocEntry + "' AND ItemCode = '" + sOldItemCode + "'";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Executing the Query : " + command.CommandText, sFuncName);

                con.Open();
                command.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw Ex;
            }

            return "SUCCESS";
        }

        public DataSet SPA_ProcessCase_CanCloseCurrentSection(string sCaseNo)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "SPA_ProcessCase_CanCloseCurrentSection()";
                sProcName = "canCloseCurrentSection";

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

        // Written by Jeeva

        public string SPA_ProcessCase_CreateBilling(DataTable dt)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();
            string sResult = string.Empty;
            string sCardCode = string.Empty;
            string sQuoteEntry = string.Empty;
            string sSalesEntry = string.Empty;
            string sPurchaseEntry = string.Empty;
            string sApInvEntry = string.Empty;
            string sDocType = string.Empty;
            string sItemLine = string.Empty;
            string sCaseNo = string.Empty;

            try
            {
                sFuncName = "SPA_ProcessCase_CreateBilling()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Connecting to target company ", sFuncName);
                oDICompany = oLogin.ConnectToTargetCompany(ConnectionString);

                foreach (DataRow iRow in dt.Rows)
                {
                    sDocType = Convert.ToString(iRow["TrnspName"]);
                    sQuoteEntry = Convert.ToString(iRow["DocEntry"]);
                    sItemLine = Convert.ToString(iRow["LineNum"]);
                    sCaseNo = Convert.ToString(iRow["CaseNo"]);

                    if (sDocType == "ADD-PO-D")
                    {
                        oDICompany.StartTransaction();
                        sSalesEntry = AddDocuments(dt, oDICompany, sDocType);
                        if (sSalesEntry == string.Empty)
                        {
                            if (oDICompany.InTransaction == true)
                            {
                                oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                            }
                            else
                            {
                                oDICompany.StartTransaction();
                                oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                            }
                            throw new ArgumentException(sErrDesc);
                        }
                        else if (sSalesEntry != string.Empty)
                        {
                            sPurchaseEntry = CreatePO(sSalesEntry, oDICompany, dt);
                            if (sPurchaseEntry == string.Empty)
                            {
                                if (oDICompany.InTransaction == true)
                                {
                                    oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                                }
                                else
                                {
                                    oDICompany.StartTransaction();
                                    oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                                }
                                throw new ArgumentException(sErrDesc);
                            }
                            else if (sPurchaseEntry != string.Empty)
                            {
                                sApInvEntry = CreateAPInv(sPurchaseEntry, oDICompany, sCaseNo);
                                if (sApInvEntry == string.Empty)
                                {
                                    if (oDICompany.InTransaction == true)
                                    {
                                        oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                                    }
                                    else
                                    {
                                        oDICompany.StartTransaction();
                                        oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                                    }
                                    throw new ArgumentException(sErrDesc);
                                }
                            }
                        }
                        if (sQuoteEntry != string.Empty)
                        {
                            SAPbobsCOM.Documents oSalesQuote;
                            oSalesQuote = oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oQuotations);

                            if (oSalesQuote.GetByKey(Convert.ToInt16(sQuoteEntry)))
                            {
                                oSalesQuote.Lines.SetCurrentLine(Convert.ToInt16(sItemLine));
                                oSalesQuote.Lines.UserFields.Fields.Item("U_NEXT_ACTION_BY").Value = "FN";
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_LAST_UPDAT").Value = DateTime.Now.Date;

                                if (oSalesQuote.Update() != 0)
                                {
                                    sErrDesc = oDICompany.GetLastErrorDescription(); throw new ArgumentException(sErrDesc);
                                }
                            }
                        }


                        oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                    }
                    else if (sDocType == "ADD-PO")
                    {
                        oDICompany.StartTransaction();
                        sSalesEntry = AddDocuments(dt, oDICompany, sDocType);
                        if (sSalesEntry == string.Empty)
                        {
                            if (oDICompany.InTransaction == true)
                            {
                                oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                            }
                            else
                            {
                                oDICompany.StartTransaction();
                                oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                            }
                            throw new ArgumentException(sErrDesc);
                        }
                        else if (sSalesEntry != string.Empty)
                        {
                            sPurchaseEntry = CreatePO(sSalesEntry, oDICompany, dt);
                            if (sPurchaseEntry == string.Empty)
                            {
                                if (oDICompany.InTransaction == true)
                                {
                                    oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                                }
                                else
                                {
                                    oDICompany.StartTransaction();
                                    oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                                }
                                throw new ArgumentException(sErrDesc);
                            }
                            else if (sPurchaseEntry != string.Empty)
                            {
                                sApInvEntry = CreateAPInv(sPurchaseEntry, oDICompany, sCaseNo);
                                if (sApInvEntry == string.Empty)
                                {
                                    if (oDICompany.InTransaction == true)
                                    {
                                        oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                                    }
                                    else
                                    {
                                        oDICompany.StartTransaction();
                                        oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                                    }
                                    throw new ArgumentException(sErrDesc);
                                }
                            }
                        }
                        if (sQuoteEntry != string.Empty)
                        {
                            SAPbobsCOM.Documents oSalesQuote;
                            oSalesQuote = oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oQuotations);

                            if (oSalesQuote.GetByKey(Convert.ToInt16(sQuoteEntry)))
                            {
                                oSalesQuote.Lines.SetCurrentLine(Convert.ToInt16(sItemLine));
                                oSalesQuote.Lines.UserFields.Fields.Item("U_NEXT_ACTION_BY").Value = "FN";
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_LAST_UPDAT").Value = DateTime.Now.Date;

                                if (oSalesQuote.Update() != 0)
                                {
                                    sErrDesc = oDICompany.GetLastErrorDescription(); throw new ArgumentException(sErrDesc);
                                }
                            }
                        }
                        oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                    }
                    else if (sDocType == "FEES")
                    {
                        oDICompany.StartTransaction();
                        sSalesEntry = AddDocuments(dt, oDICompany, sDocType);
                        if (sSalesEntry == string.Empty)
                        {
                            if (oDICompany.InTransaction == true)
                            {
                                oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                            }
                            else
                            {
                                oDICompany.StartTransaction();
                                oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                            }
                            throw new ArgumentException(sErrDesc);
                        }
                        if (sQuoteEntry != string.Empty)
                        {
                            SAPbobsCOM.Documents oSalesQuote;
                            oSalesQuote = oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oQuotations);

                            if (oSalesQuote.GetByKey(Convert.ToInt16(sQuoteEntry)))
                            {
                                oSalesQuote.Lines.SetCurrentLine(Convert.ToInt16(sItemLine));
                                oSalesQuote.Lines.UserFields.Fields.Item("U_STATUS").Value = "ACCEPT";
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_LAST_UPDAT").Value = DateTime.Now.Date;
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_ACCEPT_DAT").Value = DateTime.Now.Date;
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_ACCEPTBY").Value = Convert.ToString(iRow["UserName"]);

                                if (oSalesQuote.Update() != 0)
                                {
                                    sErrDesc = oDICompany.GetLastErrorDescription(); throw new ArgumentException(sErrDesc);
                                }
                            }
                        }
                        oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                    }
                }

                // Note : Write the comments while doing the 
                sResult = "SUCCESS";
                return sResult;
            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                sResult = Ex.Message.ToString();
                if (oDICompany.InTransaction) oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                throw Ex;
            }
        }

        public string SPA_ProcessCase_Save(DataTable dt)
        {
            string sFuncName = "SPA_ProcessCase_Save";
            string sDocType = string.Empty;
            string sQuoteEntry = string.Empty;
            string sItemLine = string.Empty;
            string sUserRole = string.Empty;
            SAPbobsCOM.Documents oSalesQuote;
            string sRemarks = string.Empty;
            string sItemCode = string.Empty;
            string sVisOrder = string.Empty;
            string sResult = string.Empty;

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Connecting to target company ", sFuncName);
                oDICompany = oLogin.ConnectToTargetCompany(ConnectionString);

                foreach (DataRow iRow in dt.Rows)
                {
                    sDocType = Convert.ToString(iRow["TrnspName"]);
                    sQuoteEntry = Convert.ToString(iRow["DocEntry"]);
                    sItemLine = Convert.ToString(iRow["LineNum"]);
                    sUserRole = Convert.ToString(iRow["ActionBy"]);
                    sRemarks = Convert.ToString(iRow["Remarks"]);
                    sItemCode = Convert.ToString(iRow["ItemCode"]);

                    oSalesQuote = oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oQuotations);

                    if ((sDocType == "ADD-PO-D") || (sDocType == "ADD-PO") || (sDocType == "SDOC") || (sDocType == "TDOC") || (sDocType == "FWDOC") || (sDocType == "MANUAL-FIN") || (sDocType == "MANUAL-FIN-C") || (sDocType == "MANUAL-IC") || (sDocType == "FEES"))
                    {
                        //NEEDED SALES QUOT DOCENTRY,DOCNUM,ITEM LINENUM TO UPDATE REMARKS
                        //NEEDED USER ROLE TO UPDATE

                        if (oSalesQuote.GetByKey(Convert.ToInt16(sQuoteEntry)))
                        {
                            oSalesQuote.Lines.SetCurrentLine(Convert.ToInt16(sItemLine));

                            if (sUserRole == "MG")
                            {
                                oSalesQuote.Lines.UserFields.Fields.Item("U_MG_REMARKS").Value = sRemarks;
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_LAST_UPDAT").Value = DateTime.Now.Date;

                            }
                            else if (sUserRole == "LA")
                            {
                                oSalesQuote.Lines.UserFields.Fields.Item("U_LA_REMARKS").Value = sRemarks;
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_LAST_UPDAT").Value = DateTime.Now.Date;
                            }
                            else if (sUserRole == "PN")
                            {
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PT_REMARKS").Value = sRemarks;
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_LAST_UPDAT").Value = DateTime.Now.Date;
                            }

                            if (oSalesQuote.Update() != 0)
                            {
                                sErrDesc = oDICompany.GetLastErrorDescription(); throw new ArgumentException(sErrDesc);
                            }
                            else
                            {
                                sResult = "SUCCESS";
                            }
                        }
                    }
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS ", sFuncName);

            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw ex;
            }

            return sResult;
        }

        public string SPA_ProcessCase_UpdateStatus(DataTable dt)
        {
            string sFuncName = "SPA_ProcessCase_UpdateStatus";
            string sDocType = string.Empty;
            string sQuoteEntry = string.Empty;
            string sItemCode = string.Empty;
            string sItemLine = string.Empty;
            string sUserRole = string.Empty;
            SAPbobsCOM.Documents oSalesQuote;
            string sStatus = string.Empty;
            string sCaseNo = string.Empty;
            string sActionBy = string.Empty;

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Connecting to target company ", sFuncName);
                oDICompany = oLogin.ConnectToTargetCompany(ConnectionString);

                foreach (DataRow iRow in dt.Rows)
                {
                    sCaseNo = Convert.ToString(iRow["CaseNo"]);
                    sItemCode = Convert.ToString(iRow["ItemCode"]);
                    sDocType = Convert.ToString(iRow["TrnspName"]);
                    sQuoteEntry = Convert.ToString(iRow["DocEntry"]);
                    sItemLine = Convert.ToString(iRow["LineNum"]);
                    sUserRole = Convert.ToString(iRow["UserRole"]);
                    sStatus = Convert.ToString(iRow["Status"]);
                    sActionBy = Convert.ToString(iRow["ActionBy"]);

                    oSalesQuote = oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oQuotations);

                    if ((sActionBy == "MG" || sActionBy == "LA") && (sUserRole == "MG" || sUserRole == "LA"))
                    {
                        if (oSalesQuote.GetByKey(Convert.ToInt16(sQuoteEntry)))
                        {
                            oSalesQuote.Lines.SetCurrentLine(Convert.ToInt16(sItemLine));
                            oSalesQuote.Lines.UserFields.Fields.Item("U_STATUS").Value = sStatus;
                            oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_LAST_UPDAT").Value = DateTime.Now.Date;
                            if (sStatus == "ACCEPT")
                            {
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_ACCEPT_DAT").Value = DateTime.Now.Date;
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_ACCEPTBY").Value = Convert.ToString(iRow["UserName"]);
                                Updateddate(sCaseNo, sItemCode, oDICompany);
                            }
                            else if (sStatus == "FAIL")
                            {
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_FAIL_DAT").Value = DateTime.Now.Date;
                            }
                            if (oSalesQuote.Update() != 0)
                            {
                                sErrDesc = oDICompany.GetLastErrorDescription(); throw new ArgumentException(sErrDesc);
                            }
                        }
                    }

                    else if (sDocType == "FWDOC")
                    {
                        //NEEDED SALES QUOTATION DOCENTRY,LINENUM,DOCNUM TO UPDATE THE STATUS IN LINE LEVEL
                        if (oSalesQuote.GetByKey(Convert.ToInt16(sQuoteEntry)))
                        {
                            oSalesQuote.Lines.SetCurrentLine(Convert.ToInt16(sItemLine));
                            oSalesQuote.Lines.UserFields.Fields.Item("U_STATUS").Value = sStatus;
                            oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_LAST_UPDAT").Value = DateTime.Now.Date;
                            if (sStatus == "ACCEPT")
                            {
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_ACCEPT_DAT").Value = DateTime.Now.Date;
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_ACCEPTBY").Value = Convert.ToString(iRow["UserName"]);
                                Updateddate(sCaseNo, sItemCode, oDICompany);
                            }
                            else if (sStatus == "FAIL")
                            {
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_FAIL_DAT").Value = DateTime.Now.Date;
                            }
                            if (oSalesQuote.Update() != 0)
                            {
                                sErrDesc = oDICompany.GetLastErrorDescription(); throw new ArgumentException(sErrDesc);
                            }
                        }
                    }
                    else if (sDocType == "MANUAL-FIN")
                    {
                        //NEEDED SALES QUOTATION DOCENTRY,LINENUM,DOCNUM TO UPDATE THE STATUS IN LINE LEVEL
                        if (oSalesQuote.GetByKey(Convert.ToInt16(sQuoteEntry)))
                        {
                            oSalesQuote.Lines.SetCurrentLine(Convert.ToInt16(sItemLine));
                            oSalesQuote.Lines.UserFields.Fields.Item("U_STATUS").Value = sStatus;
                            oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_LAST_UPDAT").Value = DateTime.Now.Date;
                            if (sStatus == "ACCEPT")
                            {
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_ACCEPT_DAT").Value = DateTime.Now.Date;
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_ACCEPTBY").Value = Convert.ToString(iRow["UserName"]);
                                Updateddate(sCaseNo, sItemCode, oDICompany);
                            }
                            else if (sStatus == "FAIL")
                            {
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_FAIL_DAT").Value = DateTime.Now.Date;
                            }
                            if (oSalesQuote.Update() != 0)
                            {
                                sErrDesc = oDICompany.GetLastErrorDescription(); throw new ArgumentException(sErrDesc);
                            }
                        }
                    }
                    else if (sDocType == "MANUAL-FIN-C")
                    {
                        //NEEDED SALES QUOTATION DOCENTRY,LINENUM,DOCNUM TO UPDATE THE STATUS IN LINE LEVEL
                        if (oSalesQuote.GetByKey(Convert.ToInt16(sQuoteEntry)))
                        {
                            oSalesQuote.Lines.SetCurrentLine(Convert.ToInt16(sItemLine));
                            oSalesQuote.Lines.UserFields.Fields.Item("U_STATUS").Value = sStatus;
                            oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_LAST_UPDAT").Value = DateTime.Now.Date;
                            if (sStatus == "ACCEPT")
                            {
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_ACCEPT_DAT").Value = DateTime.Now.Date;
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_ACCEPTBY").Value = Convert.ToString(iRow["UserName"]);
                                Updateddate(sCaseNo, sItemCode, oDICompany);
                            }
                            else if (sStatus == "FAIL")
                            {
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_FAIL_DAT").Value = DateTime.Now.Date;
                            }
                            if (oSalesQuote.Update() != 0)
                            {
                                sErrDesc = oDICompany.GetLastErrorDescription(); throw new ArgumentException(sErrDesc);
                            }
                        }
                    }
                    else if (sDocType == "MANUAL-IC")
                    {
                        //NEEDED SALES QUOTATION DOCENTRY,LINENUM,DOCNUM TO UPDATE THE STATUS IN LINE LEVEL
                        if (oSalesQuote.GetByKey(Convert.ToInt16(sQuoteEntry)))
                        {
                            oSalesQuote.Lines.SetCurrentLine(Convert.ToInt16(sItemLine));
                            oSalesQuote.Lines.UserFields.Fields.Item("U_STATUS").Value = sStatus;
                            oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_LAST_UPDAT").Value = DateTime.Now.Date;
                            if (sStatus == "ACCEPT")
                            {
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_ACCEPT_DAT").Value = DateTime.Now.Date;
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_ACCEPTBY").Value = Convert.ToString(iRow["UserName"]);
                                Updateddate(sCaseNo, sItemCode, oDICompany);
                            }
                            else if (sStatus == "FAIL")
                            {
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_FAIL_DAT").Value = DateTime.Now.Date;
                            }
                            if (oSalesQuote.Update() != 0)
                            {
                                sErrDesc = oDICompany.GetLastErrorDescription(); throw new ArgumentException(sErrDesc);
                            }
                        }
                    }
                }
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS ", sFuncName);

            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw ex;
            }

            return "SUCCESS";
        }

        public string SPA_ProcessCase_Alternative(DataTable dt)
        {
            string sFuncName = "SPA_ProcessCase_Alternative";
            string sDocType = string.Empty;

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Connecting to target company ", sFuncName);
                oDICompany = oLogin.ConnectToTargetCompany(ConnectionString);

                foreach (DataRow iRow in dt.Rows)
                {
                    sDocType = Convert.ToString(iRow["TrnspName"]);
                }
                if (sDocType == "ADD-PO-D")
                {
                }
                else if (sDocType == "ADD-PO")
                {
                }
                else if (sDocType == "GDOC")
                {
                }
                else if (sDocType == "SDOC")
                {
                }
                else if (sDocType == "OPDOC")
                {
                }
                else if (sDocType == "TDOC")
                {
                }
                else if (sDocType == "FWDOC")
                {
                }
                else if (sDocType == "MANUAL-FIN")
                {
                }
                else if (sDocType == "MANUAL-FIN-C")
                {
                }
                else if (sDocType == "MANUAL-IC")
                {
                }
                else if (sDocType == "FEES")
                {
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS ", sFuncName);

            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw ex;
            }

            return "SUCCESS";
        }

        public string SPA_ProcessCase_BrowseUpload(DataTable dt, string sFileName, string sStatus)
        {
            string sFuncName = "SPA_ProcessCase_BrowseUpload";
            string sDocType = string.Empty;
            //string sFileName = string.Empty;
            string sQuoteEntry = string.Empty;
            string sItemLine = string.Empty;
            string sItemCode = string.Empty;
            SAPbobsCOM.Documents oSalesQuote;
            //string sStatus = string.Empty;
            DataTable results = new DataTable();
            DataTable results1 = new DataTable();
            int sScanCount = 0;
            string sActionBy = string.Empty;
            string sSql = string.Empty;
            SAPbobsCOM.Recordset oRecSet;
            string sCaseNo = string.Empty;

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);
                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand command = con.CreateCommand();

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Connecting to target company ", sFuncName);
                oDICompany = oLogin.ConnectToTargetCompany(ConnectionString);

                foreach (DataRow iRow in dt.Rows)
                {
                    sCaseNo = Convert.ToString(iRow["CaseNo"]);
                    sDocType = Convert.ToString(iRow["TrnspName"]);
                    sQuoteEntry = Convert.ToString(iRow["DocEntry"]);
                    sItemLine = Convert.ToString(iRow["LineNum"]);
                    sItemCode = Convert.ToString(iRow["ItemCode"]);

                    if (sFileName != string.Empty)
                    {
                        oSalesQuote = oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oQuotations);

                        if (sDocType == "ADD-PO-D")
                        {
                            //NEED TO CALL GOPI'S PROGRAM VALIDATE FILE WITH ITEM CODE AND SET THE RESULT TO THE VARIABLE 
                            //sStatus = "0";
                            if (sStatus == "0")
                            {
                                sStatus = "ACCEPT";
                            }
                            else
                            {
                                sStatus = "FAIL";
                            }

                            if (oSalesQuote.GetByKey(Convert.ToInt16(sQuoteEntry)))
                            {
                                oSalesQuote.Lines.SetCurrentLine(Convert.ToInt16(sItemLine));
                                oSalesQuote.Lines.UserFields.Fields.Item("U_NEXT_ACTION_BY").Value = "IC";
                                if (sStatus == "ACCEPT")
                                {
                                    oSalesQuote.Lines.UserFields.Fields.Item("U_RESULTS_FILE").Value = sFileName;
                                    oSalesQuote.Lines.UserFields.Fields.Item("U_STATUS").Value = sStatus;
                                    Updateddate(sCaseNo, sItemCode, oDICompany);
                                }
                                else if (sStatus == "FAIL")
                                {
                                    oSalesQuote.Lines.UserFields.Fields.Item("U_RESULTS_FILE").Value = sFileName;
                                    oSalesQuote.Lines.UserFields.Fields.Item("U_STATUS").Value = sStatus;
                                    sSql = "SELECT U_FAIL_CLOSE FROM OITM WITH (NOLOCK) WHERE ItemCode = '" + sItemCode + "'";
                                    oRecSet = oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                                    oRecSet.DoQuery(sSql);
                                    if (oRecSet.RecordCount > 0)
                                    {
                                        oSalesQuote.Lines.UserFields.Fields.Item("U_NEXT_ACTION_BY").Value = oRecSet.Fields.Item("U_FAIL_CLOSE").Value;
                                    }
                                }

                                if (oSalesQuote.Update() != 0)
                                {
                                    sErrDesc = oDICompany.GetLastErrorDescription(); throw new ArgumentException(sErrDesc);
                                }
                            }
                        }
                        else if (sDocType == "ADD-PO")
                        {
                            //NEED TO CALL GOPI'S PROGRAM VALIDATE FILE WITH ITEM CODE AND SET THE RESULT TO THE VARIABLE 
                            //sStatus = "0";
                            if (sStatus == "0")
                            {
                                sStatus = "ACCEPT";
                            }
                            else
                            {
                                sStatus = "FAIL";
                            }

                            if (oSalesQuote.GetByKey(Convert.ToInt16(sQuoteEntry)))
                            {
                                oSalesQuote.Lines.SetCurrentLine(Convert.ToInt16(sItemLine));
                                oSalesQuote.Lines.UserFields.Fields.Item("U_NEXT_ACTION_BY").Value = "IC";
                                if (sStatus == "ACCEPT")
                                {
                                    oSalesQuote.Lines.UserFields.Fields.Item("U_RESULTS_FILE").Value = sFileName;
                                    oSalesQuote.Lines.UserFields.Fields.Item("U_STATUS").Value = sStatus;
                                    Updateddate(sCaseNo, sItemCode, oDICompany);
                                }
                                else if (sStatus == "FAIL")
                                {
                                    oSalesQuote.Lines.UserFields.Fields.Item("U_RESULTS_FILE").Value = sFileName;
                                    oSalesQuote.Lines.UserFields.Fields.Item("U_STATUS").Value = sStatus;
                                    sSql = "SELECT U_FAIL_CLOSE FROM OITM WITH (NOLOCK) WHERE ItemCode = '" + sItemCode + "'";
                                    oRecSet = oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                                    oRecSet.DoQuery(sSql);
                                    if (oRecSet.RecordCount > 0)
                                    {
                                        oSalesQuote.Lines.UserFields.Fields.Item("U_NEXT_ACTION_BY").Value = oRecSet.Fields.Item("U_FAIL_CLOSE").Value;
                                    }
                                }

                                if (oSalesQuote.Update() != 0)
                                {
                                    sErrDesc = oDICompany.GetLastErrorDescription(); throw new ArgumentException(sErrDesc);
                                }
                            }
                        }
                        else if (sDocType == "SDOC")
                        {
                            command.CommandText = "select IsNull(U_ScanCount,0) [ScanCount] from QUT1 WITH (NOLOCK) where DocEntry = '" + sQuoteEntry + "' and LineNum = '" + sItemLine + "'";
                            con.Open();
                            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                            dataAdapter.Fill(results);
                            con.Close();
                            if (results.Rows.Count > 0)
                            {
                                sScanCount = Convert.ToInt32(results.Rows[0][0]);
                            }

                            //CALL GOPI'S PROGRAM TO GET THE FILE NAME AFTER UPLOAD
                            //sFileName = "CS_UPLOAD_FILE_20151016";

                            //CALL GOPI'S PROGRAM TO GET THE STATUS 
                            sStatus = "0";
                            if (sStatus == "0")
                            {
                                sStatus = "ACCEPT";
                            }
                            else
                            {
                                sStatus = "FAIL";
                            }

                            if (oSalesQuote.GetByKey(Convert.ToInt16(sQuoteEntry)))
                            {
                                oSalesQuote.Lines.SetCurrentLine(Convert.ToInt16(sItemLine));
                                oSalesQuote.Lines.UserFields.Fields.Item("U_RESULTS_FILE").Value = sFileName;
                                oSalesQuote.Lines.UserFields.Fields.Item("U_STATUS").Value = sStatus;
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_LAST_UPDAT").Value = DateTime.Now.Date;
                                if (sStatus == "ACCEPT")
                                {
                                    oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_ACCEPT_DAT").Value = DateTime.Now.Date;
                                    oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_ACCEPTBY").Value = Convert.ToString(iRow["UserName"]);
                                    Updateddate(sCaseNo, sItemCode, oDICompany);
                                }
                                else if (sStatus == "FAIL")
                                {
                                    oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_FAIL_DAT").Value = DateTime.Now.Date;
                                    if (sScanCount > 5)
                                    {
                                        command.CommandText = "select U_FAIL_CLOSE from OITM WITH (NOLOCK) where ItemCode = '" + sItemCode + "'";
                                        con.Open();
                                        SqlDataAdapter dataAdapter1 = new SqlDataAdapter(command);
                                        dataAdapter1.Fill(results1);
                                        con.Close();
                                        if (results1.Rows.Count > 0)
                                        {
                                            sActionBy = Convert.ToString(results1.Rows[0][0]);
                                        }

                                    }
                                    oSalesQuote.Lines.UserFields.Fields.Item("U_NEXT_ACTION_BY").Value = sActionBy;
                                    oSalesQuote.Lines.UserFields.Fields.Item("U_ScanCount").ValidValue = Convert.ToString(sScanCount + 1);
                                }
                                if (oSalesQuote.Update() != 0)
                                {
                                    sErrDesc = oDICompany.GetLastErrorDescription(); throw new ArgumentException(sErrDesc);
                                }
                            }
                        }
                        else if (sDocType == "OPDOC")
                        {
                            //CALL GOPI'S PROGRAM TO GET THE FILE NAME AFTER UPLOAD
                            //sFileName = "CS_UPLOAD_FILE_20151016";

                            if (oSalesQuote.GetByKey(Convert.ToInt16(sQuoteEntry)))
                            {
                                oSalesQuote.Lines.SetCurrentLine(Convert.ToInt16(sItemLine));
                                oSalesQuote.Lines.UserFields.Fields.Item("U_RESULTS_FILE").Value = sFileName;
                                oSalesQuote.Lines.UserFields.Fields.Item("U_STATUS").Value = "ACCEPT";
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_LAST_UPDAT").Value = DateTime.Now.Date;
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_ACCEPT_DAT").Value = DateTime.Now.Date;
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_ACCEPTBY").Value = Convert.ToString(iRow["UserName"]);
                                Updateddate(sCaseNo, sItemCode, oDICompany);

                                if (oSalesQuote.Update() != 0)
                                {
                                    sErrDesc = oDICompany.GetLastErrorDescription(); throw new ArgumentException(sErrDesc);
                                }
                            }
                        }
                        else if (sDocType == "TDOC")
                        {
                            //CALL GOPI'S PROGRAM TO GET THE FILE NAME AFTER UPLOAD
                            //sFileName = "CS_UPLOAD_FILE_20151016";

                            //CALL GOPI'S PROGRAM TO VERIFY THE DOCUMENT AND SAVE THE RESULT TO THE VARIABLE
                            sStatus = "0";
                            if (sStatus == "0")
                            {
                                sStatus = "ACCEPT";
                            }
                            else
                            {
                                sStatus = "FAIL";
                            }

                            if (oSalesQuote.GetByKey(Convert.ToInt16(sQuoteEntry)))
                            {
                                oSalesQuote.Lines.SetCurrentLine(Convert.ToInt16(sItemLine));
                                oSalesQuote.Lines.UserFields.Fields.Item("U_RESULTS_FILE").Value = sFileName;
                                oSalesQuote.Lines.UserFields.Fields.Item("U_STATUS").Value = sStatus;
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_LAST_UPDAT").Value = DateTime.Now.Date;
                                if (sStatus == "ACCEPT")
                                {
                                    oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_ACCEPT_DAT").Value = DateTime.Now.Date;
                                    oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_ACCEPTBY").Value = Convert.ToString(iRow["UserName"]);
                                    Updateddate(sCaseNo, sItemCode, oDICompany);
                                }
                                else if (sStatus == "FAIL")
                                {
                                    oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_FAIL_DAT").Value = DateTime.Now.Date;
                                }
                                if (oSalesQuote.Update() != 0)
                                {
                                    sErrDesc = oDICompany.GetLastErrorDescription(); throw new ArgumentException(sErrDesc);
                                }
                            }

                        }
                    }
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS ", sFuncName);

            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw ex;
            }

            return "SUCCESS";
        }

        public string SPA_ProcessCase_BrowseGenerate(string sFileName, DataTable dt)
        {
            string sFuncName = "SPA_ProcessCase_BrowseGenerate";
            string sReturnResult = string.Empty;
            string sDocType = string.Empty;
            string sQuoteEntry = string.Empty;
            string sItemLine = string.Empty;
            SAPbobsCOM.Documents oSalesQuote;
            string sCaseNo = string.Empty;
            string sItemCode = string.Empty;

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Connecting to target company ", sFuncName);
                oDICompany = oLogin.ConnectToTargetCompany(ConnectionString);

                foreach (DataRow iRow in dt.Rows)
                {
                    sCaseNo = Convert.ToString(iRow["CaseNo"]);
                    sItemCode = Convert.ToString(iRow["ItemCode"]);
                    sDocType = Convert.ToString(iRow["TrnspName"]);
                    sQuoteEntry = Convert.ToString(iRow["DocEntry"]);
                    sItemLine = Convert.ToString(iRow["LineNum"]);

                    //AFTER CALLING GOPI'S PROGRAM TO GENERATE PDF FILE THE FILE NAME TO BE SAVED IN sFileName VARIABLE
                    //sFileName = "CS_ITM_20151016";

                    if (sFileName != string.Empty)
                    {
                        oSalesQuote = oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oQuotations);

                        if (sDocType == "GDOC")
                        {
                            if (oSalesQuote.GetByKey(Convert.ToInt16(sQuoteEntry)))
                            {
                                oSalesQuote.Lines.SetCurrentLine(Convert.ToInt16(sItemLine));
                                oSalesQuote.Lines.UserFields.Fields.Item("U_RESULTS_FILE").Value = sFileName;
                                oSalesQuote.Lines.UserFields.Fields.Item("U_STATUS").Value = "ACCEPT";
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_LAST_UPDAT").Value = DateTime.Now.Date;
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_ACCEPT_DAT").Value = DateTime.Now.Date;
                                oSalesQuote.Lines.UserFields.Fields.Item("U_PROCESS_ACCEPTBY").Value = Convert.ToString(iRow["UserName"]);
                                Updateddate(sCaseNo, sItemCode, oDICompany);

                                if (oSalesQuote.Update() != 0)
                                {
                                    sErrDesc = oDICompany.GetLastErrorDescription(); throw new ArgumentException(sErrDesc);
                                }
                            }
                        }
                    }
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS ", sFuncName);

            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw ex;
            }

            return "SUCCESS";
        }

        public string CreateSalesQuote(DataTable oDt, SAPbobsCOM.Company oCompany)
        {
            string sFuncName = "CreateSalesQuote";
            SAPbobsCOM.Documents oSalesQuote;
            string sSql;
            int sumOfQty = 0;
            string sReturnResult = string.Empty;
            SAPbobsCOM.Recordset oRecSet;
            string sCaseNo = string.Empty;
            int sQuoteEntry = 0;

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                oSalesQuote = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oQuotations);
                double iCount = 0;

                sCaseNo = Convert.ToString(oDt.Rows[0]["CaseNo"]);

                sSql = "SELECT DocEntry FROM OQUT WITH (NOLOCK) WHERE CardCode = '" + sCaseNo + "' AND DocStatus = 'O' AND CANCELED = 'N'";
                oRecSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecSet.DoQuery(sSql);
                if (oRecSet.RecordCount > 0)
                {
                    sQuoteEntry = oRecSet.Fields.Item("DocEntry").Value;
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecSet);

                if (oSalesQuote.GetByKey(sQuoteEntry))
                {
                    foreach (DataRow iRow in oDt.Rows)
                    {
                        if (Convert.ToDouble(iRow["Qty"]) > 0)
                        {
                            if (oSalesQuote.Lines.Count > 0)
                            {
                                oSalesQuote.Lines.Add();
                            }
                            oSalesQuote.Lines.ItemCode = Convert.ToString(iRow["ItemCode"]);
                            oSalesQuote.Lines.Quantity = Convert.ToDouble(iRow["Qty"]);
                            oSalesQuote.Lines.Price = Convert.ToDouble(iRow["Price"]);
                            oSalesQuote.Lines.UnitPrice = Convert.ToDouble(iRow["Price"]);

                            oSalesQuote.Lines.UserFields.Fields.Item("U_STEP_CREATION_DT").Value = DateTime.Now.Date;
                            oSalesQuote.Lines.UserFields.Fields.Item("U_INT_CASE_SEQ").Value = Convert.ToString(iRow["IntNo"]);
                            oSalesQuote.Lines.UserFields.Fields.Item("U_NEXT_ACTION_BY").Value = Convert.ToString(iRow["ActionBy"]);

                            sSql = "SELECT U_CASE_BRANCH,U_LOAN_MSTR_BANKCODE,ProjectCod FROM OCRD WITH (NOLOCK) WHERE CardCode = '" + sCaseNo + "'";
                            oRecSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                            oRecSet.DoQuery(sSql);
                            if (oRecSet.RecordCount > 0)
                            {
                                oSalesQuote.Lines.COGSCostingCode = oRecSet.Fields.Item("U_CASE_BRANCH").Value;
                                oSalesQuote.Lines.COGSCostingCode2 = oRecSet.Fields.Item("U_LOAN_MSTR_BANKCODE").Value;
                                oSalesQuote.Lines.ProjectCode = oRecSet.Fields.Item("ProjectCod").Value;
                            }

                            sumOfQty = sumOfQty + Convert.ToInt32(iRow["Qty"]);
                        }
                    }
                    if (sumOfQty > 0)
                    {
                        if (oSalesQuote.Update() != 0)
                        {
                            sErrDesc = oCompany.GetLastErrorDescription();
                            sReturnResult = sErrDesc.ToString();
                            throw new ArgumentException(sErrDesc);
                        }
                        else
                        {
                            sReturnResult = "SUCCESS";
                            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS ", sFuncName);
                        }
                    }
                    else
                    {
                        sReturnResult = "";
                    }
                }
                else
                {
                    foreach (DataRow iRow in oDt.Rows)
                    {
                        if (Convert.ToDouble(iRow["Qty"]) > 0)
                        {

                            oSalesQuote.CardCode = Convert.ToString(iRow["CaseNo"]);
                            oSalesQuote.DocDate = DateTime.Now.Date;
                            oSalesQuote.TaxDate = DateTime.Now.Date;
                            oSalesQuote.DocDueDate = DateTime.Now.Date;

                            if (iCount > 0)
                            {
                                oSalesQuote.Lines.Add();
                            }

                            oSalesQuote.Lines.ItemCode = Convert.ToString(iRow["ItemCode"]);
                            oSalesQuote.Lines.Quantity = Convert.ToDouble(iRow["Qty"]);
                            oSalesQuote.Lines.Price = Convert.ToDouble(iRow["Price"]);
                            oSalesQuote.Lines.UnitPrice = Convert.ToDouble(iRow["Price"]);

                            oSalesQuote.Lines.UserFields.Fields.Item("U_STEP_CREATION_DT").Value = DateTime.Now.Date;
                            oSalesQuote.Lines.UserFields.Fields.Item("U_INT_CASE_SEQ").Value = Convert.ToString(iRow["IntNo"]);
                            oSalesQuote.Lines.UserFields.Fields.Item("U_NEXT_ACTION_BY").Value = Convert.ToString(iRow["ActionBy"]);

                            sSql = "SELECT U_CASE_BRANCH,U_LOAN_MSTR_BANKCODE,ProjectCod FROM OCRD WITH (NOLOCK) WHERE CardCode = '" + Convert.ToString(iRow["CaseNo"]) + "'";
                            oRecSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                            oRecSet.DoQuery(sSql);
                            if (oRecSet.RecordCount > 0)
                            {
                                oSalesQuote.Lines.COGSCostingCode = oRecSet.Fields.Item("U_CASE_BRANCH").Value;
                                oSalesQuote.Lines.COGSCostingCode2 = oRecSet.Fields.Item("U_LOAN_MSTR_BANKCODE").Value;
                                oSalesQuote.Lines.ProjectCode = oRecSet.Fields.Item("ProjectCod").Value;
                            }

                            sumOfQty = sumOfQty + Convert.ToInt32(iRow["Qty"]);
                            iCount = iCount + 1;
                        }
                    }

                    if (sumOfQty > 0)
                    {
                        if (oSalesQuote.Add() != 0)
                        {
                            sErrDesc = oCompany.GetLastErrorDescription();
                            sReturnResult = sErrDesc.ToString();
                            throw new ArgumentException(sErrDesc);
                        }
                        else
                        {
                            sReturnResult = "SUCCESS";
                            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS ", sFuncName);
                        }
                    }
                    else
                    {
                        sReturnResult = "";
                    }
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw ex;
            }
            return sReturnResult;
        }

        public string AddDocuments(DataTable oDt, SAPbobsCOM.Company oCompany, string sDocType)
        {
            string sFuncName = "AddSales_Order";
            string sItemCode;
            SAPbobsCOM.Documents oSalesOrder;
            SAPbobsCOM.Recordset oRecSet;
            string sSoDocEntry;
            string sSql;
            int iRetCode;
            double dPrice = 0;
            double dFee = 0;

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                oSalesOrder = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders);
                double iCount = 0;

                foreach (DataRow iRow in oDt.Rows)
                {
                    if (Convert.ToDouble(iRow["Qty"]) > 0)
                    {
                        oSalesOrder.CardCode = Convert.ToString(iRow["CaseNo"]);
                        oSalesOrder.DocDate = DateTime.Now.Date;
                        oSalesOrder.TaxDate = DateTime.Now.Date;
                        oSalesOrder.DocDueDate = DateTime.Now.Date;
                        oSalesOrder.NumAtCard = Convert.ToString(iRow["DocNum"]) + "/" + Convert.ToString(iRow["LineNum"]);

                        if (iCount > 0)
                        {
                            oSalesOrder.Lines.Add();
                        }

                        sSql = "select U_PROPERTY_PURPRC [Price] from [OCRD] WITH (NOLOCK) where CardCode = '" + Convert.ToString(iRow["CaseNo"]) + "'";
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("OCRD Price - SQL Query" + sSql, sFuncName);
                        oRecSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                        oRecSet.DoQuery(sSql);
                        if (oRecSet.RecordCount > 0)
                        {
                            dPrice = Convert.ToDouble(oRecSet.Fields.Item("Price").Value);
                        }

                        sSql = "SELECT DBO.GETFEE('" + Convert.ToString(iRow["ItemCode"]) + "','" + Convert.ToString(iRow["CaseNo"]) + "', '" + dPrice + "','','','') [FeePrice]";
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("GetFEE - SQL Query" + sSql, sFuncName);
                        oRecSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                        oRecSet.DoQuery(sSql);
                        if (oRecSet.RecordCount > 0)
                        {
                            dFee = Convert.ToDouble(oRecSet.Fields.Item("FeePrice").Value);
                        }

                        sItemCode = Convert.ToString(iRow["ItemCode"]);
                        oSalesOrder.Lines.ItemCode = sItemCode;
                        oSalesOrder.Lines.Quantity = Convert.ToDouble(iRow["Qty"]);
                        //oSalesOrder.Lines.Price = Convert.ToDouble(iRow["Price"]);
                        //oSalesOrder.Lines.UnitPrice = Convert.ToDouble(iRow["Price"]);
                        oSalesOrder.Lines.Price = dFee;
                        oSalesOrder.Lines.UnitPrice = dFee;

                        sSql = "SELECT U_CASE_BRANCH,U_LOAN_MSTR_BANKCODE,ProjectCod FROM OCRD WITH (NOLOCK) WHERE CardCode = '" + Convert.ToString(iRow["CaseNo"]) + "'";
                        oRecSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                        oRecSet.DoQuery(sSql);
                        if (oRecSet.RecordCount > 0)
                        {
                            oSalesOrder.Lines.COGSCostingCode = oRecSet.Fields.Item("U_CASE_BRANCH").Value;
                            oSalesOrder.Lines.COGSCostingCode2 = oRecSet.Fields.Item("U_LOAN_MSTR_BANKCODE").Value;
                            oSalesOrder.Lines.ProjectCode = oRecSet.Fields.Item("ProjectCod").Value;
                        }

                        iCount = iCount + 1;
                    }
                }

                iRetCode = oSalesOrder.Add();
                if (iRetCode != 0)
                {
                    sErrDesc = oCompany.GetLastErrorDescription(); throw new ArgumentException(sErrDesc);
                }
                else
                {
                    sSoDocEntry = oCompany.GetNewObjectKey();
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS ", sFuncName);
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw ex;
            }

            return sSoDocEntry;
        }

        public string CreatePO(string sSoDocEntry, SAPbobsCOM.Company oCompany, DataTable dt)
        {
            string sFuncName = "CreatePO";
            string sPoEntry = string.Empty;
            SAPbobsCOM.Documents objPO;
            string sItemLine = string.Empty;
            string sSql;
            string sCardCode = string.Empty;

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                foreach (DataRow iRow in dt.Rows)
                {
                    sItemLine = Convert.ToString(iRow["LineNum"]);

                    SAPbobsCOM.Recordset oRecSet;

                    sSql = "SELECT CardCode FROM OITM WITH (NOLOCK) WHERE ItemCode = '" + Convert.ToString(iRow["ItemCode"]) + "'";
                    oRecSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRecSet.DoQuery(sSql);
                    if (oRecSet.RecordCount > 0)
                    {
                        sCardCode = oRecSet.Fields.Item("CardCode").Value;
                    }
                    else
                    {
                        sCardCode = "";
                    }

                    if (sCardCode != string.Empty)
                    {
                        if (Convert.ToDouble(iRow["Qty"]) > 0)
                        {
                            objPO = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseOrders);

                            objPO.CardCode = sCardCode;
                            objPO.DocDate = DateTime.Now.Date;
                            objPO.TaxDate = DateTime.Now.Date;
                            objPO.DocDueDate = DateTime.Now.Date;
                            objPO.NumAtCard = Convert.ToString(iRow["CaseNo"]);

                            int iCount = 0;

                            if (iCount != 0)
                            {
                                objPO.Lines.Add();
                            }
                            objPO.Lines.ItemCode = Convert.ToString(iRow["ItemCode"]);
                            objPO.Lines.Price = Convert.ToDouble(iRow["Price"]);
                            objPO.Lines.UnitPrice = Convert.ToDouble(iRow["Price"]);
                            objPO.Lines.Quantity = Convert.ToDouble(iRow["Qty"]);


                            sSql = "SELECT U_CASE_BRANCH,U_LOAN_MSTR_BANKCODE,ProjectCod FROM OCRD WITH (NOLOCK) WHERE CardCode = '" + sCardCode + "'";
                            oRecSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                            oRecSet.DoQuery(sSql);
                            if (oRecSet.RecordCount > 0)
                            {
                                objPO.Lines.ProjectCode = oRecSet.Fields.Item("ProjectCod").Value;
                                objPO.Lines.COGSCostingCode = oRecSet.Fields.Item("U_CASE_BRANCH").Value;
                                objPO.Lines.COGSCostingCode2 = oRecSet.Fields.Item("U_LOAN_MSTR_BANKCODE").Value;
                            }

                            iCount = iCount + 1;

                            if (objPO.Add() != 0)
                            {
                                sErrDesc = oCompany.GetLastErrorDescription(); throw new ArgumentException(sErrDesc);
                            }
                            else
                            {
                                sPoEntry = oCompany.GetNewObjectKey();

                                //UPDATE THE SALES ODER WITH PURCHASE ORDER DETAILS
                                sSql = "UPDATE RDR1 SET PoTrgNum = (SELECT DocNum FROM OPOR WITH (NOLOCK) WHERE DocEntry = '" + sPoEntry + "'), PoTrgEntry = '" + sPoEntry + "', PoLineNum = '" + sItemLine + "' WHERE DocEntry = '" + sSoDocEntry + "'";
                                oRecSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                                oRecSet.DoQuery(sSql);

                                sSql = "UPDATE POR1 SET BaseType = 17, BaseEntry = '" + sSoDocEntry + "', BaseLine = '" + sItemLine + "',BaseRef = (SELECT DocNum FROM ORDR WITH (NOLOCK) WHERE DocEntry = '" + sSoDocEntry + "') WHERE DocEntry = '" + sPoEntry + "'";
                                oRecSet.DoQuery(sSql);
                            }
                        }

                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS ", sFuncName);
                    }
                }

            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw ex;
            }

            return sPoEntry;
        }

        public string CreateAPInv(string sPoEntry, SAPbobsCOM.Company oCompany, string sCaseNo)
        {
            string sFuncName = "CreateAPInv";
            string sSql;
            string sAPInvEntry = string.Empty;
            SAPbobsCOM.Recordset oRecSet;
            SAPbobsCOM.Documents oAPInv;

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (sPoEntry != string.Empty)
                {
                    sSql = "SELECT A.CardCode,B.ItemCode,B.Quantity,B.Price,B.CogsOcrCod,B.CogsOcrCo2,B.Project,B.ObjType,B.LineNum ";
                    sSql += " FROM OPOR A WITH (NOLOCK) ";
                    sSql += " INNER JOIN POR1 B WITH (NOLOCK) ON B.DocEntry = A.DocEntry ";
                    sSql += " WHERE A.DocEntry = '" + sPoEntry + "' ";
                    oRecSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRecSet.DoQuery(sSql);
                    if (oRecSet.RecordCount > 0)
                    {
                        oAPInv = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseInvoices);

                        oAPInv.CardCode = oRecSet.Fields.Item("CardCode").Value;
                        oAPInv.DocDate = DateTime.Now.Date;
                        oAPInv.TaxDate = DateTime.Now.Date;
                        oAPInv.DocDueDate = DateTime.Now.Date;
                        oAPInv.NumAtCard = sCaseNo.ToString();

                        int iCount = 0;

                        if (iCount != 0)
                        {
                            oAPInv.Lines.Add();
                        }
                        oAPInv.Lines.ItemCode = oRecSet.Fields.Item("ItemCode").Value;
                        oAPInv.Lines.Quantity = oRecSet.Fields.Item("Quantity").Value;
                        oAPInv.Lines.Price = oRecSet.Fields.Item("Price").Value;
                        oAPInv.Lines.UnitPrice = oRecSet.Fields.Item("Price").Value;
                        oAPInv.Lines.COGSCostingCode = oRecSet.Fields.Item("CogsOcrCod").Value;
                        oAPInv.Lines.COGSCostingCode2 = oRecSet.Fields.Item("CogsOcrCo2").Value;
                        oAPInv.Lines.ProjectCode = oRecSet.Fields.Item("Project").Value;
                        oAPInv.Lines.BaseEntry = Convert.ToInt16(sPoEntry);
                        oAPInv.Lines.BaseType = Convert.ToInt16(oRecSet.Fields.Item("ObjType").Value);
                        oAPInv.Lines.BaseLine = Convert.ToInt16(oRecSet.Fields.Item("LineNum").Value);
                        iCount = iCount + 1;

                        if (oAPInv.Add() != 0)
                        {
                            sErrDesc = oCompany.GetLastErrorDescription(); throw new ArgumentException(sErrDesc);
                        }
                        else
                        {
                            sAPInvEntry = oCompany.GetNewObjectKey();
                        }
                    }

                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS ", sFuncName);
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw ex;
            }
            return sAPInvEntry;
        }

        public string Updateddate(string sCaseNo, string sItemCode, SAPbobsCOM.Company oCompany)
        {
            string sFuncName = "GetUpdateddate";
            string sSql;
            SAPbobsCOM.Recordset oRecSet;

            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                sSql = "DECLARE @V_CASENO NVARCHAR(MAX),@V_ITEMCODE NVARCHAR(MAX) ";
                sSql += " DECLARE @V_TABLENAME NVARCHAR(MAX),@V_COLUMN NVARCHAR(MAX)";
                sSql += " SET @V_CASENO = '" + sCaseNo + "' ";
                sSql += " SET @V_ITEMCODE = '" + sItemCode + "'";
                sSql += " SELECT @V_TABLENAME = SUBSTRING(U_UPDATE_CASE_DATE,0,CHARINDEX('.',U_UPDATE_CASE_DATE,1)) , ";
                sSql += " @V_COLUMN = SUBSTRING(U_UPDATE_CASE_DATE,CHARINDEX('.',U_UPDATE_CASE_DATE,1) + 1,LEN(U_UPDATE_CASE_DATE)) ";
                sSql += " FROM OITM WITH (NOLOCK) WHERE ItemCode = @V_ITEMCODE ";
                sSql += " AND ISNULL(U_UPDATE_CASE_DATE,'') <> '' ";
                sSql += "  IF(ISNULL(@V_TABLENAME,'') != '' AND ISNULL(@V_COLUMN,'') != '') ";
                sSql += " BEGIN ";
                sSql += " EXEC (' ";
                sSql += " UPDATE '+ @V_TABLENAME +' SET '+ @V_COLUMN +' = CONVERT(CHAR(10),GETDATE(),120) WHERE '+ @V_TABLENAME +'.CardCode = '''+@V_CASENO+''' ";
                sSql += " ')  END";
                oRecSet = oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecSet.DoQuery(sSql);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS ", sFuncName);
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw ex;
            }
            return "SUCCESS";
        }

        public string CloseCase(string sCaseNo, string sUserRole, string sStatus, string sKIV)
        {
            string sFuncName = "CloseCase";
            string sSql = string.Empty;
            SAPbobsCOM.Recordset oRecSet;
            string sResult = string.Empty;
            double lRetCode = 0;
            try
            {
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Connecting to target company ", sFuncName);
                oDICompany = oLogin.ConnectToTargetCompany(ConnectionString);

                if (sStatus == "CLOSED")
                {
                    sSql = "EXEC AE_UPDATECASESTATUS '" + sCaseNo + "','" + sUserRole + "'";
                    oRecSet = oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRecSet.DoQuery(sSql);
                    if (oRecSet.RecordCount > 0)
                    {
                        sResult = oRecSet.Fields.Item("Result").Value;
                    }

                }
                else
                {
                    SAPbobsCOM.BusinessPartners oBP = (SAPbobsCOM.BusinessPartners)(oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners));

                    oBP.GetByKey(sCaseNo);
                    if (sStatus.ToString() != strSelect.ToString())
                    {
                        oBP.UserFields.Fields.Item("U_CASESTATUS").Value = sStatus.ToString();
                    }
                    oBP.UserFields.Fields.Item("U_KIVSTATUS").Value = sKIV.ToString();
                    lRetCode = oBP.Update();
                    if (lRetCode == 0)
                    {
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Case Header details Updated Successfully ", sFuncName);
                    }
                    sResult = "SUCCESS";
                }
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS  ", sFuncName);
                return sResult;
            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw Ex;
            }
        }

        public string SPA_ProcessCase_GetFilePath()
        {
            string sFuncName = string.Empty;
            string sReturnResult = string.Empty;
            DataTable results = new DataTable();
            try
            {
                sFuncName = "SPA_ProcessCase_GetFilePath";
                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand command = con.CreateCommand();
                command.CommandText = "select U_SETUP from [@AE_SETUP] WITH (NOLOCK) where Code = 'FILEPATH'";
                con.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(results);
                con.Close();
                if (results != null && results.Rows.Count > 0)
                {
                    sReturnResult = results.Rows[0][0].ToString();
                }
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

        public string SPA_ProcessCase_UpdatePathtoSAP(string sFile, string sCaseNo, string sItemCode, string sDocEntry, string sDocNum, string sLineNum)
        {
            string sFuncName = string.Empty;
            string sReturnResult = string.Empty;
            DataTable results = new DataTable();
            try
            {
                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand command = con.CreateCommand();
                command.CommandText = "Update [QUT1] SET U_RESULTS_FILE ='" + sFile + "' where ItemCode = '" + sItemCode + "' and LineNum = '" + sLineNum + "' and " +
                                      "DocEntry IN (select DocEntry from [OQUT] WITH (NOLOCK) where CardCode = '" + sCaseNo + "' and DocNum = '" + sDocNum + "')";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Executing the Query : " + command.CommandText, sFuncName);

                con.Open();
                command.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw Ex;
            }

            return "SUCCESS";
        }

        public string SPA_ProcessCase_SaveOptionalItems(DataTable oDTSQData)
        {
            // This method is to create the sales Quotation line items for the Optional Items
            string sReturnResult = string.Empty;
            string sFuncName = string.Empty;
            SAPbobsCOM.Documents oSalesQuotation;
            int lRetCode;
            string sSql = string.Empty;
            SAPbobsCOM.Recordset oRecSet;
            try
            {
                sFuncName = "Add_SalesQuotation()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Connecting to target company ", sFuncName);
                oDICompany = oLogin.ConnectToTargetCompany(ConnectionString);

                oSalesQuotation = oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oQuotations);

                DataRow iRow = oDTSQData.Rows[0];

                sSql = "SELECT ItemCode FROM QUT1 WITH (NOLOCK) WHERE ItemCode = '" + iRow["ItemCode"].ToString() + "' and DocEntry in (select DocEntry  from OQUT WITH (NOLOCK) where CardCode = '" + iRow["CaseNo"].ToString() + "')";
                oRecSet = oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                oRecSet.DoQuery(sSql);
                if (oRecSet.RecordCount > 0)
                {
                    sReturnResult = "Sales Quotation line is already exists in the cardcode";
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Sales Quotation line is already exists for this cardcode", sFuncName);
                }
                else
                {
                    sSql = "select DocEntry from OQUT WITH (NOLOCK) where CardCode = '" + iRow["CaseNo"].ToString() + "'";
                    oRecSet = oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRecSet.DoQuery(sSql);
                    var docEntry = string.Empty;
                    if (oRecSet.RecordCount > 0)
                    {
                        docEntry = Convert.ToString(oRecSet.Fields.Item("DocEntry").Value);
                    }
                    oSalesQuotation.GetByKey(Convert.ToInt32(docEntry));
                    //oSalesQuotation.CardCode = iRow["CaseNo"].ToString();
                    //oSalesQuotation.DocDate = DateTime.Now.Date;
                    //oSalesQuotation.TaxDate = DateTime.Now.Date;
                    //oSalesQuotation.DocDueDate = DateTime.Now.Date;

                    double iCount = 0;
                    int sumOfQty = 0;

                    if (Convert.ToDouble(iRow["Qty"]) > 0)
                    {
                        sSql = "SELECT TOP 1 U_INT_CASE_SEQ [IntNo] FROM QUT1 WITH (NOLOCK) WHERE DocEntry in (select DocEntry  from OQUT WITH (NOLOCK) where CardCode = '" + iRow["CaseNo"].ToString() + "') and LineStatus = 'O' and U_INT_CASE_SEQ <> 0";
                        oRecSet = oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                        oRecSet.DoQuery(sSql);
                        int SeqNo = 0;
                        if (oRecSet.RecordCount > 0)
                        {
                            SeqNo = Convert.ToInt32(oRecSet.Fields.Item("IntNo").Value);
                        }
                        oSalesQuotation.Lines.Add();
                        oSalesQuotation.Lines.ItemCode = Convert.ToString(iRow["ItemCode"]);
                        oSalesQuotation.Lines.Quantity = Convert.ToInt32(iRow["Qty"]);
                        oSalesQuotation.Lines.Price = Convert.ToDouble(iRow["Price"]);

                        oSalesQuotation.Lines.UserFields.Fields.Item("U_STEP_CREATION_DT").Value = DateTime.Now.Date;
                        oSalesQuotation.Lines.UserFields.Fields.Item("U_STATUS").Value = "PENDING";
                        oSalesQuotation.Lines.UserFields.Fields.Item("U_NEXT_ACTION_BY").Value = Convert.ToString(iRow["ACTIONBY"]);
                        oSalesQuotation.Lines.UserFields.Fields.Item("U_INT_CASE_SEQ").Value = Convert.ToString(SeqNo);

                        iCount = iCount + 1;
                        sumOfQty = sumOfQty + Convert.ToInt32(iRow["Qty"]);
                    }

                    if (sumOfQty > 0)
                    {
                        lRetCode = oSalesQuotation.Update();

                        if (lRetCode != 0)
                        {
                            sErrDesc = oDICompany.GetLastErrorDescription();
                            sReturnResult = sErrDesc.ToString();
                            throw new ArgumentException(sErrDesc);
                        }
                        else
                        {
                            sReturnResult = "SUCCESS";
                            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS ", sFuncName);
                        }
                    }
                    else
                    {
                        sReturnResult = "";
                    }
                }
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

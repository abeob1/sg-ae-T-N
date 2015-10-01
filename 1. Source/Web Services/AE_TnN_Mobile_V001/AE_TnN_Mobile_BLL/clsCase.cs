using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using AE_TnN_Mobile_Common;
using System.Data;
using SAP.Admin.DAO;
using System.Data.SqlClient;
using System.IO;

namespace AE_TnN_Mobile_BLL
{
    public class clsCase
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
        public string sQueryString = string.Empty;
        public SqlDataAdapter oSQLAdapter = new SqlDataAdapter();
        public SqlCommand oSQLCommand = new SqlCommand();
        public SqlConnection oConnection = new SqlConnection();
        public DataSet oDataset = new DataSet();

        public static string ConnectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public static string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
        public static string AttachmentPath = ConfigurationManager.AppSettings["AttachmentPath"].ToString();

        public DataSet SPA_AddCase_GetCardCode(string sUserName, string sPassword, string sCategory)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "SPA_AddCase_GetCardCode()";
                sProcName = "AE_SPA014_Mobile_Staging_AddCase_GetCardCode";

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

        public DataSet SPA_AddCase_CheckStatus()
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "SPA_AddCase_CheckStatus()";
                sProcName = "AE_SPA013_Mobile_AddCase_CheckStatus";

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

        public DataSet SPA_AddCase_ListOfItems()
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "SPA_AddCase_ListOfItems()";
                sProcName = "AE_SPA015_Mobile_Staging_AddCase_GetListofItems";

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

        public DataSet SPA_AddCase_SaveAttachment(string sDocString, string sDocName, string sItemCode, string sItemName, string sCardCode)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();
            try
            {
                sFuncName = "SPA_AddCase_SaveDocument()";
                //sProcName = "AE_SPA013_Mobile_AddCase_CheckStatus";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                // This following part of code is for converting and saving the Binary stream to PDF File.
                string strdocPath = null;
                strdocPath = AttachmentPath + sDocName + ".pdf";
                FileStream objfilestream = new FileStream(strdocPath, FileMode.Create, FileAccess.ReadWrite);

                byte[] bytes = new byte[sDocString.Length * sizeof(char)];
                System.Buffer.BlockCopy(sDocString.ToCharArray(), 0, bytes, 0, bytes.Length);

                //objfilestream.Write(bDocBinaryArray, 0, bDocBinaryArray.Length);
                objfilestream.Write(bytes, 0, bytes.Length);
                objfilestream.Close();

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Attached file is saved Properly ", sFuncName);

                // The following part of code is for Update the Selected ItemCode and ItemName in the Temp table [AE_OCRD]
                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand command = con.CreateCommand();
                command.CommandText = "Update [AE_OCRD] set ItemCode ='" + sItemCode + "',ItemName ='" + sItemName + "' where Code ='" + sCardCode + "'";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Executing the Query : " + command.CommandText, sFuncName);

                con.Open();
                command.ExecuteNonQuery();
                con.Close();

                // The following part of code is for Calling the Web method by passing the Binary file as Input to read the Fields from the file.

                oDataset = new DataSet();

            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw Ex;
            }
            return oDataset;
        }

        public DataSet Run_QueryString(string sQuery, string sConnString)
        {

            oConnection = new SqlConnection(sConnString);
            sQueryString = sQuery;
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "Run_QueryString()";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Query : " + sQuery, sFuncName);

                if (oConnection.State == ConnectionState.Closed)
                    oConnection.Open();

                oSQLCommand = new SqlCommand(sQueryString, oConnection);
                oSQLAdapter.SelectCommand = oSQLCommand;
                oSQLCommand.CommandTimeout = 0;
                oSQLAdapter.Fill(oDataset);
                oSQLAdapter.Dispose();
                oSQLCommand.Dispose();
                oConnection.Close();

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS ", sFuncName);

                return oDataset;

            }
            catch (Exception Ex)
            {

                sErrDesc = Ex.Message.ToString();
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw Ex;
            }
        }

        public DataSet SPA_AddCase_CheckPropertyExists(string sTitleType, string sTitleNo, string sLotNo, string sLotType)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "SPA_AddCase_CheckPropertyExists()";
                sProcName = "AE_SPA016_Mobile_AddCase_CheckPropertyExists";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);


                oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName,
                    Data.CreateParameter("@TITLETYPE", sTitleType), Data.CreateParameter("@TITLENO", sTitleNo), Data.CreateParameter("@LOTNO", sLotNo)
                    , Data.CreateParameter("@LOTTYPE", sLotType));

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

        public DataSet SPA_AddCase_CheckAEProperty(string sTitleType, string sTitleNo, string sLotNo, string sLotType)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "SPA_AddCase_CheckAEProperty()";
                sProcName = "AE_SPA018_Mobile_AddCase_CheckAEProperty";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);


                oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName,
                    Data.CreateParameter("@TITLETYPE", sTitleType), Data.CreateParameter("@TITLENO", sTitleNo), Data.CreateParameter("@LOTNO", sLotNo)
                    , Data.CreateParameter("@LOTTYPE", sLotType));

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

        public DataSet SPA_AddCase_ScanIC(Byte[] bDocBinaryArray)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();
            try
            {
                sFuncName = "SPA_AddCase_ScanIC()";
                //sProcName = "AE_SPA013_Mobile_AddCase_CheckStatus";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                // This following part of code is for converting and saving the Binary stream to PDF File.
                //string strdocPath = null;
                //strdocPath = AttachmentPath + sDocName + ".pdf";
                //FileStream objfilestream = new FileStream(strdocPath, FileMode.Create, FileAccess.ReadWrite);
                //objfilestream.Write(bDocBinaryArray, 0, bDocBinaryArray.Length);
                //objfilestream.Close();

                // The following part of code is for Calling the Web method by passing the Binary file as Input to read the Fields from the file.

                oDataset = new DataSet();

            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw Ex;
            }
            return oDataset;
        }

        public DataTable SPA_AddCase_GetProject()
        {
            string sFuncName = string.Empty;
            string sReturnResult = string.Empty;
            DataTable results = new DataTable();
            try
            {
                sFuncName = "SPA_AddCase_GetProject";
                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand command = con.CreateCommand();
                command.CommandText = "SELECT '-- Select --' [Id], '-- Select --' [Name] UNION " +
                                      "SELECT Prjcode [Id], PrjName [Name] FROM oprj p INNER JOIN [@AE_RELATEDPARTY] c ON p. [U_DEVELOPER] = c.code " +
                                      "WHERE p.active='Y' OR p.prjcode in ('I', 'NP') ";
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

        public DataSet SPA_AddCase_GetTempOCRD(string sCardCode, string sCategory, string sUsrName)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "SPA_AddCase_GetTempOCRD()";
                sProcName = "AE_SPA019_Mobile_AddCase";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);

                oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName,
                    Data.CreateParameter("@CardCode", sCardCode), Data.CreateParameter("@Category", sCategory), Data.CreateParameter("@UserName", sUsrName));

                //  oDataset = ExecuteProcedureForDataSet(sProcName, ConnectionString, new SqlParameter("@CardCode", sCardCode),new SqlParameter("@Category", sCategory), new SqlParameter("@UserName", sUsrName));


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

        //This Method is for the SAP BP Master Creation;
        //public string SPA_AddCase(DataTable dtDatatable)
        //{
        //    DataSet oDataset = new DataSet();
        //    string sFuncName = string.Empty;
        //    string sProcName = string.Empty;
        //    DataView oDTView = new DataView();
        //    string sResult = string.Empty;
        //    string sCardCode = string.Empty;
        //    string sDocEntry = string.Empty;
        //    try
        //    {
        //        sFuncName = "SPA_AddCase()";
        //        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

        //        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_AddCase_CheckStatus", sFuncName);
        //        DataSet dsResult = SPA_AddCase_CheckStatus();
        //        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_AddCase_CheckStatus", sFuncName);
        //        if (dsResult != null && dsResult.Tables.Count > 0)
        //        {
        //            if (dsResult.Tables[0].Rows[0]["CaseStatus"].ToString() != string.Empty)
        //            {
        //                sCardCode = dsResult.Tables[0].Rows[0]["CardCode"].ToString();
        //                sDocEntry = dsResult.Tables[0].Rows[0]["DocEntry"].ToString();

        //                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Connecting to target company ", sFuncName);
        //                oDICompany = oLogin.ConnectToTargetCompany(ConnectionString);

        //                SAPbobsCOM.BusinessPartners oBP = oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);

        //                oBP.GetByKey("SSS");

        //                oBP.UserFields.Fields.Item("").Value = "";
        //                //CUFD Table - For Listing the UDF's





        //                //SAPbobsCOM.GeneralService oGeneralService = null;
        //                //SAPbobsCOM.GeneralData oGeneralData;
        //                //SAPbobsCOM.CompanyService oCompanyService = oDICompany.GetCompanyService();
        //                //SAPbobsCOM.GeneralDataParams oGeneralParams = null;
        //                //oGeneralService = oCompanyService.GetGeneralService("OCRD");

        //                //oGeneralParams = (SAPbobsCOM.GeneralDataParams)oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams);
        //                //oGeneralParams.SetProperty("DocEntry", sDocEntry);
        //                //oGeneralData = oGeneralService.GetByParams(oGeneralParams);

        //                //oGeneralData.SetProperty("QryGroup3", dsResult.Tables[0].Rows[0]["QryGroup3"].ToString());

        //                //oGeneralService.Update(oGeneralData);
        //                //sResult = "Document Updated Successfully for the CardCode = " + sCardCode + " and DocEntry = " + sDocEntry;
        //            }
        //            else
        //            {
        //                string sUserName = dtDatatable.Rows[0]["UserName"].ToString();
        //                string sPassword = dtDatatable.Rows[0]["Password"].ToString();
        //                string sCategory = dtDatatable.Rows[0]["Category"].ToString();
        //                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_AddCase_CheckStatus", sFuncName);
        //                DataSet dsCardCode = SPA_AddCase_GetCardCode(sUserName, sPassword, sCategory);
        //                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_AddCase_CheckStatus", sFuncName);
        //                sCardCode = dsCardCode.Tables[0].Rows[0]["CardCode"].ToString();
        //                sResult = "Add Document for the CardCode = " + sCardCode;
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        sErrDesc = Ex.Message.ToString();
        //        oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
        //        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
        //        throw Ex;
        //    }
        //    return sResult;
        //}

        public string SPA_AddCase(DataTable dtDatatable)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();
            string sResult = string.Empty;
            string sCardCode = string.Empty;
            string sDocEntry = string.Empty;
            bool isExists = false;
            try
            {
                sFuncName = "SPA_AddCase()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (dtDatatable != null && dtDatatable.Rows.Count > 0)
                {
                    //sCardCode = dsResult.Tables[0].Rows[0]["CardCode"].ToString();
                    //sDocEntry = dsResult.Tables[0].Rows[0]["DocEntry"].ToString();

                    DataRow dr = dtDatatable.Rows[0];

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Connecting to target company ", sFuncName);
                    oDICompany = oLogin.ConnectToTargetCompany(ConnectionString);

                    SAPbobsCOM.BusinessPartners oBP = oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);

                    if (oBP.GetByKey(dr["CardCode"].ToString()))
                    {
                        //exists
                        isExists = true;
                    }
                    else
                    {
                        //not exists
                        oBP.CardCode = dr["CardCode"].ToString();
                    }

                    //  oBP.CardCode = dr["CardCode"].ToString();
                    oBP.CardName = dr["CardName"].ToString();
                    oBP.CardForeignName = dr["CardFName"].ToString();
                    oBP.CardType = SAPbobsCOM.BoCardTypes.cCustomer;
                    oBP.GroupCode = Convert.ToInt32(dr["GroupCode"]);
                    oBP.PayTermsGrpCode = -1;

                    oBP.Currency = dr["Currency"].ToString();
                    oBP.AdditionalID = dr["AddID"].ToString();

                    if (dr["QryGroup3"].ToString() == "Y")
                    {
                        oBP.set_Properties(3, SAPbobsCOM.BoYesNoEnum.tYES);
                    }
                    if (dr["QryGroup4"].ToString() == "Y")
                    {
                        oBP.set_Properties(4, SAPbobsCOM.BoYesNoEnum.tYES);
                    } if (dr["QryGroup5"].ToString() == "Y")
                    {
                        oBP.set_Properties(5, SAPbobsCOM.BoYesNoEnum.tYES);
                    } if (dr["QryGroup6"].ToString() == "Y")
                    {
                        oBP.set_Properties(6, SAPbobsCOM.BoYesNoEnum.tYES);
                    } if (dr["QryGroup7"].ToString() == "Y")
                    {
                        oBP.set_Properties(7, SAPbobsCOM.BoYesNoEnum.tYES);
                    } if (dr["QryGroup8"].ToString() == "Y")
                    {
                        oBP.set_Properties(8, SAPbobsCOM.BoYesNoEnum.tYES);
                    } if (dr["QryGroup9"].ToString() == "Y")
                    {
                        oBP.set_Properties(9, SAPbobsCOM.BoYesNoEnum.tYES);
                    } if (dr["QryGroup10"].ToString() == "Y")
                    {
                        oBP.set_Properties(10, SAPbobsCOM.BoYesNoEnum.tYES);
                    } if (dr["QryGroup11"].ToString() == "Y")
                    {
                        oBP.set_Properties(11, SAPbobsCOM.BoYesNoEnum.tYES);
                    } if (dr["QryGroup17"].ToString() == "Y")
                    {
                        oBP.set_Properties(17, SAPbobsCOM.BoYesNoEnum.tYES);
                    }

                    oBP.DebitorAccount = dr["DebPayAcct"].ToString(); //if cardCode without 'C', then its DebPayAcct else SalesDebPayAcct
                    oBP.ProjectCode = dr["ProjectCod"].ToString();

                    oBP.UserFields.Fields.Item("U_CASESTATUS").Value = dr["U_CASESTATUS"].ToString();
                    oBP.UserFields.Fields.Item("U_FILEOPENDATE").Value = dr["U_FILEOPENDATE"].ToString();

                    oBP.UserFields.Fields.Item("U_PARTNER_EMPID").Value = dr["U_PARTNER_EMPID"].ToString();
                    oBP.UserFields.Fields.Item("U_LA_EMPID").Value = dr["U_LA_EMPID"].ToString();
                    oBP.UserFields.Fields.Item("U_MANAGER_EMPID").Value = dr["U_MANAGER_EMPID"].ToString();
                    oBP.UserFields.Fields.Item("U_IC_EMPID").Value = dr["U_IC_EMPID"].ToString();
                    oBP.UserFields.Fields.Item("U_CS_EMPID").Value = dr["U_CS_EMPID"].ToString();

                    oBP.UserFields.Fields.Item("U_PURCH_RP_ID1").Value = dr["U_PURCH_RP_ID1"].ToString();
                    oBP.UserFields.Fields.Item("U_PURCH_RP_NAME1").Value = dr["U_PURCH_RP_NAME1"].ToString();
                    oBP.UserFields.Fields.Item("U_PURCH_RP_CODE1").Value = dr["U_PURCH_RP_CODE1"].ToString();
                    oBP.UserFields.Fields.Item("U_PURCH_RP_TAX1").Value = dr["U_PURCH_RP_TAX1"].ToString();
                    oBP.UserFields.Fields.Item("U_PURCH_CONTACT1").Value = dr["U_PURCH_CONTACT1"].ToString();
                    oBP.UserFields.Fields.Item("U_PURCH_IDTYPE1").Value = dr["U_PURCH_IDTYPE1"].ToString();

                    oBP.UserFields.Fields.Item("U_PURCH_RP_ID2").Value = dr["U_PURCH_RP_ID2"].ToString();
                    oBP.UserFields.Fields.Item("U_PURCH_RP_NAME2").Value = dr["U_PURCH_RP_NAME2"].ToString();
                    oBP.UserFields.Fields.Item("U_PURCH_RP_CODE2").Value = dr["U_PURCH_RP_CODE2"].ToString();
                    oBP.UserFields.Fields.Item("U_PURCH_RP_TAX2").Value = dr["U_PURCH_RP_TAX2"].ToString();
                    oBP.UserFields.Fields.Item("U_PURCH_CONTACT2").Value = dr["U_PURCH_CONTACT2"].ToString();
                    oBP.UserFields.Fields.Item("U_PURCH_IDTYPE2").Value = dr["U_PURCH_IDTYPE2"].ToString();

                    oBP.UserFields.Fields.Item("U_PURCH_RP_ID3").Value = dr["U_PURCH_RP_ID3"].ToString();
                    oBP.UserFields.Fields.Item("U_PURCH_RP_NAME3").Value = dr["U_PURCH_RP_NAME3"].ToString();
                    oBP.UserFields.Fields.Item("U_PURCH_RP_CODE3").Value = dr["U_PURCH_RP_CODE3"].ToString();
                    oBP.UserFields.Fields.Item("U_PURCH_RP_TAX3").Value = dr["U_PURCH_RP_TAX3"].ToString();
                    oBP.UserFields.Fields.Item("U_PURCH_CONTACT3").Value = dr["U_PURCH_CONTACT3"].ToString();
                    oBP.UserFields.Fields.Item("U_PURCH_IDTYPE3").Value = dr["U_PURCH_IDTYPE3"].ToString();

                    oBP.UserFields.Fields.Item("U_PURCH_RP_ID4").Value = dr["U_PURCH_RP_ID4"].ToString();
                    oBP.UserFields.Fields.Item("U_PURCH_RP_NAME4").Value = dr["U_PURCH_RP_NAME4"].ToString();
                    oBP.UserFields.Fields.Item("U_PURCH_RP_CODE4").Value = dr["U_PURCH_RP_CODE4"].ToString();
                    oBP.UserFields.Fields.Item("U_PURCH_RP_TAX4").Value = dr["U_PURCH_RP_TAX4"].ToString();
                    oBP.UserFields.Fields.Item("U_PURCH_CONTACT4").Value = dr["U_PURCH_CONTACT4"].ToString();
                    oBP.UserFields.Fields.Item("U_PURCH_IDTYPE4").Value = dr["U_PURCH_IDTYPE4"].ToString();

                    oBP.UserFields.Fields.Item("U_VNDR_RP_ID1").Value = dr["U_VNDR_RP_ID1"].ToString();
                    oBP.UserFields.Fields.Item("U_VNDR_RP_NAME1").Value = dr["U_VNDR_RP_NAME1"].ToString();
                    oBP.UserFields.Fields.Item("U_VNDR_RP_CODE1").Value = dr["U_VNDR_RP_CODE1"].ToString();
                    oBP.UserFields.Fields.Item("U_VNDR_RP_TAX1").Value = dr["U_VNDR_RP_TAX1"].ToString();
                    oBP.UserFields.Fields.Item("U_VNDR_CONTACT1").Value = dr["U_VNDR_CONTACT1"].ToString();
                    oBP.UserFields.Fields.Item("U_VNDR_IDTYPE1").Value = dr["U_VNDR_IDTYPE1"].ToString();

                    oBP.UserFields.Fields.Item("U_VNDR_RP_ID2").Value = dr["U_VNDR_RP_ID2"].ToString();
                    oBP.UserFields.Fields.Item("U_VNDR_RP_NAME2").Value = dr["U_VNDR_RP_NAME2"].ToString();
                    oBP.UserFields.Fields.Item("U_VNDR_RP_CODE2").Value = dr["U_VNDR_RP_CODE2"].ToString();
                    oBP.UserFields.Fields.Item("U_VNDR_RP_TAX2").Value = dr["U_VNDR_RP_TAX2"].ToString();
                    oBP.UserFields.Fields.Item("U_VNDR_CONTACT2").Value = dr["U_VNDR_CONTACT2"].ToString();
                    oBP.UserFields.Fields.Item("U_VNDR_IDTYPE2").Value = dr["U_VNDR_IDTYPE2"].ToString();

                    oBP.UserFields.Fields.Item("U_VNDR_RP_ID3").Value = dr["U_VNDR_RP_ID3"].ToString();
                    oBP.UserFields.Fields.Item("U_VNDR_RP_NAME3").Value = dr["U_VNDR_RP_NAME3"].ToString();
                    oBP.UserFields.Fields.Item("U_VNDR_RP_CODE3").Value = dr["U_VNDR_RP_CODE3"].ToString();
                    oBP.UserFields.Fields.Item("U_VNDR_RP_TAX3").Value = dr["U_VNDR_RP_TAX3"].ToString();
                    oBP.UserFields.Fields.Item("U_VNDR_CONTACT3").Value = dr["U_VNDR_CONTACT3"].ToString();
                    oBP.UserFields.Fields.Item("U_VNDR_IDTYPE3").Value = dr["U_VNDR_IDTYPE3"].ToString();

                    oBP.UserFields.Fields.Item("U_VNDR_RP_ID4").Value = dr["U_VNDR_RP_ID4"].ToString();
                    oBP.UserFields.Fields.Item("U_VNDR_RP_NAME4").Value = dr["U_VNDR_RP_NAME4"].ToString();
                    oBP.UserFields.Fields.Item("U_VNDR_RP_CODE4").Value = dr["U_VNDR_RP_CODE4"].ToString();
                    oBP.UserFields.Fields.Item("U_VNDR_RP_TAX4").Value = dr["U_VNDR_RP_TAX4"].ToString();
                    oBP.UserFields.Fields.Item("U_VNDR_CONTACT4").Value = dr["U_VNDR_CONTACT4"].ToString();
                    oBP.UserFields.Fields.Item("U_VNDR_IDTYPE4").Value = dr["U_VNDR_IDTYPE4"].ToString();

                    oBP.UserFields.Fields.Item("U_PROPERTY_CODE").Value = dr["U_PROPERTY_CODE"].ToString();
                    oBP.UserFields.Fields.Item("U_CHRG_BANK_CODE").Value = dr["U_CHRG_BANK_CODE"].ToString();
                    oBP.UserFields.Fields.Item("U_CHRG_BANK_BRANCH").Value = dr["U_CHRG_BANK_BRANCH"].ToString();
                    oBP.UserFields.Fields.Item("U_CHRG_BANK_PA_NAME").Value = dr["U_CHRG_BANK_PA_NAME"].ToString();
                    oBP.UserFields.Fields.Item("U_CHRG_BANK_PRSNTNO").Value = dr["U_CHRG_BANK_PRSNTNO"].ToString();

                    oBP.UserFields.Fields.Item("U_TITLETYPE").Value = dr["U_TITLETYPE"].ToString();
                    oBP.UserFields.Fields.Item("U_TITLENO").Value = dr["U_TITLENO"].ToString();
                    oBP.UserFields.Fields.Item("U_LOTNO").Value = dr["U_LOTNO"].ToString();
                    oBP.UserFields.Fields.Item("U_LOTTYPE").Value = dr["U_LOTTYPE"].ToString();
                    oBP.UserFields.Fields.Item("U_FORMERLY_KNOWN_AS").Value = dr["U_FORMERLY_KNOWN_AS"].ToString();
                    oBP.UserFields.Fields.Item("U_STATE").Value = dr["U_STATE"].ToString();
                    oBP.UserFields.Fields.Item("U_AREA").Value = dr["U_AREA"].ToString();
                    oBP.UserFields.Fields.Item("U_BPM").Value = dr["U_BPM"].ToString();
                    oBP.UserFields.Fields.Item("U_LOTAREA_SQM").Value = dr["U_LOTAREA_SQM"].ToString();
                    oBP.UserFields.Fields.Item("U_LOTAREA_SQFT").Value = dr["U_LOTAREA_SQFT"].ToString();

                    oBP.UserFields.Fields.Item("U_PARTNER_FST_NAME").Value = dr["U_PARTNER_FST_NAME"].ToString();
                    oBP.UserFields.Fields.Item("U_LA_FST_NAME").Value = dr["U_LA_FST_NAME"].ToString();
                    oBP.UserFields.Fields.Item("U_MANAGER_FST_NAME").Value = dr["U_MANAGER_FST_NAME"].ToString();
                    oBP.UserFields.Fields.Item("U_IC_FST_NAME").Value = dr["U_IC_FST_NAME"].ToString();
                    oBP.UserFields.Fields.Item("U_CS_FST_NAME").Value = dr["U_CS_FST_NAME"].ToString();

                    oBP.UserFields.Fields.Item("U_PROJECT_NAME").Value = dr["U_PROJECT_NAME"].ToString();
                    oBP.UserFields.Fields.Item("U_CASE_BRANCH").Value = dr["U_CASE_BRANCH"].ToString();
                    oBP.UserFields.Fields.Item("U_PURCH_RP_FIRM").Value = dr["U_PURCH_RP_FIRM"].ToString();
                    oBP.UserFields.Fields.Item("U_PURCH_RP_LWYR").Value = dr["U_PURCH_RP_LWYR"].ToString();
                    oBP.UserFields.Fields.Item("U_VNDR_RP_FIRM").Value = dr["U_VNDR_RP_FIRM"].ToString();
                    oBP.UserFields.Fields.Item("U_VNDR_RP_LWYR").Value = dr["U_VNDR_RP_LWYR"].ToString();

                    if (!oDICompany.InTransaction) oDICompany.StartTransaction();

                    double lRetCode;
                    if (isExists == true)
                    {
                        lRetCode = oBP.Update();
                    }
                    else
                    {
                        lRetCode = oBP.Add();
                    }
                    if (lRetCode == 0)
                    {
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("BP Record Created Successfully ", sFuncName);
                        oBP.GetByKey(dr["CardCode"].ToString());
                        oBP.CardCode = dr["SalesCardCode"].ToString();
                        oBP.CardName = dr["SalesCardName"].ToString();
                        oBP.CardForeignName = dr["SalesCardFName"].ToString();

                        lRetCode = oBP.Add();
                        if (lRetCode == 0)
                        {
                            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("BP Record Duplicated Successfully ", sFuncName);
                            oBP.GetByKey(dr["SalesCardCode"].ToString());
                            oBP.DebitorAccount = dr["SalesDebPayAcct"].ToString();
                            lRetCode = oBP.Update();
                            if (lRetCode == 0)
                            {
                                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("BP Record DebitorAccount Updated Successfully ", sFuncName);
                            }
                            sResult = "Customer Added/ successfully for the CardName = " + dr["CardName"].ToString();
                        }
                        else
                        {
                            if (oDICompany.InTransaction) oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                            sResult = oDICompany.GetLastErrorDescription(); throw new ArgumentException(sResult);
                        }
                        
                        // After Customer added in BP Master, Update the BPCode in the TempOCRD Table

                        // Create sales Quotation
                        //ItemCode - 

                        if (oDICompany.InTransaction) oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);

                        DataTable dt = new DataTable();
                        DataSet ds = SPA_AddCase_GetSalesQuotationItems(dr["CardCode"].ToString());
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                dt = ds.Tables[0];

                                if (dt.Rows.Count > 0)
                                {

                                    if (Add_SalesQuotation(dt, oDICompany, dr["CardCode"].ToString(), sErrDesc) != "SUCCESS")
                                        throw new ArgumentException(sErrDesc);
                                }

                            }
                        }

                        SqlConnection con = new SqlConnection(ConnectionString);
                        SqlCommand command = con.CreateCommand();
                        command.CommandText = "Update [AE_OCRD] set U_BPCode ='" + dr["CardCode"].ToString() + "' where Code ='" + dr["TempCardCode"].ToString() + "'";
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Executing the Query : " + command.CommandText, sFuncName);

                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();

                        sResult = "SUCCESS";
                    }
                    else
                    {
                        if (oDICompany.InTransaction) oDICompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                        sResult = oDICompany.GetLastErrorDescription(); throw new ArgumentException(sResult);
                    }
                }
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
            return sResult;
        }

        public string Add_SalesQuotation(DataTable oDTSQData, SAPbobsCOM.Company oDICompany,
                                         string sCardCode, string sErrDesc)
        {

            string sFuncName = string.Empty;
            SAPbobsCOM.Documents oSalesQuotation;
            int lRetCode;

            try
            {
                sFuncName = "Add_SalesQuotation()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                oSalesQuotation = oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oQuotations);

                oSalesQuotation.CardCode = sCardCode;
                oSalesQuotation.DocDate = DateTime.Now.Date;
                double iCount = 0;

                foreach (DataRow iRow in oDTSQData.Rows)
                {
                    //if (iCount != 0)
                    //    oSalesQuotation.Lines.Add();

                    //oSalesQuotation.Lines.SetCurrentLine();
                    oSalesQuotation.Lines.ItemCode = Convert.ToString(iRow["ItemCode"]);
                    oSalesQuotation.Lines.Quantity = Convert.ToInt32(iRow["Qty"]);
                    oSalesQuotation.Lines.Price = Convert.ToDouble(iRow["Price"]);
                    oSalesQuotation.Lines.Add();
                    //oSalesQuotation.Lines.VatGroup = "SR";
                    iCount = iCount + 1;
                }

                lRetCode = oSalesQuotation.Add();

                if (lRetCode != 0)
                {
                    sErrDesc = oDICompany.GetLastErrorDescription(); throw new ArgumentException(sErrDesc);
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS ", sFuncName);
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

        public string SPA_AddCase_Questions(DataTable dtDatatable)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();
            string sResult = string.Empty;
            string sUserName = string.Empty;
            string sPassword = string.Empty;
            string sCategory = string.Empty;
            string sCardCode = string.Empty;
            string sBranchId = string.Empty;
            string sBranchName = string.Empty;
            string sAppType = string.Empty;
            try
            {
                sFuncName = "SPA_AddCase_Questions()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (dtDatatable.Rows.Count > 0)
                {
                    sUserName = dtDatatable.Rows[0]["UserName"].ToString();
                    sPassword = dtDatatable.Rows[0]["Password"].ToString();
                    sCategory = dtDatatable.Rows[0]["Category"].ToString();
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_AddCase_GetCardCode", sFuncName);
                    DataSet dsResult = SPA_AddCase_GetCardCode(sUserName, sPassword, sCategory);
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_AddCase_GetCardCode", sFuncName);
                    if (dsResult != null && dsResult.Tables.Count > 0)
                    {
                        if (dsResult.Tables[0].Rows[0]["CardCode"].ToString() != string.Empty)
                        {
                            sCardCode = dsResult.Tables[0].Rows[0]["CardCode"].ToString();
                            sBranchId = dsResult.Tables[0].Rows[0]["BranchId"].ToString();
                            sBranchName = dsResult.Tables[0].Rows[0]["BranchName"].ToString();
                            sAppType = dsResult.Tables[0].Rows[0]["AppType"].ToString();

                            SqlConnection con = new SqlConnection(ConnectionString);

                            string insertQuery = "INSERT INTO [AE_OCRD](Code,Name,U_Branch,U_QryGroup3,U_VNDR_RP_FIRM,U_VNDR_RP_LWYR,U_PURCH_RP_FIRM,U_PURCH_RP_LWYR," +
                                                 "U_CASESTATUS,GroupCode,U_QryGroup4,U_QryGroup5,U_QryGroup6,U_QryGroup7,U_QryGroup8,U_QryGroup9,U_QryGroup10,U_QryGroup11,U_QryGroup17)" +
                                                 "VALUES (@Code,@Name,@U_Branch,@U_QryGroup3,@U_VNDR_RP_FIRM,@U_VNDR_RP_LWYR,@U_PURCH_RP_FIRM,@U_PURCH_RP_LWYR," +
                                                 "@U_CASESTATUS,@GroupCode,@U_QryGroup4,@U_QryGroup5,@U_QryGroup6,@U_QryGroup7,@U_QryGroup8,@U_QryGroup9,@U_QryGroup10,@U_QryGroup11,@U_QryGroup17)";

                            foreach (DataRow url in dtDatatable.Rows)
                            {
                                con.Open();
                                SqlCommand cmd = new SqlCommand();
                                cmd = con.CreateCommand();
                                cmd.CommandText = insertQuery;

                                cmd.Parameters.AddWithValue("@Code", sCardCode);
                                cmd.Parameters.AddWithValue("@Name", sBranchName + "/" + sCardCode + "/" + sAppType);
                                cmd.Parameters.AddWithValue("@U_Branch", sBranchId);
                                cmd.Parameters.AddWithValue("@GroupCode", 107); // Here we need to call the SP to get the Group code
                                cmd.Parameters.AddWithValue("@U_CASESTATUS", "OPEN"); // Default case status is Open
                                cmd.Parameters.AddWithValue("@U_QryGroup3", url["QryGroup3"]);
                                cmd.Parameters.AddWithValue("@U_VNDR_RP_FIRM", url["VNDR_RP_FIRM_SELL"]);
                                cmd.Parameters.AddWithValue("@U_VNDR_RP_LWYR", url["VNDR_RP_LWYR_SELL"]);
                                cmd.Parameters.AddWithValue("@U_PURCH_RP_FIRM", url["VNDR_RP_FIRM_BUY"]);
                                cmd.Parameters.AddWithValue("@U_PURCH_RP_LWYR", url["VNDR_RP_LWYR_BUY"]);
                                cmd.Parameters.AddWithValue("@U_QryGroup4", url["QryGroup4"]);
                                cmd.Parameters.AddWithValue("@U_QryGroup5", url["QryGroup5"]);
                                cmd.Parameters.AddWithValue("@U_QryGroup6", url["QryGroup6"]);
                                cmd.Parameters.AddWithValue("@U_QryGroup7", url["QryGroup7"]);
                                cmd.Parameters.AddWithValue("@U_QryGroup8", url["QryGroup8"]);
                                cmd.Parameters.AddWithValue("@U_QryGroup9", url["QryGroup9"]);
                                cmd.Parameters.AddWithValue("@U_QryGroup10", url["QryGroup10"]);
                                cmd.Parameters.AddWithValue("@U_QryGroup11", url["QryGroup11"]);
                                cmd.Parameters.AddWithValue("@U_QryGroup17", url["QryGroup17"]);

                                // don't forget to take care of connection - I omit this part for clearness
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }

                            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("BP Document Successfully Created for the CardCode = " + sCardCode + " and BranchName = " + sBranchName, sFuncName);
                            sResult = sCardCode;
                        }
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
            return sResult;
        }

        public string SPA_AddCase_AddPropertyEnquiry(DataTable dtDatatable)
        {
            string sFuncName = string.Empty;
            string sReturnResult = string.Empty;
            //int sCode = 1;
            string sCode = string.Empty;
            try
            {
                sFuncName = "SPA_AddCase_AddPropertyEnquiry";

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
                oGeneralData.SetProperty("U_LOTAREA_SQM", dtDatatable.Rows[0]["LOTAREA_SQM"].ToString());
                oGeneralData.SetProperty("U_LOTAREA_SQFT", dtDatatable.Rows[0]["LOTAREA_SQFT"].ToString());
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

                //SqlConnection con = new SqlConnection(ConnectionString);
                //SqlCommand command = con.CreateCommand();
                command.CommandText = "Update [AE_OCRD] set U_PROPERTY_CODE ='" + sCode + "' where Code ='" + dtDatatable.Rows[0]["CARDCODE"].ToString() + "'";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Executing the Query : " + command.CommandText, sFuncName);

                con.Open();

                command.ExecuteNonQuery();

                con.Close();

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

        public string SPA_AddCase_EditPropertyEnquiry(DataTable dtDatatable, string sCardCode)
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
                                        + "U_LOTAREA_SQM = '" + dtDatatable.Rows[0]["LOTAREA_SQM"] + "',"
                                        + "U_LOTAREA_SQFT = '" + dtDatatable.Rows[0]["LOTAREA_SQFT"] + "',"
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
                                        + " WHERE Code = '" + sCardCode + "'";
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

        public string SPA_AddCase_AddIndividual(DataTable dtDatatable, string sType)
        {
            string sFuncName = string.Empty;
            string sReturnResult = string.Empty;
            string sCode = string.Empty;
            int iLoopCount = 0;
            try
            {
                sFuncName = "SPA_AddCase_AddIndividual";

                foreach (DataRow item in dtDatatable.Rows)
                {
                    iLoopCount = iLoopCount + 1;
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

                    string[] sIDSplitUp = item["IDNo1"].ToString().Split('-');
                    string sIDNo2 = item["IDNo1"].ToString();
                    string sAddressId = item["IDAddress1"].ToString() + System.Environment.NewLine + item["IDAddress2"].ToString() + System.Environment.NewLine
                                        + item["IDAddress3"].ToString() + System.Environment.NewLine +
                                        item["IDAddress4"].ToString() + System.Environment.NewLine + item["IDAddress5"].ToString();
                    string sCorrespondAddressId = item["CorresAddr1"].ToString() + System.Environment.NewLine + item["CorresAddr2"].ToString() + System.Environment.NewLine
                                        + item["CorresAddr3"].ToString() + System.Environment.NewLine +
                                        item["CorresAddr4"].ToString() + System.Environment.NewLine + item["CorresAddr5"].ToString();

                    //Adding the Informations
                    oGeneralData.SetProperty("Code", sCode);
                    //oGeneralData.SetProperty("DocEntry", item["DocEntry"].ToString());
                    oGeneralData.SetProperty("U_NAME", item["EmployeeName"].ToString());
                    oGeneralData.SetProperty("U_INDIVIDUAL_TITLE", item["Title"].ToString());
                    oGeneralData.SetProperty("U_GENDER", item["Gender"].ToString());
                    oGeneralData.SetProperty("U_IDNO_F1", item["IDNo1"].ToString());
                    oGeneralData.SetProperty("U_IDNO_F2", sIDNo2.Replace("-", ""));
                    oGeneralData.SetProperty("U_IDNO_F3", item["IDNo3"].ToString());
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
                    oGeneralData.SetProperty("U_TAXNOFORMAT1", item["TaxNo"].ToString());
                    oGeneralData.SetProperty("U_CONTACT_MOBILE", item["MobileNo"].ToString());
                    oGeneralData.SetProperty("U_CONTACT_TELEPHONE", item["Telephone"].ToString());
                    oGeneralData.SetProperty("U_SYARIKATNO", item["OfficeNo"].ToString());
                    oGeneralData.SetProperty("U_ADDSEG1", item["IDAddress1"].ToString());
                    oGeneralData.SetProperty("U_ADDSEG2", item["IDAddress2"].ToString());
                    oGeneralData.SetProperty("U_ADDSEG3", item["IDAddress3"].ToString());
                    oGeneralData.SetProperty("U_ADDSEG4", item["IDAddress4"].ToString());
                    oGeneralData.SetProperty("U_ADDSEG5", item["IDAddress5"].ToString());
                    oGeneralData.SetProperty("U_ADDRESS_ID", sAddressId);
                    oGeneralData.SetProperty("U_CADDSEG1", item["CorresAddr1"].ToString());
                    oGeneralData.SetProperty("U_CADDSEG2", item["CorresAddr2"].ToString());
                    oGeneralData.SetProperty("U_CADDSEG3", item["CorresAddr3"].ToString());
                    oGeneralData.SetProperty("U_CADDSEG4", item["CorresAddr4"].ToString());
                    oGeneralData.SetProperty("U_CADDSEG5", item["CorresAddr5"].ToString());
                    oGeneralData.SetProperty("U_ADDRESS_CORRESPOND", sCorrespondAddressId);
                    oGeneralData.SetProperty("U_ADDRESS_TOUSE", item["AddressToUse"].ToString());
                    oGeneralData.SetProperty("U_BANK", "N");
                    oGeneralData.SetProperty("U_DEVELOPER", "N");
                    oGeneralData.SetProperty("U_SOLICITOR", "N");
                    //oGeneralData.SetProperty("LastUpdatedOn", item["LastUpdatedOn"].ToString());

                    oGeneralService.Add(oGeneralData);

                    // The following code is update the Related Party data in the Temp table

                    string sResult = SPA_AddCase_Purch_UpdatePropInTemp(sType, iLoopCount.ToString(), item["CardCode"].ToString(), sCode, item["IDNo1"].ToString(), item["EmployeeName"].ToString(), item["TaxNo"].ToString(), item["MobileNo"].ToString(), "INDIVIDUAL");
                    if (sResult != "SUCCESS")
                    {
                        sReturnResult = sResult;
                        break;
                    }
                    sReturnResult = "SUCCESS";
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

        public string SPA_UpdateCheck(string sItemCode, string sItemName, string sCardCode)
        {
            string sFuncName = "SPA_UpdateCheck";
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand command = con.CreateCommand();
            command.CommandText = "Update [AE_OCRD] set ItemCode ='" + sItemCode + "',ItemName ='" + sItemName + "' where Code ='" + sCardCode + "'";
            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Executing the Query : " + command.CommandText, sFuncName);

            con.Open();

            command.ExecuteNonQuery();

            con.Close();
            return "SUCCESS";
        }

        public string SPA_AddCase_Purch_UpdatePropInTemp(string sType, string sCount, string sCardCode, string sCode, string sId, string sName, string sTax, string sContact, string sIDType)
        {
            string sFuncName = "SPA_AddCase_Purch_UpdatePropInTemp";
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand command = con.CreateCommand();
            if (sType == "Purchase")
            {
                string sPurchCode = "U_PURCH_RP_CODE" + sCount;
                string sPurchId = "U_PURCH_RP_ID" + sCount;
                string sPurchName = "U_PURCH_RP_NAME" + sCount;
                string sPurchTax = "U_PURCH_RP_TAX" + sCount;
                string sPurchContact = "U_PURCH_CONTACT" + sCount;
                string sIdType = "U_PURCH_IDTYPE" + sCount;

                command.CommandText = "UPDATE [AE_OCRD] SET " + sPurchCode + " ='" + sCode + "'," + sPurchId + " ='" + sId + "'," + sPurchName + " ='" + sName + "', " +
                                      "" + sPurchTax + " ='" + sTax + "'," + sPurchContact + " ='" + sContact + "'," + sIdType + " ='" + sIDType + "' where Code ='" + sCardCode + "'";
            }
            else if (sType == "Vendor")
            {
                string sVndrCode = "U_VNDR_RP_CODE" + sCount;
                string sVndrId = "U_VNDR_RP_ID" + sCount;
                string sVndrName = "U_VNDR_RP_NAME" + sCount;
                string sVndrTax = "U_VNDR_RP_TAX" + sCount;
                string sVndrContact = "U_VNDR_CONTACT" + sCount;
                string sIdType = "U_VNDR_IDTYPE" + sCount;

                command.CommandText = "UPDATE [AE_OCRD] SET " + sVndrCode + " ='" + sCode + "'," + sVndrId + " ='" + sId + "'," + sVndrName + " ='" + sName + "', " +
                                      "" + sVndrTax + " ='" + sTax + "'," + sVndrContact + " ='" + sContact + "'," + sIdType + " ='" + sIDType + "' where Code ='" + sCardCode + "'";
            }
            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Executing the Query : " + command.CommandText, sFuncName);

            con.Open();

            command.ExecuteNonQuery();

            con.Close();
            return "SUCCESS";
        }

        public DataSet SPA_AddCase_GetSalesQuotationItems(string sCardCode)
        {
            DataSet oDataset = new DataSet();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();

            try
            {
                sFuncName = "SPA_AddCase_GetSalesQuotationItems()";
                sProcName = "getNextSection";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Calling Run_StoredProcedure() " + sProcName, sFuncName);


                oDataset = SqlHelper.ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, sProcName,
                    Data.CreateParameter("@caseno", sCardCode));

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

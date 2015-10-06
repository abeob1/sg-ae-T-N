using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using AE_TnN_Mobile_BLL;
using AE_TnN_Mobile_Common;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Data;
using System.Reflection;
using System.Xml.Serialization;
using System.IO;


namespace AE_TnN_Mobile_V001
{
    /// <summary>
    /// Summary description for SPAMobile
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SPAMobile : System.Web.Services.WebService
    {
        #region Objects
        public string sErrDesc = string.Empty;

        public Int16 p_iDebugMode = DEBUG_ON;

        public const Int16 RTN_SUCCESS = 1;
        public const Int16 RTN_ERROR = 0;
        public const Int16 DEBUG_ON = 1;
        public const Int16 DEBUG_OFF = 0;

        clsLog oLog = new clsLog();
        clsLogin oLogin = new clsLogin();
        clsDashboard oDashboard = new clsDashboard();
        clsWalkin oWalkin = new clsWalkin();
        clsCase oCase = new clsCase();
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<result> lstResult = new List<result>();
        SAPbobsCOM.Company oDICompany;
        public static string FileUploadPath = ConfigurationManager.AppSettings["FileUploadPath"].ToString();
        #endregion

        #region WebMethods

        #region login
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_ValidateUser(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "SPA_ValidateUser()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);
                string sUserName = string.Empty;
                string sPassword = string.Empty;
                string sCategory = string.Empty;

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<Json_ValidateUser> lstDeserialize = js.Deserialize<List<Json_ValidateUser>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    Json_ValidateUser objUserInfo = lstDeserialize[0];
                    sUserName = objUserInfo.sUserName;
                    sPassword = objUserInfo.sPassword;
                    sCategory = objUserInfo.sCategory;
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_ValidateUser() ", sFuncName);
                DataSet ds = oLogin.SPA_ValidateUser(sUserName, sPassword, sCategory);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_ValidateUser() ", sFuncName);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<UserInfo> lstUserInfo = new List<UserInfo>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        UserInfo _userInfo = new UserInfo();
                        _userInfo.UserName = r["UserName"].ToString();
                        _userInfo.Password = r["Password"].ToString();
                        _userInfo.EmployeeName = r["EmployeeName"].ToString();
                        _userInfo.FirstName = r["FirstName"].ToString();
                        _userInfo.Category = r["Category"].ToString();
                        _userInfo.SubRole = r["SubRole"].ToString();
                        _userInfo.Status = r["Status"].ToString();
                        _userInfo.EmpId = r["EmpId"].ToString();
                        _userInfo.Message = r["Message"].ToString();
                        _userInfo.TB_TM_NewCases_Per = r["TB_TM_NewCases_Per"].ToString();
                        _userInfo.TB_TM_NewCases = r["TB_TM_NewCases"].ToString();
                        _userInfo.TB_TM_ClosedCases_Per = r["TB_TM_ClosedCases_Per"].ToString();
                        _userInfo.TB_TM_ClosedCases = r["TB_TM_ClosedCases"].ToString();
                        _userInfo.TB_LM_NewCases_Per = r["TB_LM_NewCases_Per"].ToString();
                        _userInfo.TB_LM_NewCases = r["TB_LM_NewCases"].ToString();
                        _userInfo.TB_LM_ClosedCases_Per = r["TB_LM_ClosedCases_Per"].ToString();
                        _userInfo.TB_LM_ClosedCases = r["TB_LM_ClosedCases"].ToString();
                        _userInfo.YS_TM_Turnaround = r["YS_TM_Turnaround"].ToString();
                        _userInfo.YS_TM_Totaloutput = r["YS_TM_Totaloutput"].ToString();
                        _userInfo.YS_LM_Turnaround = r["YS_LM_Turnaround"].ToString();
                        _userInfo.YS_LM_Totaloutput = r["YS_LM_Totaloutput"].ToString();
                        _userInfo.Priority = r["Priority"].ToString();
                        _userInfo.Action = r["Action"].ToString();
                        _userInfo.Open = r["Open"].ToString();

                        lstUserInfo.Add(_userInfo);
                    }
                    if (lstUserInfo.Count == 0)
                    {
                        List<UserInfo> lstUserInfo1 = new List<UserInfo>();
                        UserInfo objUserInfo = new UserInfo();
                        objUserInfo.UserName = sUserName;
                        objUserInfo.Password = sPassword;
                        objUserInfo.Category = sCategory;
                        objUserInfo.Status = clsAppConstants.Failure;
                        lstUserInfo1.Add(objUserInfo);

                        Context.Response.Output.Write(js.Serialize(lstUserInfo1));
                    }
                    else
                    {
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Serializing the UserInformation ", sFuncName);
                        Context.Response.Output.Write(js.Serialize(lstUserInfo));
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Serializing the UserInformation , the Serialized data is ' " + js.Serialize(lstUserInfo) + " '", sFuncName);
                    }
                }
                else
                {
                    List<UserInfo> lstUserInfo = new List<UserInfo>();
                    UserInfo objUserInfo = new UserInfo();
                    objUserInfo.UserName = sUserName;
                    objUserInfo.Password = sPassword;
                    objUserInfo.Category = sCategory;
                    objUserInfo.Status = clsAppConstants.Failure;
                    lstUserInfo.Add(objUserInfo);

                    Context.Response.Output.Write(js.Serialize(lstUserInfo));
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
            //return string.Empty;
        }
        #endregion

        #region Common
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_RelatedCases(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "SPA_RelatedCases()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                string sPropertyCode = string.Empty;
                string sRelatedPartyCode = string.Empty;
                string sCallFrom = string.Empty;
                string sCategory = string.Empty;

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_RelatedCases> lstDeserialize = js.Deserialize<List<JSON_RelatedCases>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    JSON_RelatedCases objLstInfo = lstDeserialize[0];

                    sPropertyCode = objLstInfo.PropertyCode;
                    sRelatedPartyCode = objLstInfo.RelatedPartyCode;
                    sCallFrom = objLstInfo.CallFrom;
                    sCategory = objLstInfo.Category;
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_RelatedCases() ", sFuncName);
                DataSet ds = oDashboard.SPA_RelatedCases(sPropertyCode, sRelatedPartyCode, sCallFrom, sCategory);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_RelatedCases() ", sFuncName);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<RelatedCases> lstCaseInfo = new List<RelatedCases>();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            RelatedCases _RelatedInfo = new RelatedCases();

                            _RelatedInfo.CaseFileNo = r["CaseFileNo"].ToString();
                            _RelatedInfo.RelatedFileNo = r["RelatedFileNo"].ToString();
                            _RelatedInfo.BranchCode = r["BranchCode"].ToString();
                            _RelatedInfo.FileOpenedDate = r["FileOpenedDate"].ToString();
                            _RelatedInfo.IC = r["IC"].ToString();
                            _RelatedInfo.CaseType = r["CaseType"].ToString();
                            _RelatedInfo.ClientName = r["ClientName"].ToString();
                            _RelatedInfo.BankName = r["BankName"].ToString();
                            _RelatedInfo.Branch = r["Branch"].ToString();
                            _RelatedInfo.LOTNo = r["LOTNo"].ToString();
                            _RelatedInfo.CaseAmount = r["CaseAmount"].ToString();
                            _RelatedInfo.UserCode = r["UserCode"].ToString();
                            _RelatedInfo.Status = r["Status"].ToString();
                            _RelatedInfo.FileClosedDate = r["FileClosedDate"].ToString();

                            lstCaseInfo.Add(_RelatedInfo);
                        }
                    }
                    else
                    {
                        RelatedCases _RelatedInfo = new RelatedCases();

                        _RelatedInfo.CaseFileNo = string.Empty;
                        _RelatedInfo.RelatedFileNo = string.Empty;
                        _RelatedInfo.BranchCode = string.Empty;
                        _RelatedInfo.FileOpenedDate = string.Empty;
                        _RelatedInfo.IC = string.Empty;
                        _RelatedInfo.CaseType = string.Empty;
                        _RelatedInfo.ClientName = string.Empty;
                        _RelatedInfo.BankName = string.Empty;
                        _RelatedInfo.Branch = string.Empty;
                        _RelatedInfo.LOTNo = string.Empty;
                        _RelatedInfo.CaseAmount = string.Empty;
                        _RelatedInfo.UserCode = string.Empty;
                        _RelatedInfo.Status = string.Empty;
                        _RelatedInfo.FileClosedDate = string.Empty;

                        lstCaseInfo.Add(_RelatedInfo);
                    }

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Serializing the Related Case Information ", sFuncName);
                    Context.Response.Output.Write(js.Serialize(lstCaseInfo));
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Serializing the Related Case Details , the Serialized data is ' " + js.Serialize(lstCaseInfo) + " '", sFuncName);
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_GetValidValues(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "SPA_GetValidValues()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                string sTableName = string.Empty;
                string sFieldName = string.Empty;

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_ValidValues> lstDeserialize = js.Deserialize<List<JSON_ValidValues>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    JSON_ValidValues objLstInfo = lstDeserialize[0];

                    sTableName = objLstInfo.TableName;
                    sFieldName = objLstInfo.FieldName;
                }


                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_GetValidValues() ", sFuncName);
                DataSet ds = oWalkin.SPA_GetValidValues(sTableName, sFieldName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_GetValidValues() ", sFuncName);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<ValidValues> lstValidInfo = new List<ValidValues>();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            ValidValues _ValidInfo = new ValidValues();

                            _ValidInfo.Id = r["Id"].ToString();
                            _ValidInfo.Name = r["Name"].ToString();
                            lstValidInfo.Add(_ValidInfo);
                        }
                    }
                    else
                    {
                        ValidValues _ValidInfo = new ValidValues();

                        _ValidInfo.Id = string.Empty;
                        _ValidInfo.Name = string.Empty;
                        lstValidInfo.Add(_ValidInfo);
                    }

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Serializing the Valid Values Information ", sFuncName);
                    Context.Response.Output.Write(js.Serialize(lstValidInfo));
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Serializing the Valid Values Information , the Serialized data is ' " + js.Serialize(lstValidInfo) + " '", sFuncName);
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public string SPA_NextLineAddressCheck(string sAddress1, string sAddress2, string sAddress3, string sAddress4, string sAddress5)
        {
            string sResult = oWalkin.SPA_NextLineAddressCheck(sAddress1, sAddress2, sAddress3, sAddress4, sAddress5);
            return sResult;
        }

        [WebMethod(EnableSession = true)]
        public string SPA_Customer_Signature(string PetCode, string PetName, string CustNameName, [XmlElement(IsNullable = true)] Byte[] docbinaryarray, string imgname)
        {
            bool IMG = false;
            IMG = SaveDocument_Boarding(docbinaryarray, imgname);
            if (IMG == true)
            {
                return "Succcess";
            }
            else
            {
                return "False";
            }
        }

        public bool SaveDocument_Boarding(Byte[] docbinaryarray, string docname)
        {
            string strdocPath = null;
            strdocPath = @"E:\Abeo-Projects\TnN\TnN Files\" + docname;
            FileStream objfilestream = new FileStream(strdocPath, FileMode.Create, FileAccess.ReadWrite);
            objfilestream.Write(docbinaryarray, 0, docbinaryarray.Length);
            objfilestream.Close();
            return true;
        }

        #endregion

        #region Dashboard
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_DashboardInfo(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "SPA_DashboardInfo()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);
                string sAppType = string.Empty;
                string sUserCode = string.Empty;

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<Json_DashboardInfo> lstDeserialize = js.Deserialize<List<Json_DashboardInfo>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    Json_DashboardInfo objUserInfo = lstDeserialize[0];
                    sAppType = objUserInfo.sAppType;
                    sUserCode = objUserInfo.sUserCode;
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_DashboardInfo() ", sFuncName);
                DataSet ds = oDashboard.SPA_DashboardInfo(sAppType, sUserCode);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_DashboardInfo() ", sFuncName);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<DashboardInfo> lstUserInfo = new List<DashboardInfo>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        DashboardInfo _DashInfo = new DashboardInfo();
                        _DashInfo.TB_TM_NewCases_Per = r["TB_TM_NewCases_Per"].ToString();
                        _DashInfo.TB_TM_NewCases = r["TB_TM_NewCases"].ToString();
                        _DashInfo.TB_TM_ClosedCases_Per = r["TB_TM_ClosedCases_Per"].ToString();
                        _DashInfo.TB_TM_ClosedCases = r["TB_TM_ClosedCases"].ToString();
                        _DashInfo.TB_LM_NewCases_Per = r["TB_LM_NewCases_Per"].ToString();
                        _DashInfo.TB_LM_NewCases = r["TB_LM_NewCases"].ToString();
                        _DashInfo.TB_LM_ClosedCases_Per = r["TB_LM_ClosedCases_Per"].ToString();
                        _DashInfo.TB_LM_ClosedCases = r["TB_LM_ClosedCases"].ToString();
                        _DashInfo.YS_TM_Turnaround = r["YS_TM_Turnaround"].ToString();
                        _DashInfo.YS_TM_Totaloutput = r["YS_TM_Totaloutput"].ToString();
                        _DashInfo.YS_LM_Turnaround = r["YS_LM_Turnaround"].ToString();
                        _DashInfo.YS_LM_Totaloutput = r["YS_LM_Totaloutput"].ToString();
                        _DashInfo.Priority = r["Priority"].ToString();
                        _DashInfo.Action = r["Action"].ToString();
                        _DashInfo.Open = r["Open"].ToString();

                        lstUserInfo.Add(_DashInfo);
                    }

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Serializing the DashboardInformation ", sFuncName);
                    Context.Response.Output.Write(js.Serialize(lstUserInfo));
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Serializing the DashboardInformation , the Serialized data is ' " + js.Serialize(lstUserInfo) + " '", sFuncName);
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_ListOfCases(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "SPA_ListOfCases()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                string sFiltertype = string.Empty;
                string sCasetype = string.Empty;
                string sCasestatus = string.Empty;
                string sUsercode = string.Empty;
                string sDateOpenFr = string.Empty;
                string sDateOpenTo = string.Empty;
                string sCaseFileNoFr = string.Empty;
                string sCaseFileNoTo = string.Empty;
                string sClientName = string.Empty;
                string sCaseAmtFr = string.Empty;
                string sCaseAmtTo = string.Empty;
                string sDateClosedFr = string.Empty;
                string sDateClosedTo = string.Empty;


                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_ListofCases> lstDeserialize = js.Deserialize<List<JSON_ListofCases>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    JSON_ListofCases objCaseInfo = lstDeserialize[0];

                    sFiltertype = objCaseInfo.sFiltertype;
                    sCasetype = objCaseInfo.sCasetype;
                    sCasestatus = objCaseInfo.sCasestatus;
                    sUsercode = objCaseInfo.sUsercode;
                    sDateOpenFr = objCaseInfo.sDateOpenFr;
                    sDateOpenTo = objCaseInfo.sDateOpenTo;
                    sCaseFileNoFr = objCaseInfo.sCaseFileNoFr;
                    sCaseFileNoTo = objCaseInfo.sCaseFileNoTo;
                    sClientName = objCaseInfo.sClientName;
                    sCaseAmtFr = objCaseInfo.sCaseAmtFr;
                    sCaseAmtTo = objCaseInfo.sCaseAmtTo;
                    sDateClosedFr = objCaseInfo.sDateClosedFr;
                    sDateClosedTo = objCaseInfo.sDateClosedTo;
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_CaseInfo() ", sFuncName);
                DataSet ds = oDashboard.SPA_CaseInfo(sFiltertype, sCasetype, sCasestatus, sUsercode, sDateOpenFr, sDateOpenTo, sCaseFileNoFr, sCaseFileNoTo,
                    sClientName, sCaseAmtFr, sCaseAmtTo, sDateClosedFr, sDateClosedTo);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_CaseInfo() ", sFuncName);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<ListofCases> lstCaseInfo = new List<ListofCases>();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        ListofCases _CaseInfo = new ListofCases();

                        _CaseInfo.CaseFileNo = r["CaseFileNo"].ToString();
                        _CaseInfo.RelatedFileNo = r["RelatedFileNo"].ToString();
                        _CaseInfo.Branch = r["Branch"].ToString();
                        _CaseInfo.FileOpened = r["FileOpened"].ToString();
                        _CaseInfo.InChargeName = r["InChargeName"].ToString();
                        _CaseInfo.CaseType = r["CaseType"].ToString();
                        _CaseInfo.ClientName = r["ClientName"].ToString();
                        _CaseInfo.BankName = r["BankName"].ToString();
                        _CaseInfo.Lot_PTD_PTNo = r["Lot_PTD_PTNo"].ToString();
                        _CaseInfo.CaseAmount = r["CaseAmount"].ToString();
                        _CaseInfo.UserCode = r["UserCode"].ToString();
                        _CaseInfo.CaseStatus = r["CaseStatus"].ToString();
                        _CaseInfo.FileClosedDate = r["FileClosedDate"].ToString();

                        lstCaseInfo.Add(_CaseInfo);
                    }

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Serializing the Case Information ", sFuncName);
                    Context.Response.Output.Write(js.Serialize(lstCaseInfo));
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Serializing the CaseInformation , the Serialized data is ' " + js.Serialize(lstCaseInfo) + " '", sFuncName);
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }
        #endregion

        #region property
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_ListofPropertyEnquiry(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "SPA_ListofPropertyEnquiry()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                string sTITLETYPE = string.Empty;
                string sTITLENO = string.Empty;
                string sLOTTYPE = string.Empty;
                string sLOT_NO = string.Empty;
                string sFORMERLY_KNOWN_AS = string.Empty;
                string sBPM = string.Empty;
                string sSTATE = string.Empty;
                string sAREA = string.Empty;

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_ListofPropertySearch> lstDeserialize = js.Deserialize<List<JSON_ListofPropertySearch>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    JSON_ListofPropertySearch objLstInfo = lstDeserialize[0];

                    sTITLETYPE = objLstInfo.TITLETYPE;
                    sTITLENO = objLstInfo.TITLENO;
                    sLOTTYPE = objLstInfo.LOTTYPE;
                    sLOT_NO = objLstInfo.LOT_NO;
                    sFORMERLY_KNOWN_AS = objLstInfo.FORMERLY_KNOWN_AS;
                    sBPM = objLstInfo.BPM;
                    sSTATE = objLstInfo.STATE;
                    sAREA = objLstInfo.AREA;
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_ListofPropertyEnquiry() ", sFuncName);
                DataSet ds = oDashboard.SPA_ListofPropertyEnquiry(sTITLETYPE, sTITLENO, sLOTTYPE, sLOT_NO, sFORMERLY_KNOWN_AS, sBPM, sSTATE, sAREA);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_ListofPropertyEnquiry() ", sFuncName);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<ListofPropertySearch> lstCaseInfo = new List<ListofPropertySearch>();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            ListofPropertySearch _SearchInfo = new ListofPropertySearch();

                            _SearchInfo.CODE = r["CODE"].ToString();
                            _SearchInfo.TITLETYPE = r["TITLETYPE"].ToString();
                            _SearchInfo.TITLENO = r["TITLENO"].ToString();
                            _SearchInfo.LOTTYPE = r["LOTTYPE"].ToString();
                            _SearchInfo.LOT_NO = r["LOT_NO"].ToString();
                            _SearchInfo.FORMERLY_KNOWN_AS = r["FORMERLY_KNOWN_AS"].ToString();
                            _SearchInfo.BPM = r["BPM"].ToString();
                            _SearchInfo.STATE = r["STATE"].ToString();
                            _SearchInfo.AREA = r["AREA"].ToString();

                            lstCaseInfo.Add(_SearchInfo);
                        }
                    }
                    else
                    {
                        ListofPropertySearch _SearchInfo = new ListofPropertySearch();

                        _SearchInfo.CODE = string.Empty;
                        _SearchInfo.TITLETYPE = string.Empty;
                        _SearchInfo.TITLENO = string.Empty;
                        _SearchInfo.LOTTYPE = string.Empty;
                        _SearchInfo.LOT_NO = string.Empty;
                        _SearchInfo.FORMERLY_KNOWN_AS = string.Empty;
                        _SearchInfo.BPM = string.Empty;
                        _SearchInfo.STATE = string.Empty;
                        _SearchInfo.AREA = string.Empty;

                        lstCaseInfo.Add(_SearchInfo);
                    }

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Serializing the Case Information ", sFuncName);
                    Context.Response.Output.Write(js.Serialize(lstCaseInfo));
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Serializing the Property Enquiry , the Serialized data is ' " + js.Serialize(lstCaseInfo) + " '", sFuncName);
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_PropertyEnquiryDetails(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "SPA_PropertyEnquiryDetails()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                string sCode = string.Empty;
                string sCategory = string.Empty;

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_PropertyEnquiryDetails> lstDeserialize = js.Deserialize<List<JSON_PropertyEnquiryDetails>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    JSON_PropertyEnquiryDetails objLstInfo = lstDeserialize[0];

                    sCode = objLstInfo.Code;
                    sCategory = objLstInfo.Category;
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_PropertyEnquiryDetails() ", sFuncName);
                DataSet ds = oDashboard.SPA_PropertyEnquiryDetails(sCode, sCategory);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_PropertyEnquiryDetails() ", sFuncName);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<PropertyEnquiryDetails> lstCaseInfo = new List<PropertyEnquiryDetails>();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            PropertyEnquiryDetails _DetailInfo = new PropertyEnquiryDetails();

                            _DetailInfo.CODE = r["CODE"].ToString();
                            _DetailInfo.TITLETYPE = r["TITLETYPE"].ToString();
                            _DetailInfo.TITLENO = r["TITLENO"].ToString();
                            _DetailInfo.LOTTYPE = r["LOTTYPE"].ToString();
                            _DetailInfo.LOTNO = r["LOTNO"].ToString();
                            _DetailInfo.FORMERLY_KNOWN_AS = r["FORMERLY_KNOWN_AS"].ToString();
                            _DetailInfo.BPM = r["BPM"].ToString();
                            _DetailInfo.STATE = r["STATE"].ToString();
                            _DetailInfo.AREA = r["AREA"].ToString();
                            _DetailInfo.LOTAREA_SQM = r["LOTAREA_SQM"].ToString();
                            _DetailInfo.LOTAREA_SQFT = r["LOTAREA_SQFT"].ToString();
                            _DetailInfo.LASTUPDATEDON = r["LASTUPDATEDON"].ToString();
                            _DetailInfo.DEVELOPER = r["DEVELOPER"].ToString();
                            _DetailInfo.DVLPR_CODE = r["DVLPR_CODE"].ToString();
                            _DetailInfo.PROJECT_CODE = r["PROJECT_CODE"].ToString();
                            _DetailInfo.PROJECTNAME = r["PROJECTNAME"].ToString();
                            _DetailInfo.DEVLICNO = r["DEVLICNO"].ToString();
                            _DetailInfo.DEVSOLICTOR = r["DEVSOLICTOR"].ToString();
                            _DetailInfo.DVLPR_SOL_CODE = r["DVLPR_SOL_CODE"].ToString();
                            _DetailInfo.DVLPR_LOC = r["DVLPR_LOC"].ToString();
                            _DetailInfo.LSTCHG_BANKCODE = r["LSTCHG_BANKCODE"].ToString();
                            _DetailInfo.LSTCHG_BANKNAME = r["LSTCHG_BANKNAME"].ToString();
                            _DetailInfo.LSTCHG_BRANCH = r["LSTCHG_BRANCH"].ToString();
                            _DetailInfo.LSTCHG_PANO = r["LSTCHG_PANO"].ToString();
                            _DetailInfo.LSTCHG_PRSTNO = r["LSTCHG_PRSTNO"].ToString();
                            lstCaseInfo.Add(_DetailInfo);
                        }
                    }
                    else
                    {
                        PropertyEnquiryDetails _DetailInfo = new PropertyEnquiryDetails();

                        _DetailInfo.CODE = string.Empty;
                        _DetailInfo.TITLETYPE = string.Empty;
                        _DetailInfo.TITLENO = string.Empty;
                        _DetailInfo.LOTTYPE = string.Empty;
                        _DetailInfo.LOTNO = string.Empty;
                        _DetailInfo.FORMERLY_KNOWN_AS = string.Empty;
                        _DetailInfo.BPM = string.Empty;
                        _DetailInfo.STATE = string.Empty;
                        _DetailInfo.AREA = string.Empty;
                        _DetailInfo.LOTAREA_SQM = string.Empty;
                        _DetailInfo.LOTAREA_SQFT = string.Empty;
                        _DetailInfo.LASTUPDATEDON = string.Empty;
                        _DetailInfo.DEVELOPER = string.Empty;
                        _DetailInfo.DVLPR_CODE = string.Empty;
                        _DetailInfo.PROJECT_CODE = string.Empty;
                        _DetailInfo.PROJECTNAME = string.Empty;
                        _DetailInfo.DEVLICNO = string.Empty;
                        _DetailInfo.DEVSOLICTOR = string.Empty;
                        _DetailInfo.DVLPR_SOL_CODE = string.Empty;
                        _DetailInfo.DVLPR_LOC = string.Empty;
                        _DetailInfo.LSTCHG_BANKCODE = string.Empty;
                        _DetailInfo.LSTCHG_BANKNAME = string.Empty;
                        _DetailInfo.LSTCHG_BRANCH = string.Empty;
                        _DetailInfo.LSTCHG_PANO = string.Empty;
                        _DetailInfo.LSTCHG_PRSTNO = string.Empty;
                        lstCaseInfo.Add(_DetailInfo);
                    }

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Serializing the Case Information ", sFuncName);
                    Context.Response.Output.Write(js.Serialize(lstCaseInfo));
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Serializing the Property Enquiry Details , the Serialized data is ' " + js.Serialize(lstCaseInfo) + " '", sFuncName);
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_AddPropertyEnquiryDetails(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "SPA_AddPropertyEnquiryDetails()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_EditPropertyEnquiryDetails> lstDeserialize = js.Deserialize<List<JSON_EditPropertyEnquiryDetails>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    JSON_EditPropertyEnquiryDetails objLstInfo = lstDeserialize[0];
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Converting Json to Datatable ", sFuncName);
                    DataTable Table = Edit_PropertyEnquiry(objLstInfo);

                    DataTable TableCopy = Table.Copy();

                    if (TableCopy != null && TableCopy.Rows.Count > 0)
                    {
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_AddPropertyEnquiry() ", sFuncName);
                        string sReturnResult = oDashboard.SPA_AddPropertyEnquiry(TableCopy);
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_AddPropertyEnquiry() ", sFuncName);

                        if (sReturnResult == "SUCCESS")
                        {
                            result objResult = new result();
                            objResult.Result = "Success";
                            objResult.DisplayMessage = "Property is Added successfully in SAP";
                            lstResult.Add(objResult);
                            Context.Response.Output.Write(js.Serialize(lstResult));
                            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS  ", sFuncName);
                        }
                        else
                        {
                            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                            result objResult = new result();
                            objResult.Result = "Error";
                            objResult.DisplayMessage = sReturnResult;
                            lstResult.Add(objResult);
                            Context.Response.Output.Write(js.Serialize(lstResult));
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_EditPropertyEnquiryDetails(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "SPA_EditPropertyEnquiryDetails()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_EditPropertyEnquiryDetails> lstDeserialize = js.Deserialize<List<JSON_EditPropertyEnquiryDetails>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    JSON_EditPropertyEnquiryDetails objLstInfo = lstDeserialize[0];
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Converting Json to Datatable ", sFuncName);
                    DataTable Table = Edit_PropertyEnquiry(objLstInfo);

                    DataTable TableCopy = Table.Copy();

                    if (TableCopy != null && TableCopy.Rows.Count > 0)
                    {
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_EditPropertyEnquiry() ", sFuncName);
                        string sReturnResult = oDashboard.SPA_EditPropertyEnquiry(TableCopy);
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_EditPropertyEnquiry() ", sFuncName);

                        if (sReturnResult == "SUCCESS")
                        {
                            result objResult = new result();
                            objResult.Result = "Success";
                            objResult.DisplayMessage = "Property is Updated successfully in SAP";
                            lstResult.Add(objResult);
                            Context.Response.Output.Write(js.Serialize(lstResult));
                            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS  ", sFuncName);
                        }
                        else
                        {
                            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                            result objResult = new result();
                            objResult.Result = "Error";
                            objResult.DisplayMessage = sReturnResult;
                            lstResult.Add(objResult);
                            Context.Response.Output.Write(js.Serialize(lstResult));
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_GetProject()
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "SPA_GetProject()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_GetProject() ", sFuncName);
                DataTable dtProject = oDashboard.SPA_GetProject();
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_GetProject() ", sFuncName);
                List<ValidValues> lstValidInfo = new List<ValidValues>();
                if (dtProject != null && dtProject.Rows.Count > 0)
                {
                    foreach (DataRow r in dtProject.Rows)
                    {
                        ValidValues _ValidInfo = new ValidValues();

                        _ValidInfo.Id = r["Id"].ToString();
                        _ValidInfo.Name = r["Name"].ToString();
                        lstValidInfo.Add(_ValidInfo);
                    }
                }
                else
                {
                    ValidValues _ValidInfo = new ValidValues();

                    _ValidInfo.Id = string.Empty;
                    _ValidInfo.Name = string.Empty;
                    lstValidInfo.Add(_ValidInfo);
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Serializing the Project Information ", sFuncName);
                Context.Response.Output.Write(js.Serialize(lstValidInfo));
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Serializing the Project Information , the Serialized data is ' " + js.Serialize(lstValidInfo) + " '", sFuncName);
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_Property_GetDropdownValues()
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "SPA_Property_GetDropdownValues()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_Property_GetDropdownValues() ", sFuncName);
                DataSet ds = oDashboard.SPA_Property_GetDropdownValues();
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_Property_GetDropdownValues() ", sFuncName);

                GetDropdownValues drpDownValues = new GetDropdownValues();
                List<Bank> lstBank = new List<Bank>();
                List<Developer> lstDev = new List<Developer>();
                List<Solicitor> lstSoli = new List<Solicitor>();
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Bank _bankInfo = new Bank();
                        if (r["BankCode"].ToString() != string.Empty)
                        {
                            _bankInfo.BankCode = r["BankCode"].ToString();
                            _bankInfo.BankName = r["BankName"].ToString();
                            lstBank.Add(_bankInfo);
                        }
                        Developer _DevInfo = new Developer();
                        if (r["DevCode"].ToString() != string.Empty)
                        {
                            _DevInfo.DevCode = r["DevCode"].ToString();
                            _DevInfo.DevName = r["DevName"].ToString();
                            lstDev.Add(_DevInfo);
                        }
                        Solicitor _SoliInfo = new Solicitor();
                        if (r["SoliCode"].ToString() != string.Empty)
                        {
                            _SoliInfo.SoliCode = r["SoliCode"].ToString();
                            _SoliInfo.SoliName = r["SoliName"].ToString();
                            lstSoli.Add(_SoliInfo);
                        }
                    }
                }
                drpDownValues.Bank = lstBank;
                drpDownValues.Developer = lstDev;
                drpDownValues.Solicitor = lstSoli;

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Serializing the Dropdown Information ", sFuncName);
                Context.Response.Output.Write(js.Serialize(drpDownValues));
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Serializing the Dropdown Information , the Serialized data is ' " + js.Serialize(drpDownValues) + " '", sFuncName);
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        #endregion

        #region Individual

        //[WebMethod(EnableSession = true)]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        //public void SPA_GetValidValueNames(string sJsonInput)
        //{
        //    string sFuncName = string.Empty;
        //    try
        //    {
        //        sFuncName = "SPA_GetValidValueNames()";
        //        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

        //        string sTableName = string.Empty;
        //        string sFieldName = string.Empty;

        //        sJsonInput = "[" + sJsonInput + "]";
        //        //Split JSON to Individual String
        //        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
        //        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
        //        List<JSON_ValidValues> lstDeserialize = js.Deserialize<List<JSON_ValidValues>>(sJsonInput);
        //        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
        //        if (lstDeserialize.Count > 0)
        //        {
        //            JSON_ValidValues objLstInfo = lstDeserialize[0];

        //            sTableName = objLstInfo.TableName;
        //            sFieldName = objLstInfo.FieldName;
        //        }


        //        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_GetValidValues() ", sFuncName);
        //        DataSet ds = oWalkin.SPA_GetValidValuesNames(sTableName, sFieldName);
        //        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_GetValidValues() ", sFuncName);
        //        if (ds != null && ds.Tables.Count > 0)
        //        {
        //            List<ValidValueNames> lstValidInfo = new List<ValidValueNames>();
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                foreach (DataRow r in ds.Tables[0].Rows)
        //                {
        //                    ValidValueNames _ValidInfo = new ValidValueNames();

        //                    _ValidInfo.Name = r["Name"].ToString();
        //                    lstValidInfo.Add(_ValidInfo);
        //                }
        //            }
        //            else
        //            {
        //                ValidValueNames _ValidInfo = new ValidValueNames();

        //                _ValidInfo.Name = string.Empty;
        //                lstValidInfo.Add(_ValidInfo);
        //            }

        //            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Serializing the Valid Values Information ", sFuncName);
        //            Context.Response.Output.Write(js.Serialize(lstValidInfo));
        //            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Serializing the Valid Values Information , the Serialized data is ' " + js.Serialize(lstValidInfo) + " '", sFuncName);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        sErrDesc = ex.Message.ToString();
        //        oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
        //        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
        //        result objResult = new result();
        //        objResult.Result = "Error";
        //        objResult.DisplayMessage = sErrDesc;
        //        lstResult.Add(objResult);
        //        Context.Response.Output.Write(js.Serialize(lstResult));
        //    }
        //}

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_IndividualSearch(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "SPA_IndividualSearch()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                string sFullName = string.Empty;
                string sMobileNum = string.Empty;
                string sIDNum = string.Empty;
                string sCategory = string.Empty;

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_IndividualSearch> lstDeserialize = js.Deserialize<List<JSON_IndividualSearch>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    JSON_IndividualSearch objLstInfo = lstDeserialize[0];

                    sFullName = objLstInfo.FullName;
                    sMobileNum = objLstInfo.MobileNum;
                    sIDNum = objLstInfo.IDNum;
                    sCategory = objLstInfo.Category;
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_IndividualSearch() ", sFuncName);
                DataSet ds = oWalkin.SPA_IndividualSearch(sFullName, sMobileNum, sIDNum, sCategory);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_IndividualSearch() ", sFuncName);
                if (ds != null && ds.Tables.Count > 0)
                {
                    List<IndividualSearch> lstSearchInfo = new List<IndividualSearch>();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            IndividualSearch _SearchInfo = new IndividualSearch();

                            _SearchInfo.Code = r["Code"].ToString();
                            _SearchInfo.DocEntry = r["DocEntry"].ToString();
                            _SearchInfo.EmployeeName = r["EmployeeName"].ToString();
                            _SearchInfo.Title = r["Title"].ToString();
                            _SearchInfo.Gender = r["Gender"].ToString();
                            _SearchInfo.IDNo1 = r["IDNo1"].ToString();
                            _SearchInfo.IDNo3 = r["IDNo3"].ToString();
                            _SearchInfo.TaxNo = r["TaxNo"].ToString();
                            _SearchInfo.MobileNo = r["MobileNo"].ToString();
                            _SearchInfo.Telephone = r["Telephone"].ToString();
                            _SearchInfo.OfficeNo = r["OfficeNo"].ToString();
                            _SearchInfo.IDAddress1 = r["IDAddress1"].ToString();
                            _SearchInfo.IDAddress2 = r["IDAddress2"].ToString();
                            _SearchInfo.IDAddress3 = r["IDAddress3"].ToString();
                            _SearchInfo.IDAddress4 = r["IDAddress4"].ToString();
                            _SearchInfo.IDAddress5 = r["IDAddress5"].ToString();
                            _SearchInfo.CorresAddr1 = r["CorresAddr1"].ToString();
                            _SearchInfo.CorresAddr2 = r["CorresAddr2"].ToString();
                            _SearchInfo.CorresAddr3 = r["CorresAddr3"].ToString();
                            _SearchInfo.CorresAddr4 = r["CorresAddr4"].ToString();
                            _SearchInfo.CorresAddr5 = r["CorresAddr5"].ToString();
                            _SearchInfo.AddressToUse = r["AddressToUse"].ToString();
                            _SearchInfo.LastUpdatedOn = r["LastUpdatedOn"].ToString();

                            lstSearchInfo.Add(_SearchInfo);
                        }
                    }
                    else
                    {
                        IndividualSearch _SearchInfo = new IndividualSearch();

                        _SearchInfo.Code = string.Empty;
                        _SearchInfo.DocEntry = string.Empty;
                        _SearchInfo.EmployeeName = string.Empty;
                        _SearchInfo.Title = string.Empty;
                        _SearchInfo.Gender = string.Empty;
                        _SearchInfo.IDNo1 = string.Empty;
                        _SearchInfo.IDNo3 = string.Empty;
                        _SearchInfo.TaxNo = string.Empty;
                        _SearchInfo.MobileNo = string.Empty;
                        _SearchInfo.Telephone = string.Empty;
                        _SearchInfo.OfficeNo = string.Empty;
                        _SearchInfo.IDAddress1 = string.Empty;
                        _SearchInfo.IDAddress2 = string.Empty;
                        _SearchInfo.IDAddress3 = string.Empty;
                        _SearchInfo.IDAddress4 = string.Empty;
                        _SearchInfo.IDAddress5 = string.Empty;
                        _SearchInfo.CorresAddr1 = string.Empty;
                        _SearchInfo.CorresAddr2 = string.Empty;
                        _SearchInfo.CorresAddr3 = string.Empty;
                        _SearchInfo.CorresAddr4 = string.Empty;
                        _SearchInfo.CorresAddr5 = string.Empty;
                        _SearchInfo.AddressToUse = string.Empty;
                        _SearchInfo.LastUpdatedOn = string.Empty;

                        lstSearchInfo.Add(_SearchInfo);
                    }

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Serializing the Individual Search Information ", sFuncName);
                    Context.Response.Output.Write(js.Serialize(lstSearchInfo));
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Serializing the Individual Search Informations , the Serialized data is ' " + js.Serialize(lstSearchInfo) + " '", sFuncName);
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_AddIndividual(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "SPA_AddIndividual()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_AddIndividualSearch> lstDeserialize = js.Deserialize<List<JSON_AddIndividualSearch>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    JSON_AddIndividualSearch objLstInfo = lstDeserialize[0];
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Converting Json to Datatable ", sFuncName);
                    DataTable Table = Add_Individual(objLstInfo);

                    DataTable TableCopy = Table.Copy();

                    if (TableCopy != null && TableCopy.Rows.Count > 0)
                    {
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_AddIndividual() ", sFuncName);
                        string sReturnResult = oWalkin.SPA_AddIndividual(TableCopy);
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_AddIndividual() ", sFuncName);

                        if (sReturnResult == "SUCCESS")
                        {
                            result objResult = new result();
                            objResult.Result = "Success";
                            objResult.DisplayMessage = "Individual is Added successfully in SAP";
                            lstResult.Add(objResult);
                            Context.Response.Output.Write(js.Serialize(lstResult));
                            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS  ", sFuncName);
                        }
                        else
                        {
                            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                            result objResult = new result();
                            objResult.Result = "Error";
                            objResult.DisplayMessage = sReturnResult;
                            lstResult.Add(objResult);
                            Context.Response.Output.Write(js.Serialize(lstResult));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_EditIndividual(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "SPA_EditIndividual()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_AddIndividualSearch> lstDeserialize = js.Deserialize<List<JSON_AddIndividualSearch>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    JSON_AddIndividualSearch objLstInfo = lstDeserialize[0];
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Converting Json to Datatable ", sFuncName);
                    DataTable Table = Add_Individual(objLstInfo);

                    DataTable TableCopy = Table.Copy();

                    if (TableCopy != null && TableCopy.Rows.Count > 0)
                    {
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_EditIndividual() ", sFuncName);
                        string sReturnResult = oWalkin.SPA_EditIndividual(TableCopy);
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_EditIndividual() ", sFuncName);

                        if (sReturnResult == "SUCCESS")
                        {
                            result objResult = new result();
                            objResult.Result = "Success";
                            objResult.DisplayMessage = "Individual is Updated successfully in SAP";
                            lstResult.Add(objResult);
                            Context.Response.Output.Write(js.Serialize(lstResult));
                            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS  ", sFuncName);
                        }
                        else
                        {
                            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                            result objResult = new result();
                            objResult.Result = "Error";
                            objResult.DisplayMessage = sReturnResult;
                            lstResult.Add(objResult);
                            Context.Response.Output.Write(js.Serialize(lstResult));
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        #endregion

        #region Corporate

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_CorporateSearch(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                List<CorporateSearch> lstSearchInfo = new List<CorporateSearch>();
                sFuncName = "SPA_CorporateSearch()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                string sCompanyName = string.Empty;
                string sRegNum = string.Empty;
                string sAddress = string.Empty;
                string sCategory = string.Empty;

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_CorporateSearch> lstDeserialize = js.Deserialize<List<JSON_CorporateSearch>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    JSON_CorporateSearch objLstInfo = lstDeserialize[0];

                    sCompanyName = objLstInfo.CompanyName;
                    sRegNum = objLstInfo.RegNum;
                    sAddress = objLstInfo.Address;
                    sCategory = objLstInfo.Category;
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_CorporateSearch() ", sFuncName);
                DataSet ds = oWalkin.SPA_CorporateSearch(sCompanyName, sRegNum, sAddress, sCategory);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_CorporateSearch() ", sFuncName);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataView dv = ds.Tables[0].DefaultView;
                    List<Director> lstDirector = new List<Director>();
                    foreach (DataRowView rowView in dv)
                    {
                        DataRow r = rowView.Row;
                        if (r["DirCode"].ToString() != string.Empty && r["DirName"].ToString() != string.Empty)
                        {
                            Director clsDirector = new Director();
                            clsDirector.DirCode = r["DirCode"].ToString();
                            clsDirector.DirName = r["DirName"].ToString();
                            clsDirector.DirContactNum = r["DirContactNum"].ToString();
                            lstDirector.Add(clsDirector);
                        }
                    }

                    DataTable dtRemove = ds.Tables[0];
                    dtRemove.Columns.Remove("DirCode");
                    dtRemove.Columns.Remove("DirName");
                    dtRemove.Columns.Remove("DirContactNum");

                    DataView view = new DataView(dtRemove);
                    DataTable distinctValues = view.ToTable(true, "Code", "DocEntry", "CompName", "BRNNo", "TaxNo", "OfficeNo",
                                                            "IDAddress1", "IDAddress2", "IDAddress3", "IDAddress4", "IDAddress5"
                                                            , "CorresAddr1", "CorresAddr2", "CorresAddr3", "CorresAddr4", "CorresAddr5",
                                                            "AddressToUse", "LastUpdatedOn");

                    foreach (DataRow r in distinctValues.Rows)
                    {
                        CorporateSearch _SearchInfo = new CorporateSearch();

                        _SearchInfo.Code = r["Code"].ToString();
                        _SearchInfo.DocEntry = r["DocEntry"].ToString();
                        _SearchInfo.CompName = r["CompName"].ToString();
                        _SearchInfo.BRNNo = r["BRNNo"].ToString();
                        _SearchInfo.TaxNo = r["TaxNo"].ToString();
                        _SearchInfo.OfficeNo = r["OfficeNo"].ToString();
                        _SearchInfo.IDAddress1 = r["IDAddress1"].ToString();
                        _SearchInfo.IDAddress2 = r["IDAddress2"].ToString();
                        _SearchInfo.IDAddress3 = r["IDAddress3"].ToString();
                        _SearchInfo.IDAddress4 = r["IDAddress4"].ToString();
                        _SearchInfo.IDAddress5 = r["IDAddress5"].ToString();
                        _SearchInfo.CorresAddr1 = r["CorresAddr1"].ToString();
                        _SearchInfo.CorresAddr2 = r["CorresAddr2"].ToString();
                        _SearchInfo.CorresAddr3 = r["CorresAddr3"].ToString();
                        _SearchInfo.CorresAddr4 = r["CorresAddr4"].ToString();
                        _SearchInfo.CorresAddr5 = r["CorresAddr5"].ToString();
                        _SearchInfo.AddressToUse = r["AddressToUse"].ToString();
                        _SearchInfo.LastUpdatedOn = r["LastUpdatedOn"].ToString();
                        _SearchInfo.Director = lstDirector;

                        lstSearchInfo.Add(_SearchInfo);
                    }
                }
                else
                {
                    CorporateSearch _SearchInfo = new CorporateSearch();

                    _SearchInfo.Code = string.Empty;
                    _SearchInfo.DocEntry = string.Empty;
                    _SearchInfo.CompName = string.Empty;
                    _SearchInfo.BRNNo = string.Empty;
                    _SearchInfo.TaxNo = string.Empty;
                    _SearchInfo.OfficeNo = string.Empty;
                    _SearchInfo.IDAddress1 = string.Empty;
                    _SearchInfo.IDAddress2 = string.Empty;
                    _SearchInfo.IDAddress3 = string.Empty;
                    _SearchInfo.IDAddress4 = string.Empty;
                    _SearchInfo.IDAddress5 = string.Empty;
                    _SearchInfo.CorresAddr1 = string.Empty;
                    _SearchInfo.CorresAddr2 = string.Empty;
                    _SearchInfo.CorresAddr3 = string.Empty;
                    _SearchInfo.CorresAddr4 = string.Empty;
                    _SearchInfo.CorresAddr5 = string.Empty;
                    _SearchInfo.AddressToUse = string.Empty;
                    _SearchInfo.LastUpdatedOn = string.Empty;

                    lstSearchInfo.Add(_SearchInfo);
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Serializing the Corporate Search Information ", sFuncName);
                Context.Response.Output.Write(js.Serialize(lstSearchInfo));
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Serializing the Corporate Search Informations , the Serialized data is ' " + js.Serialize(lstSearchInfo) + " '", sFuncName);

            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        #endregion

        #region AddCase

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public DataSet SPA_AddCase_GetCardCode(string sUserName, string sPassword, string sCategory)
        {
            string sFuncName = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                sFuncName = "SPA_AddCase_GetCardCode()";
                ds = oCase.SPA_AddCase_GetCardCode(sUserName, sPassword, sCategory);
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
            return ds;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public DataSet SPA_AddCase_CheckStatus()
        {
            string sFuncName = string.Empty;
            DataSet sResult = new DataSet();
            try
            {
                sFuncName = "SPA_AddCase_CheckStatus()";
                sResult = oCase.SPA_AddCase_CheckStatus();
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
            return sResult;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_AddCase(string sJsonInput)
        {
            string sFuncName = string.Empty;
            string sResult = string.Empty;
            try
            {
                sFuncName = "SPA_AddCase()";

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_AddCase> lstDeserialize = js.Deserialize<List<JSON_AddCase>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    DataTable dt = ToDataTable(lstDeserialize);
                    sResult = oCase.SPA_AddCase(dt);

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS", sFuncName);
                    result objResult = new result();
                    objResult.Result = "SUCCESS";
                    objResult.DisplayMessage = sResult;
                    lstResult.Add(objResult);
                    Context.Response.Output.Write(js.Serialize(lstResult));
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_AddCase_Questions(string sJsonInput)
        {
            string sFuncName = string.Empty;
            string sResult = string.Empty;
            try
            {
                sFuncName = "SPA_AddCase_Questions()";

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_AddCase_Questions> lstDeserialize = js.Deserialize<List<JSON_AddCase_Questions>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    DataTable dt = ToDataTable(lstDeserialize);
                    sResult = oCase.SPA_AddCase_Questions(dt);

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS", sFuncName);
                    result objResult = new result();
                    objResult.Result = "SUCCESS";
                    objResult.DisplayMessage = sResult;
                    lstResult.Add(objResult);
                    Context.Response.Output.Write(js.Serialize(lstResult));
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_AddCase_ListOfItems()
        {
            string sFuncName = string.Empty;
            string sResult = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                sFuncName = "SPA_AddCase_ListOfItems()";
                ds = oCase.SPA_AddCase_ListOfItems();

                List<JSON_AddCase_ListofItems> lstCaseInfo = new List<JSON_AddCase_ListofItems>();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            JSON_AddCase_ListofItems objItems = new JSON_AddCase_ListofItems();
                            objItems.ItemCode = dr["ItemCode"].ToString();
                            objItems.ItemName = dr["ItemName"].ToString();
                            lstCaseInfo.Add(objItems);
                        }
                    }
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Serializing the List of Items", sFuncName);
                Context.Response.Output.Write(js.Serialize(lstCaseInfo));
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Serializing the List of Items , the Serialized data is ' " + js.Serialize(lstCaseInfo) + " '", sFuncName);
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_AddCase_DocumentToRead(string sJsonInput)
        {
            string sFuncName = string.Empty;
            string sResult = string.Empty;
            try
            {
                sFuncName = "SPA_AddCase_DocumentToRead()";

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<DocumentToRead> lstCaseInfo = new List<DocumentToRead>();
                List<JSON_AddCase_DocumentToRead> lstDeserialize = js.Deserialize<List<JSON_AddCase_DocumentToRead>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    JSON_AddCase_DocumentToRead objLstInfo = lstDeserialize[0];

                    //The following code is for Converting the Binary file to Original file and Read the data from the File
                    DataSet ds = oCase.SPA_AddCase_SaveAttachment(objLstInfo.FileName, objLstInfo.ItemCode, objLstInfo.ItemName, objLstInfo.CardCode);
                    //DataSet ds = new DataSet();

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow r in ds.Tables[0].Rows)
                            {
                                DocumentToRead _DetailInfo = new DocumentToRead();

                                _DetailInfo.Message = ConfigurationManager.AppSettings["Message"].ToString();
                                _DetailInfo.CODE = ConfigurationManager.AppSettings["CODE"].ToString();
                                _DetailInfo.TITLETYPE = ConfigurationManager.AppSettings["TITLETYPE"];
                                _DetailInfo.TITLENO = ConfigurationManager.AppSettings["TITLENO"];
                                _DetailInfo.LOTTYPE = ConfigurationManager.AppSettings["LOTTYPE"];
                                _DetailInfo.LOTNO = ConfigurationManager.AppSettings["LOTNO"];
                                _DetailInfo.FORMERLY_KNOWN_AS = ConfigurationManager.AppSettings["FORMERLY_KNOWN_AS"];
                                _DetailInfo.BPM = ConfigurationManager.AppSettings["BPM"];
                                _DetailInfo.STATE = ConfigurationManager.AppSettings["STATE"];
                                _DetailInfo.AREA = ConfigurationManager.AppSettings["AREA"];
                                _DetailInfo.LOTAREA_SQM = ConfigurationManager.AppSettings["LOTAREA_SQM"];
                                _DetailInfo.LOTAREA_SQFT = ConfigurationManager.AppSettings["LOTAREA_SQFT"];
                                _DetailInfo.LASTUPDATEDON = "";
                                _DetailInfo.DEVELOPER = "";
                                _DetailInfo.DVLPR_CODE = "";
                                _DetailInfo.PROJECT_CODE = "";
                                _DetailInfo.PROJECTNAME = "";
                                _DetailInfo.DEVLICNO = ConfigurationManager.AppSettings["DEVLICNO"];
                                _DetailInfo.DEVSOLICTOR = "";
                                _DetailInfo.DVLPR_SOL_CODE = "";
                                _DetailInfo.DVLPR_LOC = ConfigurationManager.AppSettings["DVLPR_LOC"];
                                _DetailInfo.LSTCHG_BANKCODE = "";
                                _DetailInfo.LSTCHG_BANKNAME = "";
                                _DetailInfo.LSTCHG_BRANCH = "";
                                _DetailInfo.LSTCHG_PANO = ConfigurationManager.AppSettings["LSTCHG_PANO"];
                                _DetailInfo.LSTCHG_PRSTNO = ConfigurationManager.AppSettings["LSTCHG_PRSTNO"];
                                lstCaseInfo.Add(_DetailInfo);

                                //_DetailInfo.Message = "";
                                ////_DetailInfo.CODE = r["CODE"].ToString();
                                //_DetailInfo.TITLETYPE = r["TITLETYPE"].ToString();
                                //_DetailInfo.TITLENO = r["TITLENO"].ToString();
                                //_DetailInfo.LOTTYPE = r["LOTTYPE"].ToString();
                                //_DetailInfo.LOTNO = r["LOTNO"].ToString();
                                //_DetailInfo.FORMERLY_KNOWN_AS = r["FORMERLY_KNOWN_AS"].ToString();
                                //_DetailInfo.BPM = r["BPM"].ToString();
                                //_DetailInfo.STATE = r["STATE"].ToString();
                                //_DetailInfo.AREA = r["AREA"].ToString();
                                //_DetailInfo.LOTAREA_SQM = r["LOTAREA_SQM"].ToString();
                                //_DetailInfo.LOTAREA_SQFT = r["LOTAREA_SQFT"].ToString();
                                //_DetailInfo.LASTUPDATEDON = r["LASTUPDATEDON"].ToString();
                                //_DetailInfo.DEVELOPER = r["DEVELOPER"].ToString();
                                ////_DetailInfo.DVLPR_CODE = r["DVLPR_CODE"].ToString();
                                ////_DetailInfo.PROJECT_CODE = r["PROJECT_CODE"].ToString();
                                //_DetailInfo.PROJECTNAME = r["PROJECTNAME"].ToString();
                                //_DetailInfo.DEVLICNO = r["DEVLICNO"].ToString();
                                //_DetailInfo.DEVSOLICTOR = r["DEVSOLICTOR"].ToString();
                                ////_DetailInfo.DVLPR_SOL_CODE = r["DVLPR_SOL_CODE"].ToString();
                                //_DetailInfo.DVLPR_LOC = r["DVLPR_LOC"].ToString();
                                ////_DetailInfo.LSTCHG_BANKCODE = r["LSTCHG_BANKCODE"].ToString();
                                //_DetailInfo.LSTCHG_BANKNAME = r["LSTCHG_BANKNAME"].ToString();
                                //_DetailInfo.LSTCHG_BRANCH = r["LSTCHG_BRANCH"].ToString();
                                //_DetailInfo.LSTCHG_PANO = r["LSTCHG_PANO"].ToString();
                                //_DetailInfo.LSTCHG_PRSTNO = r["LSTCHG_PRSTNO"].ToString();
                                //lstCaseInfo.Add(_DetailInfo);
                            }
                        }
                        else
                        {
                            DocumentToRead _DetailInfo = new DocumentToRead();

                            _DetailInfo.Message = "Cannot Read the Scanned Files. Kindly key in the data";
                            _DetailInfo.CODE = "";
                            _DetailInfo.TITLETYPE = "";
                            _DetailInfo.TITLENO = "";
                            _DetailInfo.LOTTYPE = "";
                            _DetailInfo.LOTNO = "";
                            _DetailInfo.FORMERLY_KNOWN_AS = "";
                            _DetailInfo.BPM = "";
                            _DetailInfo.STATE = "";
                            _DetailInfo.AREA = "";
                            _DetailInfo.LOTAREA_SQM = "";
                            _DetailInfo.LOTAREA_SQFT = "";
                            _DetailInfo.LASTUPDATEDON = "";
                            _DetailInfo.DEVELOPER = "";
                            _DetailInfo.DVLPR_CODE = "";
                            _DetailInfo.PROJECT_CODE = "";
                            _DetailInfo.PROJECTNAME = "";
                            _DetailInfo.DEVLICNO = "";
                            _DetailInfo.DEVSOLICTOR = "";
                            _DetailInfo.DVLPR_SOL_CODE = "";
                            _DetailInfo.DVLPR_LOC = "";
                            _DetailInfo.LSTCHG_BANKCODE = "";
                            _DetailInfo.LSTCHG_BANKNAME = "";
                            _DetailInfo.LSTCHG_BRANCH = "";
                            _DetailInfo.LSTCHG_PANO = "";
                            _DetailInfo.LSTCHG_PRSTNO = "";
                            lstCaseInfo.Add(_DetailInfo);
                        }
                    }
                    else
                    {
                        DocumentToRead _DetailInfo = new DocumentToRead();

                        _DetailInfo.Message = "Cannot Read the Scanned Files. Kindly key in the data";
                        _DetailInfo.CODE = "";
                        _DetailInfo.TITLETYPE = "";
                        _DetailInfo.TITLENO = "";
                        _DetailInfo.LOTTYPE = "";
                        _DetailInfo.LOTNO = "";
                        _DetailInfo.FORMERLY_KNOWN_AS = "";
                        _DetailInfo.BPM = "";
                        _DetailInfo.STATE = "";
                        _DetailInfo.AREA = "";
                        _DetailInfo.LOTAREA_SQM = "";
                        _DetailInfo.LOTAREA_SQFT = "";
                        _DetailInfo.LASTUPDATEDON = "";
                        _DetailInfo.DEVELOPER = "";
                        _DetailInfo.DVLPR_CODE = "";
                        _DetailInfo.PROJECT_CODE = "";
                        _DetailInfo.PROJECTNAME = "";
                        _DetailInfo.DEVLICNO = "";
                        _DetailInfo.DEVSOLICTOR = "";
                        _DetailInfo.DVLPR_SOL_CODE = "";
                        _DetailInfo.DVLPR_LOC = "";
                        _DetailInfo.LSTCHG_BANKCODE = "";
                        _DetailInfo.LSTCHG_BANKNAME = "";
                        _DetailInfo.LSTCHG_BRANCH = "";
                        _DetailInfo.LSTCHG_PANO = "";
                        _DetailInfo.LSTCHG_PRSTNO = "";
                        lstCaseInfo.Add(_DetailInfo);
                    }
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS  ", sFuncName);
                Context.Response.Output.Write(js.Serialize(lstCaseInfo));

                //JSON_EditPropertyEnquiryDetails entityEditProperty = new JSON_EditPropertyEnquiryDetails();
                //if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Converting Json to Datatable ", sFuncName);
                //DataTable Table = Edit_PropertyEnquiry(entityEditProperty);

                //DataTable TableCopy = Table.Copy();

                //if (TableCopy != null && TableCopy.Rows.Count > 0)
                //{
                //    // The Following Code in to check if title type, title no, lot type and lot no exists in open case and If exists, pull out the existing record with List of Case.
                //    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_AddPropertyEnquiry() ", sFuncName);
                //    string sReturnResult = oDashboard.SPA_AddPropertyEnquiry(TableCopy);
                //    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_AddPropertyEnquiry() ", sFuncName);
                //}

                //if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS  ", sFuncName);
                //result objResult = new result();
                //objResult.Result = "SUCCESS";
                //objResult.DisplayMessage = "File Uploaded and Data Updated";
                //lstResult.Add(objResult);
                //Context.Response.Output.Write(js.Serialize(lstResult));
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }

            //string PetCode, string PetName, string CustNameName, [XmlElement(IsNullable = true)] Byte[] docbinaryarray, string imgname
            //bool IMG = false;
            //IMG = SaveDocument_Boarding(docbinaryarray, imgname);
            //if (IMG == true)
            //{
            //    return "Succcess";
            //}
            //else
            //{
            //    return "False";
            //}
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_AddCase_AddPropery(string sJsonInput)
        {
            string sFuncName = string.Empty;
            string sResult = string.Empty;
            try
            {
                sFuncName = "SPA_AddCase_AddPropery()";

                sJsonInput = "[" + sJsonInput + "]";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_AddCase_AddProperty> lstDeserialize = js.Deserialize<List<JSON_AddCase_AddProperty>>(sJsonInput);
                List<RelatedCases> lstCaseInfo = new List<RelatedCases>();
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Converting Json to Datatable ", sFuncName);
                    DataTable dt = ToDataTable(lstDeserialize);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // The Following Code in to check if title type, title no, lot type and lot no exists in open case  

                        DataSet ds = oCase.SPA_AddCase_CheckPropertyExists(dt.Rows[0]["TITLETYPE"].ToString(), dt.Rows[0]["TITLENO"].ToString(), dt.Rows[0]["LOTNO"].ToString(), dt.Rows[0]["LOTTYPE"].ToString());
                        if (ds.Tables[0].Rows.Count == 0) //this part will do, If open case not exists, Add the Property in [@AE_PROPERTY] Table and update in temp OCRD.
                        {
                            // The following code is to check , if the Property is already Exists in [@AE_PROPERTY] table or not 
                            DataSet dsCheck = oCase.SPA_AddCase_CheckAEProperty(dt.Rows[0]["TITLETYPE"].ToString(), dt.Rows[0]["TITLENO"].ToString(), dt.Rows[0]["LOTNO"].ToString(), dt.Rows[0]["LOTTYPE"].ToString());
                            if (dsCheck.Tables[0].Rows.Count == 0)
                            {
                                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_AddCase_AddPropertyEnquiry() ", sFuncName);
                                string sReturnResult = oCase.SPA_AddCase_AddPropertyEnquiry(dt);
                                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_AddCase_AddPropertyEnquiry() ", sFuncName);
                                result objResult = new result();
                                objResult.Result = "SUCCESS";
                                objResult.DisplayMessage = "Property is Added successfully in SAP";
                                lstResult.Add(objResult);
                                Context.Response.Output.Write(js.Serialize(lstResult));
                            }
                            else
                            {
                                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_AddCase_EditPropertyEnquiry() ", sFuncName);
                                string sReturnResult = oCase.SPA_AddCase_EditPropertyEnquiry(dt, dsCheck.Tables[0].Rows[0]["Code"].ToString());
                                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_AddCase_EditPropertyEnquiry() ", sFuncName);
                                result objResult = new result();
                                objResult.Result = "SUCCESS";
                                objResult.DisplayMessage = "Property is Updated successfully in SAP";
                                lstResult.Add(objResult);
                                Context.Response.Output.Write(js.Serialize(lstResult));
                            }
                        }
                        else //this part will do, If open case exists, pull out the existing record with List of Case and show in list in UI.
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow r in ds.Tables[0].Rows)
                                {
                                    RelatedCases _RelatedInfo = new RelatedCases();

                                    _RelatedInfo.CaseFileNo = r["CaseFileNo"].ToString();
                                    _RelatedInfo.RelatedFileNo = r["RelatedFileNo"].ToString();
                                    _RelatedInfo.BranchCode = r["BranchCode"].ToString();
                                    _RelatedInfo.FileOpenedDate = r["FileOpenedDate"].ToString();
                                    _RelatedInfo.IC = r["IC"].ToString();
                                    _RelatedInfo.CaseType = r["CaseType"].ToString();
                                    _RelatedInfo.ClientName = r["ClientName"].ToString();
                                    _RelatedInfo.BankName = r["BankName"].ToString();
                                    _RelatedInfo.Branch = r["Branch"].ToString();
                                    _RelatedInfo.LOTNo = r["LOTNo"].ToString();
                                    _RelatedInfo.CaseAmount = r["CaseAmount"].ToString();
                                    _RelatedInfo.UserCode = r["UserCode"].ToString();
                                    _RelatedInfo.Status = r["Status"].ToString();
                                    _RelatedInfo.FileClosedDate = r["FileClosedDate"].ToString();

                                    lstCaseInfo.Add(_RelatedInfo);
                                }
                            }
                            Context.Response.Output.Write(js.Serialize(lstCaseInfo));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_UpdateCheck()
        {
            oCase.SPA_UpdateCheck("200046", "ORIGINAL PROPERTY TITLE", "1500000001");
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_AddCase_GetProject()
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "SPA_AddCase_GetProject()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_GetProject() ", sFuncName);
                DataTable dtProject = oCase.SPA_AddCase_GetProject();
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_GetProject() ", sFuncName);
                List<ValidValues> lstValidInfo = new List<ValidValues>();
                if (dtProject != null && dtProject.Rows.Count > 0)
                {
                    foreach (DataRow r in dtProject.Rows)
                    {
                        ValidValues _ValidInfo = new ValidValues();

                        _ValidInfo.Id = r["Id"].ToString();
                        _ValidInfo.Name = r["Name"].ToString();
                        lstValidInfo.Add(_ValidInfo);
                    }
                }
                else
                {
                    ValidValues _ValidInfo = new ValidValues();

                    _ValidInfo.Id = string.Empty;
                    _ValidInfo.Name = string.Empty;
                    lstValidInfo.Add(_ValidInfo);
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Serializing the Project Information ", sFuncName);
                Context.Response.Output.Write(js.Serialize(lstValidInfo));
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Serializing the Project Information , the Serialized data is ' " + js.Serialize(lstValidInfo) + " '", sFuncName);
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_AddCase_Purch_AddIndividual(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "SPA_AddCase_Purch_AddIndividual()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                //sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_AddCase_AddIndividualSearch> lstDeserialize = js.Deserialize<List<JSON_AddCase_AddIndividualSearch>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    //JSON_AddIndividualSearch objLstInfo = lstDeserialize[0];
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Converting Json to Datatable ", sFuncName);
                    //DataTable Table = Add_Individual(objLstInfo);
                    DataTable Table = ToDataTable(lstDeserialize);
                    //DataTable TableCopy = Table.Copy();

                    if (Table != null && Table.Rows.Count > 0)
                    {
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_AddCase_AddIndividual() ", sFuncName);
                        string sReturnResult = oCase.SPA_AddCase_AddIndividual(Table, "Purchase");
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_AddCase_AddIndividual() ", sFuncName);

                        if (sReturnResult == "SUCCESS")
                        {
                            result objResult = new result();
                            objResult.Result = "Success";
                            objResult.DisplayMessage = "Purchasers Added successfully in SAP";
                            lstResult.Add(objResult);
                            Context.Response.Output.Write(js.Serialize(lstResult));
                            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS  ", sFuncName);
                        }
                        else
                        {
                            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                            result objResult = new result();
                            objResult.Result = "Error";
                            objResult.DisplayMessage = sReturnResult;
                            lstResult.Add(objResult);
                            Context.Response.Output.Write(js.Serialize(lstResult));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_AddCase_Vndr_AddIndividual(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "SPA_AddCase_Vndr_AddIndividual()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                //sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_AddCase_AddIndividualSearch> lstDeserialize = js.Deserialize<List<JSON_AddCase_AddIndividualSearch>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    //JSON_AddIndividualSearch objLstInfo = lstDeserialize[0];
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Converting Json to Datatable ", sFuncName);
                    //DataTable Table = Add_Individual(objLstInfo);
                    DataTable Table = ToDataTable(lstDeserialize);
                    //DataTable TableCopy = Table.Copy();

                    if (Table != null && Table.Rows.Count > 0)
                    {
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_AddCase_AddIndividual() ", sFuncName);
                        string sReturnResult = oCase.SPA_AddCase_AddIndividual(Table, "Vendor");
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_AddCase_AddIndividual() ", sFuncName);

                        if (sReturnResult == "SUCCESS")
                        {
                            result objResult = new result();
                            objResult.Result = "Success";
                            objResult.DisplayMessage = "Vendors Added successfully in SAP";
                            lstResult.Add(objResult);
                            Context.Response.Output.Write(js.Serialize(lstResult));
                            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS  ", sFuncName);
                        }
                        else
                        {
                            if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                            result objResult = new result();
                            objResult.Result = "Error";
                            objResult.DisplayMessage = sReturnResult;
                            lstResult.Add(objResult);
                            Context.Response.Output.Write(js.Serialize(lstResult));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_AddCase_ScanIC(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "SPA_AddCase_ScanIC()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                sJsonInput = "[" + sJsonInput + "]";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_AddCase_ScanIC> lstDeserialize = js.Deserialize<List<JSON_AddCase_ScanIC>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    Byte[] bBinaryData = lstDeserialize[0].DocBinaryArray;
                    // Call the web method by passing the Binary data to the method and get the result

                    DataSet ds = oCase.SPA_AddCase_ScanIC(bBinaryData);

                    ////if (ds != null && ds.Tables.Count > 0)
                    ////{
                    List<ScanIC> lstSearchInfo = new List<ScanIC>();
                    ////if (ds.Tables[0].Rows.Count > 0)
                    ////{
                    ////foreach (DataRow r in ds.Tables[0].Rows)
                    ////{
                    ScanIC _SearchInfo = new ScanIC();

                    ////_SearchInfo.Code =  r["Code"].ToString();
                    ////_SearchInfo.DocEntry = r["DocEntry"].ToString();
                    ////_SearchInfo.EmployeeName = r["EmployeeName"].ToString();
                    ////_SearchInfo.Title = r["Title"].ToString();
                    ////_SearchInfo.Gender = r["Gender"].ToString();
                    ////_SearchInfo.IDNo1 = r["IDNo1"].ToString();
                    ////_SearchInfo.IDNo3 = r["IDNo3"].ToString();
                    ////_SearchInfo.TaxNo = r["TaxNo"].ToString();
                    ////_SearchInfo.MobileNo = r["MobileNo"].ToString();
                    ////_SearchInfo.Telephone = r["Telephone"].ToString();
                    ////_SearchInfo.OfficeNo = r["OfficeNo"].ToString();
                    ////_SearchInfo.IDAddress1 = r["IDAddress1"].ToString();
                    ////_SearchInfo.IDAddress2 = r["IDAddress2"].ToString();
                    ////_SearchInfo.IDAddress3 = r["IDAddress3"].ToString();
                    ////_SearchInfo.IDAddress4 = r["IDAddress4"].ToString();
                    ////_SearchInfo.IDAddress5 = r["IDAddress5"].ToString();
                    ////_SearchInfo.CorresAddr1 = r["CorresAddr1"].ToString();
                    ////_SearchInfo.CorresAddr2 = r["CorresAddr2"].ToString();
                    ////_SearchInfo.CorresAddr3 = r["CorresAddr3"].ToString();
                    ////_SearchInfo.CorresAddr4 = r["CorresAddr4"].ToString();
                    ////_SearchInfo.CorresAddr5 = r["CorresAddr5"].ToString();
                    ////_SearchInfo.AddressToUse = r["AddressToUse"].ToString();
                    ////_SearchInfo.LastUpdatedOn = r["LastUpdatedOn"].ToString();

                    _SearchInfo.Message = "OCR Cannot able to Read the data from Scanned File.Kindly Key in.";
                    _SearchInfo.Code = string.Empty;
                    _SearchInfo.DocEntry = string.Empty;
                    _SearchInfo.EmployeeName = string.Empty;
                    _SearchInfo.Title = string.Empty;
                    _SearchInfo.Gender = string.Empty;
                    _SearchInfo.IDNo1 = string.Empty;
                    _SearchInfo.IDNo3 = string.Empty;
                    _SearchInfo.TaxNo = string.Empty;
                    _SearchInfo.MobileNo = string.Empty;
                    _SearchInfo.Telephone = string.Empty;
                    _SearchInfo.OfficeNo = string.Empty;
                    _SearchInfo.IDAddress1 = string.Empty;
                    _SearchInfo.IDAddress2 = string.Empty;
                    _SearchInfo.IDAddress3 = string.Empty;
                    _SearchInfo.IDAddress4 = string.Empty;
                    _SearchInfo.IDAddress5 = string.Empty;
                    _SearchInfo.CorresAddr1 = string.Empty;
                    _SearchInfo.CorresAddr2 = string.Empty;
                    _SearchInfo.CorresAddr3 = string.Empty;
                    _SearchInfo.CorresAddr4 = string.Empty;
                    _SearchInfo.CorresAddr5 = string.Empty;
                    _SearchInfo.AddressToUse = string.Empty;
                    _SearchInfo.LastUpdatedOn = string.Empty;

                    lstSearchInfo.Add(_SearchInfo);
                    ////    }
                    ////}
                    ////else
                    ////{
                    ////    IndividualSearch _SearchInfo = new IndividualSearch();

                    ////    _SearchInfo.Code = string.Empty;
                    ////    _SearchInfo.DocEntry = string.Empty;
                    ////    _SearchInfo.EmployeeName = string.Empty;
                    ////    _SearchInfo.Title = string.Empty;
                    ////    _SearchInfo.Gender = string.Empty;
                    ////    _SearchInfo.IDNo1 = string.Empty;
                    ////    _SearchInfo.IDNo3 = string.Empty;
                    ////    _SearchInfo.TaxNo = string.Empty;
                    ////    _SearchInfo.MobileNo = string.Empty;
                    ////    _SearchInfo.Telephone = string.Empty;
                    ////    _SearchInfo.OfficeNo = string.Empty;
                    ////    _SearchInfo.IDAddress1 = string.Empty;
                    ////    _SearchInfo.IDAddress2 = string.Empty;
                    ////    _SearchInfo.IDAddress3 = string.Empty;
                    ////    _SearchInfo.IDAddress4 = string.Empty;
                    ////    _SearchInfo.IDAddress5 = string.Empty;
                    ////    _SearchInfo.CorresAddr1 = string.Empty;
                    ////    _SearchInfo.CorresAddr2 = string.Empty;
                    ////    _SearchInfo.CorresAddr3 = string.Empty;
                    ////    _SearchInfo.CorresAddr4 = string.Empty;
                    ////    _SearchInfo.CorresAddr5 = string.Empty;
                    ////    _SearchInfo.AddressToUse = string.Empty;
                    ////    _SearchInfo.LastUpdatedOn = string.Empty;

                    ////    lstSearchInfo.Add(_SearchInfo);
                    ////}

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Serializing the Scan Information ", sFuncName);
                    Context.Response.Output.Write(js.Serialize(lstSearchInfo));
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Serializing the Scan Informations , the Serialized data is ' " + js.Serialize(lstSearchInfo) + " '", sFuncName);
                    ////}

                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_AddCase_TransferToOriginalOCRD(string sJsonInput)
        {
            string sFuncName = string.Empty;
            string sCardCode = string.Empty;
            string sCategory = string.Empty;
            string sUserName = string.Empty;
            try
            {
                sFuncName = "SPA_AddCase_TransferToOriginalOCRD()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                sJsonInput = "[" + sJsonInput + "]";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_AddCase_ConvertToOriginalOCRD> lstDeserialize = js.Deserialize<List<JSON_AddCase_ConvertToOriginalOCRD>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    sCardCode = lstDeserialize[0].CardCode;
                    sCategory = lstDeserialize[0].Category;
                    sUserName = lstDeserialize[0].UserName;

                    //The following code is for getting the  temp OCRD records from DB
                    DataSet ds = oCase.SPA_AddCase_GetTempOCRD(sCardCode, sCategory, sUserName);

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            string sResult = oCase.SPA_AddCase(ds.Tables[0]);
                            if (sResult == "SUCCESS")
                            {
                                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS  ", sFuncName);
                                result objResult = new result();
                                objResult.Result = "SUCCESS";
                                objResult.DisplayMessage = "Case Created Successfully";
                                lstResult.Add(objResult);
                                Context.Response.Output.Write(js.Serialize(lstResult));
                            }
                            else
                            {
                                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                                result objResult = new result();
                                objResult.Result = "ERROR";
                                objResult.DisplayMessage = sResult;
                                lstResult.Add(objResult);
                                Context.Response.Output.Write(js.Serialize(lstResult));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_AddCase_GetCorporate()
        {
            string sFuncName = string.Empty;
            string sCardCode = string.Empty;
            string sCategory = string.Empty;
            string sUserName = string.Empty;
            try
            {
                sFuncName = "SPA_AddCase_GetCorporate()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_AddCase_GetCorporate() ", sFuncName);
                DataTable dt = oCase.SPA_AddCase_GetCorporate();
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_AddCase_GetCorporate() ", sFuncName);
                List<ScanIC> lstdoc = new List<ScanIC>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        ScanIC _SearchInfo = new ScanIC();

                        _SearchInfo.Code = r["Code"].ToString();
                        _SearchInfo.DocEntry = r["DocEntry"].ToString();
                        _SearchInfo.EmployeeName = r["EmployeeName"].ToString();
                        _SearchInfo.Title = string.Empty;
                        _SearchInfo.Gender = string.Empty;
                        _SearchInfo.IDNo1 = r["IDNo1"].ToString();
                        _SearchInfo.IDNo3 = r["IDNo3"].ToString();
                        _SearchInfo.TaxNo = r["TaxNo"].ToString();
                        _SearchInfo.MobileNo = r["MobileNo"].ToString();
                        _SearchInfo.Telephone = r["Telephone"].ToString();
                        _SearchInfo.OfficeNo = string.Empty;
                        _SearchInfo.IDAddress1 = r["IDAddress1"].ToString();
                        _SearchInfo.IDAddress2 = r["IDAddress2"].ToString();
                        _SearchInfo.IDAddress3 = r["IDAddress3"].ToString();
                        _SearchInfo.IDAddress4 = r["IDAddress4"].ToString();
                        _SearchInfo.IDAddress5 = r["IDAddress5"].ToString();
                        _SearchInfo.CorresAddr1 = r["CorresAddr1"].ToString();
                        _SearchInfo.CorresAddr2 = r["CorresAddr2"].ToString();
                        _SearchInfo.CorresAddr3 = r["CorresAddr3"].ToString();
                        _SearchInfo.CorresAddr4 = r["CorresAddr4"].ToString();
                        _SearchInfo.CorresAddr5 = r["CorresAddr5"].ToString();
                        _SearchInfo.AddressToUse = string.Empty;
                        _SearchInfo.LastUpdatedOn = string.Empty;

                        lstdoc.Add(_SearchInfo);
                    }
                }
                else
                {
                    ScanIC _SearchInfo = new ScanIC();

                    _SearchInfo.Message = string.Empty;
                    _SearchInfo.Code = string.Empty;
                    _SearchInfo.DocEntry = string.Empty;
                    _SearchInfo.EmployeeName = string.Empty;
                    _SearchInfo.Title = string.Empty;
                    _SearchInfo.Gender = string.Empty;
                    _SearchInfo.IDNo1 = string.Empty;
                    _SearchInfo.IDNo3 = string.Empty;
                    _SearchInfo.TaxNo = string.Empty;
                    _SearchInfo.MobileNo = string.Empty;
                    _SearchInfo.Telephone = string.Empty;
                    _SearchInfo.OfficeNo = string.Empty;
                    _SearchInfo.IDAddress1 = string.Empty;
                    _SearchInfo.IDAddress2 = string.Empty;
                    _SearchInfo.IDAddress3 = string.Empty;
                    _SearchInfo.IDAddress4 = string.Empty;
                    _SearchInfo.IDAddress5 = string.Empty;
                    _SearchInfo.CorresAddr1 = string.Empty;
                    _SearchInfo.CorresAddr2 = string.Empty;
                    _SearchInfo.CorresAddr3 = string.Empty;
                    _SearchInfo.CorresAddr4 = string.Empty;
                    _SearchInfo.CorresAddr5 = string.Empty;
                    _SearchInfo.AddressToUse = string.Empty;
                    _SearchInfo.LastUpdatedOn = string.Empty;
                    lstdoc.Add(_SearchInfo);
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Serializing the Corporate Information ", sFuncName);
                Context.Response.Output.Write(js.Serialize(lstdoc));
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Serializing the Corporate Information , the Serialized data is ' " + js.Serialize(lstdoc) + " '", sFuncName);
            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                result objResult = new result();
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                lstResult.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstResult));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void Attachments()
        {
            string sFuncName = string.Empty;
            result objResult = new result();
            List<result> lstAttachment = new List<result>();
            try
            {
                sFuncName = "Attachments";
                int i;
                HttpContext postedContext = HttpContext.Current;

                for (i = 0; i < postedContext.Request.Files.Count; i++)
                {
                    HttpPostedFile hpf = postedContext.Request.Files[i];

                    //DataSet dsCompanyList = oLogin.Get_CompanyList();
                    string PathToShowInSap = string.Empty;
                    //string PathToShowInWeb = string.Empty;
                    string sTimeStampValue = string.Empty;
                    //string sCompanyName = (string)postedContext.Request.Form["companyname"];
                    //if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Company name in request form : " + sCompanyName, sFuncName);
                    //DataSet ds = oShowAround.Get_AttachPath(dsCompanyList, sCompanyName);
                    //if (ds != null && ds.Tables[0].Rows.Count > 0)
                    //{
                    //PathToShowInSap = ds.Tables[0].Rows[0][0].ToString();
                    //PathToShowInWeb = Server.MapPath("~/Attachments/");
                    PathToShowInSap = FileUploadPath;
                    sTimeStampValue = MyExtensions.AppendTimeStamp(hpf.FileName);
                    hpf.SaveAs(PathToShowInSap + sTimeStampValue);
                    //hpf.SaveAs(PathToShowInWeb + sTimeStampValue);
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With Success  ", sFuncName);

                    objResult.Result = sTimeStampValue;
                    objResult.DisplayMessage = "Attachment Successfully Added";
                    lstAttachment.Add(objResult);
                    //objResult.Attachments = lstAttachment;
                    //lstAttResult.Add(objResult);
                    //if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Attachment Path from Mobile for SAP : " + objResult.Attachments[i].SAPURL, sFuncName);
                    //}
                    //else
                    //{
                    //    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                    //    // AttachmentResult objResult = new AttachmentResult();
                    //    objResult.Result = "Error";
                    //    objResult.DisplayMessage = "Attachment Path is Empty in  SAP. Check with SAP";
                    //    objResult.Attachments = lstAttachment;
                    //    lstAttResult.Add(objResult);
                    //}
                }
                Context.Response.Output.Write(js.Serialize(lstAttachment));

            }
            catch (Exception ex)
            {
                sErrDesc = ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                objResult.Result = "Error";
                objResult.DisplayMessage = sErrDesc;
                //objResult.Attachments = lstAttachment;
                lstAttachment.Add(objResult);
                Context.Response.Output.Write(js.Serialize(lstAttachment));
            }
        }

        #endregion

        #endregion

        #region Classes

        class result
        {
            public string Result { get; set; }
            public string DisplayMessage { get; set; }
        }

        class Json_ValidateUser
        {
            public string sUserName { get; set; }
            public string sPassword { get; set; }
            public string sCategory { get; set; }
        }

        class UserInfo
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string EmployeeName { get; set; }
            public string FirstName { get; set; }
            public string Category { get; set; }
            public string SubRole { get; set; }
            public string Status { get; set; }
            public string EmpId { get; set; }
            public string Message { get; set; }
            public string TB_TM_NewCases_Per { get; set; }
            public string TB_TM_NewCases { get; set; }
            public string TB_TM_ClosedCases_Per { get; set; }
            public string TB_TM_ClosedCases { get; set; }
            public string TB_LM_NewCases_Per { get; set; }
            public string TB_LM_NewCases { get; set; }
            public string TB_LM_ClosedCases_Per { get; set; }
            public string TB_LM_ClosedCases { get; set; }
            public string YS_TM_Turnaround { get; set; }
            public string YS_TM_Totaloutput { get; set; }
            public string YS_LM_Turnaround { get; set; }
            public string YS_LM_Totaloutput { get; set; }
            public string Priority { get; set; }
            public string Action { get; set; }
            public string Open { get; set; }
        }

        class Json_DashboardInfo
        {
            public string sAppType { get; set; }
            public string sUserCode { get; set; }
        }

        class DashboardInfo
        {
            public string TB_TM_NewCases_Per { get; set; }
            public string TB_TM_NewCases { get; set; }
            public string TB_TM_ClosedCases_Per { get; set; }
            public string TB_TM_ClosedCases { get; set; }
            public string TB_LM_NewCases_Per { get; set; }
            public string TB_LM_NewCases { get; set; }
            public string TB_LM_ClosedCases_Per { get; set; }
            public string TB_LM_ClosedCases { get; set; }
            public string YS_TM_Turnaround { get; set; }
            public string YS_TM_Totaloutput { get; set; }
            public string YS_LM_Turnaround { get; set; }
            public string YS_LM_Totaloutput { get; set; }
            public string Priority { get; set; }
            public string Action { get; set; }
            public string Open { get; set; }
        }

        class JSON_ListofCases
        {
            public string sFiltertype { get; set; }
            public string sCasetype { get; set; }
            public string sCasestatus { get; set; }
            public string sUsercode { get; set; }
            public string sDateOpenFr { get; set; }
            public string sDateOpenTo { get; set; }
            public string sCaseFileNoFr { get; set; }
            public string sCaseFileNoTo { get; set; }
            public string sClientName { get; set; }
            public string sCaseAmtFr { get; set; }
            public string sCaseAmtTo { get; set; }
            public string sDateClosedFr { get; set; }
            public string sDateClosedTo { get; set; }
        }

        class ListofCases
        {
            public string CaseFileNo { get; set; }
            public string RelatedFileNo { get; set; }
            public string Branch { get; set; }
            public string FileOpened { get; set; }
            public string InChargeName { get; set; }
            public string CaseType { get; set; }
            public string ClientName { get; set; }
            public string BankName { get; set; }
            public string Lot_PTD_PTNo { get; set; }
            public string CaseAmount { get; set; }
            public string UserCode { get; set; }
            public string CaseStatus { get; set; }
            public string FileClosedDate { get; set; }
        }

        class JSON_ListofPropertySearch
        {
            public string TITLETYPE { get; set; }
            public string TITLENO { get; set; }
            public string LOTTYPE { get; set; }
            public string LOT_NO { get; set; }
            public string FORMERLY_KNOWN_AS { get; set; }
            public string BPM { get; set; }
            public string STATE { get; set; }
            public string AREA { get; set; }
        }

        class ListofPropertySearch
        {
            public string CODE { get; set; }
            public string TITLETYPE { get; set; }
            public string TITLENO { get; set; }
            public string LOTTYPE { get; set; }
            public string LOT_NO { get; set; }
            public string FORMERLY_KNOWN_AS { get; set; }
            public string BPM { get; set; }
            public string STATE { get; set; }
            public string AREA { get; set; }
        }

        class JSON_PropertyEnquiryDetails
        {
            public string Code { get; set; }
            public string Category { get; set; }
        }

        class PropertyEnquiryDetails
        {
            public string CODE { get; set; }
            public string TITLETYPE { get; set; }
            public string TITLENO { get; set; }
            public string LOTTYPE { get; set; }
            public string LOTNO { get; set; }
            public string FORMERLY_KNOWN_AS { get; set; }
            public string BPM { get; set; }
            public string STATE { get; set; }
            public string AREA { get; set; }
            public string LOTAREA_SQM { get; set; }
            public string LOTAREA_SQFT { get; set; }
            public string LASTUPDATEDON { get; set; }
            public string DEVELOPER { get; set; }
            public string DVLPR_CODE { get; set; }
            public string PROJECT_CODE { get; set; }
            public string PROJECTNAME { get; set; }
            public string DEVLICNO { get; set; }
            public string DEVSOLICTOR { get; set; }
            public string DVLPR_SOL_CODE { get; set; }
            public string DVLPR_LOC { get; set; }
            public string LSTCHG_BANKCODE { get; set; }
            public string LSTCHG_BANKNAME { get; set; }
            public string LSTCHG_BRANCH { get; set; }
            public string LSTCHG_PANO { get; set; }
            public string LSTCHG_PRSTNO { get; set; }
        }

        class JSON_RelatedCases
        {
            public string PropertyCode { get; set; }
            public string RelatedPartyCode { get; set; }
            public string CallFrom { get; set; }
            public string Category { get; set; }
        }

        class RelatedCases
        {
            public string CaseFileNo { get; set; }
            public string RelatedFileNo { get; set; }
            public string BranchCode { get; set; }
            public string FileOpenedDate { get; set; }
            public string IC { get; set; }
            public string CaseType { get; set; }
            public string ClientName { get; set; }
            public string BankName { get; set; }
            public string Branch { get; set; }
            public string LOTNo { get; set; }
            public string CaseAmount { get; set; }
            public string UserCode { get; set; }
            public string Status { get; set; }
            public string FileClosedDate { get; set; }
        }

        class JSON_EditPropertyEnquiryDetails
        {
            public string CODE { get; set; }
            public string TITLETYPE { get; set; }
            public string TITLENO { get; set; }
            public string LOTTYPE { get; set; }
            public string LOTNO { get; set; }
            public string FORMERLY_KNOWN_AS { get; set; }
            public string BPM { get; set; }
            public string STATE { get; set; }
            public string AREA { get; set; }
            public string LOTAREA_SQM { get; set; }
            public string LOTAREA_SQFT { get; set; }
            public string LASTUPDATEDON { get; set; }
            public string DEVELOPER { get; set; }
            public string DVLPR_CODE { get; set; }
            public string PROJECT_CODE { get; set; }
            public string PROJECTNAME { get; set; }
            public string DEVLICNO { get; set; }
            public string DEVSOLICTOR { get; set; }
            public string DVLPR_SOL_CODE { get; set; }
            public string DVLPR_LOC { get; set; }
            public string LSTCHG_BANKCODE { get; set; }
            public string LSTCHG_BANKNAME { get; set; }
            public string LSTCHG_BRANCH { get; set; }
            public string LSTCHG_PANO { get; set; }
            public string LSTCHG_PRSTNO { get; set; }
        }

        class ValidValues
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        class JSON_ValidValues
        {
            public string TableName { get; set; }
            public string FieldName { get; set; }
        }

        class ValidValueNames
        {
            public string Name { get; set; }
        }

        class JSON_IndividualSearch
        {
            public string FullName { get; set; }
            public string MobileNum { get; set; }
            public string IDNum { get; set; }
            public string Category { get; set; }
        }

        class IndividualSearch
        {
            public string Code { get; set; }
            public string DocEntry { get; set; }
            public string EmployeeName { get; set; }
            public string Title { get; set; }
            public string Gender { get; set; }
            public string IDNo1 { get; set; }
            public string IDNo3 { get; set; }
            public string TaxNo { get; set; }
            public string MobileNo { get; set; }
            public string Telephone { get; set; }
            public string OfficeNo { get; set; }
            public string IDAddress1 { get; set; }
            public string IDAddress2 { get; set; }
            public string IDAddress3 { get; set; }
            public string IDAddress4 { get; set; }
            public string IDAddress5 { get; set; }
            public string CorresAddr1 { get; set; }
            public string CorresAddr2 { get; set; }
            public string CorresAddr3 { get; set; }
            public string CorresAddr4 { get; set; }
            public string CorresAddr5 { get; set; }
            public string AddressToUse { get; set; }
            public string LastUpdatedOn { get; set; }
        }

        class JSON_AddIndividualSearch
        {
            public string Code { get; set; }
            public string DocEntry { get; set; }
            public string EmployeeName { get; set; }
            public string Title { get; set; }
            public string Gender { get; set; }
            public string IDNo1 { get; set; }
            public string IDNo3 { get; set; }
            public string TaxNo { get; set; }
            public string MobileNo { get; set; }
            public string Telephone { get; set; }
            public string OfficeNo { get; set; }
            public string IDAddress1 { get; set; }
            public string IDAddress2 { get; set; }
            public string IDAddress3 { get; set; }
            public string IDAddress4 { get; set; }
            public string IDAddress5 { get; set; }
            public string CorresAddr1 { get; set; }
            public string CorresAddr2 { get; set; }
            public string CorresAddr3 { get; set; }
            public string CorresAddr4 { get; set; }
            public string CorresAddr5 { get; set; }
            public string AddressToUse { get; set; }
            public string LastUpdatedOn { get; set; }
        }

        class Bank
        {
            public string BankCode { get; set; }
            public string BankName { get; set; }
        }

        class Developer
        {
            public string DevCode { get; set; }
            public string DevName { get; set; }
        }

        class Solicitor
        {
            public string SoliCode { get; set; }
            public string SoliName { get; set; }
        }

        class GetDropdownValues
        {
            public List<Bank> Bank { get; set; }
            public List<Developer> Developer { get; set; }
            public List<Solicitor> Solicitor { get; set; }
        }

        class JSON_CorporateSearch
        {
            public string CompanyName { get; set; }
            public string RegNum { get; set; }
            public string Address { get; set; }
            public string Category { get; set; }
        }

        class CorporateSearch
        {
            public string Code { get; set; }
            public string DocEntry { get; set; }
            public string CompName { get; set; }
            public string BRNNo { get; set; }
            public string TaxNo { get; set; }
            public string OfficeNo { get; set; }
            public string IDAddress1 { get; set; }
            public string IDAddress2 { get; set; }
            public string IDAddress3 { get; set; }
            public string IDAddress4 { get; set; }
            public string IDAddress5 { get; set; }
            public string CorresAddr1 { get; set; }
            public string CorresAddr2 { get; set; }
            public string CorresAddr3 { get; set; }
            public string CorresAddr4 { get; set; }
            public string CorresAddr5 { get; set; }
            public string AddressToUse { get; set; }
            public string LastUpdatedOn { get; set; }
            public List<Director> Director { get; set; }
        }

        class Director
        {
            public string DirCode { get; set; }
            public string DirName { get; set; }
            public string DirContactNum { get; set; }
        }

        class JSON_AddCase
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Category { get; set; }
            public string QryGroup3 { get; set; }
            public string U_VNDR_RP_FIRM_SELL { get; set; }
            public string U_VNDR_RP_LWYR_SELL { get; set; }
            public string QryGroup4 { get; set; }
            public string U_VNDR_RP_FIRM_BUY { get; set; }
            public string U_VNDR_RP_LWYR_BUY { get; set; }
            public string QryGroup6 { get; set; }
            public string QryGroup5 { get; set; }
            public string QryGroup7 { get; set; }
            public string QryGroup8 { get; set; }
            public string QryGroup9 { get; set; }
            public string QryGroup10 { get; set; }
            public string QryGroup11 { get; set; }
        }

        class JSON_AddCase_Questions
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Category { get; set; }
            public string QryGroup3 { get; set; }
            public string VNDR_RP_FIRM_SELL { get; set; }
            public string VNDR_RP_LWYR_SELL { get; set; }
            public string QryGroup4 { get; set; }
            public string VNDR_RP_FIRM_BUY { get; set; }
            public string VNDR_RP_LWYR_BUY { get; set; }
            public string QryGroup6 { get; set; }
            public string QryGroup5 { get; set; }
            public string QryGroup7 { get; set; }
            public string QryGroup8 { get; set; }
            public string QryGroup9 { get; set; }
            public string QryGroup10 { get; set; }
            public string QryGroup11 { get; set; }
            public string QryGroup17 { get; set; }
        }

        class JSON_AddCase_ListofItems
        {
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
        }

        class JSON_AddCase_DocumentToRead
        {
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string FileName { get; set; }
            //public string sDoc { get; set; }
            public string CardCode { get; set; }
        }

        class DocumentToRead
        {
            public string Message { get; set; }
            public string CODE { get; set; }
            public string TITLETYPE { get; set; }
            public string TITLENO { get; set; }
            public string LOTTYPE { get; set; }
            public string LOTNO { get; set; }
            public string FORMERLY_KNOWN_AS { get; set; }
            public string BPM { get; set; }
            public string STATE { get; set; }
            public string AREA { get; set; }
            public string LOTAREA_SQM { get; set; }
            public string LOTAREA_SQFT { get; set; }
            public string LASTUPDATEDON { get; set; }
            public string DEVELOPER { get; set; }
            public string DVLPR_CODE { get; set; }
            public string PROJECT_CODE { get; set; }
            public string PROJECTNAME { get; set; }
            public string DEVLICNO { get; set; }
            public string DEVSOLICTOR { get; set; }
            public string DVLPR_SOL_CODE { get; set; }
            public string DVLPR_LOC { get; set; }
            public string LSTCHG_BANKCODE { get; set; }
            public string LSTCHG_BANKNAME { get; set; }
            public string LSTCHG_BRANCH { get; set; }
            public string LSTCHG_PANO { get; set; }
            public string LSTCHG_PRSTNO { get; set; }
        }

        class JSON_AddCase_AddProperty
        {
            public string CARDCODE { get; set; }
            public string CODE { get; set; }
            public string TITLETYPE { get; set; }
            public string TITLENO { get; set; }
            public string LOTTYPE { get; set; }
            public string LOTNO { get; set; }
            public string FORMERLY_KNOWN_AS { get; set; }
            public string BPM { get; set; }
            public string STATE { get; set; }
            public string AREA { get; set; }
            //public string LOTAREA_SQM { get; set; }
            //public string LOTAREA_SQFT { get; set; }
            public string LOTAREA { get; set; }
            public string LASTUPDATEDON { get; set; }
            public string DEVELOPER { get; set; }
            public string DVLPR_CODE { get; set; }
            public string PROJECT_CODE { get; set; }
            public string PROJECTNAME { get; set; }
            public string DEVLICNO { get; set; }
            public string DEVSOLICTOR { get; set; }
            public string DVLPR_SOL_CODE { get; set; }
            public string DVLPR_LOC { get; set; }
            public string LSTCHG_BANKCODE { get; set; }
            public string LSTCHG_BANKNAME { get; set; }
            public string LSTCHG_BRANCH { get; set; }
            public string LSTCHG_PANO { get; set; }
            public string LSTCHG_PRSTNO { get; set; }
        }

        class JSON_AddCase_AddIndividualSearch
        {
            public string CardCode { get; set; }
            public string Code { get; set; }
            public string DocEntry { get; set; }
            public string EmployeeName { get; set; }
            public string IDType { get; set; }
            public string Title { get; set; }
            public string Gender { get; set; }
            public string IDNo1 { get; set; }
            public string IDNo3 { get; set; }
            public string TaxNo { get; set; }
            public string MobileNo { get; set; }
            public string Telephone { get; set; }
            public string OfficeNo { get; set; }
            public string IDAddress1 { get; set; }
            public string IDAddress2 { get; set; }
            public string IDAddress3 { get; set; }
            public string IDAddress4 { get; set; }
            public string IDAddress5 { get; set; }
            public string CorresAddr1 { get; set; }
            public string CorresAddr2 { get; set; }
            public string CorresAddr3 { get; set; }
            public string CorresAddr4 { get; set; }
            public string CorresAddr5 { get; set; }
            public string AddressToUse { get; set; }
            public string LastUpdatedOn { get; set; }
        }

        class JSON_AddCase_ScanIC
        {
            public Byte[] DocBinaryArray { get; set; }
        }

        class ScanIC
        {
            public string Message { get; set; }
            public string Code { get; set; }
            public string DocEntry { get; set; }
            public string EmployeeName { get; set; }
            public string Title { get; set; }
            public string Gender { get; set; }
            public string IDNo1 { get; set; }
            public string IDNo3 { get; set; }
            public string TaxNo { get; set; }
            public string MobileNo { get; set; }
            public string Telephone { get; set; }
            public string OfficeNo { get; set; }
            public string IDAddress1 { get; set; }
            public string IDAddress2 { get; set; }
            public string IDAddress3 { get; set; }
            public string IDAddress4 { get; set; }
            public string IDAddress5 { get; set; }
            public string CorresAddr1 { get; set; }
            public string CorresAddr2 { get; set; }
            public string CorresAddr3 { get; set; }
            public string CorresAddr4 { get; set; }
            public string CorresAddr5 { get; set; }
            public string AddressToUse { get; set; }
            public string LastUpdatedOn { get; set; }
        }

        class JSON_AddCase_ConvertToOriginalOCRD
        {
            public string CardCode { get; set; }
            public string Category { get; set; }
            public string UserName { get; set; }
        }

        #endregion

        #region tempTables

        private DataTable Edit_PropertyEnquiry(JSON_EditPropertyEnquiryDetails objEditDetails)
        {
            DataTable tbNew = new DataTable();
            tbNew = EditPropertyEnquiry();

            DataRow rowNew = tbNew.NewRow();

            rowNew["CODE"] = objEditDetails.CODE;
            rowNew["TITLETYPE"] = objEditDetails.TITLETYPE;
            rowNew["TITLENO"] = objEditDetails.TITLENO;
            rowNew["LOTTYPE"] = objEditDetails.LOTTYPE;
            rowNew["LOTNO"] = objEditDetails.LOTNO;
            rowNew["FORMERLY_KNOWN_AS"] = objEditDetails.FORMERLY_KNOWN_AS;
            rowNew["BPM"] = objEditDetails.BPM;
            rowNew["STATE"] = objEditDetails.STATE;
            rowNew["AREA"] = objEditDetails.AREA;
            rowNew["LOTAREA_SQM"] = objEditDetails.LOTAREA_SQM;
            rowNew["LOTAREA_SQFT"] = objEditDetails.LOTAREA_SQFT;
            rowNew["LASTUPDATEDON"] = objEditDetails.LASTUPDATEDON;
            rowNew["DEVELOPER"] = objEditDetails.DEVELOPER;
            rowNew["DVLPR_CODE"] = objEditDetails.DVLPR_CODE;
            rowNew["PROJECT_CODE"] = objEditDetails.PROJECT_CODE;
            rowNew["PROJECTNAME"] = objEditDetails.PROJECTNAME;
            rowNew["DEVLICNO"] = objEditDetails.DEVLICNO;
            rowNew["DEVSOLICTOR"] = objEditDetails.DEVSOLICTOR;
            rowNew["DVLPR_SOL_CODE"] = objEditDetails.DVLPR_SOL_CODE;
            rowNew["DVLPR_LOC"] = objEditDetails.DVLPR_LOC;
            rowNew["LSTCHG_BANKCODE"] = objEditDetails.LSTCHG_BANKCODE;
            rowNew["LSTCHG_BANKNAME"] = objEditDetails.LSTCHG_BANKNAME;
            rowNew["LSTCHG_BRANCH"] = objEditDetails.LSTCHG_BRANCH;
            rowNew["LSTCHG_PANO"] = objEditDetails.LSTCHG_PANO;
            rowNew["LSTCHG_PRSTNO"] = objEditDetails.LSTCHG_PRSTNO;

            tbNew.Rows.Add(rowNew);

            return tbNew.Copy();
        }

        private DataTable EditPropertyEnquiry()
        {
            DataTable tbProperty = new DataTable();

            tbProperty.Columns.Add("CODE");
            tbProperty.Columns.Add("TITLETYPE");
            tbProperty.Columns.Add("TITLENO");
            tbProperty.Columns.Add("LOTTYPE");
            tbProperty.Columns.Add("LOTNO");
            tbProperty.Columns.Add("FORMERLY_KNOWN_AS");
            tbProperty.Columns.Add("BPM");
            tbProperty.Columns.Add("STATE");
            tbProperty.Columns.Add("AREA");
            tbProperty.Columns.Add("LOTAREA_SQM");
            tbProperty.Columns.Add("LOTAREA_SQFT");
            tbProperty.Columns.Add("LASTUPDATEDON");
            tbProperty.Columns.Add("DEVELOPER");
            tbProperty.Columns.Add("DVLPR_CODE");
            tbProperty.Columns.Add("PROJECT_CODE");
            tbProperty.Columns.Add("PROJECTNAME");
            tbProperty.Columns.Add("DEVLICNO");
            tbProperty.Columns.Add("DEVSOLICTOR");
            tbProperty.Columns.Add("DVLPR_SOL_CODE");
            tbProperty.Columns.Add("DVLPR_LOC");
            tbProperty.Columns.Add("LSTCHG_BANKCODE");
            tbProperty.Columns.Add("LSTCHG_BANKNAME");
            tbProperty.Columns.Add("LSTCHG_BRANCH");
            tbProperty.Columns.Add("LSTCHG_PANO");
            tbProperty.Columns.Add("LSTCHG_PRSTNO");

            return tbProperty;
        }

        private DataTable Add_Individual(JSON_AddIndividualSearch objAddIndividuals)
        {
            DataTable tbNew = new DataTable();
            tbNew = AddIndividual();

            DataRow rowNew = tbNew.NewRow();

            rowNew["Code"] = objAddIndividuals.Code;
            rowNew["DocEntry"] = objAddIndividuals.DocEntry;
            rowNew["EmployeeName"] = objAddIndividuals.EmployeeName;
            rowNew["Title"] = objAddIndividuals.Title;
            rowNew["Gender"] = objAddIndividuals.Gender;
            rowNew["IDNo1"] = objAddIndividuals.IDNo1;
            rowNew["IDNo3"] = objAddIndividuals.IDNo3;
            rowNew["TaxNo"] = objAddIndividuals.TaxNo;
            rowNew["MobileNo"] = objAddIndividuals.MobileNo;
            rowNew["Telephone"] = objAddIndividuals.Telephone;
            rowNew["OfficeNo"] = objAddIndividuals.OfficeNo;
            rowNew["IDAddress1"] = objAddIndividuals.IDAddress1;
            rowNew["IDAddress2"] = objAddIndividuals.IDAddress2;
            rowNew["IDAddress3"] = objAddIndividuals.IDAddress3;
            rowNew["IDAddress4"] = objAddIndividuals.IDAddress4;
            rowNew["IDAddress5"] = objAddIndividuals.IDAddress5;
            rowNew["CorresAddr1"] = objAddIndividuals.CorresAddr1;
            rowNew["CorresAddr2"] = objAddIndividuals.CorresAddr2;
            rowNew["CorresAddr3"] = objAddIndividuals.CorresAddr3;
            rowNew["CorresAddr4"] = objAddIndividuals.CorresAddr4;
            rowNew["CorresAddr5"] = objAddIndividuals.CorresAddr5;
            rowNew["AddressToUse"] = objAddIndividuals.AddressToUse;
            rowNew["LastUpdatedOn"] = objAddIndividuals.LastUpdatedOn;

            tbNew.Rows.Add(rowNew);
            return tbNew.Copy();
        }

        private DataTable AddIndividual()
        {
            DataTable tbIndividual = new DataTable();

            tbIndividual.Columns.Add("Code");
            tbIndividual.Columns.Add("DocEntry");
            tbIndividual.Columns.Add("EmployeeName");
            tbIndividual.Columns.Add("Title");
            tbIndividual.Columns.Add("Gender");
            tbIndividual.Columns.Add("IDNo1");
            tbIndividual.Columns.Add("IDNo3");
            tbIndividual.Columns.Add("TaxNo");
            tbIndividual.Columns.Add("MobileNo");
            tbIndividual.Columns.Add("Telephone");
            tbIndividual.Columns.Add("OfficeNo");
            tbIndividual.Columns.Add("IDAddress1");
            tbIndividual.Columns.Add("IDAddress2");
            tbIndividual.Columns.Add("IDAddress3");
            tbIndividual.Columns.Add("IDAddress4");
            tbIndividual.Columns.Add("IDAddress5");
            tbIndividual.Columns.Add("CorresAddr1");
            tbIndividual.Columns.Add("CorresAddr2");
            tbIndividual.Columns.Add("CorresAddr3");
            tbIndividual.Columns.Add("CorresAddr4");
            tbIndividual.Columns.Add("CorresAddr5");
            tbIndividual.Columns.Add("AddressToUse");
            tbIndividual.Columns.Add("LastUpdatedOn");

            return tbIndividual;
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        #endregion
    }
}

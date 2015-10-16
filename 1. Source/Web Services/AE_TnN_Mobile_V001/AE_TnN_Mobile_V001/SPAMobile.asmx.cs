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
using System.Data.SqlClient;


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
        clsProcessCase oProcessCase = new clsProcessCase();
        clsEncryptDecrypt clsEncypt = new clsEncryptDecrypt();
        JavaScriptSerializer js = new JavaScriptSerializer();
        List<result> lstResult = new List<result>();
        SAPbobsCOM.Company oDICompany;
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public static string FileUploadPath = ConfigurationManager.AppSettings["FileUploadPath"].ToString();
        public static string FileUploadEncryptedPath = ConfigurationManager.AppSettings["FileUploadEncryptedPath"].ToString();
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

        #region CaseEnquiry

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_CaseEnquiry(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "SPA_CaseEnquiry()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                string sInput1 = string.Empty;
                string sInput2 = string.Empty;
                string sInput3 = string.Empty;
                string sInput4 = string.Empty;
                string sInput5 = string.Empty;
                string sInput6 = string.Empty;
                string sInput7 = string.Empty;
                List<RelatedCases> lstCaseInfo = new List<RelatedCases>();

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_CaseEnquiry> lstDeserialize = js.Deserialize<List<JSON_CaseEnquiry>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    JSON_CaseEnquiry objLstInfo = lstDeserialize[0];

                    sInput1 = objLstInfo.FileOpenDateFrom;
                    sInput2 = objLstInfo.FileOpenDateTo;
                    sInput3 = objLstInfo.CaseFileNo;
                    sInput4 = objLstInfo.CaseType;
                    sInput5 = objLstInfo.ClientName;
                    sInput6 = objLstInfo.AmountFrom;
                    sInput7 = objLstInfo.AmountTo;
                    sInput1 = objLstInfo.FileOpenDateFrom;
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_CaseEnquiry() ", sFuncName);
                DataSet ds = oDashboard.SPA_CaseEnquiry(sInput1, sInput2, sInput3, sInput4, sInput5, sInput6, sInput7);
                //DataSet ds = new DataSet();
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_CaseEnquiry() ", sFuncName);
                if (ds != null && ds.Tables.Count > 0)
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            RelatedCases _RelatedInfo = new RelatedCases();

                            _RelatedInfo.CaseFileNo = r["CaseFileNo"].ToString();
                            _RelatedInfo.RelatedFileNo = r["RelatedCase"].ToString();
                            _RelatedInfo.BranchCode = r["Branch"].ToString();
                            _RelatedInfo.FileOpenedDate = r["FileOpenDate"].ToString();
                            _RelatedInfo.IC = r["InChargeName"].ToString();
                            _RelatedInfo.CaseType = r["CaseType"].ToString();
                            _RelatedInfo.ClientName = r["FirstClientName"].ToString();
                            _RelatedInfo.BankName = "";
                            _RelatedInfo.Branch = "";
                            _RelatedInfo.LOTNo = r["LOTNo"].ToString();
                            _RelatedInfo.CaseAmount = r["CaseAmount"].ToString();
                            _RelatedInfo.UserCode = r["UserCode"].ToString();
                            _RelatedInfo.Status = r["CaseStatus"].ToString();
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
                    Context.Response.Output.Write(js.Serialize(lstCaseInfo));
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

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_OpenCases(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "SPA_OpenCases()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                string sFilterType = "";
                string sCaseStatus = "Open";
                string sUserCode = string.Empty;

                List<RelatedCases> lstCaseInfo = new List<RelatedCases>();

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_OpenCase> lstDeserialize = js.Deserialize<List<JSON_OpenCase>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    JSON_OpenCase objLstInfo = lstDeserialize[0];

                    sUserCode = objLstInfo.sUserName;
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_DashboardCaseButtonInfo() ", sFuncName);
                DataSet ds = oDashboard.SPA_DashboardCaseButtonInfo(sFilterType, sCaseStatus, sUserCode);
                //DataSet ds = new DataSet();
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_DashboardCaseButtonInfo() ", sFuncName);
                if (ds != null && ds.Tables.Count > 0)
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            RelatedCases _RelatedInfo = new RelatedCases();

                            _RelatedInfo.CaseFileNo = r["CaseFileNo"].ToString();
                            _RelatedInfo.RelatedFileNo = r["RelatedCase"].ToString();
                            _RelatedInfo.BranchCode = r["Branch"].ToString();
                            _RelatedInfo.FileOpenedDate = r["FileOpenDate"].ToString();
                            _RelatedInfo.IC = r["InChargeName"].ToString();
                            _RelatedInfo.CaseType = r["CaseType"].ToString();
                            _RelatedInfo.ClientName = r["FirstClientName"].ToString();
                            _RelatedInfo.BankName = "";
                            _RelatedInfo.Branch = "";
                            _RelatedInfo.LOTNo = r["LOTNo"].ToString();
                            _RelatedInfo.CaseAmount = r["CaseAmount"].ToString();
                            _RelatedInfo.UserCode = r["UserCode"].ToString();
                            _RelatedInfo.Status = r["CaseStatus"].ToString();
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

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Serializing the Open Case Information ", sFuncName);
                    Context.Response.Output.Write(js.Serialize(lstCaseInfo));
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Serializing the Open Case Details , the Serialized data is ' " + js.Serialize(lstCaseInfo) + " '", sFuncName);
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
                    Context.Response.Output.Write(js.Serialize(lstCaseInfo));
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
        public void SPA_ActionCases(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "SPA_ActionCases()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                string sFilterType = "Pend";
                string sCaseStatus = "Open";
                string sUserCode = string.Empty;

                List<RelatedCases> lstCaseInfo = new List<RelatedCases>();

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_OpenCase> lstDeserialize = js.Deserialize<List<JSON_OpenCase>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    JSON_OpenCase objLstInfo = lstDeserialize[0];

                    sUserCode = objLstInfo.sUserName;
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_DashboardCaseButtonInfo() ", sFuncName);
                DataSet ds = oDashboard.SPA_DashboardCaseButtonInfo(sFilterType, sCaseStatus, sUserCode);
                //DataSet ds = new DataSet();
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_DashboardCaseButtonInfo() ", sFuncName);
                if (ds != null && ds.Tables.Count > 0)
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            RelatedCases _RelatedInfo = new RelatedCases();

                            _RelatedInfo.CaseFileNo = r["CaseFileNo"].ToString();
                            _RelatedInfo.RelatedFileNo = r["RelatedCase"].ToString();
                            _RelatedInfo.BranchCode = r["Branch"].ToString();
                            _RelatedInfo.FileOpenedDate = r["FileOpenDate"].ToString();
                            _RelatedInfo.IC = r["InChargeName"].ToString();
                            _RelatedInfo.CaseType = r["CaseType"].ToString();
                            _RelatedInfo.ClientName = r["FirstClientName"].ToString();
                            _RelatedInfo.BankName = "";
                            _RelatedInfo.Branch = "";
                            _RelatedInfo.LOTNo = r["LOTNo"].ToString();
                            _RelatedInfo.CaseAmount = r["CaseAmount"].ToString();
                            _RelatedInfo.UserCode = r["UserCode"].ToString();
                            _RelatedInfo.Status = r["CaseStatus"].ToString();
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

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Serializing the Action Case Information ", sFuncName);
                    Context.Response.Output.Write(js.Serialize(lstCaseInfo));
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Serializing the Action Case Details , the Serialized data is ' " + js.Serialize(lstCaseInfo) + " '", sFuncName);
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
                    Context.Response.Output.Write(js.Serialize(lstCaseInfo));
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
        public void SPA_PriorityCases(string sJsonInput)
        {
            string sFuncName = string.Empty;
            try
            {
                sFuncName = "SPA_PriorityCases()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                string sFilterType = "Priority";
                string sCaseStatus = "Open";
                string sUserCode = string.Empty;

                List<RelatedCases> lstCaseInfo = new List<RelatedCases>();

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_OpenCase> lstDeserialize = js.Deserialize<List<JSON_OpenCase>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    JSON_OpenCase objLstInfo = lstDeserialize[0];

                    sUserCode = objLstInfo.sUserName;
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_DashboardCaseButtonInfo() ", sFuncName);
                DataSet ds = oDashboard.SPA_DashboardCaseButtonInfo(sFilterType, sCaseStatus, sUserCode);
                //DataSet ds = new DataSet();
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_DashboardCaseButtonInfo() ", sFuncName);
                if (ds != null && ds.Tables.Count > 0)
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            RelatedCases _RelatedInfo = new RelatedCases();

                            _RelatedInfo.CaseFileNo = r["CaseFileNo"].ToString();
                            _RelatedInfo.RelatedFileNo = r["RelatedCase"].ToString();
                            _RelatedInfo.BranchCode = r["Branch"].ToString();
                            _RelatedInfo.FileOpenedDate = r["FileOpenDate"].ToString();
                            _RelatedInfo.IC = r["InChargeName"].ToString();
                            _RelatedInfo.CaseType = r["CaseType"].ToString();
                            _RelatedInfo.ClientName = r["FirstClientName"].ToString();
                            _RelatedInfo.BankName = "";
                            _RelatedInfo.Branch = "";
                            _RelatedInfo.LOTNo = r["LOTNo"].ToString();
                            _RelatedInfo.CaseAmount = r["CaseAmount"].ToString();
                            _RelatedInfo.UserCode = r["UserCode"].ToString();
                            _RelatedInfo.Status = r["CaseStatus"].ToString();
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

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Serializing the Priority Case Information ", sFuncName);
                    Context.Response.Output.Write(js.Serialize(lstCaseInfo));
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Serializing the Priority Case Details , the Serialized data is ' " + js.Serialize(lstCaseInfo) + " '", sFuncName);
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
                    Context.Response.Output.Write(js.Serialize(lstCaseInfo));
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
                            _DetailInfo.LOTAREA = r["LOTAREA"].ToString();
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
                        _DetailInfo.LOTAREA = string.Empty;
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
                    DataTable dt = SPA_AddCase_SaveAttachment(objLstInfo.FileName, objLstInfo.ItemCode, objLstInfo.ItemName, objLstInfo.CardCode);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DocumentToRead _DetailInfo = new DocumentToRead();

                        DataTable dtTransposed = GenerateTransposedTable(dt);
                        DataColumnCollection columns = dtTransposed.Columns;

                        _DetailInfo.Message = "";
                        _DetailInfo.CODE = string.Empty;
                        _DetailInfo.TITLETYPE = string.Empty;
                        if (columns.Contains("U_TITLENO"))
                        {
                            _DetailInfo.TITLENO = dtTransposed.Rows[0]["U_TITLENO"].ToString();
                        }
                        else
                        {
                            _DetailInfo.TITLENO = string.Empty;
                        }
                        _DetailInfo.LOTTYPE = string.Empty;
                        if (columns.Contains("U_LOTNO"))
                        {
                            _DetailInfo.LOTNO = dtTransposed.Rows[0]["U_LOTNO"].ToString();
                        }
                        else
                        {
                            _DetailInfo.LOTNO = string.Empty;
                        }
                        if (columns.Contains("U_FORMERLY_KNOWN_AS"))
                        {
                            _DetailInfo.FORMERLY_KNOWN_AS = dtTransposed.Rows[0]["U_FORMERLY_KNOWN_AS"].ToString();
                        }
                        else
                        {
                            _DetailInfo.FORMERLY_KNOWN_AS = string.Empty;
                        }
                        if (columns.Contains("U_BPM"))
                        {
                            _DetailInfo.BPM = dtTransposed.Rows[0]["U_BPM"].ToString();
                        }
                        else
                        {
                            _DetailInfo.BPM = string.Empty;
                        }
                        if (columns.Contains("U_STATE"))
                        {
                            _DetailInfo.STATE = dtTransposed.Rows[0]["U_STATE"].ToString();
                        }
                        else
                        {
                            _DetailInfo.STATE = string.Empty;
                        }
                        if (columns.Contains("U_AREA"))
                        {
                            _DetailInfo.AREA = dtTransposed.Rows[0]["U_AREA"].ToString();
                        }
                        else
                        {
                            _DetailInfo.AREA = string.Empty;
                        }
                        if (columns.Contains("U_LOTAREA"))
                        {
                            _DetailInfo.LOTAREA = dtTransposed.Rows[0]["U_LOTAREA"].ToString();
                        }
                        else
                        {
                            _DetailInfo.LOTAREA = string.Empty;
                        }
                        _DetailInfo.LASTUPDATEDON = string.Empty;
                        _DetailInfo.DEVELOPER = string.Empty;
                        _DetailInfo.PROJECTNAME = string.Empty;
                        _DetailInfo.DEVLICNO = string.Empty;
                        _DetailInfo.DEVSOLICTOR = string.Empty;
                        _DetailInfo.DVLPR_LOC = string.Empty;
                        _DetailInfo.LSTCHG_BANKNAME = string.Empty;
                        _DetailInfo.LSTCHG_BRANCH = string.Empty;
                        _DetailInfo.LSTCHG_PANO = string.Empty;
                        _DetailInfo.LSTCHG_PRSTNO = string.Empty;
                        lstCaseInfo.Add(_DetailInfo);
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
                        _DetailInfo.LOTAREA = "";
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
                    //Byte[] bBinaryData = lstDeserialize[0].DocBinaryArray;
                    // Call the web method by passing the Binary data to the method and get the result

                    JSON_AddCase_ScanIC objLstInfo = lstDeserialize[0];

                    DataTable dt = SPA_AddCase_ScanIC(objLstInfo.FileName, objLstInfo.ItemCode, objLstInfo.ItemName, objLstInfo.ICType);
                    List<ScanIC> lstSearchInfo = new List<ScanIC>();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataTable dtTransposed = GenerateTransposedTable(dt);
                        DataColumnCollection columns = dtTransposed.Columns;

                        ScanIC _SearchInfo = new ScanIC();
                        _SearchInfo.Message = "";
                        _SearchInfo.ScanICLocation = FileUploadPath + objLstInfo.FileName;
                        _SearchInfo.ScanFrontICLocation = string.Empty;
                        _SearchInfo.ScanFrontICLocation = string.Empty;
                        if (objLstInfo.ICType == "Front")
                        {
                            if (columns.Contains("U_NAME"))
                            {
                                _SearchInfo.EmployeeName = dtTransposed.Rows[0]["U_NAME"].ToString();
                            }
                            else
                            {
                                _SearchInfo.EmployeeName = string.Empty;
                            }
                            if (columns.Contains("U_GENDER"))
                            {
                                _SearchInfo.Gender = dtTransposed.Rows[0]["U_GENDER"].ToString();
                            }
                            else
                            {
                                _SearchInfo.Gender = string.Empty;
                            }
                            if (columns.Contains("U_IDNO_F1"))
                            {
                                _SearchInfo.IDNo1 = dtTransposed.Rows[0]["U_IDNO_F1"].ToString();
                            }
                            else
                            {
                                _SearchInfo.IDNo1 = string.Empty;
                            }
                            if (columns.Contains("U_ADDRESS_ID"))
                            {
                                int sLen = dtTransposed.Rows[0]["U_ADDRESS_ID"].ToString().Length;
                                _SearchInfo.IDAddress1 = dtTransposed.Rows[0]["U_ADDRESS_ID"].ToString().Substring(0, 20);
                                _SearchInfo.IDAddress2 = dtTransposed.Rows[0]["U_ADDRESS_ID"].ToString().Substring((_SearchInfo.IDAddress1.Length), (sLen - (_SearchInfo.IDAddress1.Length)));
                            }
                            else
                            {
                                _SearchInfo.IDAddress1 = string.Empty;
                                _SearchInfo.IDAddress2 = string.Empty;
                            }

                        }
                        else if (objLstInfo.ICType == "Back")
                        {
                            if (columns.Contains("U_TITLENO"))
                            {
                                _SearchInfo.IDNo3 = dtTransposed.Rows[0]["U_TITLENO"].ToString();
                            }
                            else
                            {
                                _SearchInfo.IDNo3 = string.Empty;
                            }
                        }
                        _SearchInfo.Code = string.Empty;
                        _SearchInfo.DocEntry = string.Empty;
                        _SearchInfo.Title = string.Empty;
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
                    else
                    {
                        ScanIC _SearchInfo = new ScanIC();

                        _SearchInfo.Message = "OCR Cannot able to Read the data from Scanned File.Kindly Key in.";
                        _SearchInfo.ScanICLocation = FileUploadEncryptedPath + objLstInfo.FileName;
                        _SearchInfo.ScanFrontICLocation = string.Empty;
                        _SearchInfo.ScanFrontICLocation = string.Empty;
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
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Serializing the Scan Information ", sFuncName);
                    Context.Response.Output.Write(js.Serialize(lstSearchInfo));
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Serializing the Scan Informations , the Serialized data is ' " + js.Serialize(lstSearchInfo) + " '", sFuncName);
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

        #region ProcessCase

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void SPA_ProcessCase_GetDataFromOCRD(string sJsonInput)
        {
            string sFuncName = string.Empty;
            ProcessCase_Details oDetails = new ProcessCase_Details();
            ProcessCase_Purchaser oPurchase = new ProcessCase_Purchaser();
            ProcessCase_Vendor oVendor = new ProcessCase_Vendor();
            ProcessCase_Property oProperty = new ProcessCase_Property();
            ProcessCase_LoanPrinciple oLoanPrinciple = new ProcessCase_LoanPrinciple();
            ProcessCase_LoanSubsidary oLoanSubsidary = new ProcessCase_LoanSubsidary();
            ProcessCase_GetDataFromOCRD oHeader = new ProcessCase_GetDataFromOCRD();
            List<ProcessCase_GetDataFromOCRD> lstProcessCase = new List<ProcessCase_GetDataFromOCRD>();
            try
            {
                List<ProcessCase_GetDataFromOCRD> lstSearchInfo = new List<ProcessCase_GetDataFromOCRD>();
                sFuncName = "SPA_ProcessCase_GetDataFromOCRD()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                string sCaseNo = string.Empty;
                string sUserName = string.Empty;
                string sUserRole = string.Empty;

                sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_ProcessCase_GetDataFromOCRD> lstDeserialize = js.Deserialize<List<JSON_ProcessCase_GetDataFromOCRD>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    JSON_ProcessCase_GetDataFromOCRD objLstInfo = lstDeserialize[0];

                    sCaseNo = objLstInfo.CaseNo;
                    sUserName = objLstInfo.UserName;
                    sUserRole = objLstInfo.UserRole;
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_ProcessCase_GetDataFromOCRD() ", sFuncName);
                DataSet ds = oProcessCase.SPA_ProcessCase_GetDataFromOCRD(sCaseNo, sUserName, sUserRole);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            // This is for the Details Tab
                            oDetails.LA = dr["LA"].ToString();
                            oDetails.MANAGER = dr["MANAGER"].ToString();
                            oDetails.InCharge = dr["InCharge"].ToString();
                            oDetails.CustomerService = dr["CustomerService"].ToString();
                            oDetails.CaseType = dr["CaseType"].ToString();
                            oDetails.FileLocation = dr["FileLocation"].ToString();
                            oDetails.FileClosedDate = dr["FileClosedDate"].ToString();
                            oDetails.VendAcqDt = dr["VendAcqDt"].ToString();
                            oDetails.CompanyBuisnessSearch = dr["CompanyBuisnessSearch"].ToString();
                            oDetails.BankWindingSearch = dr["BankWindingSearch"].ToString();

                            // This is for the Purchase Tab
                            oPurchase.PurRepresentedByFirm = dr["PurRepresentedByFirm"].ToString();
                            oPurchase.PurlawyerRepresented = dr["PurlawyerRepresented"].ToString();
                            oPurchase.PurSPADate = dr["PurSPADate"].ToString();
                            oPurchase.PurEntryOfPrivateCaveat = dr["PurEntryOfPrivateCaveat"].ToString();
                            oPurchase.PurWithOfPrivateCaveat = dr["PurWithOfPrivateCaveat"].ToString();
                            oPurchase.PurFirstName = dr["PurFirstName"].ToString();
                            oPurchase.PurFirstID = dr["PurFirstID"].ToString();
                            oPurchase.PurFirstTaxNo = dr["PurFirstTaxNo"].ToString();
                            oPurchase.PurFirstContactNo = dr["PurFirstContactNo"].ToString();
                            oPurchase.PurFirstType = dr["PurFirstType"].ToString();
                            oPurchase.PurSecName = dr["PurSecName"].ToString();
                            oPurchase.PurSecID = dr["PurSecID"].ToString();
                            oPurchase.PurSecTaxNo = dr["PurSecTaxNo"].ToString();
                            oPurchase.PurSecContactNo = dr["PurSecContactNo"].ToString();
                            oPurchase.PurSecType = dr["PurSecType"].ToString();
                            oPurchase.PurThirdName = dr["PurThirdName"].ToString();
                            oPurchase.PurThirdID = dr["PurThirdID"].ToString();
                            oPurchase.PurThirdTaxNo = dr["PurThirdTaxNo"].ToString();
                            oPurchase.PurThirdContactNo = dr["PurThirdContactNo"].ToString();
                            oPurchase.PurThirdType = dr["PurThirdType"].ToString();
                            oPurchase.PurFourthName = dr["PurFourthName"].ToString();
                            oPurchase.PurFourthID = dr["PurFourthID"].ToString();
                            oPurchase.PurFourthTaxNo = dr["PurFourthTaxNo"].ToString();
                            oPurchase.PurFourthContactNo = dr["PurFourthContactNo"].ToString();
                            oPurchase.PurFourthType = dr["PurFourthType"].ToString();

                            // This is for the Vendor Tab
                            oVendor.VndrRepresentedByFirm = dr["VndrRepresentedByFirm"].ToString();
                            oVendor.VndrlawyerRepresented = dr["VndrlawyerRepresented"].ToString();
                            oVendor.VndrReqDevConsent = dr["VndrReqDevConsent"].ToString();
                            oVendor.VndrReceiveDevConsent = dr["VndrReceiveDevConsent"].ToString();
                            oVendor.VndrFirstName = dr["VndrFirstName"].ToString();
                            oVendor.VndrFirstID = dr["VndrFirstID"].ToString();
                            oVendor.VndrFirstTaxNo = dr["VndrFirstTaxNo"].ToString();
                            oVendor.VndrFirstContactNo = dr["VndrFirstContactNo"].ToString();
                            oVendor.VndrFirstType = dr["VndrFirstType"].ToString();
                            oVendor.VndrSecName = dr["VndrSecName"].ToString();
                            oVendor.VndrSecID = dr["VndrSecID"].ToString();
                            oVendor.VndrSecTaxNo = dr["VndrSecTaxNo"].ToString();
                            oVendor.VndrSecContactNo = dr["VndrSecContactNo"].ToString();
                            oVendor.VndrSecType = dr["VndrSecType"].ToString();
                            oVendor.VndrThirdName = dr["VndrThirdName"].ToString();
                            oVendor.VndrThirdID = dr["VndrThirdID"].ToString();
                            oVendor.VndrThirdTaxNo = dr["VndrThirdTaxNo"].ToString();
                            oVendor.VndrThirdContactNo = dr["VndrThirdContactNo"].ToString();
                            oVendor.VndrThirdType = dr["VndrThirdType"].ToString();
                            oVendor.VndrFourthName = dr["VndrFourthName"].ToString();
                            oVendor.VndrFourthID = dr["VndrFourthID"].ToString();
                            oVendor.VndrFourthTaxNo = dr["VndrFourthTaxNo"].ToString();
                            oVendor.VndrFourthContactNo = dr["VndrFourthContactNo"].ToString();
                            oVendor.VndrFourthType = dr["VndrFourthType"].ToString();

                            // This is for Property Tab
                            oProperty.TitleType = dr["TitleType"].ToString();
                            oProperty.CertifiedPlanNo = dr["CertifiedPlanNo"].ToString();
                            oProperty.LotNo = dr["LotNo"].ToString();
                            oProperty.PreviouslyKnownAs = dr["PreviouslyKnownAs"].ToString();
                            oProperty.State = dr["State"].ToString();
                            oProperty.Area = dr["Area"].ToString();
                            oProperty.BPM = dr["BPM"].ToString();
                            oProperty.GovSurvyPlan = dr["GovSurvyPlan"].ToString();
                            oProperty.LotArea = dr["LotArea"].ToString();
                            oProperty.Developer = dr["Developer"].ToString();
                            oProperty.Project = dr["Project"].ToString();
                            oProperty.DevLicenseNo = dr["DevLicenseNo"].ToString();
                            oProperty.DevSolicitor = dr["DevSolicitor"].ToString();
                            oProperty.DevSoliLoc = dr["DevSoliLoc"].ToString();
                            oProperty.TitleSearchDate = dr["TitleSearchDate"].ToString();
                            oProperty.DSCTransfer = dr["DSCTransfer"].ToString();
                            oProperty.DRCTransfer = dr["DRCTransfer"].ToString();
                            oProperty.FourteenADate = dr["FourteenADate"].ToString();
                            oProperty.DRTLRegistry = dr["DRTLRegistry"].ToString();
                            oProperty.PropertyCharged = dr["PropertyCharged"].ToString();
                            oProperty.BankName = dr["BankName"].ToString();
                            oProperty.Branch = dr["Branch"].ToString();
                            oProperty.PAName = dr["PAName"].ToString();
                            oProperty.PresentationNo = dr["PresentationNo"].ToString();
                            oProperty.ExistChargeRef = dr["ExistChargeRef"].ToString();
                            oProperty.ReceiptType = dr["ReceiptType"].ToString();
                            oProperty.ReceiptNo = dr["ReceiptNo"].ToString();
                            oProperty.ReceiptDate = dr["ReceiptDate"].ToString();
                            oProperty.PurchasePrice = dr["PurchasePrice"].ToString();
                            oProperty.AdjValue = dr["AdjValue"].ToString();
                            oProperty.VndrPrevSPAValue = dr["VndrPrevSPAValue"].ToString();
                            oProperty.Deposit = dr["Deposit"].ToString();
                            oProperty.BalPurPrice = dr["BalPurPrice"].ToString();
                            oProperty.LoanAmount = dr["LoanAmount"].ToString();
                            oProperty.LoanCaseNo = dr["LoanCaseNo"].ToString();
                            oProperty.DiffSum = dr["DiffSum"].ToString();
                            oProperty.RedAmt = dr["RedAmt"].ToString();
                            oProperty.RedDate = dr["RedDate"].ToString();
                            oProperty.DefRdmptSum = dr["DefRdmptSum"].ToString();

                            // This is for Loan Principle Tab

                            oLoanPrinciple.ReqRedStatement = dr["ReqRedStatement"].ToString();
                            oLoanPrinciple.RedStmtDate = dr["RedStmtDate"].ToString();
                            oLoanPrinciple.RedPayDate = dr["RedPayDate"].ToString();
                            oLoanPrinciple.RepByFirm = dr["RepByFirm"].ToString();
                            oLoanPrinciple.LoanCaseNo = dr["LoanCaseNo"].ToString();
                            oLoanPrinciple.Project = dr["Project"].ToString();
                            oLoanPrinciple.MasterBankName = dr["MasterBankName"].ToString();
                            oLoanPrinciple.BranchName = dr["BranchName"].ToString();
                            oLoanPrinciple.Address = dr["Address"].ToString();
                            oLoanPrinciple.PAName = dr["PAName"].ToString();
                            oLoanPrinciple.BankRef = dr["BankRef"].ToString();
                            oLoanPrinciple.BankInsDate = dr["BankInsDate"].ToString();
                            oLoanPrinciple.LOFDate = dr["LOFDate"].ToString();
                            oLoanPrinciple.BankSolicitor = dr["BankSolicitor"].ToString();
                            oLoanPrinciple.SoliLoc = dr["SoliLoc"].ToString();
                            oLoanPrinciple.SoliRef = dr["SoliRef"].ToString();
                            oLoanPrinciple.TypeofLoan = dr["TypeofLoan"].ToString();
                            oLoanPrinciple.TypeofFacility = dr["TypeofFacility"].ToString();
                            oLoanPrinciple.FacilityAmt = dr["FacilityAmt"].ToString();
                            oLoanPrinciple.Repaymt = dr["Repaymt"].ToString();
                            oLoanPrinciple.IntrstRate = dr["IntrstRate"].ToString();
                            oLoanPrinciple.MonthlyInstmt = dr["MonthlyInstmt"].ToString();
                            oLoanPrinciple.TermLoanAmt = dr["TermLoanAmt"].ToString();
                            oLoanPrinciple.Interest = dr["Interest"].ToString();
                            oLoanPrinciple.ODLoan = dr["ODLoan"].ToString();
                            oLoanPrinciple.MRTA = dr["MRTA"].ToString();
                            oLoanPrinciple.BankGuarantee = dr["BankGuarantee"].ToString();
                            oLoanPrinciple.LetterofCredit = dr["LetterofCredit"].ToString();
                            oLoanPrinciple.TrustReceipt = dr["TrustReceipt"].ToString();
                            oLoanPrinciple.Others = dr["Others"].ToString();
                            oLoanPrinciple.LoanDet1 = dr["LoanDet1"].ToString();
                            oLoanPrinciple.LoanDet2 = dr["LoanDet2"].ToString();
                            oLoanPrinciple.LoanDet3 = dr["LoanDet3"].ToString();
                            oLoanPrinciple.LoanDet4 = dr["LoanDet4"].ToString();
                            oLoanPrinciple.LoanDet5 = dr["LoanDet5"].ToString();

                            // This is for Loan Subsidary Tab

                            oLoanSubsidary.LoanDocForBankExe = dr["LoanDocForBankExe"].ToString();
                            oLoanSubsidary.FaciAgreeDate = dr["FaciAgreeDate"].ToString();
                            oLoanSubsidary.LoanDocRetFromBank = dr["LoanDocRetFromBank"].ToString();
                            oLoanSubsidary.DischargeofCharge = dr["DischargeofCharge"].ToString();
                            oLoanSubsidary.FirstTypeofFacility = dr["FirstTypeofFacility"].ToString();
                            oLoanSubsidary.FirstFacilityAmt = dr["FirstFacilityAmt"].ToString();
                            oLoanSubsidary.FirstRepaymt = dr["FirstRepaymt"].ToString();
                            oLoanSubsidary.FirstIntrstRate = dr["FirstIntrstRate"].ToString();
                            oLoanSubsidary.FirstMonthlyInstmt = dr["FirstMonthlyInstmt"].ToString();
                            oLoanSubsidary.FirstTermLoanAmt = dr["FirstTermLoanAmt"].ToString();
                            oLoanSubsidary.FirstInterest = dr["FirstInterest"].ToString();
                            oLoanSubsidary.FirstODLoan = dr["FirstODLoan"].ToString();
                            oLoanSubsidary.FirstMRTA = dr["FirstMRTA"].ToString();
                            oLoanSubsidary.FirstBankGuarantee = dr["FirstBankGuarantee"].ToString();
                            oLoanSubsidary.FirstLetterofCredit = dr["FirstLetterofCredit"].ToString();
                            oLoanSubsidary.FirstTrustReceipt = dr["FirstTrustReceipt"].ToString();
                            oLoanSubsidary.FirstOthers = dr["FirstOthers"].ToString();
                            oLoanSubsidary.SecTypeofFacility = dr["SecTypeofFacility"].ToString();
                            oLoanSubsidary.SecFacilityAmt = dr["SecFacilityAmt"].ToString();
                            oLoanSubsidary.SecRepaymt = dr["SecRepaymt"].ToString();
                            oLoanSubsidary.SecIntrstRate = dr["SecIntrstRate"].ToString();
                            oLoanSubsidary.SecMonthlyInstmt = dr["SecMonthlyInstmt"].ToString();
                            oLoanSubsidary.SecTermLoanAmt = dr["SecTermLoanAmt"].ToString();
                            oLoanSubsidary.SecInterest = dr["SecInterest"].ToString();
                            oLoanSubsidary.SecODLoan = dr["SecODLoan"].ToString();
                            oLoanSubsidary.SecMRTA = dr["SecMRTA"].ToString();
                            oLoanSubsidary.SecBankGuarantee = dr["SecBankGuarantee"].ToString();
                            oLoanSubsidary.SecLetterofCredit = dr["SecLetterofCredit"].ToString();
                            oLoanSubsidary.SecTrustReceipt = dr["SecTrustReceipt"].ToString();
                            oLoanSubsidary.SecOthers = dr["SecOthers"].ToString();
                            oLoanSubsidary.ThirdTypeofFacility = dr["ThirdTypeofFacility"].ToString();
                            oLoanSubsidary.ThirdFacilityAmt = dr["ThirdFacilityAmt"].ToString();
                            oLoanSubsidary.ThirdRepaymt = dr["ThirdRepaymt"].ToString();
                            oLoanSubsidary.ThirdIntrstRate = dr["ThirdIntrstRate"].ToString();
                            oLoanSubsidary.ThirdMonthlyInstmt = dr["ThirdMonthlyInstmt"].ToString();
                            oLoanSubsidary.ThirdTermLoanAmt = dr["ThirdTermLoanAmt"].ToString();
                            oLoanSubsidary.ThirdInterest = dr["ThirdInterest"].ToString();
                            oLoanSubsidary.ThirdODLoan = dr["ThirdODLoan"].ToString();
                            oLoanSubsidary.ThirdMRTA = dr["ThirdMRTA"].ToString();
                            oLoanSubsidary.ThirdBankGuarantee = dr["ThirdBankGuarantee"].ToString();
                            oLoanSubsidary.ThirdLetterofCredit = dr["ThirdLetterofCredit"].ToString();
                            oLoanSubsidary.ThirdTrustReceipt = dr["ThirdTrustReceipt"].ToString();
                            oLoanSubsidary.ThirdOthers = dr["ThirdOthers"].ToString();
                            oLoanSubsidary.FourthTypeofFacility = dr["FourthTypeofFacility"].ToString();
                            oLoanSubsidary.FourthFacilityAmt = dr["FourthFacilityAmt"].ToString();
                            oLoanSubsidary.FourthRepaymt = dr["FourthRepaymt"].ToString();
                            oLoanSubsidary.FourthIntrstRate = dr["FourthIntrstRate"].ToString();
                            oLoanSubsidary.FourthMonthlyInstmt = dr["FourthMonthlyInstmt"].ToString();
                            oLoanSubsidary.FourthTermLoanAmt = dr["FourthTermLoanAmt"].ToString();
                            oLoanSubsidary.FourthInterest = dr["FourthInterest"].ToString();
                            oLoanSubsidary.FourthODLoan = dr["FourthODLoan"].ToString();
                            oLoanSubsidary.FourthMRTA = dr["FourthMRTA"].ToString();
                            oLoanSubsidary.FourthBankGuarantee = dr["FourthBankGuarantee"].ToString();
                            oLoanSubsidary.FourthLetterofCredit = dr["FourthLetterofCredit"].ToString();
                            oLoanSubsidary.FourthTrustReceipt = dr["FourthTrustReceipt"].ToString();
                            oLoanSubsidary.FourthOthers = dr["FourthOthers"].ToString();
                            oLoanSubsidary.FifthTypeofFacility = dr["FifthTypeofFacility"].ToString();
                            oLoanSubsidary.FifthFacilityAmt = dr["FifthFacilityAmt"].ToString();
                            oLoanSubsidary.FifthRepaymt = dr["FifthRepaymt"].ToString();
                            oLoanSubsidary.FifthIntrstRate = dr["FifthIntrstRate"].ToString();
                            oLoanSubsidary.FifthMonthlyInstmt = dr["FifthMonthlyInstmt"].ToString();
                            oLoanSubsidary.FifthTermLoanAmt = dr["FifthTermLoanAmt"].ToString();
                            oLoanSubsidary.FifthInterest = dr["FifthInterest"].ToString();
                            oLoanSubsidary.FifthODLoan = dr["FifthODLoan"].ToString();
                            oLoanSubsidary.FifthMRTA = dr["FifthMRTA"].ToString();
                            oLoanSubsidary.FifthBankGuarantee = dr["FifthBankGuarantee"].ToString();
                            oLoanSubsidary.FifthLetterofCredit = dr["FifthLetterofCredit"].ToString();
                            oLoanSubsidary.FifthTrustReceipt = dr["FifthTrustReceipt"].ToString();
                            oLoanSubsidary.FifthOthers = dr["FifthOthers"].ToString();


                            // For holding the header information and the tab list of datas
                            oHeader.Case = dr["Case"].ToString();
                            oHeader.CaseType = dr["CaseType"].ToString();
                            oHeader.CaseStatus = dr["CaseStatus"].ToString();
                            oHeader.FileOpenDate = dr["FileOpenDate"].ToString();
                            oHeader.CaseFileNo = dr["CaseFileNo"].ToString();
                            oHeader.KIV = dr["KIV"].ToString();
                            oHeader.Details = oDetails;
                            oHeader.Purchaser = oPurchase;
                            oHeader.Vendor = oVendor;
                            oHeader.Property = oProperty;
                            oHeader.LoanPrinciple = oLoanPrinciple;
                            oHeader.LoanSubsidary = oLoanSubsidary;

                            lstProcessCase.Add(oHeader);
                        }
                    }

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Serializing the Process case Information ", sFuncName);
                    Context.Response.Output.Write(js.Serialize(lstProcessCase));
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Serializing the Process case Informations , the Serialized data is ' " + js.Serialize(lstProcessCase) + " '", sFuncName);

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
        public void SPA_ProcessCase_UpdateCaseTabDetails(string sJsonInput)
        {
            string sFuncName = string.Empty;
            string sTabId = string.Empty;
            try
            {
                sFuncName = "SPA_ProcessCase_UpdateCaseTabDetails()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                //sJsonInput = "[" + sJsonInput + "]";
                //Split JSON to Individual String
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<ProcessCase_GetDataFromOCRD> lstDeserialize = js.Deserialize<List<ProcessCase_GetDataFromOCRD>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    //JSON_AddIndividualSearch objLstInfo = lstDeserialize[0];
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Converting Json to Datatable ", sFuncName);
                    ProcessCase_Header objHeader = new ProcessCase_Header();
                    objHeader.Case = lstDeserialize[0].Case;
                    objHeader.CaseType = lstDeserialize[0].CaseType;
                    objHeader.CaseStatus = lstDeserialize[0].CaseStatus;
                    objHeader.FileOpenDate = lstDeserialize[0].FileOpenDate;
                    objHeader.CaseFileNo = lstDeserialize[0].CaseFileNo;
                    objHeader.KIV = lstDeserialize[0].KIV;
                    objHeader.TabId = lstDeserialize[0].TabId;
                    sTabId = lstDeserialize[0].TabId;

                    ProcessCase_Details objLstInfo = lstDeserialize[0].Details;
                    ProcessCase_Purchaser objLstInfo1 = lstDeserialize[0].Purchaser;
                    ProcessCase_Vendor objLstInfo2 = lstDeserialize[0].Vendor;
                    ProcessCase_Property objLstInfo3 = lstDeserialize[0].Property;
                    ProcessCase_LoanPrinciple objLstInfo4 = lstDeserialize[0].LoanPrinciple;
                    ProcessCase_LoanSubsidary objLstInfo5 = lstDeserialize[0].LoanSubsidary;
                    DataTable dtHeader = ObjectToData(objHeader);
                    DataTable dtDetails = ObjectToData(objLstInfo);
                    DataTable dtPurchaser = ObjectToData(objLstInfo1);
                    DataTable dtVendor = ObjectToData(objLstInfo2);
                    DataTable dtProperty = ObjectToData(objLstInfo3);
                    DataTable dtLoanPrinciple = ObjectToData(objLstInfo4);
                    DataTable dtLoanSubsidary = ObjectToData(objLstInfo5);

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Converting Json to Datatable , Calling the method SPA_ProcessCase_UpdateCaseTabDetails()", sFuncName);
                    string sResult = oProcessCase.SPA_ProcessCase_UpdateCaseTabDetails(sTabId, dtHeader, dtDetails, dtPurchaser, dtVendor, dtProperty, dtLoanPrinciple, dtLoanSubsidary);
                    if (sResult == "SUCCESS")
                    {
                        if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With SUCCESS  ", sFuncName);
                        result objResult = new result();
                        objResult.Result = "SUCCESS";
                        objResult.DisplayMessage = "Case Details Updated Successfully";
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
        public void SPA_ProcessCase_GetIDType()
        {
            string sFuncName = string.Empty;
            string sCardCode = string.Empty;
            string sCategory = string.Empty;
            string sUserName = string.Empty;
            try
            {
                sFuncName = "SPA_ProcessCase_GetIDType()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_ProcessCase_GetIDType() ", sFuncName);
                DataTable dt = oProcessCase.SPA_ProcessCase_GetIDType();
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_ProcessCase_GetIDType() ", sFuncName);
                List<ValidValues> lstValues = new List<ValidValues>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        ValidValues _Info = new ValidValues();

                        _Info.Id = r["Id"].ToString();
                        _Info.Name = r["Name"].ToString();
                        lstValues.Add(_Info);
                    }
                }
                else
                {
                    ValidValues _Info = new ValidValues();

                    _Info.Id = "-- Select --";
                    _Info.Name = "-- Select --";
                    lstValues.Add(_Info);
                }

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Serializing the ID Type Information ", sFuncName);
                Context.Response.Output.Write(js.Serialize(lstValues));
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Serializing the ID Type Information , the Serialized data is ' " + js.Serialize(lstValues) + " '", sFuncName);
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
        public void SPA_ProcessCase_CreateBilling(string sJsonInput)
        {
            string sFuncName = string.Empty;
            string sReturnResult = string.Empty;
            try
            {
                sFuncName = "SPA_ProcessCase_CreateBilling()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_ProcessCase_CreateBilling() ", sFuncName);
                sReturnResult = oProcessCase.SPA_ProcessCase_CreateBilling(sJsonInput, new DataTable());
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_ProcessCase_CreateBilling() ", sFuncName);
                if (sReturnResult == "SUCCESS")
                {
                    result objResult = new result();
                    objResult.Result = "Success";
                    objResult.DisplayMessage = "Create Billing is successfull in SAP";
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
        public void SPA_ProcessCase_GetNextSection(string sJsonInput)
        {
            string sFuncName = string.Empty;
            DataSet ds = new DataSet();
            string sReturnResult = string.Empty;
            try
            {
                sFuncName = "SPA_ProcessCase_GetNextSection()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);
                string sCaseNo = string.Empty;

                sJsonInput = "[" + sJsonInput + "]";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_NextSection> lstDeserialize = js.Deserialize<List<JSON_NextSection>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    JSON_NextSection objLstInfo = lstDeserialize[0];
                    List<GetNextSection> lstGetNextSection = new List<GetNextSection>();
                    sCaseNo = objLstInfo.sCaseNo;

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_ProcessCase_GetNextSection() ", sFuncName);
                    ds = oProcessCase.SPA_ProcessCase_GetNextSection(sCaseNo);
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_ProcessCase_GetNextSection() ", sFuncName);

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {

                                GetNextSection oGetNextSection = new GetNextSection();

                                oGetNextSection.ItemCode = dr["ItemCode"].ToString();
                                oGetNextSection.ItemName = dr["ItemName"].ToString();
                                oGetNextSection.INTNO = dr["INTNO"].ToString();
                                oGetNextSection.U_SORT_CODE = dr["U_SORT_CODE"].ToString();
                                oGetNextSection.CREATION_DATE = dr["CREATION_DATE"].ToString();
                                oGetNextSection.Qty = dr["Qty"].ToString();
                                oGetNextSection.price = dr["price"].ToString();
                                oGetNextSection.u_ACTION_BY = dr["u_ACTION_BY"].ToString();
                                oGetNextSection.LAST_UPDATE = dr["LAST_UPDATE"].ToString();
                                oGetNextSection.STATUS_DATE = dr["STATUS_DATE"].ToString();
                                oGetNextSection.TrnspName = dr["TrnspName"].ToString();
                                oGetNextSection.ForwardParty = dr["ForwardParty"].ToString();
                                oGetNextSection.ItmsGrpNam = dr["ItmsGrpNam"].ToString();
                                lstGetNextSection.Add(oGetNextSection);
                            }
                        }
                    }
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Serializing the Next section Information ", sFuncName);
                    Context.Response.Output.Write(js.Serialize(lstGetNextSection));
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Serializing the Next section Informations , the Serialized data is ' " + js.Serialize(lstGetNextSection) + " '", sFuncName);

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
        public void SPA_ProcessCase_GetOptionalItems(string sJsonInput)
        {
            string sFuncName = string.Empty;
            DataSet ds = new DataSet();
            string sReturnResult = string.Empty;
            try
            {
                sFuncName = "SPA_ProcessCase_GetOptionalItems()";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);
                string sCaseNo = string.Empty;

                sJsonInput = "[" + sJsonInput + "]";
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Getting the Json Input from Mobile  '" + sJsonInput + "'", sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Deserialize the Json Input ", sFuncName);
                List<JSON_NextSection> lstDeserialize = js.Deserialize<List<JSON_NextSection>>(sJsonInput);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Deserialize the Json Input ", sFuncName);
                if (lstDeserialize.Count > 0)
                {
                    JSON_NextSection objLstInfo = lstDeserialize[0];
                    List<GetNextSection> lstGetNextSection = new List<GetNextSection>();
                    sCaseNo = objLstInfo.sCaseNo;

                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before calling the Method SPA_ProcessCase_GetOptionalItems() ", sFuncName);
                    ds = oProcessCase.SPA_ProcessCase_GetOptionalItems(sCaseNo);
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After calling the Method SPA_ProcessCase_GetOptionalItems() ", sFuncName);

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {

                                GetNextSection oGetNextSection = new GetNextSection();

                                oGetNextSection.ItemCode = dr["ItemCode"].ToString();
                                oGetNextSection.ItemName = dr["ItemName"].ToString();
                                oGetNextSection.INTNO = dr["U_SORT_CODE"].ToString();
                                oGetNextSection.U_SORT_CODE = dr["U_SORT_CODE"].ToString();
                                oGetNextSection.CREATION_DATE = dr["CREATION_DATE"].ToString();
                                oGetNextSection.Qty = dr["Qty"].ToString();
                                oGetNextSection.price = dr["price"].ToString();
                                oGetNextSection.u_ACTION_BY = dr["u_ACTION_BY"].ToString();
                                oGetNextSection.LAST_UPDATE = dr["LAST_UPDATE"].ToString();
                                oGetNextSection.STATUS_DATE = dr["STATUS_DATE"].ToString();
                                oGetNextSection.TrnspName = dr["TrnspName"].ToString();
                                oGetNextSection.ForwardParty = dr["ForwardParty"].ToString();
                                oGetNextSection.ItmsGrpNam = dr["ItmsGrpNam"].ToString();
                                lstGetNextSection.Add(oGetNextSection);
                            }
                        }
                    }
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Before Serializing the Optional Items Information ", sFuncName);
                    Context.Response.Output.Write(js.Serialize(lstGetNextSection));
                    if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("After Serializing the Optional Items Informations , the Serialized data is ' " + js.Serialize(lstGetNextSection) + " '", sFuncName);
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
            public string QryGroup21 { get; set; }
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
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string FileName { get; set; }
            public string ICType { get; set; }
        }

        class ScanIC
        {
            public string Message { get; set; }
            public string ScanICLocation { get; set; }
            public string ScanFrontICLocation { get; set; }
            public string ScanBackICLocation { get; set; }
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

        class JSON_ProcessCase_GetDataFromOCRD
        {
            public string CaseNo { get; set; }
            public string UserName { get; set; }
            public string UserRole { get; set; }
        }

        class ProcessCase_GetDataFromOCRD
        {
            public string Case { get; set; }
            public string CaseType { get; set; }
            public string CaseStatus { get; set; }
            public string FileOpenDate { get; set; }
            public string CaseFileNo { get; set; }
            public string KIV { get; set; }
            public string TabId { get; set; }
            public ProcessCase_Details Details { get; set; }
            public ProcessCase_Purchaser Purchaser { get; set; }
            public ProcessCase_Vendor Vendor { get; set; }
            public ProcessCase_Property Property { get; set; }
            public ProcessCase_LoanPrinciple LoanPrinciple { get; set; }
            public ProcessCase_LoanSubsidary LoanSubsidary { get; set; }
        }

        class ProcessCase_Header
        {
            public string Case { get; set; }
            public string CaseType { get; set; }
            public string CaseStatus { get; set; }
            public string FileOpenDate { get; set; }
            public string CaseFileNo { get; set; }
            public string KIV { get; set; }
            public string TabId { get; set; }
        }

        class ProcessCase_Details
        {
            public string LA { get; set; }
            public string MANAGER { get; set; }
            public string InCharge { get; set; }
            public string CustomerService { get; set; }
            public string CaseType { get; set; }
            public string FileLocation { get; set; }
            public string FileClosedDate { get; set; }
            public string VendAcqDt { get; set; }
            public string CompanyBuisnessSearch { get; set; }
            public string BankWindingSearch { get; set; }
        }

        class ProcessCase_Purchaser
        {
            public string PurRepresentedByFirm { get; set; }
            public string PurlawyerRepresented { get; set; }
            public string PurSPADate { get; set; }
            public string PurEntryOfPrivateCaveat { get; set; }
            public string PurWithOfPrivateCaveat { get; set; }
            public string PurFirstName { get; set; }
            public string PurFirstID { get; set; }
            public string PurFirstTaxNo { get; set; }
            public string PurFirstContactNo { get; set; }
            public string PurFirstType { get; set; }
            public string PurSecName { get; set; }
            public string PurSecID { get; set; }
            public string PurSecTaxNo { get; set; }
            public string PurSecContactNo { get; set; }
            public string PurSecType { get; set; }
            public string PurThirdName { get; set; }
            public string PurThirdID { get; set; }
            public string PurThirdTaxNo { get; set; }
            public string PurThirdContactNo { get; set; }
            public string PurThirdType { get; set; }
            public string PurFourthName { get; set; }
            public string PurFourthID { get; set; }
            public string PurFourthTaxNo { get; set; }
            public string PurFourthContactNo { get; set; }
            public string PurFourthType { get; set; }
        }

        class ProcessCase_Vendor
        {
            public string VndrRepresentedByFirm { get; set; }
            public string VndrlawyerRepresented { get; set; }
            public string VndrReqDevConsent { get; set; }
            public string VndrReceiveDevConsent { get; set; }
            public string VndrFirstName { get; set; }
            public string VndrFirstID { get; set; }
            public string VndrFirstTaxNo { get; set; }
            public string VndrFirstContactNo { get; set; }
            public string VndrFirstType { get; set; }
            public string VndrSecName { get; set; }
            public string VndrSecID { get; set; }
            public string VndrSecTaxNo { get; set; }
            public string VndrSecContactNo { get; set; }
            public string VndrSecType { get; set; }
            public string VndrThirdName { get; set; }
            public string VndrThirdID { get; set; }
            public string VndrThirdTaxNo { get; set; }
            public string VndrThirdContactNo { get; set; }
            public string VndrThirdType { get; set; }
            public string VndrFourthName { get; set; }
            public string VndrFourthID { get; set; }
            public string VndrFourthTaxNo { get; set; }
            public string VndrFourthContactNo { get; set; }
            public string VndrFourthType { get; set; }
        }

        class ProcessCase_Property
        {
            public string TitleType { get; set; }
            public string CertifiedPlanNo { get; set; }
            public string LotNo { get; set; }
            public string PreviouslyKnownAs { get; set; }
            public string State { get; set; }
            public string Area { get; set; }
            public string BPM { get; set; }
            public string GovSurvyPlan { get; set; }
            public string LotArea { get; set; }
            public string Developer { get; set; }
            public string Project { get; set; }
            public string DevLicenseNo { get; set; }
            public string DevSolicitor { get; set; }
            public string DevSoliLoc { get; set; }
            public string TitleSearchDate { get; set; }
            public string DSCTransfer { get; set; }
            public string DRCTransfer { get; set; }
            public string FourteenADate { get; set; }
            public string DRTLRegistry { get; set; }
            public string PropertyCharged { get; set; }
            public string BankName { get; set; }
            public string Branch { get; set; }
            public string PAName { get; set; }
            public string PresentationNo { get; set; }
            public string ExistChargeRef { get; set; }
            public string ReceiptType { get; set; }
            public string ReceiptNo { get; set; }
            public string ReceiptDate { get; set; }
            public string PurchasePrice { get; set; }
            public string AdjValue { get; set; }
            public string VndrPrevSPAValue { get; set; }
            public string Deposit { get; set; }
            public string BalPurPrice { get; set; }
            public string LoanAmount { get; set; }
            public string LoanCaseNo { get; set; }
            public string DiffSum { get; set; }
            public string RedAmt { get; set; }
            public string RedDate { get; set; }
            public string DefRdmptSum { get; set; }
        }

        public class ProcessCase_LoanPrinciple
        {
            public string ReqRedStatement { get; set; }
            public string RedStmtDate { get; set; }
            public string RedPayDate { get; set; }
            public string RepByFirm { get; set; }
            public string LoanCaseNo { get; set; }
            public string Project { get; set; }
            public string MasterBankName { get; set; }
            public string BranchName { get; set; }
            public string Address { get; set; }
            public string PAName { get; set; }
            public string BankRef { get; set; }
            public string BankInsDate { get; set; }
            public string LOFDate { get; set; }
            public string BankSolicitor { get; set; }
            public string SoliLoc { get; set; }
            public string SoliRef { get; set; }
            public string TypeofLoan { get; set; }
            public string TypeofFacility { get; set; }
            public string FacilityAmt { get; set; }
            public string Repaymt { get; set; }
            public string IntrstRate { get; set; }
            public string MonthlyInstmt { get; set; }
            public string TermLoanAmt { get; set; }
            public string Interest { get; set; }
            public string ODLoan { get; set; }
            public string MRTA { get; set; }
            public string BankGuarantee { get; set; }
            public string LetterofCredit { get; set; }
            public string TrustReceipt { get; set; }
            public string Others { get; set; }
            public string LoanDet1 { get; set; }
            public string LoanDet2 { get; set; }
            public string LoanDet3 { get; set; }
            public string LoanDet4 { get; set; }
            public string LoanDet5 { get; set; }
        }

        public class ProcessCase_LoanSubsidary
        {
            public string LoanDocForBankExe { get; set; }
            public string FaciAgreeDate { get; set; }
            public string LoanDocRetFromBank { get; set; }
            public string DischargeofCharge { get; set; }
            public string FirstTypeofFacility { get; set; }
            public string FirstFacilityAmt { get; set; }
            public string FirstRepaymt { get; set; }
            public string FirstIntrstRate { get; set; }
            public string FirstMonthlyInstmt { get; set; }
            public string FirstTermLoanAmt { get; set; }
            public string FirstInterest { get; set; }
            public string FirstODLoan { get; set; }
            public string FirstMRTA { get; set; }
            public string FirstBankGuarantee { get; set; }
            public string FirstLetterofCredit { get; set; }
            public string FirstTrustReceipt { get; set; }
            public string FirstOthers { get; set; }

            public string SecTypeofFacility { get; set; }
            public string SecFacilityAmt { get; set; }
            public string SecRepaymt { get; set; }
            public string SecIntrstRate { get; set; }
            public string SecMonthlyInstmt { get; set; }
            public string SecTermLoanAmt { get; set; }
            public string SecInterest { get; set; }
            public string SecODLoan { get; set; }
            public string SecMRTA { get; set; }
            public string SecBankGuarantee { get; set; }
            public string SecLetterofCredit { get; set; }
            public string SecTrustReceipt { get; set; }
            public string SecOthers { get; set; }

            public string ThirdTypeofFacility { get; set; }
            public string ThirdFacilityAmt { get; set; }
            public string ThirdRepaymt { get; set; }
            public string ThirdIntrstRate { get; set; }
            public string ThirdMonthlyInstmt { get; set; }
            public string ThirdTermLoanAmt { get; set; }
            public string ThirdInterest { get; set; }
            public string ThirdODLoan { get; set; }
            public string ThirdMRTA { get; set; }
            public string ThirdBankGuarantee { get; set; }
            public string ThirdLetterofCredit { get; set; }
            public string ThirdTrustReceipt { get; set; }
            public string ThirdOthers { get; set; }

            public string FourthTypeofFacility { get; set; }
            public string FourthFacilityAmt { get; set; }
            public string FourthRepaymt { get; set; }
            public string FourthIntrstRate { get; set; }
            public string FourthMonthlyInstmt { get; set; }
            public string FourthTermLoanAmt { get; set; }
            public string FourthInterest { get; set; }
            public string FourthODLoan { get; set; }
            public string FourthMRTA { get; set; }
            public string FourthBankGuarantee { get; set; }
            public string FourthLetterofCredit { get; set; }
            public string FourthTrustReceipt { get; set; }
            public string FourthOthers { get; set; }

            public string FifthTypeofFacility { get; set; }
            public string FifthFacilityAmt { get; set; }
            public string FifthRepaymt { get; set; }
            public string FifthIntrstRate { get; set; }
            public string FifthMonthlyInstmt { get; set; }
            public string FifthTermLoanAmt { get; set; }
            public string FifthInterest { get; set; }
            public string FifthODLoan { get; set; }
            public string FifthMRTA { get; set; }
            public string FifthBankGuarantee { get; set; }
            public string FifthLetterofCredit { get; set; }
            public string FifthTrustReceipt { get; set; }
            public string FifthOthers { get; set; }
        }

        public class JSON_CaseEnquiry
        {
            public string FileOpenDateFrom { get; set; }
            public string FileOpenDateTo { get; set; }
            public string CaseFileNo { get; set; }
            public string CaseType { get; set; }
            public string ClientName { get; set; }
            public string AmountFrom { get; set; }
            public string AmountTo { get; set; }
        }

        public class JSON_OpenCase
        {
            public string sUserName { get; set; }
        }

        public class JSON_NextSection
        {
            public string sCaseNo { get; set; }
        }

        public class GetNextSection
        {
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string INTNO { get; set; }
            public string U_SORT_CODE { get; set; }
            public string CREATION_DATE { get; set; }
            public string Qty { get; set; }
            public string price { get; set; }
            public string u_ACTION_BY { get; set; }
            public string LAST_UPDATE { get; set; }
            public string STATUS_DATE { get; set; }
            public string TrnspName { get; set; }
            public string ForwardParty { get; set; }
            public string ItmsGrpNam { get; set; }
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
            rowNew["LOTAREA"] = objEditDetails.LOTAREA;
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
            tbProperty.Columns.Add("LOTAREA");
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

        public static DataTable ObjectToData(object o)
        {
            DataTable dt = new DataTable("OutputData");

            DataRow dr = dt.NewRow();
            dt.Rows.Add(dr);

            o.GetType().GetProperties().ToList().ForEach(f =>
            {
                try
                {
                    f.GetValue(o, null);
                    dt.Columns.Add(f.Name, f.PropertyType);
                    dt.Rows[0][f.Name] = f.GetValue(o, null);
                }
                catch { }
            });
            return dt;
        }

        public DataTable SPA_AddCase_ScanIC(string sDocName, string sItemCode, string sItemName, string sICType)
        {
            DataTable oDatatable = new DataTable();
            string sFuncName = string.Empty;
            string sProcName = string.Empty;
            DataView oDTView = new DataView();
            string sCode = string.Empty;
            try
            {
                sFuncName = "SPA_AddCase_ScanIC()";
                //sProcName = "AE_SPA013_Mobile_AddCase_CheckStatus";

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Starting Function ", sFuncName);

                // This following part of code is for converting and saving the Binary stream to PDF File.
                string strdocPath = null;
                string EncryptToPath = null;
                strdocPath = FileUploadPath + sDocName;
                EncryptToPath = FileUploadEncryptedPath + sDocName;

                byte[] bytes = SPA_ConvertUploadFiletoBinary(strdocPath);

                //Pass the byte array to gopi web service

                var client = new ReadDocument.Service1SoapClient();

                oDatatable = client.ReadDocument(sItemCode, bytes, sDocName);
                if (sICType == "Front")
                {
                    sCode = "100021";
                }
                else if (sICType == "Back")
                {
                    sCode = "100022";
                }
                DataSet ds = oCase.SPA_AddCase_ScanDocumentMapping_RELPARTY(sCode);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in oDatatable.Rows) // The data from Gopi webservices
                        {
                            foreach (DataRow InnerItem in ds.Tables[0].Rows) // This is from the Item Maintenance table
                            {
                                if (item[0].ToString().Trim() == InnerItem[1].ToString().Trim()) // if(Gopi webservice first column is equal to the Item Maintenance Second Column
                                {
                                    item[0] = InnerItem[0].ToString(); // Assign the Item Maintenance First column to the Gopi webservice first column
                                }
                            }
                        }
                    }
                }
                //oDatatable.Columns["Marks"].ColumnName = "SubjectMarks";

                // Convert the Uploaded File to Encrypted Format and Save it to Location
                clsEncypt.EncryptFile(strdocPath, EncryptToPath);

                // Delete the file stored in the temp folder from mobile app
                //File.Delete(strdocPath);

                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Attached file is saved Properly ", sFuncName);

            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw Ex;
            }
            return oDatatable;
        }

        public DataTable SPA_AddCase_SaveAttachment(string sDocName, string sItemCode, string sItemName, string sCardCode)
        {
            DataTable oDatatable = new DataTable();
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
                string EncryptToPath = null;
                strdocPath = FileUploadPath + sDocName;
                EncryptToPath = FileUploadEncryptedPath + sDocName;

                byte[] bytes = SPA_ConvertUploadFiletoBinary(strdocPath);

                //Pass the byte array to gopi web service

                var client = new ReadDocument.Service1SoapClient();

                oDatatable = client.ReadDocument(sItemCode, bytes, sDocName);

                DataSet ds = oCase.SPA_AddCase_ScanDocumentMapping_Property(sItemCode);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow item in oDatatable.Rows) // The data from Gopi webservices
                        {
                            foreach (DataRow InnerItem in ds.Tables[0].Rows) // This is from the Item Maintenance table
                            {
                                if (item[0].ToString().Trim() == InnerItem[1].ToString().Trim()) // if(Gopi webservice first column is equal to the Item Maintenance Second Column
                                {
                                    item[0] = InnerItem[0].ToString(); // Assign the Item Maintenance First column to the Gopi webservice first column
                                }
                            }
                        }
                    }
                }

                //Pass the byte array to gopi web service

                // Convert the Uploaded File to Encrypted Format and Save it to Location

                clsEncypt.EncryptFile(strdocPath, EncryptToPath);

                //byte[] Encryptedbytes = SPA_ConvertUploadFiletoBinary(strdocPath);

                //FileStream objfilestream = new FileStream(EncryptToPath, FileMode.Create, FileAccess.ReadWrite);
                //objfilestream.Write(Encryptedbytes, 0, Encryptedbytes.Length);
                //objfilestream.Close();

                //File.Delete(strdocPath);

                // Update the Encrypted Location in SAP



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


                //oDataset = new DataSet();

            }
            catch (Exception Ex)
            {
                sErrDesc = Ex.Message.ToString();
                oLog.WriteToErrorLogFile(sErrDesc, sFuncName);
                if (p_iDebugMode == DEBUG_ON) oLog.WriteToDebugLogFile("Completed With ERROR  ", sFuncName);
                throw Ex;
            }
            return oDatatable;
        }

        public byte[] SPA_ConvertUploadFiletoBinary(string sFilePath)
        {
            try
            {
                //string filename = @"E:\Documents\TiaNoordin\" + FileUpload1.FileName;
                //string filename = FileUpload1.FileName;
                byte[] bytes;
                Random rndm = new Random();
                using (FileStream file = new FileStream(sFilePath, FileMode.Open, FileAccess.Read))
                {
                    bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);
                }
                return bytes;
                //string sResult = bytes.ToString();
                //string result = System.Text.Encoding.UTF8.GetString(bytes);
                //SaveDocument_Boarding(bytes, rndm.Next().ToString() + ".pdf");
                //TextBox1.Text = "SUCCESS";
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        private DataTable GenerateTransposedTable(DataTable inputTable)
        {
            DataTable outputTable = new DataTable();

            // Add columns by looping rows

            // Header row's first column is same as in inputTable
            outputTable.Columns.Add(inputTable.Columns[0].ColumnName.ToString());

            // Header row's second column onwards, 'inputTable's first column taken
            foreach (DataRow inRow in inputTable.Rows)
            {
                string newColName = inRow[0].ToString();
                outputTable.Columns.Add(newColName);
            }

            // Add rows by looping columns        
            for (int rCount = 1; rCount <= inputTable.Columns.Count - 1; rCount++)
            {
                DataRow newRow = outputTable.NewRow();

                // First column is inputTable's Header row's second column
                newRow[0] = inputTable.Columns[rCount].ColumnName.ToString();
                for (int cCount = 0; cCount <= inputTable.Rows.Count - 1; cCount++)
                {
                    string colValue = inputTable.Rows[cCount][rCount].ToString();
                    newRow[cCount + 1] = colValue;
                }
                outputTable.Rows.Add(newRow);
            }

            return outputTable;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
namespace AE_TnN_Mobile_Common
{
    public class clsLog
    {
        public const Int16 RTN_SUCCESS = 1;
        public const Int16 RTN_ERROR = 0;
        public const Int16 DEBUG_ON = 1;
        public const Int16 DEBUG_OFF = 0;
        public Int16 p_iErrDispMethod;
        public Int16 p_iDeleteDebugLog = 0;
        public string p_sLogDir;
        public string path;
        private const Int16 MAXFILESIZE_IN_MB = 5;
        private const string LOG_FILE_ERROR = "ErrorLog";
        private const string LOG_FILE_ERROR_ARCH = "ErrorLog_";
        private const string LOG_FILE_DEBUG = "DebugLog";
        private const string LOG_FILE_DEBUG_ARCH = "DebugLog_";
        private const Int16 FILE_SIZE_CHECK_ENABLE = 1;
        private const Int16 FILE_SIZE_CHECK_DISABLE = 0;

        public static string logPath = ConfigurationManager.AppSettings["LogPath"].ToString();

        public long WriteToDebugLogFile(string strErrText, string strSourceName, Int16 intCheckFileForDelete = 1)
        {
            long functionReturnValue = 0;

            // **********************************************************************************
            //   Function   :    WriteToLogFile_Debug()
            //   Purpose    :    This function checks if given input file name exists or not
            //
            //   Parameters :    ByVal strErrText As String
            //                       strErrText = Text to be written to the log
            //                   ByVal intLogType As Integer
            //                       intLogType = Log type (1 - Log ; 2 - Error ; 0 - None)
            //                   ByVal strSourceName As String
            //                       strSourceName = Function name calling this function
            //                   Optional ByVal intCheckFileForDelete As Integer
            //                       intCheckFileForDelete = Flag to indicate if file size need to be checked before logging (0 - No check ; 1 - Check)
            //   Return     :    0 - FAILURE
            //                   1 - SUCCESS
            //   Author     :    JOHN
            //   Date       :    MAY 2013
            //   Changes    : 
            //                   
            // **********************************************************************************

            StreamWriter oStreamWriter = null;
            string strFileName = string.Empty;
            string strArchFileName = string.Empty;
            // string strTempString = string.Empty;
            string strTempString = string.Empty;

            double lngFileSizeInMB = 0;
            //int iFileCount = 0;


            try
            {

                if (strSourceName.Length > 30)
                    strTempString = strTempString.PadLeft(0);
                else
                    strTempString = strTempString.PadLeft(30 - strSourceName.Length);


                strSourceName = strTempString.ToString().Trim() + strSourceName.Trim();

                strErrText = "[" + string.Format(DateTime.Now.ToString(), "MM/dd/yyyy HH:mm:ss") + "]" + "[" + strSourceName + "] " + strErrText;

                //strFileName = p_sLogDir + "\\" + LOG_FILE_DEBUG + ".log";
                //strArchFileName = p_sLogDir + "\\" + LOG_FILE_DEBUG_ARCH + string.Format(DateTime.Now.ToString(), "yyMMddHHMMss") + ".log";

                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string Datapath = Uri.UnescapeDataString(uri.Path);
                path = Path.GetDirectoryName(Datapath);

                path = logPath;
                //strFileName = path + "\\" + LOG_FILE_DEBUG + "_" + DateTime.Now.Date.ToString().Replace("/","").Substring(0,8) + ".log";
                strFileName = path + "\\" + LOG_FILE_DEBUG + ".log";
                strArchFileName = path + "\\" + LOG_FILE_DEBUG_ARCH + string.Format(DateTime.Now.ToString(), "yyMMddHHMMss") + ".log";

                if (intCheckFileForDelete == FILE_SIZE_CHECK_ENABLE)
                {
                    if (File.Exists(strFileName))
                    {
                        FileInfo fi = new FileInfo(strFileName);

                        lngFileSizeInMB = (fi.Length / 1024) / 1024;

                        if (lngFileSizeInMB >= MAXFILESIZE_IN_MB)
                        {
                            //If intCheckDeleteDebugLog=1 then remove all debug_log file
                            if (p_iDeleteDebugLog == 1)
                            {
                                foreach (string sFileName in Directory.GetFiles(System.IO.Directory.GetCurrentDirectory(), LOG_FILE_DEBUG_ARCH + "*"))
                                {
                                    File.Delete(sFileName);
                                }
                            }
                            File.Move(strFileName, strArchFileName);
                        }
                    }
                }
                oStreamWriter = File.AppendText(strFileName);
                oStreamWriter.WriteLine(strErrText);
                functionReturnValue = RTN_SUCCESS;
            }
            catch (Exception exc)
            {
                string s = exc.ToString();
                functionReturnValue = RTN_ERROR;
            }
            finally
            {
                if ((oStreamWriter != null))
                {
                    oStreamWriter.Flush();
                    oStreamWriter.Close();
                    oStreamWriter = null;
                }
            }
            return functionReturnValue;

        }

        public long WriteToErrorLogFile(string strErrText, string strSourceName, Int16 intCheckFileForDelete = 1)
        {
            long functionReturnValue = 0;

            // **********************************************************************************
            //   Function   :    WriteToLogFile()
            //   Purpose    :    This function checks if given input file name exists or not
            //
            //   Parameters :    ByVal strErrText As String
            //                       strErrText = Text to be written to the log
            //                   ByVal intLogType As Integer
            //                       intLogType = Log type (1 - Log ; 2 - Error ; 0 - None)
            //                   ByVal strSourceName As String
            //                       strSourceName = Function name calling this function
            //                   Optional ByVal intCheckFileForDelete As Integer
            //                       intCheckFileForDelete = Flag to indicate if file size need to be checked before logging (0 - No check ; 1 - Check)
            //   Return     :    0 - FAILURE
            //                   1 - SUCCESS
            //   Author     :    JOHN
            //   Date       :    MAY 2014
            // **********************************************************************************

            StreamWriter oStreamWriter = null;
            string strFileName = string.Empty;
            string strArchFileName = string.Empty;
            // string strTempString = string.Empty;
            double lngFileSizeInMB = 0;
            string strTempString = string.Empty;

            try
            {
                if (strSourceName.Length > 30)
                    strTempString = strTempString.PadLeft(0);
                else
                    strTempString = strTempString.PadLeft(30 - strSourceName.Length);


                strSourceName = strTempString.ToString() + strSourceName;

                strErrText = "[" + string.Format(DateTime.Now.ToString(), "MM/dd/yyyy HH:mm:ss") + "]" + "[" + strSourceName + "] " + strErrText;

                //strFileName = p_sLogDir + "\\" + LOG_FILE_DEBUG + ".log";
                //strArchFileName = p_sLogDir + "\\" + LOG_FILE_DEBUG_ARCH + string.Format(DateTime.Now.ToString(), "yyMMddHHMMss") + ".log";

                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string Datapath = Uri.UnescapeDataString(uri.Path);
                path = Path.GetDirectoryName(Datapath);
                path = logPath;
                //strFileName = path + "\\" + LOG_FILE_ERROR + "_" + DateTime.Now.Date.ToString().Replace("/", "").Substring(0, 8) + ".log";
                strFileName = path + "\\" + LOG_FILE_ERROR + ".log";
                strArchFileName = path + "\\" + LOG_FILE_ERROR_ARCH + string.Format(DateTime.Now.ToString(), "yyMMddHHMMss") + ".log";

                //strFileName = System.IO.Directory.GetCurrentDirectory() + "\\" + LOG_FILE_DEBUG + ".log";
                //strArchFileName = System.IO.Directory.GetCurrentDirectory() + "\\" + LOG_FILE_DEBUG_ARCH + string.Format(DateTime.Now.ToString(), "yyMMddHHMMss") + ".log";

                if (intCheckFileForDelete == FILE_SIZE_CHECK_ENABLE)
                {
                    if (File.Exists(strFileName))
                    {
                        FileInfo fi = new FileInfo(strFileName);

                        lngFileSizeInMB = (fi.Length / 1024) / 1024;

                        if (lngFileSizeInMB >= MAXFILESIZE_IN_MB)
                        {
                            //If intCheckDeleteDebugLog=1 then remove all debug_log file
                            if (p_iDeleteDebugLog == 1)
                            {
                                foreach (string sFileName in Directory.GetFiles(System.IO.Directory.GetCurrentDirectory(), LOG_FILE_ERROR_ARCH + "*"))
                                {
                                    File.Delete(sFileName);
                                }
                            }
                            File.Move(strFileName, strArchFileName);
                        }
                    }
                }
                oStreamWriter = File.AppendText(strFileName);
                oStreamWriter.WriteLine(strErrText);
                functionReturnValue = RTN_SUCCESS;
            }
            catch (Exception exc)
            {
                string s = exc.ToString();
                functionReturnValue = RTN_ERROR;
            }
            finally
            {
                if ((oStreamWriter != null))
                {
                    oStreamWriter.Flush();
                    oStreamWriter.Close();
                    oStreamWriter = null;
                }
            }
            return functionReturnValue;

        }
    }
}

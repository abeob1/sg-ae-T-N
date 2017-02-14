Option Explicit On
Imports System.Xml
Imports System.IO
Imports System.Windows.Forms
Imports System.Globalization
Imports System.Net.Mail
Imports System.Configuration

Module modCommon

    Public Function ConnectDICompSSO(ByRef objCompany As SAPbobsCOM.Company, ByRef sErrDesc As String) As Long
        ' ***********************************************************************************
        '   Function   :    ConnectDICompSSO()
        '   Purpose    :    Connect To DI Company Object
        '
        '   Parameters :    ByRef objCompany As SAPbobsCOM.Company
        '                       objCompany = set the SAP Company Object
        '                   ByRef sErrDesc As String
        '                       sErrDesc = Error Description to be returned to calling function
        '   Return     :    0 - FAILURE
        '                   1 - SUCCESS
        '   Author     :    Sri
        '   Date       :    29 April 2013
        '   Change     :
        ' ***********************************************************************************
        Dim sCookie As String = String.Empty
        Dim sConnStr As String = String.Empty
        Dim sFuncName As String = String.Empty
        Dim lRetval As Long
        Dim iErrCode As Int32
        Try
            sFuncName = "ConnectDICompSSO()"
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Starting function", sFuncName)

            objCompany = New SAPbobsCOM.Company

            sCookie = objCompany.GetContextCookie
            sConnStr = p_oUICompany.GetConnectionContext(sCookie)
            'sConnStr = p_oSBOApplication.Company.GetConnectionContext(sCookie)
            lRetval = objCompany.SetSboLoginContext(sConnStr)

            If Not lRetval = 0 Then
                Throw New ArgumentException("SetSboLoginContext of Single SignOn Failed.")
            End If
            p_oSBOApplication.StatusBar.SetText("Please Wait While Company Connecting... ", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
            lRetval = objCompany.Connect
            If lRetval <> 0 Then
                objCompany.GetLastError(iErrCode, sErrDesc)
                Throw New ArgumentException("Connect of Single SignOn failed : " & sErrDesc)
            Else
                p_oSBOApplication.StatusBar.SetText("Company Connection Has Established with the " & objCompany.CompanyName, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)

            End If
            ConnectDICompSSO = RTN_SUCCESS
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Completed with SUCCESS", sFuncName)
        Catch exc As Exception
            sErrDesc = exc.Message
            Call WriteToLogFile(sErrDesc, sFuncName)
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Completed with ERROR", sFuncName)
            ConnectDICompSSO = RTN_ERROR
        End Try
    End Function

    Public Sub ShowErr(ByVal sErrMsg As String)
        ' ***********************************************************************************
        '   Function   :    ShowErr()
        '   Purpose    :    Show Error Message
        '   Parameters :  
        '                   ByVal sErrDesc As String
        '                       sErrDesc = Error Description to be returned to calling function
        '   Return     :    0 - FAILURE
        '                   1 - SUCCESS
        '   Author     :    Dev
        '   Date       :    23 Jan 2007
        '   Change     :
        ' ***********************************************************************************
        Try
            If sErrMsg <> "" Then
                If Not p_oSBOApplication Is Nothing Then
                    If p_iErrDispMethod = ERR_DISPLAY_STATUS Then

                        p_oSBOApplication.SetStatusBarMessage("Error : " & sErrMsg, SAPbouiCOM.BoMessageTime.bmt_Short)
                    ElseIf p_iErrDispMethod = ERR_DISPLAY_DIALOGUE Then
                        p_oSBOApplication.MessageBox("Error : " & sErrMsg)
                    End If
                End If
            End If
        Catch exc As Exception
            WriteToLogFile(exc.Message, "ShowErr()")
        End Try
    End Sub

    Public Sub LoadFromXML(ByVal FileName As String, ByVal Sbo_application As SAPbouiCOM.Application)
        Try
            Dim oXmlDoc As New Xml.XmlDocument
            Dim sPath As String
            ''sPath = IO.Directory.GetParent(Application.StartupPath).ToString
            sPath = Application.StartupPath.ToString
            'oXmlDoc.Load(sPath & "\AE_FleetMangement\" & FileName)
            oXmlDoc.Load(sPath & "\" & FileName)
            ' MsgBox(Application.StartupPath)

            Sbo_application.LoadBatchActions(oXmlDoc.InnerXml)
        Catch ex As Exception
            MsgBox(ex)
        End Try

    End Sub

    Public Function Udoform(ByVal n As String) As Integer 'function to load Udt forms
        Dim oMenu As SAPbouiCOM.MenuItem
        Dim i As Integer
        oMenu = p_oSBOApplication.Menus.Item("43535")
        For i = 0 To oMenu.SubMenus.Count - 1
            If oMenu.SubMenus.Item(i).String = n Then
                Return oMenu.SubMenus.Item(i).UID
                Exit For
            End If
        Next
    End Function

End Module




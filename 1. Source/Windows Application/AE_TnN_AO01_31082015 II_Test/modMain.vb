Imports SAPbouiCOM.Framework

Module modMain

    Public p_oApps As SAPbouiCOM.SboGuiApi
    Public p_oEventHandler As clsEventHandler
    Public WithEvents p_oSBOApplication As SAPbouiCOM.Application
    Public p_oDICompany As SAPbobsCOM.Company
    Public p_oUICompany As SAPbouiCOM.Company
    Public sFuncName As String
    Public sErrDesc As String


    Public p_iDebugMode As Int16
    Public p_iErrDispMethod As Int16
    Public p_iDeleteDebugLog As Int16

    Public p_sSQLName As String = String.Empty
    Public p_sSQLPass As String = String.Empty

    Public Const RTN_SUCCESS As Int16 = 1
    Public Const RTN_ERROR As Int16 = 0

    Public Const DEBUG_ON As Int16 = 1
    Public Const DEBUG_OFF As Int16 = 0

    Public Const ERR_DISPLAY_STATUS As Int16 = 1
    Public Const ERR_DISPLAY_DIALOGUE As Int16 = 2
    Public format1 As New System.Globalization.CultureInfo("fr-FR", True)
    Public oDT_TxtFileGeneration As DataTable
    Public dtTable As New DataTable

    Public p_sSelectedFilepath As String = String.Empty
    Public p_bBPcus As Boolean = False






    <STAThread()>
    Sub Main(ByVal args() As String)

        ''Dim oApp As Application
        Dim sconn As String = String.Empty
        ''If (args.Length < 1) Then
        ''    oApp = New Application
        ''Else
        ''    oApp = New Application(args(0))
        ''End If

        sFuncName = "Main()"
        Try
            p_iDebugMode = DEBUG_ON
            p_iErrDispMethod = ERR_DISPLAY_STATUS

            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Starting Addon startup function", sFuncName)
            p_oApps = New SAPbouiCOM.SboGuiApi
            'sconn = "0030002C0030002C00530041005000420044005F00440061007400650076002C0050004C006F006D0056004900490056"
            'p_oApps.Connect(args(0))
            p_oApps.Connect(args(0))

            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Initializing public SBO Application object", sFuncName)
            p_oSBOApplication = p_oApps.GetApplication

            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Retriving SBO application company handle", sFuncName)
            p_oUICompany = p_oSBOApplication.Company


            p_oDICompany = New SAPbobsCOM.Company
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Retrived SBO application company handle", sFuncName)
            If Not p_oDICompany.Connected Then
                If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Calling ConnectDICompSSO()", sFuncName)
                If ConnectDICompSSO(p_oDICompany, sErrDesc) <> RTN_SUCCESS Then Throw New ArgumentException(sErrDesc)
            End If

            'Call WriteToLogFile_Debug("Calling DisplayStatus()", sFuncName)
            'Call DisplayStatus(Nothing, "Addon starting.....please wait....", sErrDesc)

            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Creating Event handler class", sFuncName)
            p_oEventHandler = New clsEventHandler(p_oSBOApplication, p_oDICompany)

            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Calling AddMenuItems()", sFuncName)
            p_oEventHandler.AddMenuItems()


            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Calling SetApplication Function", sFuncName)
            ' Call p_oEventHandler.SetApplication(sErrDesc)

            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Addon started successfully", "Main()")

            p_oSBOApplication.StatusBar.SetText("Addon Started Successfully", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
            System.Windows.Forms.Application.Run()

        Catch ex As Exception
            sErrDesc = ex.Message
            Call WriteToLogFile(sErrDesc, sFuncName)
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Completed with ERROR", sFuncName)
        End Try


    End Sub

End Module

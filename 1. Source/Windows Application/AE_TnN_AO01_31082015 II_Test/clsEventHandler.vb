Option Explicit On
Imports SAPbouiCOM.Framework
Imports System.Windows.Forms

Public Class clsEventHandler
    Dim WithEvents SBO_Application As SAPbouiCOM.Application ' holds connection with SBO
    Dim p_oDICompany As New SAPbobsCOM.Company

    Public Sub New(ByRef oApplication As SAPbouiCOM.Application, ByRef oCompany As SAPbobsCOM.Company)
        Dim sFuncName As String = String.Empty
        Try
            sFuncName = "Class_Initialize()"
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Starting function", sFuncName)

            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Retriving SBO Application handle", sFuncName)
            SBO_Application = oApplication
            p_oDICompany = oCompany

            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Completed with SUCCESS", sFuncName)
        Catch exc As Exception
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Completed with ERROR", sFuncName)
            Call WriteToLogFile(exc.Message, sFuncName)
        End Try
    End Sub

    Public Function SetApplication(ByRef sErrDesc As String) As Long
        ' **********************************************************************************
        '   Function   :    SetApplication()
        '   Purpose    :    This function will be calling to initialize the default settings
        '                   such as Retrieving the Company Default settings, Creating Menus, and
        '                   Initialize the Event Filters
        '               
        '   Parameters :    ByRef sErrDesc AS string
        '                       sErrDesc = Error Description to be returned to calling function
        '   Return     :    0 - FAILURE
        '                   1 - SUCCESS
        ' **********************************************************************************
        Dim sFuncName As String = String.Empty

        Try
            sFuncName = "SetApplication()"
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Starting Function", sFuncName)

            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Calling SetMenus()", sFuncName)
            If SetMenus(sErrDesc) <> RTN_SUCCESS Then Throw New ArgumentException(sErrDesc)

            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Calling SetFilters()", sFuncName)
            If SetFilters(sErrDesc) <> RTN_SUCCESS Then Throw New ArgumentException(sErrDesc)

            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Completed with SUCCESS", sFuncName)
            SetApplication = RTN_SUCCESS
        Catch exc As Exception
            sErrDesc = exc.Message
            Call WriteToLogFile(exc.Message, sFuncName)
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Completed with ERROR", sFuncName)
            SetApplication = RTN_ERROR
        End Try
    End Function

    Private Function SetMenus(ByRef sErrDesc As String) As Long
        ' **********************************************************************************
        '   Function   :    SetMenus()
        '   Purpose    :    This function will be gathering to create the customized menu
        '               
        '   Parameters :    ByRef sErrDesc AS string
        '                       sErrDesc = Error Description to be returned to calling function
        '   Return     :    0 - FAILURE
        '                   1 - SUCCESS
        ' **********************************************************************************
        Dim sFuncName As String = String.Empty
        ' Dim oMenuItem As SAPbouiCOM.MenuItem
        Try
            sFuncName = "SetMenus()"
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Starting Function", sFuncName)

            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Completed with SUCCESS", sFuncName)
            SetMenus = RTN_SUCCESS
        Catch ex As Exception
            sErrDesc = ex.Message
            Call WriteToLogFile(ex.Message, sFuncName)
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Completed with ERROR", sFuncName)
            SetMenus = RTN_ERROR
        End Try
    End Function

    Private Function SetFilters(ByRef sErrDesc As String) As Long

        ' **********************************************************************************
        '   Function   :    SetFilters()
        '   Purpose    :    This function will be gathering to declare the event filter 
        '                   before starting the AddOn Application
        '               
        '   Parameters :    ByRef sErrDesc AS string
        '                       sErrDesc = Error Description to be returned to calling function
        '   Return     :    0 - FAILURE
        '                   1 - SUCCESS
        ' **********************************************************************************

        Dim oFilters As SAPbouiCOM.EventFilters
        Dim oFilter As SAPbouiCOM.EventFilter
        Dim sFuncName As String = String.Empty

        Try
            sFuncName = "SetFilters()"
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Starting function", sFuncName)

            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Initializing EventFilters object", sFuncName)
            oFilters = New SAPbouiCOM.EventFilters

            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_LOAD)
            oFilter.AddEx("134")

            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST)
            oFilter.AddEx("134")

            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_COMBO_SELECT)
            oFilter.AddEx("134")
            oFilter.AddEx("RELPTY")

            oFilter = oFilters.Add(SAPbouiCOM.BoEventTypes.et_KEY_DOWN)
            oFilter.AddEx("134")

            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Adding filters", sFuncName)
            SBO_Application.SetFilter(oFilters)

            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Completed with SUCCESS", sFuncName)
            SetFilters = RTN_SUCCESS
        Catch exc As Exception
            sErrDesc = exc.Message
            Call WriteToLogFile(sErrDesc, sFuncName)
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Completed with ERROR", sFuncName)
            SetFilters = RTN_ERROR
        End Try
    End Function

    Private Sub SBO_Application_AppEvent(ByVal EventType As SAPbouiCOM.BoAppEventTypes) Handles SBO_Application.AppEvent
        ' **********************************************************************************
        '   Function   :    SBO_Application_AppEvent()
        '   Purpose    :    This function will be handling the SAP Application Event
        '               
        '   Parameters :    ByVal EventType As SAPbouiCOM.BoAppEventTypes
        '                       EventType = set the SAP UI Application Eveny Object        
        ' **********************************************************************************
        Dim sFuncName As String = String.Empty
        Dim sErrDesc As String = String.Empty
        Dim sMessage As String = String.Empty

        Try
            sFuncName = "SBO_Application_AppEvent()"
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Starting function", sFuncName)

            Select Case EventType
                Case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged, SAPbouiCOM.BoAppEventTypes.aet_ShutDown, SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition
                    sMessage = String.Format("Please wait for a while to disconnect the AddOn {0} ....", System.Windows.Forms.Application.ProductName)
                    p_oSBOApplication.SetStatusBarMessage(sMessage, SAPbouiCOM.BoMessageTime.bmt_Medium, False)
                    End
            End Select

            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Completed with SUCCESS", sFuncName)
        Catch ex As Exception
            sErrDesc = ex.Message
            WriteToLogFile(sErrDesc, sFuncName)
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Completed with ERROR", sFuncName)
            ShowErr(sErrDesc)
        Finally
            GC.Collect()  'Forces garbage collection of all generations.
        End Try
    End Sub

    Private Sub SBO_Application_MenuEvent(ByRef pVal As SAPbouiCOM.MenuEvent, ByRef BubbleEvent As Boolean) Handles SBO_Application.MenuEvent
        ' **********************************************************************************
        '   Function   :    SBO_Application_MenuEvent()
        '   Purpose    :    This function will be handling the SAP Menu Event
        '               
        '   Parameters :    ByRef pVal As SAPbouiCOM.MenuEvent
        '                       pVal = set the SAP UI MenuEvent Object
        '                   ByRef BubbleEvent As Boolean
        '                       BubbleEvent = set the True/False        
        ' **********************************************************************************
        ' Dim oForm As SAPbouiCOM.Form = Nothing
        Dim sErrDesc As String = String.Empty
        Dim sFuncName As String = String.Empty
        Dim oForm As SAPbouiCOM.Form = Nothing
        Try
            sFuncName = "SBO_Application_MenuEvent()"
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Starting function", sFuncName)

            If Not p_oDICompany.Connected Then
                If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Calling ConnectDICompSSO()", sFuncName)
                If ConnectDICompSSO(p_oDICompany, sErrDesc) <> RTN_SUCCESS Then Throw New ArgumentException(sErrDesc)
            End If

            If pVal.BeforeAction = False Then
                Select Case pVal.MenuUID
                    Case "AE_VM"
                        BP_SBO_MenuEvent(pVal, BubbleEvent)
                    Case "AE_CM"
                        BP_SBO_MenuEvent(pVal, BubbleEvent)
                    Case "AE_RP"
                        RP_SBO_MenuEvent(pVal, BubbleEvent)
                    Case "1282"
                        If p_oSBOApplication.Forms.ActiveForm.TypeEx = "134" Then
                            BP_SBO_MenuEvent(pVal, BubbleEvent)
                        End If
                    Case "1288", "1289", "1290", "1291"
                        If p_oSBOApplication.Forms.ActiveForm.TypeEx = "134" Then
                            BP_SBO_MenuEvent(pVal, BubbleEvent)
                        End If
                End Select
            End If

            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Completed with SUCCESS", sFuncName)
        Catch exc As Exception
            BubbleEvent = False
            ShowErr(exc.Message)
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Completed with ERROR", sFuncName)
            WriteToLogFile(Err.Description, sFuncName)
        End Try
    End Sub

    Private Sub SBO_Application_ItemEvent(ByVal FormUID As String, ByRef pVal As SAPbouiCOM.ItemEvent, _
            ByRef BubbleEvent As Boolean) Handles SBO_Application.ItemEvent
        ' **********************************************************************************
        '   Function   :    SBO_Application_ItemEvent()
        '   Purpose    :    This function will be handling the SAP Menu Event
        '               
        '   Parameters :    ByVal FormUID As String
        '                       FormUID = set the FormUID
        '                   ByRef pVal As SAPbouiCOM.ItemEvent
        '                       pVal = set the SAP UI ItemEvent Object
        '                   ByRef BubbleEvent As Boolean
        '                       BubbleEvent = set the True/False        
        ' **********************************************************************************

        Dim sErrDesc As String = String.Empty
        Dim sFuncName As String = String.Empty
        Dim p_oDVJE As DataView = Nothing
        Dim oDTDistinct As DataTable = Nothing
        Dim oDTRowFilter As DataTable = Nothing
        Dim oForm As SAPbouiCOM.Form = Nothing

        Try
            sFuncName = "SBO_Application_ItemEvent()"
            ' If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Starting function", sFuncName)

            If Not IsNothing(p_oDICompany) Then
                If Not p_oDICompany.Connected Then
                    If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Calling ConnectDICompSSO()", sFuncName)
                    If ConnectDICompSSO(p_oDICompany, sErrDesc) <> RTN_SUCCESS Then Throw New ArgumentException(sErrDesc)
                End If
            End If

            BubbleEvent = True
            Select Case pVal.FormTypeEx
                Case "134"
                    If Not (pVal.EventType = SAPbouiCOM.BoEventTypes.et_FORM_UNLOAD Or pVal.EventType = SAPbouiCOM.BoEventTypes.et_FORM_CLOSE) Then
                        oForm = p_oSBOApplication.Forms.GetForm(pVal.FormType, pVal.FormTypeCount)
                        BP_SBO_ItemEvent(FormUID, pVal, BubbleEvent, oForm)
                        Exit Sub
                    End If
                Case "RELPTY"
                    If Not (pVal.EventType = SAPbouiCOM.BoEventTypes.et_FORM_UNLOAD Or pVal.EventType = SAPbouiCOM.BoEventTypes.et_FORM_CLOSE) Then
                        oForm = p_oSBOApplication.Forms.GetForm(pVal.FormType, pVal.FormTypeCount)
                        RP_SBO_ItemEvent(FormUID, pVal, BubbleEvent, oForm)
                        Exit Sub
                    End If
            End Select

            'If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Completed with SUCCESS", sFuncName)
        Catch exc As Exception
            BubbleEvent = False
            sErrDesc = exc.Message
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Completed with ERROR", sFuncName)
            WriteToLogFile(Err.Description, sFuncName)
            ShowErr(sErrDesc)
        End Try

    End Sub

    ''Sub AddMenuItems()
    ''    Dim oMenus As SAPbouiCOM.Menus
    ''    Dim oMenuItem As SAPbouiCOM.MenuItem
    ''    oMenus = SBO_Application.Menus

    ''    Dim oCreationPackage As SAPbouiCOM.MenuCreationParams
    ''    oCreationPackage = (SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams))
    ''    oMenuItem = SBO_Application.Menus.Item("43520") 'Modules

    ''    oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP
    ''    oCreationPackage.UniqueID = "PWC"
    ''    oCreationPackage.String = "Customization"
    ''    oCreationPackage.Enabled = True
    ''    oCreationPackage.Position = -1

    ''    oCreationPackage.Image = System.Windows.Forms.Application.StartupPath & "\Logo.bmp"
    ''    oMenus = oMenuItem.SubMenus

    ''    Try
    ''        'If the manu already exists this code will fail
    ''        If Not p_oSBOApplication.Menus.Exists("PWC") Then
    ''            oMenus.AddEx(oCreationPackage)
    ''        End If

    ''    Catch
    ''    End Try

    ''    Try
    ''        'Get the menu collection of the newly added pop-up item
    ''        oMenuItem = SBO_Application.Menus.Item("PWC")
    ''        oMenus = oMenuItem.SubMenus

    ''        'Create s sub menu
    ''        oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING
    ''        oCreationPackage.UniqueID = "FGTF"
    ''        oCreationPackage.String = "Generate Text File"

    ''        If Not p_oSBOApplication.Menus.Exists("FGTF") Then
    ''            oMenus.AddEx(oCreationPackage)
    ''        End If

    ''    Catch
    ''        'Menu already exists
    ''        SBO_Application.SetStatusBarMessage("Menu Already Exists", SAPbouiCOM.BoMessageTime.bmt_Short, False)
    ''    End Try
    ''End Sub

    Sub AddMenuItems()

        If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Calling LoadFromXML()", sFuncName)
        LoadFromXML("TnNMenu.xml", SBO_Application)

        ''Dim oMenus As SAPbouiCOM.Menus
        ''Dim oMenuItem As SAPbouiCOM.MenuItem
        ''oMenus = SBO_Application.Menus

        ''Dim oCreationPackage As SAPbouiCOM.MenuCreationParams
        ''oCreationPackage = (SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams))
        ''oMenuItem = SBO_Application.Menus.Item("43520") 'Modules

        ''oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP
        ''oCreationPackage.UniqueID = "PWC"
        ''oCreationPackage.String = "Customization"
        ''oCreationPackage.Enabled = True
        ''oCreationPackage.Position = -1

        ''oCreationPackage.Image = System.Windows.Forms.Application.StartupPath & "\Logo.bmp"
        ''oMenus = oMenuItem.SubMenus

        ''Try
        ''    'If the manu already exists this code will fail
        ''    If Not p_oSBOApplication.Menus.Exists("PWC") Then
        ''        oMenus.AddEx(oCreationPackage)
        ''    End If

        ''Catch
        ''End Try

        ''Try
        ''    'Get the menu collection of the newly added pop-up item
        ''    oMenuItem = SBO_Application.Menus.Item("PWC")
        ''    oMenus = oMenuItem.SubMenus

        ''    'Create s sub menu
        ''    oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING
        ''    oCreationPackage.UniqueID = "FGTF"
        ''    oCreationPackage.String = "Generate Text File"

        ''    If Not p_oSBOApplication.Menus.Exists("FGTF") Then
        ''        oMenus.AddEx(oCreationPackage)
        ''    End If

        ''Catch
        ''    'Menu already exists
        ''    SBO_Application.SetStatusBarMessage("Menu Already Exists", SAPbouiCOM.BoMessageTime.bmt_Short, False)
        ''End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

End Class




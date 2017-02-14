Module modRelatedParty

    Private oEdit As SAPbouiCOM.EditText
    Private oComboBox As SAPbouiCOM.ComboBox
    Private oMatrix As SAPbouiCOM.Matrix

    Private Sub InitializeRPForm(ByVal objForm As SAPbouiCOM.Form)
        Dim sFuncName As String = "InitializeForm"
        Dim sErrDesc As String = String.Empty
        Try
            objForm.Freeze(True)
            objForm.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE

            AutoGenCode(objForm)

            oMatrix = objForm.Items.Item("72").Specific
            oMatrix.AddRow(1)

            oComboBox = objForm.Items.Item("40").Specific
            oComboBox.Select(0, SAPbouiCOM.BoSearchKey.psk_Index)

            objForm.Freeze(False)
            objForm.Update()
        Catch ex As Exception
            sErrDesc = ex.Message
            Call WriteToLogFile(sErrDesc, sFuncName)
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Completed with ERROR", sFuncName)
            Throw New ArgumentException(sErrDesc)
        End Try
    End Sub

    Private Sub AutoGenCode(objForm As SAPbouiCOM.Form)
        Dim sFuncName As String = "AutoGenCode"
        Dim sErrDesc As String = String.Empty
        Dim sSQL As String = String.Empty
        Dim oRecSet As SAPbobsCOM.Recordset = Nothing
        Try
            sSQL = "SELECT REPLICATE('0', (12-LEN(ISNULL(MAX(SUBSTRING(Code,4,LEN(Code)))+1,1)))) + CONVERT(VARCHAR, ISNULL(MAX(SUBSTRING(Code,4,LEN(Code)))+1,1)) [Code] FROM [@AE_RELATEDPARTY]"
            oRecSet = p_oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecSet.DoQuery(sSQL)
            If oRecSet.RecordCount > 0 Then
                oEdit = objForm.Items.Item("4").Specific
                oEdit.Value = oRecSet.Fields.Item("Code").Value
            End If
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecSet)
            objForm.Items.Item("6").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
            objForm.Items.Item("4").Enabled = False
        Catch ex As Exception
            sErrDesc = ex.Message
            Call WriteToLogFile(sErrDesc, sFuncName)
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Completed with ERROR", sFuncName)
            Throw New ArgumentException(sErrDesc)
        End Try
    End Sub

    Public Sub RP_SBO_ItemEvent(ByVal FormUID As String, ByVal pval As SAPbouiCOM.ItemEvent, ByRef BubbleEvent As Boolean, ByVal objForm As SAPbouiCOM.Form)
        Dim sFuncName As String = "RP_SBO_ItemEvent"
        Dim sErrDesc As String = String.Empty
        Try
            If pval.Before_Action = True Then
                Select Case pval.EventType

                End Select
            Else
                Select Case pval.EventType
                    Case SAPbouiCOM.BoEventTypes.et_COMBO_SELECT
                        objForm = p_oSBOApplication.Forms.GetForm(pval.FormTypeEx, pval.FormTypeCount)
                        If pval.ItemUID = "40" Then
                            oComboBox = objForm.Items.Item("40").Specific
                            If oComboBox.Selected.Value = "CORPORATE" Then
                                objForm.Items.Item("72").Enabled = True
                            Else
                                objForm.Items.Item("72").Enabled = False
                            End If
                        End If

                End Select
            End If
        Catch ex As Exception
            sErrDesc = ex.Message
            Call WriteToLogFile(sErrDesc, sFuncName)
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Completed with ERROR", sFuncName)
            Throw New ArgumentException(sErrDesc)
        End Try
    End Sub

    Public Sub RP_SBO_MenuEvent(ByVal pVal As SAPbouiCOM.MenuEvent, ByRef BubbleEvent As Boolean)
        Try
            If pVal.BeforeAction = False Then
                Dim objForm As SAPbouiCOM.Form
                If pVal.MenuUID = "AE_RP" Then
                    LoadFromXML("Related Party.srf", p_oSBOApplication)
                    objForm = p_oSBOApplication.Forms.Item("RELPTY")
                    objForm.Visible = True
                    InitializeRPForm(objForm)
                    Exit Try
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

End Module

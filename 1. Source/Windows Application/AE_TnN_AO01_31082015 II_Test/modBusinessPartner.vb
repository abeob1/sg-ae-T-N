Module modBusinessPartner

    Private objItem, oItem As SAPbouiCOM.Item
    Private oFolder As SAPbouiCOM.Folder
    Private oStatic As SAPbouiCOM.StaticText
    Private oEdit As SAPbouiCOM.EditText
    Private oOptionBtn As SAPbouiCOM.OptionBtn
    Private oLinkedBtn As SAPbouiCOM.LinkedButton
    Private oComboBox As SAPbouiCOM.ComboBox
    Private oCheckBox As SAPbouiCOM.CheckBox
    Private oRecordSet As SAPbobsCOM.Recordset

#Region "Form Modificaiton"

    Private Function FormModification(ByRef objForm As SAPbouiCOM.Form, ByRef sErrDesc As String) As Long
        Dim sFuncName As String = "FormModification"
        Try
            DisableGeneraltabItems(objForm)
            ModifyBPHeader(objForm, sErrDesc)

            objItem = objForm.Items.Add("tcSPADt", SAPbouiCOM.BoFormItemTypes.it_FOLDER)
            oItem = objForm.Items.Item("3")
            objItem.Height = oItem.Height
            objItem.Left = oItem.Left + 5
            objItem.Width = oItem.Width
            objItem.AffectsFormMode = False

            oFolder = objItem.Specific
            oFolder.Caption = "SPA Related Parties"
            oFolder.Pane = "102"

            oFolder.GroupWith("3")

            LoadSPAItems(objForm, sErrDesc)

            objItem = objForm.Items.Add("tcLnPrts", SAPbouiCOM.BoFormItemTypes.it_FOLDER)
            oItem = objForm.Items.Item("tcSPADt")
            objItem.Height = oItem.Height
            objItem.Left = oItem.Width + oItem.Left + 5
            objItem.Width = oItem.Width
            objItem.AffectsFormMode = False

            oFolder = objItem.Specific
            oFolder.Caption = "Loan Related Parties"
            oFolder.Pane = "99"

            oFolder.GroupWith("3")

            LoadLoanRelatedParties(objForm, sErrDesc)

            objItem = objForm.Items.Add("tcCseFl", SAPbouiCOM.BoFormItemTypes.it_FOLDER)
            oItem = objForm.Items.Item("tcLnPrts")
            objItem.Height = oItem.Height
            objItem.Left = oItem.Width + oItem.Left + 5
            objItem.Width = oItem.Width
            objItem.AffectsFormMode = False

            oFolder = objItem.Specific
            oFolder.Caption = "Property"
            oFolder.Pane = "100"

            oFolder.GroupWith("3")

            LoadPropertiesItem(objForm, sErrDesc)

            objItem = objForm.Items.Add("tcLnPpl", SAPbouiCOM.BoFormItemTypes.it_FOLDER)
            oItem = objForm.Items.Item("tcCseFl")
            objItem.Height = oItem.Height
            objItem.Left = oItem.Width + oItem.Left + 5
            objItem.Width = oItem.Width
            objItem.AffectsFormMode = False

            oFolder = objItem.Specific
            oFolder.Caption = "Loan Principle"
            oFolder.Pane = "101"

            oFolder.GroupWith("3")

            LoadLoanPrncpleItems(objForm, sErrDesc)

            objItem = objForm.Items.Add("tcLnSub", SAPbouiCOM.BoFormItemTypes.it_FOLDER)
            oItem = objForm.Items.Item("tcLnPpl")
            objItem.Height = oItem.Height
            objItem.Left = oItem.Width + oItem.Left + 5
            objItem.Width = oItem.Width
            objItem.AffectsFormMode = False

            oFolder = objItem.Specific
            oFolder.Caption = "Loan Subsidiary"
            oFolder.Pane = "103"

            oFolder.GroupWith("3")

            LoadLoanSubsidy1(objForm, sErrDesc)
            LoadLoanSubsidy2(objForm, sErrDesc)
            LoadLoanSubsidy3(objForm, sErrDesc)

            'objItem = objForm.Items.Add("tcLnSub2", SAPbouiCOM.BoFormItemTypes.it_FOLDER)
            'oItem = objForm.Items.Item("tcLnSub1")
            'objItem.Height = oItem.Height
            'objItem.Left = oItem.Width + oItem.Left + 5
            'objItem.Width = oItem.Width
            'objItem.AffectsFormMode = False

            'oFolder = objItem.Specific
            'oFolder.Caption = "Loan Subsidy 2"
            'oFolder.Pane = "104"

            'oFolder.GroupWith("3")

            'objItem = objForm.Items.Add("tcLnSub3", SAPbouiCOM.BoFormItemTypes.it_FOLDER)
            'oItem = objForm.Items.Item("tcLnSub2")
            'objItem.Height = oItem.Height
            'objItem.Left = oItem.Width + oItem.Left + 5
            'objItem.Width = oItem.Width
            'objItem.AffectsFormMode = False

            'oFolder = objItem.Specific
            'oFolder.Caption = "Loan Subsidy 3"
            'oFolder.Pane = "105"

            'oFolder.GroupWith("3")

            AddChooseFromList(objForm)
            CFLDataBinding(objForm)

            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Completed with SUCCESS", sFuncName)
            FormModification = RTN_SUCCESS
        Catch ex As Exception
            sErrDesc = ex.Message
            Call WriteToLogFile_Debug(sErrDesc, sFuncName)
            FormModification = RTN_ERROR
        End Try
    End Function

    Private Sub ModifyBPHeader(ByRef objForm As SAPbouiCOM.Form, ByRef sErrDesc As String)
        oItem = objForm.Items.Item("40")
        objItem = objForm.Items.Item("1320002080")
        objItem.Left = oItem.Left + oItem.Width + 5

        oStatic = objForm.Items.Item("6").Specific
        oStatic.Caption = "Case File No"

        oStatic = objForm.Items.Item("8").Specific
        oStatic.Caption = "Case File Ref"

        oStatic = objForm.Items.Item("17").Specific
        oStatic.Caption = "Case Type"

        'Foreign Name
        objForm.Items.Item("129").Top = objForm.Items.Item("44").Top
        objForm.Items.Item("129").FromPane = 1
        objForm.Items.Item("129").ToPane = 1
        objForm.Items.Item("128").Top = objForm.Items.Item("43").Top
        objForm.Items.Item("128").FromPane = 1
        objForm.Items.Item("128").ToPane = 1

        'Currency
        objForm.Items.Item("12").Top = objForm.Items.Item("129").Top + 15
        objForm.Items.Item("12").FromPane = 1
        objForm.Items.Item("12").ToPane = 1
        objForm.Items.Item("11").Top = objForm.Items.Item("128").Top + 15
        objForm.Items.Item("11").FromPane = 1
        objForm.Items.Item("11").ToPane = 1

        'Federal tax id
        objForm.Items.Item("42").Top = objForm.Items.Item("12").Top + 15
        objForm.Items.Item("42").FromPane = 1
        objForm.Items.Item("42").ToPane = 1
        objForm.Items.Item("41").Top = objForm.Items.Item("11").Top + 15
        objForm.Items.Item("41").FromPane = 1
        objForm.Items.Item("41").ToPane = 1

        'BP Currency Combo
        objForm.Items.Item("38").Top = objForm.Items.Item("117").Top
        objForm.Items.Item("38").FromPane = 1
        objForm.Items.Item("38").ToPane = 1

        ''GLN
        'objForm.Items.Item("1470002109").Top = objForm.Items.Item("38").Top + 30
        'objForm.Items.Item("1470002110").Top = objForm.Items.Item("38").Top + 30

        'Account Balance
        objForm.Items.Item("27").Top = objForm.Items.Item("38").Top + 15
        objForm.Items.Item("27").Left = objForm.Items.Item("1470002109").Left
        objForm.Items.Item("27").FromPane = 1
        objForm.Items.Item("27").ToPane = 1
        objForm.Items.Item("28").Top = objForm.Items.Item("27").Top
        objForm.Items.Item("28").FromPane = 1
        objForm.Items.Item("28").ToPane = 1

        objForm.Items.Item("26").Top = objForm.Items.Item("38").Top + 15
        objForm.Items.Item("26").FromPane = 1
        objForm.Items.Item("26").ToPane = 1
        objForm.Items.Item("33").Top = objForm.Items.Item("26").Top
        objForm.Items.Item("33").FromPane = 1
        objForm.Items.Item("33").ToPane = 1

        'Deliveries
        objForm.Items.Item("30").Top = objForm.Items.Item("27").Top + 15
        objForm.Items.Item("30").Left = objForm.Items.Item("27").Left
        objForm.Items.Item("30").FromPane = 1
        objForm.Items.Item("30").ToPane = 1
        objForm.Items.Item("36").Top = objForm.Items.Item("30").Top
        objForm.Items.Item("36").FromPane = 1
        objForm.Items.Item("36").ToPane = 1

        objForm.Items.Item("29").Top = objForm.Items.Item("26").Top + 15
        objForm.Items.Item("29").FromPane = 1
        objForm.Items.Item("29").ToPane = 1
        objForm.Items.Item("34").Top = objForm.Items.Item("29").Top
        objForm.Items.Item("34").FromPane = 1
        objForm.Items.Item("34").ToPane = 1

        'Orders
        objForm.Items.Item("32").Top = objForm.Items.Item("30").Top + 15
        objForm.Items.Item("32").Left = objForm.Items.Item("30").Left
        objForm.Items.Item("32").FromPane = 1
        objForm.Items.Item("32").ToPane = 1
        objForm.Items.Item("37").Top = objForm.Items.Item("32").Top
        objForm.Items.Item("37").FromPane = 1
        objForm.Items.Item("37").ToPane = 1

        objForm.Items.Item("31").Top = objForm.Items.Item("29").Top + 15
        objForm.Items.Item("31").FromPane = 1
        objForm.Items.Item("31").ToPane = 1
        objForm.Items.Item("35").Top = objForm.Items.Item("31").Top
        objForm.Items.Item("35").FromPane = 1
        objForm.Items.Item("35").ToPane = 1

        'Opportunities
        objForm.Items.Item("148").Top = objForm.Items.Item("32").Top + 15
        objForm.Items.Item("148").Left = objForm.Items.Item("32").Left
        objForm.Items.Item("148").FromPane = 1
        objForm.Items.Item("148").ToPane = 1
        objForm.Items.Item("146").Top = objForm.Items.Item("148").Top
        objForm.Items.Item("146").FromPane = 1
        objForm.Items.Item("146").ToPane = 1

        objForm.Items.Item("149").Top = objForm.Items.Item("31").Top + 15
        objForm.Items.Item("149").FromPane = 1
        objForm.Items.Item("149").ToPane = 1
        objForm.Items.Item("147").Top = objForm.Items.Item("149").Top
        objForm.Items.Item("147").FromPane = 1
        objForm.Items.Item("147").ToPane = 1

        'Group - Case Type
        objForm.Items.Item("17").Top = objForm.Items.Item("8").Top + 15
        objForm.Items.Item("16").Top = objForm.Items.Item("7").Top + 15

        'Case Status
        oItem = objForm.Items.Item("16")
        objItem = objForm.Items.Add("BPHItm_1", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
        objItem.Top = oItem.Top + 15
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.DisplayDesc = True

        oComboBox = objItem.Specific
        oComboBox.DataBind.SetBound(True, "OCRD", "U_CASESTATUS")

        oItem = objForm.Items.Item("17")
        objItem = objForm.Items.Add("BPHItm_2", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.LinkTo = "BPHItm_1"

        oStatic = objItem.Specific
        oStatic.Caption = "Case Status"

        'Kiv
        oItem = objForm.Items.Item("BPHItm_1")
        objItem = objForm.Items.Add("BPHItm_3", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
        objItem.Top = oItem.Top + 15
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.DisplayDesc = True

        oComboBox = objItem.Specific
        oComboBox.DataBind.SetBound(True, "OCRD", "U_KIVSTATUS")

        oItem = objForm.Items.Item("BPHItm_2")
        objItem = objForm.Items.Add("BPHItm_4", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.LinkTo = "BPHItm_3"

        oStatic = objItem.Specific
        oStatic.Caption = "KIV"

        'File Opened date
        oItem = objForm.Items.Item("BPHItm_3")
        objItem = objForm.Items.Add("BPHItm_5", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Top = oItem.Top + 15
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_FILEOPENDATE")

        oItem = objForm.Items.Item("BPHItm_4")
        objItem = objForm.Items.Add("BPHItm_6", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.LinkTo = "BPHItm_5"

        oStatic = objItem.Specific
        oStatic.Caption = "File Opened Date"

        'File Closed date
        oItem = objForm.Items.Item("BPHItm_5")
        objItem = objForm.Items.Add("BPHItm_7", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Top = oItem.Top + 15
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_FILECLOSEDATE")

        oItem = objForm.Items.Item("BPHItm_6")
        objItem = objForm.Items.Add("BPHItm_8", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.LinkTo = "BPHItm_7"

        oStatic = objItem.Specific
        oStatic.Caption = "File Closed Date"

        'Partners
        oItem = objForm.Items.Item("1320002080")
        objItem = objForm.Items.Add("BPHItm_20", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left + oItem.Width + 10
        oItem = objForm.Items.Item("40")
        objItem.Top = oItem.Top

        oStatic = objItem.Specific
        oStatic.Caption = "Partners"

        oItem = objForm.Items.Item("117")
        objItem = objForm.Items.Add("BPHItm_19", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        oItem = objForm.Items.Item("BPHItm_20")
        objItem.Left = oItem.Left + oItem.Width + 30
        oItem = objForm.Items.Item("40")
        objItem.Top = oItem.Top

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PARTNER_FST_NAME")

        objItem = objForm.Items.Item("BPHItm_20")
        objItem.LinkTo = "BPHItm_19"

        'Partner id
        oItem = objForm.Items.Item("BPHItm_19")
        objItem = objForm.Items.Add("BPHItm_23", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Left = oItem.Left + oItem.Width + 5
        objItem.Top = oItem.Top
        objItem.Width = 0

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PARTNER_EMPID")

        oItem = objForm.Items.Item("BPHItm_19")
        objItem = objForm.Items.Add("PLkItm_9", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.LinkTo = "BPHItm_23"

        oLinkedBtn = objItem.Specific
        oLinkedBtn.LinkedObject = SAPbouiCOM.BoLinkedObject.lf_Employee

        'LA
        oItem = objForm.Items.Item("BPHItm_19")
        objItem = objForm.Items.Add("BPHItm_21", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Top = oItem.Top + 15
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_LA_FST_NAME")

        oItem = objForm.Items.Item("BPHItm_21")
        objItem = objForm.Items.Add("BPHItm_24", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Left = oItem.Left + oItem.Width + 5
        objItem.Top = oItem.Top
        objItem.Width = 0

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_LA_EMPID")

        oItem = objForm.Items.Item("BPHItm_21")
        objItem = objForm.Items.Add("LALkItm_9", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.LinkTo = "BPHItm_24"

        oLinkedBtn = objItem.Specific
        oLinkedBtn.LinkedObject = SAPbouiCOM.BoLinkedObject.lf_Employee

        oItem = objForm.Items.Item("BPHItm_20")
        objItem = objForm.Items.Add("BPHItm_22", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.LinkTo = "BPHItm_21"

        oStatic = objItem.Specific
        oStatic.Caption = "LA"

        'Manager
        oItem = objForm.Items.Item("BPHItm_22")
        objItem = objForm.Items.Add("BPHItm_10", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15

        oStatic = objItem.Specific
        oStatic.Caption = "Manager"

        oItem = objForm.Items.Item("BPHItm_21")
        objItem = objForm.Items.Add("BPHItm_9", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_MANAGER_FST_NAME")

        objItem = objForm.Items.Item("BPHItm_10")
        objItem.LinkTo = "BPHItm_9"

        oItem = objForm.Items.Item("BPHItm_9")
        objItem = objForm.Items.Add("BPHItm_25", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Left = oItem.Left + oItem.Width + 5
        objItem.Top = oItem.Top
        objItem.Width = 0

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_MANAGER_EMPID")

        oItem = objForm.Items.Item("BPHItm_9")
        objItem = objForm.Items.Add("MLkItm_9", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.LinkTo = "BPHItm_25"

        oLinkedBtn = objItem.Specific
        oLinkedBtn.LinkedObject = SAPbouiCOM.BoLinkedObject.lf_Employee

        'In-Charge
        oItem = objForm.Items.Item("BPHItm_9")
        objItem = objForm.Items.Add("BPHItm_11", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Top = oItem.Top + 15
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_IC_FST_NAME")

        oItem = objForm.Items.Item("BPHItm_11")
        objItem = objForm.Items.Add("BPHItm_26", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Left = oItem.Left + oItem.Width + 5
        objItem.Top = oItem.Top
        objItem.Width = 0

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_IC_EMPID")

        oItem = objForm.Items.Item("BPHItm_11")
        objItem = objForm.Items.Add("ILkItm_9", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.LinkTo = "BPHItm_26"

        oLinkedBtn = objItem.Specific
        oLinkedBtn.LinkedObject = SAPbouiCOM.BoLinkedObject.lf_Employee

        oItem = objForm.Items.Item("BPHItm_10")
        objItem = objForm.Items.Add("BPHItm_12", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.LinkTo = "BPHItm_11"

        oStatic = objItem.Specific
        oStatic.Caption = "In-Charge"

        'Customer Service
        oItem = objForm.Items.Item("BPHItm_11")
        objItem = objForm.Items.Add("BPHItm_13", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Top = oItem.Top + 15
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_CS_FST_NAME")

        oItem = objForm.Items.Item("BPHItm_13")
        objItem = objForm.Items.Add("BPHItm_27", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Left = oItem.Left + oItem.Width + 5
        objItem.Top = oItem.Top
        objItem.Width = 0

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_CS_EMPID")

        oItem = objForm.Items.Item("BPHItm_13")
        objItem = objForm.Items.Add("CSLkItm_9", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.LinkTo = "BPHItm_27"

        oLinkedBtn = objItem.Specific
        oLinkedBtn.LinkedObject = SAPbouiCOM.BoLinkedObject.lf_Employee

        oItem = objForm.Items.Item("BPHItm_12")
        objItem = objForm.Items.Add("BPHItm_14", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.Width = 80
        objItem.LinkTo = "BPHItm_13"

        oStatic = objItem.Specific
        oStatic.Caption = "Customer Service"

        'Vendor ACQ Date
        oItem = objForm.Items.Item("BPHItm_13")
        objItem = objForm.Items.Add("BPHItm_15", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Top = oItem.Top + 15
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VENDORACQDATE")

        oItem = objForm.Items.Item("BPHItm_14")
        objItem = objForm.Items.Add("BPHItm_16", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.Width = 120
        objItem.LinkTo = "BPHItm_15"

        oStatic = objItem.Specific
        oStatic.Caption = "Vendor ACQ Date"

        'File Location
        oItem = objForm.Items.Item("BPHItm_15")
        objItem = objForm.Items.Add("BPHItm_17", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Top = oItem.Top + 15
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_FILELOC")

        oItem = objForm.Items.Item("BPHItm_16")
        objItem = objForm.Items.Add("BPHItm_18", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.LinkTo = "BPHItm_17"

        oStatic = objItem.Specific
        oStatic.Caption = "File Location"

    End Sub

    Private Sub LoadSPAItems(ByRef objForm As SAPbouiCOM.Form, ByRef sErrDesc As String)
        objForm.DataSources.UserDataSources.Add("FolderDS", SAPbouiCOM.BoDataType.dt_LONG_TEXT)
        ''Purchaser
        objItem = objForm.Items.Add("fSPAPurch", SAPbouiCOM.BoFormItemTypes.it_FOLDER)
        oItem = objForm.Items.Item("3")
        objItem.Height = oItem.Height
        objItem.Left = oItem.Left + 5
        objItem.Width = oItem.Width
        oItem = objForm.Items.Item("44")
        objItem.Top = oItem.Top
        objItem.FromPane = "102"
        objItem.ToPane = "102"
        objItem.AffectsFormMode = False

        oFolder = objItem.Specific
        oFolder.Caption = "Purchaser"
        oFolder.Pane = "555"

        oFolder.Select()

        oFolder.DataBind.SetBound(True, "", "FolderDS")

        'Vendor
        objItem = objForm.Items.Add("fSPAVend", SAPbouiCOM.BoFormItemTypes.it_FOLDER)
        oItem = objForm.Items.Item("fSPAPurch")
        objItem.Height = oItem.Height
        objItem.Left = oItem.Left + oItem.Width + 5
        objItem.Width = oItem.Width
        objItem.Top = oItem.Top
        objItem.FromPane = "102"
        objItem.ToPane = "102"
        objItem.AffectsFormMode = False

        oFolder = objItem.Specific
        oFolder.Caption = "Vendor"
        oFolder.Pane = "556"
        oFolder.GroupWith("fSPAPurch")

        oFolder.DataBind.SetBound(True, "", "FolderDS")

        '******************************ADDNING PURCHASE TAB ITEMS********************************
        'SPA DATE
        oItem = objForm.Items.Item("41")
        objItem = objForm.Items.Add("item_2", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Height = oItem.Height
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        oItem = objForm.Items.Item("fSPAPurch")
        objItem.Top = oItem.Top + oItem.Height + 5
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_SPADATE")

        oItem = objForm.Items.Item("fSPAPurch")
        objItem = objForm.Items.Add("item_1", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + oItem.Height + 5
        objItem.Left = oItem.Left + 5
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_2"

        oStatic = objItem.Specific
        oStatic.Caption = "SPA Date"

        'Entry of Private Cavaet
        oItem = objForm.Items.Item("item_2")
        objItem = objForm.Items.Add("item_4", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PRVTCAVEATENTRYDT")

        oItem = objForm.Items.Item("item_1")
        objItem = objForm.Items.Add("item_3", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.Width = "120"
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_4"

        oStatic = objItem.Specific
        oStatic.Caption = "Entry of Private Cavaet"

        'Purchaser
        oItem = objForm.Items.Item("item_3")
        objItem = objForm.Items.Add("item_5", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 20
        objItem.Left = oItem.Left
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_6"

        oStatic = objItem.Specific
        oStatic.Caption = "Purchaser"

        oItem = objForm.Items.Item("item_4")
        objItem = objForm.Items.Add("item_6", SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oCheckBox = objItem.Specific
        oCheckBox.Caption = "Represented by Firm"
        oCheckBox.DataBind.SetBound(True, "OCRD", "U_PURCH_RP_FIRM")

        oItem = objForm.Items.Item("item_6")
        objItem = objForm.Items.Add("item_7", SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oCheckBox = objItem.Specific
        oCheckBox.Caption = "Lawyer Represented"
        oCheckBox.DataBind.SetBound(True, "OCRD", "U_PURCH_RP_LWYR")

        '1st Name Code
        oItem = objForm.Items.Item("item_7")
        objItem = objForm.Items.Add("item_9", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_RP_CODE1")

        oItem = objForm.Items.Item("item_5")
        objItem = objForm.Items.Add("item_8", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        oItem = objForm.Items.Item("item_7")
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_9"

        oStatic = objItem.Specific
        oStatic.Caption = "1st Name Code"

        oItem = objForm.Items.Item("item_9")
        objItem = objForm.Items.Add("LkItm_9", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_9"

        '1st Name
        oItem = objForm.Items.Item("item_9")
        objItem = objForm.Items.Add("item_11", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_RP_NAME1")

        oItem = objForm.Items.Item("item_8")
        objItem = objForm.Items.Add("item_10", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_11"

        oStatic = objItem.Specific
        oStatic.Caption = "1st Name"

        'ID/BRN No
        oItem = objForm.Items.Item("item_11")
        objItem = objForm.Items.Add("item_13", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_RP_ID1")

        oItem = objForm.Items.Item("item_10")
        objItem = objForm.Items.Add("item_12", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_13"

        oStatic = objItem.Specific
        oStatic.Caption = "ID / BRN No"

        'Tax No
        oItem = objForm.Items.Item("item_13")
        objItem = objForm.Items.Add("item_15", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_RP_TAX1")

        oItem = objForm.Items.Item("item_12")
        objItem = objForm.Items.Add("item_14", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_15"

        oStatic = objItem.Specific
        oStatic.Caption = "Tax No"

        'Contact No
        oItem = objForm.Items.Item("item_15")
        objItem = objForm.Items.Add("item_17", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_CONTACT1")

        oItem = objForm.Items.Item("item_14")
        objItem = objForm.Items.Add("item_16", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_17"

        oStatic = objItem.Specific
        oStatic.Caption = "Contact No"

        '1st Type
        oItem = objForm.Items.Item("item_17")
        objItem = objForm.Items.Add("item_53", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.DisplayDesc = True

        oComboBox = objItem.Specific
        oComboBox.DataBind.SetBound(True, "OCRD", "U_PURCH_IDTYPE1")

        oItem = objForm.Items.Item("item_16")
        objItem = objForm.Items.Add("item_52", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_53"

        oStatic = objItem.Specific
        oStatic.Caption = "Type"

        '2nd Name Code
        oItem = objForm.Items.Item("item_53")
        objItem = objForm.Items.Add("item_19", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_RP_CODE2")

        oItem = objForm.Items.Item("item_19")
        objItem = objForm.Items.Add("LkItm_19", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_19"

        oItem = objForm.Items.Item("item_52")
        objItem = objForm.Items.Add("item_18", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_19"

        oStatic = objItem.Specific
        oStatic.Caption = "2nd Name Code"

        '2nd Name
        oItem = objForm.Items.Item("item_19")
        objItem = objForm.Items.Add("item_21", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_RP_NAME2")

        oItem = objForm.Items.Item("item_18")
        objItem = objForm.Items.Add("item_20", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_21"

        oStatic = objItem.Specific
        oStatic.Caption = "2nd Name"

        '2nd ID / BRN
        oItem = objForm.Items.Item("item_21")
        objItem = objForm.Items.Add("item_23", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_RP_ID2")

        oItem = objForm.Items.Item("item_20")
        objItem = objForm.Items.Add("item_22", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_23"

        oStatic = objItem.Specific
        oStatic.Caption = "ID / BRN No"

        'Tax No
        oItem = objForm.Items.Item("item_23")
        objItem = objForm.Items.Add("item_25", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_RP_TAX2")

        oItem = objForm.Items.Item("item_22")
        objItem = objForm.Items.Add("item_24", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_25"

        oStatic = objItem.Specific
        oStatic.Caption = "Tax No"

        '2nd Type
        oItem = objForm.Items.Item("item_25")
        objItem = objForm.Items.Add("item_63", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.DisplayDesc = True

        oComboBox = objItem.Specific
        oComboBox.DataBind.SetBound(True, "OCRD", "U_PURCH_IDTYPE2")

        oItem = objForm.Items.Item("item_24")
        objItem = objForm.Items.Add("item_62", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_63"

        oStatic = objItem.Specific
        oStatic.Caption = "Type"

        '3rd name code
        oItem = objForm.Items.Item("item_25")
        objItem = objForm.Items.Add("item_27", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 30
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_RP_CODE3")

        oItem = objForm.Items.Item("item_27")
        objItem = objForm.Items.Add("LkItm_27", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_27"

        oItem = objForm.Items.Item("item_24")
        objItem = objForm.Items.Add("item_26", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 30
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_27"

        oStatic = objItem.Specific
        oStatic.Caption = "3rd Name Code"

        '3rd Name
        oItem = objForm.Items.Item("item_27")
        objItem = objForm.Items.Add("item_29", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_RP_NAME3")

        oItem = objForm.Items.Item("item_26")
        objItem = objForm.Items.Add("item_28", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_29"

        oStatic = objItem.Specific
        oStatic.Caption = "3rd Name"

        'ID / BRN No
        oItem = objForm.Items.Item("item_29")
        objItem = objForm.Items.Add("item_31", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_RP_ID3")

        oItem = objForm.Items.Item("item_28")
        objItem = objForm.Items.Add("item_30", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_31"

        oStatic = objItem.Specific
        oStatic.Caption = "ID / BRN No"

        'Tax No
        oItem = objForm.Items.Item("item_31")
        objItem = objForm.Items.Add("item_33", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_RP_TAX3")

        oItem = objForm.Items.Item("item_30")
        objItem = objForm.Items.Add("item_32", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_33"

        oStatic = objItem.Specific
        oStatic.Caption = "Tax No"

        '3rd Type
        oItem = objForm.Items.Item("item_33")
        objItem = objForm.Items.Add("item_73", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.DisplayDesc = True

        oComboBox = objItem.Specific
        oComboBox.DataBind.SetBound(True, "OCRD", "U_PURCH_IDTYPE3")

        oItem = objForm.Items.Item("item_32")
        objItem = objForm.Items.Add("item_72", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_73"

        oStatic = objItem.Specific
        oStatic.Caption = "Type"

        '4th name code
        oItem = objForm.Items.Item("item_33")
        objItem = objForm.Items.Add("item_35", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 30
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_RP_CODE4")

        oItem = objForm.Items.Item("item_35")
        objItem = objForm.Items.Add("LkItm_35", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_35"

        oItem = objForm.Items.Item("item_32")
        objItem = objForm.Items.Add("item_34", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 30
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_35"

        oStatic = objItem.Specific
        oStatic.Caption = "4th Name Code"

        '4th Name
        oItem = objForm.Items.Item("item_35")
        objItem = objForm.Items.Add("item_37", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_RP_NAME4")

        oItem = objForm.Items.Item("item_34")
        objItem = objForm.Items.Add("item_36", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_37"

        oStatic = objItem.Specific
        oStatic.Caption = "4th Name"

        'ID / BRN No
        oItem = objForm.Items.Item("item_37")
        objItem = objForm.Items.Add("item_39", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_RP_ID4")

        oItem = objForm.Items.Item("item_36")
        objItem = objForm.Items.Add("item_38", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_39"

        oStatic = objItem.Specific
        oStatic.Caption = "ID / BRN No"

        'Tax No
        oItem = objForm.Items.Item("item_39")
        objItem = objForm.Items.Add("item_41", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_RP_TAX4")

        oItem = objForm.Items.Item("item_38")
        objItem = objForm.Items.Add("item_40", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_41"

        oStatic = objItem.Specific
        oStatic.Caption = "Tax No"

        '4th Type
        oItem = objForm.Items.Item("item_41")
        objItem = objForm.Items.Add("item_83", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.DisplayDesc = True

        oComboBox = objItem.Specific
        oComboBox.DataBind.SetBound(True, "OCRD", "U_PURCH_IDTYPE4")

        oItem = objForm.Items.Item("item_40")
        objItem = objForm.Items.Add("item_82", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_83"

        oStatic = objItem.Specific
        oStatic.Caption = "Type"

        'withdrawal of private caveat
        oItem = objForm.Items.Item("149")
        objItem = objForm.Items.Add("item_43", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        oItem = objForm.Items.Item("item_4")
        objItem.Top = oItem.Top
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PRVTCAVEATWITHDWDT")

        oItem = objForm.Items.Item("item_3")
        objItem = objForm.Items.Add("item_42", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top
        oItem = objForm.Items.Item("118")
        objItem.Left = oItem.Left
        objItem.Width = "125"
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_43"

        oStatic = objItem.Specific
        oStatic.Caption = "Withdrawal of Private Cavaet"

        '1ST Solicitor Code
        oItem = objForm.Items.Item("item_43")
        objItem = objForm.Items.Add("item_45", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        oItem = objForm.Items.Item("item_8")
        objItem.Top = oItem.Top
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_SOL1")

        oItem = objForm.Items.Item("item_45")
        objItem = objForm.Items.Add("LkItm_45", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_45"

        oItem = objForm.Items.Item("item_42")
        objItem = objForm.Items.Add("item_44", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        oItem = objForm.Items.Item("item_8")
        objItem.Top = oItem.Top
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_45"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor"

        '1ST Solicitor Name
        oItem = objForm.Items.Item("item_45")
        objItem = objForm.Items.Add("item_47", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_SOL_NAME1")

        oItem = objForm.Items.Item("item_44")
        objItem = objForm.Items.Add("item_46", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_47"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor Name"

        '1ST S.Location
        oItem = objForm.Items.Item("item_47")
        objItem = objForm.Items.Add("item_49", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_SOL_LOC1")

        oItem = objForm.Items.Item("item_46")
        objItem = objForm.Items.Add("item_48", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_49"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Location"

        '1st S.Ref
        oItem = objForm.Items.Item("item_49")
        objItem = objForm.Items.Add("item_51", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_SOL_REF1")

        oItem = objForm.Items.Item("item_48")
        objItem = objForm.Items.Add("item_50", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_51"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Ref"

        '2nd Solicitor Code
        oItem = objForm.Items.Item("item_51")
        objItem = objForm.Items.Add("item_55", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 45
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_SOL2")

        oItem = objForm.Items.Item("item_55")
        objItem = objForm.Items.Add("LkItm_55", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_55"

        oItem = objForm.Items.Item("item_50")
        objItem = objForm.Items.Add("item_54", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 45
        objItem.Left = oItem.Left
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_55"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor"

        '2nd Solicitor Name
        oItem = objForm.Items.Item("item_55")
        objItem = objForm.Items.Add("item_57", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_SOL_NAME2")

        oItem = objForm.Items.Item("item_54")
        objItem = objForm.Items.Add("item_56", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_57"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor Name"

        '2nd S.Location
        oItem = objForm.Items.Item("item_57")
        objItem = objForm.Items.Add("item_59", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_SOL_LOC2")

        oItem = objForm.Items.Item("item_56")
        objItem = objForm.Items.Add("item_58", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_59"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Location"

        '2nd S.Ref
        oItem = objForm.Items.Item("item_59")
        objItem = objForm.Items.Add("item_61", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_SOL_REF2")

        oItem = objForm.Items.Item("item_58")
        objItem = objForm.Items.Add("item_60", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_61"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Ref"

        '3rd Solicitor Code
        oItem = objForm.Items.Item("item_61")
        objItem = objForm.Items.Add("item_65", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 30
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_SOL3")

        oItem = objForm.Items.Item("item_65")
        objItem = objForm.Items.Add("LkItm_65", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_65"

        oItem = objForm.Items.Item("item_60")
        objItem = objForm.Items.Add("item_64", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 30
        objItem.Left = oItem.Left
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_65"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor"

        '3rd Solicitor Name
        oItem = objForm.Items.Item("item_65")
        objItem = objForm.Items.Add("item_67", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_SOL_NAME3")

        oItem = objForm.Items.Item("item_64")
        objItem = objForm.Items.Add("item_66", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_67"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor Name"

        '3rd S.Location
        oItem = objForm.Items.Item("item_67")
        objItem = objForm.Items.Add("item_69", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_SOL_LOC3")

        oItem = objForm.Items.Item("item_66")
        objItem = objForm.Items.Add("item_68", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_69"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Location"

        '3rd S.Ref
        oItem = objForm.Items.Item("item_69")
        objItem = objForm.Items.Add("item_71", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_SOL_REF3")

        oItem = objForm.Items.Item("item_68")
        objItem = objForm.Items.Add("item_70", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_71"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Ref"

        '4th Solicitor Code
        oItem = objForm.Items.Item("item_71")
        objItem = objForm.Items.Add("item_75", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 30
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_SOL4")

        oItem = objForm.Items.Item("item_75")
        objItem = objForm.Items.Add("LkItm_75", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_75"

        oItem = objForm.Items.Item("item_70")
        objItem = objForm.Items.Add("item_74", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 30
        objItem.Left = oItem.Left
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_75"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor"

        '4th Solicitor Name
        oItem = objForm.Items.Item("item_75")
        objItem = objForm.Items.Add("item_77", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_SOL_NAME4")

        oItem = objForm.Items.Item("item_74")
        objItem = objForm.Items.Add("item_76", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_77"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor Name"

        '4th S.Location
        oItem = objForm.Items.Item("item_77")
        objItem = objForm.Items.Add("item_79", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_SOL_LOC4")

        oItem = objForm.Items.Item("item_76")
        objItem = objForm.Items.Add("item_78", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_79"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Location"

        '4th S.Ref
        oItem = objForm.Items.Item("item_79")
        objItem = objForm.Items.Add("item_81", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "555"
        objItem.ToPane = "555"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PURCH_SOL_REF4")

        oItem = objForm.Items.Item("item_78")
        objItem = objForm.Items.Add("item_80", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "555"
        objItem.ToPane = "555"
        objItem.LinkTo = "item_81"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Ref"

        '*************************ADDING VENDOR TAB ITEMS****************************************
        'Request for Developer consent
        oItem = objForm.Items.Item("41")
        objItem = objForm.Items.Add("item_84", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Height = oItem.Height
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left + 50
        oItem = objForm.Items.Item("item_1")
        objItem.Top = oItem.Top
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_REQ_DEV_CONSENT")

        oItem = objForm.Items.Item("fSPAPurch")
        objItem = objForm.Items.Add("item_85", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + oItem.Height + 5
        objItem.Left = oItem.Left + 5
        objItem.Width = 150
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_84"

        oStatic = objItem.Specific
        oStatic.Caption = "Request for Developer consent"

        'Represented by Firm
        oItem = objForm.Items.Item("item_84")
        objItem = objForm.Items.Add("item_86", SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oCheckBox = objItem.Specific
        oCheckBox.Caption = "Represented by Firm"
        oCheckBox.DataBind.SetBound(True, "OCRD", "U_VNDR_RP_FIRM")

        'Lawyer Represented
        oItem = objForm.Items.Item("item_86")
        objItem = objForm.Items.Add("item_87", SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oCheckBox = objItem.Specific
        oCheckBox.Caption = "Lawyer Represented"
        oCheckBox.DataBind.SetBound(True, "OCRD", "U_VNDR_RP_LWYR")

        'Vendor
        oItem = objForm.Items.Item("item_85")
        objItem = objForm.Items.Add("item_88", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 20
        objItem.Left = oItem.Left
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_86"

        oStatic = objItem.Specific
        oStatic.Caption = "Vendor"

        '1st Name Code
        oItem = objForm.Items.Item("item_87")
        objItem = objForm.Items.Add("item_90", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_RP_CODE1")

        oItem = objForm.Items.Item("item_90")
        objItem = objForm.Items.Add("LkItm_90", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_90"

        oItem = objForm.Items.Item("item_88")
        objItem = objForm.Items.Add("item_91", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        oItem = objForm.Items.Item("item_90")
        objItem.Top = oItem.Top
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_90"

        oStatic = objItem.Specific
        oStatic.Caption = "1st Name Code"

        '1st Name
        oItem = objForm.Items.Item("item_90")
        objItem = objForm.Items.Add("item_92", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_RP_NAME1")

        oItem = objForm.Items.Item("item_91")
        objItem = objForm.Items.Add("item_93", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_92"

        oStatic = objItem.Specific
        oStatic.Caption = "1st Name"

        'ID/BRN No
        oItem = objForm.Items.Item("item_92")
        objItem = objForm.Items.Add("item_94", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_RP_ID1")

        oItem = objForm.Items.Item("item_93")
        objItem = objForm.Items.Add("item_95", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_94"

        oStatic = objItem.Specific
        oStatic.Caption = "ID / BRN No"

        'Tax No
        oItem = objForm.Items.Item("item_94")
        objItem = objForm.Items.Add("item_96", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_RP_TAX1")

        oItem = objForm.Items.Item("item_95")
        objItem = objForm.Items.Add("item_97", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_96"

        oStatic = objItem.Specific
        oStatic.Caption = "Tax No"

        'Contact No
        oItem = objForm.Items.Item("item_96")
        objItem = objForm.Items.Add("item_98", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_CONTACT1")

        oItem = objForm.Items.Item("item_97")
        objItem = objForm.Items.Add("item_99", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_98"

        oStatic = objItem.Specific
        oStatic.Caption = "Contact No"

        '1st Type
        oItem = objForm.Items.Item("item_98")
        objItem = objForm.Items.Add("item_134", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.DisplayDesc = True

        oComboBox = objItem.Specific
        oComboBox.DataBind.SetBound(True, "OCRD", "U_VNDR_IDTYPE1")

        oItem = objForm.Items.Item("item_99")
        objItem = objForm.Items.Add("item_135", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_134"

        oStatic = objItem.Specific
        oStatic.Caption = "Type"

        '2nd Name Code
        oItem = objForm.Items.Item("item_134")
        objItem = objForm.Items.Add("item_100", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_RP_CODE2")

        oItem = objForm.Items.Item("item_100")
        objItem = objForm.Items.Add("LkItm_100", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_100"

        oItem = objForm.Items.Item("item_135")
        objItem = objForm.Items.Add("item_101", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_100"

        oStatic = objItem.Specific
        oStatic.Caption = "2nd Name Code"

        '2nd Name
        oItem = objForm.Items.Item("item_100")
        objItem = objForm.Items.Add("item_102", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_RP_NAME2")

        oItem = objForm.Items.Item("item_101")
        objItem = objForm.Items.Add("item_103", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_102"

        oStatic = objItem.Specific
        oStatic.Caption = "2nd Name"

        '2nd ID / BRN
        oItem = objForm.Items.Item("item_102")
        objItem = objForm.Items.Add("item_104", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_RP_ID2")

        oItem = objForm.Items.Item("item_103")
        objItem = objForm.Items.Add("item_105", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_104"

        oStatic = objItem.Specific
        oStatic.Caption = "ID / BRN No"

        '2ND Tax No
        oItem = objForm.Items.Item("item_104")
        objItem = objForm.Items.Add("item_106", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_RP_TAX2")

        oItem = objForm.Items.Item("item_105")
        objItem = objForm.Items.Add("item_107", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_106"

        oStatic = objItem.Specific
        oStatic.Caption = "Tax No"

        '2nd Type
        oItem = objForm.Items.Item("item_106")
        objItem = objForm.Items.Add("item_144", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.DisplayDesc = True

        oComboBox = objItem.Specific
        oComboBox.DataBind.SetBound(True, "OCRD", "U_VNDR_IDTYPE2")

        oItem = objForm.Items.Item("item_107")
        objItem = objForm.Items.Add("item_145", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_144"

        oStatic = objItem.Specific
        oStatic.Caption = "Type"

        '3rd name code
        oItem = objForm.Items.Item("item_144")
        objItem = objForm.Items.Add("item_108", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_RP_CODE3")

        oItem = objForm.Items.Item("item_108")
        objItem = objForm.Items.Add("LkItm_108", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_108"

        oItem = objForm.Items.Item("item_145")
        objItem = objForm.Items.Add("item_109", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_108"

        oStatic = objItem.Specific
        oStatic.Caption = "3rd Name Code"

        '3rd Name
        oItem = objForm.Items.Item("item_108")
        objItem = objForm.Items.Add("item_110", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_RP_NAME3")

        oItem = objForm.Items.Item("item_109")
        objItem = objForm.Items.Add("item_111", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_110"

        oStatic = objItem.Specific
        oStatic.Caption = "3rd Name"

        '3rd ID / BRN No
        oItem = objForm.Items.Item("item_110")
        objItem = objForm.Items.Add("item_112", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_RP_ID3")

        oItem = objForm.Items.Item("item_111")
        objItem = objForm.Items.Add("item_113", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_112"

        oStatic = objItem.Specific
        oStatic.Caption = "ID / BRN No"

        '3rd Tax No
        oItem = objForm.Items.Item("item_112")
        objItem = objForm.Items.Add("item_114", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_RP_TAX3")

        oItem = objForm.Items.Item("item_113")
        objItem = objForm.Items.Add("item_115", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_114"

        oStatic = objItem.Specific
        oStatic.Caption = "Tax No"

        '3rd Type
        oItem = objForm.Items.Item("item_114")
        objItem = objForm.Items.Add("item_154", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.DisplayDesc = True

        oComboBox = objItem.Specific
        oComboBox.DataBind.SetBound(True, "OCRD", "U_VNDR_IDTYPE3")

        oItem = objForm.Items.Item("item_115")
        objItem = objForm.Items.Add("item_155", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_154"

        oStatic = objItem.Specific
        oStatic.Caption = "Type"

        '4th name code
        oItem = objForm.Items.Item("item_114")
        objItem = objForm.Items.Add("item_116", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 30
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_RP_CODE4")

        oItem = objForm.Items.Item("item_116")
        objItem = objForm.Items.Add("LkItm_116", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_116"

        oItem = objForm.Items.Item("item_115")
        objItem = objForm.Items.Add("item_117", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 30
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_116"

        oStatic = objItem.Specific
        oStatic.Caption = "4th Name Code"

        '4th Name
        oItem = objForm.Items.Item("item_116")
        objItem = objForm.Items.Add("item_118", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_RP_NAME4")

        oItem = objForm.Items.Item("item_117")
        objItem = objForm.Items.Add("item_119", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_118"

        oStatic = objItem.Specific
        oStatic.Caption = "4th Name"

        '4th ID / BRN No
        oItem = objForm.Items.Item("item_118")
        objItem = objForm.Items.Add("item_120", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_RP_ID4")

        oItem = objForm.Items.Item("item_119")
        objItem = objForm.Items.Add("item_121", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_120"

        oStatic = objItem.Specific
        oStatic.Caption = "ID / BRN No"

        '4th Tax No
        oItem = objForm.Items.Item("item_120")
        objItem = objForm.Items.Add("item_122", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_RP_TAX4")

        oItem = objForm.Items.Item("item_121")
        objItem = objForm.Items.Add("item_123", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_122"

        oStatic = objItem.Specific
        oStatic.Caption = "Tax No"

        '4th Type
        oItem = objForm.Items.Item("item_122")
        objItem = objForm.Items.Add("item_164", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.DisplayDesc = True

        oComboBox = objItem.Specific
        oComboBox.DataBind.SetBound(True, "OCRD", "U_VNDR_IDTYPE4")

        oItem = objForm.Items.Item("item_123")
        objItem = objForm.Items.Add("item_165", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_164"

        oStatic = objItem.Specific
        oStatic.Caption = "Type"

        'Receive Developer consent
        oItem = objForm.Items.Item("117")
        objItem = objForm.Items.Add("item_124", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        oItem = objForm.Items.Item("BPHItm_17")
        objItem.Left = oItem.Left + 30
        oItem = objForm.Items.Item("fSPAPurch")
        objItem.Top = oItem.Top + oItem.Height + 5
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_RECV_DEV_CONSENT")

        oItem = objForm.Items.Item("BPHItm_18")
        objItem = objForm.Items.Add("item_125", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        oItem = objForm.Items.Item("item_124")
        objItem.Top = oItem.Top
        objItem.Width = "130"
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_124"

        oStatic = objItem.Specific
        oStatic.Caption = "Receive Developer consent"

        '1st Solicitor Code
        oItem = objForm.Items.Item("item_124")
        objItem = objForm.Items.Add("item_126", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        oItem = objForm.Items.Item("item_90")
        objItem.Top = oItem.Top
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_SOL1")

        oItem = objForm.Items.Item("item_126")
        objItem = objForm.Items.Add("LkItm_126", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_126"

        oItem = objForm.Items.Item("item_125")
        objItem = objForm.Items.Add("item_127", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        oItem = objForm.Items.Item("item_126")
        objItem.Top = oItem.Top
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_126"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor"

        '1st Solicitor Name
        oItem = objForm.Items.Item("item_126")
        objItem = objForm.Items.Add("item_128", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_SOL_NAME1")

        oItem = objForm.Items.Item("item_127")
        objItem = objForm.Items.Add("item_129", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_128"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor Name"

        '1st S.Location
        oItem = objForm.Items.Item("item_128")
        objItem = objForm.Items.Add("item_130", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_SOL_LOC1")

        oItem = objForm.Items.Item("item_129")
        objItem = objForm.Items.Add("item_131", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_130"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Location"

        '1st S.Ref
        oItem = objForm.Items.Item("item_130")
        objItem = objForm.Items.Add("item_132", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_SOL_REF1")

        oItem = objForm.Items.Item("item_131")
        objItem = objForm.Items.Add("item_133", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_132"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Ref"

        '2nd Solicitor Code
        oItem = objForm.Items.Item("item_132")
        objItem = objForm.Items.Add("item_136", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 45
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_SOL2")

        oItem = objForm.Items.Item("item_136")
        objItem = objForm.Items.Add("LkItm_136", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_136"

        oItem = objForm.Items.Item("item_133")
        objItem = objForm.Items.Add("item_137", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 45
        objItem.Left = oItem.Left
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_136"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor"

        '2nd Solicitor Name
        oItem = objForm.Items.Item("item_136")
        objItem = objForm.Items.Add("item_138", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_SOL_NAME2")

        oItem = objForm.Items.Item("item_137")
        objItem = objForm.Items.Add("item_139", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_138"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor Name"

        '2nd S.Location
        oItem = objForm.Items.Item("item_138")
        objItem = objForm.Items.Add("item_140", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_SOL_LOC2")

        oItem = objForm.Items.Item("item_139")
        objItem = objForm.Items.Add("item_141", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_140"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Location"

        '2nd S.Ref
        oItem = objForm.Items.Item("item_140")
        objItem = objForm.Items.Add("item_142", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_SOL_REF2")

        oItem = objForm.Items.Item("item_141")
        objItem = objForm.Items.Add("item_143", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_142"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Ref"

        '3rd Solicitor Code
        oItem = objForm.Items.Item("item_142")
        objItem = objForm.Items.Add("item_146", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 30
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_SOL3")

        oItem = objForm.Items.Item("item_146")
        objItem = objForm.Items.Add("LkItm_146", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_146"

        oItem = objForm.Items.Item("item_143")
        objItem = objForm.Items.Add("item_147", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 30
        objItem.Left = oItem.Left
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_146"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor"

        '3rd Solicitor Name
        oItem = objForm.Items.Item("item_146")
        objItem = objForm.Items.Add("item_148", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_SOL_NAME3")

        oItem = objForm.Items.Item("item_147")
        objItem = objForm.Items.Add("item_149", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_148"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor Name"

        '3rd S.Location
        oItem = objForm.Items.Item("item_148")
        objItem = objForm.Items.Add("item_150", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_SOL_LOC3")

        oItem = objForm.Items.Item("item_149")
        objItem = objForm.Items.Add("item_151", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_150"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Location"

        '3rd S.Ref
        oItem = objForm.Items.Item("item_150")
        objItem = objForm.Items.Add("item_152", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_SOL_REF3")

        oItem = objForm.Items.Item("item_151")
        objItem = objForm.Items.Add("item_153", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_152"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Ref"

        '4th Solicitor Code
        oItem = objForm.Items.Item("item_152")
        objItem = objForm.Items.Add("item_156", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 30
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_SOL4")

        oItem = objForm.Items.Item("item_156")
        objItem = objForm.Items.Add("LkItm_156", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_156"

        oItem = objForm.Items.Item("item_153")
        objItem = objForm.Items.Add("item_157", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 30
        objItem.Left = oItem.Left
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_156"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor"

        '4th Solicitor Name
        oItem = objForm.Items.Item("item_156")
        objItem = objForm.Items.Add("item_158", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_SOL_NAME4")

        oItem = objForm.Items.Item("item_157")
        objItem = objForm.Items.Add("item_159", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_158"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor Name"

        '4th S.Location
        oItem = objForm.Items.Item("item_158")
        objItem = objForm.Items.Add("item_160", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_SOL_LOC4")

        oItem = objForm.Items.Item("item_159")
        objItem = objForm.Items.Add("item_161", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_160"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Location"

        '4th S.Ref
        oItem = objForm.Items.Item("item_160")
        objItem = objForm.Items.Add("item_162", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "556"
        objItem.ToPane = "556"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_SOL_REF4")

        oItem = objForm.Items.Item("item_161")
        objItem = objForm.Items.Add("item_163", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "556"
        objItem.ToPane = "556"
        objItem.LinkTo = "item_162"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Ref"

    End Sub

    Private Sub LoadLoanRelatedParties(ByRef objForm As SAPbouiCOM.Form, ByRef sErrDesc As String)
        ''Borrower
        objItem = objForm.Items.Add("fBorrow", SAPbouiCOM.BoFormItemTypes.it_FOLDER)
        oItem = objForm.Items.Item("3")
        objItem.Height = oItem.Height
        objItem.Left = oItem.Left + 5
        objItem.Width = oItem.Width
        oItem = objForm.Items.Item("44")
        objItem.Top = oItem.Top
        objItem.FromPane = "99"
        objItem.ToPane = "99"
        objItem.AffectsFormMode = False

        oFolder = objItem.Specific
        oFolder.Caption = "Borrower"
        oFolder.Pane = "665"

        oFolder.Select()

        oFolder.DataBind.SetBound(True, "", "FolderDS")

        'Guarantor
        objItem = objForm.Items.Add("fGurant", SAPbouiCOM.BoFormItemTypes.it_FOLDER)
        oItem = objForm.Items.Item("fBorrow")
        objItem.Height = oItem.Height
        objItem.Left = oItem.Left + oItem.Width + 5
        objItem.Width = oItem.Width
        objItem.Top = oItem.Top
        objItem.FromPane = "99"
        objItem.ToPane = "99"
        objItem.AffectsFormMode = False

        oFolder = objItem.Specific
        oFolder.Caption = "Guarantor"
        oFolder.Pane = "666"
        oFolder.GroupWith("fBorrow")

        oFolder.DataBind.SetBound(True, "", "FolderDS")

        '***********************LOAD BORROWER TAB ITEMS**************************
        'Borrower
        objItem = objForm.Items.Add("LRPItm_6", SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX)
        oItem = objForm.Items.Item("5")
        objItem.Left = oItem.Left
        objItem.Width = 150
        oItem = objForm.Items.Item("fBorrow")
        objItem.Top = oItem.Top + oItem.Height + 5
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oCheckBox = objItem.Specific
        oCheckBox.Caption = "Represented by Firm"
        oCheckBox.DataBind.SetBound(True, "OCRD", "U_BRWR_RP_FIRM")

        oItem = objForm.Items.Item("LRPItm_6")
        objItem = objForm.Items.Add("LRPItm_7", SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Width = 150
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oCheckBox = objItem.Specific
        oCheckBox.Caption = "Lawyer Represented"
        oCheckBox.DataBind.SetBound(True, "OCRD", "U_BRWR_RP_LWYR")

        objItem = objForm.Items.Add("LRPItm_5", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        oItem = objForm.Items.Item("LRPItm_6")
        objItem.Top = oItem.Top
        oItem = objForm.Items.Item("6")
        objItem.Left = oItem.Left + 5
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "Borrower"

        '1st Name Code
        oItem = objForm.Items.Item("LRPItm_5")
        objItem = objForm.Items.Add("LRPItm_8", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        oItem = objForm.Items.Item("LRPItm_7")
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "1st Name Code"

        oItem = objForm.Items.Item("LRPItm_7")
        objItem = objForm.Items.Add("LRPItm_9", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_RP_CODE1")

        objItem = objForm.Items.Item("LRPItm_8")
        objItem.LinkTo = "LRPItm_9"

        oItem = objForm.Items.Item("LRPItm_9")
        objItem = objForm.Items.Add("LRPlkItm_9", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "665"
        objItem.ToPane = "665"
        objItem.LinkTo = "LRPItm_9"

        '1st Name
        oItem = objForm.Items.Item("LRPItm_8")
        objItem = objForm.Items.Add("LRPItm_10", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "1st Name"

        oItem = objForm.Items.Item("LRPItm_9")
        objItem = objForm.Items.Add("LRPItm_11", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_RP_NAME1")

        objItem = objForm.Items.Item("LRPItm_10")
        objItem.LinkTo = "LRPItm_11"

        '1st ID/BRN No
        oItem = objForm.Items.Item("LRPItm_10")
        objItem = objForm.Items.Add("LRPItm_12", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "ID / BRN No"

        oItem = objForm.Items.Item("LRPItm_11")
        objItem = objForm.Items.Add("LRPItm_13", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_RP_ID1")

        objItem = objForm.Items.Item("LRPItm_12")
        objItem.LinkTo = "LRPItm_13"

        '1st Tax No
        oItem = objForm.Items.Item("LRPItm_12")
        objItem = objForm.Items.Add("LRPItm_14", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "Tax No"

        oItem = objForm.Items.Item("LRPItm_13")
        objItem = objForm.Items.Add("LRPItm_15", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_RP_TAX1")

        objItem = objForm.Items.Item("LRPItm_14")
        objItem.LinkTo = "LRPItm_15"

        '1st Contact No
        oItem = objForm.Items.Item("LRPItm_14")
        objItem = objForm.Items.Add("LRPItm_16", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "Contact No"

        oItem = objForm.Items.Item("LRPItm_15")
        objItem = objForm.Items.Add("LRPItm_17", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_CONTACT1")

        objItem = objForm.Items.Item("LRPItm_16")
        objItem.LinkTo = "LRPItm_17"

        '1st Type
        oItem = objForm.Items.Item("LRPItm_16")
        objItem = objForm.Items.Add("LRPItm_52", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "Type"

        oItem = objForm.Items.Item("LRPItm_17")
        objItem = objForm.Items.Add("LRPItm_53", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"
        objItem.DisplayDesc = True

        oComboBox = objItem.Specific
        oComboBox.DataBind.SetBound(True, "OCRD", "U_BRWR_IDTYPE1")

        objItem = objForm.Items.Item("LRPItm_52")
        objItem.LinkTo = "LRPItm_53"

        '2nd Name Code
        oItem = objForm.Items.Item("LRPItm_52")
        objItem = objForm.Items.Add("LRPItm_18", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "2nd Name Code"

        oItem = objForm.Items.Item("LRPItm_53")
        objItem = objForm.Items.Add("LRPItm_19", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_RP_CODE2")

        objItem = objForm.Items.Item("LRPItm_18")
        objItem.LinkTo = "LRPItm_19"

        oItem = objForm.Items.Item("LRPItm_19")
        objItem = objForm.Items.Add("LRPlItm_19", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "665"
        objItem.ToPane = "665"
        objItem.LinkTo = "LRPItm_19"

        '2nd Name
        oItem = objForm.Items.Item("LRPItm_18")
        objItem = objForm.Items.Add("LRPItm_20", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "2nd Name"

        oItem = objForm.Items.Item("LRPItm_19")
        objItem = objForm.Items.Add("LRPItm_21", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_RP_NAME2")

        objItem = objForm.Items.Item("LRPItm_20")
        objItem.LinkTo = "LRPItm_21"

        '2nd ID / BRN
        oItem = objForm.Items.Item("LRPItm_20")
        objItem = objForm.Items.Add("LRPItm_22", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "ID / BRN No"

        oItem = objForm.Items.Item("LRPItm_21")
        objItem = objForm.Items.Add("LRPItm_23", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_RP_ID2")

        objItem = objForm.Items.Item("LRPItm_22")
        objItem.LinkTo = "LRPItm_23"

        '2nd Tax No
        oItem = objForm.Items.Item("LRPItm_22")
        objItem = objForm.Items.Add("LRPItm_24", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "Tax No"

        oItem = objForm.Items.Item("LRPItm_23")
        objItem = objForm.Items.Add("LRPItm_25", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_RP_TAX2")

        objItem = objForm.Items.Item("LRPItm_24")
        objItem.LinkTo = "LRPItm_25"

        '2nd Type
        oItem = objForm.Items.Item("LRPItm_24")
        objItem = objForm.Items.Add("LRPItm_62", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "Type"

        oItem = objForm.Items.Item("LRPItm_25")
        objItem = objForm.Items.Add("LRPItm_63", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"
        objItem.DisplayDesc = True

        oComboBox = objItem.Specific
        oComboBox.DataBind.SetBound(True, "OCRD", "U_BRWR_IDTYPE2")

        objItem = objForm.Items.Item("LRPItm_62")
        objItem.LinkTo = "LRPItm_63"

        '3rd name code
        oItem = objForm.Items.Item("LRPItm_24")
        objItem = objForm.Items.Add("LRPItm_26", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 30
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "3rd Name Code"

        oItem = objForm.Items.Item("LRPItm_25")
        objItem = objForm.Items.Add("LRPItm_27", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 30
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_RP_CODE3")

        objItem = objForm.Items.Item("LRPItm_26")
        objItem.LinkTo = "LRPItm_27"

        oItem = objForm.Items.Item("LRPItm_27")
        objItem = objForm.Items.Add("LRPlItm_27", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "665"
        objItem.ToPane = "665"
        objItem.LinkTo = "LRPItm_27"

        '3rd Name
        oItem = objForm.Items.Item("LRPItm_26")
        objItem = objForm.Items.Add("LRPItm_28", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "3rd Name"

        oItem = objForm.Items.Item("LRPItm_27")
        objItem = objForm.Items.Add("LRPItm_29", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_RP_NAME3")

        objItem = objForm.Items.Item("LRPItm_28")
        objItem.LinkTo = "LRPItm_29"

        '3rd ID / BRN No
        oItem = objForm.Items.Item("LRPItm_28")
        objItem = objForm.Items.Add("LRPItm_30", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "ID / BRN No"

        oItem = objForm.Items.Item("LRPItm_29")
        objItem = objForm.Items.Add("LRPItm_31", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_RP_ID3")

        objItem = objForm.Items.Item("LRPItm_30")
        objItem.LinkTo = "LRPItm_31"

        '3rd Tax No
        oItem = objForm.Items.Item("LRPItm_30")
        objItem = objForm.Items.Add("LRPItm_32", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "Tax No"

        oItem = objForm.Items.Item("LRPItm_31")
        objItem = objForm.Items.Add("LRPItm_33", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_RP_TAX3")

        objItem = objForm.Items.Item("LRPItm_32")
        objItem.LinkTo = "LRPItm_33"

        '3rd Type
        oItem = objForm.Items.Item("LRPItm_32")
        objItem = objForm.Items.Add("LRPItm_72", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "Type"

        oItem = objForm.Items.Item("LRPItm_33")
        objItem = objForm.Items.Add("LRPItm_73", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"
        objItem.DisplayDesc = True

        oComboBox = objItem.Specific
        oComboBox.DataBind.SetBound(True, "OCRD", "U_BRWR_IDTYPE3")

        objItem = objForm.Items.Item("LRPItm_72")
        objItem.LinkTo = "LRPItm_73"

        '4th name code
        oItem = objForm.Items.Item("LRPItm_32")
        objItem = objForm.Items.Add("LRPItm_34", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 30
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "4th Name Code"

        oItem = objForm.Items.Item("LRPItm_33")
        objItem = objForm.Items.Add("LRPItm_35", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 30
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_RP_CODE4")

        objItem = objForm.Items.Item("LRPItm_34")
        objItem.LinkTo = "LRPItm_35"

        oItem = objForm.Items.Item("LRPItm_35")
        objItem = objForm.Items.Add("LRPlItm_35", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "665"
        objItem.ToPane = "665"
        objItem.LinkTo = "LRPItm_35"

        '4th Name
        oItem = objForm.Items.Item("LRPItm_34")
        objItem = objForm.Items.Add("LRPItm_36", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "4th Name"

        oItem = objForm.Items.Item("LRPItm_35")
        objItem = objForm.Items.Add("LRPItm_37", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_RP_NAME4")

        objItem = objForm.Items.Item("LRPItm_36")
        objItem.LinkTo = "LRPItm_37"

        '4th ID / BRN No
        oItem = objForm.Items.Item("LRPItm_36")
        objItem = objForm.Items.Add("LRPItm_38", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "ID / BRN No"

        oItem = objForm.Items.Item("LRPItm_37")
        objItem = objForm.Items.Add("LRPItm_39", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_RP_ID4")

        objItem = objForm.Items.Item("LRPItm_38")
        objItem.LinkTo = "LRPItm_39"

        '4th Tax No
        oItem = objForm.Items.Item("LRPItm_38")
        objItem = objForm.Items.Add("LRPItm_40", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "Tax No"

        oItem = objForm.Items.Item("LRPItm_39")
        objItem = objForm.Items.Add("LRPItm_41", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_RP_TAX4")

        objItem = objForm.Items.Item("LRPItm_40")
        objItem.LinkTo = "LRPItm_41"

        '4th Type
        oItem = objForm.Items.Item("LRPItm_40")
        objItem = objForm.Items.Add("LRPItm_82", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "Type"

        oItem = objForm.Items.Item("LRPItm_41")
        objItem = objForm.Items.Add("LRPItm_83", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"
        objItem.DisplayDesc = True

        oComboBox = objItem.Specific
        oComboBox.DataBind.SetBound(True, "OCRD", "U_BRWR_IDTYPE4")

        objItem = objForm.Items.Item("LRPItm_82")
        objItem.LinkTo = "LRPItm_83"

        '1st Solicitor Code
        oItem = objForm.Items.Item("32")
        objItem = objForm.Items.Add("LRPItm_44", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        oItem = objForm.Items.Item("LRPItm_9")
        objItem.Top = oItem.Top
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor"

        oItem = objForm.Items.Item("31")
        objItem = objForm.Items.Add("LRPItm_45", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        oItem = objForm.Items.Item("LRPItm_44")
        objItem.Top = oItem.Top
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_SOL1")

        objItem = objForm.Items.Item("LRPItm_44")
        objItem.LinkTo = "LRPItm_45"

        oItem = objForm.Items.Item("LRPItm_45")
        objItem = objForm.Items.Add("LRPlItm_45", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "665"
        objItem.ToPane = "665"
        objItem.LinkTo = "LRPItm_45"

        '1st Solicitor Name
        oItem = objForm.Items.Item("LRPItm_44")
        objItem = objForm.Items.Add("LRPItm_46", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor Name"

        oItem = objForm.Items.Item("LRPItm_45")
        objItem = objForm.Items.Add("LRPItm_47", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_SOL_NAME1")

        objItem = objForm.Items.Item("LRPItm_46")
        objItem.LinkTo = "LRPItm_47"

        '1st S.Location
        oItem = objForm.Items.Item("LRPItm_46")
        objItem = objForm.Items.Add("LRPItm_48", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Location"

        oItem = objForm.Items.Item("LRPItm_47")
        objItem = objForm.Items.Add("LRPItm_49", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_SOL_LOC1")

        objItem = objForm.Items.Item("LRPItm_48")
        objItem.LinkTo = "LRPItm_49"

        '1st S.Ref
        oItem = objForm.Items.Item("LRPItm_48")
        objItem = objForm.Items.Add("LRPItm_50", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Ref"

        oItem = objForm.Items.Item("LRPItm_49")
        objItem = objForm.Items.Add("LRPItm_51", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_SOL_REF1")

        objItem = objForm.Items.Item("LRPItm_50")
        objItem.LinkTo = "LRPItm_51"

        '2nd Solicitor Code
        oItem = objForm.Items.Item("LRPItm_50")
        objItem = objForm.Items.Add("LRPItm_54", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 45
        objItem.Left = oItem.Left
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor"

        oItem = objForm.Items.Item("LRPItm_51")
        objItem = objForm.Items.Add("LRPItm_55", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 45
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_SOL2")

        objItem = objForm.Items.Item("LRPItm_54")
        objItem.LinkTo = "LRPItm_55"

        oItem = objForm.Items.Item("LRPItm_55")
        objItem = objForm.Items.Add("LRPlItm_55", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "665"
        objItem.ToPane = "665"
        objItem.LinkTo = "LRPItm_55"

        '2nd Solicitor Name
        oItem = objForm.Items.Item("LRPItm_54")
        objItem = objForm.Items.Add("LRPItm_56", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor Name"

        oItem = objForm.Items.Item("LRPItm_55")
        objItem = objForm.Items.Add("LRPItm_57", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_SOL_NAME2")

        objItem = objForm.Items.Item("LRPItm_56")
        objItem.LinkTo = "LRPItm_57"

        '2nd S.Location
        oItem = objForm.Items.Item("LRPItm_56")
        objItem = objForm.Items.Add("LRPItm_58", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Location"

        oItem = objForm.Items.Item("LRPItm_57")
        objItem = objForm.Items.Add("LRPItm_59", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_SOL_LOC2")

        objItem = objForm.Items.Item("LRPItm_58")
        objItem.LinkTo = "LRPItm_59"

        '2nd S.Ref
        oItem = objForm.Items.Item("LRPItm_58")
        objItem = objForm.Items.Add("LRPItm_60", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Ref"

        oItem = objForm.Items.Item("LRPItm_59")
        objItem = objForm.Items.Add("LRPItm_61", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_SOL_REF2")

        objItem = objForm.Items.Item("LRPItm_60")
        objItem.LinkTo = "LRPItm_61"

        '3rd Solicitor Code
        oItem = objForm.Items.Item("LRPItm_60")
        objItem = objForm.Items.Add("LRPItm_64", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 30
        objItem.Left = oItem.Left
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor"

        oItem = objForm.Items.Item("LRPItm_61")
        objItem = objForm.Items.Add("LRPItm_65", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 30
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_SOL3")

        objItem = objForm.Items.Item("LRPItm_64")
        objItem.LinkTo = "LRPItm_65"

        oItem = objForm.Items.Item("LRPItm_65")
        objItem = objForm.Items.Add("LRPlItm_65", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "665"
        objItem.ToPane = "665"
        objItem.LinkTo = "LRPItm_65"

        '3rd Solicitor Name
        oItem = objForm.Items.Item("LRPItm_64")
        objItem = objForm.Items.Add("LRPItm_66", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor Name"

        oItem = objForm.Items.Item("LRPItm_65")
        objItem = objForm.Items.Add("LRPItm_67", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_SOL_NAME3")

        objItem = objForm.Items.Item("LRPItm_66")
        objItem.LinkTo = "LRPItm_67"

        '3rd S.Location
        oItem = objForm.Items.Item("LRPItm_66")
        objItem = objForm.Items.Add("LRPItm_68", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Location"

        oItem = objForm.Items.Item("LRPItm_67")
        objItem = objForm.Items.Add("LRPItm_69", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_SOL_LOC3")

        objItem = objForm.Items.Item("LRPItm_68")
        objItem.LinkTo = "LRPItm_69"

        '3rd S.Ref
        oItem = objForm.Items.Item("LRPItm_68")
        objItem = objForm.Items.Add("LRPItm_70", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Ref"

        oItem = objForm.Items.Item("LRPItm_69")
        objItem = objForm.Items.Add("LRPItm_71", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_SOL_REF3")

        objItem = objForm.Items.Item("LRPItm_70")
        objItem.LinkTo = "LRPItm_71"

        '4th Solicitor Code
        oItem = objForm.Items.Item("LRPItm_70")
        objItem = objForm.Items.Add("LRPItm_74", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 30
        objItem.Left = oItem.Left
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor"

        oItem = objForm.Items.Item("LRPItm_71")
        objItem = objForm.Items.Add("LRPItm_75", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 30
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_SOL4")

        objItem = objForm.Items.Item("LRPItm_74")
        objItem.LinkTo = "LRPItm_75"

        oItem = objForm.Items.Item("LRPItm_75")
        objItem = objForm.Items.Add("LRPlItm_75", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "665"
        objItem.ToPane = "665"
        objItem.LinkTo = "LRPItm_75"

        '4th Solicitor Name
        oItem = objForm.Items.Item("LRPItm_74")
        objItem = objForm.Items.Add("LRPItm_76", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor Name"

        oItem = objForm.Items.Item("LRPItm_75")
        objItem = objForm.Items.Add("LRPItm_77", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_SOL_NAME4")

        objItem = objForm.Items.Item("LRPItm_76")
        objItem.LinkTo = "LRPItm_77"

        '4th S.Location
        oItem = objForm.Items.Item("LRPItm_76")
        objItem = objForm.Items.Add("LRPItm_78", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Location"

        oItem = objForm.Items.Item("LRPItm_77")
        objItem = objForm.Items.Add("LRPItm_79", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_SOL_LOC4")

        objItem = objForm.Items.Item("LRPItm_78")
        objItem.LinkTo = "LRPItm_79"

        '4th S.Ref
        oItem = objForm.Items.Item("LRPItm_78")
        objItem = objForm.Items.Add("LRPItm_80", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Ref"

        oItem = objForm.Items.Item("LRPItm_79")
        objItem = objForm.Items.Add("LRPItm_81", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "665"
        objItem.ToPane = "665"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BRWR_SOL_REF4")

        objItem = objForm.Items.Item("LRPItm_80")
        objItem.LinkTo = "LRPItm_81"

        '*********************************LOAD GUARANTOR TAB ITEMS*******************************
        'Guarantor
        objItem = objForm.Items.Add("LRPItm_84", SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX)
        oItem = objForm.Items.Item("5")
        objItem.Left = oItem.Left
        objItem.Width = 150
        oItem = objForm.Items.Item("fBorrow")
        objItem.Top = oItem.Top + oItem.Height + 5
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oCheckBox = objItem.Specific
        oCheckBox.Caption = "Represented by Firm"
        oCheckBox.DataBind.SetBound(True, "OCRD", "U_GNTR_RP_FIRM")

        oItem = objForm.Items.Item("LRPItm_84")
        objItem = objForm.Items.Add("LRPItm_85", SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Width = 150
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oCheckBox = objItem.Specific
        oCheckBox.Caption = "Lawyer Represented"
        oCheckBox.DataBind.SetBound(True, "OCRD", "U_GNTR_RP_LWYR")

        objItem = objForm.Items.Add("LRPItm_86", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        oItem = objForm.Items.Item("LRPItm_84")
        objItem.Top = oItem.Top
        oItem = objForm.Items.Item("6")
        objItem.Left = oItem.Left + 5
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "Guarantor"

        '1st Name Code
        oItem = objForm.Items.Item("LRPItm_85")
        objItem = objForm.Items.Add("LRPItm_88", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_RP_CODE1")

        oItem = objForm.Items.Item("LRPItm_86")
        objItem = objForm.Items.Add("LRPItm_87", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        oItem = objForm.Items.Item("LRPItm_7")
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"
        objItem.LinkTo = "LRPItm_88"

        oStatic = objItem.Specific
        oStatic.Caption = "1st Name Code"

        oItem = objForm.Items.Item("LRPItm_88")
        objItem = objForm.Items.Add("LRPltm_88", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "666"
        objItem.ToPane = "666"
        objItem.LinkTo = "LRPItm_88"

        '1st Name
        oItem = objForm.Items.Item("LRPItm_88")
        objItem = objForm.Items.Add("LRPItm_90", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_RP_NAME1")

        oItem = objForm.Items.Item("LRPItm_87")
        objItem = objForm.Items.Add("LRPItm_89", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"
        objItem.LinkTo = "LRPItm_90"

        oStatic = objItem.Specific
        oStatic.Caption = "1st Name"

        '1st ID/BRN No
        oItem = objForm.Items.Item("LRPItm_90")
        objItem = objForm.Items.Add("LRPItm_92", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_RP_ID1")

        oItem = objForm.Items.Item("LRPItm_89")
        objItem = objForm.Items.Add("LRPItm_91", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"
        objItem.LinkTo = "LRPItm_92"

        oStatic = objItem.Specific
        oStatic.Caption = "ID / BRN No"

        '1st Tax No
        oItem = objForm.Items.Item("LRPItm_92")
        objItem = objForm.Items.Add("LRPItm_94", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_RP_TAX1")

        oItem = objForm.Items.Item("LRPItm_91")
        objItem = objForm.Items.Add("LRPItm_93", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"
        objItem.LinkTo = "LRPItm_94"

        oStatic = objItem.Specific
        oStatic.Caption = "Tax No"

        '1st Contact No
        oItem = objForm.Items.Item("LRPItm_93")
        objItem = objForm.Items.Add("LRPItm_95", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "Contact No"

        oItem = objForm.Items.Item("LRPItm_94")
        objItem = objForm.Items.Add("LRPItm_96", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_CONTACT1")

        objItem = objForm.Items.Item("LRPItm_95")
        objItem.LinkTo = "LRPItm_96"

        '1st Type
        oItem = objForm.Items.Item("LRPItm_95")
        objItem = objForm.Items.Add("LRPItm_129", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "Type"

        oItem = objForm.Items.Item("LRPItm_96")
        objItem = objForm.Items.Add("LRPItm_130", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"
        objItem.DisplayDesc = True

        oComboBox = objItem.Specific
        oComboBox.DataBind.SetBound(True, "OCRD", "U_GNTR_IDTYPE1")

        objItem = objForm.Items.Item("LRPItm_129")
        objItem.LinkTo = "LRPItm_130"

        '2nd Name Code
        oItem = objForm.Items.Item("LRPItm_129")
        objItem = objForm.Items.Add("LRPItm_97", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "2nd Name Code"

        oItem = objForm.Items.Item("LRPItm_130")
        objItem = objForm.Items.Add("LRPItm_98", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_RP_CODE2")

        objItem = objForm.Items.Item("LRPItm_97")
        objItem.LinkTo = "LRPItm_98"

        oItem = objForm.Items.Item("LRPItm_98")
        objItem = objForm.Items.Add("LRPltm_98", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "666"
        objItem.ToPane = "666"
        objItem.LinkTo = "LRPItm_98"

        '2nd Name
        oItem = objForm.Items.Item("LRPItm_97")
        objItem = objForm.Items.Add("LRPItm_99", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "2nd Name"

        oItem = objForm.Items.Item("LRPItm_98")
        objItem = objForm.Items.Add("LRPItm_100", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_RP_NAME2")

        objItem = objForm.Items.Item("LRPItm_99")
        objItem.LinkTo = "LRPItm_100"

        '2nd ID / BRN
        oItem = objForm.Items.Item("LRPItm_99")
        objItem = objForm.Items.Add("LRPItm_101", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "ID / BRN No"

        oItem = objForm.Items.Item("LRPItm_100")
        objItem = objForm.Items.Add("LRPItm_102", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_RP_ID2")

        objItem = objForm.Items.Item("LRPItm_101")
        objItem.LinkTo = "LRPItm_102"

        '2nd Tax No
        oItem = objForm.Items.Item("LRPItm_101")
        objItem = objForm.Items.Add("LRPItm_103", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "Tax No"

        oItem = objForm.Items.Item("LRPItm_102")
        objItem = objForm.Items.Add("LRPItm_104", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_RP_TAX2")

        objItem = objForm.Items.Item("LRPItm_103")
        objItem.LinkTo = "LRPItm_104"

        '2nd Type
        oItem = objForm.Items.Item("LRPItm_103")
        objItem = objForm.Items.Add("LRPItm_139", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "Type"

        oItem = objForm.Items.Item("LRPItm_104")
        objItem = objForm.Items.Add("LRPItm_140", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"
        objItem.DisplayDesc = True

        oComboBox = objItem.Specific
        oComboBox.DataBind.SetBound(True, "OCRD", "U_GNTR_IDTYPE2")

        objItem = objForm.Items.Item("LRPItm_139")
        objItem.LinkTo = "LRPItm_140"

        '3rd name code
        oItem = objForm.Items.Item("LRPItm_103")
        objItem = objForm.Items.Add("LRPItm_105", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 30
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "3rd Name Code"

        oItem = objForm.Items.Item("LRPItm_104")
        objItem = objForm.Items.Add("LRPItm_106", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 30
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_RP_CODE3")

        objItem = objForm.Items.Item("LRPItm_105")
        objItem.LinkTo = "LRPItm_106"

        oItem = objForm.Items.Item("LRPItm_106")
        objItem = objForm.Items.Add("LRPltm_106", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "666"
        objItem.ToPane = "666"
        objItem.LinkTo = "LRPItm_106"

        '3rd Name
        oItem = objForm.Items.Item("LRPItm_105")
        objItem = objForm.Items.Add("LRPItm_107", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "3rd Name"

        oItem = objForm.Items.Item("LRPItm_106")
        objItem = objForm.Items.Add("LRPItm_108", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_RP_NAME3")

        objItem = objForm.Items.Item("LRPItm_107")
        objItem.LinkTo = "LRPItm_108"

        '3rd ID / BRN No
        oItem = objForm.Items.Item("LRPItm_107")
        objItem = objForm.Items.Add("LRPItm_109", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "ID / BRN No"

        oItem = objForm.Items.Item("LRPItm_108")
        objItem = objForm.Items.Add("LRPItm_110", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_RP_ID3")

        objItem = objForm.Items.Item("LRPItm_109")
        objItem.LinkTo = "LRPItm_110"

        '3rd Tax No
        oItem = objForm.Items.Item("LRPItm_109")
        objItem = objForm.Items.Add("LRPItm_111", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "Tax No"

        oItem = objForm.Items.Item("LRPItm_110")
        objItem = objForm.Items.Add("LRPItm_112", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_RP_TAX3")

        objItem = objForm.Items.Item("LRPItm_111")
        objItem.LinkTo = "LRPItm_112"

        '3rd Type
        oItem = objForm.Items.Item("LRPItm_111")
        objItem = objForm.Items.Add("LRPItm_149", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "Type"

        oItem = objForm.Items.Item("LRPItm_112")
        objItem = objForm.Items.Add("LRPItm_150", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"
        objItem.DisplayDesc = True

        oComboBox = objItem.Specific
        oComboBox.DataBind.SetBound(True, "OCRD", "U_GNTR_IDTYPE3")

        objItem = objForm.Items.Item("LRPItm_149")
        objItem.LinkTo = "LRPItm_150"

        '4th name code
        oItem = objForm.Items.Item("LRPItm_111")
        objItem = objForm.Items.Add("LRPItm_113", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 30
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "4th Name Code"

        oItem = objForm.Items.Item("LRPItm_112")
        objItem = objForm.Items.Add("LRPItm_114", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 30
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_RP_CODE4")

        objItem = objForm.Items.Item("LRPItm_113")
        objItem.LinkTo = "LRPItm_114"

        oItem = objForm.Items.Item("LRPItm_114")
        objItem = objForm.Items.Add("LRPltm_114", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "666"
        objItem.ToPane = "666"
        objItem.LinkTo = "LRPItm_114"

        '4th Name
        oItem = objForm.Items.Item("LRPItm_113")
        objItem = objForm.Items.Add("LRPItm_115", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "4th Name"

        oItem = objForm.Items.Item("LRPItm_114")
        objItem = objForm.Items.Add("LRPItm_116", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_RP_NAME4")

        objItem = objForm.Items.Item("LRPItm_115")
        objItem.LinkTo = "LRPItm_116"

        '4th ID / BRN No
        oItem = objForm.Items.Item("LRPItm_115")
        objItem = objForm.Items.Add("LRPItm_117", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "ID / BRN No"

        oItem = objForm.Items.Item("LRPItm_116")
        objItem = objForm.Items.Add("LRPItm_118", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_RP_ID4")

        objItem = objForm.Items.Item("LRPItm_117")
        objItem.LinkTo = "LRPItm_118"

        '4th Tax No
        oItem = objForm.Items.Item("LRPItm_117")
        objItem = objForm.Items.Add("LRPItm_119", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "Tax No"

        oItem = objForm.Items.Item("LRPItm_118")
        objItem = objForm.Items.Add("LRPItm_120", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_RP_TAX4")

        objItem = objForm.Items.Item("LRPItm_119")
        objItem.LinkTo = "LRPItm_120"

        '4th Type
        oItem = objForm.Items.Item("LRPItm_119")
        objItem = objForm.Items.Add("LRPItm_159", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "Type"

        oItem = objForm.Items.Item("LRPItm_120")
        objItem = objForm.Items.Add("LRPItm_160", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"
        objItem.DisplayDesc = True

        oComboBox = objItem.Specific
        oComboBox.DataBind.SetBound(True, "OCRD", "U_GNTR_IDTYPE4")

        objItem = objForm.Items.Item("LRPItm_159")
        objItem.LinkTo = "LRPItm_160"

        '1st Solicitor Code
        oItem = objForm.Items.Item("118")
        objItem = objForm.Items.Add("LRPItm_121", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        oItem = objForm.Items.Item("LRPItm_8")
        objItem.Top = oItem.Top
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor"

        oItem = objForm.Items.Item("117")
        objItem = objForm.Items.Add("LRPItm_122", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        oItem = objForm.Items.Item("LRPItm_9")
        objItem.Top = oItem.Top
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_SOL1")

        objItem = objForm.Items.Item("LRPItm_121")
        objItem.LinkTo = "LRPItm_122"

        oItem = objForm.Items.Item("LRPItm_122")
        objItem = objForm.Items.Add("LRPltm_122", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "666"
        objItem.ToPane = "666"
        objItem.LinkTo = "LRPItm_122"

        '1st Solicitor Name
        oItem = objForm.Items.Item("LRPItm_121")
        objItem = objForm.Items.Add("LRPItm_123", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor Name"

        oItem = objForm.Items.Item("LRPItm_122")
        objItem = objForm.Items.Add("LRPItm_124", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_SOL_NAME1")

        objItem = objForm.Items.Item("LRPItm_123")
        objItem.LinkTo = "LRPItm_124"

        '1st S.Location
        oItem = objForm.Items.Item("LRPItm_123")
        objItem = objForm.Items.Add("LRPItm_125", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Location"

        oItem = objForm.Items.Item("LRPItm_124")
        objItem = objForm.Items.Add("LRPItm_126", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_SOL_LOC1")

        objItem = objForm.Items.Item("LRPItm_125")
        objItem.LinkTo = "LRPItm_126"

        '1st S.Ref
        oItem = objForm.Items.Item("LRPItm_125")
        objItem = objForm.Items.Add("LRPItm_127", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Ref"

        oItem = objForm.Items.Item("LRPItm_126")
        objItem = objForm.Items.Add("LRPItm_128", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_SOL_REF1")

        objItem = objForm.Items.Item("LRPItm_127")
        objItem.LinkTo = "LRPItm_128"

        '2nd Solicitor Code
        oItem = objForm.Items.Item("LRPItm_127")
        objItem = objForm.Items.Add("LRPItm_131", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 45
        objItem.Left = oItem.Left
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor"

        oItem = objForm.Items.Item("LRPItm_128")
        objItem = objForm.Items.Add("LRPItm_132", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 45
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_SOL2")

        objItem = objForm.Items.Item("LRPItm_131")
        objItem.LinkTo = "LRPItm_132"

        oItem = objForm.Items.Item("LRPItm_132")
        objItem = objForm.Items.Add("LRPltm_132", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "666"
        objItem.ToPane = "666"
        objItem.LinkTo = "LRPItm_132"

        '2nd Solicitor Name
        oItem = objForm.Items.Item("LRPItm_131")
        objItem = objForm.Items.Add("LRPItm_133", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor Name"

        oItem = objForm.Items.Item("LRPItm_132")
        objItem = objForm.Items.Add("LRPItm_134", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_SOL_NAME2")

        objItem = objForm.Items.Item("LRPItm_133")
        objItem.LinkTo = "LRPItm_134"

        '2nd S.Location
        oItem = objForm.Items.Item("LRPItm_133")
        objItem = objForm.Items.Add("LRPItm_135", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Location"

        oItem = objForm.Items.Item("LRPItm_134")
        objItem = objForm.Items.Add("LRPItm_136", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_SOL_LOC2")

        objItem = objForm.Items.Item("LRPItm_135")
        objItem.LinkTo = "LRPItm_136"

        '2nd S.Ref
        oItem = objForm.Items.Item("LRPItm_135")
        objItem = objForm.Items.Add("LRPItm_137", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Ref"

        oItem = objForm.Items.Item("LRPItm_136")
        objItem = objForm.Items.Add("LRPItm_138", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_SOL_REF2")

        objItem = objForm.Items.Item("LRPItm_137")
        objItem.LinkTo = "LRPItm_138"

        '3rd Solicitor Code
        oItem = objForm.Items.Item("LRPItm_137")
        objItem = objForm.Items.Add("LRPItm_141", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 30
        objItem.Left = oItem.Left
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor"

        oItem = objForm.Items.Item("LRPItm_138")
        objItem = objForm.Items.Add("LRPItm_142", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 30
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_SOL3")

        objItem = objForm.Items.Item("LRPItm_141")
        objItem.LinkTo = "LRPItm_142"

        oItem = objForm.Items.Item("LRPItm_142")
        objItem = objForm.Items.Add("LRPltm_142", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "666"
        objItem.ToPane = "666"
        objItem.LinkTo = "LRPItm_142"

        '3rd Solicitor Name
        oItem = objForm.Items.Item("LRPItm_141")
        objItem = objForm.Items.Add("LRPItm_143", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor Name"

        oItem = objForm.Items.Item("LRPItm_142")
        objItem = objForm.Items.Add("LRPItm_144", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_SOL_NAME3")

        objItem = objForm.Items.Item("LRPItm_143")
        objItem.LinkTo = "LRPItm_144"

        '3rd S.Location
        oItem = objForm.Items.Item("LRPItm_143")
        objItem = objForm.Items.Add("LRPItm_145", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Location"

        oItem = objForm.Items.Item("LRPItm_144")
        objItem = objForm.Items.Add("LRPItm_146", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_SOL_LOC3")

        objItem = objForm.Items.Item("LRPItm_145")
        objItem.LinkTo = "LRPItm_146"

        '3rd S.Ref
        oItem = objForm.Items.Item("LRPItm_145")
        objItem = objForm.Items.Add("LRPItm_147", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Ref"

        oItem = objForm.Items.Item("LRPItm_146")
        objItem = objForm.Items.Add("LRPItm_148", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_SOL_REF3")

        objItem = objForm.Items.Item("LRPItm_147")
        objItem.LinkTo = "LRPItm_148"

        '4th Solicitor Code
        oItem = objForm.Items.Item("LRPItm_147")
        objItem = objForm.Items.Add("LRPItm_151", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 30
        objItem.Left = oItem.Left
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor"

        oItem = objForm.Items.Item("LRPItm_148")
        objItem = objForm.Items.Add("LRPItm_152", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 30
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_SOL4")

        objItem = objForm.Items.Item("LRPItm_151")
        objItem.LinkTo = "LRPItm_152"

        oItem = objForm.Items.Item("LRPItm_152")
        objItem = objForm.Items.Add("LRPltm_152", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "666"
        objItem.ToPane = "666"
        objItem.LinkTo = "LRPItm_152"

        '4th Solicitor Name
        oItem = objForm.Items.Item("LRPItm_151")
        objItem = objForm.Items.Add("LRPItm_153", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "Solicitor Name"

        oItem = objForm.Items.Item("LRPItm_152")
        objItem = objForm.Items.Add("LRPItm_154", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"
        objItem.Enabled = False

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_SOL_NAME4")

        objItem = objForm.Items.Item("LRPItm_153")
        objItem.LinkTo = "LRPItm_154"

        '4th S.Location
        oItem = objForm.Items.Item("LRPItm_153")
        objItem = objForm.Items.Add("LRPItm_155", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Location"

        oItem = objForm.Items.Item("LRPItm_154")
        objItem = objForm.Items.Add("LRPItm_156", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_SOL_LOC4")

        objItem = objForm.Items.Item("LRPItm_155")
        objItem.LinkTo = "LRPItm_156"

        '4th S.Ref
        oItem = objForm.Items.Item("LRPItm_155")
        objItem = objForm.Items.Add("LRPItm_157", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Top = oItem.Top + 15
        objItem.Left = oItem.Left
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oStatic = objItem.Specific
        oStatic.Caption = "S.Ref"

        oItem = objForm.Items.Item("LRPItm_156")
        objItem = objForm.Items.Add("LRPItm_158", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "666"
        objItem.ToPane = "666"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_GNTR_SOL_REF4")

        objItem = objForm.Items.Item("LRPItm_157")
        objItem.LinkTo = "LRPItm_158"

    End Sub

    Private Sub LoadPropertiesItem(ByRef objForm As SAPbouiCOM.Form, ByRef sErrDesc As String)
        'Property Details
        objItem = objForm.Items.Add("fPropty", SAPbouiCOM.BoFormItemTypes.it_FOLDER)
        oItem = objForm.Items.Item("3")
        objItem.Height = oItem.Height
        objItem.Left = oItem.Left + 5
        objItem.Width = oItem.Width + 50
        oItem = objForm.Items.Item("44")
        objItem.Top = oItem.Top
        objItem.FromPane = "100"
        objItem.ToPane = "100"
        objItem.AffectsFormMode = False

        oFolder = objItem.Specific
        oFolder.Caption = "Property Details"
        oFolder.Pane = "667"

        oFolder.Select()

        oFolder.DataBind.SetBound(True, "", "FolderDS")

        'Property Valuation & Loans
        objItem = objForm.Items.Add("fValLon", SAPbouiCOM.BoFormItemTypes.it_FOLDER)
        oItem = objForm.Items.Item("fPropty")
        objItem.Height = oItem.Height
        objItem.Left = oItem.Left + oItem.Width + 5
        objItem.Width = oItem.Width
        objItem.Top = oItem.Top
        objItem.FromPane = "100"
        objItem.ToPane = "100"
        objItem.AffectsFormMode = False

        oFolder = objItem.Specific
        oFolder.Caption = "Property Valuation & Loans"
        oFolder.Pane = "668"
        oFolder.GroupWith("fPropty")

        oFolder.DataBind.SetBound(True, "", "FolderDS")

        'Apportionment
        objItem = objForm.Items.Add("fAport", SAPbouiCOM.BoFormItemTypes.it_FOLDER)
        oItem = objForm.Items.Item("fPropty")
        objItem.Height = oItem.Height
        objItem.Left = oItem.Left + oItem.Width + 5
        objItem.Width = oItem.Width
        objItem.Top = oItem.Top
        objItem.FromPane = "100"
        objItem.ToPane = "100"
        objItem.AffectsFormMode = False

        oFolder = objItem.Specific
        oFolder.Caption = "Apportionment"
        oFolder.Pane = "669"
        oFolder.GroupWith("fPropty")

        oFolder.DataBind.SetBound(True, "", "FolderDS")

        '**************************LOAD PROPERTIES TAB ITEMS*****************************
        'Title sys id
        oItem = objForm.Items.Item("41")
        objItem = objForm.Items.Add("CFPItm_3", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left + 30
        oItem = objForm.Items.Item("fPropty")
        objItem.Top = oItem.Top + oItem.Height + 5
        objItem.FromPane = "667"
        objItem.ToPane = "667"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PROPERTY_CODE")

        oItem = objForm.Items.Item("CFPItm_3")
        objItem = objForm.Items.Add("CPFlkItm_3", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "667"
        objItem.ToPane = "667"
        objItem.LinkTo = "CFPItm_3"

        oItem = objForm.Items.Item("fPropty")
        objItem = objForm.Items.Add("CFPItm_2", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        oItem = objForm.Items.Item("CFPItm_3")
        objItem.Top = oItem.Top
        objItem.FromPane = "667"
        objItem.ToPane = "667"
        objItem.LinkTo = "CFPItm_3"

        oStatic = objItem.Specific
        oStatic.Caption = "Title Sys.Id"

        'Title type
        oItem = objForm.Items.Item("CFPItm_3")
        objItem = objForm.Items.Add("CFPItm_4", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_TITLETYPE")

        oItem = objForm.Items.Item("CFPItm_2")
        objItem = objForm.Items.Add("CFPItm_5", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 120
        objItem.FromPane = "667"
        objItem.ToPane = "667"
        objItem.LinkTo = "CFPItm_4"

        oStatic = objItem.Specific
        oStatic.Caption = "Title Type - Jenis Hakmilik"

        'NO Lot
        oItem = objForm.Items.Item("CFPItm_4")
        objItem = objForm.Items.Add("CFPItm_7", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_LOTNO")

        oItem = objForm.Items.Item("CFPItm_5")
        objItem = objForm.Items.Add("CFPItm_6", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"
        objItem.LinkTo = "CFPItm_7"

        oStatic = objItem.Specific
        oStatic.Caption = "No Lot"

        'Previously known as
        oItem = objForm.Items.Item("CFPItm_7")
        objItem = objForm.Items.Add("CFPItm_8", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_FORMERLY_KNOWN_AS")

        oItem = objForm.Items.Item("CFPItm_6")
        objItem = objForm.Items.Add("CFPItm_9", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 120
        objItem.FromPane = "667"
        objItem.ToPane = "667"
        objItem.LinkTo = "CFPItm_8"

        oStatic = objItem.Specific
        oStatic.Caption = "Previously Known as"

        'State
        oItem = objForm.Items.Item("CFPItm_8")
        objItem = objForm.Items.Add("CFPItm_10", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_STATE")

        oItem = objForm.Items.Item("CFPItm_9")
        objItem = objForm.Items.Add("CFPItm_11", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"
        objItem.LinkTo = "CFPItm_10"

        oStatic = objItem.Specific
        oStatic.Caption = "State  - Negeri"

        'Area
        oItem = objForm.Items.Item("CFPItm_10")
        objItem = objForm.Items.Add("CFPItm_12", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_AREA")

        oItem = objForm.Items.Item("CFPItm_11")
        objItem = objForm.Items.Add("CFPItm_13", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"
        objItem.LinkTo = "CFPItm_12"

        oStatic = objItem.Specific
        oStatic.Caption = "Area  - Daerah"

        'Bandar/ Pekan/ Mukim
        oItem = objForm.Items.Item("CFPItm_12")
        objItem = objForm.Items.Add("CFPItm_14", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_BPM")

        oItem = objForm.Items.Item("CFPItm_13")
        objItem = objForm.Items.Add("CFPItm_15", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 120
        objItem.FromPane = "667"
        objItem.ToPane = "667"
        objItem.LinkTo = "CFPItm_14"

        oStatic = objItem.Specific
        oStatic.Caption = "Bandar/ Pekan/ Mukim"

        'Lot Area Square Meter:Luas Lot"
        oItem = objForm.Items.Item("CFPItm_14")
        objItem = objForm.Items.Add("CFPItm_16", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_LOTAREA_SQM")

        oItem = objForm.Items.Item("CFPItm_15")
        objItem = objForm.Items.Add("CFPItm_17", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 130
        objItem.FromPane = "667"
        objItem.ToPane = "667"
        objItem.LinkTo = "CFPItm_16"

        oStatic = objItem.Specific
        oStatic.Caption = "Lot Area Square Meter:Luas Lot"

        'Lot Area in Square Feet:
        oItem = objForm.Items.Item("CFPItm_16")
        objItem = objForm.Items.Add("CFPItm_18", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_LOTAREA_SQFT")

        oItem = objForm.Items.Item("CFPItm_17")
        objItem = objForm.Items.Add("CFPItm_19", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 120
        objItem.FromPane = "667"
        objItem.ToPane = "667"
        objItem.LinkTo = "CFPItm_18"

        oStatic = objItem.Specific
        oStatic.Caption = "Lot Area in Square Feet"

        'Title Search Date:
        oItem = objForm.Items.Item("CFPItm_18")
        objItem = objForm.Items.Add("CFPItm_20", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_TITLESEARCH_DT")

        oItem = objForm.Items.Item("CFPItm_19")
        objItem = objForm.Items.Add("CFPItm_21", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 120
        objItem.FromPane = "667"
        objItem.ToPane = "667"
        objItem.LinkTo = "CFPItm_20"

        oStatic = objItem.Specific
        oStatic.Caption = "Title Search Date"

        'Date submit Consent to Transfer:
        oItem = objForm.Items.Item("CFPItm_20")
        objItem = objForm.Items.Add("CFPItm_22", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_SBM_CNST_TRNSF_DT")

        oItem = objForm.Items.Item("CFPItm_21")
        objItem = objForm.Items.Add("CFPItm_23", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 145
        objItem.FromPane = "667"
        objItem.ToPane = "667"
        objItem.LinkTo = "CFPItm_22"

        oStatic = objItem.Specific
        oStatic.Caption = "Date submit Consent to Transfer"

        'Date receive consent to Transfer:
        oItem = objForm.Items.Item("CFPItm_22")
        objItem = objForm.Items.Add("CFPItm_24", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_RCV_CNST_TRNSF_DT")

        oItem = objForm.Items.Item("CFPItm_23")
        objItem = objForm.Items.Add("CFPItm_25", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 150
        objItem.FromPane = "667"
        objItem.ToPane = "667"
        objItem.LinkTo = "CFPItm_24"

        oStatic = objItem.Specific
        oStatic.Caption = "Date receive Consent to Transfer"

        '14A Date:
        oItem = objForm.Items.Item("CFPItm_24")
        objItem = objForm.Items.Add("CFPItm_26", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_14A_DT")

        oItem = objForm.Items.Item("CFPItm_25")
        objItem = objForm.Items.Add("CFPItm_27", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"
        objItem.LinkTo = "CFPItm_26"

        oStatic = objItem.Specific
        oStatic.Caption = "14A Date"

        'Date of Return of Title from Land Registry:
        oItem = objForm.Items.Item("CFPItm_26")
        objItem = objForm.Items.Add("CFPItm_28", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_TITLE_RTN_LNDRG_DT")

        oItem = objForm.Items.Item("CFPItm_27")
        objItem = objForm.Items.Add("CFPItm_29", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 150
        objItem.FromPane = "667"
        objItem.ToPane = "667"
        objItem.LinkTo = "CFPItm_28"

        oStatic = objItem.Specific
        oStatic.Caption = "Date of Return of Title from Land Registry"

        'Developer
        oItem = objForm.Items.Item("CFPItm_28")
        objItem = objForm.Items.Add("CFPItm_30", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_DEVELOPER_CODE")

        oItem = objForm.Items.Item("CFPItm_30")
        objItem = objForm.Items.Add("CFPlKtm_30", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "667"
        objItem.ToPane = "667"
        objItem.LinkTo = "CFPItm_30"

        oItem = objForm.Items.Item("CFPItm_29")
        objItem = objForm.Items.Add("CFPItm_31", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"
        objItem.LinkTo = "CFPItm_30"

        oStatic = objItem.Specific
        oStatic.Caption = "Developer"

        'Project
        oItem = objForm.Items.Item("CFPItm_30")
        objItem = objForm.Items.Add("CFPItm_32", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PROJECT_CODE")

        oItem = objForm.Items.Item("CFPItm_31")
        objItem = objForm.Items.Add("CFPItm_33", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"
        objItem.LinkTo = "CFPItm_32"

        oStatic = objItem.Specific
        oStatic.Caption = "Project"

        'Advertising / Developer License No.
        oItem = objForm.Items.Item("CFPItm_32")
        objItem = objForm.Items.Add("CFPItm_34", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_ADV_DEV_LIC_NO")

        oItem = objForm.Items.Item("CFPItm_33")
        objItem = objForm.Items.Add("CFPItm_35", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 140
        objItem.FromPane = "667"
        objItem.ToPane = "667"
        objItem.LinkTo = "CFPItm_34"

        oStatic = objItem.Specific
        oStatic.Caption = "Advertising / Developer License No."

        'Developer Solicitor Code
        oItem = objForm.Items.Item("CFPItm_34")
        objItem = objForm.Items.Add("CFPCItm_36", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_DEV_SOL_CODE")

        oItem = objForm.Items.Item("CFPCItm_36")
        objItem = objForm.Items.Add("CFPCltm_36", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "667"
        objItem.ToPane = "667"
        objItem.LinkTo = "CFPCItm_36"

        oItem = objForm.Items.Item("CFPItm_35")
        objItem = objForm.Items.Add("CFPCItm_37", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 120
        objItem.FromPane = "667"
        objItem.ToPane = "667"
        objItem.LinkTo = "CFPCItm_36"

        oStatic = objItem.Specific
        oStatic.Caption = "Developer Solicitor Code"

        'Developer Solicitor Name
        oItem = objForm.Items.Item("CFPCItm_36")
        objItem = objForm.Items.Add("CFPItm_36", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_DEV_SOL_NAME")

        oItem = objForm.Items.Item("CFPCItm_37")
        objItem = objForm.Items.Add("CFPItm_37", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 150
        objItem.FromPane = "667"
        objItem.ToPane = "667"
        objItem.LinkTo = "CFPItm_36"

        oStatic = objItem.Specific
        oStatic.Caption = "Developer Solicitor Name"

        'Dvlp Solicitor Location
        oItem = objForm.Items.Item("CFPItm_36")
        objItem = objForm.Items.Add("CFPItm_38", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "667"
        objItem.ToPane = "667"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_DEV_SOL_LOC")

        oItem = objForm.Items.Item("CFPItm_37")
        objItem = objForm.Items.Add("CFPItm_39", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 120
        objItem.FromPane = "667"
        objItem.ToPane = "667"
        objItem.LinkTo = "CFPItm_38"

        oStatic = objItem.Specific
        oStatic.Caption = "Dvlp Solicitor Location"

        '*********************LOAD PROPERTY VALUES AND LOADN TAB ITEMS**********************************
        'Purchase Price:
        oItem = objForm.Items.Item("41")
        objItem = objForm.Items.Add("CFPItm_42", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left + 50
        oItem = objForm.Items.Item("fPropty")
        objItem.Top = oItem.Top + oItem.Height + 5
        objItem.FromPane = "668"
        objItem.ToPane = "668"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PROPERTY_PURPRC")

        oItem = objForm.Items.Item("fPropty")
        objItem = objForm.Items.Add("CFPItm_41", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        oItem = objForm.Items.Item("CFPItm_42")
        objItem.Top = oItem.Top
        objItem.Width = 120
        objItem.FromPane = "668"
        objItem.ToPane = "668"
        objItem.LinkTo = "CFPItm_42"

        oStatic = objItem.Specific
        oStatic.Caption = "Purchase Price"

        'Adjudicated Value
        oItem = objForm.Items.Item("CFPItm_42")
        objItem = objForm.Items.Add("CFPItm_44", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PROPERTY_ADJ_VALUE")

        oItem = objForm.Items.Item("CFPItm_41")
        objItem = objForm.Items.Add("CFPItm_43", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 120
        objItem.FromPane = "668"
        objItem.ToPane = "668"
        objItem.LinkTo = "CFPItm_44"

        oStatic = objItem.Specific
        oStatic.Caption = "Adjudicated Value"

        'Vendor's Previous SPA Value
        oItem = objForm.Items.Item("CFPItm_44")
        objItem = objForm.Items.Add("CFPItm_46", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_VNDR_PRV_SPA_VALUE")

        oItem = objForm.Items.Item("CFPItm_43")
        objItem = objForm.Items.Add("CFPItm_45", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 130
        objItem.FromPane = "668"
        objItem.ToPane = "668"
        objItem.LinkTo = "CFPItm_46"

        oStatic = objItem.Specific
        oStatic.Caption = "Vendor's Previous SPA Value"

        'Deposit
        oItem = objForm.Items.Item("CFPItm_46")
        objItem = objForm.Items.Add("CFPItm_48", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PROPERTY_DEPOSIT")

        oItem = objForm.Items.Item("CFPItm_45")
        objItem = objForm.Items.Add("CFPItm_47", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"
        objItem.LinkTo = "CFPItm_48"

        oStatic = objItem.Specific
        oStatic.Caption = "Deposit"

        'Balance Purchase Price
        oItem = objForm.Items.Item("CFPItm_48")
        objItem = objForm.Items.Add("CFPItm_50", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PROPERTY_BALPURPRC")

        oItem = objForm.Items.Item("CFPItm_47")
        objItem = objForm.Items.Add("CFPItm_49", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 120
        objItem.FromPane = "668"
        objItem.ToPane = "668"
        objItem.LinkTo = "CFPItm_50"

        oStatic = objItem.Specific
        oStatic.Caption = "Balance Purchase Price"

        'Loan Amount
        oItem = objForm.Items.Item("CFPItm_50")
        objItem = objForm.Items.Add("CFPItm_52", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PROPERTY_LOAN_AMT")

        oItem = objForm.Items.Item("CFPItm_49")
        objItem = objForm.Items.Add("CFPItm_51", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"
        objItem.LinkTo = "CFPItm_52"

        oStatic = objItem.Specific
        oStatic.Caption = "Loan Amount"

        'Loan Case No
        oItem = objForm.Items.Item("CFPItm_52")
        objItem = objForm.Items.Add("CFPItm_54", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_PROPERTY_LOAN_CASE")

        oItem = objForm.Items.Item("CFPItm_51")
        objItem = objForm.Items.Add("CFPItm_53", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"
        objItem.LinkTo = "CFPItm_54"

        oStatic = objItem.Specific
        oStatic.Caption = "Loan Case No"

        'Differential Sum
        oItem = objForm.Items.Item("CFPItm_54")
        objItem = objForm.Items.Add("CFPItm_56", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_DIFFERENTIAL_SUM")

        oItem = objForm.Items.Item("CFPItm_53")
        objItem = objForm.Items.Add("CFPItm_55", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"
        objItem.LinkTo = "CFPItm_56"

        oStatic = objItem.Specific
        oStatic.Caption = "Differential Sum"

        'Redemption Amt
        oItem = objForm.Items.Item("CFPItm_56")
        objItem = objForm.Items.Add("CFPItm_58", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_REDEMPTION_AMT")

        oItem = objForm.Items.Item("CFPItm_55")
        objItem = objForm.Items.Add("CFPItm_57", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"
        objItem.LinkTo = "CFPItm_58"

        oStatic = objItem.Specific
        oStatic.Caption = "Redemption Amt"

        'Redemption Date
        oItem = objForm.Items.Item("CFPItm_58")
        objItem = objForm.Items.Add("CFPItm_60", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_REDEMPTION_DT")

        oItem = objForm.Items.Item("CFPItm_57")
        objItem = objForm.Items.Add("CFPItm_59", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"
        objItem.LinkTo = "CFPItm_60"

        oStatic = objItem.Specific
        oStatic.Caption = "Redemption Date"

        'Deficit Redemption Sum
        oItem = objForm.Items.Item("CFPItm_60")
        objItem = objForm.Items.Add("CFPItm_62", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_DEFICIT_REDEMPTSUM")

        oItem = objForm.Items.Item("CFPItm_59")
        objItem = objForm.Items.Add("CFPItm_61", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"
        objItem.LinkTo = "CFPItm_62"

        oStatic = objItem.Specific
        oStatic.Caption = "Deficit Redemption Sum"

        'Receipt Type
        oItem = objForm.Items.Item("CFPItm_62")
        objItem = objForm.Items.Add("CFPItm_64", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"
        objItem.DisplayDesc = True

        oComboBox = objItem.Specific
        oComboBox.DataBind.SetBound(True, "OCRD", "U_RECEIPT_TYPE")

        oItem = objForm.Items.Item("CFPItm_61")
        objItem = objForm.Items.Add("CFPItm_63", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"
        objItem.LinkTo = "CFPItm_64"

        oStatic = objItem.Specific
        oStatic.Caption = "Receipt Type"

        'Receipt Number
        oItem = objForm.Items.Item("CFPItm_64")
        objItem = objForm.Items.Add("CFPItm_66", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_RECEIPT_NO")

        oItem = objForm.Items.Item("CFPItm_63")
        objItem = objForm.Items.Add("CFPItm_65", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"
        objItem.LinkTo = "CFPItm_66"

        oStatic = objItem.Specific
        oStatic.Caption = "Receipt Number"

        'Receipt Date
        oItem = objForm.Items.Item("CFPItm_66")
        objItem = objForm.Items.Add("CFPItm_68", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_RECEIPT_DT")

        oItem = objForm.Items.Item("CFPItm_65")
        objItem = objForm.Items.Add("CFPItm_67", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"
        objItem.LinkTo = "CFPItm_68"

        oStatic = objItem.Specific
        oStatic.Caption = "Receipt Date"

        'Property currently charged/ assigned to
        oItem = objForm.Items.Item("CFPItm_67")
        objItem = objForm.Items.Add("CFPItm_69", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 195
        objItem.FromPane = "668"
        objItem.ToPane = "668"
        objItem.LinkTo = "CFPItm_68"

        oStatic = objItem.Specific
        oStatic.Caption = "Property currently charged/ assigned to"

        'Charged or Free
        oItem = objForm.Items.Item("CFPItm_68")
        objItem = objForm.Items.Add("CFPItm_70", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 30
        objItem.FromPane = "668"
        objItem.ToPane = "668"
        objItem.DisplayDesc = True

        oComboBox = objItem.Specific
        oComboBox.DataBind.SetBound(True, "OCRD", "U_PROPERTY_ISCHARGED")

        oItem = objForm.Items.Item("CFPItm_69")
        objItem = objForm.Items.Add("CFPItm_71", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"
        objItem.LinkTo = "CFPItm_70"

        oStatic = objItem.Specific
        oStatic.Caption = "Charged or Free"

        'Bank Name
        oItem = objForm.Items.Item("CFPItm_70")
        objItem = objForm.Items.Add("CFPItm_72", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_CHRG_BANK_CODE")

        oItem = objForm.Items.Item("CFPItm_72")
        objItem = objForm.Items.Add("CFPlItm_72", SAPbouiCOM.BoFormItemTypes.it_LINKED_BUTTON)
        objItem.Top = oItem.Top
        objItem.Left = oItem.Left - 20
        objItem.FromPane = "668"
        objItem.ToPane = "668"
        objItem.LinkTo = "CFPItm_72"

        oItem = objForm.Items.Item("CFPItm_71")
        objItem = objForm.Items.Add("CFPItm_73", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"
        objItem.LinkTo = "CFPItm_72"

        oStatic = objItem.Specific
        oStatic.Caption = "Bank Name"

        'Branch
        oItem = objForm.Items.Item("CFPItm_72")
        objItem = objForm.Items.Add("CFPItm_74", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_CHRG_BANK_BRANCH")

        oItem = objForm.Items.Item("CFPItm_73")
        objItem = objForm.Items.Add("CFPItm_75", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"
        objItem.LinkTo = "CFPItm_74"

        oStatic = objItem.Specific
        oStatic.Caption = "Branch"

        'PA Name
        oItem = objForm.Items.Item("CFPItm_74")
        objItem = objForm.Items.Add("CFPItm_76", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_CHRG_BANK_PA_NAME")

        oItem = objForm.Items.Item("CFPItm_75")
        objItem = objForm.Items.Add("CFPItm_77", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"
        objItem.LinkTo = "CFPItm_76"

        oStatic = objItem.Specific
        oStatic.Caption = "PA Name"

        'Presentation No.
        oItem = objForm.Items.Item("CFPItm_76")
        objItem = objForm.Items.Add("CFPItm_78", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_CHRG_BANK_PRSNTNO")

        oItem = objForm.Items.Item("CFPItm_77")
        objItem = objForm.Items.Add("CFPItm_79", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "668"
        objItem.ToPane = "668"
        objItem.LinkTo = "CFPItm_78"

        oStatic = objItem.Specific
        oStatic.Caption = "Presentation No."

        '*********************LOAD APPORTIONMENT TAB ITEMS*********************
        'Legal Completion Date
        oItem = objForm.Items.Item("41")
        objItem = objForm.Items.Add("CFPItm_82", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left + 30
        oItem = objForm.Items.Item("fPropty")
        objItem.Top = oItem.Top + oItem.Height + 5
        objItem.FromPane = "669"
        objItem.ToPane = "669"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_LEGAL_COMPLT_DT")

        oItem = objForm.Items.Item("fPropty")
        objItem = objForm.Items.Add("CFPItm_81", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        oItem = objForm.Items.Item("CFPItm_82")
        objItem.Top = oItem.Top
        objItem.Width = 120
        objItem.FromPane = "669"
        objItem.ToPane = "669"
        objItem.LinkTo = "CFPItm_82"

        oStatic = objItem.Specific
        oStatic.Caption = "Legal Completion Date"

        'Council Assessment Start Date
        oItem = objForm.Items.Item("CFPItm_82")
        objItem = objForm.Items.Add("CFPItm_84", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "669"
        objItem.ToPane = "669"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_COUNCIL_ASST_ST_DT")

        oItem = objForm.Items.Item("CFPItm_81")
        objItem = objForm.Items.Add("CFPItm_83", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 140
        objItem.FromPane = "669"
        objItem.ToPane = "669"
        objItem.LinkTo = "CFPItm_84"

        oStatic = objItem.Specific
        oStatic.Caption = "Council Assessment Start Date"

        'Council Assessment End Date
        oItem = objForm.Items.Item("CFPItm_84")
        objItem = objForm.Items.Add("CFPItm_86", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "669"
        objItem.ToPane = "669"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_COUNCIL_ASST_ED_DT")

        oItem = objForm.Items.Item("CFPItm_83")
        objItem = objForm.Items.Add("CFPItm_85", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 140
        objItem.FromPane = "669"
        objItem.ToPane = "669"
        objItem.LinkTo = "CFPItm_86"

        oStatic = objItem.Specific
        oStatic.Caption = "Council Assessment End Date"

        'Council Assessment Amt
        oItem = objForm.Items.Item("CFPItm_86")
        objItem = objForm.Items.Add("CFPItm_88", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "669"
        objItem.ToPane = "669"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_COUNCIL_ASST_AMT")

        oItem = objForm.Items.Item("CFPItm_85")
        objItem = objForm.Items.Add("CFPItm_87", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 120
        objItem.FromPane = "669"
        objItem.ToPane = "669"
        objItem.LinkTo = "CFPItm_88"

        oStatic = objItem.Specific
        oStatic.Caption = "Council Assessment Amt"

        'Quit Rent Start Date
        oItem = objForm.Items.Item("CFPItm_88")
        objItem = objForm.Items.Add("CFPItm_90", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "669"
        objItem.ToPane = "669"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_QUIT_RENT_ST_DT")

        oItem = objForm.Items.Item("CFPItm_87")
        objItem = objForm.Items.Add("CFPItm_89", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 120
        objItem.FromPane = "669"
        objItem.ToPane = "669"
        objItem.LinkTo = "CFPItm_90"

        oStatic = objItem.Specific
        oStatic.Caption = "Quit Rent Start Date"

        'Quit Rent End Date
        oItem = objForm.Items.Item("CFPItm_90")
        objItem = objForm.Items.Add("CFPItm_92", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "669"
        objItem.ToPane = "669"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_QUIT_RENT_ED_DT")

        oItem = objForm.Items.Item("CFPItm_89")
        objItem = objForm.Items.Add("CFPItm_91", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 120
        objItem.FromPane = "669"
        objItem.ToPane = "669"
        objItem.LinkTo = "CFPItm_92"

        oStatic = objItem.Specific
        oStatic.Caption = "Quit Rent End Date"

        'Quit Rent Amt
        oItem = objForm.Items.Item("CFPItm_92")
        objItem = objForm.Items.Add("CFPItm_94", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "669"
        objItem.ToPane = "669"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_QUIT_RENT_AMT")

        oItem = objForm.Items.Item("CFPItm_91")
        objItem = objForm.Items.Add("CFPItm_93", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 120
        objItem.FromPane = "669"
        objItem.ToPane = "669"
        objItem.LinkTo = "CFPItm_94"

        oStatic = objItem.Specific
        oStatic.Caption = "Quit Rent Amt"

        'Indah Water (Sewage) Start Date
        oItem = objForm.Items.Item("CFPItm_94")
        objItem = objForm.Items.Add("CFPItm_96", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "669"
        objItem.ToPane = "669"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_INDAH_ST_DT")

        oItem = objForm.Items.Item("CFPItm_93")
        objItem = objForm.Items.Add("CFPItm_95", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 160
        objItem.FromPane = "669"
        objItem.ToPane = "669"
        objItem.LinkTo = "CFPItm_96"

        oStatic = objItem.Specific
        oStatic.Caption = "Indah Water (Sewage) Start Date"

        'Indah Water (Sewage) End Date
        oItem = objForm.Items.Item("CFPItm_96")
        objItem = objForm.Items.Add("CFPItm_98", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "669"
        objItem.ToPane = "669"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_INDAH_ED_DT")

        oItem = objForm.Items.Item("CFPItm_95")
        objItem = objForm.Items.Add("CFPItm_97", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 160
        objItem.FromPane = "669"
        objItem.ToPane = "669"
        objItem.LinkTo = "CFPItm_98"

        oStatic = objItem.Specific
        oStatic.Caption = "Indah Water (Sewage) End Date"

        'Indah Water (Sewage) Amt
        oItem = objForm.Items.Item("CFPItm_98")
        objItem = objForm.Items.Add("CFPItm_100", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "669"
        objItem.ToPane = "669"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_INDAH_AMT")

        oItem = objForm.Items.Item("CFPItm_97")
        objItem = objForm.Items.Add("CFPItm_99", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 150
        objItem.FromPane = "669"
        objItem.ToPane = "669"
        objItem.LinkTo = "CFPItm_100"

        oStatic = objItem.Specific
        oStatic.Caption = "Indah Water (Sewage) Amt"

        'Water Start Date
        oItem = objForm.Items.Item("CFPItm_100")
        objItem = objForm.Items.Add("CFPItm_102", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "669"
        objItem.ToPane = "669"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_WATER_ST_DT")

        oItem = objForm.Items.Item("CFPItm_99")
        objItem = objForm.Items.Add("CFPItm_101", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 150
        objItem.FromPane = "669"
        objItem.ToPane = "669"
        objItem.LinkTo = "CFPItm_102"

        oStatic = objItem.Specific
        oStatic.Caption = "Water Start Date"

        'Water End Date
        oItem = objForm.Items.Item("CFPItm_102")
        objItem = objForm.Items.Add("CFPItm_104", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "669"
        objItem.ToPane = "669"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_WATER_ED_DT")

        oItem = objForm.Items.Item("CFPItm_101")
        objItem = objForm.Items.Add("CFPItm_103", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 150
        objItem.FromPane = "669"
        objItem.ToPane = "669"
        objItem.LinkTo = "CFPItm_104"

        oStatic = objItem.Specific
        oStatic.Caption = "Water End Date"

        'Water Amt
        oItem = objForm.Items.Item("CFPItm_104")
        objItem = objForm.Items.Add("CFPItm_106", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "669"
        objItem.ToPane = "669"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_WATER_AMT")

        oItem = objForm.Items.Item("CFPItm_103")
        objItem = objForm.Items.Add("CFPItm_105", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 150
        objItem.FromPane = "669"
        objItem.ToPane = "669"
        objItem.LinkTo = "CFPItm_106"

        oStatic = objItem.Specific
        oStatic.Caption = "Water Amt"

        'Electrical Start Date
        oItem = objForm.Items.Item("CFPItm_106")
        objItem = objForm.Items.Add("CFPItm_108", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "669"
        objItem.ToPane = "669"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_ELECT_ST_DT")

        oItem = objForm.Items.Item("CFPItm_105")
        objItem = objForm.Items.Add("CFPItm_107", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 150
        objItem.FromPane = "669"
        objItem.ToPane = "669"
        objItem.LinkTo = "CFPItm_108"

        oStatic = objItem.Specific
        oStatic.Caption = "Electrical Start Date"

        'Electrical End Date
        oItem = objForm.Items.Item("CFPItm_108")
        objItem = objForm.Items.Add("CFPItm_110", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "669"
        objItem.ToPane = "669"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_ELECT_ED_DT")

        oItem = objForm.Items.Item("CFPItm_107")
        objItem = objForm.Items.Add("CFPItm_109", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 150
        objItem.FromPane = "669"
        objItem.ToPane = "669"
        objItem.LinkTo = "CFPItm_110"

        oStatic = objItem.Specific
        oStatic.Caption = "Electrical End Date"

        'Electrical Amt
        oItem = objForm.Items.Item("CFPItm_110")
        objItem = objForm.Items.Add("CFPItm_112", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "669"
        objItem.ToPane = "669"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_ELECT_AMT")

        oItem = objForm.Items.Item("CFPItm_109")
        objItem = objForm.Items.Add("CFPItm_111", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 150
        objItem.FromPane = "669"
        objItem.ToPane = "669"
        objItem.LinkTo = "CFPItm_112"

        oStatic = objItem.Specific
        oStatic.Caption = "Electrical Amt"

        'Amt Due to Puchaser
        oItem = objForm.Items.Item("CFPItm_112")
        objItem = objForm.Items.Add("CFPItm_114", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "669"
        objItem.ToPane = "669"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_AMT_DUE_PURCH")

        oItem = objForm.Items.Item("CFPItm_111")
        objItem = objForm.Items.Add("CFPItm_113", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 150
        objItem.FromPane = "669"
        objItem.ToPane = "669"
        objItem.LinkTo = "CFPItm_114"

        oStatic = objItem.Specific
        oStatic.Caption = "Amt Due to Puchaser"

        'Amt Due to Vendor
        oItem = objForm.Items.Item("CFPItm_114")
        objItem = objForm.Items.Add("CFPItm_116", SAPbouiCOM.BoFormItemTypes.it_EDIT)
        objItem.Width = oItem.Width
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.FromPane = "669"
        objItem.ToPane = "669"

        oEdit = objItem.Specific
        oEdit.DataBind.SetBound(True, "OCRD", "U_AMT_DUE_VNDR")

        oItem = objForm.Items.Item("CFPItm_113")
        objItem = objForm.Items.Add("CFPItm_115", SAPbouiCOM.BoFormItemTypes.it_STATIC)
        objItem.Left = oItem.Left
        objItem.Top = oItem.Top + 15
        objItem.Width = 150
        objItem.FromPane = "669"
        objItem.ToPane = "669"
        objItem.LinkTo = "CFPItm_116"

        oStatic = objItem.Specific
        oStatic.Caption = "Amt Due to Vendor"

    End Sub

    Private Sub LoadLoanPrncpleItems(ByRef objForm As SAPbouiCOM.Form, ByRef sErrDesc As String)
        Dim sFuncName As String = "LoadLoanPrncpleItems"
        Try
            'Represented by Firm
            oItem = objForm.Items.Item("3")
            objItem = objForm.Items.Add("LPItm_1", SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX)
            objItem.Top = oItem.Top + oItem.Height + 10
            objItem.Left = oItem.Left + 10
            objItem.Width = 120
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oCheckBox = objItem.Specific
            oCheckBox.Caption = "Represented by Firm"
            oCheckBox.DataBind.SetBound(True, "OCRD", "U_BRWR_RP_FIRM")

            'Discharge of Charge
            oItem = objForm.Items.Item("LPItm_1")
            objItem = objForm.Items.Add("LPItm_2", SAPbouiCOM.BoFormItemTypes.it_CHECK_BOX)
            objItem.Top = oItem.Top + 15
            objItem.Left = oItem.Left
            objItem.Width = 120
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oCheckBox = objItem.Specific
            oCheckBox.Caption = "Discharge of Charge"
            oCheckBox.DataBind.SetBound(True, "OCRD", "U_LOAN_WITH_DISCHARG")

            'Bank
            oItem = objForm.Items.Item("LPItm_2")
            objItem = objForm.Items.Add("LPItm_3", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Top = oItem.Top + 30
            objItem.Left = oItem.Left
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oStatic = objItem.Specific
            oStatic.Caption = "Bank"

            'Master Bank Name
            oItem = objForm.Items.Item("41")
            objItem = objForm.Items.Add("LPItm_4", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left + 60
            oItem = objForm.Items.Item("LPItm_3")
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_MSTR_BANKCODE")

            oItem = objForm.Items.Item("LPItm_4")
            objItem = objForm.Items.Add("LPItm_69", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Left = oItem.Left + oItem.Width + 5
            objItem.Top = oItem.Top
            objItem.Width = 0
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oItem = objForm.Items.Item("LPItm_3")
            objItem = objForm.Items.Add("LPItm_5", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_4"

            oStatic = objItem.Specific
            oStatic.Caption = "Master Bank Name"

            'Branch Name
            oItem = objForm.Items.Item("LPItm_4")
            objItem = objForm.Items.Add("LPItm_6", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_BRANCH")

            oItem = objForm.Items.Item("LPItm_5")
            objItem = objForm.Items.Add("LPItm_7", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_6"

            oStatic = objItem.Specific
            oStatic.Caption = "Branch Name"

            'Address
            oItem = objForm.Items.Item("LPItm_6")
            objItem = objForm.Items.Add("LPItm_8", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_BANK_ADD")

            oItem = objForm.Items.Item("LPItm_7")
            objItem = objForm.Items.Add("LPItm_9", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_8"

            oStatic = objItem.Specific
            oStatic.Caption = "Address"

            'PA Name
            oItem = objForm.Items.Item("LPItm_8")
            objItem = objForm.Items.Add("LPItm_10", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_BANK_PA_NAME")

            oItem = objForm.Items.Item("LPItm_9")
            objItem = objForm.Items.Add("LPItm_11", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_10"

            oStatic = objItem.Specific
            oStatic.Caption = "PA Name"

            'Bank Ref
            oItem = objForm.Items.Item("LPItm_10")
            objItem = objForm.Items.Add("LPItm_12", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_BANK_REF")

            oItem = objForm.Items.Item("LPItm_11")
            objItem = objForm.Items.Add("LPItm_13", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_12"

            oStatic = objItem.Specific
            oStatic.Caption = "Bank Ref"

            'Bank Instuction Date
            oItem = objForm.Items.Item("LPItm_12")
            objItem = objForm.Items.Add("LPItm_14", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_BANK_INSTR_DT")

            oItem = objForm.Items.Item("LPItm_13")
            objItem = objForm.Items.Add("LPItm_15", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_14"

            oStatic = objItem.Specific
            oStatic.Caption = "Bank Instuction Date"

            'Letter of Offer Date
            oItem = objForm.Items.Item("LPItm_14")
            objItem = objForm.Items.Add("LPItm_16", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_LETTEROFFR_DT")

            oItem = objForm.Items.Item("LPItm_15")
            objItem = objForm.Items.Add("LPItm_17", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_16"

            oStatic = objItem.Specific
            oStatic.Caption = "Letter of Offer Date"

            'Bank Solicitor's Name
            oItem = objForm.Items.Item("LPItm_16")
            objItem = objForm.Items.Add("LPItm_18", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_BANK_SOL_NAME")

            oItem = objForm.Items.Item("LPItm_17")
            objItem = objForm.Items.Add("LPItm_19", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_18"

            oStatic = objItem.Specific
            oStatic.Caption = "Bank Solicitor's Name"

            'Firm Name
            oItem = objForm.Items.Item("LPItm_18")
            objItem = objForm.Items.Add("LPItm_20", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_BANK_SOL_CODE")

            oItem = objForm.Items.Item("LPItm_19")
            objItem = objForm.Items.Add("LPItm_21", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_20"

            oStatic = objItem.Specific
            oStatic.Caption = "Firm Name"

            'Firm Location
            oItem = objForm.Items.Item("LPItm_20")
            objItem = objForm.Items.Add("LPItm_22", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_BANK_SOL_LOC")

            oItem = objForm.Items.Item("LPItm_21")
            objItem = objForm.Items.Add("LPItm_23", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_22"

            oStatic = objItem.Specific
            oStatic.Caption = "Firm Location"

            'Solicitor's Ref.
            oItem = objForm.Items.Item("LPItm_22")
            objItem = objForm.Items.Add("LPItm_24", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_BANK_SOL_REF")

            oItem = objForm.Items.Item("LPItm_23")
            objItem = objForm.Items.Add("LPItm_25", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_24"

            oStatic = objItem.Specific
            oStatic.Caption = "Solicitor's Ref."

            'Date Request for Redemption Statement
            oItem = objForm.Items.Item("LPItm_24")
            objItem = objForm.Items.Add("LPItm_26", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_RDMPSTMTREQDT")

            oItem = objForm.Items.Item("LPItm_25")
            objItem = objForm.Items.Add("LPItm_27", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 180
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_26"

            oStatic = objItem.Specific
            oStatic.Caption = "Date Request for Redemption Statement"

            'Redemption Statement Date
            oItem = objForm.Items.Item("LPItm_26")
            objItem = objForm.Items.Add("LPItm_28", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_RDMPT_STMT_DT")

            oItem = objForm.Items.Item("LPItm_27")
            objItem = objForm.Items.Add("LPItm_29", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 160
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_28"

            oStatic = objItem.Specific
            oStatic.Caption = "Redemption Statement Date"

            'Redemption Payment Date
            oItem = objForm.Items.Item("LPItm_28")
            objItem = objForm.Items.Add("LPItm_30", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_RDMPTPYMTDT")

            oItem = objForm.Items.Item("LPItm_29")
            objItem = objForm.Items.Add("LPItm_31", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 160
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_30"

            oStatic = objItem.Specific
            oStatic.Caption = "Redemption Payment Date"

            'Discharge Case No
            oItem = objForm.Items.Item("31")
            objItem = objForm.Items.Add("LPItm_32", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            oItem = objForm.Items.Item("BPHItm_17")
            objItem.Left = oItem.Left
            oItem = objForm.Items.Item("LPItm_1")
            objItem.Top = oItem.Top
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_PROPERTY_DISCHRG_C")

            oItem = objForm.Items.Item("BPHItm_18")
            objItem = objForm.Items.Add("LPItm_33", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            oItem = objForm.Items.Item("LPItm_1")
            objItem.Top = oItem.Top
            objItem.Width = 120
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_32"

            oStatic = objItem.Specific
            oStatic.Caption = "Discharge Case No"

            'SPA Case No
            oItem = objForm.Items.Item("LPItm_32")
            objItem = objForm.Items.Add("LPItm_34", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_PROPERTY_SPA_CASE")

            oItem = objForm.Items.Item("LPItm_33")
            objItem = objForm.Items.Add("LPItm_35", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_34"

            oStatic = objItem.Specific
            oStatic.Caption = "SPA Case No"

            'Principal Facility
            oItem = objForm.Items.Item("LPItm_35")
            objItem = objForm.Items.Add("LPItm_36", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 30
            objItem.Width = 120
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_34"

            oStatic = objItem.Specific
            oStatic.Caption = "Principal Facility"

            'Project
            oItem = objForm.Items.Item("LPItm_34")
            objItem = objForm.Items.Add("LPItm_38", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            oItem = objForm.Items.Item("LPItm_36")
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_PROJECT_NAME")

            oItem = objForm.Items.Item("LPItm_36")
            objItem = objForm.Items.Add("LPItm_37", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_34"

            oStatic = objItem.Specific
            oStatic.Caption = "Project"

            'Type of Loan
            oItem = objForm.Items.Item("LPItm_38")
            objItem = objForm.Items.Add("LPItm_40", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.DisplayDesc = True

            oComboBox = objItem.Specific
            oComboBox.DataBind.SetBound(True, "OCRD", "U_LOAN_PRNCP_TYPE")

            oItem = objForm.Items.Item("LPItm_37")
            objItem = objForm.Items.Add("LPItm_39", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_40"

            oStatic = objItem.Specific
            oStatic.Caption = "Type of Loan"

            'Type of Facility
            oItem = objForm.Items.Item("LPItm_40")
            objItem = objForm.Items.Add("LPItm_42", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_PRNCP_FTYPE")

            oItem = objForm.Items.Item("LPItm_39")
            objItem = objForm.Items.Add("LPItm_41", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_42"

            oStatic = objItem.Specific
            oStatic.Caption = "Type of Facility"

            'Facilities Amount
            oItem = objForm.Items.Item("LPItm_42")
            objItem = objForm.Items.Add("LPItm_44", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_PRNCP_FAMT")

            oItem = objForm.Items.Item("LPItm_41")
            objItem = objForm.Items.Add("LPItm_43", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_44"

            oStatic = objItem.Specific
            oStatic.Caption = "Facilities Amount"

            'Repayment Period (Mths)
            oItem = objForm.Items.Item("LPItm_44")
            objItem = objForm.Items.Add("LPItm_46", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_PRNCP_RPYMTPD")

            oItem = objForm.Items.Item("LPItm_43")
            objItem = objForm.Items.Add("LPItm_45", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_46"

            oStatic = objItem.Specific
            oStatic.Caption = "Repayment Period (Mths)"

            'Interest Rate
            oItem = objForm.Items.Item("LPItm_46")
            objItem = objForm.Items.Add("LPItm_48", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_PRNCP_INT_RT")

            oItem = objForm.Items.Item("LPItm_45")
            objItem = objForm.Items.Add("LPItm_47", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_48"

            oStatic = objItem.Specific
            oStatic.Caption = "Interest Rate"

            'Monthly Installment
            oItem = objForm.Items.Item("LPItm_48")
            objItem = objForm.Items.Add("LPItm_50", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_PRNCP_MTHINST")

            oItem = objForm.Items.Item("LPItm_47")
            objItem = objForm.Items.Add("LPItm_49", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_50"

            oStatic = objItem.Specific
            oStatic.Caption = "Monthly Installment"

            'Term Loan Amt
            oItem = objForm.Items.Item("LPItm_50")
            objItem = objForm.Items.Add("LPItm_52", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_PRNCP_TRMLOAN")

            oItem = objForm.Items.Item("LPItm_49")
            objItem = objForm.Items.Add("LPItm_51", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_52"

            oStatic = objItem.Specific
            oStatic.Caption = "Term Loan Amt"

            'Interest
            oItem = objForm.Items.Item("LPItm_52")
            objItem = objForm.Items.Add("LPItm_54", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_PRNCP_INT")

            oItem = objForm.Items.Item("LPItm_51")
            objItem = objForm.Items.Add("LPItm_53", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_54"

            oStatic = objItem.Specific
            oStatic.Caption = "Interest"

            'OD Loan
            oItem = objForm.Items.Item("LPItm_54")
            objItem = objForm.Items.Add("LPItm_56", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_PRNCP_ODLOAN")

            oItem = objForm.Items.Item("LPItm_53")
            objItem = objForm.Items.Add("LPItm_55", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_56"

            oStatic = objItem.Specific
            oStatic.Caption = "OD Loan"

            'MRTA (Insurance)
            oItem = objForm.Items.Item("LPItm_56")
            objItem = objForm.Items.Add("LPItm_58", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_PRNCP_MRTA")

            oItem = objForm.Items.Item("LPItm_55")
            objItem = objForm.Items.Add("LPItm_57", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_58"

            oStatic = objItem.Specific
            oStatic.Caption = "MRTA (Insurance)"

            'Bank Guarentee
            oItem = objForm.Items.Item("LPItm_58")
            objItem = objForm.Items.Add("LPItm_60", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_PRNCP_BNKGNTE")

            oItem = objForm.Items.Item("LPItm_57")
            objItem = objForm.Items.Add("LPItm_59", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_60"

            oStatic = objItem.Specific
            oStatic.Caption = "Bank Guarentee"

            'Letter of Credit
            oItem = objForm.Items.Item("LPItm_60")
            objItem = objForm.Items.Add("LPItm_62", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_PRNCP_LC")

            oItem = objForm.Items.Item("LPItm_59")
            objItem = objForm.Items.Add("LPItm_61", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_62"

            oStatic = objItem.Specific
            oStatic.Caption = "Letter of Credit"

            'Trust Receipt
            oItem = objForm.Items.Item("LPItm_62")
            objItem = objForm.Items.Add("LPItm_64", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_PRNCP_TRUSTRC")

            oItem = objForm.Items.Item("LPItm_61")
            objItem = objForm.Items.Add("LPItm_63", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_64"

            oStatic = objItem.Specific
            oStatic.Caption = "Trust Receipt"

            'Others
            oItem = objForm.Items.Item("LPItm_64")
            objItem = objForm.Items.Add("LPItm_66", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_PRNCP_OTHR")

            oItem = objForm.Items.Item("LPItm_63")
            objItem = objForm.Items.Add("LPItm_65", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_66"

            oStatic = objItem.Specific
            oStatic.Caption = "Others"

            'Total
            oItem = objForm.Items.Item("LPItm_66")
            objItem = objForm.Items.Add("LPItm_68", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "101"
            objItem.ToPane = "101"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_PRNCP_TOTAL")

            oItem = objForm.Items.Item("LPItm_65")
            objItem = objForm.Items.Add("LPItm_67", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "101"
            objItem.ToPane = "101"
            objItem.LinkTo = "LPItm_68"

            oStatic = objItem.Specific
            oStatic.Caption = "Total"

        Catch ex As Exception
            sErrDesc = ex.Message
            Call WriteToLogFile_Debug(sErrDesc, sFuncName)
        End Try
    End Sub

    Private Sub LoadLoanSubsidy1(ByRef objform As SAPbouiCOM.Form, ByRef sErrDesc As String)
        Dim sFuncName As String = "LoadLoanSubsidy1"

        Try
            'Loan Subsidy 1
            objItem = objform.Items.Add("tcLnSub1", SAPbouiCOM.BoFormItemTypes.it_FOLDER)
            oItem = objform.Items.Item("3")
            objItem.Height = oItem.Height
            objItem.Left = oItem.Left + 5
            objItem.Width = oItem.Width
            oItem = objform.Items.Item("44")
            objItem.Top = oItem.Top
            objItem.FromPane = "103"
            objItem.ToPane = "103"
            objItem.AffectsFormMode = False

            oFolder = objItem.Specific
            oFolder.Caption = "Loan Subsidiary 1"
            oFolder.Pane = "670"

            oFolder.Select()

            oFolder.DataBind.SetBound(True, "", "FolderDS")

            'Loan Subsidyy 2
            objItem = objform.Items.Add("tcLnSub2", SAPbouiCOM.BoFormItemTypes.it_FOLDER)
            oItem = objform.Items.Item("tcLnSub1")
            objItem.Height = oItem.Height
            objItem.Left = oItem.Left + oItem.Width + 5
            objItem.Width = oItem.Width
            objItem.Top = oItem.Top
            objItem.FromPane = "103"
            objItem.ToPane = "103"
            objItem.AffectsFormMode = False

            oFolder = objItem.Specific
            oFolder.Caption = "Loan Subsidiary 2"
            oFolder.Pane = "671"
            oFolder.GroupWith("tcLnSub1")

            oFolder.DataBind.SetBound(True, "", "FolderDS")

            'Loan Subsidyy 3
            objItem = objform.Items.Add("tcLnSub3", SAPbouiCOM.BoFormItemTypes.it_FOLDER)
            oItem = objform.Items.Item("tcLnSub2")
            objItem.Height = oItem.Height
            objItem.Left = oItem.Left + oItem.Width + 5
            objItem.Width = oItem.Width
            objItem.Top = oItem.Top
            objItem.FromPane = "103"
            objItem.ToPane = "103"
            objItem.AffectsFormMode = False

            oFolder = objItem.Specific
            oFolder.Caption = "Loan Subsidiary 3"
            oFolder.Pane = "672"
            oFolder.GroupWith("tcLnSub1")

            oFolder.DataBind.SetBound(True, "", "FolderDS")

            'Secondary Facility 1
            objItem = objform.Items.Add("LnS1Itm_1", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            oItem = objform.Items.Item("tcLnSub1")
            objItem.Top = oItem.Top + oItem.Height + 5
            oItem = objform.Items.Item("3")
            objItem.Width = 120
            oItem = objform.Items.Item("3")
            objItem.Left = oItem.Left + 5
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oStatic = objItem.Specific
            oStatic.Caption = "Secondary Facility 1"

            'Type of loan
            oItem = objform.Items.Item("41")
            objItem = objform.Items.Add("LnS1Itm_2", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            oItem = objform.Items.Item("LnS1Itm_1")
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.DisplayDesc = True

            oComboBox = objItem.Specific
            oComboBox.DataBind.SetBound(True, "OCRD", "U_LOAN_PRNCP_TYPE")

            oItem = objform.Items.Item("LnS1Itm_1")
            objItem = objform.Items.Add("LnS1Itm_3", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_2"

            oStatic = objItem.Specific
            oStatic.Caption = "Type of loan"

            'Type of Facility
            oItem = objform.Items.Item("LnS1Itm_2")
            objItem = objform.Items.Add("LnS1Itm_4", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oComboBox = objItem.Specific
            oComboBox.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB1_FTYPE")

            oItem = objform.Items.Item("LnS1Itm_3")
            objItem = objform.Items.Add("LnS1Itm_5", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_4"

            oStatic = objItem.Specific
            oStatic.Caption = "Type of Facility"

            'Facilities Amount
            oItem = objform.Items.Item("LnS1Itm_4")
            objItem = objform.Items.Add("LnS1Itm_6", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB1_FAMT")

            oItem = objform.Items.Item("LnS1Itm_5")
            objItem = objform.Items.Add("LnS1Itm_7", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_6"

            oStatic = objItem.Specific
            oStatic.Caption = "Facilities Amount"

            'Repayment Period (Mths)
            oItem = objform.Items.Item("LnS1Itm_6")
            objItem = objform.Items.Add("LnS1Itm_8", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB1_RPYMTPD")

            oItem = objform.Items.Item("LnS1Itm_7")
            objItem = objform.Items.Add("LnS1Itm_9", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_8"

            oStatic = objItem.Specific
            oStatic.Caption = "Repayment Period (Mths)"

            'Interest Rate
            oItem = objform.Items.Item("LnS1Itm_8")
            objItem = objform.Items.Add("LnS1Itm_10", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB1_INT_RT")

            oItem = objform.Items.Item("LnS1Itm_9")
            objItem = objform.Items.Add("LnS1Itm_11", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_10"

            oStatic = objItem.Specific
            oStatic.Caption = "Interest Rate"

            'Monthly Installment
            oItem = objform.Items.Item("LnS1Itm_10")
            objItem = objform.Items.Add("LnS1Itm_12", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB1_MTHINST")

            oItem = objform.Items.Item("LnS1Itm_11")
            objItem = objform.Items.Add("LnS1Itm_13", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_12"

            oStatic = objItem.Specific
            oStatic.Caption = "Monthly Installment"

            'Term Loan Amt
            oItem = objform.Items.Item("LnS1Itm_12")
            objItem = objform.Items.Add("LnS1Itm_14", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB1_TRMLOAN")

            oItem = objform.Items.Item("LnS1Itm_13")
            objItem = objform.Items.Add("LnS1Itm_15", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_14"

            oStatic = objItem.Specific
            oStatic.Caption = "Term Loan Amt"

            'Interest
            oItem = objform.Items.Item("LnS1Itm_14")
            objItem = objform.Items.Add("LnS1Itm_16", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB1_INT")

            oItem = objform.Items.Item("LnS1Itm_15")
            objItem = objform.Items.Add("LnS1Itm_17", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_16"

            oStatic = objItem.Specific
            oStatic.Caption = "Interest"

            'OD Loan
            oItem = objform.Items.Item("LnS1Itm_16")
            objItem = objform.Items.Add("LnS1Itm_18", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB1_ODLOAN")

            oItem = objform.Items.Item("LnS1Itm_17")
            objItem = objform.Items.Add("LnS1Itm_19", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_18"

            oStatic = objItem.Specific
            oStatic.Caption = "OD Loan"

            'MRTA (Insurance)
            oItem = objform.Items.Item("LnS1Itm_18")
            objItem = objform.Items.Add("LnS1Itm_20", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB1_MRTA")

            oItem = objform.Items.Item("LnS1Itm_19")
            objItem = objform.Items.Add("LnS1Itm_21", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_20"

            oStatic = objItem.Specific
            oStatic.Caption = "MRTA (Insurance)"

            'Bank Guarentee
            oItem = objform.Items.Item("LnS1Itm_20")
            objItem = objform.Items.Add("LnS1Itm_22", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB1_BNKGNTE")

            oItem = objform.Items.Item("LnS1Itm_21")
            objItem = objform.Items.Add("LnS1Itm_23", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_22"

            oStatic = objItem.Specific
            oStatic.Caption = "Bank Guarentee"

            'Letter of Credit
            oItem = objform.Items.Item("LnS1Itm_22")
            objItem = objform.Items.Add("LnS1Itm_24", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB1_LC")

            oItem = objform.Items.Item("LnS1Itm_23")
            objItem = objform.Items.Add("LnS1Itm_25", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_24"

            oStatic = objItem.Specific
            oStatic.Caption = "Letter of Credit"

            'Trust Receipt
            oItem = objform.Items.Item("LnS1Itm_24")
            objItem = objform.Items.Add("LnS1Itm_26", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB1_TRUSTRC")

            oItem = objform.Items.Item("LnS1Itm_25")
            objItem = objform.Items.Add("LnS1Itm_27", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_26"

            oStatic = objItem.Specific
            oStatic.Caption = "Trust Receipt"

            'Others
            oItem = objform.Items.Item("LnS1Itm_26")
            objItem = objform.Items.Add("LnS1Itm_28", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB1_OTHR")

            oItem = objform.Items.Item("LnS1Itm_27")
            objItem = objform.Items.Add("LnS1Itm_29", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_28"

            oStatic = objItem.Specific
            oStatic.Caption = "Others"

            'Total
            oItem = objform.Items.Item("LnS1Itm_28")
            objItem = objform.Items.Add("LnS1Itm_30", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB1_TOTAL")

            oItem = objform.Items.Item("LnS1Itm_29")
            objItem = objform.Items.Add("LnS1Itm_31", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_30"

            oStatic = objItem.Specific
            oStatic.Caption = "Total"

            'Secondary Facility 2
            oItem = objform.Items.Item("LnS1Itm_1")
            objItem = objform.Items.Add("LnS1Itm_32", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Top = oItem.Top
            objItem.Width = 120
            oItem = objform.Items.Item("BPHItm_18")
            objItem.Left = oItem.Left
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oStatic = objItem.Specific
            oStatic.Caption = "Secondary Facility 2"

            'Type of Loan
            oItem = objform.Items.Item("BPHItm_17")
            objItem = objform.Items.Add("LnS1Itm_34", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left + 30
            oItem = objform.Items.Item("LnS1Itm_32")
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.DisplayDesc = True

            oComboBox = objItem.Specific
            oComboBox.DataBind.SetBound(True, "OCRD", "U_LOAN_PRNCP_TYPE")

            oItem = objform.Items.Item("LnS1Itm_32")
            objItem = objform.Items.Add("LnS1Itm_33", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_34"

            oStatic = objItem.Specific
            oStatic.Caption = "Type of loan"

            'Type of Facility
            oItem = objform.Items.Item("LnS1Itm_34")
            objItem = objform.Items.Add("LnS1Itm_36", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB2_FTYPE")

            oItem = objform.Items.Item("LnS1Itm_33")
            objItem = objform.Items.Add("LnS1Itm_35", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_36"

            oStatic = objItem.Specific
            oStatic.Caption = "Type of Facility"

            'Facilities Amount
            oItem = objform.Items.Item("LnS1Itm_36")
            objItem = objform.Items.Add("LnS1Itm_38", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB2_FAMT")

            oItem = objform.Items.Item("LnS1Itm_35")
            objItem = objform.Items.Add("LnS1Itm_37", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_38"

            oStatic = objItem.Specific
            oStatic.Caption = "Facilities Amount"

            'Repayment Period (Mths)
            oItem = objform.Items.Item("LnS1Itm_38")
            objItem = objform.Items.Add("LnS1Itm_40", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB2_RPYMTPD")

            oItem = objform.Items.Item("LnS1Itm_37")
            objItem = objform.Items.Add("LnS1Itm_39", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_40"

            oStatic = objItem.Specific
            oStatic.Caption = "Repayment Period (Mths)"

            'Interest Rate
            oItem = objform.Items.Item("LnS1Itm_40")
            objItem = objform.Items.Add("LnS1Itm_42", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB2_INT_RT")

            oItem = objform.Items.Item("LnS1Itm_39")
            objItem = objform.Items.Add("LnS1Itm_41", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_42"

            oStatic = objItem.Specific
            oStatic.Caption = "Interest Rate"

            'Monthly Installment
            oItem = objform.Items.Item("LnS1Itm_42")
            objItem = objform.Items.Add("LnS1Itm_44", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB2_MTHINST")

            oItem = objform.Items.Item("LnS1Itm_41")
            objItem = objform.Items.Add("LnS1Itm_43", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_44"

            oStatic = objItem.Specific
            oStatic.Caption = "Monthly Installment"

            'Term Loan Amt
            oItem = objform.Items.Item("LnS1Itm_44")
            objItem = objform.Items.Add("LnS1Itm_46", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB2_TRMLOAN")

            oItem = objform.Items.Item("LnS1Itm_43")
            objItem = objform.Items.Add("LnS1Itm_45", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_46"

            oStatic = objItem.Specific
            oStatic.Caption = "Term Loan Amt"

            'Interest
            oItem = objform.Items.Item("LnS1Itm_46")
            objItem = objform.Items.Add("LnS1Itm_48", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB2_INT")

            oItem = objform.Items.Item("LnS1Itm_45")
            objItem = objform.Items.Add("LnS1Itm_47", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_48"

            oStatic = objItem.Specific
            oStatic.Caption = "Interest"

            'OD Loan
            oItem = objform.Items.Item("LnS1Itm_48")
            objItem = objform.Items.Add("LnS1Itm_50", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB2_ODLOAN")

            oItem = objform.Items.Item("LnS1Itm_47")
            objItem = objform.Items.Add("LnS1Itm_49", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_50"

            oStatic = objItem.Specific
            oStatic.Caption = "OD Loan"

            'MRTA (Insurance)
            oItem = objform.Items.Item("LnS1Itm_50")
            objItem = objform.Items.Add("LnS1Itm_52", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB2_MRTA")

            oItem = objform.Items.Item("LnS1Itm_49")
            objItem = objform.Items.Add("LnS1Itm_51", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_52"

            oStatic = objItem.Specific
            oStatic.Caption = "MRTA (Insurance)"

            'Bank Guarentee
            oItem = objform.Items.Item("LnS1Itm_52")
            objItem = objform.Items.Add("LnS1Itm_54", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB2_BNKGNTE")

            oItem = objform.Items.Item("LnS1Itm_51")
            objItem = objform.Items.Add("LnS1Itm_53", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_54"

            oStatic = objItem.Specific
            oStatic.Caption = "Bank Guarentee"

            'Letter of Credit
            oItem = objform.Items.Item("LnS1Itm_54")
            objItem = objform.Items.Add("LnS1Itm_56", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB2_LC")

            oItem = objform.Items.Item("LnS1Itm_53")
            objItem = objform.Items.Add("LnS1Itm_55", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_56"

            oStatic = objItem.Specific
            oStatic.Caption = "Letter of Credit"

            'Trust Receipt
            oItem = objform.Items.Item("LnS1Itm_56")
            objItem = objform.Items.Add("LnS1Itm_58", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB2_TRUSTRC")

            oItem = objform.Items.Item("LnS1Itm_55")
            objItem = objform.Items.Add("LnS1Itm_57", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_58"

            oStatic = objItem.Specific
            oStatic.Caption = "Trust Receipt"

            'Others
            oItem = objform.Items.Item("LnS1Itm_58")
            objItem = objform.Items.Add("LnS1Itm_60", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB2_OTHR")

            oItem = objform.Items.Item("LnS1Itm_57")
            objItem = objform.Items.Add("LnS1Itm_59", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_60"

            oStatic = objItem.Specific
            oStatic.Caption = "Others"

            'Total
            oItem = objform.Items.Item("LnS1Itm_60")
            objItem = objform.Items.Add("LnS1Itm_62", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "670"
            objItem.ToPane = "670"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB2_TOTAL")

            oItem = objform.Items.Item("LnS1Itm_59")
            objItem = objform.Items.Add("LnS1Itm_61", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "670"
            objItem.ToPane = "670"
            objItem.LinkTo = "LnS1Itm_62"

            oStatic = objItem.Specific
            oStatic.Caption = "Total"

        Catch ex As Exception
            sErrDesc = ex.Message
            Call WriteToLogFile_Debug(sErrDesc, sFuncName)
        End Try
    End Sub

    Private Sub LoadLoanSubsidy2(ByRef objform As SAPbouiCOM.Form, ByRef sErrDesc As String)
        Dim sFuncName As String = "LoadLoanSubsidy2"

        Try
            'Secondary Facility 3
            objItem = objform.Items.Add("LnS2Itm_1", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            oItem = objform.Items.Item("tcLnSub1")
            objItem.Top = oItem.Top + oItem.Height + 5
            oItem = objform.Items.Item("3")
            objItem.Width = 120
            oItem = objform.Items.Item("3")
            objItem.Left = oItem.Left + 5
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oStatic = objItem.Specific
            oStatic.Caption = "Secondary Facility 3"

            'Type of loan
            oItem = objform.Items.Item("41")
            objItem = objform.Items.Add("LnS2Itm_2", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            oItem = objform.Items.Item("LnS2Itm_1")
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.DisplayDesc = True

            oComboBox = objItem.Specific
            oComboBox.DataBind.SetBound(True, "OCRD", "U_LOAN_PRNCP_TYPE")

            oItem = objform.Items.Item("LnS2Itm_1")
            objItem = objform.Items.Add("LnS2Itm_3", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_2"

            oStatic = objItem.Specific
            oStatic.Caption = "Type of loan"

            'Type of Facility
            oItem = objform.Items.Item("LnS2Itm_2")
            objItem = objform.Items.Add("LnS2Itm_4", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB3_FTYPE")

            oItem = objform.Items.Item("LnS2Itm_3")
            objItem = objform.Items.Add("LnS2Itm_5", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_4"

            oStatic = objItem.Specific
            oStatic.Caption = "Type of Facility"

            'Facilities Amount
            oItem = objform.Items.Item("LnS2Itm_4")
            objItem = objform.Items.Add("LnS2Itm_6", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB3_FAMT")

            oItem = objform.Items.Item("LnS2Itm_5")
            objItem = objform.Items.Add("LnS2Itm_7", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_6"

            oStatic = objItem.Specific
            oStatic.Caption = "Facilities Amount"

            'Repayment Period (Mths)
            oItem = objform.Items.Item("LnS2Itm_6")
            objItem = objform.Items.Add("LnS2Itm_8", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB3_RPYMTPD")

            oItem = objform.Items.Item("LnS2Itm_7")
            objItem = objform.Items.Add("LnS2Itm_9", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_8"

            oStatic = objItem.Specific
            oStatic.Caption = "Repayment Period (Mths)"

            'Interest Rate
            oItem = objform.Items.Item("LnS2Itm_8")
            objItem = objform.Items.Add("LnS2Itm_10", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB3_INT_RT")

            oItem = objform.Items.Item("LnS2Itm_9")
            objItem = objform.Items.Add("LnS2Itm_11", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_10"

            oStatic = objItem.Specific
            oStatic.Caption = "Interest Rate"

            'Monthly Installment
            oItem = objform.Items.Item("LnS2Itm_10")
            objItem = objform.Items.Add("LnS2Itm_12", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB3_MTHINST")

            oItem = objform.Items.Item("LnS2Itm_11")
            objItem = objform.Items.Add("LnS2Itm_13", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_12"

            oStatic = objItem.Specific
            oStatic.Caption = "Monthly Installment"

            'Term Loan Amt
            oItem = objform.Items.Item("LnS2Itm_12")
            objItem = objform.Items.Add("LnS2Itm_14", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB3_TRMLOAN")

            oItem = objform.Items.Item("LnS2Itm_13")
            objItem = objform.Items.Add("LnS2Itm_15", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_14"

            oStatic = objItem.Specific
            oStatic.Caption = "Term Loan Amt"

            'Interest
            oItem = objform.Items.Item("LnS2Itm_14")
            objItem = objform.Items.Add("LnS2Itm_16", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB3_INT")

            oItem = objform.Items.Item("LnS2Itm_15")
            objItem = objform.Items.Add("LnS2Itm_17", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_16"

            oStatic = objItem.Specific
            oStatic.Caption = "Interest"

            'OD Loan
            oItem = objform.Items.Item("LnS2Itm_16")
            objItem = objform.Items.Add("LnS2Itm_18", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB3_ODLOAN")

            oItem = objform.Items.Item("LnS2Itm_17")
            objItem = objform.Items.Add("LnS2Itm_19", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_18"

            oStatic = objItem.Specific
            oStatic.Caption = "OD Loan"

            'MRTA (Insurance)
            oItem = objform.Items.Item("LnS2Itm_18")
            objItem = objform.Items.Add("LnS2Itm_20", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB3_MRTA")

            oItem = objform.Items.Item("LnS2Itm_19")
            objItem = objform.Items.Add("LnS2Itm_21", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_20"

            oStatic = objItem.Specific
            oStatic.Caption = "MRTA (Insurance)"

            'Bank Guarentee
            oItem = objform.Items.Item("LnS2Itm_20")
            objItem = objform.Items.Add("LnS2Itm_22", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB3_BNKGNTE")

            oItem = objform.Items.Item("LnS2Itm_21")
            objItem = objform.Items.Add("LnS2Itm_23", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_22"

            oStatic = objItem.Specific
            oStatic.Caption = "Bank Guarentee"

            'Letter of Credit
            oItem = objform.Items.Item("LnS2Itm_22")
            objItem = objform.Items.Add("LnS2Itm_24", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB3_LC")

            oItem = objform.Items.Item("LnS2Itm_23")
            objItem = objform.Items.Add("LnS2Itm_25", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_24"

            oStatic = objItem.Specific
            oStatic.Caption = "Letter of Credit"

            'Trust Receipt
            oItem = objform.Items.Item("LnS2Itm_24")
            objItem = objform.Items.Add("LnS2Itm_26", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB3_TRUSTRC")

            oItem = objform.Items.Item("LnS2Itm_25")
            objItem = objform.Items.Add("LnS2Itm_27", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_26"

            oStatic = objItem.Specific
            oStatic.Caption = "Trust Receipt"

            'Others
            oItem = objform.Items.Item("LnS2Itm_26")
            objItem = objform.Items.Add("LnS2Itm_28", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB3_OTHR")

            oItem = objform.Items.Item("LnS2Itm_27")
            objItem = objform.Items.Add("LnS2Itm_29", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_28"

            oStatic = objItem.Specific
            oStatic.Caption = "Others"

            'Total
            oItem = objform.Items.Item("LnS2Itm_28")
            objItem = objform.Items.Add("LnS2Itm_30", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB3_TOTAL")

            oItem = objform.Items.Item("LnS2Itm_29")
            objItem = objform.Items.Add("LnS2Itm_31", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_30"

            oStatic = objItem.Specific
            oStatic.Caption = "Total"

            'Secondary Facility 4
            objItem = objform.Items.Add("LnS2Itm_32", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            oItem = objform.Items.Item("LnS2Itm_1")
            objItem.Top = oItem.Top
            objItem.Width = 120
            oItem = objform.Items.Item("BPHItm_18")
            objItem.Left = oItem.Left
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oStatic = objItem.Specific
            oStatic.Caption = "Secondary Facility 4"

            'Type of Loan
            oItem = objform.Items.Item("BPHItm_17")
            objItem = objform.Items.Add("LnS2Itm_34", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left + 30
            oItem = objform.Items.Item("LnS2Itm_32")
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.DisplayDesc = True

            oComboBox = objItem.Specific
            oComboBox.DataBind.SetBound(True, "OCRD", "U_LOAN_PRNCP_TYPE")

            oItem = objform.Items.Item("LnS2Itm_32")
            objItem = objform.Items.Add("LnS2Itm_33", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_34"

            oStatic = objItem.Specific
            oStatic.Caption = "Type of loan"

            'Type of Facility
            oItem = objform.Items.Item("LnS2Itm_34")
            objItem = objform.Items.Add("LnS2Itm_36", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB4_FTYPE")

            oItem = objform.Items.Item("LnS2Itm_33")
            objItem = objform.Items.Add("LnS2Itm_35", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_36"

            oStatic = objItem.Specific
            oStatic.Caption = "Type of Facility"

            'Facilities Amount
            oItem = objform.Items.Item("LnS2Itm_36")
            objItem = objform.Items.Add("LnS2Itm_38", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB4_FAMT")

            oItem = objform.Items.Item("LnS2Itm_35")
            objItem = objform.Items.Add("LnS2Itm_37", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_38"

            oStatic = objItem.Specific
            oStatic.Caption = "Facilities Amount"

            'Repayment Period (Mths)
            oItem = objform.Items.Item("LnS2Itm_38")
            objItem = objform.Items.Add("LnS2Itm_40", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB4_RPYMTPD")

            oItem = objform.Items.Item("LnS2Itm_37")
            objItem = objform.Items.Add("LnS2Itm_39", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_40"

            oStatic = objItem.Specific
            oStatic.Caption = "Repayment Period (Mths)"

            'Interest Rate
            oItem = objform.Items.Item("LnS2Itm_40")
            objItem = objform.Items.Add("LnS2Itm_42", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB4_INT_RT")

            oItem = objform.Items.Item("LnS2Itm_39")
            objItem = objform.Items.Add("LnS2Itm_41", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_42"

            oStatic = objItem.Specific
            oStatic.Caption = "Interest Rate"

            'Monthly Installment
            oItem = objform.Items.Item("LnS2Itm_42")
            objItem = objform.Items.Add("LnS2Itm_44", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB4_MTHINST")

            oItem = objform.Items.Item("LnS2Itm_41")
            objItem = objform.Items.Add("LnS2Itm_43", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_44"

            oStatic = objItem.Specific
            oStatic.Caption = "Monthly Installment"

            'Term Loan Amt
            oItem = objform.Items.Item("LnS2Itm_44")
            objItem = objform.Items.Add("LnS2Itm_46", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB4_TRMLOAN")

            oItem = objform.Items.Item("LnS2Itm_43")
            objItem = objform.Items.Add("LnS2Itm_45", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_46"

            oStatic = objItem.Specific
            oStatic.Caption = "Term Loan Amt"

            'Interest
            oItem = objform.Items.Item("LnS2Itm_46")
            objItem = objform.Items.Add("LnS2Itm_48", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB4_INT")

            oItem = objform.Items.Item("LnS2Itm_45")
            objItem = objform.Items.Add("LnS2Itm_47", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_48"

            oStatic = objItem.Specific
            oStatic.Caption = "Interest"

            'OD Loan
            oItem = objform.Items.Item("LnS2Itm_48")
            objItem = objform.Items.Add("LnS2Itm_50", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB4_ODLOAN")

            oItem = objform.Items.Item("LnS2Itm_47")
            objItem = objform.Items.Add("LnS2Itm_49", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_50"

            oStatic = objItem.Specific
            oStatic.Caption = "OD Loan"

            'MRTA (Insurance)
            oItem = objform.Items.Item("LnS2Itm_50")
            objItem = objform.Items.Add("LnS2Itm_52", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB4_MRTA")

            oItem = objform.Items.Item("LnS2Itm_49")
            objItem = objform.Items.Add("LnS2Itm_51", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_52"

            oStatic = objItem.Specific
            oStatic.Caption = "MRTA (Insurance)"

            'Bank Guarentee
            oItem = objform.Items.Item("LnS2Itm_52")
            objItem = objform.Items.Add("LnS2Itm_54", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB4_BNKGNTE")

            oItem = objform.Items.Item("LnS2Itm_51")
            objItem = objform.Items.Add("LnS2Itm_53", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_54"

            oStatic = objItem.Specific
            oStatic.Caption = "Bank Guarentee"

            'Letter of Credit
            oItem = objform.Items.Item("LnS2Itm_54")
            objItem = objform.Items.Add("LnS2Itm_56", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB4_LC")

            oItem = objform.Items.Item("LnS2Itm_53")
            objItem = objform.Items.Add("LnS2Itm_55", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_56"

            oStatic = objItem.Specific
            oStatic.Caption = "Letter of Credit"

            'Trust Receipt
            oItem = objform.Items.Item("LnS2Itm_56")
            objItem = objform.Items.Add("LnS2Itm_58", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB4_TRUSTRC")

            oItem = objform.Items.Item("LnS2Itm_55")
            objItem = objform.Items.Add("LnS2Itm_57", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_58"

            oStatic = objItem.Specific
            oStatic.Caption = "Trust Receipt"

            'Others
            oItem = objform.Items.Item("LnS2Itm_58")
            objItem = objform.Items.Add("LnS2Itm_60", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB4_OTHR")

            oItem = objform.Items.Item("LnS2Itm_57")
            objItem = objform.Items.Add("LnS2Itm_59", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_60"

            oStatic = objItem.Specific
            oStatic.Caption = "Others"

            'Total
            oItem = objform.Items.Item("LnS2Itm_60")
            objItem = objform.Items.Add("LnS2Itm_62", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "671"
            objItem.ToPane = "671"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB4_TOTAL")

            oItem = objform.Items.Item("LnS2Itm_59")
            objItem = objform.Items.Add("LnS2Itm_61", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "671"
            objItem.ToPane = "671"
            objItem.LinkTo = "LnS2Itm_62"

            oStatic = objItem.Specific
            oStatic.Caption = "Total"

        Catch ex As Exception
            sErrDesc = ex.Message
            Call WriteToLogFile_Debug(sErrDesc, sFuncName)
        End Try
    End Sub

    Private Sub LoadLoanSubsidy3(ByRef objform As SAPbouiCOM.Form, ByRef sErrDesc As String)
        Dim sFuncName As String = "LoadLoanSubsidy3"

        Try
            'Secondary Facility 5
            objItem = objform.Items.Add("LnS3Itm_1", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            oItem = objform.Items.Item("tcLnSub1")
            objItem.Top = oItem.Top + oItem.Height + 5
            objItem.Width = 120
            oItem = objform.Items.Item("3")
            objItem.Left = oItem.Left + 5
            objItem.FromPane = "672"
            objItem.ToPane = "672"

            oStatic = objItem.Specific
            oStatic.Caption = "Secondary Facility 5"

            'Type of loan
            oItem = objform.Items.Item("41")
            objItem = objform.Items.Add("LnS3Itm_2", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            oItem = objform.Items.Item("LnS3Itm_1")
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "672"
            objItem.ToPane = "672"
            objItem.DisplayDesc = True

            oComboBox = objItem.Specific
            oComboBox.DataBind.SetBound(True, "OCRD", "U_LOAN_PRNCP_TYPE")

            oItem = objform.Items.Item("LnS3Itm_1")
            objItem = objform.Items.Add("LnS3Itm_3", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "672"
            objItem.ToPane = "672"
            objItem.LinkTo = "LnS3Itm_2"

            oStatic = objItem.Specific
            oStatic.Caption = "Type of loan"

            'Type of Facility
            oItem = objform.Items.Item("LnS3Itm_2")
            objItem = objform.Items.Add("LnS3Itm_4", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "672"
            objItem.ToPane = "672"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB5_FTYPE")

            oItem = objform.Items.Item("LnS3Itm_3")
            objItem = objform.Items.Add("LnS3Itm_5", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "672"
            objItem.ToPane = "672"
            objItem.LinkTo = "LnS3Itm_4"

            oStatic = objItem.Specific
            oStatic.Caption = "Type of Facility"

            'Facilities Amount
            oItem = objform.Items.Item("LnS3Itm_4")
            objItem = objform.Items.Add("LnS3Itm_6", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "672"
            objItem.ToPane = "672"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB5_FAMT")

            oItem = objform.Items.Item("LnS3Itm_5")
            objItem = objform.Items.Add("LnS3Itm_7", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "672"
            objItem.ToPane = "672"
            objItem.LinkTo = "LnS3Itm_6"

            oStatic = objItem.Specific
            oStatic.Caption = "Facilities Amount"

            'Repayment Period (Mths)
            oItem = objform.Items.Item("LnS3Itm_6")
            objItem = objform.Items.Add("LnS3Itm_8", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "672"
            objItem.ToPane = "672"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB5_RPYMTPD")

            oItem = objform.Items.Item("LnS3Itm_7")
            objItem = objform.Items.Add("LnS3Itm_9", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "672"
            objItem.ToPane = "672"
            objItem.LinkTo = "LnS3Itm_8"

            oStatic = objItem.Specific
            oStatic.Caption = "Repayment Period (Mths)"

            'Interest Rate
            oItem = objform.Items.Item("LnS3Itm_8")
            objItem = objform.Items.Add("LnS3Itm_10", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "672"
            objItem.ToPane = "672"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB5_INT_RT")

            oItem = objform.Items.Item("LnS3Itm_9")
            objItem = objform.Items.Add("LnS3Itm_11", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "672"
            objItem.ToPane = "672"
            objItem.LinkTo = "LnS3Itm_10"

            oStatic = objItem.Specific
            oStatic.Caption = "Interest Rate"

            'Monthly Installment
            oItem = objform.Items.Item("LnS3Itm_10")
            objItem = objform.Items.Add("LnS3Itm_12", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "672"
            objItem.ToPane = "672"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB5_MTHINST")

            oItem = objform.Items.Item("LnS3Itm_11")
            objItem = objform.Items.Add("LnS3Itm_13", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "672"
            objItem.ToPane = "672"
            objItem.LinkTo = "LnS3Itm_12"

            oStatic = objItem.Specific
            oStatic.Caption = "Monthly Installment"

            'Term Loan Amt
            oItem = objform.Items.Item("LnS3Itm_12")
            objItem = objform.Items.Add("LnS3Itm_14", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "672"
            objItem.ToPane = "672"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB5_TRMLOAN")

            oItem = objform.Items.Item("LnS3Itm_13")
            objItem = objform.Items.Add("LnS3Itm_15", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "672"
            objItem.ToPane = "672"
            objItem.LinkTo = "LnS3Itm_14"

            oStatic = objItem.Specific
            oStatic.Caption = "Term Loan Amt"

            'Interest
            oItem = objform.Items.Item("LnS3Itm_14")
            objItem = objform.Items.Add("LnS3Itm_16", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "672"
            objItem.ToPane = "672"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB5_INT")

            oItem = objform.Items.Item("LnS3Itm_15")
            objItem = objform.Items.Add("LnS3Itm_17", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "672"
            objItem.ToPane = "672"
            objItem.LinkTo = "LnS3Itm_16"

            oStatic = objItem.Specific
            oStatic.Caption = "Interest"

            'OD Loan
            oItem = objform.Items.Item("LnS3Itm_16")
            objItem = objform.Items.Add("LnS3Itm_18", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "672"
            objItem.ToPane = "672"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB5_ODLOAN")

            oItem = objform.Items.Item("LnS3Itm_17")
            objItem = objform.Items.Add("LnS3Itm_19", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "672"
            objItem.ToPane = "672"
            objItem.LinkTo = "LnS3Itm_18"

            oStatic = objItem.Specific
            oStatic.Caption = "OD Loan"

            'MRTA (Insurance)
            oItem = objform.Items.Item("LnS3Itm_18")
            objItem = objform.Items.Add("LnS3Itm_20", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "672"
            objItem.ToPane = "672"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB5_MRTA")

            oItem = objform.Items.Item("LnS3Itm_19")
            objItem = objform.Items.Add("LnS3Itm_21", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "672"
            objItem.ToPane = "672"
            objItem.LinkTo = "LnS3Itm_20"

            oStatic = objItem.Specific
            oStatic.Caption = "MRTA (Insurance)"

            'Bank Guarentee
            oItem = objform.Items.Item("LnS3Itm_20")
            objItem = objform.Items.Add("LnS3Itm_22", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "672"
            objItem.ToPane = "672"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB5_BNKGNTE")

            oItem = objform.Items.Item("LnS3Itm_21")
            objItem = objform.Items.Add("LnS3Itm_23", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "672"
            objItem.ToPane = "672"
            objItem.LinkTo = "LnS3Itm_22"

            oStatic = objItem.Specific
            oStatic.Caption = "Bank Guarentee"

            'Letter of Credit
            oItem = objform.Items.Item("LnS3Itm_22")
            objItem = objform.Items.Add("LnS3Itm_24", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "672"
            objItem.ToPane = "672"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB5_LC")

            oItem = objform.Items.Item("LnS3Itm_23")
            objItem = objform.Items.Add("LnS3Itm_25", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "672"
            objItem.ToPane = "672"
            objItem.LinkTo = "LnS3Itm_24"

            oStatic = objItem.Specific
            oStatic.Caption = "Letter of Credit"

            'Trust Receipt
            oItem = objform.Items.Item("LnS3Itm_24")
            objItem = objform.Items.Add("LnS3Itm_26", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "672"
            objItem.ToPane = "672"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB5_TRUSTRC")

            oItem = objform.Items.Item("LnS3Itm_25")
            objItem = objform.Items.Add("LnS3Itm_27", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "672"
            objItem.ToPane = "672"
            objItem.LinkTo = "LnS3Itm_26"

            oStatic = objItem.Specific
            oStatic.Caption = "Trust Receipt"

            'Others
            oItem = objform.Items.Item("LnS3Itm_26")
            objItem = objform.Items.Add("LnS3Itm_28", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "672"
            objItem.ToPane = "672"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB5_OTHR")

            oItem = objform.Items.Item("LnS3Itm_27")
            objItem = objform.Items.Add("LnS3Itm_29", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "672"
            objItem.ToPane = "672"
            objItem.LinkTo = "LnS3Itm_28"

            oStatic = objItem.Specific
            oStatic.Caption = "Others"

            'Total
            oItem = objform.Items.Item("LnS3Itm_28")
            objItem = objform.Items.Add("LnS3Itm_30", SAPbouiCOM.BoFormItemTypes.it_EDIT)
            objItem.Width = oItem.Width
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.FromPane = "672"
            objItem.ToPane = "672"

            oEdit = objItem.Specific
            oEdit.DataBind.SetBound(True, "OCRD", "U_LOAN_SUB5_TOTAL")

            oItem = objform.Items.Item("LnS3Itm_29")
            objItem = objform.Items.Add("LnS3Itm_31", SAPbouiCOM.BoFormItemTypes.it_STATIC)
            objItem.Left = oItem.Left
            objItem.Top = oItem.Top + 15
            objItem.Width = 120
            objItem.FromPane = "672"
            objItem.ToPane = "672"
            objItem.LinkTo = "LnS3Itm_30"

            oStatic = objItem.Specific
            oStatic.Caption = "Total"

        Catch ex As Exception
            sErrDesc = ex.Message
            Call WriteToLogFile_Debug(sErrDesc, sFuncName)
        End Try
    End Sub

#End Region

    Private Function BP_ModifyBPItems(ByRef objForm As SAPbouiCOM.Form) As Long
        Dim sFuncName As String = "BP_ModifyBPItems"
        Dim sErrDesc As String
        Try
            objForm.Freeze(True)
            oComboBox = objForm.Items.Item("40").Specific
            If oComboBox.Selected.Value = "C" Then
                objForm.Items.Item("tcSPADt").Visible = True
                objForm.Items.Item("tcLnPrts").Visible = True
                objForm.Items.Item("tcCseFl").Visible = True
                objForm.Items.Item("tcLnPpl").Visible = True
                objForm.Items.Item("tcLnSub1").Visible = True
                objForm.Items.Item("tcLnSub2").Visible = True
                objForm.Items.Item("tcLnSub3").Visible = True
                objForm.Items.Item("3").Visible = False
                objForm.Items.Item("13").Visible = False
                objForm.Items.Item("15").Visible = False
                objForm.Items.Item("156").Visible = False
                objForm.Items.Item("214").Visible = False
                objForm.Items.Item("1320002081").Visible = False

                oItem = objForm.Items.Item("3")
                objItem = objForm.Items.Item("tcSPADt")
                objItem.Left = oItem.Left

                objForm.Items.Item("tcSPADt").Click(SAPbouiCOM.BoCellClickType.ct_Regular)

                'objForm.Update()
            ElseIf oComboBox.Selected.Value = "S" Then
                objForm.Items.Item("tcSPADt").Visible = False
                objForm.Items.Item("tcLnPrts").Visible = False
                objForm.Items.Item("tcCseFl").Visible = False
                objForm.Items.Item("tcLnPpl").Visible = False
                objForm.Items.Item("tcLnSub1").Visible = False
                objForm.Items.Item("tcLnSub2").Visible = False
                objForm.Items.Item("tcLnSub3").Visible = False
                objForm.Items.Item("3").Visible = True
                If objForm.Mode <> SAPbouiCOM.BoFormMode.fm_FIND_MODE Then
                    objForm.Items.Item("13").Visible = True
                    objForm.Items.Item("15").Visible = True
                    objForm.Items.Item("1320002081").Visible = True
                End If
                objForm.Items.Item("156").Visible = True
                objForm.Items.Item("214").Visible = True

                objForm.Items.Item("3").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                'objForm.Update()
            End If
            objForm.Freeze(False)

            BP_ModifyBPItems = RTN_SUCCESS
        Catch ex As Exception
            objForm.Freeze(False)
            sErrDesc = ex.Message
            Call WriteToLogFile_Debug(sErrDesc, sFuncName)
            BP_ModifyBPItems = RTN_ERROR
        End Try
    End Function

#Region "Disable BP Items"

    Private Sub DisableGeneraltabItems(ByRef objForm As SAPbouiCOM.Form)
        objForm.Items.Item("44").Visible = False
        objForm.Items.Item("43").Visible = False
        objForm.Items.Item("46").Visible = False
        objForm.Items.Item("45").Visible = False
        objForm.Items.Item("48").Visible = False
        objForm.Items.Item("51").Visible = False
        objForm.Items.Item("50").Visible = False
        objForm.Items.Item("47").Visible = False
        objForm.Items.Item("61").Visible = False
        objForm.Items.Item("60").Visible = False
        objForm.Items.Item("395").Visible = False
        objForm.Items.Item("394").Visible = False
        objForm.Items.Item("101").Visible = False
        objForm.Items.Item("102").Visible = False
        objForm.Items.Item("111").Visible = False
        objForm.Items.Item("184").Visible = False
        objForm.Items.Item("185").Visible = False
        objForm.Items.Item("22").Visible = False
        objForm.Items.Item("39").Visible = False
        objForm.Items.Item("49").Visible = False
        objForm.Items.Item("223").Visible = False
        objForm.Items.Item("222").Visible = False
        objForm.Items.Item("350001034").Visible = False
        objForm.Items.Item("350001035").Visible = False
        objForm.Items.Item("358").Visible = False
        objForm.Items.Item("362").Visible = False
        objForm.Items.Item("2013").Visible = False
        objForm.Items.Item("2014").Visible = False
        objForm.Items.Item("118").Visible = False
        objForm.Items.Item("117").Visible = False
        objForm.Items.Item("114").Visible = False
        objForm.Items.Item("113").Visible = False
        objForm.Items.Item("62").Visible = False
        objForm.Items.Item("73").Visible = False
        objForm.Items.Item("109").Visible = False
        objForm.Items.Item("108").Visible = False
        objForm.Items.Item("333").Visible = False
        objForm.Items.Item("335").Visible = False
        objForm.Items.Item("343").Visible = False
        objForm.Items.Item("345").Visible = False
        objForm.Items.Item("431").Visible = False
        objForm.Items.Item("432").Visible = False
        objForm.Items.Item("430").Visible = False
        objForm.Items.Item("1470002109").Visible = False
        objForm.Items.Item("1470002110").Visible = False

        objForm.Items.Item("59").Visible = False
        objForm.Items.Item("52").Visible = False
        objForm.Items.Item("53").Visible = False
        objForm.Items.Item("336").Visible = False
        objForm.Items.Item("338").Visible = False
        objForm.Items.Item("10002057").Visible = False
        objForm.Items.Item("10002058").Visible = False
        objForm.Items.Item("10002056").Visible = False
        objForm.Items.Item("10002055").Visible = False
        objForm.Items.Item("10002059").Visible = False
        objForm.Items.Item("10002060").Visible = False
    End Sub

    Private Sub DisableBPUDFFields(ByRef objForm As SAPbouiCOM.Form)
        objForm.Items.Item("13").Visible = False
        objForm.Items.Item("15").Visible = False
        objForm.Items.Item("156").Visible = False
        objForm.Items.Item("214").Visible = False
        objForm.Items.Item("9").Visible = False
        objForm.Items.Item("1320002081").Visible = False

        objForm.Items.Item("59").Visible = False
        objForm.Items.Item("52").Visible = False
        objForm.Items.Item("53").Visible = False
        objForm.Items.Item("333").Visible = False
        objForm.Items.Item("335").Visible = False
        objForm.Items.Item("336").Visible = False
        objForm.Items.Item("338").Visible = False
        objForm.Items.Item("343").Visible = False
        objForm.Items.Item("345").Visible = False
        objForm.Items.Item("10002057").Visible = False
        objForm.Items.Item("10002058").Visible = False
        objForm.Items.Item("10002056").Visible = False
        objForm.Items.Item("10002055").Visible = False
        objForm.Items.Item("10002059").Visible = False
        objForm.Items.Item("10002060").Visible = False

        objForm.Items.Item("item_11").Enabled = False
        objForm.Items.Item("item_21").Enabled = False
        objForm.Items.Item("item_29").Enabled = False
        objForm.Items.Item("item_37").Enabled = False
        objForm.Items.Item("item_47").Enabled = False
        objForm.Items.Item("item_57").Enabled = False
        objForm.Items.Item("item_67").Enabled = False
        objForm.Items.Item("item_77").Enabled = False

        objForm.Items.Item("item_92").Enabled = False
        objForm.Items.Item("item_102").Enabled = False
        objForm.Items.Item("item_110").Enabled = False
        objForm.Items.Item("item_118").Enabled = False
        objForm.Items.Item("item_128").Enabled = False
        objForm.Items.Item("item_138").Enabled = False
        objForm.Items.Item("item_148").Enabled = False
        objForm.Items.Item("item_158").Enabled = False

        objForm.Items.Item("LRPItm_11").Enabled = False
        objForm.Items.Item("LRPItm_21").Enabled = False
        objForm.Items.Item("LRPItm_29").Enabled = False
        objForm.Items.Item("LRPItm_37").Enabled = False
        objForm.Items.Item("LRPItm_47").Enabled = False
        objForm.Items.Item("LRPItm_57").Enabled = False
        objForm.Items.Item("LRPItm_67").Enabled = False
        objForm.Items.Item("LRPItm_77").Enabled = False

        objForm.Items.Item("LRPItm_90").Enabled = False
        objForm.Items.Item("LRPItm_100").Enabled = False
        objForm.Items.Item("LRPItm_108").Enabled = False
        objForm.Items.Item("LRPItm_116").Enabled = False
        objForm.Items.Item("LRPItm_124").Enabled = False
        objForm.Items.Item("LRPItm_134").Enabled = False
        objForm.Items.Item("LRPItm_144").Enabled = False
        objForm.Items.Item("LRPItm_154").Enabled = False

        '------ Property TAB
        objForm.Items.Item("CFPItm_4").Enabled = False
        objForm.Items.Item("CFPItm_7").Enabled = False
        objForm.Items.Item("CFPItm_8").Enabled = False
        objForm.Items.Item("CFPItm_10").Enabled = False
        objForm.Items.Item("CFPItm_12").Enabled = False
        objForm.Items.Item("CFPItm_14").Enabled = False
        objForm.Items.Item("CFPItm_16").Enabled = False
        objForm.Items.Item("CFPItm_18").Enabled = False

    End Sub

#End Region

#Region "Add Choose From List"

    Private Sub AddChooseFromList(ByRef objForm As SAPbouiCOM.Form)
        Try
            Dim oCFLs As SAPbouiCOM.ChooseFromListCollection
            oCFLs = objForm.ChooseFromLists
            Dim oCons As SAPbouiCOM.Conditions
            Dim oCon As SAPbouiCOM.Condition
            Dim oCFL As SAPbouiCOM.ChooseFromList
            Dim oCFLCreationParams As SAPbouiCOM.ChooseFromListCreationParams
            oCFLCreationParams = p_oSBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams)

            'Partners CFL
            oCFLCreationParams.MultiSelection = False
            oCFLCreationParams.ObjectType = "171"
            oCFLCreationParams.UniqueID = "CFL1"
            oCFL = oCFLs.Add(oCFLCreationParams)

            'LA CFL
            oCFLCreationParams.ObjectType = "171"
            oCFLCreationParams.UniqueID = "CFL2"
            oCFL = oCFLs.Add(oCFLCreationParams)

            'Manager CFL
            oCFLCreationParams.ObjectType = "171"
            oCFLCreationParams.UniqueID = "CFL3"
            oCFL = oCFLs.Add(oCFLCreationParams)

            'IN-Charge CFL
            oCFLCreationParams.ObjectType = "171"
            oCFLCreationParams.UniqueID = "CFL4"
            oCFL = oCFLs.Add(oCFLCreationParams)

            'Customer Service CFL
            oCFLCreationParams.ObjectType = "171"
            oCFLCreationParams.UniqueID = "CFL5"
            oCFL = oCFLs.Add(oCFLCreationParams)

            'SPA Related Parties Purchaser tab
            '1st Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL6"
            oCFL = oCFLs.Add(oCFLCreationParams)

            '2nd Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL7"
            oCFL = oCFLs.Add(oCFLCreationParams)

            '3rd Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL8"
            oCFL = oCFLs.Add(oCFLCreationParams)

            '4th Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL9"
            oCFL = oCFLs.Add(oCFLCreationParams)

            '1st Solicitor Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL10"
            oCFL = oCFLs.Add(oCFLCreationParams)
            oCons = oCFL.GetConditions()
            oCon = oCons.Add()
            oCon.Alias = "U_SOLICITOR"
            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
            oCon.CondVal = "Y"
            oCFL.SetConditions(oCons)

            '2nd Solicitor Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL11"
            oCFL = oCFLs.Add(oCFLCreationParams)
            oCons = oCFL.GetConditions()
            oCon = oCons.Add()
            oCon.Alias = "U_SOLICITOR"
            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
            oCon.CondVal = "Y"
            oCFL.SetConditions(oCons)

            '3rd Solicitor Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL12"
            oCFL = oCFLs.Add(oCFLCreationParams)
            oCons = oCFL.GetConditions()
            oCon = oCons.Add()
            oCon.Alias = "U_SOLICITOR"
            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
            oCon.CondVal = "Y"
            oCFL.SetConditions(oCons)

            '4th Solicitor Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL13"
            oCFL = oCFLs.Add(oCFLCreationParams)
            oCons = oCFL.GetConditions()
            oCon = oCons.Add()
            oCon.Alias = "U_SOLICITOR"
            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
            oCon.CondVal = "Y"
            oCFL.SetConditions(oCons)

            'SPA Related Parties Vendor tab
            '1st Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL14"
            oCFL = oCFLs.Add(oCFLCreationParams)

            '2nd Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL15"
            oCFL = oCFLs.Add(oCFLCreationParams)

            '3rd Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL16"
            oCFL = oCFLs.Add(oCFLCreationParams)

            '4th Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL17"
            oCFL = oCFLs.Add(oCFLCreationParams)

            '1st Solicitor Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL18"
            oCFL = oCFLs.Add(oCFLCreationParams)
            oCons = oCFL.GetConditions()
            oCon = oCons.Add()
            oCon.Alias = "U_SOLICITOR"
            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
            oCon.CondVal = "Y"
            oCFL.SetConditions(oCons)

            '2nd Solicitor Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL19"
            oCFL = oCFLs.Add(oCFLCreationParams)
            oCons = oCFL.GetConditions()
            oCon = oCons.Add()
            oCon.Alias = "U_SOLICITOR"
            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
            oCon.CondVal = "Y"
            oCFL.SetConditions(oCons)

            '3rd Solicitor Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL20"
            oCFL = oCFLs.Add(oCFLCreationParams)
            oCons = oCFL.GetConditions()
            oCon = oCons.Add()
            oCon.Alias = "U_SOLICITOR"
            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
            oCon.CondVal = "Y"
            oCFL.SetConditions(oCons)

            '4th Solicitor Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL21"
            oCFL = oCFLs.Add(oCFLCreationParams)
            oCons = oCFL.GetConditions()
            oCon = oCons.Add()
            oCon.Alias = "U_SOLICITOR"
            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
            oCon.CondVal = "Y"
            oCFL.SetConditions(oCons)

            'Loan Related Parties Borrower tab
            '1st Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL22"
            oCFL = oCFLs.Add(oCFLCreationParams)

            '2nd Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL23"
            oCFL = oCFLs.Add(oCFLCreationParams)

            '3rd Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL24"
            oCFL = oCFLs.Add(oCFLCreationParams)

            '4th Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL25"
            oCFL = oCFLs.Add(oCFLCreationParams)

            '1st Solicitor Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL26"
            oCFL = oCFLs.Add(oCFLCreationParams)
            oCons = oCFL.GetConditions()
            oCon = oCons.Add()
            oCon.Alias = "U_SOLICITOR"
            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
            oCon.CondVal = "Y"
            oCFL.SetConditions(oCons)

            '2nd Solicitor Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL27"
            oCFL = oCFLs.Add(oCFLCreationParams)
            oCons = oCFL.GetConditions()
            oCon = oCons.Add()
            oCon.Alias = "U_SOLICITOR"
            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
            oCon.CondVal = "Y"
            oCFL.SetConditions(oCons)

            '3rd Solicitor Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL28"
            oCFL = oCFLs.Add(oCFLCreationParams)
            oCons = oCFL.GetConditions()
            oCon = oCons.Add()
            oCon.Alias = "U_SOLICITOR"
            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
            oCon.CondVal = "Y"
            oCFL.SetConditions(oCons)

            '4th Solicitor Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL29"
            oCFL = oCFLs.Add(oCFLCreationParams)
            oCons = oCFL.GetConditions()
            oCon = oCons.Add()
            oCon.Alias = "U_SOLICITOR"
            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
            oCon.CondVal = "Y"
            oCFL.SetConditions(oCons)

            'Loan Related Parties Guarantor tab
            '1st Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL30"
            oCFL = oCFLs.Add(oCFLCreationParams)

            '2nd Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL31"
            oCFL = oCFLs.Add(oCFLCreationParams)

            '3rd Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL32"
            oCFL = oCFLs.Add(oCFLCreationParams)

            '4th Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL33"
            oCFL = oCFLs.Add(oCFLCreationParams)

            '1st Solicitor Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL34"
            oCFL = oCFLs.Add(oCFLCreationParams)
            oCons = oCFL.GetConditions()
            oCon = oCons.Add()
            oCon.Alias = "U_SOLICITOR"
            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
            oCon.CondVal = "Y"
            oCFL.SetConditions(oCons)

            '2nd Solicitor Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL35"
            oCFL = oCFLs.Add(oCFLCreationParams)
            oCons = oCFL.GetConditions()
            oCon = oCons.Add()
            oCon.Alias = "U_SOLICITOR"
            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
            oCon.CondVal = "Y"
            oCFL.SetConditions(oCons)

            '3rd Solicitor Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL36"
            oCFL = oCFLs.Add(oCFLCreationParams)
            oCons = oCFL.GetConditions()
            oCon = oCons.Add()
            oCon.Alias = "U_SOLICITOR"
            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
            oCon.CondVal = "Y"
            oCFL.SetConditions(oCons)

            '4th Solicitor Name Code
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL37"
            oCFL = oCFLs.Add(oCFLCreationParams)
            oCons = oCFL.GetConditions()
            oCon = oCons.Add()
            oCon.Alias = "U_SOLICITOR"
            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
            oCon.CondVal = "Y"
            oCFL.SetConditions(oCons)

            'Property tab
            'Title Sys ID
            oCFLCreationParams.ObjectType = "PROPERTY"
            oCFLCreationParams.UniqueID = "CFL38"
            oCFL = oCFLs.Add(oCFLCreationParams)

            'MASTER BANK NAME
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL39"
            oCFL = oCFLs.Add(oCFLCreationParams)
            oCons = oCFL.GetConditions()
            oCon = oCons.Add()
            oCon.Alias = "U_BANK"
            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
            oCon.CondVal = "Y"
            oCFL.SetConditions(oCons)

            'BANK SOLICITOR NAME
            oCFLCreationParams.ObjectType = "RELATEDPARTY"
            oCFLCreationParams.UniqueID = "CFL40"
            oCFL = oCFLs.Add(oCFLCreationParams)
            oCons = oCFL.GetConditions()
            oCon = oCons.Add()
            oCon.Alias = "U_SOLICITOR"
            oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
            oCon.CondVal = "Y"
            oCFL.SetConditions(oCons)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub CFLDataBinding(ByRef objForm As SAPbouiCOM.Form)
        'SPA Related Parties Purchaser tab
        '1st Name Code
        oEdit = objForm.Items.Item("item_9").Specific
        oEdit.ChooseFromListUID = "CFL6"
        oEdit.ChooseFromListAlias = "Code"

        '2nd Name Code
        oEdit = objForm.Items.Item("item_19").Specific
        oEdit.ChooseFromListUID = "CFL7"
        oEdit.ChooseFromListAlias = "Code"

        '3rd Name Code
        oEdit = objForm.Items.Item("item_27").Specific
        oEdit.ChooseFromListUID = "CFL8"
        oEdit.ChooseFromListAlias = "Code"

        '4th Name Code
        oEdit = objForm.Items.Item("item_35").Specific
        oEdit.ChooseFromListUID = "CFL9"
        oEdit.ChooseFromListAlias = "Code"

        '1st Solicitor Name Code
        oEdit = objForm.Items.Item("item_45").Specific
        oEdit.ChooseFromListUID = "CFL10"
        oEdit.ChooseFromListAlias = "Code"

        '2nd Solicitor Name Code 
        oEdit = objForm.Items.Item("item_55").Specific
        oEdit.ChooseFromListUID = "CFL11"
        oEdit.ChooseFromListAlias = "Code"

        '3rd Solicitor Name Code
        oEdit = objForm.Items.Item("item_65").Specific
        oEdit.ChooseFromListUID = "CFL12"
        oEdit.ChooseFromListAlias = "Code"

        '4th Solicitor Name Code 
        oEdit = objForm.Items.Item("item_75").Specific
        oEdit.ChooseFromListUID = "CFL13"
        oEdit.ChooseFromListAlias = "Code"

        'SPA Related Parties Vendor tab
        '1st Name Code
        oEdit = objForm.Items.Item("item_90").Specific
        oEdit.ChooseFromListUID = "CFL14"
        oEdit.ChooseFromListAlias = "Code"

        '2nd Name Code
        oEdit = objForm.Items.Item("item_100").Specific
        oEdit.ChooseFromListUID = "CFL15"
        oEdit.ChooseFromListAlias = "Code"

        '3rd Name Code
        oEdit = objForm.Items.Item("item_108").Specific
        oEdit.ChooseFromListUID = "CFL16"
        oEdit.ChooseFromListAlias = "Code"

        '4th Name Code
        oEdit = objForm.Items.Item("item_116").Specific
        oEdit.ChooseFromListUID = "CFL17"
        oEdit.ChooseFromListAlias = "Code"

        '1st Solicitor Name Code
        oEdit = objForm.Items.Item("item_126").Specific
        oEdit.ChooseFromListUID = "CFL18"
        oEdit.ChooseFromListAlias = "Code"

        '2nd Solicitor Name Code 
        oEdit = objForm.Items.Item("item_136").Specific
        oEdit.ChooseFromListUID = "CFL19"
        oEdit.ChooseFromListAlias = "Code"

        '3rd Solicitor Name Code
        oEdit = objForm.Items.Item("item_146").Specific
        oEdit.ChooseFromListUID = "CFL20"
        oEdit.ChooseFromListAlias = "Code"

        '4th Solicitor Name Code 
        oEdit = objForm.Items.Item("item_156").Specific
        oEdit.ChooseFromListUID = "CFL21"
        oEdit.ChooseFromListAlias = "Code"

        'Loan Related Parties Borrower tab
        '1st Name Code
        oEdit = objForm.Items.Item("LRPItm_9").Specific
        oEdit.ChooseFromListUID = "CFL22"
        oEdit.ChooseFromListAlias = "Code"

        '2nd Name Code
        oEdit = objForm.Items.Item("LRPItm_19").Specific
        oEdit.ChooseFromListUID = "CFL23"
        oEdit.ChooseFromListAlias = "Code"

        '3rd Name Code
        oEdit = objForm.Items.Item("LRPItm_27").Specific
        oEdit.ChooseFromListUID = "CFL24"
        oEdit.ChooseFromListAlias = "Code"

        '4th Name Code
        oEdit = objForm.Items.Item("LRPItm_35").Specific
        oEdit.ChooseFromListUID = "CFL25"
        oEdit.ChooseFromListAlias = "Code"

        '1st Solicitor Name Code
        oEdit = objForm.Items.Item("LRPItm_45").Specific
        oEdit.ChooseFromListUID = "CFL26"
        oEdit.ChooseFromListAlias = "Code"

        '2nd Solicitor Name Code 
        oEdit = objForm.Items.Item("LRPItm_55").Specific
        oEdit.ChooseFromListUID = "CFL27"
        oEdit.ChooseFromListAlias = "Code"

        '3rd Solicitor Name Code
        oEdit = objForm.Items.Item("LRPItm_65").Specific
        oEdit.ChooseFromListUID = "CFL28"
        oEdit.ChooseFromListAlias = "Code"

        '4th Solicitor Name Code 
        oEdit = objForm.Items.Item("LRPItm_75").Specific
        oEdit.ChooseFromListUID = "CFL29"
        oEdit.ChooseFromListAlias = "Code"

        'Loan Related Parties Gurantor tab
        '1st Name Code
        oEdit = objForm.Items.Item("LRPItm_88").Specific
        oEdit.ChooseFromListUID = "CFL30"
        oEdit.ChooseFromListAlias = "Code"

        '2nd Name Code
        oEdit = objForm.Items.Item("LRPItm_98").Specific
        oEdit.ChooseFromListUID = "CFL31"
        oEdit.ChooseFromListAlias = "Code"

        '3rd Name Code
        oEdit = objForm.Items.Item("LRPItm_106").Specific
        oEdit.ChooseFromListUID = "CFL32"
        oEdit.ChooseFromListAlias = "Code"

        '4th Name Code
        oEdit = objForm.Items.Item("LRPItm_114").Specific
        oEdit.ChooseFromListUID = "CFL33"
        oEdit.ChooseFromListAlias = "Code"

        '1st Solicitor Name Code
        oEdit = objForm.Items.Item("LRPItm_122").Specific
        oEdit.ChooseFromListUID = "CFL34"
        oEdit.ChooseFromListAlias = "Code"

        '2nd Solicitor Name Code 
        oEdit = objForm.Items.Item("LRPItm_132").Specific
        oEdit.ChooseFromListUID = "CFL35"
        oEdit.ChooseFromListAlias = "Code"

        '3rd Solicitor Name Code
        oEdit = objForm.Items.Item("LRPItm_142").Specific
        oEdit.ChooseFromListUID = "CFL36"
        oEdit.ChooseFromListAlias = "Code"

        '4th Solicitor Name Code 
        oEdit = objForm.Items.Item("LRPItm_152").Specific
        oEdit.ChooseFromListUID = "CFL37"
        oEdit.ChooseFromListAlias = "Code"

        'Title Sys ID
        oEdit = objForm.Items.Item("CFPItm_3").Specific
        oEdit.ChooseFromListUID = "CFL38"
        oEdit.ChooseFromListAlias = "Code"

        'MASTER BANK NAME
        oEdit = objForm.Items.Item("LPItm_4").Specific
        oEdit.ChooseFromListUID = "CFL39"
        oEdit.ChooseFromListAlias = "U_NAME"

        'BANK SOLICITOR NAME
        oEdit = objForm.Items.Item("LPItm_18").Specific
        oEdit.ChooseFromListUID = "CFL40"
        oEdit.ChooseFromListAlias = "U_NAME"

    End Sub

#End Region
#Region "Change CFL Conditions"
    Private Sub ChangeParterCFLCondt(ByVal objForm As SAPbouiCOM.Form)
        Dim sFuncName As String = "ChangeParterCFLCondt"
        Dim sGroup As String = String.Empty
        Dim sSQL As String = String.Empty
        Dim oRecordSet As SAPbobsCOM.Recordset = Nothing

        Dim oCFL As SAPbouiCOM.ChooseFromList
        Dim oCFLs As SAPbouiCOM.ChooseFromListCollection
        oCFLs = objForm.ChooseFromLists
        Dim oCFLCreationParams As SAPbouiCOM.ChooseFromListCreationParams
        Dim oCons As SAPbouiCOM.Conditions
        Dim oCon As SAPbouiCOM.Condition
        oCFLCreationParams = p_oSBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams)
        oCFLCreationParams.MultiSelection = True
        Dim i As Integer

        Try

            oComboBox = objForm.Items.Item("16").Specific
            sGroup = oComboBox.Selected.Description
            sGroup = sGroup.Substring(0, 3).Trim

            'Partner CFL
            oCFL = oCFLs.Item("CFL1")
            oCons = New SAPbouiCOM.Conditions()

            i = 0
            sSQL = "SELECT A.firstName FROM OHEM A INNER JOIN HEM6 B ON B.empID = A.empID INNER JOIN OHTY C ON C.typeID = B.roleID "
            sSQL = sSQL & " WHERE C.name = '" & sGroup & "' AND B.U_Subrole = 'PN'"
            oRecordSet = p_oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecordSet.DoQuery(sSQL)
            If Not (oRecordSet.BoF And oRecordSet.EoF) Then
                oRecordSet.MoveFirst()
                Do Until oRecordSet.EoF
                    i = i + 1
                    If i > 1 Then
                        oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_OR
                    End If
                    oCon = oCons.Add()
                    oCon.Alias = "firstName"
                    oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                    oCon.CondVal = oRecordSet.Fields.Item("firstName").Value
                    oRecordSet.MoveNext()
                Loop
            Else
                oCon = oCons.Add()
                oCon.Alias = "firstName"
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                oCon.CondVal = ""
            End If
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordSet)
            oCFL.SetConditions(oCons)

            BindPartnerCFL(objForm)

        Catch ex As Exception
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug(ex.Message, sFuncName)
            Throw New ArgumentException(ex.Message)
        End Try
    End Sub
    Private Sub BindPartnerCFL(ByVal objForm As SAPbouiCOM.Form)
        'Partners CFL
        oEdit = objForm.Items.Item("BPHItm_19").Specific
        oEdit.ChooseFromListUID = "CFL1"
        oEdit.ChooseFromListAlias = "firstName"
    End Sub

    Private Sub ChangeLACFLCondt(ByVal objForm As SAPbouiCOM.Form)
        Dim sFuncName As String = "ChangeLACFLCondt"
        Dim sGroup As String = String.Empty
        Dim sSQL As String = String.Empty
        Dim oRecordSet As SAPbobsCOM.Recordset = Nothing

        Dim oCFL As SAPbouiCOM.ChooseFromList
        Dim oCFLs As SAPbouiCOM.ChooseFromListCollection
        oCFLs = objForm.ChooseFromLists
        Dim oCFLCreationParams As SAPbouiCOM.ChooseFromListCreationParams
        Dim oCons As SAPbouiCOM.Conditions
        Dim oCon As SAPbouiCOM.Condition
        oCFLCreationParams = p_oSBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams)
        oCFLCreationParams.MultiSelection = True
        Dim i As Integer

        Try

            oComboBox = objForm.Items.Item("16").Specific
            sGroup = oComboBox.Selected.Description
            sGroup = sGroup.Substring(0, 3).Trim

            'LA CFL
            oCFL = oCFLs.Item("CFL2")
            oCons = New SAPbouiCOM.Conditions()

            i = 0
            sSQL = "SELECT A.firstName FROM OHEM A INNER JOIN HEM6 B ON B.empID = A.empID INNER JOIN OHTY C ON C.typeID = B.roleID "
            sSQL = sSQL & " WHERE C.name = '" & sGroup & "' AND B.U_Subrole = 'LA'"
            oRecordSet = p_oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecordSet.DoQuery(sSQL)
            If Not (oRecordSet.BoF And oRecordSet.EoF) Then
                oRecordSet.MoveFirst()
                Do Until oRecordSet.EoF
                    i = i + 1
                    If i > 1 Then
                        oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_OR
                    End If
                    oCon = oCons.Add()
                    oCon.Alias = "firstName"
                    oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                    oCon.CondVal = oRecordSet.Fields.Item("firstName").Value
                    oRecordSet.MoveNext()
                Loop
            Else
                oCon = oCons.Add()
                oCon.Alias = "firstName"
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                oCon.CondVal = ""
            End If
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordSet)
            oCFL.SetConditions(oCons)

            BindLACFL(objForm)

        Catch ex As Exception
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug(ex.Message, sFuncName)
            Throw New ArgumentException(ex.Message)
        End Try
    End Sub
    Private Sub BindLACFL(ByVal objForm As SAPbouiCOM.Form)
        'LA CFL
        oEdit = objForm.Items.Item("BPHItm_21").Specific
        oEdit.ChooseFromListUID = "CFL2"
        oEdit.ChooseFromListAlias = "firstName"
    End Sub

    Private Sub ChangeManagerCFLCondt(ByVal objForm As SAPbouiCOM.Form)
        Dim sFuncName As String = "ChangeManagerCFLCondt"
        Dim sGroup As String = String.Empty
        Dim sSQL As String = String.Empty
        Dim oRecordSet As SAPbobsCOM.Recordset = Nothing

        Dim oCFL As SAPbouiCOM.ChooseFromList
        Dim oCFLs As SAPbouiCOM.ChooseFromListCollection
        oCFLs = objForm.ChooseFromLists
        Dim oCFLCreationParams As SAPbouiCOM.ChooseFromListCreationParams
        Dim oCons As SAPbouiCOM.Conditions
        Dim oCon As SAPbouiCOM.Condition
        oCFLCreationParams = p_oSBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams)
        oCFLCreationParams.MultiSelection = True
        Dim i As Integer

        Try

            oComboBox = objForm.Items.Item("16").Specific
            sGroup = oComboBox.Selected.Description
            sGroup = sGroup.Substring(0, 3).Trim

            'Manager 
            oCFL = oCFLs.Item("CFL3")
            oCons = New SAPbouiCOM.Conditions()

            i = 0
            sSQL = "SELECT A.firstName FROM OHEM A INNER JOIN HEM6 B ON B.empID = A.empID INNER JOIN OHTY C ON C.typeID = B.roleID "
            sSQL = sSQL & " WHERE C.name = '" & sGroup & "' AND B.U_Subrole = 'MG'"
            oRecordSet = p_oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecordSet.DoQuery(sSQL)
            If Not (oRecordSet.BoF And oRecordSet.EoF) Then
                oRecordSet.MoveFirst()
                Do Until oRecordSet.EoF
                    i = i + 1
                    If i > 1 Then
                        oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_OR
                    End If
                    oCon = oCons.Add()
                    oCon.Alias = "firstName"
                    oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                    oCon.CondVal = oRecordSet.Fields.Item("firstName").Value
                    oRecordSet.MoveNext()
                Loop
            Else
                oCon = oCons.Add()
                oCon.Alias = "firstName"
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                oCon.CondVal = ""
            End If
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordSet)
            oCFL.SetConditions(oCons)

            BindManagerCFL(objForm)

        Catch ex As Exception
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug(ex.Message, sFuncName)
            Throw New ArgumentException(ex.Message)
        End Try
    End Sub
    Private Sub BindManagerCFL(ByVal objForm As SAPbouiCOM.Form)
        'Manager 
        oEdit = objForm.Items.Item("BPHItm_9").Specific
        oEdit.ChooseFromListUID = "CFL3"
        oEdit.ChooseFromListAlias = "firstName"
    End Sub

    Private Sub ChangeInChargeCFLCondt(ByVal objForm As SAPbouiCOM.Form)
        Dim sFuncName As String = "ChangeInChargeCFLCondt"
        Dim sGroup As String = String.Empty
        Dim sSQL As String = String.Empty
        Dim oRecordSet As SAPbobsCOM.Recordset = Nothing

        Dim oCFL As SAPbouiCOM.ChooseFromList
        Dim oCFLs As SAPbouiCOM.ChooseFromListCollection
        oCFLs = objForm.ChooseFromLists
        Dim oCFLCreationParams As SAPbouiCOM.ChooseFromListCreationParams
        Dim oCons As SAPbouiCOM.Conditions
        Dim oCon As SAPbouiCOM.Condition
        oCFLCreationParams = p_oSBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams)
        oCFLCreationParams.MultiSelection = True
        Dim i As Integer

        Try

            oComboBox = objForm.Items.Item("16").Specific
            sGroup = oComboBox.Selected.Description
            sGroup = sGroup.Substring(0, 3).Trim

            'IN-Charge CFL
            oCFL = oCFLs.Item("CFL4")
            oCons = New SAPbouiCOM.Conditions()

            i = 0
            sSQL = "SELECT A.firstName FROM OHEM A INNER JOIN HEM6 B ON B.empID = A.empID INNER JOIN OHTY C ON C.typeID = B.roleID "
            sSQL = sSQL & " WHERE C.name = '" & sGroup & "' AND B.U_Subrole = 'IC'"
            oRecordSet = p_oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecordSet.DoQuery(sSQL)
            If Not (oRecordSet.BoF And oRecordSet.EoF) Then
                oRecordSet.MoveFirst()
                Do Until oRecordSet.EoF
                    i = i + 1
                    If i > 1 Then
                        oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_OR
                    End If
                    oCon = oCons.Add()
                    oCon.Alias = "firstName"
                    oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                    oCon.CondVal = oRecordSet.Fields.Item("firstName").Value
                    oRecordSet.MoveNext()
                Loop
            Else
                oCon = oCons.Add()
                oCon.Alias = "firstName"
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                oCon.CondVal = ""
            End If
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordSet)
            oCFL.SetConditions(oCons)

            BindInChargeCFL(objForm)

        Catch ex As Exception
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug(ex.Message, sFuncName)
            Throw New ArgumentException(ex.Message)
        End Try
    End Sub
    Private Sub BindInChargeCFL(ByVal objForm As SAPbouiCOM.Form)
        'IN-Charge CFL
        oEdit = objForm.Items.Item("BPHItm_11").Specific
        oEdit.ChooseFromListUID = "CFL4"
        oEdit.ChooseFromListAlias = "firstName"
    End Sub

    Private Sub ChangeCusServiceCFLCondt(ByVal objForm As SAPbouiCOM.Form)
        Dim sFuncName As String = "ChangeCusServiceCFLCondt"
        Dim sGroup As String = String.Empty
        Dim sSQL As String = String.Empty
        Dim oRecordSet As SAPbobsCOM.Recordset = Nothing

        Dim oCFL As SAPbouiCOM.ChooseFromList
        Dim oCFLs As SAPbouiCOM.ChooseFromListCollection
        oCFLs = objForm.ChooseFromLists
        Dim oCFLCreationParams As SAPbouiCOM.ChooseFromListCreationParams
        Dim oCons As SAPbouiCOM.Conditions
        Dim oCon As SAPbouiCOM.Condition
        oCFLCreationParams = p_oSBOApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_ChooseFromListCreationParams)
        oCFLCreationParams.MultiSelection = True
        Dim i As Integer

        Try

            oComboBox = objForm.Items.Item("16").Specific
            sGroup = oComboBox.Selected.Description
            sGroup = sGroup.Substring(0, 3).Trim

            'Customer Service CFL
            oCFL = oCFLs.Item("CFL5")
            oCons = New SAPbouiCOM.Conditions()

            i = 0
            sSQL = "SELECT A.firstName FROM OHEM A INNER JOIN HEM6 B ON B.empID = A.empID INNER JOIN OHTY C ON C.typeID = B.roleID "
            sSQL = sSQL & " WHERE C.name = '" & sGroup & "' AND B.U_Subrole = 'CS'"
            oRecordSet = p_oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
            oRecordSet.DoQuery(sSQL)
            If Not (oRecordSet.BoF And oRecordSet.EoF) Then
                oRecordSet.MoveFirst()
                Do Until oRecordSet.EoF
                    i = i + 1
                    If i > 1 Then
                        oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_OR
                    End If
                    oCon = oCons.Add()
                    oCon.Alias = "firstName"
                    oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                    oCon.CondVal = oRecordSet.Fields.Item("firstName").Value
                    oRecordSet.MoveNext()
                Loop
            Else
                oCon = oCons.Add()
                oCon.Alias = "firstName"
                oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL
                oCon.CondVal = ""
            End If
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oRecordSet)
            oCFL.SetConditions(oCons)

            BindCusServiceCFL(objForm)

        Catch ex As Exception
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug(ex.Message, sFuncName)
            Throw New ArgumentException(ex.Message)
        End Try
    End Sub
    Private Sub BindCusServiceCFL(objForm As SAPbouiCOM.Form)
        'Customer Service CFL
        oEdit = objForm.Items.Item("BPHItm_13").Specific
        oEdit.ChooseFromListUID = "CFL5"
        oEdit.ChooseFromListAlias = "firstName"
    End Sub
#End Region

    Private Sub OpenRelatedProperty(ByRef objForm As SAPbouiCOM.Form, ByRef sCode As String)
        Dim oForm As SAPbouiCOM.Form
        p_oSBOApplication.ActivateMenuItem(Udoform("RELATED PARTY"))
        oForm = p_oSBOApplication.Forms.GetForm("UDO_FT_RELATEDPARTY", 1)
        oForm.Freeze(True)
        oForm.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE
        oEdit = oForm.Items.Item("0_U_E").Specific
        oEdit.Value = sCode
        oForm.Items.Item("1").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
        oForm.Freeze(False)
    End Sub

    Private Sub OpenProperty(ByRef objForm As SAPbouiCOM.Form, ByVal sTitleSysId As String)
        Dim oForm As SAPbouiCOM.Form
        p_oSBOApplication.ActivateMenuItem(Udoform("PROPERTY"))
        oForm = p_oSBOApplication.Forms.GetForm("UDO_FT_PROPERTY", 1)
        oForm.Freeze(True)
        oForm.Mode = SAPbouiCOM.BoFormMode.fm_FIND_MODE
        oEdit = oForm.Items.Item("0_U_E").Specific
        oEdit.Value = sTitleSysId
        oForm.Items.Item("1").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
        oForm.Freeze(False)
    End Sub

    Public Sub BP_SBO_ItemEvent(ByVal FormUID As String, ByVal pval As SAPbouiCOM.ItemEvent, ByRef BubbleEvent As Boolean, ByVal objForm As SAPbouiCOM.Form)
        Dim sFuncName As String = "BP_SBO_ItemEvent"
        Dim sErrDesc As String = String.Empty
        Try
            If pval.Before_Action = True Then
                Select Case pval.EventType
                    Case SAPbouiCOM.BoEventTypes.et_KEY_DOWN
                        objForm = p_oSBOApplication.Forms.GetForm(pval.FormTypeEx, pval.FormTypeCount)
                        If pval.CharPressed = "9" Then
                            If pval.ItemUID = "BPHItm_19" Then
                                ChangeParterCFLCondt(objForm)
                            ElseIf pval.ItemUID = "BPHItm_21" Then
                                ChangeLACFLCondt(objForm)
                            ElseIf pval.ItemUID = "BPHItm_9" Then
                                ChangeManagerCFLCondt(objForm)
                            ElseIf pval.ItemUID = "BPHItm_11" Then
                                ChangeInChargeCFLCondt(objForm)
                            ElseIf pval.ItemUID = "BPHItm_13" Then
                                ChangeCusServiceCFLCondt(objForm)
                            End If
                        End If

                End Select
            Else
                Select Case pval.EventType
                    Case SAPbouiCOM.BoEventTypes.et_FORM_LOAD
                        objForm = p_oSBOApplication.Forms.GetForm(pval.FormTypeEx, pval.FormTypeCount)
                        Dim sCardCode As String = objForm.Items.Item("5").Specific.String
                        Dim sSQLstring As String = String.Empty
                        sSQLstring = String.Format("SELECT T0.[CardType] FROM OCRD T0 WHERE T0.[CardCode] = '{0}'", sCardCode)
                        oRecordSet = p_oDICompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset)
                        oRecordSet.DoQuery(sSQLstring)
                        If p_bBPcus = True Or oRecordSet.Fields.Item("CardType").Value = "C" Then
                            objForm.Title = "Case Master"
                            If FormModification(objForm, sErrDesc) <> RTN_SUCCESS Then
                                Throw New ArgumentException(sErrDesc)
                                p_oSBOApplication.StatusBar.SetText("Error while modifying the form", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error)
                                p_bBPcus = False
                                Exit Sub
                            End If
                            p_bBPcus = False
                            p_oSBOApplication.StatusBar.SetText("Operation Completed successfully", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success)
                        End If

                    Case SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED
                        objForm = p_oSBOApplication.Forms.GetForm(pval.FormTypeEx, pval.FormTypeCount)
                        If p_bBPcus = True Then
                            DisableBPUDFFields(objForm)
                        End If
                        If pval.ItemUID = "tcSPADt" Then
                            objForm.Freeze(True)
                            objForm.PaneLevel = "102"
                            objForm.Items.Item("fSPAPurch").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                            objForm.Freeze(False)
                        ElseIf pval.ItemUID = "tcLnPrts" Then
                            objForm.Freeze(True)
                            objForm.PaneLevel = "99"
                            objForm.Items.Item("fBorrow").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                            objForm.Freeze(False)
                        ElseIf pval.ItemUID = "tcCseFl" Then
                            objForm.Freeze(True)
                            objForm.PaneLevel = "100"
                            objForm.Items.Item("fPropty").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                            objForm.Freeze(False)
                        ElseIf pval.ItemUID = "tcLnPpl" Then
                            objForm.PaneLevel = "101"
                        ElseIf pval.ItemUID = "tcLnSub" Then
                            objForm.Freeze(True)
                            objForm.PaneLevel = "103"
                            objForm.Items.Item("tcLnSub1").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                            objForm.Freeze(False)
                        ElseIf pval.ItemUID = "tcLnSub1" Then
                            objForm.Freeze(True)
                            objForm.PaneLevel = "670"
                            objForm.Items.Item("tcLnSub1").Visible = True
                            objForm.Items.Item("tcLnSub2").Visible = True
                            objForm.Items.Item("tcLnSub3").Visible = True
                            objForm.Freeze(False)
                        ElseIf pval.ItemUID = "tcLnSub2" Then
                            objForm.Freeze(True)
                            objForm.PaneLevel = "671"
                            objForm.Items.Item("tcLnSub1").Visible = True
                            objForm.Items.Item("tcLnSub2").Visible = True
                            objForm.Items.Item("tcLnSub3").Visible = True
                            objForm.Freeze(False)
                        ElseIf pval.ItemUID = "tcLnSub3" Then
                            objForm.Freeze(True)
                            objForm.PaneLevel = "672"
                            objForm.Items.Item("tcLnSub1").Visible = True
                            objForm.Items.Item("tcLnSub2").Visible = True
                            objForm.Items.Item("tcLnSub3").Visible = True
                            objForm.Freeze(False)
                        ElseIf pval.ItemUID = "fSPAPurch" Then
                            objForm.Freeze(True)
                            objForm.PaneLevel = "555"
                            objForm.Items.Item("fSPAPurch").Visible = True
                            objForm.Items.Item("fSPAVend").Visible = True
                            objForm.Freeze(False)
                        ElseIf pval.ItemUID = "fSPAVend" Then
                            objForm.Freeze(True)
                            objForm.PaneLevel = "556"
                            objForm.Items.Item("fSPAPurch").Visible = True
                            objForm.Items.Item("fSPAVend").Visible = True
                            objForm.Freeze(False)
                        ElseIf pval.ItemUID = "fBorrow" Then
                            objForm.Freeze(True)
                            objForm.PaneLevel = "665"
                            objForm.Items.Item("fBorrow").Visible = True
                            objForm.Items.Item("fGurant").Visible = True
                            objForm.Freeze(False)
                        ElseIf pval.ItemUID = "fGurant" Then
                            objForm.Freeze(True)
                            objForm.PaneLevel = "666"
                            objForm.Items.Item("fBorrow").Visible = True
                            objForm.Items.Item("fGurant").Visible = True
                            objForm.Freeze(False)
                        ElseIf pval.ItemUID = "fPropty" Then
                            objForm.Freeze(True)
                            objForm.PaneLevel = "667"
                            objForm.Items.Item("fPropty").Visible = True
                            objForm.Items.Item("fValLon").Visible = True
                            objForm.Items.Item("fAport").Visible = True
                            objForm.Freeze(False)
                        ElseIf pval.ItemUID = "fValLon" Then
                            objForm.Freeze(True)
                            objForm.PaneLevel = "668"
                            objForm.Items.Item("fPropty").Visible = True
                            objForm.Items.Item("fValLon").Visible = True
                            objForm.Items.Item("fAport").Visible = True
                            objForm.Freeze(False)
                        ElseIf pval.ItemUID = "fAport" Then
                            objForm.Freeze(True)
                            objForm.PaneLevel = "669"
                            objForm.Items.Item("fPropty").Visible = True
                            objForm.Items.Item("fValLon").Visible = True
                            objForm.Items.Item("fAport").Visible = True
                            objForm.Freeze(False)
                        ElseIf pval.ItemUID = "LkItm_9" Then
                            oEdit = objForm.Items.Item("item_9").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LkItm_19" Then
                            oEdit = objForm.Items.Item("item_19").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LkItm_27" Then
                            oEdit = objForm.Items.Item("item_27").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LkItm_35" Then
                            oEdit = objForm.Items.Item("item_35").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LkItm_45" Then
                            oEdit = objForm.Items.Item("item_45").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LkItm_55" Then
                            oEdit = objForm.Items.Item("item_55").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LkItm_65" Then
                            oEdit = objForm.Items.Item("item_65").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                            '*****SPA PARTIES VENDOR TAB
                        ElseIf pval.ItemUID = "LkItm_75" Then
                            oEdit = objForm.Items.Item("item_75").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LkItm_100" Then
                            oEdit = objForm.Items.Item("item_100").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LkItm_108" Then
                            oEdit = objForm.Items.Item("item_108").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LkItm_116" Then
                            oEdit = objForm.Items.Item("item_116").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LkItm_126" Then
                            oEdit = objForm.Items.Item("item_126").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LkItm_136" Then
                            oEdit = objForm.Items.Item("item_136").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LkItm_146" Then
                            oEdit = objForm.Items.Item("item_146").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LkItm_156" Then
                            oEdit = objForm.Items.Item("item_156").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                            '*****LOAN RELATED PARTIES 
                        ElseIf pval.ItemUID = "LRPlkItm_9" Then
                            oEdit = objForm.Items.Item("LRPItm_9").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LRPlItm_19" Then
                            oEdit = objForm.Items.Item("LRPItm_19").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LRPlItm_27" Then
                            oEdit = objForm.Items.Item("LRPItm_27").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LRPlItm_35" Then
                            oEdit = objForm.Items.Item("LRPItm_35").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LRPlItm_45" Then
                            oEdit = objForm.Items.Item("LRPItm_45").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LRPlItm_55" Then
                            oEdit = objForm.Items.Item("LRPItm_55").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LRPlItm_65" Then
                            oEdit = objForm.Items.Item("LRPItm_65").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LRPlItm_75" Then
                            oEdit = objForm.Items.Item("LRPItm_75").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                            '******LOAN RELATED PARTIES GUARANTOR 
                        ElseIf pval.ItemUID = "LRPltm_88" Then
                            oEdit = objForm.Items.Item("LRPItm_88").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LRPltm_98" Then
                            oEdit = objForm.Items.Item("LRPItm_98").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LRPltm_106" Then
                            oEdit = objForm.Items.Item("LRPItm_106").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LRPltm_114" Then
                            oEdit = objForm.Items.Item("LRPItm_114").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LRPltm_122" Then
                            oEdit = objForm.Items.Item("LRPItm_122").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LRPltm_132" Then
                            oEdit = objForm.Items.Item("LRPItm_132").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LRPltm_142" Then
                            oEdit = objForm.Items.Item("LRPItm_142").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                        ElseIf pval.ItemUID = "LRPltm_152" Then
                            oEdit = objForm.Items.Item("LRPItm_152").Specific
                            If oEdit.Value <> "" Then
                                OpenRelatedProperty(objForm, oEdit.Value)
                            End If
                            '**********TITLE SYSID
                        ElseIf pval.ItemUID = "CPFlkItm_3" Then
                            oEdit = objForm.Items.Item("CFPItm_3").Specific
                            If oEdit.Value <> "" Then
                                OpenProperty(objForm, oEdit.Value)
                            End If
                        End If

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

                    Case SAPbouiCOM.BoEventTypes.et_FORM_RESIZE
                        objForm = p_oSBOApplication.Forms.GetForm(pval.FormTypeEx, pval.FormTypeCount)
                        Try
                            If objForm.Title = "Case Master" Then
                                objForm.Freeze(True)
                                objForm.Items.Item("13").Visible = False
                                objForm.Items.Item("15").Visible = False
                                objForm.Items.Item("156").Visible = False
                                objForm.Items.Item("214").Visible = False
                                objForm.Items.Item("10").Visible = False
                                objForm.Items.Item("9").Visible = False
                                objForm.Items.Item("1320002081").Visible = False
                                objForm.Freeze(False)
                            End If
                        Catch ex As Exception
                            objForm.Freeze(False)
                            objForm.Update()
                        End Try

                    Case SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST
                        objForm = p_oSBOApplication.Forms.GetForm(pval.FormTypeEx, pval.FormTypeCount)
                        Dim oCFLEvento As SAPbouiCOM.IChooseFromListEvent
                        oCFLEvento = pval
                        Dim sCFL_ID As String
                        sCFL_ID = oCFLEvento.ChooseFromListUID
                        'Dim objForm As SAPbouiCOM.Form = p_oSBOApplication.Forms.Item(FormUID)
                        Dim oCFL As SAPbouiCOM.ChooseFromList
                        oCFL = objForm.ChooseFromLists.Item(sCFL_ID)
                        Try
                            If oCFLEvento.BeforeAction = False Then
                                Dim oDataTable As SAPbouiCOM.DataTable
                                oDataTable = oCFLEvento.SelectedObjects
                                If pval.ItemUID = "BPHItm_19" Then
                                    objForm.Items.Item("BPHItm_23").Specific.string = oDataTable.GetValue("empID", 0)
                                    objForm.Items.Item("PLkItm_9").Visible = True
                                    objForm.Items.Item("BPHItm_19").Specific.string = oDataTable.GetValue("firstName", 0)
                                ElseIf pval.ItemUID = "BPHItm_21" Then
                                    objForm.Items.Item("BPHItm_24").Specific.string = oDataTable.GetValue("empID", 0)
                                    objForm.Items.Item("BPHItm_21").Specific.string = oDataTable.GetValue("firstName", 0)
                                ElseIf pval.ItemUID = "BPHItm_9" Then
                                    objForm.Items.Item("BPHItm_25").Specific.string = oDataTable.GetValue("empID", 0)
                                    objForm.Items.Item("BPHItm_9").Specific.string = oDataTable.GetValue("firstName", 0)
                                ElseIf pval.ItemUID = "BPHItm_11" Then
                                    objForm.Items.Item("BPHItm_26").Specific.string = oDataTable.GetValue("empID", 0)
                                    objForm.Items.Item("BPHItm_11").Specific.string = oDataTable.GetValue("firstName", 0)
                                ElseIf pval.ItemUID = "BPHItm_13" Then
                                    objForm.Items.Item("BPHItm_27").Specific.string = oDataTable.GetValue("empID", 0)
                                    objForm.Items.Item("BPHItm_13").Specific.string = oDataTable.GetValue("firstName", 0)
                                ElseIf pval.ItemUID = "item_9" Then
                                    objForm.Items.Item("item_11").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("item_9").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "item_19" Then
                                    objForm.Items.Item("item_21").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("item_19").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "item_27" Then
                                    objForm.Items.Item("item_29").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("item_27").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "item_35" Then
                                    objForm.Items.Item("item_37").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("item_35").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "item_45" Then
                                    objForm.Items.Item("item_47").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("item_45").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "item_55" Then
                                    objForm.Items.Item("item_57").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("item_55").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "item_65" Then
                                    objForm.Items.Item("item_67").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("item_65").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "item_75" Then
                                    objForm.Items.Item("item_77").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("item_75").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "item_90" Then
                                    objForm.Items.Item("item_92").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("item_90").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "item_100" Then
                                    objForm.Items.Item("item_102").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("item_100").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "item_108" Then
                                    objForm.Items.Item("item_110").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("item_108").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "item_116" Then
                                    objForm.Items.Item("item_118").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("item_116").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "item_126" Then
                                    objForm.Items.Item("item_128").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("item_126").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "item_136" Then
                                    objForm.Items.Item("item_138").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("item_136").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "item_146" Then
                                    objForm.Items.Item("item_148").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("item_146").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "item_156" Then
                                    objForm.Items.Item("item_158").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("item_156").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "LRPItm_9" Then
                                    objForm.Items.Item("LRPItm_11").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("LRPItm_9").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "LRPItm_19" Then
                                    objForm.Items.Item("LRPItm_21").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("LRPItm_19").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "LRPItm_27" Then
                                    objForm.Items.Item("LRPItm_29").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("LRPItm_27").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "LRPItm_35" Then
                                    objForm.Items.Item("LRPItm_37").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("LRPItm_35").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "LRPItm_45" Then
                                    objForm.Items.Item("LRPItm_47").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("LRPItm_45").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "LRPItm_55" Then
                                    objForm.Items.Item("LRPItm_57").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("LRPItm_55").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "LRPItm_65" Then
                                    objForm.Items.Item("LRPItm_67").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("LRPItm_65").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "LRPItm_75" Then
                                    objForm.Items.Item("LRPItm_77").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("LRPItm_75").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "LRPItm_88" Then
                                    objForm.Items.Item("LRPItm_90").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("LRPItm_88").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "LRPItm_98" Then
                                    objForm.Items.Item("LRPItm_100").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("LRPItm_98").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "LRPItm_106" Then
                                    objForm.Items.Item("LRPItm_108").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("LRPItm_106").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "LRPItm_114" Then
                                    objForm.Items.Item("LRPItm_116").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("LRPItm_114").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "LRPItm_122" Then
                                    objForm.Items.Item("LRPItm_124").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("LRPItm_122").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "LRPItm_132" Then
                                    objForm.Items.Item("LRPItm_134").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("LRPItm_132").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "LRPItm_142" Then
                                    objForm.Items.Item("LRPItm_144").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("LRPItm_142").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "LRPItm_152" Then
                                    objForm.Items.Item("LRPItm_154").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                    objForm.Items.Item("LRPItm_152").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "CFPItm_3" Then
                                    objForm.Items.Item("CFPItm_4").Specific.string = oDataTable.GetValue("U_TITLETYPE", 0)
                                    objForm.Items.Item("CFPItm_7").Specific.string = oDataTable.GetValue("U_LOTTYPE", 0) & " " & oDataTable.GetValue("U_TITLENO", 0)
                                    objForm.Items.Item("CFPItm_8").Specific.string = "" 'oDataTable.GetValue("U_TITLENO", 0)
                                    objForm.Items.Item("CFPItm_10").Specific.string = oDataTable.GetValue("U_STATE", 0)
                                    objForm.Items.Item("CFPItm_12").Specific.string = oDataTable.GetValue("U_AREA", 0)
                                    objForm.Items.Item("CFPItm_14").Specific.string = oDataTable.GetValue("U_BPM", 0)
                                    objForm.Items.Item("CFPItm_16").Specific.string = oDataTable.GetValue("U_LOTAREA_SQM", 0)
                                    objForm.Items.Item("CFPItm_18").Specific.string = oDataTable.GetValue("U_LOTAREA_SQFT", 0)
                                    objForm.Items.Item("CFPItm_3").Specific.string = oDataTable.GetValue("Code", 0)
                                ElseIf pval.ItemUID = "LPItm_4" Then
                                    objForm.Items.Item("LPItm_4").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                ElseIf pval.ItemUID = "LPItm_18" Then
                                    objForm.Items.Item("LPItm_18").Specific.string = oDataTable.GetValue("U_NAME", 0)
                                End If
                            End If
                        Catch ex As Exception
                            objForm.Freeze(False)
                            objForm.Update()
                        End Try

                End Select
            End If
        Catch ex As Exception
            sErrDesc = ex.Message
            Call WriteToLogFile(sErrDesc, sFuncName)
            If p_iDebugMode = DEBUG_ON Then Call WriteToLogFile_Debug("Completed with ERROR", sFuncName)
            Throw New ArgumentException(sErrDesc)
        End Try
    End Sub

    Public Sub BP_SBO_MenuEvent(ByVal pVal As SAPbouiCOM.MenuEvent, ByRef BubbleEvent As Boolean)
        Try
            If pVal.BeforeAction = False Then
                Dim objForm As SAPbouiCOM.Form
                If pVal.MenuUID = "AE_VM" Then
                    p_bBPcus = False
                    p_oSBOApplication.ActivateMenuItem("2561")
                    Exit Sub
                ElseIf pVal.MenuUID = "AE_CM" Then
                    objForm = p_oSBOApplication.Forms.Item(p_oSBOApplication.Forms.ActiveForm.UniqueID)
                    p_bBPcus = True
                    p_oSBOApplication.StatusBar.SetText("Please wait....", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Warning)
                    p_oSBOApplication.ActivateMenuItem("2561")
                    objForm = p_oSBOApplication.Forms.GetFormByTypeAndCount(134, 1)
                    objForm.Items.Item("13").Visible = False
                    objForm.Items.Item("15").Visible = False
                    objForm.Items.Item("156").Visible = False
                    objForm.Items.Item("214").Visible = False
                    objForm.Items.Item("10").Visible = False
                    objForm.Items.Item("9").Visible = False
                    objForm.Items.Item("1320002081").Visible = False
                ElseIf pVal.MenuUID = "1282" Then
                    objForm = p_oSBOApplication.Forms.Item(p_oSBOApplication.Forms.ActiveForm.UniqueID)
                    If objForm.Title = "Case Master" Then
                        objForm.Freeze(True)
                        DisableBPUDFFields(objForm)

                        oComboBox = objForm.Items.Item("40").Specific
                        oComboBox.Select("Customer")
                        objForm.Items.Item("7").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                        objForm.Items.Item("40").Enabled = False
                        objForm.Items.Item("tcSPADt").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                        objForm.Freeze(False)
                    Else
                        objForm.Freeze(True)
                        oComboBox = objForm.Items.Item("40").Specific
                        oComboBox.Select("Vendor")
                        objForm.Items.Item("7").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                        objForm.Items.Item("40").Enabled = False
                        objForm.Freeze(False)
                    End If
                ElseIf pVal.MenuUID = "1288" Or pVal.MenuUID = "1289" Or pVal.MenuUID = "1290" Or pVal.MenuUID = "1291" Then
                    objForm = p_oSBOApplication.Forms.Item(p_oSBOApplication.Forms.ActiveForm.UniqueID)
                    If objForm.Title = "Case Master" Then
                        objForm.Freeze(True)
                        objForm.Items.Item("7").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                        objForm.Items.Item("tcSPADt").Click(SAPbouiCOM.BoCellClickType.ct_Regular)
                        objForm.Items.Item("40").Enabled = False
                        DisableBPUDFFields(objForm)
                        objForm.Freeze(False)
                    End If

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Module

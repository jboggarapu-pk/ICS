Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

' Test results form
Partial Public Class TestResultsUC
    Inherits BaseUserControl
    Implements ITestResultsView

    ' Changed the sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation.

#Region "Members"

    Public Event DoLoadViewDataEvent() Implements Presenter.ITestResultsView.DoLoadViewDataEvent
    Public Event DoLoadViewBlankEvent() Implements Presenter.ITestResultsView.DoLoadViewBlankEvent
    Public Event GetRequestedTests() Implements Presenter.ITestResultsView.GetRequestedTests

    Private m_presenter As TestResultsPresenter

#End Region

#Region "Constructors"

    Public Sub New()

        m_presenter = New TestResultsPresenter(Me)

    End Sub

#End Region

#Region "Public Properties"

    Public Property IsVisible() As Boolean Implements Presenter.ITestResultsView.IsVisible
        Get
            Return Me.Visible
        End Get
        Set(ByVal value As Boolean)
            Me.Visible = value
        End Set
    End Property

    Public Property InfoText() As String Implements Presenter.ITestResultsView.InfoText
        Get
            Return lblInfoText.Text
        End Get
        Set(ByVal value As String)
            lblInfoText.Text = value
        End Set
    End Property

    Public Property ErrorText() As String Implements Presenter.ITestResultsView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    Public Property MaterialNumber() As String Implements Presenter.ITestResultsView.MaterialNumber
        Get
            Return Session("TestResultMaterialNumber")
        End Get
        Set(ByVal value As String)
            Session("TestResultMaterialNumber") = value
        End Set
    End Property

    Public Property SKUID() As Integer Implements Presenter.ITestResultsView.SKUID
        Get
            Return Session("TestResultSKUID")
        End Get
        Set(ByVal value As Integer)
            Session("TestResultSKUID") = value
        End Set
    End Property

    Public Property CertificationTypeId() As Integer Implements Presenter.ITestResultsView.CertificationTypeId
        Get
            Return Session("TestResultCertificationTypeId")
        End Get
        Set(ByVal value As Integer)
            Session("TestResultCertificationTypeId") = value
        End Set
    End Property

    Public Property CertificateNumber() As String Implements Presenter.ITestResultsView.CertificateNumber
        Get
            Return Session("TestResultCertificateNumber")
        End Get
        Set(ByVal value As String)
            Session("TestResultCertificateNumber") = value
        End Set
    End Property

    Public Property ExtensionNo() As String Implements Presenter.ITestResultsView.ExtensionNo
        Get
            Return Session("TestResultExtensionNo")
        End Get
        Set(ByVal value As String)
            Session("TestResultExtensionNo") = value
        End Set
    End Property

    Public Property CertificateNumberID() As Integer Implements Presenter.ITestResultsView.CertificateNumberID
        Get
            Return Session(Me.GetType().Name & "CertificateNumberID")
        End Get
        Set(ByVal value As Integer)
            Session(Me.GetType().Name & "CertificateNumberID") = value
        End Set
    End Property

    Public Property SimilarTireMaterialNum() As String Implements Presenter.ITestResultsView.SimilarTireMatlNum
        Get
            Return Session(Me.GetType().Name & "SimilarTireMatlNum")
        End Get
        Set(ByVal value As String)
            Session(Me.GetType().Name & "SimilarTireMatlNum") = value
        End Set
    End Property

    Public Property SimilarTireSKUID() As Integer Implements Presenter.ITestResultsView.SimilarTireSKUID
        Get
            Return Session(Me.GetType().Name & "SimilarTireSKUID")
        End Get
        Set(ByVal value As Integer)
            Session(Me.GetType().Name & "SimilarTireSKUID") = value
        End Set
    End Property

    Public Property ManufacturingLocationId() As Integer Implements Presenter.ITestResultsView.ManufacturingLocationId
        Get
            Return Session(Me.GetType().Name & "ManufacturingLocationId")
        End Get
        Set(ByVal value As Integer)
            Session(Me.GetType().Name & "ManufacturingLocationId") = value
        End Set
    End Property

    Public Property TireTypeId() As Integer Implements Presenter.ITestResultsView.TireTypeId
        Get
            Return Session(Me.GetType().Name & "TireTypeId")
        End Get
        Set(ByVal value As Integer)
            Session(Me.GetType().Name & "TireTypeId") = value
        End Set
    End Property

    Public Property ClientRequest() As DataTable Implements Presenter.ITestResultsView.ClientRequest
        Get
            Return Session(Me.GetType().Name & "ClientRequest")
        End Get
        Set(ByVal value As DataTable)
            Session(Me.GetType().Name & "ClientRequest") = value
        End Set
    End Property

#End Region

#Region "Private Properties"

    Private Property TRProductSectionData_OriginalProduct() As Product
        Get
            Return Session("TRProductSectionData_OriginalProduct")
        End Get
        Set(ByVal value As Product)
            Session("TRProductSectionData_OriginalProduct") = value
        End Set
    End Property

    Private Property TRMeasureSectionData_OriginalMeasure() As Measure
        Get
            Return Session("TRMeasureSectionData_OriginalMeasure")
        End Get
        Set(ByVal value As Measure)
            Session("TRMeasureSectionData_OriginalMeasure") = value
        End Set
    End Property

    Private Property TRMeasureSectionData_OriginalTreadwear() As Treadwear
        Get
            Return Session("TRMeasureSectionData_OriginalTreadwear")
        End Get
        Set(ByVal value As Treadwear)
            Session("TRMeasureSectionData_OriginalTreadwear") = value
        End Set
    End Property

    Private Property TRMeasureSectionData_OriginalPlunger() As Plunger
        Get
            Return Session("TRMeasureSectionData_OriginalPlunger")
        End Get
        Set(ByVal value As Plunger)
            Session("TRMeasureSectionData_OriginalPlunger") = value
        End Set
    End Property

    Private Property TRMeasureSectionData_OriginalBeadUnSeat() As BeadUnSeat
        Get
            Return Session("TRMeasureSectionData_OriginalBeadUnSeat")
        End Get
        Set(ByVal value As BeadUnSeat)
            Session("TRMeasureSectionData_OriginalBeadUnSeat") = value
        End Set
    End Property

    Private Property TREnduranceSectionData_OriginalEndurance() As Endurance
        Get
            Return Session("TREnduranceSectionData_OriginalEndurance")
        End Get
        Set(ByVal value As Endurance)
            Session("TREnduranceSectionData_OriginalEndurance") = value
        End Set
    End Property

    Private Property TRHighSpeedSectionData_OriginalHighSpeed() As HighSpeed
        Get
            Return Session("TRHighSpeedSectionData_OriginalHighSpeed")
        End Get
        Set(ByVal value As HighSpeed)
            Session("TRHighSpeedSectionData_OriginalHighSpeed") = value
        End Set
    End Property

    Private Property TRSoundWetSectionData_OriginalSound() As Sound
        Get
            Return Session("TRSoundWetSectionData_OriginalSound")
        End Get
        Set(ByVal value As Sound)
            Session("TRSoundWetSectionData_OriginalSound") = value
        End Set
    End Property

    Private Property TRSoundWetSectionData_OriginalWetGrip() As WetGrip
        Get
            Return Session("TRSoundWetSectionData_OriginalWetGrip")
        End Get
        Set(ByVal value As WetGrip)
            Session("TRSoundWetSectionData_OriginalWetGrip") = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Initiate data load
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DoLoadViewData() Implements Presenter.ITestResultsView.DoLoadViewData

        RaiseEvent DoLoadViewDataEvent()

    End Sub

    ''' <summary>
    ''' Initiate blank load
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DoLoadViewBlank() Implements Presenter.ITestResultsView.DoLoadViewBlank

        RaiseEvent DoLoadViewBlankEvent()

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AdjustViewToCertificationType() Implements ITestResultsView.AdjustViewToCertificationType

       
        'Dim strCertificationTypeName As String = [Enum].GetName(GetType(NameAid.Certification), CertificationTypeId)
        Dim strCertificationTypeName As String = m_presenter.GetCertificationTypeName(CertificationTypeId)

        'Indicator is used to determine which fields to display on screen
        Dim strCertificationIndicator As String

        'check here for generic type
        Dim strCertTemplate As String = m_presenter.CertTemplate(strCertificationTypeName)

        If strCertTemplate = "GENERAL" Then
            strCertificationIndicator = "X"

        Else

            'yfye added logic to include india mark
            strCertificationIndicator = IIf(strCertificationTypeName.Contains("India"), "D", strCertificationTypeName.Substring(0, 1))

        End If

        'Hide Test Results title label if CCC.
        If strCertificationIndicator.ToLower() = "c" Or strCertificationIndicator.ToLower = "d" Or strCertificationIndicator.ToLower = "x" Then
            title.Visible = False
        End If

        For Each ctlControl As Control In pnlTestResult.Controls
            If ctlControl.ID IsNot Nothing Then
                If ctlControl.GetType().Name = GetType(Panel).Name Then
                    ctlControl.Visible = BelongsToCertificationType(ctlControl.ID, strCertificationIndicator)
                    For Each ctlSection As Control In ctlControl.Controls
                        If ctlSection.ID IsNot Nothing Then
                            If ctlSection.GetType().Name = GetType(Panel).Name And ctlSection.ID.Contains("_Data") Then
                                For Each ctlData As Control In ctlSection.Controls
                                    If ctlData.GetType().Name = GetType(Panel).Name Then
                                        ctlData.Visible = BelongsToCertificationType(ctlData.ID, strCertificationIndicator)
                                    End If
                                Next
                            End If
                        End If
                    Next
                End If
            End If
        Next

        'ECE30/54
        If CertificationTypeId = 1 Then
            Me.SoundWetGrip_E.Visible = False
            Me.pnlSoundWetGrip_E.Visible = False
            Me.optlstNominalWidthYN.Visible = False
            If TireTypeId = 1 Then
                'ECE30/54 Turn off these for passenger tire types
                Me.lblSingLoadCapacityIndex.Visible = False
                Me.txtSingLoadCapacityIndex_CEGID.Visible = False
                Me.lblDualLoadCapacityIndex.Visible = False
                Me.txtDualLoadCapacityIndex_CEGID.Visible = False
                Me.lblIndicationRegroovable.Visible = False
                Me.optlstIndicationRegroovable_CEGD.Visible = False
            Else
                Me.lblSingLoadCapacityIndex.Visible = True
                Me.txtSingLoadCapacityIndex_CEGID.Visible = True
                Me.lblDualLoadCapacityIndex.Visible = True
                Me.txtDualLoadCapacityIndex_CEGID.Visible = True
                Me.lblIndicationRegroovable.Visible = True
                Me.optlstIndicationRegroovable_CEGD.Visible = True
            End If
        End If

        'ECE117
        If CertificationTypeId = 6 Then
            Me.pnlEndurance_Data.Visible = False
            Me.Endurance_EGIN.Visible = False
            Me.EnduranceTestBefore_EI.Visible = False
            Me.EnduranceTestAfter_EIN.Visible = False
            Me.pnlHighSpeed_Data.Visible = False
            Me.HighSpeed_EGIN.Visible = False
            Me.HighSpeedTestBefore_EI.Visible = False
            Me.HighSpeedTestAfter_EIN.Visible = False
            Me.pnlMeasurement_Data.Visible = False
            Me.Measurement_EGIN.Visible = False
            If TireTypeId = 1 Then
                'ECE117 Turn off these for passenger tire types
                Me.lblSingLoadCapacityIndex.Visible = False
                Me.txtSingLoadCapacityIndex_CEGID.Visible = False
                Me.lblDualLoadCapacityIndex.Visible = False
                Me.txtDualLoadCapacityIndex_CEGID.Visible = False
                Me.lblIndicationRegroovable.Visible = False
                Me.optlstIndicationRegroovable_CEGD.Visible = False
            Else
                Me.lblSingLoadCapacityIndex.Visible = True
                Me.txtSingLoadCapacityIndex_CEGID.Visible = True
                Me.lblDualLoadCapacityIndex.Visible = True
                Me.txtDualLoadCapacityIndex_CEGID.Visible = True
                Me.lblIndicationRegroovable.Visible = True
                Me.optlstIndicationRegroovable_CEGD.Visible = True
            End If
        End If

        If CertificationTypeId = 1 Then
            Me.pnlEnduranceDifferenceOuterDiameterToleranceAfter_E.Visible = False
            Me.pnlHighSpeedDifferenceOuterDiameterToleranceAfter_E.Visible = False
        End If

        'NOM
        If CertificationTypeId = 3 Then
            Me.EnduranceTestAfter_EIN.Visible = False
            Me.HighSpeedTestAfter_EIN.Visible = False
        End If

        'Hidden field. Do not display to client
        Me.pnlDOTSerialNumber_CEGIND.Visible = False

    End Sub

    ''' <summary>
    ''' Check if data control belongs to certification type
    ''' </summary>
    ''' <param name="p_strControlID"></param>
    ''' <param name="p_strCertificationIndicator"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function BelongsToCertificationType(ByVal p_strControlID As String, ByVal p_strCertificationIndicator As String) As Boolean

        Dim iIndex As Integer
        Dim strCertificationIndicator As String
        iIndex = p_strControlID.IndexOf("_")
        If iIndex <= 0 Then
            Return True
        End If
        strCertificationIndicator = p_strControlID.Substring(iIndex)
        Return strCertificationIndicator.Contains(p_strCertificationIndicator)

    End Function

    ''' <summary>
    ''' Get TR data control value and set respective field value in data object
    ''' </summary>
    ''' <param name="p_fField"></param>
    ''' <param name="p_ctlDataElem"></param>
    ''' <param name="p_objTRSectionData"></param>
    ''' <remarks></remarks>
    Private Sub GetTRDataControlValue(ByRef p_fField As System.Reflection.FieldInfo, ByVal p_ctlDataElem As Control, ByRef p_objTRSectionData As Object)

        Dim strTypeName As String = p_ctlDataElem.GetType().Name
        Select Case (strTypeName)
            Case GetType(TextBox).Name
                p_fField.SetValue(p_objTRSectionData, CType(p_ctlDataElem, TextBox).Text)
            Case GetType(RadioButtonList).Name
                If p_fField.FieldType.Name = "String" Then
                    If p_ctlDataElem.ID = "optlstStructureConstruction" Then
                        If CType(p_ctlDataElem, RadioButtonList).Items(0).Selected Then
                            p_fField.SetValue(p_objTRSectionData, "RADIAL") 'radial
                        ElseIf CType(p_ctlDataElem, RadioButtonList).Items(1).Selected Then
                            p_fField.SetValue(p_objTRSectionData, "BIAS") 'bias
                        ElseIf CType(p_ctlDataElem, RadioButtonList).Items(2).Selected Then
                            p_fField.SetValue(p_objTRSectionData, "BELTED") 'bias belted
                        Else
                            p_fField.SetValue(p_objTRSectionData, "RADIAL")
                        End If
                    End If
                Else
                    p_fField.SetValue(p_objTRSectionData, CType(p_ctlDataElem, RadioButtonList).Items(0).Selected)
                End If
            Case GetType(DropDownList).Name

            Case Else
                Throw New Exception("Error getting value from " + p_ctlDataElem.ID)
        End Select

    End Sub

    ''' <summary>
    ''' Set the data value to the view
    ''' </summary>
    ''' <param name="p_ctlDataElem"></param>
    ''' <param name="p_objValue"></param>
    ''' <remarks></remarks>
    Private Sub SetTRDataControlValue(ByVal p_ctlDataElem As Control, ByVal p_objValue As Object)

        Dim strTypeName As String = p_ctlDataElem.GetType().Name
        Select Case (strTypeName)
            Case GetType(TextBox).Name
                CType(p_ctlDataElem, TextBox).Text = CType(p_objValue, String)
                If CType(p_ctlDataElem, TextBox).ID = "txtMeasureMatlNum_CEGIND" Or CType(p_ctlDataElem, TextBox).ID = "txtTreadwearMatlNum_CEGIND" _
                    Or CType(p_ctlDataElem, TextBox).ID = "txtPlungerMatlNum_CEGIND" Or CType(p_ctlDataElem, TextBox).ID = "txtBeadUnseatMatlNum_CEGIND" _
                    Or CType(p_ctlDataElem, TextBox).ID = "txtEnduranceMatlNum" Or CType(p_ctlDataElem, TextBox).ID = "txtHighSpeedMatlNum" Then
                    CType(p_ctlDataElem, TextBox).Text = CType(p_ctlDataElem, TextBox).Text.TrimStart("0"c)
                End If
                If CType(p_ctlDataElem, TextBox).ID = "txtGTSpecMeasureMatlNum_CEGIND" Or CType(p_ctlDataElem, TextBox).ID = "txtGTSpecTreadwearMatlNum_CEGIND" _
                                    Or CType(p_ctlDataElem, TextBox).ID = "txtGTSpecPlungerMatlNum_CEGIND" Or CType(p_ctlDataElem, TextBox).ID = "txtGTSpecBeadUnseatMatlNum_CEGIND" _
                                    Or CType(p_ctlDataElem, TextBox).ID = "txtGTSpecEnduranceMatlNum_EGIN" Or CType(p_ctlDataElem, TextBox).ID = "txtGTSpecHighSpeedMatlNum" Then
                    CType(p_ctlDataElem, TextBox).Text = CType(p_ctlDataElem, TextBox).Text.TrimStart("0"c)
                End If

                If CType(p_ctlDataElem, TextBox).ID = "txtTireId" Then
                    CType(p_ctlDataElem, TextBox).Text = CType(p_objValue, String)
                    ddlTireType.SelectedValue = CType(CType(p_ctlDataElem, TextBox).Text, Integer)
                End If
            Case GetType(RadioButtonList).Name
                If p_objValue.GetType.Name = "String" Then
                    If p_ctlDataElem.ID = "optlstStructureConstruction" Then
                        Select Case p_objValue.ToString().ToUpper()
                            Case "RADIAL" 'radial
                                CType(p_ctlDataElem, RadioButtonList).Items(0).Selected = True
                                CType(p_ctlDataElem, RadioButtonList).Items(1).Selected = False
                                CType(p_ctlDataElem, RadioButtonList).Items(2).Selected = False
                            Case "BIAS" 'bias
                                CType(p_ctlDataElem, RadioButtonList).Items(0).Selected = False
                                CType(p_ctlDataElem, RadioButtonList).Items(1).Selected = True
                                CType(p_ctlDataElem, RadioButtonList).Items(2).Selected = False
                            Case "BELTED" 'bias belted
                                CType(p_ctlDataElem, RadioButtonList).Items(0).Selected = False
                                CType(p_ctlDataElem, RadioButtonList).Items(1).Selected = False
                                CType(p_ctlDataElem, RadioButtonList).Items(2).Selected = True
                        End Select
                    End If
                Else
                    CType(p_ctlDataElem, RadioButtonList).Items(0).Selected = CType(p_objValue, Boolean)
                    CType(p_ctlDataElem, RadioButtonList).Items(1).Selected = Not CType(p_objValue, Boolean)
                End If
            Case GetType(DropDownList).Name
                If CType(p_ctlDataElem, DropDownList).ID = "ddlTireType" Then
                    Dim ddlTireType As DropDownList = CType(p_ctlDataElem, DropDownList)
                    ddlTireType.DataSource = CType(p_objValue, DataTable)
                    ddlTireType.DataBind()
                End If
            Case Else
                Throw New Exception("Error setting value to " + p_ctlDataElem.ID)
        End Select

    End Sub

    ''' <summary>
    ''' Put TR section data to panel data controls
    ''' </summary>
    ''' <param name="p_objTRSectionData"></param>
    ''' <param name="p_pnlTRSD"></param>
    ''' <remarks></remarks>
    Private Sub PutTRSectionDataToPanel(ByVal p_objTRSectionData As Object, ByVal p_pnlTRSD As Panel)

        Dim typeTRSD As Type = p_objTRSectionData.GetType()
        Dim fields As Reflection.FieldInfo() = typeTRSD.GetFields()

        For Each field As Reflection.FieldInfo In fields
            Dim strDataElemName As String = field.Name
            Dim ctlDataElem As Control = FindTRDataControl(strDataElemName, p_pnlTRSD)

            ' Set control value depending on member-value type
            If ctlDataElem IsNot Nothing Then
                SetTRDataControlValue(ctlDataElem, field.GetValue(p_objTRSectionData))
            End If
        Next

    End Sub

    ''' <summary>
    ''' Read TR section data from panel into respective data object
    ''' </summary>
    ''' <param name="p_objTRSectionData"></param>
    ''' <param name="p_pnlTRSD"></param>
    ''' <remarks></remarks>
    Private Sub ReadTRSectionDataFromPanel(ByRef p_objTRSectionData As Object, ByVal p_pnlTRSD As Panel)

        Dim typeTRSD As Type = p_objTRSectionData.GetType()
        Dim fields As System.Reflection.FieldInfo() = typeTRSD.GetFields()

        For Each field As System.Reflection.FieldInfo In fields
            Dim strDataElemName As String = field.Name
            Dim ctlDataElem As Control = FindTRDataControl(strDataElemName, p_pnlTRSD)

            If ctlDataElem IsNot Nothing Then
                GetTRDataControlValue(field, ctlDataElem, p_objTRSectionData)
            End If
        Next

    End Sub

    ''' <summary>
    ''' Set TRProject section data to the view
    ''' </summary>
    ''' <param name="p_ObjTRPSD"></param>
    ''' <remarks></remarks>
    Sub SetTRProjectSectionData(ByVal p_objTRPSD As DomainEntities.TRProjectSectionData) Implements Presenter.ITestResultsView.SetTRProjectSectionData

        PutTRSectionDataToPanel(p_objTRPSD, pnlProjectTest_Data)

    End Sub

    ''' <summary>
    ''' Set TRProduct section data to the view
    ''' </summary>
    ''' <param name="p_objTRPSD"></param>
    ''' <remarks></remarks>
    Public Sub SetTRProductSectionData(ByVal p_objTRPSD As DomainEntities.TRProductSectionData) Implements Presenter.ITestResultsView.SetTRProductSectionData

        TRProductSectionData_OriginalProduct = p_objTRPSD.OriginalProduct

        PutTRSectionDataToPanel(p_objTRPSD, pnlProductData_Data)

    End Sub

    ''' <summary>
    ''' Set Measurement section data to the view
    ''' </summary>
    ''' <param name="p_objTRMSD"></param>
    ''' <remarks></remarks>
    Public Sub SetTRMeasureSectionData(ByVal p_objTRMSD As TRMeasurementSectionData) Implements Presenter.ITestResultsView.SetTRMeasureSectionData

        TRMeasureSectionData_OriginalMeasure = p_objTRMSD.OriginalMeasure
        TRMeasureSectionData_OriginalTreadwear = p_objTRMSD.OriginalTreadwear
        TRMeasureSectionData_OriginalPlunger = p_objTRMSD.OriginalPlunger
        TRMeasureSectionData_OriginalBeadUnSeat = p_objTRMSD.OriginalBeadUnSeat

        PutTRSectionDataToPanel(p_objTRMSD, pnlMeasurement_Data)

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_objTREBSD"></param>
    ''' <remarks></remarks>
    Public Sub SetTREnduranceBeforeSectionData(ByVal p_objTREBSD As TREnduranceTestGeneralBeforeSectionData) Implements Presenter.ITestResultsView.SetTREnduranceBeforeSectionData

        PutTRSectionDataToPanel(p_objTREBSD, pnlEnduranceTestBefore_Data)

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_objTRESD"></param>
    ''' <remarks></remarks>
    Public Sub SetTREnduranceSectionData(ByVal p_objTRESD As TREnduranceSectionData) Implements Presenter.ITestResultsView.SetTREnduranceSectionData

        TREnduranceSectionData_OriginalEndurance = p_objTRESD.OriginalEndurance

        PutTRSectionDataToPanel(p_objTRESD, pnlEndurance_Data)

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_objTREASD"></param>
    ''' <remarks></remarks>
    Public Sub SetTREnduranceAfterSectionData(ByVal p_objTREASD As TREnduranceTestGeneralAfterSectionData) Implements Presenter.ITestResultsView.SetTREnduranceAfterSectionData

        PutTRSectionDataToPanel(p_objTREASD, pnlEnduranceTestAfter_Data)

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_objTRHSBSD"></param>
    ''' <remarks></remarks>
    Public Sub SetTRHighSpeedBeforeSectionData(ByVal p_objTRHSBSD As TRHighSpeedTestGeneralBeforeSectionData) Implements Presenter.ITestResultsView.SetTRHighSpeedBeforeSectionData

        PutTRSectionDataToPanel(p_objTRHSBSD, pnlHighSpeedTestBefore_Data)

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_objTRHSSD"></param>
    ''' <remarks></remarks>
    Public Sub SetTRHighSpeedSectionData(ByVal p_objTRHSSD As TRHighSpeedSectionData) Implements Presenter.ITestResultsView.SetTRHighSpeedSectionData

        TRHighSpeedSectionData_OriginalHighSpeed = p_objTRHSSD.OriginalHighSpeed

        PutTRSectionDataToPanel(p_objTRHSSD, pnlHighSpeed_Data)

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_objTRHSASD"></param>
    ''' <remarks></remarks>
    Public Sub SetTRHighSpeedAfterSectionData(ByVal p_objTRHSASD As TRHighSpeedTestGeneralAfterSectionData) Implements Presenter.ITestResultsView.SetTRHighSpeedAfterSectionData

        PutTRSectionDataToPanel(p_objTRHSASD, pnlHighSpeedTestAfter_Data)

    End Sub

    Public Sub SetTRSoundWetSectionData(ByVal p_objTRSWSD As TRSoundWetSectionData) Implements Presenter.ITestResultsView.SetTRSoundWetSectionData

        TRSoundWetSectionData_OriginalSound = p_objTRSWSD.OriginalSound
        TRSoundWetSectionData_OriginalWetGrip = p_objTRSWSD.OriginalWetGrip

        PutTRSectionDataToPanel(p_objTRSWSD, pnlSoundWetGrip_Data)

    End Sub

    ''' <summary>
    ''' Find TR section Data Control for respective data element
    ''' </summary>
    ''' <param name="p_strDataElemName"></param>
    ''' <param name="p_ctlContainer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FindTRDataControl(ByVal p_strDataElemName As String, ByVal p_ctlContainer As Control) As Control

        Dim ctlDataElem As Control = Nothing

        For Each control As Control In p_ctlContainer.Controls
            If control.ID Is Nothing Then Continue For

            If control.GetType().Name = GetType(Panel).Name Then

                For Each ctlData As Control In control.Controls
                    If ctlData.ID Is Nothing Then Continue For
                    If ctlData.GetType().Name = GetType(Label).Name Then Continue For
                    If Not ctlData.ID.ToUpper.Contains(p_strDataElemName.ToUpper) Then Continue For

                    ctlDataElem = ctlData
                    Exit For
                Next

            End If

            If Not ctlDataElem Is Nothing Then Exit For
        Next

        Return ctlDataElem

    End Function

    ''' <summary>
    ''' Gets the ProjectSectionData
    ''' </summary>
    ''' <returns>
    ''' objTRProjectData
    ''' </returns>
    ''' <remarks></remarks>
    Public Function GetTRProjectSectionData() As DomainEntities.TRProjectSectionData Implements Presenter.ITestResultsView.GetTRProjectSectionData

        Dim objTRProjectData As TRProjectSectionData = Nothing

        If Not Me.IsVisible Then
            Return objTRProjectData
        End If

        objTRProjectData = New TRProjectSectionData()
        ReadTRSectionDataFromPanel(objTRProjectData, pnlProjectTest_Data)

        Return objTRProjectData

    End Function

    ''' <summary>
    ''' Get TR Product section data from the view controls
    ''' </summary>
    ''' <returns>
    ''' objProductData
    ''' </returns>
    ''' <remarks></remarks>
    Public Function GetTRProductSectionData() As DomainEntities.TRProductSectionData Implements Presenter.ITestResultsView.GetTRProductSectionData

        Dim objProductData As TRProductSectionData = Nothing
        If Not Me.IsVisible Then
            Return objProductData
        End If

        objProductData = New TRProductSectionData()
        ReadTRSectionDataFromPanel(objProductData, pnlProductData_Data)

        objProductData.OriginalProduct = TRProductSectionData_OriginalProduct

        Return objProductData

    End Function

    ''' <summary>
    ''' Get TR Measurement section data from the view controls
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTRMeasurementSectionData() As DomainEntities.TRMeasurementSectionData Implements Presenter.ITestResultsView.GetTRMeasurementSectionData

        Dim objMeasurementData As TRMeasurementSectionData = Nothing
        If Not Me.IsVisible Then
            Return objMeasurementData
        End If

        objMeasurementData = New TRMeasurementSectionData()
        ReadTRSectionDataFromPanel(objMeasurementData, pnlMeasurement_Data)


        objMeasurementData.OriginalMeasure = TRMeasureSectionData_OriginalMeasure
        objMeasurementData.OriginalTreadwear = TRMeasureSectionData_OriginalTreadwear
        objMeasurementData.OriginalPlunger = TRMeasureSectionData_OriginalPlunger
        objMeasurementData.OriginalBeadUnSeat = TRMeasureSectionData_OriginalBeadUnSeat

        Return objMeasurementData

    End Function

    ''' <summary>
    ''' Get TR Endurance Test Before section data from the view controls
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTREnduranceBeforeSectionData() As DomainEntities.TREnduranceTestGeneralBeforeSectionData Implements Presenter.ITestResultsView.GetTREnduranceBeforeSectionData

        Dim objEnduranceBeforeSectionData As TREnduranceTestGeneralBeforeSectionData = Nothing
        If Not Me.IsVisible Then
            Return objEnduranceBeforeSectionData
        End If

        objEnduranceBeforeSectionData = New TREnduranceTestGeneralBeforeSectionData()
        ReadTRSectionDataFromPanel(objEnduranceBeforeSectionData, pnlEnduranceTestBefore_Data)

        Return objEnduranceBeforeSectionData

    End Function

    ''' <summary>
    ''' Get TR Endurance section data from the view controls
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTREnduranceSectionData() As DomainEntities.TREnduranceSectionData Implements Presenter.ITestResultsView.GetTREnduranceSectionData

        Dim objEnduranceSectionData As TREnduranceSectionData = Nothing
        If Not Me.IsVisible Then
            Return objEnduranceSectionData
        End If

        objEnduranceSectionData = New TREnduranceSectionData()
        ReadTRSectionDataFromPanel(objEnduranceSectionData, pnlEndurance_Data)


        objEnduranceSectionData.OriginalEndurance = TREnduranceSectionData_OriginalEndurance

        Return objEnduranceSectionData

    End Function

    ''' <summary>
    ''' Get TR Endurance test after section data from the view controls
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTREnduranceAfterSectionData() As DomainEntities.TREnduranceTestGeneralAfterSectionData Implements Presenter.ITestResultsView.GetTREnduranceAfterSectionData

        Dim objEnduranceAfterSectionData As TREnduranceTestGeneralAfterSectionData = Nothing
        If Not Me.IsVisible Then
            Return objEnduranceAfterSectionData
        End If

        objEnduranceAfterSectionData = New TREnduranceTestGeneralAfterSectionData()
        ReadTRSectionDataFromPanel(objEnduranceAfterSectionData, pnlEnduranceTestAfter_Data)

        Return objEnduranceAfterSectionData

    End Function

    ''' <summary>
    ''' Get TR HighSpeed Test Before section data from the view controls
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTRHighSpeedBeforeSectionData() As DomainEntities.TRHighSpeedTestGeneralBeforeSectionData Implements Presenter.ITestResultsView.GetTRHighSpeedBeforeSectionData

        Dim objHighSpeedBeforeSectionData As TRHighSpeedTestGeneralBeforeSectionData = Nothing
        If Not Me.IsVisible Then
            Return objHighSpeedBeforeSectionData
        End If

        objHighSpeedBeforeSectionData = New TRHighSpeedTestGeneralBeforeSectionData()
        ReadTRSectionDataFromPanel(objHighSpeedBeforeSectionData, pnlHighSpeedTestBefore_Data)

        Return objHighSpeedBeforeSectionData

    End Function

    ''' <summary>
    ''' Get TR HighSpeed section data from the view controls
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTRHighSpeedSectionData() As DomainEntities.TRHighSpeedSectionData Implements Presenter.ITestResultsView.GetTRHighSpeedSectionData

        Dim objHighSpeedSectionData As TRHighSpeedSectionData = Nothing
        If Not Me.IsVisible Then
            Return objHighSpeedSectionData
        End If

        objHighSpeedSectionData = New TRHighSpeedSectionData()
        ReadTRSectionDataFromPanel(objHighSpeedSectionData, pnlHighSpeed_Data)

        objHighSpeedSectionData.OriginalHighSpeed = TRHighSpeedSectionData_OriginalHighSpeed

        Return objHighSpeedSectionData

    End Function

    ''' <summary>
    ''' Get TR HighSpeed test after section data from the view controls
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTRHighSpeedAfterSectionData() As DomainEntities.TRHighSpeedTestGeneralAfterSectionData Implements Presenter.ITestResultsView.GetTRHighSpeedAfterSectionData

        Dim objHighSpeedAfterSectionData As TRHighSpeedTestGeneralAfterSectionData = Nothing
        If Not Me.IsVisible Then
            Return objHighSpeedAfterSectionData
        End If

        objHighSpeedAfterSectionData = New TRHighSpeedTestGeneralAfterSectionData()
        ReadTRSectionDataFromPanel(objHighSpeedAfterSectionData, pnlHighSpeedTestAfter_Data)

        Return objHighSpeedAfterSectionData

    End Function

    ''' <summary>
    ''' Get TR Sound Wet test result section data from the view controls
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTRSoundWetSectionData() As DomainEntities.TRSoundWetSectionData Implements Presenter.ITestResultsView.GetTRSoundWetSectionData

        Dim objSoundWetSectionData As TRSoundWetSectionData = Nothing
        If Not Me.IsVisible Then
            Return objSoundWetSectionData
        End If

        objSoundWetSectionData = New TRSoundWetSectionData()
        ReadTRSectionDataFromPanel(objSoundWetSectionData, pnlSoundWetGrip_Data)

        objSoundWetSectionData.OriginalSound = TRSoundWetSectionData_OriginalSound
        objSoundWetSectionData.OriginalWetGrip = TRSoundWetSectionData_OriginalWetGrip

        Return objSoundWetSectionData

    End Function

    Protected Sub ppvNumber_ValueConvert(ByVal sender As System.Object, ByVal e As Microsoft.Practices.EnterpriseLibrary.Validation.Integration.ValueConvertEventArgs)

        If String.IsNullOrEmpty(e.ValueToConvert) Then
            e.ConvertedValue = 0
            Return
        End If

        Dim convertedValue As Integer

        If Int32.TryParse(e.ValueToConvert, convertedValue) Then
            e.ConvertedValue = convertedValue
        Else
            e.ConversionErrorMessage = "number Please..."
        End If

    End Sub

    'Changed as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation.
    Private Sub btnGetRequestedTests_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetRequestedTests.Click

        Dim dtbClientRequest As New DataTable

        Dim dcProjectNum As New DataColumn
        Dim dcTireNum As New DataColumn
        Dim dcOperation As New DataColumn
        Dim dcTestSpec As New DataColumn
        Dim dcTestSequence As New DataColumn
        Dim dcTestType As New DataColumn
        Dim drNewRow As DataRow

        dcProjectNum.ColumnName = "ProjectNum"
        dcTireNum.ColumnName = "TireNum"
        dcOperation.ColumnName = "Operation"
        dcTestSpec.ColumnName = "TestSpec"
        dcTestSequence.ColumnName = "TestSequence"
        dcTestType.ColumnName = "TestType"

        dtbClientRequest.Columns.Add(dcProjectNum)
        dtbClientRequest.Columns.Add(dcTireNum)
        dtbClientRequest.Columns.Add(dcOperation)
        dtbClientRequest.Columns.Add(dcTestSpec)
        dtbClientRequest.Columns.Add(dcTestSequence)
        dtbClientRequest.Columns.Add(dcTestType)

        'Bead UnSeat
        If txtBeadUnSeatProjectNumber.Text.Length > 0 And txtBeadUnSeatTireNumber.Text.Length > 0 And txtBeadUnSeatTestSpec.Text.Length > 0 Then
            drNewRow = dtbClientRequest.NewRow()
            drNewRow.Item("ProjectNum") = txtBeadUnSeatProjectNumber.Text
            drNewRow.Item("TireNum") = txtBeadUnSeatTireNumber.Text
            drNewRow.Item("Operation") = txtBeadUnSeatOperation.Text
            drNewRow.Item("TestSpec") = txtBeadUnSeatTestSpec.Text
            dtbClientRequest.Rows.Add(drNewRow)
        End If

        'Endurance
        If txtEnduranceProjectNumber.Text.Length > 0 And txtEnduranceTireNumber.Text.Length > 0 And txtEnduranceTestSpec.Text.Length > 0 Then
            drNewRow = dtbClientRequest.NewRow()
            drNewRow.Item("ProjectNum") = txtEnduranceProjectNumber.Text
            drNewRow.Item("TireNum") = txtEnduranceTireNumber.Text
            drNewRow.Item("Operation") = txtEnduranceOperation.Text
            drNewRow.Item("TestSpec") = txtEnduranceTestSpec.Text
            dtbClientRequest.Rows.Add(drNewRow)
        End If

        'High Speed
        If txtHighSpeedProjectNumber.Text.Length > 0 And txtHighSpeedTireNumber.Text.Length > 0 And txtHighSpeedTestSpec.Text.Length > 0 Then
            drNewRow = dtbClientRequest.NewRow()
            drNewRow.Item("ProjectNum") = txtHighSpeedProjectNumber.Text
            drNewRow.Item("TireNum") = txtHighSpeedTireNumber.Text
            drNewRow.Item("Operation") = txtHighSpeedOperation.Text
            drNewRow.Item("TestSpec") = txtHighSpeedTestSpec.Text
            dtbClientRequest.Rows.Add(drNewRow)
        End If

        'Measure
        If txtMeasureProjectNumber.Text.Length > 0 And txtMeasureTireNumber.Text.Length > 0 And txtMeasureTestSpec.Text.Length > 0 Then
            drNewRow = dtbClientRequest.NewRow()
            drNewRow.Item("ProjectNum") = txtMeasureProjectNumber.Text
            drNewRow.Item("TireNum") = txtMeasureTireNumber.Text
            drNewRow.Item("Operation") = txtMeasureOperation.Text
            drNewRow.Item("TestSpec") = txtMeasureTestSpec.Text
            dtbClientRequest.Rows.Add(drNewRow)
        End If

        'Plunger
        If txtPlungerProjectNumber.Text.Length > 0 And txtPlungerTireNumber.Text.Length > 0 And txtPlungerTestSpec.Text.Length > 0 Then
            drNewRow = dtbClientRequest.NewRow()
            drNewRow.Item("ProjectNum") = txtPlungerProjectNumber.Text
            drNewRow.Item("TireNum") = txtPlungerTireNumber.Text
            drNewRow.Item("Operation") = txtPlungerOperation.Text
            drNewRow.Item("TestSpec") = txtPlungerTestSpec.Text
            dtbClientRequest.Rows.Add(drNewRow)
        End If

        'Sound
        If txtSoundProjectNumber.Text.Length > 0 And txtSoundTireNumber.Text.Length > 0 And txtSoundTestSpec.Text.Length > 0 Then
            drNewRow = dtbClientRequest.NewRow()
            drNewRow.Item("ProjectNum") = txtSoundProjectNumber.Text
            drNewRow.Item("TireNum") = txtSoundTireNumber.Text
            drNewRow.Item("Operation") = txtSoundOperation.Text
            drNewRow.Item("TestSpec") = txtSoundTestSpec.Text
            dtbClientRequest.Rows.Add(drNewRow)
        End If

        'Treadwear
        If txtTreadwearProjectNumber.Text.Length > 0 And txtTreadwearTireNumber.Text.Length > 0 And txtTreadwearTestSpec.Text.Length > 0 Then
            drNewRow = dtbClientRequest.NewRow()
            drNewRow.Item("ProjectNum") = txtTreadwearProjectNumber.Text
            drNewRow.Item("TireNum") = txtTreadwearTireNumber.Text
            drNewRow.Item("Operation") = txtTreadwearOperation.Text
            drNewRow.Item("TestSpec") = txtTreadwearTestSpec.Text
            dtbClientRequest.Rows.Add(drNewRow)
        End If

        'Wet Grip
        If txtWetGripProjectNumber.Text.Length > 0 And txtWetGripTireNumber.Text.Length > 0 And txtWetGripTestSpec.Text.Length > 0 Then
            drNewRow = dtbClientRequest.NewRow()
            drNewRow.Item("ProjectNum") = txtWetGripProjectNumber.Text
            drNewRow.Item("TireNum") = txtWetGripTireNumber.Text
            drNewRow.Item("Operation") = txtWetGripOperation.Text
            drNewRow.Item("TestSpec") = txtWetGripTestSpec.Text
            dtbClientRequest.Rows.Add(drNewRow)
        End If

        ClientRequest = dtbClientRequest

        RaiseEvent GetRequestedTests()

    End Sub

    ''' <summary>
    ''' Event which captures selected Tire type Id.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub TireType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTireType.SelectedIndexChanged
        If (ddlTireType.SelectedIndex <> -1) Then
            txtTireId.Text = ddlTireType.SelectedValue.ToString()
        End If
    End Sub

#End Region

End Class

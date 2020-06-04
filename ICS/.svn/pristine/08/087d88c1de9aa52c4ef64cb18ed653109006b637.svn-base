Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

Partial Public Class FamilyMaintenanceUC
    Inherits BaseUserControl
    Implements IFamilyMaintenanceView

#Region "Members"

    Public Event SaveFamily As CustomEvents.PlainEventHandler Implements IFamilyMaintenanceView.SaveFamily
    Public Event LoadViewData() Implements Presenter.IFamilyMaintenanceView.LoadViewData
    Public Event View As CustomEvents.PlainEventHandler Implements IFamilyMaintenanceView.ShowFamiles
    Public Event DeleteFamily As CustomEvents.PlainArgumentEventHandler Implements IFamilyMaintenanceView.DeleteFamily
    Public Event AddFamily As CustomEvents.PlainEventHandler Implements IFamilyMaintenanceView.AddFamily
    Public Event EditFamily As CustomEvents.PlainArgumentEventHandler Implements IFamilyMaintenanceView.EditFamily
    Public Event ChangeCertificate As CustomEvents.PlainArgumentEventHandler Implements IFamilyMaintenanceView.ChangeCertificate
    Private m_presenter As FamilyMaintenancePresenter

    Private m_lstIMarkFamily As List(Of IMarkFamily)

#End Region

#Region "Constructors"

    Public Sub New()
        m_presenter = New FamilyMaintenancePresenter(Me)
    End Sub

#End Region

#Region "Properties"

    Public Property CertificationName() As String Implements Presenter.IFamilyMaintenanceView.CertificationName
        Get
            Return Session("AddCertName")
        End Get
        Set(ByVal value As String)
            Session("AddCertName") = value
        End Set
    End Property
    Public Property FamilyId() As Integer Implements IFamilyMaintenanceView.FamilyId

        Get
            Return CInt(txtftrFamilyId.Text)
        End Get
        Set(ByVal value As Integer)
            txtftrFamilyId.Text = value
        End Set
    End Property

    Public Property SuccessText() As String Implements Presenter.IFamilyMaintenanceView.SuccessText
        Get
            Return lblSuccessText.Text
        End Get
        Set(ByVal value As String)
            lblSuccessText.Text = value
        End Set
    End Property
    Public Property ErrorText() As String Implements Presenter.IFamilyMaintenanceView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    Public Property AddTitle() As String Implements Presenter.IFamilyMaintenanceView.AddTitle
        Get
            Return lblFormTitle.Text
        End Get
        Set(ByVal value As String)
            lblFormTitle.Text = value
        End Set
    End Property
    Public Property Families() As DataTable Implements Presenter.IFamilyMaintenanceView.Families
        Get
            Return Session("Families")
        End Get
        Set(ByVal value As DataTable)
            Session("Families") = value
            gvFamilyMaintenance.DataSource = value
        End Set
    End Property

    Public Property FamilyCode() As String Implements Presenter.IFamilyMaintenanceView.FamilyCode
        Get
            Return txtftrFamilyCode.Text
        End Get
        Set(ByVal value As String)
            txtftrFamilyCode.Text = value
        End Set
    End Property
    Public Property FamilyDesc() As String Implements Presenter.IFamilyMaintenanceView.FamilyDesc
        Get
            Return txtftrFamilyDesc.Text
        End Get
        Set(ByVal value As String)
            txtftrFamilyDesc.Text = value
        End Set
    End Property
    Public Property ApplicationCat() As String Implements Presenter.IFamilyMaintenanceView.ApplicationCat
        Get
            Return txtftrApplicationCat.Text
        End Get
        Set(ByVal value As String)
            txtftrApplicationCat.Text = value
        End Set
    End Property
    Public Property ConstructionType() As String Implements Presenter.IFamilyMaintenanceView.ConstructionType
        Get
            Return txtftrConstructionType.Text
        End Get
        Set(ByVal value As String)
            txtftrConstructionType.Text = value
        End Set
    End Property
    Public Property StructureType() As String Implements Presenter.IFamilyMaintenanceView.StructureType
        Get
            Return txtftrStructureType.Text
        End Get
        Set(ByVal value As String)
            txtftrStructureType.Text = value
        End Set
    End Property
    Public Property MountingType() As String Implements Presenter.IFamilyMaintenanceView.MountingType
        Get
            Return txtftrMountingType.Text
        End Get
        Set(ByVal value As String)
            txtftrMountingType.Text = value
        End Set
    End Property
    Public Property AspectRatioCat() As String Implements Presenter.IFamilyMaintenanceView.AspectRatioCat
        Get
            Return txtftrAspectRatioCat.Text
        End Get
        Set(ByVal value As String)
            txtftrAspectRatioCat.Text = value
        End Set
    End Property
    Public Property SpeedRatingCat() As String Implements Presenter.IFamilyMaintenanceView.SpeedRatingCat
        Get
            Return txtftrSpeedRatingCat.Text
        End Get
        Set(ByVal value As String)
            txtftrSpeedRatingCat.Text = value
        End Set
    End Property
    Public Property LoadIndexCat() As String Implements Presenter.IFamilyMaintenanceView.LoadIndexCat
        Get
            Return txtftrLoadIndexCat.Text
        End Get
        Set(ByVal value As String)
            txtftrLoadIndexCat.Text = value
        End Set
    End Property
    Public Property blnQualityUser() As Boolean Implements Presenter.IFamilyMaintenanceView.QualityUser
        Get
            Return True
        End Get
        Set(ByVal value As Boolean)
            gvFamilyMaintenance.Columns(0).Visible = value
            gvFamilyMaintenance.Columns(1).Visible = value
            btnAdd.Visible = value
        End Set
    End Property
    Public Property ImarkCertificates() As System.Data.DataTable Implements Presenter.IFamilyMaintenanceView.ImarkCertificates
        Get
            Return ddImark.DataSource
        End Get
        Set(ByVal value As System.Data.DataTable)
            ddImark.DataSource = value
            ddImark.DataTextField = value.Columns(1).ColumnName.ToString

            ddImark.DataValueField = value.Columns(0).ColumnName.ToString


            'gvFamilyMaintenance.Columns(0).Visible = value
            'gvFamilyMaintenance.Columns(1).Visible = value
            'btnAdd.Visible = value
        End Set
    End Property
    Public Property ImarkCertificateSelected() As Long Implements Presenter.IFamilyMaintenanceView.ImarkCertificateSelected
        Get
            Dim Imarkselection As Long = ddImark.SelectedItem.Value

            Return Imarkselection
        End Get
        Set(ByVal value As Long)
            ddImark.SelectedValue = value
        End Set
    End Property


#End Region

    ''' <summary>
    ''' Setup family maintenance
    ''' </summary>
    ''' <param name="p_strCertificationName"></param>
    ''' <param name="p_blnFamilymaint"></param>
    ''' <remarks></remarks>
    Public Sub SetupViewData(ByVal p_strCertificationName As String, ByVal p_blnFamilymaint As Boolean) Implements Presenter.IFamilyMaintenanceView.SetupViewData
        'Dim pn_certificateid = Me.ddImark.SelectedItem
        If p_blnFamilymaint Then
            CertificationName = p_strCertificationName
            RaiseEvent LoadViewData()
        End If
    End Sub

    ''' <summary>
    ''' This event fires when we click delete/edit link of any row
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Grid_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        Dim pn_certificateid = Me.ddImark.SelectedItem
        If e.CommandName = "EditFamily" Then
            RaiseEvent EditFamily(e.CommandArgument)
            ' Making the edit/add panel visible
            divFamilyDetails.Visible = True
        End If

        If e.CommandName = "DeleteFamily" Then
            Me.DeleteConfirmPopUp.Show()
            Me.hidConfirm.Value = e.CommandArgument
        End If
    End Sub

    ''' <summary>
    ''' This event fires when we click on Save 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Click_SaveFamily(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim pn_certificateid = Me.ddImark.SelectedItem
        RaiseEvent SaveFamily()
        If ErrorText.Length <= 0 Then
            divFamilyDetails.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' This event fires when we click on Cancel 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Click_CancelFamily(ByVal sender As System.Object, ByVal e As System.EventArgs)
        divFamilyDetails.Visible = False
    End Sub

    ''' <summary>
    ''' This event fires when we click on Add  
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Click_AddFamily(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RaiseEvent AddFamily()
        'Dim pn_certificateid = Me.ddImark.SelectedItem.value
        divFamilyDetails.Visible = True
    End Sub

    ''' <summary>
    ''' Event to delete a Family on confirm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Click_DeleteConfirm(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DeleteConfirmPopUp.Dispose()
        'Dim pn_certificateid = Me.ddImark.SelectedItem.Value
        RaiseEvent DeleteFamily(Me.hidConfirm.Value)
        Me.hidConfirm.Value = String.Empty
    End Sub

    ''' <summary>
    ''' Event to skip Family deletion on unconfirm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Click_DeleteCancel(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DeleteConfirmPopUp.Dispose()
    End Sub


    ''' <summary>
    ''' Event to select Imark for Families to edit
    '''     ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddImark_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddImark.SelectedIndexChanged
        Dim pn_certificateid = Me.ddImark.SelectedItem.Value
        RaiseEvent ChangeCertificate(ddImark.SelectedItem.Value)
    End Sub
End Class
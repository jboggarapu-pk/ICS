Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

Partial Public Class FamilyMaintenanceUC
    Inherits BaseUserControl
    Implements IFamilyMaintenanceView

#Region "Members"
    ''' <summary>
    ''' Save Family Event.
    ''' </summary>
    Public Event SaveFamily As CustomEvents.PlainEventHandler Implements IFamilyMaintenanceView.SaveFamily
    ''' <summary>
    ''' Load View Data Event.
    ''' </summary>
    Public Event LoadViewData() Implements Presenter.IFamilyMaintenanceView.LoadViewData
    ''' <summary>
    ''' View Event.
    ''' </summary>
    Public Event View As CustomEvents.PlainEventHandler Implements IFamilyMaintenanceView.ShowFamiles
    ''' <summary>
    ''' Delete Family Event.
    ''' </summary>
    Public Event DeleteFamily As CustomEvents.PlainArgumentEventHandler Implements IFamilyMaintenanceView.DeleteFamily
    ''' <summary>
    ''' Add Family Event.
    ''' </summary>
    Public Event AddFamily As CustomEvents.PlainEventHandler Implements IFamilyMaintenanceView.AddFamily
    ''' <summary>
    ''' Edit Family Event.
    ''' </summary>
    Public Event EditFamily As CustomEvents.PlainArgumentEventHandler Implements IFamilyMaintenanceView.EditFamily
    ''' <summary>
    ''' Change Certificate Event.
    ''' </summary>
    Public Event ChangeCertificate As CustomEvents.PlainArgumentEventHandler Implements IFamilyMaintenanceView.ChangeCertificate
    ''' <summary>
    ''' Family Maintenance Presenter.
    ''' </summary>
    Private m_presenter As FamilyMaintenancePresenter
    ''' <summary>
    ''' List IMark Family.
    ''' </summary>
    Private m_lstIMarkFamily As List(Of IMarkFamily)

#End Region

#Region "Constructors"
    ''' <summary>
    ''' Default Constructor to initialize class members.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New()
        m_presenter = New FamilyMaintenancePresenter(Me)
    End Sub

#End Region

#Region "Properties"
    ''' <summary>
    ''' Gets or sets Certification Name value.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property CertificationName() As String Implements Presenter.IFamilyMaintenanceView.CertificationName
        Get
            Return CStr(Session("AddCertName"))
        End Get
        Set(ByVal value As String)
            Session("AddCertName") = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Family Id value.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property FamilyId() As Integer Implements IFamilyMaintenanceView.FamilyId

        Get
            Return CInt(txtftrFamilyId.Text)
        End Get
        Set(ByVal value As Integer)
            txtftrFamilyId.Text = CStr(value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Success Text value.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property SuccessText() As String Implements Presenter.IFamilyMaintenanceView.SuccessText
        Get
            Return lblSuccessText.Text
        End Get
        Set(ByVal value As String)
            lblSuccessText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Error Text value.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ErrorText() As String Implements Presenter.IFamilyMaintenanceView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Add Title value.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property AddTitle() As String Implements Presenter.IFamilyMaintenanceView.AddTitle
        Get
            Return lblFormTitle.Text
        End Get
        Set(ByVal value As String)
            lblFormTitle.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Families value.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property Families() As DataTable Implements Presenter.IFamilyMaintenanceView.Families
        Get
            Return CType(Session("Families"), DataTable)
        End Get
        Set(ByVal value As DataTable)
            Session("Families") = value
            gvFamilyMaintenance.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Family Code value.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property FamilyCode() As String Implements Presenter.IFamilyMaintenanceView.FamilyCode
        Get
            Return txtftrFamilyCode.Text
        End Get
        Set(ByVal value As String)
            txtftrFamilyCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Family Desc value.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property FamilyDesc() As String Implements Presenter.IFamilyMaintenanceView.FamilyDesc
        Get
            Return txtftrFamilyDesc.Text
        End Get
        Set(ByVal value As String)
            txtftrFamilyDesc.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Application cat value.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ApplicationCat() As String Implements Presenter.IFamilyMaintenanceView.ApplicationCat
        Get
            Return txtftrApplicationCat.Text
        End Get
        Set(ByVal value As String)
            txtftrApplicationCat.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Construction Type value.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ConstructionType() As String Implements Presenter.IFamilyMaintenanceView.ConstructionType
        Get
            Return txtftrConstructionType.Text
        End Get
        Set(ByVal value As String)
            txtftrConstructionType.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Structure Type value.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property StructureType() As String Implements Presenter.IFamilyMaintenanceView.StructureType
        Get
            Return txtftrStructureType.Text
        End Get
        Set(ByVal value As String)
            txtftrStructureType.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Mounting Type value.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property MountingType() As String Implements Presenter.IFamilyMaintenanceView.MountingType
        Get
            Return txtftrMountingType.Text
        End Get
        Set(ByVal value As String)
            txtftrMountingType.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Aspect ratio cat.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property AspectRatioCat() As String Implements Presenter.IFamilyMaintenanceView.AspectRatioCat
        Get
            Return txtftrAspectRatioCat.Text
        End Get
        Set(ByVal value As String)
            txtftrAspectRatioCat.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Speed Rating cat value.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property SpeedRatingCat() As String Implements Presenter.IFamilyMaintenanceView.SpeedRatingCat
        Get
            Return txtftrSpeedRatingCat.Text
        End Get
        Set(ByVal value As String)
            txtftrSpeedRatingCat.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Load Index cat value.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property LoadIndexCat() As String Implements Presenter.IFamilyMaintenanceView.LoadIndexCat
        Get
            Return txtftrLoadIndexCat.Text
        End Get
        Set(ByVal value As String)
            txtftrLoadIndexCat.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Quality user value.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
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

    ''' <summary>
    ''' Gets or sets Imark Certificates.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ImarkCertificates() As System.Data.DataTable Implements Presenter.IFamilyMaintenanceView.ImarkCertificates
        Get
            Return CType(ddImark.DataSource, DataTable)
        End Get
        Set(ByVal value As System.Data.DataTable)
            ddImark.DataSource = value
            ddImark.DataTextField = value.Columns(1).ColumnName.ToString

            ddImark.DataValueField = value.Columns(0).ColumnName.ToString
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Imark Certificate selected value.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ImarkCertificateSelected() As Long Implements Presenter.IFamilyMaintenanceView.ImarkCertificateSelected
        Get
            Dim Imarkselection As Long = CLng(ddImark.SelectedItem.Value)

            Return Imarkselection
        End Get
        Set(ByVal value As Long)
            ddImark.SelectedValue = CStr(value)
        End Set
    End Property

#End Region
#Region "Methods"
    ''' <summary>
    ''' Setup family maintenance.
    ''' </summary>
    ''' <param name="p_strCertificationName">Certification Name</param>
    ''' <param name="p_blnFamilymaint">Family maint</param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub SetupViewData(ByVal p_strCertificationName As String, ByVal p_blnFamilymaint As Boolean) Implements Presenter.IFamilyMaintenanceView.SetupViewData
        Try
            'Dim pn_certificateid = Me.ddImark.SelectedItem
            If p_blnFamilymaint Then
                CertificationName = p_strCertificationName
                RaiseEvent LoadViewData()
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' This event fires when we click delete/edit link of any row.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Grid_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        Const EditFamilyText As String = "EditFamily"
        Const DeleteFamilyText As String = "DeleteFamily"
        Try
            Dim pn_certificateid As ListItem = Me.ddImark.SelectedItem
            If e.CommandName = EditFamilyText Then
                RaiseEvent EditFamily(e.CommandArgument)
                ' Making the edit/add panel visible
                divFamilyDetails.Visible = True
            End If

            If e.CommandName = DeleteFamilyText Then
                Me.DeleteConfirmPopUp.Show()
                Me.hidConfirm.Value = CStr(e.CommandArgument)
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' This event fires when we click on Save.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_SaveFamily(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'Dim pn_certificateid = Me.ddImark.SelectedItem
            RaiseEvent SaveFamily()
            If ErrorText.Length <= 0 Then
                divFamilyDetails.Visible = False
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' This event fires when we click on Cancel. 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_CancelFamily(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            divFamilyDetails.Visible = False
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' This event fires when we click on Add.  
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_AddFamily(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            RaiseEvent AddFamily()
            'Dim pn_certificateid = Me.ddImark.SelectedItem.value
            divFamilyDetails.Visible = True
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Event to delete a Family on confirm.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_DeleteConfirm(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.DeleteConfirmPopUp.Dispose()
            'Dim pn_certificateid = Me.ddImark.SelectedItem.Value
            RaiseEvent DeleteFamily(Me.hidConfirm.Value)
            Me.hidConfirm.Value = String.Empty
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Event to skip Family deletion on unconfirm.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_DeleteCancel(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.DeleteConfirmPopUp.Dispose()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Event to select Imark for Families to edit.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>11/04/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub ddImark_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddImark.SelectedIndexChanged
        Try
            Dim pn_certificateid As String = Me.ddImark.SelectedItem.Value
            RaiseEvent ChangeCertificate(ddImark.SelectedItem.Value)
        Catch
            Throw
        End Try
    End Sub
#End Region

End Class


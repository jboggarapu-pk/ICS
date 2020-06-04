Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common

' Add Certification form for all types
Partial Public Class AddCertificationUC
    Inherits BaseUserControl
    Implements IAddCertificationView

    ' Changed the sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation

#Region "Members"
    ''' <summary>
    '''  Save event
    ''' </summary>
    Public Event Save As CustomEvents.PlainEventHandler Implements IAddCertificationView.Save
    ''' <summary>
    '''  Reload View Data event
    ''' </summary>
    Public Event ReloadViewData() Implements Presenter.IAddCertificationView.ReloadViewData
    'Public Event Finish As CustomEvents.PlainEventHandler Implements IAddCertificationView.Finish
    ''' <summary>
    '''  Check SKU Exist event
    ''' </summary>
    Public Event CheckSKUExist As CustomEvents.CheckSKUExistEventHandler Implements IAddCertificationView.CheckSKUExist
    ''' <summary>
    '''  Check Duplicate Certificate Number event
    ''' </summary>
    Public Event CheckDuplicateCertificateNumber As CustomEvents.PlainEventHandler Implements IAddCertificationView.CheckDuplicateCertificateNumber
    ''' <summary>
    '''  Add Certifiation presenter
    ''' </summary>
    Private m_presenter As AddCertificationPresenter
    ''' <summary>
    '''  importers
    ''' </summary>
    Private m_importers As DataSet
    ''' <summary>
    '''  Error Number
    ''' </summary>
    Private m_ErrorNum As Integer
    ''' <summary>
    '''  InsertPC
    ''' </summary>
    Private m_InsertPC As String
    ''' <summary>
    '''  ErrorDesc
    ''' </summary>
    Private m_ErrorDesc As String
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New()

        m_presenter = New AddCertificationPresenter(Me)

    End Sub

#End Region

#Region "Properties"
    ''' <summary>
    '''  Gets or sets Success Text value.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property SuccessText() As String Implements Presenter.IAddCertificationView.SuccessText
        Get
            Return lblSuccessText.Text
        End Get
        Set(ByVal value As String)
            lblSuccessText.Text = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Error Text value.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    ''' </summary>
    Public Property ErrorText() As String Implements Presenter.IAddCertificationView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Certificate Number value.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property CertificateNumber() As String Implements Presenter.IAddCertificationView.CertificateNumber
        Get
            Return txtCertNumber.Text
        End Get
        Set(ByVal value As String)
            txtCertNumber.Text = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Extension value.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property Extension() As String Implements Presenter.IAddCertificationView.Extension
        Get
            Return txtExtension.Text
        End Get
        Set(ByVal value As String)
            txtExtension.Text = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Certification Name value.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property CertificationName() As String Implements Presenter.IAddCertificationView.CertificationName
        Get
            Return CStr(Session("AddCertName"))
        End Get
        Set(ByVal value As String)
            Session("AddCertName") = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Add Certification Title value.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property AddCertificationTitle() As String Implements Presenter.IAddCertificationView.AddCertificationTitle
        Get
            Return lblFormTitle.Text
        End Get
        Set(ByVal value As String)
            lblFormTitle.Text = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Material List value.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property MaterialList() As List(Of String) Implements Presenter.IAddCertificationView.MatlNumList
        Get
            Return CType(Session("MaterialNumList"), Global.System.Collections.Generic.List(Of String))
        End Get
        Set(ByVal value As List(Of String))
            Session("MaterialNumList") = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets CertNumErrMsgFlag value.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property CertNumErrMsgFlag() As Boolean Implements Presenter.IAddCertificationView.CertNumErrMsgFlag
        Get
            Return lblErrCertNumber.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrCertNumber.Visible = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets ExtensionErrMsgFlag value.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ExtensionErrMsgFlag() As Boolean Implements Presenter.IAddCertificationView.ExtensionErrMsgFlag
        Get
            Return lblErrExtension.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrExtension.Visible = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Importers value.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property Importers() As DataTable Implements IAddCertificationView.Importers
        Get
            Return CType(ddlImporterNumber_N.DataSource, DataTable)
        End Get
        Set(ByVal value As DataTable)
            ddlImporterNumber_N.DataSource = value
            ddlImporterNumber_N.DataValueField = "importerid"
            ddlImporterNumber_N.DataTextField = "importer"
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Importer value.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property Importer() As String Implements IAddCertificationView.Importer
        Get
            Return ddlImporterNumber_N.SelectedValue
        End Get
        Set(ByVal value As String)
            ddlImporterNumber_N.SelectedValue = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Customers value.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property Customers() As DataTable Implements IAddCertificationView.Customers
        Get
            Return CType(ddlCustomerNumber_N.DataSource, DataTable)
        End Get
        Set(ByVal value As DataTable)
            ddlCustomerNumber_N.DataSource = value
            ddlCustomerNumber_N.DataValueField = "customerid"
            ddlCustomerNumber_N.DataTextField = "customer"
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Customer value.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property Customer() As String Implements IAddCertificationView.Customer
        Get
            Return ddlCustomerNumber_N.SelectedValue
        End Get
        Set(ByVal value As String)
            ddlCustomerNumber_N.SelectedValue = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets ErrorNum value.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ErrorNum() As Integer Implements IAddCertificationView.ErrorNum
        Get
            Return m_ErrorNum
        End Get
        Set(ByVal value As Integer)
            m_ErrorNum = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets ErrorDesc value.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ErrorDesc() As String Implements IAddCertificationView.ErrorDesc
        Get
            Return m_ErrorDesc
        End Get
        Set(ByVal value As String)
            m_ErrorDesc = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets InsertPC value.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property InsertPC() As String Implements IAddCertificationView.InsertPC
        Get
            Return m_InsertPC
        End Get
        Set(ByVal value As String)
            m_InsertPC = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Setup view data if certificate is new.
    ''' </summary>
    ''' <param name="p_strCertificationName">Certification name</param>
    ''' <param name="p_blnAnew">A New</param>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub SetupViewData(ByVal p_strCertificationName As String, ByVal p_blnAnew As Boolean) Implements Presenter.IAddCertificationView.SetupViewData
        Const ECE3054Text As String = "ECE3054"
        Const ECE117Text As String = "ECE117"
        Try
            If p_blnAnew Then
                CertificationName = p_strCertificationName
                'Added as per project 2706 technical specification
                'Show extension based on certification type
                If CertificationName = ECE3054Text Or CertificationName = ECE117Text Then
                    ShowExtension(True)
                Else
                    ShowExtension(False)
                End If
                RaiseEvent ReloadViewData()
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Set up the add certification view based on the certification type.
    ''' </summary>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub SetupAddCertificationView() Implements IAddCertificationView.SetupAddCertificationView
        Const Text As String = "544"
        Const ImarkText As String = "Imark"
        Const NOMText As String = "NOM"
        Const GSOText As String = "GSO"

        Try
            'set up the cert number based on certification type
            Select Case CertificationName
                Case ImarkText
                    'pnlAddCert_I.Visible = True
                    txtCertNumber.Text = Text
                    txtCertNumber.ReadOnly = False  'Allow certificate number to be changed. - jeseitz 3/10/16
                Case NOMText
                    pnlAddCert_N.Visible = True
                    txtCertNumber.ReadOnly = False
                Case GSOText
                    txtCertNumber.ReadOnly = False
                    pnlAddCert_I.Visible = False
                    pnlAddCert_N.Visible = False
                Case Else
                    pnlAddCert_I.Visible = False
                    pnlAddCert_N.Visible = False
                    txtCertNumber.ReadOnly = False
            End Select

            're-populate the Material table since it's auto generated
            tblMatlNum.Rows.Clear()
            Dim i As Integer
            If MaterialList IsNot Nothing AndAlso MaterialList.Count <> 0 Then
                For i = 0 To MaterialList.Count - 1
                    AddSKUTableRow(MaterialList(i))
                Next
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Set up Certificate number error msg.
    ''' </summary>
    ''' <param name="p_blnDuplicateFlag">Duplicate Flag</param>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub SetupCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean) Implements Presenter.IAddCertificationView.SetupCertNumErrMsg
        Try
            lblErrCertNumber.Visible = p_blnDuplicateFlag
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Set up Extension error msg.
    ''' </summary>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub SetupExtensionErrMsg(ByVal p_blnDuplicateFlag As Boolean) Implements Presenter.IAddCertificationView.SetupExtensionErrMsg
        Try
            lblErrExtension.Visible = p_blnDuplicateFlag
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Dynamically add Material number textbox to the view.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_btnAddMaterial(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddMaterial.Click

        SuccessText = "&nbsp;"
        ErrorText = String.Empty
        Try
            If MaterialList IsNot Nothing Then
                If MaterialList.Count <> 0 Then
                    If String.IsNullOrEmpty(MaterialList(MaterialList.Count - 1)) Then
                        Me.ConfirmPopUp.Show()
                        Return
                    End If
                End If
            End If
            AddSKUTableRow(String.Empty)
            'Add to the Material list session object
            If MaterialList Is Nothing Then
                MaterialList = New List(Of String)
            End If
            MaterialList.Add(String.Empty)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Click button save.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_btnSave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        SuccessText = "&nbsp;"
        ErrorText = String.Empty
        Try
            If CheckForErrorMessages() Then
                RaiseEvent Save()
                If ErrorNum = 7 Then
                    lblErr2.Text = m_ErrorDesc
                    ConfirmPopUpErr2.Show()
                End If
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Click button cancel.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_btnCancel(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            If CertificateNumber IsNot String.Empty Or MaterialList IsNot Nothing Then
                Me.CancelAlertPopUp.Show()
            Else
                'jeseitz 6/15/2016 - added 0 as second parameter - not used in this case.
                CType(Me.Page, CertificationSearchEx).ActivateCertificateControl(String.Empty, 0)
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Click Ok.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_OK(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.ConfirmPopUp.Dispose()
            RaiseEvent Save()
            If ErrorNum = 7 Then
                lblErr2.Text = m_ErrorDesc
                ConfirmPopUpErr2.Show()
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Confirm Button Click.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_Confirm(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.CancelAlertPopUp.Dispose()
            'jeseitz 6/15/2016 - added 0 as second parameter - not used in this case.
            CType(Me.Page, CertificationSearchEx).ActivateCertificateControl(String.Empty, 0)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Show or hide Extension label and textbox.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_Cancel(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.CancelAlertPopUp.Dispose()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Setup the Material Table rows.
    ''' </summary>
    ''' <param name="p_strMatlNum">Material number</param>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub AddSKUTableRow(ByVal p_strMatlNum As String)
        Const RightText As String = "right"
        Const LeftText As String = "left"
        Const LblMaterialText As String = "lblMaterial"
        Const MaterialText As String = "Material :"
        Const AddCertificationSKULabelText As String = "AddCertificationSKULable"
        Const TxtMaterial As String = "txtMaterial"
        Const LblErrMaterialText As String = "lblErrMaterial"
        Const MaterialNumberDoesNotExistText As String = "The Material Number does not exist."
        Const AddCertificationSKUErrorText As String = "AddCertificationSKUError"

        Try
            Dim tRow As New HtmlTableRow()
            Dim rowCount As Integer = tblMatlNum.Rows.Count
            Dim tCellLeft As New HtmlTableCell()
            tCellLeft.Align = RightText
            Dim tCellRight As New HtmlTableCell()
            Dim tCellError As New HtmlTableCell()
            tCellError.Width = CStr(320)
            tCellError.Align = LeftText

            Dim lblMaterialNum As New Label()
            Dim txtMaterialNum As New TextBox()
            Dim lblErr As New Label()

            lblMaterialNum.ID = LblMaterialText + rowCount.ToString
            lblMaterialNum.Text = MaterialText
            lblMaterialNum.CssClass = AddCertificationSKULabelText
            lblMaterialNum.Width = 100
            lblMaterialNum.Font.Size = FontUnit.XSmall

            txtMaterialNum.ID = TxtMaterial + rowCount.ToString
            txtMaterialNum.Text = p_strMatlNum
            AddHandler txtMaterialNum.TextChanged, AddressOf Me.TextChanged_txtMaterialNum
            txtMaterialNum.Font.Size = FontUnit.XSmall
            txtMaterialNum.Width = 170
            txtMaterialNum.AutoPostBack = False

            lblErr.ID = LblErrMaterialText + rowCount.ToString
            lblErr.Text = MaterialNumberDoesNotExistText
            lblErr.CssClass = AddCertificationSKUErrorText
            lblErr.Visible = False
            lblErr.Width = 320
            lblErr.Font.Size = FontUnit.XSmall

            tCellLeft.Controls.Add(lblMaterialNum)
            tCellRight.Controls.Add(txtMaterialNum)
            tCellError.Controls.Add(lblErr)
            tRow.Cells.Add(tCellLeft)
            tRow.Cells.Add(tCellRight)
            tRow.Cells.Add(tCellError)

            tblMatlNum.Rows.Add(tRow)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Called when textbox textchanged event raised.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub TextChanged_txtMaterialNum(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim txtMaterial As TextBox = CType(sender, TextBox)
            Dim intIndex As Integer = CInt(txtMaterial.ID.Substring(11))
            RaiseEvent CheckSKUExist(txtMaterial.Text, intIndex)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Set up SKU error msg.
    ''' </summary>
    ''' <param name="p_intIndex">Index</param>
    ''' <param name="p_blnExistFlag">Exist Flag</param>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub SetupSKUErrMsg(ByVal p_intIndex As Integer, ByVal p_blnExistFlag As Boolean) Implements Presenter.IAddCertificationView.SetupSKUErrMsg
        Const ErrMaterialNumText As String = "ErrMaterialNum"
        Try
            For Each tbrRow As HtmlTableRow In tblMatlNum.Rows
                If CType(tbrRow.Cells(2).Controls(0), Label).ID.Contains(ErrMaterialNumText + p_intIndex.ToString()) Then
                    CType(tbrRow.Cells(2).Controls(0), Label).Visible = Not p_blnExistFlag
                    Exit For
                End If
            Next
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Check for error messages.
    ''' </summary>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Function CheckForErrorMessages() As Boolean
        Try
            If lblErrCertNumber.Visible Or lblErrExtension.Visible Then
                Return False
            End If
            For Each tbrRow As HtmlTableRow In tblMatlNum.Rows
                If CType(tbrRow.Cells(2).Controls(0), Label).Visible Then
                    Return False
                End If
            Next
            Return True
        Catch
            Throw
        End Try
    End Function

    'Added as per project 2706 technical specification
    ''' <summary>
    ''' Show or hide Extension label and textbox.
    ''' </summary>
    ''' <param name="p_blnShowFlag">Show Flag</param>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub ShowExtension(ByVal p_blnShowFlag As Boolean)
        Try
            lblExtension.Visible = p_blnShowFlag
            txtExtension.Visible = p_blnShowFlag
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' button Ok Err2 Click.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub btnOkErr2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOkErr2.Click
        InsertPC = "y"
        Try
            RaiseEvent Save()
            ConfirmPopUpErr2.Dispose()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' button Cancle Err2 Click.
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub btnCancleErr2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancleErr2.Click
        Try
            ConfirmPopUpErr2.Dispose()
        Catch
            Throw
        End Try
    End Sub

#End Region

End Class
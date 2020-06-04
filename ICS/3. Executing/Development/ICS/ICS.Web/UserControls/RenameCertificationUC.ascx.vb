Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common

' Rename Certification form for all types
Partial Public Class RenameCertificationUC
    Inherits BaseUserControl
    Implements IRenameCertificationView

#Region "Members"
    ''' <summary>
    ''' variable to hold Check For Certified Materials.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event CheckForCertifiedMaterials As CustomEvents.PlainEventHandler Implements IRenameCertificationView.CheckForCertifiedMaterials

    ''' <summary>
    ''' variable to hold save Rename Certification View.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Save As CustomEvents.PlainEventHandler Implements IRenameCertificationView.Save

    ''' <summary>
    ''' variable to hold Reload View Data.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ReloadViewData() Implements Presenter.IRenameCertificationView.ReloadViewData

    ''' <summary>
    ''' variable to hold Rename Certification Presenter.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_presenter As RenameCertificationPresenter

#End Region

#Region "Constructors"

    ''' <summary>
    ''' Constructor for this class.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/06/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Sub New()

        m_presenter = New RenameCertificationPresenter(Me)

    End Sub

#End Region

#Region "Properties"
    ''' <summary>
    ''' Gets or sets Success Text value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Success Text.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SuccessText() As String Implements Presenter.IRenameCertificationView.SuccessText
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
    ''' <value>String</value>
    ''' <returns>Error Text.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ErrorText() As String Implements Presenter.IRenameCertificationView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Old Certificate Number value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Old Certificate Number.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property OldCertificateNumber() As String Implements Presenter.IRenameCertificationView.OldCertificateNumber
        Get
            Return txtOldCertNumber.Text
        End Get
        Set(ByVal value As String)
            txtOldCertNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets New Certificate Number value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>New Certificate Number.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property NewCertificateNumber() As String Implements Presenter.IRenameCertificationView.NewCertificateNumber
        Get
            Return txtNewCertNumber.Text
        End Get
        Set(ByVal value As String)
            txtNewCertNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Old Extension value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Old Extension.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property OldExtension() As String Implements Presenter.IRenameCertificationView.OldExtension
        Get
            Return txtOldExtension.Text
        End Get
        Set(ByVal value As String)
            txtOldExtension.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets New Extension value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>New Extension.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property NewExtension() As String Implements Presenter.IRenameCertificationView.NewExtension
        Get
            Return txtNewExtension.Text
        End Get
        Set(ByVal value As String)
            txtNewExtension.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certification Name value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Certification Name.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property CertificationName() As String Implements Presenter.IRenameCertificationView.CertificationName
        Get
            Return CStr(Session("AddCertName"))
        End Get
        Set(ByVal value As String)
            Session("AddCertName") = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Add Certification Title value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Add Certification Title.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property AddCertificationTitle() As String Implements Presenter.IRenameCertificationView.AddCertificationTitle
        Get
            Return lblFormTitle.Text
        End Get
        Set(ByVal value As String)
            lblFormTitle.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets old Certificate Number Error Message Flag value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>old Certificate Number Error Message Flag.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property OldCertNumErrMsgFlag() As Boolean Implements Presenter.IRenameCertificationView.OldCertNumErrMsgFlag
        Get
            Return lblErrOldCertNumber.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrOldCertNumber.Visible = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets New Certificate Number Error Message Flag value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>New Certificate Number Error Message Flag.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property NewCertNumErrMsgFlag() As Boolean Implements Presenter.IRenameCertificationView.NewCertNumErrMsgFlag
        Get
            Return lblErrNewCertNumber.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrNewCertNumber.Visible = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Old Extension Error message Flag value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>Old Extension Error message Flag.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property OldExtensionErrMsgFlag() As Boolean Implements Presenter.IRenameCertificationView.OldExtensionErrMsgFlag
        Get
            Return lblErrOldExtension.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrOldExtension.Visible = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets New Extension Error Message Flag value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>New Extension Error Message Flag.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property NewExtensionErrMsgFlag() As Boolean Implements Presenter.IRenameCertificationView.NewExtensionErrMsgFlag
        Get
            Return lblErrNewExtension.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrNewExtension.Visible = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Warning Message value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Warning Message.</returns>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property WarningMessage() As String Implements Presenter.IRenameCertificationView.WarningMessage
        Get
            Return lblWarningMessage.Text
        End Get
        Set(ByVal value As String)
            lblWarningMessage.Text = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Method to Setup view data if certificate is new.
    ''' </summary> 
    ''' <param name="p_blnRename">Rename</param>
    ''' <param name="p_strCertificationName">Certification Name</param>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Sub SetupViewData(ByVal p_strCertificationName As String, ByVal p_blnRename As Boolean) Implements Presenter.IRenameCertificationView.SetupViewData
        Try
            If p_blnRename Then
                CertificationName = p_strCertificationName
                RaiseEvent ReloadViewData()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    ''' <summary>
    ''' Method to Setup the add certification view based on the certification type.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>  
    Public Sub SetupAddCertificationView() Implements IRenameCertificationView.SetupRenameCertificationView
        Try
            'Show extension based on certification type
            If CertificationName = "ECE3054" Or CertificationName = "ECE117" Then
                ShowExtension(True)
            Else
                ShowExtension(False)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Method to Setup Old Certificate Number Error Message.
    ''' </summary>
    ''' <param name="p_blnDuplicateFlag">Duplicate Flag</param>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Public Sub SetupOldCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean) Implements Presenter.IRenameCertificationView.SetupOldCertNumErrMsg
        Try
            lblErrOldCertNumber.Visible = p_blnDuplicateFlag
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Method to Setup New Certificate Number Error Message.
    ''' </summary>
    ''' <param name="p_blnDuplicateFlag">Duplicate Flag</param>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Public Sub SetupNewCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean) Implements Presenter.IRenameCertificationView.SetupNewCertNumErrMsg
        Try
            lblErrNewCertNumber.Visible = p_blnDuplicateFlag
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Method to Setup Old CExtension Error Message.
    ''' </summary> 
    ''' <param name="p_blnDuplicateFlag">Duplicate Flag</param>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Public Sub SetupOldCExtensionErrMsg(ByVal p_blnDuplicateFlag As Boolean) Implements Presenter.IRenameCertificationView.SetupOldExtensionErrMsg
        Try
            lblErrOldExtension.Visible = p_blnDuplicateFlag
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Method  to Setup New Extension Error Message.
    ''' </summary>
    ''' <param name="p_blnDuplicateFlag">Duplicate Flag</param>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Public Sub SetupNewExtensionErrMsg(ByVal p_blnDuplicateFlag As Boolean) Implements Presenter.IRenameCertificationView.SetupNewExtensionErrMsg
        Try
            lblErrNewExtension.Visible = p_blnDuplicateFlag
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Save Button Click .
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">e</param>
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_btnSave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            ClearMessages()

            If CheckForErrorMessages() Then
                RaiseEvent CheckForCertifiedMaterials()
                If Not String.IsNullOrEmpty(WarningMessage) Then
                    Me.ConfirmPopUp.Show()
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Cancel Button Click .
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">e</param>
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_btnCancel(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            If OldCertificateNumber IsNot String.Empty Or NewCertificateNumber IsNot Nothing Then
                Me.CancelAlertPopUp.Show()
            Else
                'jeseitz 6/15/2016 - added 0 as second parameter - not used in this case.
                CType(Me.Page, CertificationSearchEx).ActivateCertificateControl(String.Empty, 0)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Save Confirm Button Click .
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">e</param>
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_SaveConfirm(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            ClearMessages()
            RaiseEvent Save()
            Me.ConfirmPopUp.Dispose()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Save Button Click .
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">e</param>
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_SaveCancel(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.ConfirmPopUp.Dispose()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Confirm Button Click .
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">e</param>
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
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
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Cancel Button Click .
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">e</param>
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks> 
    Protected Sub Click_Cancel(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.CancelAlertPopUp.Dispose()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Method to Setup view data if certificate is new.
    ''' </summary>
    ''' <returns>Boolean.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Private Function CheckForErrorMessages() As Boolean
        Try
            If lblErrOldCertNumber.Visible Or lblErrNewCertNumber.Visible Or _
           lblErrOldExtension.Visible Or lblErrNewExtension.Visible Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to Show or hide extension controls.
    ''' </summary>
    ''' <param name="p_blnShow">Show</param>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Private Sub ShowExtension(ByVal p_blnShow As Boolean)
        Try
            lblOldExtension.Visible = p_blnShow
            txtOldExtension.Visible = p_blnShow
            lblNewExtension.Visible = p_blnShow
            txtNewExtension.Visible = p_blnShow
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Method to Clear Messages.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/05/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>  
    Private Sub ClearMessages()
        Try
            SuccessText = "&nbsp;"
            ErrorText = String.Empty
            WarningMessage = String.Empty
        Catch ex As Exception
            Throw
        End Try
    End Sub

#End Region

End Class
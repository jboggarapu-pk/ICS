Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common

' Delete Certification form for all types
Partial Public Class DeleteCertificationUC
    Inherits BaseUserControl
    Implements IDeleteCertificationView

#Region "Members"
    ''' <summary>
    ''' Check for Certified field Materials event.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event CheckForCertifiedMaterials As CustomEvents.PlainEventHandler Implements IDeleteCertificationView.CheckForCertifiedMaterials
    ''' <summary>
    ''' Save event.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Save As CustomEvents.PlainEventHandler Implements IDeleteCertificationView.Save
    ''' <summary>
    ''' Reload view data event.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ReloadViewData() Implements Presenter.IDeleteCertificationView.ReloadViewData
    ''' <summary>
    ''' Delete Certification presenter.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_presenter As DeleteCertificationPresenter

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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New()

        m_presenter = New DeleteCertificationPresenter(Me)

    End Sub

#End Region

#Region "Properties"
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property SuccessText() As String Implements Presenter.IDeleteCertificationView.SuccessText
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ErrorText() As String Implements Presenter.IDeleteCertificationView.ErrorText
        Get
            Return lblErrorText.Text
        End Get
        Set(ByVal value As String)
            lblErrorText.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certificate Number value.
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property CertificateNumber() As String Implements Presenter.IDeleteCertificationView.CertificateNumber
        Get
            Return txtCertNumber.Text
        End Get
        Set(ByVal value As String)
            txtCertNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Extension value.
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property Extension() As String Implements Presenter.IDeleteCertificationView.Extension
        Get
            Return txtExtension.Text
        End Get
        Set(ByVal value As String)
            txtExtension.Text = value
        End Set
    End Property

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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property CertificationName() As String Implements Presenter.IDeleteCertificationView.CertificationName
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property AddCertificationTitle() As String Implements Presenter.IDeleteCertificationView.AddCertificationTitle
        Get
            Return lblFormTitle.Text
        End Get
        Set(ByVal value As String)
            lblFormTitle.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certificate Number Error Messge Flag value.
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property CertNumErrMsgFlag() As Boolean Implements Presenter.IDeleteCertificationView.CertNumErrMsgFlag
        Get
            Return lblErrCertNumber.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrCertNumber.Visible = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Extension Error Messge Flag value.
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property ExtensionErrMsgFlag() As Boolean Implements Presenter.IDeleteCertificationView.ExtensionErrMsgFlag
        Get
            Return lblErrExtension.Visible
        End Get
        Set(ByVal value As Boolean)
            lblErrExtension.Visible = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Warning Message value.
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property WarningMessage() As String Implements Presenter.IDeleteCertificationView.WarningMessage
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
    ''' Setup view data if certificate is new.
    ''' </summary>
    ''' <param name="p_strCertificationName">Certification name</param>
    ''' <param name="p_blnDelete">Delete</param>
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub SetupViewData(ByVal p_strCertificationName As String, ByVal p_blnDelete As Boolean) Implements Presenter.IDeleteCertificationView.SetupViewData
        Try
            If p_blnDelete Then
                CertificationName = p_strCertificationName
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub SetupAddCertificationView() Implements IDeleteCertificationView.SetupDeleteCertificationView
        Const ECE3054Text As String = "ECE3054"
        Const ECE117Text As String = "ECE117"
        Try
            'Show extension based on certification type
            If CertificationName = ECE3054Text Or CertificationName = ECE117Text Then
                ShowExtension(True)
            Else
                ShowExtension(False)
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Set up Certificate Number Error Message.
    ''' </summary>
    ''' <param name="p_blnDuplicateFlag">Duplicate flag</param>
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub SetupCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean) Implements Presenter.IDeleteCertificationView.SetupCertNumErrMsg
        Try
            lblErrCertNumber.Visible = p_blnDuplicateFlag
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Set up Extension Error message.
    ''' </summary>
    ''' <param name="p_blnDuplicateFlag">Duplicate flag</param>
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub SetupExtensionErrMsg(ByVal p_blnDuplicateFlag As Boolean) Implements Presenter.IDeleteCertificationView.SetupExtensionErrMsg
        Try
            lblErrExtension.Visible = p_blnDuplicateFlag
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Save button click.
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
    ''' <para>10/30/2019</para>
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
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Save Confirm button click.
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
    ''' <para>10/30/2019</para>
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
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Save Cance button click.
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_SaveCancel(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.ConfirmPopUp.Dispose()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Cancel button click.
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_btnCancel(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            If CertificateNumber IsNot String.Empty Then
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
    ''' Confirm button click.
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_Confirm(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.CancelAlertPopUp.Dispose()
            CType(Me.Page, CertificationSearchEx).ActivateCertificateControl(String.Empty, 0)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Cancel button click.
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
    ''' <para>10/30/2019</para>
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
    ''' Check for Error Messages.
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
    ''' <para>10/30/2019</para>
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
            Return True
        Catch
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Show or hide extension controls.
    ''' </summary>
    ''' <param name="p_blnShow">Show</param>
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub ShowExtension(ByVal p_blnShow As Boolean)
        Try
            lblExtension.Visible = p_blnShow
            txtExtension.Visible = p_blnShow
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Clear all messages.
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
    ''' <para>10/30/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub ClearMessages()
        Try
            SuccessText = "&nbsp;"
            ErrorText = String.Empty
            WarningMessage = String.Empty
        Catch
            Throw
        End Try
    End Sub
#End Region

End Class
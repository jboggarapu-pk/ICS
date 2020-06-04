Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Archive certification presenter
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
''' <para>10/15/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class ArchiveCertificationPresenter

#Region "Members"
    ''' <summary>
    '''  Interface to the Archive certification user control view.
    ''' </summary>
    Private m_view As IArchiveCertificationView

#End Region

#Region "Constructors"
    ''' <summary>
    '''  Custom Constructor to initialize class members.
    ''' </summary>
    ''' <param name="p_view">View</param>
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New(ByVal p_view As IArchiveCertificationView)
        Const ErrorCreatingText As String = "Error creating "
        Try
            m_view = p_view
            SubscribeToEvents()
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw New Exception(ErrorCreatingText + Me.ToString())
        End Try

    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Attach presenter to view’s events.
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub SubscribeToEvents()
        Try
            AddHandler m_view.ReloadViewData, AddressOf OnReloadViewData
            AddHandler m_view.Save, AddressOf OnSave
            AddHandler m_view.LoadView, AddressOf OnLoadView
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Load data from business process.
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub LoadViewData()
        Const ArchiveCertificateText As String = "Archive Certificate"
        Try
            m_view.ArchiveCertificationTitle = ArchiveCertificateText

        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Load data for the view.
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnLoadView(ByVal sender As Object, ByVal e As EventArgs)
        Const ErrorLoadFormDataText As String = "Error loading form data."
        Try
            LoadViewData()
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorLoadFormDataText
        End Try
    End Sub

    ''' <summary>
    ''' Reload data for the view - start anew.
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnReloadViewData()
        Try
            ' Flush all properties:
            m_view.CertificateNumber = String.Empty
            m_view.ErrorText = String.Empty
            m_view.SuccessText = String.Empty
            m_view.CertNumErrMsgFlag = False
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Archives the certificate.
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnSave()

        Dim blnSaveSuccess As Boolean
        Dim objArchiveCertificationModel As New ArchiveCertificationModel
        Const ErrorCertNumberEmptyText As String = "Certificate Number can not be empty."
        Const SaveText As String = "Saved."
        Const ErrorSaveFailedText As String = "Save Failed."
        Const ErrorArchiveCertificationText As String = "Error archiving certification."

        Try
            If String.IsNullOrEmpty(m_view.CertificateNumber) Then
                m_view.ErrorText = ErrorCertNumberEmptyText
                Return
            End If

            blnSaveSuccess = objArchiveCertificationModel.ArchiveCertification(m_view.CertificateNumber)

            If blnSaveSuccess Then
                m_view.SuccessText = SaveText
            Else
                m_view.ErrorText = ErrorSaveFailedText
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorArchiveCertificationText
        End Try
    End Sub

    ''' <summary>
    ''' Check certificate number exists.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
    ''' <returns>Error Messge</returns> 
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function CheckCertificateNumberExists() As String

        Dim strErrorMessage As String = String.Empty
        Const ErrorCertNumberNullText As String = "Certificate number cannot be null"
        Const ErrorInvalidCertNumberLengthText As String = "Invalid certificate number length"
        Const ErrorCertNumberNotFoundText As String = "Certificate number not found"
        Const ErrorValidatingCertNumberText As String = "Error validating certificate number."

        Try
            Dim blnCertNumExists As Boolean = False
            Dim objArchiveCertificationModel As New ArchiveCertificationModel

            If String.IsNullOrEmpty(m_view.CertificateNumber.Trim) Then
                strErrorMessage = ErrorCertNumberNullText
                Return strErrorMessage
            ElseIf m_view.CertificateNumber.Trim.Length < 4 Or m_view.CertificateNumber.Trim.Length > 20 Then
                strErrorMessage = ErrorInvalidCertNumberLengthText
                Return strErrorMessage
            End If

            blnCertNumExists = objArchiveCertificationModel.CheckCertificateNumberExists(m_view.CertificateNumber)
            If Not blnCertNumExists Then
                strErrorMessage = ErrorCertNumberNotFoundText
                Return strErrorMessage
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            strErrorMessage = ErrorValidatingCertNumberText
            Return strErrorMessage
        End Try

        Return strErrorMessage
    End Function
#End Region

End Class
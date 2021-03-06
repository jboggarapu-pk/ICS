Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Add certification presenter
''' </summary>
''' <remarks></remarks>
Public Class ArchiveCertificationPresenter

#Region "Members"

    Private m_view As IArchiveCertificationView

#End Region

#Region "Constructors"

    Public Sub New(ByVal p_view As IArchiveCertificationView)

        Try
            m_view = p_view
            SubscribeToEvents()
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw New Exception("Error creating " + Me.ToString())
        End Try

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Attach presenter to view�s events.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SubscribeToEvents()

        AddHandler m_view.ReloadViewData, AddressOf OnReloadViewData
        AddHandler m_view.Save, AddressOf OnSave
        AddHandler m_view.LoadView, AddressOf OnLoadView

    End Sub

    ''' <summary>
    ''' Load data from business process
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadViewData()

        m_view.ArchiveCertificationTitle = "Archive Certificate"

    End Sub

    ''' <summary>
    ''' Load data for the view
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnLoadView(ByVal sender As Object, ByVal e As EventArgs)

        Try
            LoadViewData()
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = "Error loading form data."
        End Try

    End Sub

    ''' <summary>
    ''' Reoad data for the view - start anew
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnReloadViewData()

        ' Flush all properties:
        m_view.CertificateNumber = String.Empty
        m_view.ErrorText = String.Empty
        m_view.SuccessText = String.Empty
        m_view.CertNumErrMsgFlag = False

    End Sub

    ''' <summary>
    ''' Archives the certificate
    ''' </summary>    
    Private Sub OnSave()

        Dim blnSaveSuccess As Boolean
        Dim objArchiveCertificationModel As New ArchiveCertificationModel

        Try
            If String.IsNullOrEmpty(m_view.CertificateNumber) Then
                m_view.ErrorText = "Certificate Number can not be empty."
                Return
            End If

            blnSaveSuccess = objArchiveCertificationModel.ArchiveCertification(m_view.CertificateNumber)

            If blnSaveSuccess Then
                m_view.SuccessText = "Saved."
            Else
                m_view.ErrorText = "Save Failed."
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error archiving certification."
        End Try

    End Sub

    Public Function CheckCertificateNumberExists() As String

        Dim strErrorMessage = String.Empty

        Try
            Dim blnCertNumExists As Boolean = False
            Dim objArchiveCertificationModel As New ArchiveCertificationModel

            If String.IsNullOrEmpty(m_view.CertificateNumber.Trim) Then
                strErrorMessage = "Certificate number cannot be null"
                Return strErrorMessage
            ElseIf m_view.CertificateNumber.Trim.Length < 4 Or m_view.CertificateNumber.Trim.Length > 20 Then
                strErrorMessage = "Invalid certificate number length"
                Return strErrorMessage
            End If

            blnCertNumExists = objArchiveCertificationModel.CheckCertificateNumberExists(m_view.CertificateNumber)
            If Not blnCertNumExists Then
                strErrorMessage = "Certificate number not found"
                Return strErrorMessage
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            strErrorMessage = "Error validating certificate number."
            Return strErrorMessage
        End Try

        Return strErrorMessage
    End Function

#End Region

End Class
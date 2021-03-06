Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Add certification presenter
''' </summary>
''' <remarks></remarks>
Public Class AttachCertificationPresenter

#Region "Members"

    Private Shared m_view As IAttachCertification = Nothing

#End Region

#Region "Constructors"

    Public Sub New(ByVal p_view As IAttachCertification)
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
        m_view.AttachCertificationTitle = "Attach Certificate"
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
        m_view.ExtensionEn = String.Empty
        m_view.CertificationTypeId = String.Empty
        m_view.SkuId = String.Empty
        m_view.ErrorText = String.Empty
        m_view.SuccessText = String.Empty
    End Sub

    ''' <summary>
    ''' Archives the certificate
    ''' </summary> 
    ''' <remarks></remarks>
    Private Sub OnSave()
        Dim strSaveSuccess As String = String.Empty
        Dim objAttachCertificateModel As AttachCertificateModel = Nothing

        Try
            m_view.SuccessText = String.Empty
            m_view.ErrorText = String.Empty

            If String.IsNullOrEmpty(m_view.CertificateNumber) Then
                m_view.ErrorText = "Enter valid Certificate Number."
                Return
            End If
            If String.IsNullOrEmpty(m_view.SkuId) Then
                m_view.ErrorText = "Enter Valid Sku Id."
                Return
            End If
            If String.IsNullOrEmpty(m_view.ExtensionEn) Then
                m_view.ErrorText = "Enter Valid Extension Number."
                Return
            End If
            If String.IsNullOrEmpty(m_view.CertificationTypeId) Then
                m_view.ErrorText = "Enter Valid Certificate Type Id."
                Return
            End If

            objAttachCertificateModel = New AttachCertificateModel
            strSaveSuccess = objAttachCertificateModel.AttachCertification(m_view.SkuId, m_view.CertificateNumber, m_view.ExtensionEn, m_view.CertificationTypeId)

            If String.IsNullOrEmpty(strSaveSuccess) Then
                m_view.SuccessText = "Saved."
            ElseIf strSaveSuccess.ToUpper() = "INVALID CERTIFICATE" Then
                m_view.ErrorText = "invalid certificate."
            ElseIf strSaveSuccess.ToUpper() = "SKUID DOES NOT EXIST" Then
                m_view.ErrorText = "skuid does not exist."
            ElseIf strSaveSuccess.ToUpper() = "DUPLICATE RECORD" Then
                m_view.ErrorText = "duplicate record."
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error attaching certification."
        End Try
    End Sub

    ''' <summary>
    ''' Check Certificate Number exists.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckCertificateNumberExists() As String
        Dim strErrorMessage = String.Empty
        Dim objAttachCertificationModel As AttachCertificateModel = Nothing
        Dim blnCertNumExists As Boolean = False

        Try
            If String.IsNullOrEmpty(m_view.CertificateNumber.Trim) Then
                strErrorMessage = "Certificate number cannot be null"
                Return strErrorMessage
            ElseIf m_view.CertificateNumber.Trim.Length < 4 Or m_view.CertificateNumber.Trim.Length > 20 Then
                strErrorMessage = "Invalid certificate number length"
                Return strErrorMessage
            End If

        Catch exc As Exception
            EventLogger.Enter(exc)
            strErrorMessage = "Error validating certificate number."
        End Try

        Return strErrorMessage
    End Function

#End Region

End Class








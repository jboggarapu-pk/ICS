Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Delete certification presenter
''' </summary>
''' <remarks></remarks>
Public Class DeleteCertificationPresenter

#Region "Members"

    Private m_view As IDeleteCertificationView

#End Region

#Region "Constructors"

    Public Sub New(ByVal p_view As IDeleteCertificationView)

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

        AddHandler m_view.LoadView, AddressOf OnLoadView
        AddHandler m_view.ReloadViewData, AddressOf OnReloadViewData
        AddHandler m_view.CheckForCertifiedMaterials, AddressOf OnCheckForCertifiedMaterials
        AddHandler m_view.Save, AddressOf OnSave

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
    ''' Reload data for the view - start anew
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnReloadViewData()

        Try
            ' Flush all properties:
            m_view.CertificateNumber = String.Empty
            m_view.Extension = String.Empty
            m_view.ErrorText = String.Empty
            m_view.SuccessText = String.Empty
            m_view.WarningMessage = String.Empty
            m_view.CertNumErrMsgFlag = False
            m_view.ExtensionErrMsgFlag = False

            LoadViewData()
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = "Error re-loading form data."
        End Try

    End Sub

    ''' <summary>
    ''' Load data from business process   
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadViewData()

        m_view.AddCertificationTitle = "Delete " + m_view.CertificationName + " certificate"
        m_view.SetupDeleteCertificationView()

    End Sub

    ''' <summary>
    ''' Check for certified materials
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnCheckForCertifiedMaterials()

        Dim intCertifiedMaterialCount As Integer
        Dim objCertificationActionModel As New CertificationActionModel
        Dim objCertModel As New CertificateModel
        Dim intCertTypeId As Integer = objCertModel.GetCertificateTypeID(m_view.CertificationName)

        Try
            If String.IsNullOrEmpty(m_view.CertificateNumber) Then
                m_view.ErrorText = "Certificate Number can not be empty."
                Return
            End If

            intCertifiedMaterialCount = objCertificationActionModel.GetCertifiedMaterialCount(intCertTypeId, _
                                                                                              m_view.CertificateNumber, _
                                                                                              m_view.Extension)

            If intCertifiedMaterialCount > 0 Then
                Dim strWarningMessage As String
                If intCertifiedMaterialCount = 1 Then
                    strWarningMessage = "There is {0} material attached to this certificate. The certificate will be deleted and the material will be detached."
                Else
                    strWarningMessage = "There are {0} materials attached to this certificate. The certificate will be deleted and all materials will be detached."
                End If
                m_view.WarningMessage = String.Format(strWarningMessage, intCertifiedMaterialCount)
            Else
                m_view.WarningMessage = String.Format("Certificate {0} will be deleted.", _
                                                       m_view.CertificateNumber)
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            If InStr(exc.Message.ToString(), "ORA-20001") > 0 Then
                m_view.ErrorText = String.Format("Certificate number {0} not exist.", m_view.CertificateNumber)
            Else
                m_view.ErrorText = "Error deleting certificate."
            End If
        End Try

    End Sub

    ''' <summary>
    ''' Deletes the certificate
    ''' </summary>    
    Private Sub OnSave()

        Dim blnDeleteSuccess As Boolean
        Dim objCertificationActionModel As New CertificationActionModel
        Dim objCertModel As New CertificateModel
        Dim intCertTypeId As Integer = objCertModel.GetCertificateTypeID(m_view.CertificationName)

        Try
            blnDeleteSuccess = objCertificationActionModel.DeleteCertificate(intCertTypeId, _
                                                                             m_view.CertificateNumber, _
                                                                             m_view.Extension)
            If blnDeleteSuccess Then
                m_view.SuccessText = "Deleted."
            Else
                m_view.ErrorText = "Delete failed."
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            If InStr(exc.Message.ToString(), "ORA-20001") > 0 Then
                m_view.ErrorText = String.Format("Certificate number {0} not exist.", m_view.CertificateNumber)
            Else
                m_view.ErrorText = "Error deleting certificate."
            End If
        End Try

    End Sub

#End Region

End Class
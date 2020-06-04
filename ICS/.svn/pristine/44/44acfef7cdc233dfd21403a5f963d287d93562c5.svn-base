Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Rename certification presenter
''' </summary>
''' <remarks></remarks>
Public Class RenameCertificationPresenter

#Region "Members"

    Private m_view As IRenameCertificationView

#End Region

#Region "Constructors"

    Public Sub New(ByVal p_view As IRenameCertificationView)

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
    ''' Attach presenter to view’s events.
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
            m_view.OldCertificateNumber = String.Empty
            m_view.NewCertificateNumber = String.Empty
            m_view.OldExtension = String.Empty
            m_view.NewExtension = String.Empty
            m_view.ErrorText = String.Empty
            m_view.SuccessText = String.Empty
            m_view.WarningMessage = String.Empty
            m_view.OldCertNumErrMsgFlag = False
            m_view.NewCertNumErrMsgFlag = False
            m_view.OldExtensionErrMsgFlag = False
            m_view.NewExtensionErrMsgFlag = False

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

        m_view.AddCertificationTitle = "Rename " + m_view.CertificationName + " certificate"
        m_view.SetupRenameCertificationView()

    End Sub

    ''' <summary>
    ''' Check for certified materials
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnCheckForCertifiedMaterials()

        Dim intCertifiedMaterialCount As Integer
        Dim objCertificationActionModel As New CertificationActionModel
        Dim objCertificateModel As New CertificateModel
        Dim intCertTypeId As Integer = objCertificateModel.GetCertificateTypeID(m_view.CertificationName)

        Try
            If String.IsNullOrEmpty(m_view.OldCertificateNumber) Then
                m_view.ErrorText = "Old Certificate Number can not be empty."
                Return
            ElseIf String.IsNullOrEmpty(m_view.NewCertificateNumber) Then
                m_view.ErrorText = "New Certificate Number can not be empty."
                Return
            End If

            intCertifiedMaterialCount = objCertificationActionModel.GetCertifiedMaterialCount(intCertTypeId, _
                                                                                              m_view.OldCertificateNumber, _
                                                                                              m_view.OldExtension)

            If intCertifiedMaterialCount > 0 Then
                Dim strWarningMessage As String
                If intCertifiedMaterialCount = 1 Then
                    strWarningMessage = "There is {0} material on this certificate – this material will be moved from {1} to {2}"
                Else
                    strWarningMessage = "There are {0} materials on this certificate – these materials will be moved from {1} to {2}"
                End If
                m_view.WarningMessage = String.Format(strWarningMessage, intCertifiedMaterialCount, m_view.OldCertificateNumber, m_view.NewCertificateNumber)
            Else
                m_view.WarningMessage = String.Format("Certificate {0} will be renamed to {1}", _
                                                m_view.OldCertificateNumber, m_view.NewCertificateNumber)
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            If InStr(exc.Message.ToString(), "ORA-20001") > 0 Then
                m_view.ErrorText = String.Format("Old certificate number {0} not exist.", m_view.OldCertificateNumber)
            Else
                m_view.ErrorText = "Error renaming certificate."
            End If
        End Try

    End Sub

    ''' <summary>
    ''' Renames the old certificate to new certificate
    ''' </summary>    
    Private Sub OnSave()

        Dim blnRenameSuccess As Boolean
        Dim objCertificationActionModel As New CertificationActionModel
        Dim objCertModel As New CertificateModel
        Dim intCertTypeId As Integer = objCertModel.GetCertificateTypeID(m_view.CertificationName)

        Try
            blnRenameSuccess = objCertificationActionModel.RenameCertificate(intCertTypeId, _
                                                                             m_view.OldCertificateNumber, _
                                                                             m_view.OldExtension, _
                                                                             m_view.NewCertificateNumber, _
                                                                             m_view.NewExtension)
            If blnRenameSuccess Then
                m_view.SuccessText = "Renamed."
            Else
                m_view.ErrorText = "Rename failed."
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            If InStr(exc.Message.ToString(), "ORA-20001") > 0 Then
                m_view.ErrorText = String.Format("New certificate number {0} already exist.", m_view.NewCertificateNumber)
            Else
                m_view.ErrorText = "Error renaming certificate."
            End If
        End Try

    End Sub

#End Region

End Class

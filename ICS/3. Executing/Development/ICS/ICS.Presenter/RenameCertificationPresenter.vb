Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Rename certification presenter
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
''' <para>10/22/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class RenameCertificationPresenter

#Region "Members and Constants"
    ''' <summary>
    ''' Interface to the Rename certification user control view.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_view As IRenameCertificationView
    ''' <summary>
    '''  Constant to hold ORA text
    ''' </summary>
    Private Const ORAText As String = "ORA-20001"
    ''' <summary>
    '''  Constant to hold ErrorRenameCertificate text
    ''' </summary>
    Private Const ErrorRenameCertificateText As String = "Error renaming certificate."

#End Region

#Region "Constructors"
    ''' <summary>
    '''  Custom Constructor to initialize class members.
    ''' </summary>
    ''' <param name="p_view">Viewm</param>
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
    ''' <para>10/22/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New(ByVal p_view As IRenameCertificationView)
        Const ErrorCreateText As String = "Error creating "
        Try
            m_view = p_view
            SubscribeToEvents()
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw New Exception(ErrorCreateText + Me.ToString())
        End Try
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Attach presenter to view�s events.
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
    ''' <para>10/22/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub SubscribeToEvents()
        Try
            AddHandler m_view.LoadView, AddressOf OnLoadView
            AddHandler m_view.ReloadViewData, AddressOf OnReloadViewData
            AddHandler m_view.CheckForCertifiedMaterials, AddressOf OnCheckForCertifiedMaterials
            AddHandler m_view.Save, AddressOf OnSave
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
    ''' <para>10/22/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnLoadView(ByVal sender As Object, ByVal e As EventArgs)
        Const ErrorLoadFormData As String = "Error loading form data."
        Try
            LoadViewData()
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorLoadFormData
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
    ''' <para>10/22/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnReloadViewData()
        Const ErrorReloadFormDataText As String = "Error re-loading form data."
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
            m_view.ErrorText = ErrorReloadFormDataText
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
    ''' <para>10/22/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub LoadViewData()
        Const RenameText As String = "Rename "
        Const CertificateText As String = " certificate"
        Try
            m_view.AddCertificationTitle = RenameText + m_view.CertificationName + CertificateText
            m_view.SetupRenameCertificationView()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Check for certified materials.
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
    ''' <para>10/22/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnCheckForCertifiedMaterials()

        Dim intCertifiedMaterialCount As Integer
        Dim objCertificationActionModel As New CertificationActionModel
        Dim objCertificateModel As New CertificateModel
        Dim intCertTypeId As Integer = objCertificateModel.GetCertificateTypeID(m_view.CertificationName)
        Const OldCertNumberEmptyText As String = "Old Certificate Number can not be empty."
        Const NewCertNumberEmptyText As String = "New Certificate Number can not be empty."
        
        Try
            If String.IsNullOrEmpty(m_view.OldCertificateNumber) Then
                m_view.ErrorText = OldCertNumberEmptyText
                Return
            ElseIf String.IsNullOrEmpty(m_view.NewCertificateNumber) Then
                m_view.ErrorText = NewCertNumberEmptyText
                Return
            End If

            intCertifiedMaterialCount = objCertificationActionModel.GetCertifiedMaterialCount(intCertTypeId, _
                                                                                              m_view.OldCertificateNumber, _
                                                                                              m_view.OldExtension)

            If intCertifiedMaterialCount > 0 Then
                Dim strWarningMessage As String
                If intCertifiedMaterialCount = 1 Then
                    strWarningMessage = "There is {0} material on this certificate � this material will be moved from {1} to {2}"
                Else
                    strWarningMessage = "There are {0} materials on this certificate � these materials will be moved from {1} to {2}"
                End If
                m_view.WarningMessage = String.Format(strWarningMessage, intCertifiedMaterialCount, m_view.OldCertificateNumber, m_view.NewCertificateNumber)
            Else
                m_view.WarningMessage = String.Format("Certificate {0} will be renamed to {1}", _
                                                m_view.OldCertificateNumber, m_view.NewCertificateNumber)
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            If InStr(exc.Message.ToString(), ORAText) > 0 Then
                m_view.ErrorText = String.Format("Old certificate number {0} not exist.", m_view.OldCertificateNumber)
            Else
                m_view.ErrorText = ErrorRenameCertificateText
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Renames the old certificate to new certificate.
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
    ''' <para>10/22/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>    
    Private Sub OnSave()

        Dim blnRenameSuccess As Boolean
        Dim objCertificationActionModel As New CertificationActionModel
        Dim objCertModel As New CertificateModel
        Dim intCertTypeId As Integer = objCertModel.GetCertificateTypeID(m_view.CertificationName)
        Const RenamedText As String = "Renamed."
        Const RenameFailedText As String = "Rename failed."
        Const ErrorRenameCertificateText As String = "Error renaming certificate."

        Try
            blnRenameSuccess = objCertificationActionModel.RenameCertificate(intCertTypeId, _
                                                                             m_view.OldCertificateNumber, _
                                                                             m_view.OldExtension, _
                                                                             m_view.NewCertificateNumber, _
                                                                             m_view.NewExtension)
            If blnRenameSuccess Then
                m_view.SuccessText = RenamedText
            Else
                m_view.ErrorText = RenameFailedText
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            If InStr(exc.Message.ToString(), ORAText) > 0 Then
                m_view.ErrorText = String.Format("New certificate number {0} already exist.", m_view.NewCertificateNumber)
            Else
                m_view.ErrorText = ErrorRenameCertificateText
            End If
        End Try
    End Sub
#End Region

End Class

Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Delete certification presenter
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
''' <para>10/16/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class DeleteCertificationPresenter

#Region "Members and Constants"
    ''' <summary>
    '''  Interface to the Delete certification user control view.
    ''' </summary>
    Private m_view As IDeleteCertificationView
    ''' <summary>
    '''  Constant to hold ORA text
    ''' </summary>
    Private Const ORAText As String = "ORA-20001"
    ''' <summary>
    '''  Constant to hold Delete Certificate Error text
    ''' </summary>
    Private Const DeleteCertificateError As String = "Error deleting certificate."

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
    ''' <para>10/16/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New(ByVal p_view As IDeleteCertificationView)
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
    ''' <para>10/16/2019</para>
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
    ''' <para>10/16/2019</para>
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
    ''' <para>10/16/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnReloadViewData()
        Const ErrorReLoadFormDataText As String = "Error re-loading form data."
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
            m_view.ErrorText = ErrorReLoadFormDataText
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
    ''' <para>10/16/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub LoadViewData()
        Const DeleteText As String = "Delete "
        Const CertificateText As String = " certificate"
        Try
            m_view.AddCertificationTitle = DeleteText + m_view.CertificationName + CertificateText
            m_view.SetupDeleteCertificationView()
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
    ''' <para>10/16/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnCheckForCertifiedMaterials()

        Dim intCertifiedMaterialCount As Integer
        Dim objCertificationActionModel As New CertificationActionModel
        Dim objCertModel As New CertificateModel
        Const EmptyCertificateNumberText As String = "Certificate Number can not be empty."

        Dim intCertTypeId As Integer = objCertModel.GetCertificateTypeID(m_view.CertificationName)

        Try
            If String.IsNullOrEmpty(m_view.CertificateNumber) Then
                m_view.ErrorText = EmptyCertificateNumberText
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
            If InStr(exc.Message.ToString(), ORAText) > 0 Then
                m_view.ErrorText = String.Format("Certificate number {0} not exist.", m_view.CertificateNumber)
            Else
                m_view.ErrorText = DeleteCertificateError
            End If
        End Try

    End Sub

    ''' <summary>
    ''' Deletes the certificate.
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
    ''' <para>10/16/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>    
    Private Sub OnSave()
        Dim blnDeleteSuccess As Boolean
        Dim objCertificationActionModel As New CertificationActionModel
        Dim objCertModel As New CertificateModel
        Const SuccessText As String = "Deleted."
        Const ErrorText As String = "Delete failed."

        Dim intCertTypeId As Integer = objCertModel.GetCertificateTypeID(m_view.CertificationName)
        Try
            blnDeleteSuccess = objCertificationActionModel.DeleteCertificate(intCertTypeId, _
                                                                             m_view.CertificateNumber, _
                                                                             m_view.Extension)
            If blnDeleteSuccess Then
                m_view.SuccessText = SuccessText
            Else
                m_view.ErrorText = ErrorText
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            If InStr(exc.Message.ToString(), ORAText) > 0 Then
                m_view.ErrorText = String.Format("Certificate number {0} not exist.", m_view.CertificateNumber)
            Else
                m_view.ErrorText = DeleteCertificateError
            End If
        End Try
    End Sub
#End Region

End Class

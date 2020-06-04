Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Detach certification presenter
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
Public Class DetachOrMoveCertificationPresenter

#Region "Members and Constants"
    ''' <summary>
    '''  Interface to the Detach Or Move Certification User control view.
    ''' </summary>
    Private m_view As IDetachOrMoveCertificationView
    ''' <summary>
    '''  Constant to hold ORA text
    ''' </summary>
    Private Const ORAText As String = "ORA-20001"

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
    Public Sub New(ByVal p_view As IDetachOrMoveCertificationView)
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
            AddHandler m_view.ShowCertificateMaterials, AddressOf OnShowCertificateMaterials
            AddHandler m_view.Detach, AddressOf OnDetach
            AddHandler m_view.Move, AddressOf OnMove
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
        Const LoadFormErrorText As String = "Error loading form data."
        Try
            LoadViewData()
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = LoadFormErrorText
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
        Const ReloadErrorText As String = "Error re-loading form data."
        Try
            ' Flush all properties:
            m_view.CertificateNumber = String.Empty
            m_view.Extension = String.Empty
            m_view.ErrorText = String.Empty
            m_view.SuccessText = String.Empty
            m_view.SkuId = 0
            m_view.CertificateId = 0
            m_view.NewCertificateNumber = String.Empty
            m_view.NewExtension = String.Empty
            m_view.CertNumErrMsgFlag = False
            m_view.ExtensionErrMsgFlag = False
            m_view.NewCertNumErrMsgFlag = False
            m_view.NewExtensionErrMsgFlag = False

            m_view.CertificateMaterials = Nothing
            m_view.DataBindView()

            LoadViewData()
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ReloadErrorText
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
        Const DetachText As String = "Detach "
        Const CertificateText As String = " certificate"
        Try
            m_view.AddCertificationTitle = DetachText + m_view.CertificationName + CertificateText
            m_view.SetupDetachOrMoveCertificationView()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Get Certificate Materials.
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
    Private Sub OnShowCertificateMaterials()

        Dim dtCertificateMaterials As New DataTable
        Dim objCertificationActionModel As New CertificationActionModel
        Dim objCertModel As New CertificateModel
        Const NoDataFoundError As String = "No data found."
        Const GetCertificateError As String = "Error getting certification materials."

        Dim intCertTypeId As Integer = objCertModel.GetCertificateTypeID(m_view.CertificationName)
        Try
            dtCertificateMaterials = objCertificationActionModel.GetCertificateMaterials(intCertTypeId, _
                                                                                         m_view.CertificateNumber, _
                                                                                         m_view.Extension)
            If (dtCertificateMaterials.Rows.Count > 0) Then
                m_view.CertificateMaterials = dtCertificateMaterials
            Else
                m_view.ErrorText = NoDataFoundError
            End If
            m_view.DataBindView()

        Catch exc As Exception
            EventLogger.Enter(exc)
            If InStr(exc.Message.ToString(), ORAText) > 0 Then
                m_view.ErrorText = String.Format("Certificate number {0} not exist.", m_view.CertificateNumber)
            Else
                m_view.ErrorText = GetCertificateError
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Detach the certificate.
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
    Private Sub OnDetach()

        Dim blnDetachSuccess As Boolean
        Dim objCertificationActionModel As New CertificationActionModel
        Const DetachSuccessText As String = "Detached."
        Const DetachErrorText As String = "Detach failed."
        Const DetachCertificateError As String = "Error detaching certification."
        Try
            blnDetachSuccess = objCertificationActionModel.DetachCertificate(m_view.SkuId, _
                                                                             m_view.CertificateId)

            If blnDetachSuccess Then
                m_view.SuccessText = DetachSuccessText
            Else
                m_view.ErrorText = DetachErrorText
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            If InStr(exc.Message.ToString(), ORAText) > 0 Then
                m_view.ErrorText = String.Format("SkuId {0}, CertificateId {1} not exist.", m_view.SkuId, m_view.CertificateId)
            Else
                m_view.ErrorText = DetachCertificateError
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Move the certificate.
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
    Private Sub OnMove()

        Dim blnMoveSuccess As Boolean
        Dim objCertificationActionModel As New CertificationActionModel
        Dim objCertModel As New CertificateModel
        Dim intCertTypeId As Integer = objCertModel.GetCertificateTypeID(m_view.CertificationName)
        Const MoveSuccessText As String = "Moved."
        Const MoveErrorText As String = "Move failed."
        Const MoveCertificateError As String = "Error moving certification."
        Const NewCertificateNumberErrorText As String = "New Certificate Number can not be empty."
        Try
            If String.IsNullOrEmpty(m_view.NewCertificateNumber) Then
                m_view.ErrorText = NewCertificateNumberErrorText
                Return
            End If

            blnMoveSuccess = objCertificationActionModel.MoveCertificate(intCertTypeId, _
                                                                         m_view.NewCertificateNumber, _
                                                                         m_view.NewExtension, _
                                                                         m_view.SkuId, _
                                                                         m_view.CertificateId)

            If blnMoveSuccess Then
                m_view.SuccessText = MoveSuccessText
            Else
                m_view.ErrorText = MoveErrorText
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            If InStr(exc.Message.ToString(), ORAText) > 0 Then
                m_view.ErrorText = String.Format("New Certificate Number {0} not exist.", m_view.NewCertificateNumber)
            Else
                m_view.ErrorText = MoveCertificateError
            End If
        End Try
    End Sub
#End Region

End Class

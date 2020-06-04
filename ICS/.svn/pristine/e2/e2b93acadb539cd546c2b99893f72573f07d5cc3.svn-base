Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Add certification presenter
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
Public Class AttachCertificationPresenter

#Region "Members"
    ''' <summary>
    '''  Interface to the Attach certification.
    ''' </summary>
    Private Shared m_view As IAttachCertification = Nothing

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
    Public Sub New(ByVal p_view As IAttachCertification)
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
        Const AttachCertificateText As String = "Attach Certificate"
        Try
            m_view.AttachCertificationTitle = AttachCertificateText
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
            m_view.ExtensionEn = String.Empty
            m_view.CertificationTypeId = String.Empty
            m_view.SkuId = String.Empty
            m_view.ErrorText = String.Empty
            m_view.SuccessText = String.Empty
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
        Dim strSaveSuccess As String = String.Empty
        Dim objAttachCertificateModel As AttachCertificateModel = Nothing
        Const ErrorEnterValidCertNumberText As String = "Enter valid Certificate Number."
        Const ErrorEnterValidSKUIdText As String = "Enter Valid Sku Id."
        Const ErrorEnterValidExtensionNumberText As String = "Enter Valid Extension Number."
        Const ErrorEnterValidCertTypeIdText As String = "Enter Valid Certificate Type Id."
        Const SavedText As String = "Saved."
        Const InValidCertText As String = "INVALID CERTIFICATE"
        Const ErrorInValidCertText As String = "invalid certificate."
        Const SKUIDDoesNotEXISTText As String = "SKUID DOES NOT EXIST"
        Const ErrorSKUIdDoesNotExistText As String = "skuid does not exist."
        Const DuplicateRecordText As String = "DUPLICATE RECORD"
        Const ErrorDuplicateRecordText As String = "duplicate record."
        Const ErrorAttachCertText As String = "Error attaching certification."

        Try
            m_view.SuccessText = String.Empty
            m_view.ErrorText = String.Empty

            If String.IsNullOrEmpty(m_view.CertificateNumber) Then
                m_view.ErrorText = ErrorEnterValidCertNumberText
                Return
            End If
            If String.IsNullOrEmpty(m_view.SkuId) Then
                m_view.ErrorText = ErrorEnterValidSKUIdText
                Return
            End If
            If String.IsNullOrEmpty(m_view.ExtensionEn) Then
                m_view.ErrorText = ErrorEnterValidExtensionNumberText
                Return
            End If
            If String.IsNullOrEmpty(m_view.CertificationTypeId) Then
                m_view.ErrorText = ErrorEnterValidCertTypeIdText
                Return
            End If

            objAttachCertificateModel = New AttachCertificateModel
            strSaveSuccess = objAttachCertificateModel.AttachCertification(CInt(m_view.SkuId), m_view.CertificateNumber, m_view.ExtensionEn, CInt(m_view.CertificationTypeId))

            If String.IsNullOrEmpty(strSaveSuccess) Then
                m_view.SuccessText = SavedText
            ElseIf strSaveSuccess.ToUpper() = InValidCertText Then
                m_view.ErrorText = ErrorInValidCertText
            ElseIf strSaveSuccess.ToUpper() = SKUIDDoesNotEXISTText Then
                m_view.ErrorText = ErrorSKUIdDoesNotExistText
            ElseIf strSaveSuccess.ToUpper() = DuplicateRecordText Then
                m_view.ErrorText = ErrorDuplicateRecordText
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorAttachCertText
        End Try
    End Sub

    ''' <summary>
    ''' Check Certificate Number exists.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception> 
    ''' <returns>Error Message</returns>
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
        Dim objAttachCertificationModel As AttachCertificateModel = Nothing
        Dim blnCertNumExists As Boolean = False
        Const CertNumberCannotBeNullText As String = "Certificate number cannot be null"
        Const InvalidCertNumberLengthText As String = "Invalid certificate number length"
        Const ErrorValidatingCertNumberText As String = "Error validating certificate number."

        Try
            If String.IsNullOrEmpty(m_view.CertificateNumber.Trim) Then
                strErrorMessage = CertNumberCannotBeNullText
                Return strErrorMessage
            ElseIf m_view.CertificateNumber.Trim.Length < 4 Or m_view.CertificateNumber.Trim.Length > 20 Then
                strErrorMessage = InvalidCertNumberLengthText
                Return strErrorMessage
            End If

        Catch exc As Exception
            EventLogger.Enter(exc)
            strErrorMessage = ErrorValidatingCertNumberText
        End Try

        Return strErrorMessage
    End Function

#End Region

End Class








Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Certification defaults view presenter
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
Public Class CertificationDefaultsPresenter
    Inherits ViewPresenterBase

#Region "Members and Constants"
    ''' <summary>
    ''' Certification defaults view presenter
    ''' </summary>
    Private m_view As ICertificationDefaultsView
    ''' <summary>
    '''  Constant to hold ErrorSavingFormData text
    ''' </summary>
    Const ErrorSavingFormDataText As String = "Error saving form data."

#End Region

#Region "Constructors / Destructors"
    ''' <summary>
    ''' Custom Constructor to initialize class members.
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
    Public Sub New(ByVal p_view As ICertificationDefaultsView)
        MyBase.New(p_view)
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
    ''' <para>10/15/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub SubscribeToEvents()
        Try
            AddHandler m_view.LoadView, AddressOf OnLoadView
            AddHandler m_view.SaveCertificateDefaults, AddressOf OnSaveCertificateDefaults
            AddHandler m_view.SaveCertificateTypeDefaults, AddressOf OnSaveCertificateTypeDefaults
            AddHandler m_view.CertificateTypeSelected, AddressOf OnCertificateTypeSelected
        Catch
            Throw
        End Try   
    End Sub

    ''' <summary>
    ''' Initial load Set up default labels.
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
            If (Not m_view.IsPostBackView) Then
                LoadViewData()
            Else
                m_view.InfoText = String.Empty
            End If
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorLoadFormDataText
        End Try
    End Sub

    ''' <summary>
    ''' Initial load for a certificate type or number.
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
        Const SelectText As String = "Select ..."
        Try
            Dim certSearch As CertificationSearch = New CertificationSearch()
            Dim listCertNames As List(Of String) = certSearch.GetCertificationNames()

            listCertNames.Insert(0, SelectText)
            m_view.CertificationNames = listCertNames
            m_view.DataBindView()

            Dim strCertificateType As String = String.Empty
            Dim intCertificateNoID As String = CStr(0)
            m_view.GetViewInputParams(strCertificateType, CInt(intCertificateNoID))

            Dim defaultFieldModel As New DefaultFieldModel()

            'm_view.CertificateType = strCertificateType
            If strCertificateType = String.Empty Then
                m_view.CertificateType = strCertificateType
            Else
                'Convert Numeric type passed in to type name jes 01/31/2017
                m_view.CertificateType = defaultFieldModel.GetCertificationName(CInt(strCertificateType))
            End If
            m_view.CertificateNumberID = CInt(intCertificateNoID)

            OnCertificateTypeSelected()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Displays default data from the database.
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
    Private Sub OnCertificateTypeSelected()
        Const ErrorSettingFormDataText As String = "Error setting form data."
        Try
            Dim listDefaultFields As List(Of CertificationDefaultField) = Nothing
            Dim strCertificateNo As String = String.Empty

            Dim defaultFieldModel As New DefaultFieldModel()
            If Not m_view.CertificateType = Nothing Then
                listDefaultFields = defaultFieldModel.GetDefaultValues(m_view.CertificateType, m_view.CertificateNumberID, strCertificateNo)
            End If

            m_view.CertificateNo = strCertificateNo
            m_view.CertificateNoToShow = strCertificateNo
            m_view.CertificateFields = listDefaultFields

            m_view.SetupControlContextState()
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorSettingFormDataText
        End Try
    End Sub

    ''' <summary>
    ''' Saves Certificate default values to the database.
    ''' </summary>
    ''' <param name="objCertDefFieldList">Certificate Def Field List</param>
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
    Private Sub OnSaveCertificateDefaults(ByVal objCertDefFieldList As Object)
        Const CertDefaultvalueSavedText As String = "Certificate default values saved."
        Try
            Dim listDefFields As List(Of CertificationDefaultField) = CType(objCertDefFieldList, Global.System.Collections.Generic.List(Of Global.CooperTire.ICS.DomainEntities.CertificationDefaultField))
            Dim objDefaultFieldModel As New DefaultFieldModel

            If (objDefaultFieldModel.CertificateValueSave(listDefFields, m_view.CertificateNo)) Then
                m_view.InfoText = CertDefaultvalueSavedText
            End If
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorSavingFormDataText
        End Try
    End Sub

    ''' <summary>
    ''' Saves Certificate Type default values.
    ''' </summary>
    ''' <param name="objCertDefFieldList">Ceetificate Def Field List</param>
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
    Private Sub OnSaveCertificateTypeDefaults(ByVal objCertDefFieldList As Object)
        Const CertTypeDefaultvalueSavedText As String = "Certificate type default values saved."
        Try
            Dim fldList As List(Of CertificationDefaultField) = CType(objCertDefFieldList, Global.System.Collections.Generic.List(Of Global.CooperTire.ICS.DomainEntities.CertificationDefaultField))
            Dim objDefaultFieldModel As New DefaultFieldModel

            If (objDefaultFieldModel.CertificateDefaultvalueSave(fldList)) Then
                m_view.InfoText = CertTypeDefaultvalueSavedText
            End If
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorSavingFormDataText
        End Try
    End Sub

#End Region

End Class

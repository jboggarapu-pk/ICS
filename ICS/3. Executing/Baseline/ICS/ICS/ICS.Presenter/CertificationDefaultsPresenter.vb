'Imports System.Xml

Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Certification defaults view presenter
''' </summary>
''' <remarks></remarks>
Public Class CertificationDefaultsPresenter
    Inherits ViewPresenterBase

#Region "Members"

    Private m_view As ICertificationDefaultsView

#End Region

#Region "Constructors / Destructors"

    Public Sub New(ByVal p_view As ICertificationDefaultsView)

        MyBase.New(p_view)

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
        AddHandler m_view.SaveCertificateDefaults, AddressOf OnSaveCertificateDefaults
        AddHandler m_view.SaveCertificateTypeDefaults, AddressOf OnSaveCertificateTypeDefaults
        AddHandler m_view.CertificateTypeSelected, AddressOf OnCertificateTypeSelected

    End Sub

    ''' <summary>
    ''' Initial load Set up default labels
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnLoadView(ByVal sender As Object, ByVal e As EventArgs)

        Try
            If (Not m_view.IsPostBackView) Then
                LoadViewData()
            Else
                m_view.InfoText = String.Empty
            End If
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = "Error loading form data."
        End Try

    End Sub

    ''' <summary>
    ''' Initial load for a certificate type or number
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadViewData()

        Dim certSearch As CertificationSearch = New CertificationSearch()
        Dim listCertNames As List(Of String) = certSearch.GetCertificationNames()

        listCertNames.Insert(0, "Select ...")
        m_view.CertificationNames = listCertNames
        m_view.DataBindView()

        Dim strCertificateType As String = String.Empty
        Dim intCertificateNoID As String = 0
        m_view.GetViewInputParams(strCertificateType, intCertificateNoID)

        Dim defaultFieldModel As New DefaultFieldModel()

        'm_view.CertificateType = strCertificateType
        If strCertificateType = String.Empty Then
            m_view.CertificateType = strCertificateType
        Else
            'Convert Numeric type passed in to type name jes 01/31/2017
            m_view.CertificateType = defaultFieldModel.GetCertificationName(strCertificateType)
        End If
        m_view.CertificateNumberID = intCertificateNoID

        OnCertificateTypeSelected()

    End Sub

    ''' <summary>
    ''' Displays default data from the database
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnCertificateTypeSelected()

        Try
            Dim listDefaultFields As List(Of CertificationDefaultField) = Nothing
            Dim strCertificateNo As String = String.Empty

            Dim defaultFieldModel As New DefaultFieldModel()
            If Not m_view.CertificateType = Nothing Then
                listDefaultFields = defaultFieldModel.GetDefaultValues(m_view.CertificateType, m_view.CertificateNumberID, strCertificateNo)
            End If

            m_view.CertificateNo = strCertificateNo
            'jeseitz 4/12/16
            '' Imark to the user always looks the same
            'If strCertificateNo.StartsWith(ImarkCertificateModel.ImarkCertNumber) Then
            '    strCertificateNo = ImarkCertificateModel.ImarkCertNumber
            'End If

            m_view.CertificateNoToShow = strCertificateNo
            m_view.CertificateFields = listDefaultFields

            m_view.SetupControlContextState()
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = "Error setting form data."
        End Try

    End Sub

    ''' <summary>
    ''' Saves Certificate default values to the database
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnSaveCertificateDefaults(ByVal objCertDefFieldList As Object)

        Try
            Dim listDefFields As List(Of CertificationDefaultField) = objCertDefFieldList
            Dim objDefaultFieldModel As New DefaultFieldModel

            If (objDefaultFieldModel.CertificateValueSave(listDefFields, m_view.CertificateNo)) Then
                m_view.InfoText = "Certificate default values saved."
            End If
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = "Error saving form data."
        End Try

    End Sub

    ''' <summary>
    ''' Saves Certificate Type default values
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnSaveCertificateTypeDefaults(ByVal objCertDefFieldList As Object)

        Try
            Dim fldList As List(Of CertificationDefaultField) = objCertDefFieldList
            Dim objDefaultFieldModel As New DefaultFieldModel

            If (objDefaultFieldModel.CertificateDefaultvalueSave(fldList)) Then
                m_view.InfoText = "Certificate type default values saved."
            End If
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = "Error saving form data."
        End Try

    End Sub

#End Region

End Class

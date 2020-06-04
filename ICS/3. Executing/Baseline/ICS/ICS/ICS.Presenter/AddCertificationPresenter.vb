Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Add certification presenter
''' </summary>
''' <remarks></remarks>
Public Class AddCertificationPresenter

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

#Region "Members"

    Private m_view As IAddCertificationView

#End Region

#Region "Constructors"

    Public Sub New(ByVal p_view As IAddCertificationView)

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
        AddHandler m_view.Save, AddressOf OnSave
        AddHandler m_view.CheckSKUExist, AddressOf OnCheckSKUExist

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
            m_view.Importers = Nothing
            m_view.Customers = Nothing
            m_view.ErrorText = String.Empty
            m_view.SuccessText = String.Empty
            m_view.MatlNumList = Nothing
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

        m_view.AddCertificationTitle = "Add " + m_view.CertificationName + " certificate"

        If m_view.CertificationName = "GSO" Then
            If m_view.CertificateNumber = String.Empty Then
                Dim addcertModel As New AddCertificationModel()
                m_view.CertificateNumber = addcertModel.GetGSOCertificateNumber
            End If
        End If

        If m_view.CertificationName = "NOM" Then
            Dim addcertModel As New AddCertificationModel()

            'Get Importers
            m_view.Importers = addcertModel.GetImporters()
            If Not m_view.Importer Is Nothing And m_view.Importer <> String.Empty Then
                m_view.Importer = m_view.Importer
            End If

            'Get Customers
            m_view.Customers = addcertModel.GetCustomers()
            If Not m_view.Customer Is Nothing And m_view.Customer <> String.Empty Then
                m_view.Customer = m_view.Customer
            End If
            m_view.DataBindView()
        End If
        'NOTE - possible Imark handling as single cert (not form marketing), if approved ...
        ' ''    If m_view.CertificateNumber = String.Empty Then

        '        If m_view.CertificationName = NameAid.Certification.Imark.ToString() Then
        '            If m_view.CertificateNumberImark = String.Empty Then
        '                m_view.CertificateNumberImark = ImarkCertificateModel.MakeNewCertificationNumber
        '            Else
        '    m_view.CertificateNumberImark = m_view.CertificateNumberImark
        '            End If
        '        Else
        '            m_view.CertificateNumberImark = String.Empty
        '        End If
        ''    End If

        m_view.SetupAddCertificationView()

    End Sub

    ''' <summary>
    ''' Saves the data that has been entered on the Emark Certificate form
    ''' </summary>    
    Private Sub OnSave()

        Dim objAddCertificationModel As New AddCertificationModel
        'Dim intCertTypeId As Integer = NameAid.GetCertificationTypeIDByName(m_view.CertificationName)
        Dim intCertTypeId As Integer = objAddCertificationModel.GetCertificationTypeID(m_view.CertificationName)

        Try
            If String.IsNullOrEmpty(m_view.CertificateNumber) Then
                m_view.ErrorText = "Certificate Number can not be empty."
                Return
            ElseIf m_view.MatlNumList Is Nothing Then
                m_view.ErrorText = "Material Number can not be empty."
                Return
            ElseIf m_view.MatlNumList.Count = 0 Then
                m_view.ErrorText = "Material Number can not be empty."
                Return
            ElseIf m_view.MatlNumList.Count = 1 And String.IsNullOrEmpty(m_view.MatlNumList(0)) Then
                m_view.ErrorText = "Material Number can not be empty."
                Return
            End If

            'Add Imark certificate
            If m_view.CertificationName = "Imark" Then
                For Each strMatlNum As String In m_view.MatlNumList
                    If Not String.IsNullOrEmpty(strMatlNum) Then
                        'Modified as per project 2706 technical specification
                        'm_view.ErrorNum = objAddCertificationModel.SaveCertificateSKUAssociation(ImarkCertificateModel.ImarkCertNumber.ToString(), strMatlNum, intCertTypeId, m_view.Importer, m_view.Customer, m_view.Extension, m_view.InsertPC, m_view.ErrorDesc)
                        'Modified jeseitz 4/12/16 - new imark
                        m_view.ErrorNum = objAddCertificationModel.SaveCertificateSKUAssociation(m_view.CertificateNumber, strMatlNum, intCertTypeId, m_view.Importer, m_view.Customer, m_view.Extension, m_view.InsertPC, m_view.ErrorDesc)
                    End If

                    If m_view.ErrorNum <> 1 Then Exit For
                Next
            Else
                'Add other certificate
                For Each strMatlNum As String In m_view.MatlNumList
                    If Not String.IsNullOrEmpty(strMatlNum) Then
                        'Modified as per project 2706 technical specification
                        m_view.ErrorNum = objAddCertificationModel.SaveCertificateSKUAssociation(m_view.CertificateNumber, strMatlNum, intCertTypeId, m_view.Importer, m_view.Customer, m_view.Extension, m_view.InsertPC, m_view.ErrorDesc)
                        If (m_view.ErrorNum = 1 And (intCertTypeId = 1 Or 6)) Then
                            m_view.InsertPC = Nothing
                        End If
                    End If

                    If m_view.ErrorNum <> 1 Then Exit For
                Next
            End If

            If m_view.ErrorNum = 1 Then
                m_view.SuccessText = "Saved."
            ElseIf m_view.ErrorNum = 5 Or m_view.ErrorNum = 6 Or m_view.ErrorNum = 7 Then
                m_view.ErrorText = m_view.ErrorDesc
            ElseIf m_view.ErrorNum = 0 Then
                m_view.ErrorText = "Save Failed."
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            If InStr(exc.Message.ToString(), "ORA-20000") > 0 Then
                m_view.ErrorText = "Existing certificate already has date submitted for material numbers."
            Else
                m_view.ErrorText = "Error saving new certification."
            End If
        End Try

    End Sub

    Private Sub OnCheckSKUExist(ByVal p_strMatlNum As String, ByVal p_intIndex As Integer)

        Try
            If String.IsNullOrEmpty(p_strMatlNum) Then
                m_view.SetupSKUErrMsg(p_intIndex, False)
                Return
            End If

            Dim objAddCertificationModel As New AddCertificationModel
            Dim blnExistFlag = objAddCertificationModel.CheckMatlNumExist(p_strMatlNum)

            m_view.MatlNumList(p_intIndex) = p_strMatlNum
            m_view.SetupSKUErrMsg(p_intIndex, blnExistFlag)
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error checking the current material number."
        End Try

    End Sub

#End Region

End Class

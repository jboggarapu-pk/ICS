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
Public Class AddCertificationPresenter

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

#Region "Members"
    ''' <summary>
    '''  Interface to the Add certification user control view.
    ''' </summary>
    Private m_view As IAddCertificationView

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
    Public Sub New(ByVal p_view As IAddCertificationView)
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
    ''' <remarks>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception> 
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
            AddHandler m_view.ReloadViewData, AddressOf OnReloadViewData
            AddHandler m_view.Save, AddressOf OnSave
            AddHandler m_view.CheckSKUExist, AddressOf OnCheckSKUExist
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
    ''' <para>15/10/2019</para>
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
    ''' <para>15/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub LoadViewData()
        Const CertificationName As String = "GSO"
        Const CertificationName1 As String = "NOM"
        Const AddText As String = "Add "
        Const CertificateText As String = " certificate"

        Try
            m_view.AddCertificationTitle = AddText + m_view.CertificationName + CertificateText

            If m_view.CertificationName = CertificationName Then
                If m_view.CertificateNumber = String.Empty Then
                    Dim addcertModel As New AddCertificationModel()
                    m_view.CertificateNumber = addcertModel.GetGSOCertificateNumber
                End If
            End If

            If m_view.CertificationName = CertificationName1 Then
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
            m_view.SetupAddCertificationView()
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Saves the data that has been entered on the Emark Certificate form.
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
    ''' <para>15/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>    
    Private Sub OnSave()
        Const CertificationName As String = "Imark"
        Const ErrorCertNumberEmptyText As String = "Certificate Number can not be empty."
        Const ErrorMaterialNumberEmptyText As String = "Material Number can not be empty."
        Const SaveText As String = "Saved."
        Const ErrorSaveFailedText As String = "Save Failed."
        Const ErrorSavingCertificationText As String = "Error saving new certification."
        Const ORAText As String = "ORA-20000"
        Const ExistingCertSubmittedText As String = "Existing certificate already has date submitted for material numbers."
        Try
            Dim objAddCertificationModel As New AddCertificationModel
            'Dim intCertTypeId As Integer = NameAid.GetCertificationTypeIDByName(m_view.CertificationName)
            Dim intCertTypeId As Integer = objAddCertificationModel.GetCertificationTypeID(m_view.CertificationName)

            Try
                If String.IsNullOrEmpty(m_view.CertificateNumber) Then
                    m_view.ErrorText = ErrorCertNumberEmptyText
                    Return
                ElseIf m_view.MatlNumList Is Nothing Then
                    m_view.ErrorText = ErrorMaterialNumberEmptyText
                    Return
                ElseIf m_view.MatlNumList.Count = 0 Then
                    m_view.ErrorText = ErrorMaterialNumberEmptyText
                    Return
                ElseIf m_view.MatlNumList.Count = 1 And String.IsNullOrEmpty(m_view.MatlNumList(0)) Then
                    m_view.ErrorText = ErrorMaterialNumberEmptyText
                    Return
                End If

                'Add Imark certificate
                If m_view.CertificationName = CertificationName Then
                    For Each strMatlNum As String In m_view.MatlNumList
                        If Not String.IsNullOrEmpty(strMatlNum) Then
                            'Modified as per project 2706 technical specification
                            'm_view.ErrorNum = objAddCertificationModel.SaveCertificateSKUAssociation(ImarkCertificateModel.ImarkCertNumber.ToString(), strMatlNum, intCertTypeId, m_view.Importer, m_view.Customer, m_view.Extension, m_view.InsertPC, m_view.ErrorDesc)
                            'Modified jeseitz 4/12/16 - new imark
                            m_view.ErrorNum = objAddCertificationModel.SaveCertificateSKUAssociation(m_view.
                                                                                                     CertificateNumber,
                                                                                                     strMatlNum,
                                                                                                     intCertTypeId,
                                                                                                     m_view.Importer,
                                                                                                     m_view.Customer,
                                                                                                     m_view.Extension,
                                                                                                     m_view.InsertPC,
                                                                                                     m_view.ErrorDesc)
                        End If

                        If m_view.ErrorNum <> 1 Then Exit For
                    Next
                Else
                    'Add other certificate
                    For Each strMatlNum As String In m_view.MatlNumList
                        If Not String.IsNullOrEmpty(strMatlNum) Then
                            'Modified as per project 2706 technical specification
                            m_view.ErrorNum = objAddCertificationModel.SaveCertificateSKUAssociation(m_view.
                                                                                                     CertificateNumber,
                                                                                                     strMatlNum,
                                                                                                     intCertTypeId,
                                                                                                     m_view.Importer,
                                                                                                     m_view.Customer,
                                                                                                     m_view.Extension,
                                                                                                     m_view.InsertPC,
                                                                                                     m_view.ErrorDesc)
                            If CBool((CInt(m_view.ErrorNum = 1) And (CInt(intCertTypeId = 1) Or 6))) Then
                                m_view.InsertPC = Nothing
                            End If
                        End If

                        If m_view.ErrorNum <> 1 Then Exit For
                    Next
                End If

                If m_view.ErrorNum = 1 Then
                    m_view.SuccessText = SaveText
                ElseIf m_view.ErrorNum = 5 Or m_view.ErrorNum = 6 Or m_view.ErrorNum = 7 Then
                    m_view.ErrorText = m_view.ErrorDesc
                ElseIf m_view.ErrorNum = 0 Then
                    m_view.ErrorText = ErrorSaveFailedText
                End If
            Catch exc As Exception
                EventLogger.Enter(exc)
                If InStr(exc.Message.ToString(), ORAText) > 0 Then
                    m_view.ErrorText = ExistingCertSubmittedText
                Else
                    m_view.ErrorText = ErrorSavingCertificationText
                End If
            End Try
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' On check SKU Exist.
    ''' </summary>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <param name="p_intIndex">Index</param>
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
    ''' <para>15/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>    
    Private Sub OnCheckSKUExist(ByVal p_strMatlNum As String, ByVal p_intIndex As Integer)
        Const ErrorCheckingCurrentMaterialText As String = "Error checking the current material number."

        Try
            If String.IsNullOrEmpty(p_strMatlNum) Then
                m_view.SetupSKUErrMsg(p_intIndex, False)
                Return
            End If

            Dim objAddCertificationModel As New AddCertificationModel
            Dim blnExistFlag As Boolean = objAddCertificationModel.CheckMatlNumExist(p_strMatlNum)

            m_view.MatlNumList(p_intIndex) = p_strMatlNum
            m_view.SetupSKUErrMsg(p_intIndex, blnExistFlag)
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorCheckingCurrentMaterialText
        End Try
    End Sub

#End Region

End Class

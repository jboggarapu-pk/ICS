Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DepositoryTender

''' <summary>
''' Add certification model
''' </summary>
''' <remarks></remarks>
Public Class AddCertificationModel

    ' Changed sku to material number, as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

#Region "Methods"

    ''' <summary>
    ''' Check material number exist 
    ''' </summary>
    ''' <param name="p_strMatlNum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckMatlNumExist(ByVal p_strMatlNum As String) As Boolean

        Return Depository.Current.CheckMatlNumExists(p_strMatlNum)

    End Function

    ' Modified as per project 2706 technical specification
    ''' <summary>
    ''' Save certificate to material number association
    ''' </summary>
    ''' <param name="p_strCertNum"></param>
    ''' <param name="p_strMatlNum"></param>
    ''' <param name="p_intCertTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveCertificateSKUAssociation(ByVal p_strCertNum As String, ByVal p_strMatlNum As String, ByVal p_intCertTypeId As Integer, ByVal p_strImporter As String, ByVal p_strCustomer As String, ByVal p_strCertExtension As String) As Boolean

        Dim blnSaved As Boolean = False
        blnSaved = Depository.Current.SaveNewCertificate(p_strCertNum, p_intCertTypeId, p_strMatlNum, p_strImporter, p_strCustomer, SecurityModel.GetUserName(), p_strCertExtension)
        Return blnSaved

    End Function

    ' Modified as per project 2706 technical specification
    ''' <summary>
    ''' Save certificate to material number association
    ''' </summary>
    ''' <param name="p_strCertNum">Certificate Number</param>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <param name="p_intCertTypeId">Certificate Type Id</param>
    ''' <param name="p_strImporter">Importer</param>
    ''' <param name="p_strCustomer">Customer</param>
    ''' <param name="p_strCertExtension">Certification Extension</param>
    ''' <param name="p_InsertPC">Insert pc</param>
    ''' <param name="p_ErrorDesc">Error Desc.</param>
    ''' <returns>Result Number</returns>
    ''' <remarks></remarks>
    Public Function SaveCertificateSKUAssociation(ByVal p_strCertNum As String, ByVal p_strMatlNum As String, ByVal p_intCertTypeId As Integer, ByVal p_strImporter As String, ByVal p_strCustomer As String, ByVal p_strCertExtension As String, ByVal p_InsertPC As String, ByRef p_ErrorDesc As String) As Integer

        Dim resultNum As Integer
        resultNum = Depository.Current.SaveNewCertificate(p_strCertNum, p_intCertTypeId, p_strMatlNum, p_strImporter, p_strCustomer, SecurityModel.GetUserName(), p_strCertExtension, p_InsertPC, p_ErrorDesc)
        Return resultNum

    End Function

    Public Function GetImarkCertificateId() As Integer

        Dim strImarkCertId As Integer

        strImarkCertId = Depository.Current.GetLatestImarkCertifId()

        Return strImarkCertId

    End Function

    Public Function GetCertificateExtension(ByVal p_intImarkCertId As Integer) As String

        Dim strImarkCertExtension As String = String.Empty

        strImarkCertExtension = Depository.Current.GetCertifExtension(p_intImarkCertId)

        Return strImarkCertExtension

    End Function

    Public Function GetGSOCertificateNumber() As String
        Dim strGSOTempCertNumber As String = String.Empty

        strGSOTempCertNumber = Depository.Current.GetLatestGSOCertifNumber()

        Return strGSOTempCertNumber

    End Function

    Public Function GetImporters() As DataTable

        Dim dtbImporters As DataTable = Depository.Current.GetImporters()

        Return dtbImporters

    End Function

    Public Function GetCustomers() As DataTable

        Dim dtbCustomers As DataTable = Depository.Current.GetCustomers()

        Return dtbCustomers

    End Function


    Public Function GetCertificationTypeID(ByVal p_strCertificationTypeName As String) As Integer
        Dim nCertificationTypeId As Integer = 0

        nCertificationTypeId = Depository.Current.GetCertificationTypeID(p_strCertificationTypeName)

        Return nCertificationTypeId

    End Function

#End Region
End Class

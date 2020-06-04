Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DepositoryTender

''' <summary>
''' Contains data access methods related to Add Certification.
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
''' <term>Sujitha</term>
''' <description>
''' <para>11/25/2019</para>
''' <para>Implemented Coding Standards</para>
''' </description>
''' </item> 
''' </list>
''' </remarks>
Public Class AddCertificationModel

#Region "Methods"
    ' Changed sku to material number, as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

    ''' <summary>
    '''  Method to check material number exist or not.
    ''' </summary>
    ''' <param name="p_strMatlNum">Material Number.</param>
    ''' <returns>Boolean for Material number existence.</returns>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function CheckMatlNumExist(ByVal p_strMatlNum As String) As Boolean
        Try
            Return Depository.Current.CheckMatlNumExists(p_strMatlNum)
        Catch
            Throw
        End Try

    End Function

    ' Modified as per project 2706 technical specification

    ''' <summary>
    '''  Method to Save certificate to material number association
    ''' </summary>
    ''' <param name="p_strCertNum">Certificate Number</param>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <param name="p_intCertTypeId">Certificate Type Id</param>
    ''' <param name="p_strImporter">Importer</param>
    ''' <param name="p_strCustomer">Customer</param>
    ''' <param name="p_strCertExtension">Certification Extension</param>
    ''' <returns>Result Number</returns>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>  
 
    Public Function SaveCertificateSKUAssociation(ByVal p_strCertNum As String, ByVal p_strMatlNum As String, ByVal p_intCertTypeId As Integer, ByVal p_strImporter As String, ByVal p_strCustomer As String,
                                                  ByVal p_strCertExtension As String) As Boolean
        Try
            Dim blnSaved As Boolean = False
            blnSaved = Depository.Current.SaveNewCertificate(p_strCertNum, p_intCertTypeId, p_strMatlNum, p_strImporter, p_strCustomer, SecurityModel.GetUserName(), p_strCertExtension)
            Return blnSaved
        Catch ex As Exception
            Throw
        End Try
    End Function

    ' Modified as per project 2706 technical specification
    ''' <summary>
    '''  Method to Save certificate to material number association
    ''' </summary>
    ''' <param name="p_strCertNum">Certificate Number</param>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <param name="p_intCertTypeId">Certificate Type Id</param>
    ''' <param name="p_strImporter">Importer</param>
    ''' <param name="p_strCustomer">Customer</param>
    ''' <param name="p_strCertExtension">Certification Extension</param>
    ''' <param name="p_InsertPC">Insert pc></param>
    ''' <param name="p_ErrorDesc">Error Desc></param>
    ''' <returns>Result Number</returns>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>      
    Public Function SaveCertificateSKUAssociation(ByVal p_strCertNum As String, ByVal p_strMatlNum As String, ByVal p_intCertTypeId As Integer, ByVal p_strImporter As String, ByVal p_strCustomer As String,
                                                  ByVal p_strCertExtension As String, ByVal p_InsertPC As String, ByRef p_ErrorDesc As String) As Integer
        Try
            Dim resultNum As Integer
            resultNum = Depository.Current.SaveNewCertificate(p_strCertNum, p_intCertTypeId, p_strMatlNum, p_strImporter, p_strCustomer, SecurityModel.GetUserName(), p_strCertExtension, p_InsertPC,
                                                              p_ErrorDesc)
            Return resultNum
        Catch
            Throw
        End Try

    End Function


    ''' <summary>
    '''  Method to Get Imark certificateId
    ''' </summary> 
    ''' <returns>Integer Certificate Id</returns>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>  
    Public Function GetImarkCertificateId() As Integer
        Try
            Dim nImarkCertId As Integer
            nImarkCertId = Depository.Current.GetLatestImarkCertifId()
            Return nImarkCertId
        Catch
            Throw
        End Try
    End Function



    ''' <summary>
    '''  Method to  Get certification Extension
    ''' </summary>
    ''' <param name="p_intImarkCertId">Mark CertificateId.</param>
    ''' <returns>Result mark certificate Id</returns>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>  
       
    Public Function GetCertificateExtension(ByVal p_intImarkCertId As Integer) As String
        Try
            Dim strImarkCertExtension As String = String.Empty
            strImarkCertExtension = Depository.Current.GetCertifExtension(p_intImarkCertId)
            Return strImarkCertExtension
        Catch
            Throw
        End Try
    End Function


    ''' <summary>
    '''  Method to  Get GSO certification number
    ''' </summary>
    ''' <returns>Result GSO Certificate number</returns>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>        
    Public Function GetGSOCertificateNumber() As String
        Try
            Dim strGSOTempCertNumber As String = String.Empty
            strGSOTempCertNumber = Depository.Current.GetLatestGSOCertifNumber()
            Return strGSOTempCertNumber
        Catch
            Throw
        End Try
    End Function


    ''' <summary>
    '''  Method to  Get list of Importers
    ''' </summary>
    ''' <returns>Result list of Importers</returns>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>   
    Public Function GetImporters() As DataTable
        Try
            Dim dtbImporters As DataTable = Depository.Current.GetImporters()
            Return dtbImporters
        Catch
            Throw
        End Try

    End Function

    ''' <summary>
    '''  Method to  Get list of Customers
    ''' </summary>
    ''' <returns>Result list of Customers</returns>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>   
    Public Function GetCustomers() As DataTable
        Try
            Dim dtbCustomers As DataTable = Depository.Current.GetCustomers()
            Return dtbCustomers
        Catch
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Get certification Type Id
    ''' </summary>
    ''' <param name="p_strCertificationTypeName">Certification Type Name.</param>
    ''' <returns>Result Certification Type Id</returns>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>  
    Public Function GetCertificationTypeID(ByVal p_strCertificationTypeName As String) As Integer
        Try
            Dim nCertificationTypeId As Integer = 0
            nCertificationTypeId = Depository.Current.GetCertificationTypeID(p_strCertificationTypeName)
            Return nCertificationTypeId
        Catch
            Throw
        End Try
    End Function

#End Region
End Class

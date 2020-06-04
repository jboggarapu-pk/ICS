Imports CooperTire.ICS.DomainEntities
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DepositoryTender

''' <summary>
'''  Class containing Customer processing model
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
''' <para>09/26/2019</para>
''' <para>Implemented Coding Standards</para>
''' </description>
''' </item> 
''' </list>
''' </remarks> 
Public Class CustomerModel

#Region "Methods"

    ''' <summary>
    '''  Method to Save Customer data.
    ''' </summary>
    ''' <returns>NameAid.SaveResult</returns> 
    ''' <param name="p_objCustomer">Customer Object</param>
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
    ''' <para>09/26/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Public Function SaveCustomerData(ByVal p_objCustomer As Customer) As NameAid.SaveResult
        Try
            Debug.WriteLine("SaveCustomerData")
            Dim enuSaveResult As NameAid.SaveResult

            'Validate Objects prior the saving
            If Not p_objCustomer.DoValidate() Then
                Return NameAid.SaveResult.ValidationError
            End If

            'Starting the transaction to save the record to database
            enuSaveResult = SaveCustomer(p_objCustomer)

            Return enuSaveResult
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Save Customer.
    ''' </summary>
    ''' <returns>NameAid.SaveResult</returns> 
    ''' <param name="p_objCustomer">Customer Object</param>
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
    ''' <para>09/26/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Private Function SaveCustomer(ByVal p_objCustomer As Customer) As NameAid.SaveResult
        Try
            Dim enumTestResult As NameAid.SaveResult = NameAid.SaveResult.SaveError

            enumTestResult = Depository.Current.AddCustomer(p_objCustomer.SKUID, _
                                                            p_objCustomer.Customer_N, _
                                                            p_objCustomer.Importer_N, _
                                                            p_objCustomer.ImporterRepresentative_N, _
                                                            p_objCustomer.ImporterAddress_N, _
                                                            p_objCustomer.CountryLocation_N)

            Return enumTestResult
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Map datatable to customer entity
    ''' </summary>
    ''' <returns>Customer Object</returns> 
    ''' <param name="p_dtbCustomerDataInfo">Customer Data Info</param>
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
    ''' <para>09/26/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Private Function MapDataTableToCustomer(ByVal p_dtbCustomerDataInfo As DataTable) As Customer
        Try
            Dim objCustomer As Customer = Nothing

            If p_dtbCustomerDataInfo.Rows.Count = 0 Then
                Return objCustomer
            End If

            ' Keep object defaults unless there is a value in table.
            objCustomer = New Customer()

            If Not String.IsNullOrEmpty(p_dtbCustomerDataInfo.Rows(0)("SKUID").ToString()) Then
                objCustomer.SKUID = Convert.ToInt32(p_dtbCustomerDataInfo.Rows(0)("SKUID"))
            End If

            If Not String.IsNullOrEmpty(p_dtbCustomerDataInfo.Rows(0)("CUSTOMER").ToString()) Then
                objCustomer.Customer_N = p_dtbCustomerDataInfo.Rows(0)("CUSTOMER").ToString()
            End If

            If Not String.IsNullOrEmpty(p_dtbCustomerDataInfo.Rows(0)("IMPORTER").ToString()) Then
                objCustomer.Importer_N = p_dtbCustomerDataInfo.Rows(0)("IMPORTER").ToString()
            End If

            If Not String.IsNullOrEmpty(p_dtbCustomerDataInfo.Rows(0)("IMPORTERREPRESENTATIVE").ToString()) Then
                objCustomer.ImporterRepresentative_N = p_dtbCustomerDataInfo.Rows(0)("IMPORTERREPRESENTATIVE").ToString()
            End If

            If Not String.IsNullOrEmpty(p_dtbCustomerDataInfo.Rows(0)("IMPORTERADDRESS").ToString()) Then
                objCustomer.ImporterAddress_N = p_dtbCustomerDataInfo.Rows(0)("IMPORTERADDRESS").ToString()
            End If

            If Not String.IsNullOrEmpty(p_dtbCustomerDataInfo.Rows(0)("COUNTRYLOCATION").ToString()) Then
                objCustomer.CountryLocation_N = p_dtbCustomerDataInfo.Rows(0)("COUNTRYLOCATION").ToString()
            End If

            Return objCustomer
        Catch ex As Exception
            Throw
        End Try
    End Function

#End Region

End Class

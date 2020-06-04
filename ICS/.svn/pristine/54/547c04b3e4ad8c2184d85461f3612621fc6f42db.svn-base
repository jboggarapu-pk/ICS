Imports CooperTire.ICS.DomainEntities
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DepositoryTender

''' <summary>
''' Customer processing model
''' </summary>
''' <remarks></remarks>
Public Class CustomerModel

#Region " Members "
#End Region

#Region "Properties"
#End Region

#Region " Constructors / Destructors "
#End Region

#Region "Methods"

    ''' <summary>
    ''' Save Customer data
    ''' </summary>
    ''' <param name="p_objCustomer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveCustomerData(ByVal p_objCustomer As Customer) As NameAid.SaveResult

        Debug.WriteLine("SaveCustomerData")
        Dim enuSaveResult As NameAid.SaveResult

        'Validate Objects prior the saving
        If Not p_objCustomer.DoValidate() Then
            Return NameAid.SaveResult.ValidationError
        End If
        
        'Starting the transaction to save the record to database
        enuSaveResult = SaveCustomer(p_objCustomer)

        Return enuSaveResult

    End Function

    ''' <summary>
    ''' Save Customer data
    ''' </summary>
    ''' <param name="p_objCustomer"></param>
    ''' <returns></returns>
    ''' <remarks>non transactional</remarks>
    Private Function SaveCustomer(ByVal p_objCustomer As Customer) As NameAid.SaveResult

        Dim enumTestResult As NameAid.SaveResult = NameAid.SaveResult.SaveError

        enumTestResult = Depository.Current.AddCustomer(p_objCustomer.SKUID, _
                                                        p_objCustomer.Customer_N, _
                                                        p_objCustomer.Importer_N, _
                                                        p_objCustomer.ImporterRepresentative_N, _
                                                        p_objCustomer.ImporterAddress_N, _
                                                        p_objCustomer.CountryLocation_N)

        Return enumTestResult

    End Function

    ''' <summary>
    ''' Map datatable to cusotmer entity
    ''' </summary>
    ''' <param name="p_dtbCustomerDataInfo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MapDataTableToCustomer(ByVal p_dtbCustomerDataInfo As DataTable) As Customer

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

    End Function

#End Region

End Class

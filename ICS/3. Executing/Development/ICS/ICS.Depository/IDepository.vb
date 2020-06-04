
Imports CooperTire.ICS.DomainEntities
Imports CooperTire.ICS.Datasets
Imports CooperTire.ICS.Common

''' <summary>
''' To be implemented by all Depositories
''' </summary>
''' <remarks></remarks>
Public Interface IDepository
    Inherits IDisposable

    ' Modified the Interface methods as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation.
    ' Used Material Number instead of SKU, PSN instead of NPRId, TPN instead of PPN and Brand, Brand Line instead of Brand Code.
    ' Added Operation as paramter for HDR save methods. Also added some new methods to retrieve data from web service.
    ''' <summary>
    ''' Method to Get Requested Tests.
    ''' </summary>
    ''' <param name="p_dstClientRequest">Client Request</param>
    ''' <param name="p_iCertificationTypeId">Certification Type ID</param>
    ''' <param name="p_iTireTypeId">Tire Type Id</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetRequestedTests(ByVal p_iCertificationTypeId As Integer, ByVal p_iTireTypeId As Integer, ByVal p_dstClientRequest As DataSet) As DataSet

    ''' <summary>
    ''' Method to Get Audit Log.
    ''' </summary> 
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetAuditLog() As DataSet

    ''' <summary>
    ''' Method to Get audit log after date.
    ''' </summary>
    ''' <param name="p_dtmChangeDateTime">Change Date Time</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetAuditLogAfterDate(ByVal p_dtmChangeDateTime As DateTime) As DataSet

    ''' <summary>
    ''' Method to Get Approval reasons.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetApprovalReasons(ByVal p_intCertificationTypeId As Integer) As DataSet

    ''' <summary>
    ''' Method to Get Approved Substitution.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_intSKUID">SKU ID</param>
    ''' <param name="p_sngValue">Value</param>
    ''' <param name="p_strField">Field</param>
    ''' <returns>Single.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetApprovedSubstitution(ByVal p_intCertificationTypeId As Integer, ByVal p_strField As String, ByVal p_sngValue As Single, ByVal p_intSKUID As Integer) As Single
    ''' <summary>
    ''' Method to Get Default Value.
    ''' </summary>
    ''' <param name="p_intCertificateNumberID">Certificate Number Id</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <param name="p_strCertificateType">Certificate Type</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetDefaultValues(ByVal p_strCertificateType As String, ByVal p_intCertificateNumberID As Integer, ByRef p_strCertificateNumber As String) As DataSet

    ''' <summary>
    ''' Method to Get Query Control GridSource.
    ''' </summary>
    ''' <returns>Datatable.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetQueryControlGridSource() As DataTable

    ''' <summary>
    ''' Method to Certificate Default value Save.
    ''' </summary>
    ''' <param name="certDeftValues">Certificate Default Values</param>
    ''' <returns>Boolean.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function CertificateDefaultvalueSave(ByVal certDeftValues As List(Of CertificationDefaultField)) As Boolean

    ''' <summary>
    ''' Method to Certificate Value Save.
    ''' </summary>
    ''' <param name="p_certDeftValues">Certificate Default Values</param>
    ''' <param name="p_certificateNo">Certificate NUmber</param>
    ''' <returns>Boolean.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function CertificateValueSave(ByVal p_certDeftValues As List(Of CertificationDefaultField), ByVal p_certificateNo As String) As Boolean

    ''' <summary>
    ''' Method to Update Audit Log Entry.
    ''' </summary>
    ''' <param name="p_dtmChangeDateTime">Change DateTime</param>
    ''' <param name="p_intChangeLogID">Change Log Id</param>
    ''' <param name="p_strApprovalStatus">Approval Status</param>
    ''' <param name="p_strApprover">Approver</param>
    ''' <returns>Boolean.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function UpdateAuditLogEntry(ByVal p_intChangeLogID As Integer, ByVal p_dtmChangeDateTime As DateTime, ByVal p_strApprovalStatus As String, ByVal p_strApprover As String) As Boolean

    ''' <summary>
    ''' Method to Get ProductData SKUTRACS.
    ''' </summary>
    ''' <param name="strMatlNum">Material Number</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetProductData_SKUTRACS(ByVal strMatlNum As String) As ICS.Datasets.SKUtoICSDataset

    ''' <summary>
    ''' Method to Get TRACS Data.
    ''' </summary>
    ''' <param name="intCertType">Certificate Type</param>
    ''' <param name="intManufacturingLocationId">Manufacturing Location Id</param>
    ''' <param name="intTireType">Tire Type</param>
    ''' <param name="strMatlNum">Material Number</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetTRACSData(ByVal intCertType As Integer, ByVal intTireType As Integer, ByVal strMatlNum As String, ByVal intManufacturingLocationId As Integer) As ICS.Datasets.TRACStoICSDataset

    ''' <summary>
    ''' Method to Get Data For Emark Passenger Report.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_intTireTypeId">Tire Type Id</param>
    ''' <param name="p_strCertificateNumber">Certificate NUmber</param>
    ''' <param name="p_strExtension">Extension</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetDataForEmarkPassengerReport(ByVal p_strCertificateNumber As String, _
                                            ByVal p_intCertificationTypeId As Integer, _
                                            ByVal p_strExtension As String, _
                                            ByVal p_intTireTypeId As Integer) As DataSet
    ''' <summary>
    ''' Method to Get Data For Emark E117 Report.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_intTireTypeId">Tire Type Id</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <param name="p_strExtension">Extension</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetDataForEmarkE117Report(ByVal p_strCertificateNumber As String, _
                                            ByVal p_intCertificationTypeId As Integer, _
                                            ByVal p_strExtension As String, _
                                            ByVal p_intTireTypeId As Integer) As DataSet

    ''' <summary>
    ''' Method to Get Data For Emark Report With TR.
    ''' </summary>
    ''' <param name="p_intTireTypeId">Tire Type Id</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetDataForEmarkReportWithTR(ByVal p_strCertificateNumber As String, _
                                         ByVal p_intTireTypeId As Integer) As Dataset
    ''' <summary>
    ''' Method to Get Data For CCC Report.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <param name="p_strExtension">Extension</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetDataForCCCReport(ByVal p_strCertificateNumber As String, _
                                 ByVal p_intCertificationTypeId As Integer, _
                                 ByVal p_strExtension As String) As CCCSequentialDataSet
    ''' <summary>
    ''' Method to Get Data For CCC Product Description Report.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_strCertificateNumber">Certificate NUmber</param>
    ''' <param name="p_strExtension">Extension</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetDataForCCCProductDescriptionReport(ByVal p_strCertificateNumber As String, _
                                 ByVal p_intCertificationTypeId As Integer, _
                                 ByVal p_strExtension As String) As CCCProductDescriptionDataSet

    ' Added p_strBrandLine parameter as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Method to Get Data For SKU Certification.
    ''' </summary>
    ''' <param name="p_strBrand">Brand</param>
    ''' <param name="p_strBrandLine">Brand Line</param>
    ''' <param name="p_strCertType">Certificate Type</param>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <returns>CertificateReport Object.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetDataForSKUCertification(ByVal p_strMatlNum As String, ByVal p_strBrand As String, ByVal p_strBrandLine As String, ByVal p_strCertType As String) As CertificateReport


    ''' <summary>
    ''' Method to Get Data For Imark Certification.
    ''' </summary>
    ''' <param name="p_dtDateParam">\Date param</param>
    ''' <returns>ImarkCertificationDataSet Object.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetDataForImarkCertification(ByVal p_dtDateParam As DateTime) As ImarkCertificationDataSet

    ''' <summary>
    ''' Method to Get Data For GSO Passenger Report.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_intTireTypeId">Tire Type Id</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <param name="p_strExtension">Extension</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetDataForGSOPassengerReport(ByVal p_strCertificateNumber As String, _
                                          ByVal p_intCertificationTypeId As Integer, _
                                          ByVal p_strExtension As String, _
                                          ByVal p_intTireTypeId As Integer) As GSOCertificateDataSet
    ''' <summary>
    ''' Method to Get Data For GSO Conformity Report.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_intTireTypeId">Tire Type Id</param>
    ''' <param name="p_strBatchNumber">Batch Number</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetDataForGSOConformityReport(ByVal p_strBatchNumber As String, _
                                       ByVal p_intCertificationTypeId As Integer, _
                                       ByVal p_intTireTypeId As Integer) As DataSet

    ''' <summary>
    ''' Method to Get Data For Imark Conformity Report.
    ''' </summary>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetDataForImarkConformityReport() As DataSet

    ' Added p_strBrandLine parameter as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Method to Get Data For Emark Certification Report.
    ''' </summary>
    ''' <param name="p_strBrand">Brand</param>
    ''' <param name="p_strBrandLine">Brand Line</param>
    ''' <param name="p_strCertificationNo">Certification Number</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetDataForEmarkCertificationReport(ByVal p_strCertificationNo As String, ByVal p_strBrand As String, ByVal p_strBrandLine As String) As DataSet

    ''' <summary>
    ''' Method to Get Data For Imark Sampling Tire Tests Report.
    ''' </summary>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Function GetDataForImarkSamplingTireTestsReport(ByVal p_strMatlNum As String) As DataSet

    ''' <summary>
    ''' Method to Get Traceability Report Info.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <param name="p_strIncludeArchived">Include Archived</param>
    ''' <returns>Traceability Object.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetTraceabilityReportInfo(ByVal p_strCertificateNumber As String, ByVal p_intCertificationTypeID As Integer, ByVal p_strIncludeArchived As String) As Traceability

    ''' <summary>
    ''' Method to Get Exception Report Info.
    ''' </summary>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetExceptionReportInfo() As ExceptionReport_DataSet

    ''' <summary>
    ''' Method to Get Emark Application.
    ''' </summary>
    ''' <param name="p_intTireTypeID">Tire Type Id</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetEmarkApplication(ByVal p_strCertificateNumber As String, ByVal p_intTireTypeID As Integer) As DataSet

    ''' <summary>
    ''' Method to Get Nom Certification.
    ''' </summary>
    ''' <param name="p_intTireTypeID">Tire Type Id</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 

    Function GetNomCertification(ByVal p_strCertificateNumber As String, ByVal p_intTireTypeID As Integer) As DataSet

    ''' <summary>
    ''' Method to Get Authenticity Report.
    ''' </summary>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetAuthenticityReport() As DataSet

    ''' <summary>
    ''' Method to Get Emark Similar Certificate Search Report.
    ''' </summary>
    ''' <param name="p_MatlNum">Material Number</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetEmarkSimilarCertificateSearchReport(ByVal p_MatlNum As String) As DataSet

    ''' <summary>
    ''' Method to Get Certification Renewal.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetCertificationRenewal(ByVal p_strCertificateNumber As String, ByVal p_intCertificationTypeID As Integer) As DataSet

    ''' <summary>
    ''' Method to Check Similar Tire.
    ''' </summary>
    ''' <param name="ECEReference">ECE reference</param>
    ''' <param name="p_strSimilarMatlNum">Similar material Number</param>
    ''' <param name="intCertType">Certification Type</param>
    ''' <param name="intImarkFamily">Imark Family</param>
    ''' <param name="strInMatlNum">InMaterial Number</param>
    ''' <param name="strMessage">Message</param>
    ''' <returns>Integer.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function CheckSimilarTire(ByVal intCertType As Integer, ByVal strInMatlNum As String, ByRef p_strSimilarMatlNum As String, ByRef intImarkFamily As Integer, ByRef ECEReference As String, ByRef strMessage As String) As Integer

    'JBH_2.00 Project 5325: Added Mold Change Required and Operations Date Approved parameters
    'jeseitz 10/29/2016 added Additional Info field parameter
    ''' <summary>
    ''' Method to Save High Speed.
    ''' </summary>
    ''' <param name="p_intCertificateNumberID">Certificate Number Id</param>
    ''' <param name="p_strCERTIFICATENUMBER">Certificate Number</param>
    ''' <param name="p_strMatlNum"> Material Number</param>
    ''' <param name="p_blnAddNewCustomer">Add New Customer</param>
    ''' <param name="p_blnAddNewImporter">Add new Importer</param>
    ''' <param name="p_blnMoldChgRequired">Mold Change required</param>
    ''' <param name="p_blnRemoveMatlNum">Remove Material NUmber</param>
    ''' <param name="p_dteCertDateApproved_CEGI">Certdate Approved cegi</param>
    ''' <param name="p_dteCertDateSubmitted">Cert Date Submitted</param>
    ''' <param name="p_dteDATEAPROVED_CEGI">Date Approved CEGI</param>
    ''' <param name="p_dteDATEASSIGNED_EGI">Date Assigned EGI</param>
    ''' <param name="p_dteDATESUBMITED">Date Submitted</param>
    ''' <param name="p_dteEXPIRYDATE_I">Expiry Date I</param>
    ''' <param name="p_dteOperDateApproved">Operator Date Approved</param>
    ''' <param name="p_dteSUPPLEMENTALAPPROVED">Supplemental approved</param>
    ''' <param name="p_dteSUPPLEMENTALASSIGNED">Supplemental Assigned</param>
    ''' <param name="p_dteSUPPLEMENTALSUBMITTED">Supplemental Submitted</param>
    ''' <param name="p_intCustomerId">Customer Id</param>
    ''' <param name="p_intImporterId">Importer Id</param>
    ''' <param name="p_iSKUId">SKU ID</param>
    ''' <param name="p_strActSigReq">Act SigReq</param>
    ''' <param name="p_strAddInfo">Add Info</param> 
    ''' <param name="p_strBATCHNUMBER_G">Batch Number G</param>
    ''' <param name="p_strCOMPANYNAME">Company Name</param>
    ''' <param name="p_strCertificationTypeName">Certification Type Name</param>
    ''' <param name="p_strCOUNTRYLOCATION_N">Country Location N</param>
    ''' <param name="p_strCOUNTRYOFMANUFACTURE_N">Country of Manufacture N</param>
    ''' <param name="p_strCUSTOMER_N">Customer N</param>
    ''' <param name="p_strCustomerAddress">Customer Address</param>
    ''' <param name="p_strCUSTOMERSPECIFIC_N">Customer Specific N</param>
    ''' <param name="p_strEMARKREFERENCE_I">Emark Reference I</param>
    ''' <param name="p_strEXTENSION_EN">Extension EN</param>
    ''' <param name="p_strFAMILY_I">Family</param>
    ''' <param name="p_strFamilyDesc">Family Desc</param>
    ''' <param name="p_strImporter">Importer</param>
    ''' <param name="p_strImporterAddress">Importer Address</param>
    ''' <param name="p_strImporterRepresentative">Importer Representative</param>
    ''' <param name="p_strJOBREPORTNUMBER_CEN">Job report NUmber CEN</param>
    ''' <param name="p_strPRODUCTLOCATION">Product Location</param>
    ''' <param name="p_strSUPPLEMENTALMOLDSTAMPING_E">Supplemental Mold Stamping E</param>
    ''' <param name="p_strUserName">User Name</param>
    ''' <param name="pc_ACTIVESTATUS">Active Status</param>
    ''' <param name="pc_RENEWALREQUIRED_CGIN">Renewal Required CGIN</param>
    ''' <returns>NameAid.SaveResult Object.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function SaveCertificate(ByVal p_iSKUId As Integer, _
                                ByVal p_strMatlNum As String, _
                                ByVal p_blnRemoveMatlNum As Boolean, _
                                ByVal p_strCertificationTypeName As String, _
                                ByVal p_strCERTIFICATENUMBER As String, _
                                ByVal p_dteCertDateSubmitted As DateTime, _
                                ByVal p_dteCertDateApproved_CEGI As DateTime, _
                                ByVal p_dteDATESUBMITED As DateTime, _
                                ByVal pc_ACTIVESTATUS As String, _
                                ByVal p_dteDATEASSIGNED_EGI As DateTime, _
                                ByVal p_dteDATEAPROVED_CEGI As DateTime, _
                                ByVal pc_RENEWALREQUIRED_CGIN As Char, _
                                ByVal p_strJOBREPORTNUMBER_CEN As String, _
                                ByVal p_strEXTENSION_EN As String, _
                                ByVal p_strSUPPLEMENTALMOLDSTAMPING_E As String, _
                                ByVal p_strEMARKREFERENCE_I As String, _
                                ByVal p_dteEXPIRYDATE_I As DateTime, _
                                ByVal p_strFAMILY_I As String, _
                                ByVal p_strPRODUCTLOCATION As String, _
                                ByVal p_strCOUNTRYOFMANUFACTURE_N As String, _
                                ByVal p_blnAddNewCustomer As Boolean, _
                                ByVal p_strActSigReq As String, _
                                ByVal p_intCustomerId As Integer, _
                                ByVal p_strCUSTOMER_N As String, _
                                ByVal p_strCustomerAddress As String, _
                                ByVal p_strCUSTOMERSPECIFIC_N As String, _
                                ByVal p_blnAddNewImporter As Boolean, _
                                ByVal p_intImporterId As Integer, _
                                ByVal p_strImporter As String, _
                                ByVal p_strImporterAddress As String, _
                                ByVal p_strImporterRepresentative As String, _
                                ByVal p_strCOUNTRYLOCATION_N As String, _
                                ByVal p_strBATCHNUMBER_G As String, _
                                ByVal p_dteSUPPLEMENTALASSIGNED As DateTime, _
                                ByVal p_dteSUPPLEMENTALSUBMITTED As DateTime, _
                                ByVal p_dteSUPPLEMENTALAPPROVED As DateTime, _
                                ByVal p_strCOMPANYNAME As String, _
                                ByVal p_strUserName As String, _
                                ByRef p_intCertificateNumberID As Integer, _
                                ByVal p_strFamilyDesc As String, _
                                ByVal p_blnMoldChgRequired As Boolean, _
                                ByVal p_dteOperDateApproved As DateTime, _
                                ByVal p_strAddInfo As String) As NameAid.SaveResult
    ''' <summary>
    ''' Method to Batch Num Mass Update.
    ''' </summary>
    ''' <param name="p_strCertifName">Certificate Name</param>
    ''' <param name="p_strGSOBatchNum">GSO Batch Num</param>
    ''' <param name="p_strTempBatchNum">Temp Batch Num</param>
    ''' <param name="p_strUserName">User Name</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function BatchNumMassUpdate(ByVal p_strCertifName As String, _
                                ByVal p_strTempBatchNum As String, _
                                ByVal p_strGSOBatchNum As String, _
                                ByVal p_strUserName As String) As NameAid.SaveResult
    ''' <summary>
    ''' Method to Get Region Certification Status.
    ''' </summary>
    ''' <param name="p_strBrand">Brand</param>
    ''' <param name="p_strBrandLine">Brand line</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetRegionCertStatus(ByVal p_strBrand As String, ByVal p_strBrandLine As String) As DataSet

    ''' <summary>
    ''' Method to Get Product Certification Status.
    ''' </summary>
    ''' <param name="p_strBrand">Brand</param>
    ''' <param name="p_strBrandLine">Brand Line</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetProductCertStatus(ByVal p_strBrand As String, ByVal p_strBrandLine As String) As DataSet

    ''' <summary>
    ''' Method to Get Countries.
    ''' </summary>
    ''' <param name="p_strRegionName">Region Name</param>
    ''' <returns>Datatable.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetCountries(ByVal p_strRegionName As String) As DataTable

    ''' <summary>
    ''' Method to Get Importers.
    ''' </summary>
    ''' <returns>Datatable.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetImporters() As DataTable

    ''' <summary>
    ''' Method to Get Customers.
    ''' </summary>
    ''' <returns>Datatable.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetCustomers() As DataTable

    ''' <summary>
    ''' Method to Save Certification Request.
    ''' </summary>
    ''' <param name="p_intCountryID">Country ID</param>
    ''' <param name="p_blnDeleteMe">Delete Me</param>
    ''' <param name="p_intSKUID">SKU ID</param>
    ''' <param name="p_strMatlNum">Material NUmber</param>
    ''' <returns>Boolean.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function SaveCertificationRequest(ByVal p_blnDeleteMe As Boolean, _
                                      ByVal p_strMatlNum As String, _
                                      ByVal p_intCountryID As Integer, _
                                      ByVal p_intSKUID As Integer) As Boolean
    ''' <summary>
    ''' Method to Save Certification Group.
    ''' </summary>
    ''' <param name="p_intCertificationID">Certification Id</param>
    ''' <param name="p_blnDeleteMe">Delete Me</param>
    ''' <param name="p_intSKUID">SKU ID</param>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <returns>Boolean.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function SaveCertificationGroup(ByVal p_blnDeleteMe As Boolean, _
                                    ByVal p_strMatlNum As String, _
                                    ByVal p_intCertificationID As Integer, _
                                    ByVal p_intSKUID As Integer) As Boolean
    ''' <summary>
    ''' Method to Save Request Certificate.
    ''' </summary>
    ''' <param name="p_intCertificationID">Certification Id</param>
    ''' <param name="p_blnDeleteMe">Delete Me</param>
    ''' <param name="p_intSKUID">SKU ID</param>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <returns>Boolean.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function SaveRequestCert(ByVal p_blnDeleteMe As Boolean, _
                                ByVal p_strMatlNum As String, _
                                ByVal p_intCertificationID As Integer, _
                                ByVal p_intSKUID As Integer) As Boolean
    ''' <summary>
    ''' Method to Check Material NumBER Exists.
    ''' </summary>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function CheckMatlNumExists(ByVal p_strMatlNum As String) As Boolean

    ''' <summary>
    ''' Method to Refresh Product.
    ''' </summary>
    ''' <param name="p_strErrorDesc">Error Description</param>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <returns>Integer.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function RefreshProduct(ByVal p_strMatlNum As String, ByRef p_strErrorDesc As String) As Integer


    ''' <summary>
    ''' Method to Check If Certificate Number Exists.
    ''' </summary>
    ''' <param name="p_strCertNum">Certification TNumber</param>
    ''' <returns>Boolean.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function CheckIfCertificateNumberExists(ByVal p_strCertNum As String) As Boolean

    ''' <summary>
    ''' Method to Get Latest Imark Certificate Id.
    ''' </summary>
    ''' <returns>Integer.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetLatestImarkCertifId() As Integer

    ''' <summary>
    ''' Method to Renew Certificate.
    ''' </summary>
    ''' <param name="p_intCertificateId">Certification Id</param>
    ''' <param name="p_intNewCertificateID">New Certificate Id</param>
    ''' <param name="p_strUserName">User Name</param>
    ''' <returns>SaveResult Object.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function RenewCertificate(ByVal p_intCertificateId As Integer, ByRef p_intNewCertificateID As Integer, ByVal p_strUserName As String) As NameAid.SaveResult

    ''' <summary>
    ''' Method to Get Certificate Extension.
    ''' </summary>
    ''' <param name="p_intImarkCertId">IMark Certificate Id</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetCertifExtension(ByVal p_intImarkCertId As Integer) As String

    ''' <summary>
    ''' Method to Get Latest GSO Certificate Number.
    ''' </summary>
    ''' <returns>String.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetLatestGSOCertifNumber() As String

    ''' <summary>
    ''' Method to Save New Certificate.
    ''' </summary>
    ''' <param name="p_intCertTypeId">Certificate Type Id</param>
    ''' <param name="p_strCertNum">Certificate Number</param>
    ''' <param name="p_strCustomer">Customer</param>
    ''' <param name="p_strExtension">Extension</param>
    ''' <param name="p_strImporter">Importer</param>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <param name="p_strUserName">User Name</param>
    ''' <returns>Boolean.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function SaveNewCertificate(ByVal p_strCertNum As String, _
                                ByVal p_intCertTypeId As Integer, _
                                ByVal p_strMatlNum As String, _
                                ByVal p_strImporter As String, _
                                ByVal p_strCustomer As String, _
                                ByVal p_strUserName As String, _
                                ByVal p_strExtension As String) As Boolean
    ''' <summary>
    ''' Method to Save New Certificate.
    ''' </summary>
    ''' <param name="p_intCertTypeId">Certification Type Id</param>
    ''' <param name="p_ErrorDesc">Error Description</param>
    ''' <param name="p_InsertPC">Insert PC</param>
    ''' <param name="p_strCertNum">Certificate Number</param>
    ''' <param name="p_strCustomer">Customer</param>
    ''' <param name="p_strExtension">Extension</param>
    ''' <param name="p_strImporter">Importer</param>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <param name="p_strUserName">UserName</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function SaveNewCertificate(ByVal p_strCertNum As String, _
                               ByVal p_intCertTypeId As Integer, _
                               ByVal p_strMatlNum As String, _
                               ByVal p_strImporter As String, _
                               ByVal p_strCustomer As String, _
                               ByVal p_strUserName As String, _
                               ByVal p_strExtension As String, _
                               ByVal p_InsertPC As String, _
                               ByRef p_ErrorDesc As String) As Integer
    ''' <summary>
    ''' Method to Archive Certification.
    ''' </summary>
    ''' <param name="p_strCertNum">Certification Number</param>
    ''' <param name="p_strUserName">User Name</param>
    ''' <returns>Boolean.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function ArchiveCertification(ByVal p_strCertNum As String, _
                                  ByVal p_strUserName As String) As Boolean
    ''' <summary>
    ''' Method to Save Audit Log Entry.
    ''' </summary>
    ''' <param name="p_dteChangeDateTime">Change DateTime</param>
    ''' <param name="m_intReasonID">Reason Id</param>
    ''' <param name="m_strArea">Area</param>
    ''' <param name="m_strChangedBy">Changed by</param>
    ''' <param name="m_strChangedFieldElement">Changed Field Element</param>
    ''' <param name="m_strNewValue">New Value</param>
    ''' <param name="m_strNote">Note</param>
    ''' <param name="m_strOldValue">Old Value</param>
    ''' <returns>Boolean.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function SaveAuditLogEntry(ByVal p_dteChangeDateTime As DateTime, _
                                ByVal m_strChangedBy As String, _
                                ByVal m_strArea As String, _
                                ByVal m_strChangedFieldElement As String, _
                                ByVal m_strOldValue As String, _
                                ByVal m_strNewValue As String, _
                                ByVal m_intReasonID As Integer, _
                                ByVal m_strNote As String) As System.Boolean
    ''' <summary>
    ''' Method to Get Certifications.
    ''' </summary>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetCertifications() As DataSet

    ''' <summary>
    ''' Method to Get Search Type Results.
    ''' </summary>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetSearchTypeResults() As DataSet

    ''' <summary>
    ''' Method to Get Manufacturing Locations Results.
    ''' </summary>
    ''' <param name="p_strSize">Size</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetManufacturingLocationsResults(ByVal p_strSize As String) As DataSet

    ''' <summary>
    ''' Method to Get Company Name List.
    ''' </summary>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetCompanyNameList() As DataSet

    ''' <summary>
    ''' Method to Get Certification Search Results.
    ''' </summary>
    ''' <param name="p_strExtensionNo">Extension Number</param>
    ''' <param name="p_strImarkFamily">IMark Family</param>
    ''' <param name="p_strSearchCriteria">Search Criteria</param>
    ''' <param name="p_strSearchType">Search Type</param>
    ''' <param name="ps_BrandLine">Brand Line</param>
    ''' <returns>Datatable.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetCertificationSearchResults(ByVal p_strSearchCriteria As String, _
                                           ByVal p_strSearchType As String, _
                                           ByVal p_strExtensionNo As String, _
                                           ByVal p_strImarkFamily As String, _
                                           ByVal ps_BrandLine As String) As DataTable

    ''' <summary>
    ''' Method to Get Certificate.
    ''' </summary>
    ''' <param name="ps_CertificationTypeID">Certification Type Id</param>
    ''' <param name="p_blnTRsExist">TRs Exist</param>
    ''' <param name="p_iSKUID">SKU ID</param>
    ''' <param name="ps_CertificatonNumber">Certification Number</param>
    ''' <param name="ps_ExtensionNo">Extension number</param>
    ''' <returns>Datatable.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetCertificate(ByVal ps_CertificatonNumber As String, ByVal ps_ExtensionNo As String, ByVal ps_CertificationTypeID As Integer, ByVal p_iSKUID As Integer, ByRef p_blnTRsExist As Boolean) As DataTable

    ''' <summary>
    ''' Method to Get Similar Certificate.
    ''' </summary>
    ''' <param name="p_iCertificationTypeID">Certification Type Id</param>
    ''' <param name="p_strCertificationNumber">Certification Number</param>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <returns>Datatable.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetSimilarCertificate(ByVal p_iCertificationTypeID As Integer, ByVal p_strMatlNum As String, ByVal p_strCertificationNumber As String) As DataTable


    ''' <summary>
    ''' Method to Get Product Data.
    ''' </summary>
    ''' <param name="p_iSKUID">SKU ID</param>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <returns>Datatable.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetProductData(ByVal p_strMatlNum As String, ByVal p_iSKUID As Integer) As ICSDataSet.ProductDataDataTable

    ''' <summary>
    ''' Method to Get Test Result Data.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_intCertificateNumberID">Certificate Number Id</param>
    ''' <param name="p_intSKUID">SKU ID</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetTestResultData(ByVal p_strMatlNum As String, ByVal p_intSKUID As Integer, ByVal p_strCertificateNumber As String, ByVal p_intCertificateNumberID As Integer, ByVal p_intCertificationTypeId As Integer) As ICSDataSet

    ''' <summary>
    ''' Method to Save product.
    ''' </summary>
    ''' <param name="p_dteDiscontinuedDate">DiscontinuedDate</param>
    ''' <param name="p_dteFechaDate">Fecha Date</param>
    ''' <param name="p_dteMostRecentTestDate">Most Recent Test Date</param>
    ''' <param name="p_dteSerialDate">Serial Date</param>
    ''' <param name="p_iRIMDIAMETER">RIM Diameter</param>
    ''' <param name="p_iSKUID">SKU ID</param>
    ''' <param name="p_iTireTypeId">Tire Type ID</param>
    ''' <param name="p_strAspectRadio">Aspect Radio</param>
    ''' <param name="p_strBELTEDRADIALYN">Belted Radial YN</param>
    ''' <param name="p_strBrand">Brand</param>
    ''' <param name="p_strBrandDesc">Brand Desc</param>
    ''' <param name="p_strBrandLine">BrandLine</param>
    ''' <param name="p_strDOTSerialNumber">DOT Serial Number</param>
    ''' <param name="p_strDUALLOADINDEX">DUAL load Index</param>
    ''' <param name="p_strEXTRALOADYN">Extra Load YN</param>
    ''' <param name="p_strFamily">Family</param>
    ''' <param name="p_strIMark">I Mark</param>
    ''' <param name="p_strInformeNumber">InformeNumber</param>
    ''' <param name="p_strLoadRange">Load Range</param>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <param name="p_strMeaRimWidth">MeaRim Width</param>
    ''' <param name="p_strMFGWWYY">MFGWWYY</param>
    ''' <param name="p_strMUDSNOWYN">MUDSNOWYN</param>
    ''' <param name="p_strNameOfManufacturer">Name of Manufacturer</param>
    ''' <param name="p_strNominalTireWidth">Nominal Tire Width</param>
    ''' <param name="p_strPlantProduced">Plant Produced</param>
    ''' <param name="p_strPSN">PSN</param>
    ''' <param name="p_strRegroovableInd">Regroovable Ind</param>
    ''' <param name="p_strREINFORCEDYN">Reinforced YN</param>
    ''' <param name="p_strSEVEREWEATHERIND">Severe Weather Ind</param>
    ''' <param name="p_strSINGLOADINDEX">SingLoad Index</param>
    ''' <param name="p_strSizeStamp">Size Stamp</param>
    ''' <param name="p_strSpecialProtectiveBand">Special Protective Brand</param>
    ''' <param name="p_strSPECNUMBER">SPEC Number</param>
    ''' <param name="p_strSPEEDRATING">Speed rating</param>
    ''' <param name="p_strTPN">TPN</param>
    ''' <param name="p_strTreadPattern">Tread Pattern</param>
    ''' <param name="p_strTreadwearIndicators">TreadWear Indicators</param>
    ''' <param name="p_strTUBElESSYN">Tubeless YN</param>
    ''' <param name="p_strUserName">User Name</param>
    ''' <param name="p_strUTQGTEMP">UTQG Temp</param>
    ''' <param name="p_strUTQGTRACTION">UTQG Traction</param>
    ''' <param name="p_strUTQGTREADWEAR">UTQG Tread wear</param>
    ''' <returns>SaveResult Object.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function Save_Product(ByVal p_iSKUID As Integer, _
                         ByVal p_strMatlNum As String, _
                         ByVal p_strBrand As String, _
                         ByVal p_strBrandLine As String, _
                         ByVal p_iTireTypeId As Integer, _
                         ByVal p_strPSN As String, _
                         ByVal p_strSizeStamp As String, _
                         ByVal p_dteDiscontinuedDate As DateTime, _
                         ByVal p_strSPECNUMBER As String, _
                         ByVal p_strSPEEDRATING As String, _
                         ByVal p_strSINGLOADINDEX As String, _
                         ByVal p_strDUALLOADINDEX As String, _
                         ByVal p_strBELTEDRADIALYN As String, _
                         ByVal p_strTUBElESSYN As String, _
                         ByVal p_strREINFORCEDYN As String, _
                         ByVal p_strEXTRALOADYN As String, _
                         ByVal p_strUTQGTREADWEAR As String, _
                         ByVal p_strUTQGTRACTION As String, _
                         ByVal p_strUTQGTEMP As String, _
                         ByVal p_strMUDSNOWYN As String, _
                         ByVal p_iRIMDIAMETER As Single, _
                         ByVal p_dteSerialDate As DateTime, _
                         ByVal p_strBrandDesc As String, _
                         ByVal p_strMeaRimWidth As Single, _
                         ByVal p_strLoadRange As String, _
                         ByVal p_strRegroovableInd As String, _
                         ByVal p_strPlantProduced As String, _
                         ByVal p_dteMostRecentTestDate As DateTime, _
                         ByVal p_strIMark As String, _
                         ByVal p_strInformeNumber As String, _
                         ByVal p_dteFechaDate As DateTime, _
                         ByVal p_strTreadPattern As String, _
                         ByVal p_strSpecialProtectiveBand As String, _
                         ByVal p_strNominalTireWidth As String, _
                         ByVal p_strAspectRadio As String, _
                         ByVal p_strTreadwearIndicators As String, _
                         ByVal p_strNameOfManufacturer As String, _
                         ByVal p_strFamily As String, _
                         ByVal p_strDOTSerialNumber As String, _
                         ByVal p_strTPN As String, _
                         ByVal p_strUserName As String, _
                         ByVal p_strSEVEREWEATHERIND As String, _
                         ByVal p_strMFGWWYY As String) As NameAid.SaveResult

    'jeseitz added 4/7/16
    ''' <summary>
    ''' Method to Get Certificates By Type.
    ''' </summary>
    ''' <param name="p_certificationtypeid">Certification Type Id</param>
    ''' <param name="p_all">All</param>
    ''' <returns>Datatable.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetCertificatesByType(ByVal p_certificationtypeid As Integer, ByVal p_all As String) As DataTable

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Method to Save Measurement.
    ''' </summary>
    ''' <param name="p_intCertificateID">Certification Id</param>
    ''' <param name="p_dteCOMPLETIONDATE">Completion Date</param>
    ''' <param name="p_dteEndTime">End Time</param>
    ''' <param name="p_dteMOUNTTIME">Mount Time</param>
    ''' <param name="p_dteSerialDate">Serial Date</param>
    ''' <param name="p_intCertType">Certificate Type</param>
    ''' <param name="p_intMEASUREID">Measure Id</param>
    ''' <param name="p_intSKUID">SKU ID</param>
    ''' <param name="p_sngActSizeFactor">ActSize Factor</param>
    ''' <param name="p_sngAVGOVERALLWIDTH">Average Overall Width</param>
    ''' <param name="p_sngAVGSECTIONWIDTH">Average Section width</param>
    ''' <param name="p_sngCIRCUNFERENCE">Circumference</param>
    ''' <param name="p_sngDIAMETER">Diameter</param>
    ''' <param name="p_sngDIAMETERDIFERENCE">Diameter Difference</param>
    ''' <param name="p_sngDIAMETERTOLERANCE">Diameter Tolerance</param>
    ''' <param name="p_sngELONGATION1">Longation1</param>
    ''' <param name="p_sngELONGATION2">Longation2</param>
    ''' <param name="p_sngINFLATIONPRESSURE">Inflation pressure</param>
    ''' <param name="p_sngMAXOVERALLDIAMETER">Max Overall Diameter</param>
    ''' <param name="p_sngMAXOVERALLWIDTH">Max Overall Width</param>
    ''' <param name="p_sngMINOVERALLDIAMETER">Min Overall Diameter</param>
    ''' <param name="p_sngMOUNTTEMP">Mount Temp</param>
    ''' <param name="p_sngNOMINALDIAMETER">Nominal Diameter</param>
    ''' <param name="p_sngNOMINALWIDTH">Nominal Width</param>
    ''' <param name="p_sngNOMINALWIDTHDIFERENCE">Nominal WiIdth Difference</param>
    ''' <param name="p_sngNOMINALWIDTHTOLERANCE">Nominal Width Tolerance</param>
    ''' <param name="p_sngRIMWIDTH">RIM Width</param>
    ''' <param name="p_sngSIZEFACTOR">Size Factor</param>
    ''' <param name="p_sngTENSILESTRENGHT1">Tensiles Strength1</param>
    ''' <param name="p_sngTENSILESTRENGHT2">Tensiles Strength2</param>
    ''' <param name="p_sngTENSILESTRENGHTAFTERAGE1">Tensile Strength after age1</param>
    ''' <param name="p_sngTENSILESTRENGHTAFTERAGE2">Tensile Strength after age2</param>
    ''' <param name="p_sngTIRENUMBER">Tire Number</param>
    ''' <param name="p_srtENDINFLATIONPRESSURE">End Inflation Pressure</param>
    ''' <param name="p_srtSTARTINFLATIONPRESSURE">Start Inflation Pressure</param>
    ''' <param name="p_strADJUSTMENT">Adjustment</param>
    ''' <param name="p_strCERTIFICATENUMBER">Certificate Number</param>
    ''' <param name="p_strDOTSERIALNUMBER">Dot Serial Number</param>
    ''' <param name="p_strGTSpecMeasurement">GT Spec Measurement</param>
    ''' <param name="p_strMatlNum">Material NUmber</param>
    ''' <param name="p_strMFGWWYY">MFGWWYY</param>
    ''' <param name="p_strMOLDDESIGN">Mold Design</param>
    ''' <param name="p_strNOMINALWIDTHPASSFAIL">Nominal Width Pass Fail</param>
    ''' <param name="p_strOperation">Operation</param>
    ''' <param name="p_strOperatorName">Operator Name</param>
    ''' <param name="p_strOVERALLDIAMETERPASSFAIL">Overall Diameter Pass Fail</param>
    ''' <param name="p_strOVERALLWIDTHPASSFAIL">Overall Width Pass Fail</param>
    ''' <param name="p_strPROJECTNUMBER">Project Number</param>
    ''' <param name="p_strTEMPRESISTANCEGRADING">Temp Resistance Grading</param>
    ''' <param name="p_strTESTSPEC">Test spec</param>
    ''' <returns>Save Result Object.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function SaveMeasurement(ByVal p_strPROJECTNUMBER As String, _
                            ByVal p_sngTIRENUMBER As Single, _
                            ByVal p_strTESTSPEC As String, _
                            ByVal p_dteCOMPLETIONDATE As DateTime, _
                            ByVal p_sngINFLATIONPRESSURE As Single, _
                            ByVal p_strMOLDDESIGN As String, _
                            ByVal p_sngRIMWIDTH As Single, _
                            ByVal p_strDOTSERIALNUMBER As String, _
                            ByVal p_sngDIAMETER As Single, _
                            ByVal p_sngAVGSECTIONWIDTH As Single, _
                            ByVal p_sngAVGOVERALLWIDTH As Single, _
                            ByVal p_sngMAXOVERALLWIDTH As Single, _
                            ByVal p_sngSIZEFACTOR As Single, _
                            ByVal p_dteMOUNTTIME As DateTime, _
                            ByVal p_sngMOUNTTEMP As Single, _
                            ByVal p_intSKUID As Integer, _
                            ByVal p_intCertType As Integer, _
                            ByVal p_strCERTIFICATENUMBER As String, _
                            ByRef p_intMEASUREID As Integer, _
                            ByVal p_dteSerialDate As DateTime, _
                            ByVal p_dteEndTime As DateTime, _
                            ByVal p_sngActSizeFactor As Single, _
                            ByVal p_srtSTARTINFLATIONPRESSURE As Short, _
                            ByVal p_srtENDINFLATIONPRESSURE As Short, _
                            ByVal p_strADJUSTMENT As String, _
                            ByVal p_sngCIRCUNFERENCE As Single, _
                            ByVal p_sngNOMINALDIAMETER As Single, _
                            ByVal p_sngNOMINALWIDTH As Single, _
                            ByVal p_strNOMINALWIDTHPASSFAIL As String, _
                            ByVal p_sngNOMINALWIDTHDIFERENCE As Single, _
                            ByVal p_sngNOMINALWIDTHTOLERANCE As Single, _
                            ByVal p_sngMAXOVERALLDIAMETER As Single, _
                            ByVal p_sngMINOVERALLDIAMETER As Single, _
                            ByVal p_strOVERALLWIDTHPASSFAIL As String, _
                            ByVal p_strOVERALLDIAMETERPASSFAIL As String, _
                            ByVal p_sngDIAMETERDIFERENCE As Single, _
                            ByVal p_sngDIAMETERTOLERANCE As Single, _
                            ByVal p_strTEMPRESISTANCEGRADING As String, _
                            ByVal p_sngTENSILESTRENGHT1 As Single, _
                            ByVal p_sngTENSILESTRENGHT2 As Single, _
                            ByVal p_sngELONGATION1 As Single, _
                            ByVal p_sngELONGATION2 As Single, _
                            ByVal p_sngTENSILESTRENGHTAFTERAGE1 As Single, _
                            ByVal p_sngTENSILESTRENGHTAFTERAGE2 As Single, _
                            ByVal p_strOperatorName As String, _
                            ByVal p_intCertificateID As Integer, _
                            ByVal p_strMatlNum As String, _
                            ByVal p_strOperation As String, _
                            ByVal p_strGTSpecMeasurement As String, _
                            ByVal p_strMFGWWYY As String) As NameAid.SaveResult
    ''' <summary>
    ''' Method to Measurement Detail Save.
    ''' </summary>
    ''' <param name="p_iMEASUREID">Measure Id</param>
    ''' <param name="p_sngITERATION">Iteration</param>
    ''' <param name="p_sngOVERALLWIDTH">Overall Width</param>
    ''' <param name="p_sngSectionWidth">Section Width</param>
    ''' <param name="p_strUserName">UserName</param>
    ''' <returns>SaveResult Object.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function MeasurementDetail_Save(ByVal p_sngSectionWidth As Single, _
                                   ByVal p_sngOVERALLWIDTH As Single, _
                                   ByVal p_iMEASUREID As Integer, _
                                   ByVal p_sngITERATION As Single, _
                                   ByVal p_strUserName As String) As NameAid.SaveResult

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Method to Save Plunger.
    ''' </summary>
    ''' <param name="p_intCertificateNumberID">Certification Number Id</param>
    ''' <param name="p_dteCOMPLETIONDATE">Completion Date</param>
    ''' <param name="p_dteSerialDate">Serial Date</param>
    ''' <param name="p_intCertType">Certificate Type</param>
    ''' <param name="p_intPLUNGERID">Plunger ID</param>
    ''' <param name="p_intSKUID">SKU ID</param>
    ''' <param name="p_sngAVGBREAKINGENERGY">Average Breaking Energy</param>
    ''' <param name="p_sngMinPlunger">sngMin Plunger</param>
    ''' <param name="p_sngTIRENUMBER">Tire NUmber</param>
    ''' <param name="p_strCERTIFICATENUMBER">Certificate Number</param>
    ''' <param name="p_strDOTSERIALNUMBER">DOT Serial Number</param>
    ''' <param name="p_strGTSpecPlunger">GT Spec Plunger</param>
    ''' <param name="p_strMatlNum">Material NUmber</param>
    ''' <param name="p_strMFGWWYY">MFGWWYY</param>
    ''' <param name="p_strOperation">Operation</param>
    ''' <param name="p_strPASSYN">Pass YN</param>
    ''' <param name="p_strPROJECTNUMBER">Project NUmber</param>
    ''' <param name="p_strTESTSPEC">TEST Spec</param>
    ''' <param name="p_strUserName">User Name</param>
    ''' <returns>SaveResult Object.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function SavePlunger(ByVal p_strPROJECTNUMBER As String, _
                        ByVal p_sngTIRENUMBER As Single, _
                        ByVal p_strTESTSPEC As String, _
                        ByVal p_dteCOMPLETIONDATE As DateTime, _
                        ByVal p_strDOTSERIALNUMBER As String, _
                        ByVal p_sngAVGBREAKINGENERGY As Single, _
                        ByVal p_strPASSYN As String, _
                        ByVal p_intSKUID As Integer, _
                        ByVal p_intCertType As Integer, _
                        ByVal p_strCERTIFICATENUMBER As String, _
                        ByRef p_intPLUNGERID As Integer, _
                        ByVal p_dteSerialDate As DateTime, _
                        ByVal p_sngMinPlunger As Single, _
                        ByVal p_strUserName As String, _
                        ByVal p_intCertificateNumberID As Integer, _
                        ByVal p_strMatlNum As String, _
                        ByVal p_strOperation As String, _
                        ByVal p_strGTSpecPlunger As String, _
                        ByVal p_strMFGWWYY As String) As NameAid.SaveResult
    ''' <summary>
    ''' Method to Save Plunger Detail.
    ''' </summary>
    ''' <param name="p_intPlungerID">Plunger ID</param>
    ''' <param name="p_sngBREAKINGENERGY">sngBreaking Energy</param>
    ''' <param name="p_sngIteration">Iteration</param>
    ''' <param name="p_strUserName">UserName</param>
    ''' <returns>Save result Object.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function SavePlungerDetail(ByVal p_sngBREAKINGENERGY As Single, _
                              ByVal p_intPlungerID As Integer, _
                              ByVal p_sngIteration As Single, _
                              ByVal p_strUserName As String) As NameAid.SaveResult

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Method to Save TreadWear.
    ''' </summary>
    ''' <param name="p_intCertificateID">Certification Id</param>
    ''' <param name="p_dteCOMPLETIONDATE">Completion Date</param>
    ''' <param name="p_dteSERIALDATE">Serial Date</param>
    ''' <param name="p_intCertType">Certificate Type</param>
    ''' <param name="p_intSKUID">SKU ID</param>
    ''' <param name="p_intTREADWEARID">Tread Wear Id</param>
    ''' <param name="p_sngINDICATORSREQUIREMENT">sng Indicators Requirement</param>
    ''' <param name="p_sngLOWESTWEARBAR">Lowest Wear Bar</param>
    ''' <param name="p_sngTIRENUMBER">Tire Number</param>
    ''' <param name="p_strCERTIFICATENUMBER">Certificate Number</param>
    ''' <param name="p_strDOTSERIALNUMBER">DOT Serial Number</param>
    ''' <param name="p_strGTSpecTreadWear">GT Spec TreadWear</param>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <param name="p_strMFGWWYY">MFGWWYY</param>
    ''' <param name="p_strOperation">Operation</param>
    ''' <param name="p_strOperatorName">Operator Name</param>
    ''' <param name="p_strPassyn">Pass YN</param>
    ''' <param name="p_strPROJECTNUMBER">Project Number</param>
    ''' <param name="p_strTESTSPEC">Test Spec</param>
    ''' <returns>Save Result Object.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function SaveTreadWear(ByVal p_strPROJECTNUMBER As String, _
                          ByVal p_sngTIRENUMBER As Single, _
                          ByVal p_strTESTSPEC As String, _
                          ByVal p_dteCOMPLETIONDATE As DateTime, _
                          ByVal p_strDOTSERIALNUMBER As String, _
                          ByVal p_sngLOWESTWEARBAR As Single, _
                          ByVal p_strPassyn As String, _
                          ByVal p_intSKUID As Integer, _
                          ByVal p_intCertType As Integer, _
                          ByVal p_strCERTIFICATENUMBER As String, _
                          ByRef p_intTREADWEARID As Integer, _
                          ByVal p_dteSERIALDATE As DateTime, _
                          ByVal p_strOperatorName As String, _
                          ByVal p_sngINDICATORSREQUIREMENT As Single, _
                          ByVal p_intCertificateID As Integer, _
                          ByVal p_strMatlNum As String, _
                          ByVal p_strOperation As String, _
                          ByVal p_strGTSpecTreadWear As String, _
                          ByVal p_strMFGWWYY As String) As NameAid.SaveResult
    ''' <summary>
    ''' Method to Save Tread Wear Detail.
    ''' </summary>
    ''' <param name="p_intTREADWEARID">Tread Wear ID</param>
    ''' <param name="p_sngIteration">sng Iteration</param>
    ''' <param name="p_sngwearbarheight">sng WearBar Height</param>
    ''' <param name="p_strOperatorName">Operator Name</param>
    ''' <returns>Save Result Object.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function SaveTreadWearDetail(ByVal p_sngwearbarheight As Single, _
                                ByVal p_intTREADWEARID As Integer, _
                                ByVal p_sngIteration As Single, _
                                ByVal p_strOperatorName As String) As NameAid.SaveResult

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Method to Save Bead Unseat.
    ''' </summary>
    ''' <param name="p_intCertificateID">Certification Id</param>
    ''' <param name="p_dteCOMPLETIONDATE">Completion Date</param>
    ''' <param name="p_dteSerialDate">Serial Date</param>
    ''' <param name="p_intBeadUnseatID">Bead Unseat Id</param>
    ''' <param name="p_intCertType">Certificate Type </param>
    ''' <param name="p_sngLOWESTUNSEATVALUE">Lowest Unseat Value</param>
    ''' <param name="p_sngMINBEADUNSEAT">Min Bead Unseat</param>
    ''' <param name="p_sngTIRENUMBER">Tire NUmber</param>
    ''' <param name="p_strCERTIFICATENUMBER">Certificate Number</param>
    ''' <param name="p_strDOTSERIALNUMBER">DOT Serial Number</param>
    ''' <param name="p_strGTSpecBeadUnseat">GT Spec Bead Unseat</param>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <param name="p_strMFGWWYY">MFGWWYY</param>
    ''' <param name="p_strOperation">Operation</param>
    ''' <param name="p_strOperatorName">Operator Name</param>
    ''' <param name="p_strPassyn">Pass YN</param>
    ''' <param name="p_strPROJECTNUMBER">Project Number</param>
    ''' <param name="p_strTESTPASSFAIL">Test Pass Fail</param>
    ''' <param name="p_strTESTSPEC">Test Spec</param>
    ''' <returns>Save Result Object.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function SaveBeadUnseat(ByVal p_strPROJECTNUMBER As String, _
                           ByVal p_sngTIRENUMBER As Single, _
                           ByVal p_strTESTSPEC As String, _
                           ByVal p_dteCOMPLETIONDATE As DateTime, _
                           ByVal p_strDOTSERIALNUMBER As String, _
                           ByVal p_sngLOWESTUNSEATVALUE As Single, _
                           ByVal p_strPassyn As String, _
                           ByVal p_intCertType As Integer, _
                           ByVal p_strCERTIFICATENUMBER As String, _
                           ByRef p_intBeadUnseatID As Integer, _
                           ByVal p_dteSerialDate As DateTime, _
                           ByVal p_sngMINBEADUNSEAT As Single, _
                           ByVal p_strTESTPASSFAIL As String, _
                           ByVal p_strOperatorName As String, _
                           ByVal p_intCertificateID As Integer, _
                           ByVal p_strMatlNum As String, _
                           ByVal p_strOperation As String, _
                           ByVal p_strGTSpecBeadUnseat As String, _
                           ByVal p_strMFGWWYY As String) As NameAid.SaveResult
    ''' <summary>
    ''' Method to Save Bead Unseat Detail.
    ''' </summary>
    ''' <param name="p_intBEADUNSEATID">Bead UnSeat Id</param>
    ''' <param name="p_sngIteration">Iteration</param>
    ''' <param name="p_sngUNSEATFORCE">UnSeat Force</param>
    ''' <param name="p_strOperatorName">Operator Name</param>
    ''' <returns>Save result Object.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function SaveBeadUnseatDetail(ByVal p_intBEADUNSEATID As Integer, _
                                 ByVal p_sngUNSEATFORCE As Single, _
                                 ByVal p_sngIteration As Single, _
                                 ByVal p_strOperatorName As String) As NameAid.SaveResult

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Method to Save High Speed.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_dtePOSTCONDENDDATE">Post Condition end Date</param>
    ''' <param name="p_dtePOSTCONDSTARTDATE">Post Condition Start Date</param>
    ''' <param name="p_dtePRECONDENDDATE">Pre Condition end date</param>
    ''' <param name="p_dtePRECONDSTARTDATE">Pre Condition start date</param>
    ''' <param name="p_dteSERIALDATE">Serial Date</param>
    ''' <param name="p_intAFTERINFLATION">After Inflation</param>
    ''' <param name="p_intBEFOREINFLATION">Before inflation</param>
    ''' <param name="p_intCertificateNumberID">Certificate Number Id</param>
    ''' <param name="p_intFINALINFLATION">Final Inflation</param>
    ''' <param name="p_intFINALTEMP">Final Temp</param>
    ''' <param name="p_intINFLATIONPRESSURE">Inflation Pressure</param>
    ''' <param name="p_intPOSTCONDENDTEMP">Post COndition End Temp</param>
    ''' <param name="p_intPRECONDENDTEMP">Pre Condition End Temp</param>
    ''' <param name="p_intWHEELNUMBER">Wheel NUmber</param>
    ''' <param name="p_intWHEELPOSITION">Wheel Position</param>
    ''' <param name="p_sngAFTERDIAMETER">After Diameter</param>
    ''' <param name="p_sngBEFOREDIAMETER">Before Diameter</param>
    ''' <param name="p_sngCIRCUNFERENCEAFTERTEST">Circumference after test</param>
    ''' <param name="p_sngCIRCUNFERENCEBEFORETEST">Circumference before test</param>
    ''' <param name="p_sngDIAMETERTESTDRUM">Diameter testdrum</param>
    ''' <param name="p_sngFINALDISTANCE">Final Distance</param>
    ''' <param name="p_sngINFLATIONPRESSUREREADJUSTED">Inflation pressure r adjusted</param>
    ''' <param name="p_sngOUTERDIAMETERDIFERENCE">Outer Diameter Difference</param>
    ''' <param name="p_sngPRECONDSTARTTEMP">Pre Condition Start temp</param>
    ''' <param name="p_strRESULTPASSFAIL">Result Pass Fail</param>
    ''' <param name="p_sngODDIFERENCETOLERANCE">ODDifference Tolerance</param>
    ''' <param name="p_intTireNumber">Tire Number</param>
    ''' <param name="p_sngPOSTCONDTIME">Post Condition Time</param>
    ''' <param name="p_sngPRECONDTEMP">Pre Condition Temp</param>
    ''' <param name="p_sngPRECONDTIME">Pre Condition Type</param>
    ''' <param name="p_sngRIMDIAMETER">RIM Diameter</param>
    ''' <param name="p_sngRIMWIDTH">Rim Width</param>
    ''' <param name="p_dteCOMPLETIONDATE">Completion Date</param>
    ''' <param name="p_intENDURANCEID">Endurance Id</param>
    ''' <param name="p_intLowInfEndTemp">Low Inf End Temp</param>
    ''' <param name="p_sngLowInfStartInflation">Low Inf Start Inflation</param>
    ''' <param name="p_sngLowInfEndInflation">Low Inf End Inflation</param>
    ''' <param name="p_sngENDURANCEHOURS">Endurance Hours</param>
    ''' <param name="p_strAPPROVER">Approver</param>
    ''' <param name="p_strCERTIFICATENUMBER">Certificate Number</param>
    ''' <param name="p_strDOTSERIALNUMBER">DOT Serial Number</param>
    ''' <param name="p_strFINALJUDGEMENT">Final Judgment</param>
    ''' <param name="p_strPOSSIBLEFAILURESFOUND">Possible failures found</param>
    ''' <param name="p_strMatlNum"> Material Number</param>
    ''' <param name="p_strMFGWWYY">MFGWYY</param>
    ''' <param name="p_strOperation">Operation</param>
    ''' <param name="p_strOperatorName">Operator Name</param>
    ''' <param name="p_strPASSYN">Pass YN</param>
    ''' <param name="p_strPROJECTNUMBER">Project Number</param>
    ''' <param name="p_strSERIENOM">SerieNom</param>
    ''' <param name="p_strGTSpecEndurance">GT Spec Endurance</param>
    ''' <param name="p_strTESTSPEC">Test Spec</param>
    ''' <returns>NameAid.SaveResult Object.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function SaveEndurance(ByRef p_intENDURANCEID As Integer, _
                          ByVal p_strProjectNumber As String, _
                          ByVal p_intTireNumber As Integer, _
                          ByVal p_strTESTSPEC As String, _
                          ByVal p_dteCOMPLETIONDATE As DateTime, _
                          ByVal p_strDOTSERIALNUMBER As String, _
                          ByVal p_dtePRECONDSTARTDATE As DateTime, _
                          ByVal p_sngPRECONDSTARTTEMP As Single, _
                          ByVal p_sngRIMDIAMETER As Single, _
                          ByVal p_sngRIMWIDTH As Single, _
                          ByVal p_dtePRECONDENDDATE As DateTime, _
                          ByVal p_intPRECONDENDTEMP As Integer, _
                          ByVal p_intINFLATIONPRESSURE As Integer, _
                          ByVal p_sngBEFOREDIAMETER As Single, _
                          ByVal p_sngAFTERDIAMETER As Single, _
                          ByVal p_intBEFOREINFLATION As Integer, _
                          ByVal p_intAFTERINFLATION As Integer, _
                          ByVal p_intWHEELPOSITION As Integer, _
                          ByVal p_intWHEELNUMBER As Integer, _
                          ByVal p_intFINALTEMP As Integer, _
                          ByVal p_sngFINALDISTANCE As Single, _
                          ByVal p_intFINALINFLATION As Integer, _
                          ByVal p_dtePOSTCONDSTARTDATE As DateTime, _
                          ByVal p_dtePOSTCONDENDDATE As DateTime, _
                          ByVal p_intPOSTCONDENDTEMP As Integer, _
                          ByVal p_strPASSYN As String, _
                          ByVal p_intCertificationTypeID As Integer, _
                          ByVal p_strCERTIFICATENUMBER As String, _
                          ByVal p_dteSerialDate As DateTime, _
                          ByVal p_sngPostCondTime As Single, _
                          ByVal p_sngPreCondTime As Single, _
                          ByVal p_sngDIAMETERTESTDRUM As Single, _
                          ByVal p_sngPRECONDTEMP As Single, _
                          ByVal p_sngINFLATIONPRESSUREREADJUSTED As Single, _
                          ByVal p_sngCIRCUNFERENCEBEFORETEST As Single, _
                          ByVal p_strRESULTPASSFAIL As String, _
                          ByVal p_sngENDURANCEHOURS As Single, _
                          ByVal p_strPOSSIBLEFAILURESFOUND As String, _
                          ByVal p_sngCIRCUNFERENCEAFTERTEST As Single, _
                          ByVal p_sngOUTERDIAMETERDIFERENCE As Single, _
                          ByVal p_sngODDIFERENCETOLERANCE As Single, _
                          ByVal p_strSERIENOM As String, _
                          ByVal p_strFINALJUDGEMENT As String, _
                          ByVal p_strAPPROVER As String, _
                          ByVal p_strOperatorName As String, _
                          ByVal p_intCertificateNumberID As Integer, _
                          ByVal p_strMatlNum As String, _
                          ByVal p_sngLowInfStartInflation As Single, _
                          ByVal p_sngLowInfEndInflation As Single, _
                          ByVal p_intLowInfEndTemp As Integer, _
                          ByVal p_strOperation As String, _
                          ByVal p_strGTSpecEndurance As String, _
                          ByVal p_strMFGWWYY As String) As NameAid.SaveResult
    ''' <summary>
    ''' Method to Save Endurance Detail.
    ''' </summary>
    ''' <param name="p_intAMBTEMP">AMB Temp</param>
    ''' <param name="p_dteSTEPCOMPLETIONDATE">Step Completion Date</param>
    ''' <param name="p_intENDURANCEID">Endurance Id</param>
    ''' <param name="p_intINFPRESSURE">Inf Pressure</param>
    ''' <param name="p_intSETINFLATION">Set Inflation</param>
    ''' <param name="p_intSpeed">Speed</param>
    ''' <param name="p_intTESTSTEP">Test Step</param>
    ''' <param name="p_intTIMEINMIN">Time in Min</param>
    ''' <param name="p_sngLOADPERCENT">Load Percent</param>
    ''' <param name="p_sngtLOAD">Load</param>
    ''' <param name="p_sngTOTMILES">Total Miles</param>
    ''' <returns>NameAid.SaveResult Object.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function SaveEnduranceDetail(ByVal p_intTESTSTEP As Integer, _
                                ByVal p_intTIMEINMIN As Integer, _
                                ByVal p_intSpeed As Integer, _
                                ByVal p_sngTOTMILES As Single, _
                                ByVal p_sngtLOAD As Single, _
                                ByVal p_sngLOADPERCENT As Single, _
                                ByVal p_intSETINFLATION As Integer, _
                                ByVal p_intAMBTEMP As Integer, _
                                ByVal p_intINFPRESSURE As Integer, _
                                ByVal p_dteSTEPCOMPLETIONDATE As DateTime, _
                                ByVal p_intENDURANCEID As Integer) As NameAid.SaveResult

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Method to Save High Speed.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_dteCOMPETIONDATE">Competition Date</param>
    ''' <param name="p_dtePOSTCONDENDDATE">Post Condition end Date</param>
    ''' <param name="p_dtePOSTCONDSTARTDATE">Post Condition Start Date</param>
    ''' <param name="p_dtePRECONDENDDATE">Pre Condition end date</param>
    ''' <param name="p_dtePRECONDSTARTDATE">Pre Condition start date</param>
    ''' <param name="p_dteSERIALDATE">Serial Date</param>
    ''' <param name="p_intAFTERINFLATION">After Inflation</param>
    ''' <param name="p_intBEFOREINFLATION">Before inflation</param>
    ''' <param name="p_intCertificateNumberID">Certificate Number Id</param>
    ''' <param name="p_intFINALINFLATION">Final Inflation</param>
    ''' <param name="p_intFINALTEMP">Final Temp</param>
    ''' <param name="p_intHighSpeedID">High Speed Id</param>
    ''' <param name="p_intINFLATIONPRESSURE">Inflation Pressure</param>
    ''' <param name="p_intPOSTCONDENDTEMP">Post COndition End Temp</param>
    ''' <param name="p_intPRECONDENDTEMP">Pre Condition End Temp</param>
    ''' <param name="p_intPRECONDSARTTEMP">Pre Condsart Temp</param>
    ''' <param name="p_intTIRENUM">Tire NUmber</param>
    ''' <param name="p_intWHEELNUMBER">Wheel NUmber</param>
    ''' <param name="p_intWHEELPOSITION">Wheel Position</param>
    ''' <param name="p_sngAFTERDIAMETER">After Diameter</param>
    ''' <param name="p_sngBEFOREDIAMETER">Before Diameter</param>
    ''' <param name="p_sngCIRCUNFERENCEAFTERTEST">Circumference after test</param>
    ''' <param name="p_sngCIRCUNFERENCEBEFORETEST">Circumference before test</param>
    ''' <param name="p_sngDIAMETERTESTDRUM">Diameter testdrum</param>
    ''' <param name="p_sngFINALDISTANCE">Final Distance</param>
    ''' <param name="p_sngINFLATIONPRESSUREREADJUSTED">Inflation pressure readjusted</param>
    ''' <param name="p_sngMAXLOAD">Max Load</param>
    ''' <param name="p_sngMAXSPEED">Max Speed</param>
    ''' <param name="p_sngODDIFERENCE">ODDifference</param>
    ''' <param name="p_sngODDIFERENCETOLERANCE">ODDifference Tolerance</param>
    ''' <param name="p_sngPASSATKMH">Pass at KMH</param>
    ''' <param name="p_sngPOSTCONDTIME">Post Condition Time</param>
    ''' <param name="p_sngPRECONDTEMP">Pre Condition Temp</param>
    ''' <param name="p_sngPRECONDTIME">Pre Condition Type</param>
    ''' <param name="p_sngRIMDIAMETER">RIM Diameter</param>
    ''' <param name="p_sngRIMWIDTH">Rim Width</param>
    ''' <param name="p_sngSPEEDTOTALTIME">SPeed Total Time</param>
    ''' <param name="p_sngWHEELSPEEDKMH">Wheel Speed KMH</param>
    ''' <param name="p_sngWHEELSPEEDRPM">Wheel SPeed RPM</param>
    ''' <param name="p_strAPPROVER">Approver</param>
    ''' <param name="p_strCERTIFICATENUMBER">Certificate Number</param>
    ''' <param name="p_strDOTSERIALNUMBER">DOT Serial Number</param>
    ''' <param name="p_strFINALJUDGEMENT">Final Judgment</param>
    ''' <param name="p_strGTSpecHighSpeed">GT Spec High Speed</param>
    ''' <param name="p_strMatlNum"> Material Number</param>
    ''' <param name="p_strMFGWWYY">MFGWYY</param>
    ''' <param name="p_strOperation">Operation</param>
    ''' <param name="p_strOperatorName">Operator Name</param>
    ''' <param name="p_strPASSYN">Pass YN</param>
    ''' <param name="p_strPROJECTNUMBER">Project Number</param>
    ''' <param name="p_strSERIENOM">SerieNom</param>
    ''' <param name="p_strSPEEDTTESTPASSFAIL">Speed test Pass Fail</param>
    ''' <param name="p_strTESTSPEC">Test Spec</param>
    ''' <returns>NameAid.SaveResult Object.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function SaveHighSpeed(ByRef p_intHighSpeedID As Integer, _
                          ByVal p_strPROJECTNUMBER As String, _
                          ByVal p_intTIRENUM As Integer, _
                          ByVal p_strTESTSPEC As String, _
                          ByVal p_dteCOMPETIONDATE As DateTime, _
                          ByVal p_strDOTSERIALNUMBER As String, _
                          ByVal p_strMFGWWYY As String, _
                          ByVal p_dtePRECONDSTARTDATE As DateTime, _
                          ByVal p_intPRECONDSARTTEMP As Integer, _
                          ByVal p_sngRIMDIAMETER As Single, _
                          ByVal p_sngRIMWIDTH As Single, _
                          ByVal p_dtePRECONDENDDATE As DateTime, _
                          ByVal p_intPRECONDENDTEMP As Integer, _
                          ByVal p_intINFLATIONPRESSURE As Integer, _
                          ByVal p_sngBEFOREDIAMETER As Single, _
                          ByVal p_sngAFTERDIAMETER As Single, _
                          ByVal p_intBEFOREINFLATION As Integer, _
                          ByVal p_intAFTERINFLATION As Integer, _
                          ByVal p_intWHEELPOSITION As Integer, _
                          ByVal p_intWHEELNUMBER As Integer, _
                          ByVal p_intFINALTEMP As Integer, _
                          ByVal p_sngFINALDISTANCE As Single, _
                          ByVal p_intFINALINFLATION As Integer, _
                          ByVal p_dtePOSTCONDSTARTDATE As DateTime, _
                          ByVal p_dtePOSTCONDENDDATE As DateTime, _
                          ByVal p_intPOSTCONDENDTEMP As Integer, _
                          ByVal p_sngPRECONDTIME As Single, _
                          ByVal p_sngPOSTCONDTIME As Single, _
                          ByVal p_strPASSYN As String, _
                          ByVal p_dteSERIALDATE As DateTime, _
                          ByVal p_intCERTIFICATIONTYPEID As Integer, _
                          ByVal p_strCERTIFICATENUMBER As String, _
                          ByVal p_sngDIAMETERTESTDRUM As Single, _
                          ByVal p_sngPRECONDTEMP As Single, _
                          ByVal p_sngINFLATIONPRESSUREREADJUSTED As Single, _
                          ByVal p_sngCIRCUNFERENCEBEFORETEST As Single, _
                          ByVal p_sngWHEELSPEEDRPM As Single, _
                          ByVal p_sngWHEELSPEEDKMH As Single, _
                          ByVal p_sngCIRCUNFERENCEAFTERTEST As Single, _
                          ByVal p_sngODDIFERENCE As Single, _
                          ByVal p_sngODDIFERENCETOLERANCE As Single, _
                          ByVal p_strSERIENOM As String, _
                          ByVal p_strFINALJUDGEMENT As String, _
                          ByVal p_strAPPROVER As String, _
                          ByVal p_sngPASSATKMH As Single, _
                          ByVal p_strSPEEDTTESTPASSFAIL As String, _
                          ByVal p_sngSPEEDTOTALTIME As Single, _
                          ByVal p_sngMAXSPEED As Single, _
                          ByVal p_sngMAXLOAD As Single, _
                          ByVal p_strOperatorName As String, _
                          ByVal p_intCertificateNumberID As Integer, _
                          ByVal p_strMatlNum As String, _
                          ByVal p_strOperation As String, _
                          ByVal p_strGTSpecHighSpeed As String) As NameAid.SaveResult
    ''' <summary>
    ''' Method to Save High Speed Detail.
    ''' </summary>
    ''' <param name="p_intAmbTemp">AmbTemp</param>
    ''' <param name="p_dteStepCompletionDate">Step Completion Date</param>
    ''' <param name="p_intHighSpeedID">High Speed ID</param>
    ''' <param name="p_intInfPressure">Inf Pressure</param>
    ''' <param name="p_intLoadPercent">Load Percent</param>
    ''' <param name="p_intSetInflation">Set Inflation</param>
    ''' <param name="p_intTESTSTEP">Test Step</param>
    ''' <param name="p_intTimeMin">Time min</param>
    ''' <param name="p_sngLoad">Load</param>
    ''' <param name="p_sngSpeed">Speed</param>
    ''' <param name="p_sngTotMiles">TotMiles</param>
    ''' <param name="p_strOperatorId">Operation Id</param>
    ''' <returns>NameAid.SaveResult Object.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function SaveHighSpeedDetail(ByVal p_intHighSpeedID As Integer, _
                                ByVal p_strOperatorId As String, _
                                ByVal p_intTESTSTEP As Integer, _
                                ByVal p_intTimeMin As Integer, _
                                ByVal p_sngSpeed As Single, _
                                ByVal p_sngTotMiles As Single, _
                                ByVal p_sngLoad As Single, _
                                ByVal p_intLoadPercent As Single, _
                                ByVal p_intSetInflation As Integer, _
                                ByVal p_intAmbTemp As Integer, _
                                ByVal p_intInfPressure As Integer, _
                                ByVal p_dteStepCompletionDate As DateTime) As NameAid.SaveResult
    ''' <summary>
    ''' Method to Save HighSpeed Speed Test Detail.
    ''' </summary>
    ''' <param name="p_intHighSpeedID">High Speed ID</param>
    ''' <param name="p_dteTime">Time</param>
    ''' <param name="p_intIteration">Iteration</param>
    ''' <param name="p_sngSpeed">Speed</param>
    ''' <param name="p_strUserName">User Name</param>
    ''' <returns>NameAid.SaveResult Object.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function SaveHighSpeed_SpeedTestDetail(ByVal p_intHighSpeedID As Integer, _
                                          ByVal p_intIteration As Integer, _
                                          ByVal p_dteTime As DateTime, _
                                          ByVal p_sngSpeed As Single, _
                                          ByVal p_strUserName As String) As NameAid.SaveResult

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Method to Save Sound.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_dteDATEOFTEST">Date of Test</param>
    ''' <param name="p_dteDATETRACKCERTIFTOISO">Date track certification</param>
    ''' <param name="p_intCertificateNUmberID">Certificate NUmber Id</param>
    ''' <param name="p_intSKUID">SKU ID</param>
    ''' <param name="p_intSoundID">Sound Id</param>
    ''' <param name="p_intTIRENUM">Tire NUmber</param>
    ''' <param name="p_strCATEGORYOFUSE">category of Use</param>
    ''' <param name="p_strCERTIFICATENUMBER">Certificate Number</param>
    ''' <param name="p_strGTSpecSound">GT Spec Sound</param>
    ''' <param name="p_strINFLATIONPRESSURECO_FRONTL">Inflation Pressure co_FrontL</param>
    ''' <param name="p_strINFLATIONPRESSURECO_FRONTR">Inflation pressure co_frontr</param>
    ''' <param name="p_strINFLATIONPRESSURECO_REARL">Inflation pressure co_rearL</param>
    ''' <param name="p_strINFLATIONPRESSURECO_REARR">Inflation pressure co_rearR</param>
    ''' <param name="p_strLOCATIONOFTESTTRACK">Location of test track</param>
    ''' <param name="p_strMANUFACTUREANDBRAND">Manufacture and Brand</param>
    ''' <param name="p_strMFGWWYY">MFGWWYY</param>
    ''' <param name="p_strOperation">Operation</param>
    ''' <param name="p_strPROJECTNUMBER">Project Number</param>
    ''' <param name="p_strREFERENCEINFLATIONPRESSURE">Reference inflation pressure</param>
    ''' <param name="p_strTEMPMEASURESENSORTYPE">Temp Measure Sensor Type</param>
    ''' <param name="p_strTESTMASS_FRONTL">Test mass frontl</param>
    ''' <param name="p_strTESTMASS_FRONTR">Test mass frontr</param>
    ''' <param name="p_strTESTMASS_REARL">Test mass rearl</param>
    ''' <param name="p_strTESTMASS_REARR">Test mass rearr</param>
    ''' <param name="p_strTESTREPORTNUMBER">Test report number</param>
    ''' <param name="p_strTESTRIMWIDTHCODE">test Rim Width Code</param>
    ''' <param name="p_strTESTSPEC">test Spec</param>
    ''' <param name="p_strTESTVEHICULE">Test Vehicle</param>
    ''' <param name="p_strTESTVEHICULEWHEELBASE">Test Vehicle Wheel Base</param>
    ''' <param name="p_strTIRECLASS">Tire Class</param>
    ''' <param name="p_strTIRELOADINDEX_FRONTL">Tire load Index FrontL</param>
    ''' <param name="p_strTIRELOADINDEX_FRONTR">Tire load Index FrontR</param>
    ''' <param name="p_strTIRELOADINDEX_REARL">Tire load Index RearL</param>
    ''' <param name="p_strTIRELOADINDEX_REARR">Tire load Index RearR</param>
    ''' <param name="p_strTIRESERVICEDESCRIPTION">Tire Service Description</param>
    ''' <param name="p_strTIRESIZEDESIGNATION">Tire Size Designation</param>
    ''' <param name="p_strUserId">User ID</param>
    ''' <returns>NameAid.SaveResult Object.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function SaveSound(ByVal p_strUserId As String, _
                      ByRef p_intSoundID As Integer, _
                      ByVal p_strPROJECTNUMBER As String, _
                      ByVal p_intTIRENUM As Integer, _
                      ByVal p_strTESTSPEC As String, _
                      ByVal p_strTESTREPORTNUMBER As String, _
                      ByVal p_strMANUFACTUREANDBRAND As String, _
                      ByVal p_strTIRECLASS As String, _
                      ByVal p_strCATEGORYOFUSE As String, _
                      ByVal p_dteDATEOFTEST As DateTime, _
                      ByVal p_strTESTVEHICULE As String, _
                      ByVal p_strTESTVEHICULEWHEELBASE As String, _
                      ByVal p_strLOCATIONOFTESTTRACK As String, _
                      ByVal p_dteDATETRACKCERTIFTOISO As DateTime, _
                      ByVal p_strTIRESIZEDESIGNATION As String, _
                      ByVal p_strTIRESERVICEDESCRIPTION As String, _
                      ByVal p_strREFERENCEINFLATIONPRESSURE As String, _
                      ByVal p_strTESTMASS_FRONTL As String, _
                      ByVal p_strTESTMASS_FRONTR As String, _
                      ByVal p_strTESTMASS_REARL As String, _
                      ByVal p_strTESTMASS_REARR As String, _
                      ByVal p_strTIRELOADINDEX_FRONTL As String, _
                      ByVal p_strTIRELOADINDEX_FRONTR As String, _
                      ByVal p_strTIRELOADINDEX_REARL As String, _
                      ByVal p_strTIRELOADINDEX_REARR As String, _
                      ByVal p_strINFLATIONPRESSURECO_FRONTL As String, _
                      ByVal p_strINFLATIONPRESSURECO_FRONTR As String, _
                      ByVal p_strINFLATIONPRESSURECO_REARL As String, _
                      ByVal p_strINFLATIONPRESSURECO_REARR As String, _
                      ByVal p_strTESTRIMWIDTHCODE As String, _
                      ByVal p_strTEMPMEASURESENSORTYPE As String, _
                      ByVal p_intCERTIFICATIONTYPEID As Integer, _
                      ByVal p_strCERTIFICATENUMBER As String, _
                      ByVal p_intSKUID As Integer, _
                      ByVal p_intCertificateNUmberID As Integer, _
                      ByVal p_strOperation As String, _
                      ByVal p_strGTSpecSound As String, _
                      ByVal p_strMFGWWYY As String) As NameAid.SaveResult
    ''' <summary>
    ''' Method to Save Sound Detail.
    ''' </summary>
    ''' <param name="p_intITERATION">Iteration</param>
    ''' <param name="p_intSoundID">Sound Id</param>
    ''' <param name="p_strAIRTEMP">Air Temp</param>
    ''' <param name="p_strDIRECTIONOFRUN">Direction of Run</param>
    ''' <param name="p_strSOUNDLEVELLEFT">Sound Level Left</param>
    ''' <param name="p_strSOUNDLEVELLEFT_TEMPCOR">Sound level left TempCor</param>
    ''' <param name="p_strSOUNDLEVELRIGHT">Sound level Right</param>
    ''' <param name="p_strSOUNDLEVELRIGHT_TEMPCOR">Sound level right TempCor</param>
    ''' <param name="p_strTESTSPEED">Test Speed</param>
    ''' <param name="p_strTRACKTEMP">Track Temp</param>
    ''' <param name="p_strUserId">User ID</param>
    ''' <returns>NameAid.SaveResult object.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function SaveSoundDetail(ByVal p_strUserId As String, _
                            ByVal p_intITERATION As Integer, _
                            ByVal p_strTESTSPEED As String, _
                            ByVal p_strDIRECTIONOFRUN As String, _
                            ByVal p_strSOUNDLEVELLEFT As String, _
                            ByVal p_strSOUNDLEVELRIGHT As String, _
                            ByVal p_strAIRTEMP As String, _
                            ByVal p_strTRACKTEMP As String, _
                            ByVal p_strSOUNDLEVELLEFT_TEMPCOR As String, _
                            ByVal p_strSOUNDLEVELRIGHT_TEMPCOR As String, _
                            ByVal p_intSoundID As Integer) As NameAid.SaveResult

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Method to Save Wet Grip.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_strUserId">User id</param>
    ''' <param name="p_intWetGripID">Wet Grip Id</param>
    ''' <param name="p_strPROJECTNUMBER">project Number</param>
    ''' <param name="p_intTIRENUM">Tire NUmber</param>
    ''' <param name="p_strTESTSPEC">Test Spec</param>
    ''' <param name="p_dteDATEOFTEST">Date Of Test</param>
    ''' <param name="p_strTESTVEHICLE">Test Vehicle</param>
    ''' <param name="p_strLOCATIONOFTESTTRACK">Location Of Test Track</param>
    ''' <param name="p_strTESTTRACKCHARACTERISTICS">Test Track Characteristics</param>
    ''' <param name="p_strISSUEBY">Issue By</param>
    ''' <param name="p_strMETHODOFCERTIFICATION">Method of Certification</param>
    ''' <param name="p_strTESTTIREDETAILS">Test Tire Details</param>
    ''' <param name="p_strTIRESIZEANDSERVICEDESC">Tire Size and Service Description</param>
    ''' <param name="p_strTIREBRANDANDTRADEDESC">Tire brand and trade desc</param>
    ''' <param name="p_strREFERENCEINFLATIONPRESSURE">Reference inflation pressure</param>
    ''' <param name="p_strTESTRIMWITHCODE">Test rim with code</param>
    ''' <param name="p_strTEMPMEASURESENSORTYPE">Temp Measure Sensor Type</param>
    ''' <param name="p_strIDENTIFICATIONSRTT">Identification srtt</param>
    ''' <param name="p_strTESTTIRELOAD_SRTT">Test tire load srtt</param>
    ''' <param name="p_strTESTTIRELOAD_CANDIDATE">Test tire load candidate</param>
    ''' <param name="p_strTESTTIRELOAD_CONTROL">Test tire load control</param>
    ''' <param name="p_strWATERDEPTH_SRTT">Water depth srtt</param>
    ''' <param name="p_strWATERDEPTH_CANDIDATE">Water depth candidate</param>
    ''' <param name="p_strWATERDEPTH_CONTROL">Water depth control</param>
    ''' <param name="p_strWETTEDTRACKTEMPAVG">Wetted track temp average</param>
    ''' <param name="p_intCERTIFICATIONTYPEID">Certification type id</param>
    ''' <param name="p_strCERTIFICATENUMBER">Certificate Number</param>
    ''' <param name="p_intSKUID">SKU ID</param>
    ''' <param name="p_intCertificateNUmberID">Certificate NUmber Id</param>
    ''' <param name="p_strOperation">Operation</param>
    ''' <param name="p_strGTSpecWetGrip"></param>
    ''' <param name="p_strMFGWWYY">MFGWWYY</param>
    ''' <returns>Dataset.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function SaveWetGrip(ByVal p_strUserId As String, _
                        ByRef p_intWetGripID As Integer, _
                        ByVal p_strPROJECTNUMBER As String, _
                        ByVal p_intTIRENUM As Integer, _
                        ByVal p_strTESTSPEC As String, _
                        ByVal p_dteDATEOFTEST As DateTime, _
                        ByVal p_strTESTVEHICLE As String, _
                        ByVal p_strLOCATIONOFTESTTRACK As String, _
                        ByVal p_strTESTTRACKCHARACTERISTICS As String, _
                        ByVal p_strISSUEBY As String, _
                        ByVal p_strMETHODOFCERTIFICATION As String, _
                        ByVal p_strTESTTIREDETAILS As String, _
                        ByVal p_strTIRESIZEANDSERVICEDESC As String, _
                        ByVal p_strTIREBRANDANDTRADEDESC As String, _
                        ByVal p_strREFERENCEINFLATIONPRESSURE As String, _
                        ByVal p_strTESTRIMWITHCODE As String, _
                        ByVal p_strTEMPMEASURESENSORTYPE As String, _
                        ByVal p_strIDENTIFICATIONSRTT As String, _
                        ByVal p_strTESTTIRELOAD_SRTT As String, _
                        ByVal p_strTESTTIRELOAD_CANDIDATE As String, _
                        ByVal p_strTESTTIRELOAD_CONTROL As String, _
                        ByVal p_strWATERDEPTH_SRTT As String, _
                        ByVal p_strWATERDEPTH_CANDIDATE As String, _
                        ByVal p_strWATERDEPTH_CONTROL As String, _
                        ByVal p_strWETTEDTRACKTEMPAVG As String, _
                        ByVal p_intCERTIFICATIONTYPEID As Integer, _
                        ByVal p_strCERTIFICATENUMBER As String, _
                        ByVal p_intSKUID As Integer, _
                        ByVal p_intCertificateNUmberID As Integer, _
                        ByVal p_strOperation As String, _
                        ByVal p_strGTSpecWetGrip As String, _
                        ByVal p_strMFGWWYY As String) As NameAid.SaveResult
    ''' <summary>
    ''' Method to Save Wet Grip Detail.
    ''' </summary>
    ''' <param name=" p_strUserId">User Id</param>
    ''' <param name=" p_intITERATION">Iteration</param>
    ''' <param name=" p_strTESTSPEED">Test Speed</param>
    ''' <param name=" p_strDIRECTIONOFRUN">Direction of Run</param>
    ''' <param name=" p_strSRTT">SRTT</param>
    ''' <param name=" p_strCANDIDATETIRE">Candidate Tire</param>
    ''' <param name=" p_strPEAKBREAKFORCECOEFICIENT">Peak Break force coefficient</param>
    ''' <param name=" p_strMEANFULLYDEVDECELERATION">mean fully dev deceleration</param>
    ''' <param name=" p_strWETGRIPINDEX">Wet Grip Index</param>
    ''' <param name=" p_strCOMMENTS">Comments</param>
    ''' <param name=" p_intWetGripID">Wet Grip Id</param>
    ''' <returns>NameAid.SaveResult Object.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function SaveWetGripDetail(ByVal p_strUserId As String, _
                              ByVal p_intITERATION As Integer, _
                              ByVal p_strTESTSPEED As String, _
                              ByVal p_strDIRECTIONOFRUN As String, _
                              ByVal p_strSRTT As String, _
                              ByVal p_strCANDIDATETIRE As String, _
                              ByVal p_strPEAKBREAKFORCECOEFICIENT As String, _
                              ByVal p_strMEANFULLYDEVDECELERATION As String, _
                              ByVal p_strWETGRIPINDEX As String, _
                              ByVal p_strCOMMENTS As String, _
                              ByVal p_intWetGripID As Integer) As NameAid.SaveResult
    ''' <summary>
    ''' Method to Add Customer.
    ''' </summary>
    ''' <param name="p_intSKUId">SKU ID</param>
    ''' <param name="p_strCountryLocation">Country Location</param>
    ''' <param name="p_strCustomer">Customer</param>
    ''' <param name="p_strImporter">Importer</param>
    ''' <param name="p_strImporterAddress">Importer Address</param>
    ''' <param name="p_strImporterRepresentative">Importer Representative</param>
    ''' <returns>NameAid.SaveResult Object.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function AddCustomer(ByVal p_intSKUId As Integer, _
                            ByVal p_strCustomer As String, _
                            ByVal p_strImporter As String, _
                            ByVal p_strImporterRepresentative As String, _
                            ByVal p_strImporterAddress As String, _
                            ByVal p_strCountryLocation As String) As NameAid.SaveResult

    ''' <summary>
    ''' Method to Get Certificate ID.
    ''' </summary>
    ''' <param name="p_intCertificateTypeId">Certification Type Id</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <param name="p_strExtensionNo">Extension Number</param>
    ''' <returns>Integer.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Function GetCertificateID(ByVal p_strCertificateNumber As String, ByVal p_intCertificateTypeId As Integer, ByVal p_strExtensionNo As String) As Integer

    ''' <summary>
    ''' Method to Get Certification Type ID.
    ''' </summary>
    ''' <param name="p_strCertificationTypeName">Certification Type Name</param>
    ''' <returns>Integer.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetCertificationTypeID(ByVal p_strCertificationTypeName As String) As Integer

    'added - for generic certificate types - jeseitz 6/9/2016
    ''' <summary>
    ''' Method to Get Certificate Template.
    ''' </summary>
    ''' <param name="p_strCertificationTypeName">Certification Type Name</param>
    ''' <returns>String.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetCertTemplate(ByVal p_strCertificationTypeName As String) As String

    'added - for generic certificate types - jeseitz 6/10/2016
    ''' <summary>
    ''' Method to Get Certification Name By ID.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <returns>String.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetCertificationNameByID(ByVal p_intCertificationTypeID As Integer) As String

    ' Added GetMaterialAttribs function to retrieve the attributes for Material number from product data web service
    ' as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Method to Get Material Attributes.
    ''' </summary>
    ''' <param name="p_strMatlNum">Material NUmber</param>
    ''' <returns>Datatable.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Function GetMaterialAttribs(ByVal p_strMatlNum As String) As DataTable

    ' Added GetBrands function to retrieve the brands for Material number from product data web service
    ' as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Method to Get Brands.
    ''' </summary>
    ''' <param name="p_strBrand">Brand</param>
    ''' <returns>Datatable.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetBrands(ByVal p_strBrand As String) As DataTable

    ' Added GetBrandLines function to retrieve the brand lines for Material number from product data web service
    'as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ''' <summary>
    ''' Method to get Brand Lines.
    ''' </summary>
    ''' <param name="p_strBrandLine">Brand LIne</param>
    ''' <returns>Datatable.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetBrandLines(ByVal p_strBrandLine As String) As DataTable

    ' Added as per project 2706 technical specification
    ''' <summary>
    ''' Method to Get Certified Material Count.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_strCertificateExtension">Certificate Extension</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>    ''' 
    ''' <returns>Integer.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetCertifiedMaterialCount(ByVal p_intCertificationTypeId As Integer, _
                                       ByVal p_strCertificateNumber As String, _
                                       ByVal p_strCertificateExtension As String) As Integer
    ''' <summary>
    ''' Method to Rename certificate.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_strNewCertificateExtension">New Certificate Extension</param>
    ''' <param name="p_strNewCertificateNumber">New Certificate NUmber</param>
    ''' <param name="p_strOldCertificateExtension">Old Certificate Extension</param>
    ''' <param name="p_strOldCertificateNumber">Old Certificate Number</param>
    ''' <param name="p_strUserName">UserName</param>
    ''' <returns>Boolean.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function RenameCertificate(ByVal p_intCertificationTypeId As Integer, _
                               ByVal p_strOldCertificateNumber As String, _
                               ByVal p_strOldCertificateExtension As String, _
                               ByVal p_strNewCertificateNumber As String, _
                               ByVal p_strNewCertificateExtension As String, _
                               ByVal p_strUserName As String) As Boolean
    ''' <summary>
    ''' Method to Delete Certificate.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_strCertificateExtension">Certificate Extension</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <param name="p_strUserName">User Name</param>
    ''' <returns>Boolean.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function DeleteCertificate(ByVal p_intCertificationTypeId As Integer, _
                               ByVal p_strCertificateNumber As String, _
                               ByVal p_strCertificateExtension As String, _
                               ByVal p_strUserName As String) As Boolean
    ''' <summary>
    ''' Method to get Certificate Materials.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_strCertificateExtension">Certificate Extension</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <returns>Datatable.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetCertificateMaterials(ByVal p_intCertificationTypeId As Integer, _
                                     ByVal p_strCertificateNumber As String, _
                                     ByVal p_strCertificateExtension As String) As DataTable
    ''' <summary>
    ''' Method to Detach Certificate.
    ''' </summary>
    ''' <param name="p_intCertificateId">Certification Id</param>
    ''' <param name="p_intSkuId">SKU ID</param>
    ''' <returns>Boolean.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function DetachCertificate(ByVal p_intSkuId As Integer, _
                               ByVal p_intCertificateId As Integer) As Boolean
    ''' <summary>
    ''' Method to Move Certificate.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_intCertificateId">Certificate Id</param>
    ''' <param name="p_intSkuId">SKU Id</param>
    ''' <param name="p_strNewCertificateExtension">New Certification Extension</param>
    ''' <param name="p_strNewCertificateNumber">New Certificate NUmber</param>
    ''' <param name="p_strUserName">User Name</param>
    ''' <returns>Boolean.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function MoveCertificate(ByVal p_intCertificationTypeId As Integer, _
                             ByVal p_strNewCertificateNumber As String, _
                             ByVal p_strNewCertificateExtension As String, _
                             ByVal p_intSkuId As Integer, _
                             ByVal p_intCertificateId As Integer, _
                             ByVal p_strUserName As String) As Boolean
    ''' <summary>
    ''' Method to Get Duplicate Certificates.
    ''' </summary>
    ''' <param name="p_strMaterialNumber">Material Number</param>
    ''' <param name="p_strSpeedRating">Speed rating</param>
    ''' <returns>Datatable.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetDuplicateCertificates(ByVal p_strMaterialNumber As String, _
                                      ByVal p_strSpeedRating As String) As DataTable

    ''' <summary>
    ''' Method to Delete Duplicate Certificates.
    ''' </summary>
    ''' <param name="p_intSkuId">SKU ID</param>
    ''' <returns>Boolean.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function DeleteDuplicateCertificates(ByVal p_intSkuId As Integer) As Boolean

    ''' <summary>
    ''' Method to Check whether the Family id exists or not and return Family Desc.
    ''' </summary>
    ''' <param name="p_intFamilyId">Family Id</param>
    ''' <param name="p_strFamilyDesc">Family Desc</param>
    ''' <returns>Boolean.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function CheckIsFamilyIdExist(ByVal p_intCertificateid As Integer, ByVal p_intFamilyId As String, ByRef p_strFamilyDesc As String) As Boolean

    ''' <summary>
    ''' Method to Get Tire Type.
    ''' </summary>
    ''' <returns>Datatable.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetTireType() As DataTable

    ''' <summary>
    ''' Method to Inserts/updates the record in Imark family table.
    ''' </summary>
    ''' <param name="p_intFamilyID">FamilyID </param>
    ''' <param name="p_strFamilyCode">FamilyCode</param>
    ''' <param name="p_strFamilyDesc">FamilyDesc</param>
    ''' <param name="p_strApplicationCat">ApplicationCat</param>
    ''' <param name="p_strConstructionType">ConstructionType</param>
    ''' <param name="p_strStructureType">StructureType</param>
    ''' <param name="p_strMountingType">MountingType</param>
    ''' <param name="p_strAspectRatioCat">AspectRatioCat</param>
    ''' <param name="p_strSpeedRatingCat">SpeedRatingCat</param>
    ''' <param name="p_strLoadIndexCat">LoadIndexCat</param>    
    ''' <param name="p_strUserName">Username</param>  
    ''' <returns>Boolean.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function SaveFamily(ByVal p_intCertificateid As Integer, ByVal p_intFamilyID As Integer, _
                       ByVal p_strFamilyCode As String, _
                       ByVal p_strFamilyDesc As String, _
                       ByVal p_strApplicationCat As String, _
                       ByVal p_strConstructionType As String, _
                       ByVal p_strStructureType As String, _
                       ByVal p_strMountingType As String, _
                       ByVal p_strAspectRatioCat As String, _
                       ByVal p_strSpeedRatingCat As String, _
                       ByVal p_strLoadIndexCat As String, _
                       ByVal p_strUserName As String) As System.Boolean


    ''' <summary>
    ''' Method to record from Imark family table for the given family id.
    ''' </summary>
    ''' <param name="p_intCertificateid">Certification Id</param>
    ''' <param name="p_intFamilyId">Family Id</param>
    ''' <returns>Boolean.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function Deletefamily(ByVal p_intCertificateid As Integer, ByVal p_intFamilyId As Integer) As Boolean

    ''' <summary>
    ''' Method to Get records from Imark Family table.
    ''' </summary>
    ''' <param name="p_intCertificateid">Certificate Id</param>
    ''' <returns>Datatable.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetFamilies(ByVal p_intCertificateid As Integer) As DataTable

    ''' <summary>
    ''' Method to Copy Certification.
    ''' </summary>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <returns>Boolean.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function CopyCertification(ByVal p_strMatlNum As String) As Boolean

    ''' <summary>
    ''' Method to Attach Certification.
    ''' </summary>
    ''' <param name="p_certificationTypeId">Certification Type Id</param>
    ''' <param name="p_skuid">SKU ID</param>
    ''' <param name="p_strCertNum">Certificate NUmber</param>
    ''' <param name="p_strExtensionEn">Extension</param>
    ''' <returns>String.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function AttachCertification(ByVal p_skuid As Integer, _
                                 ByVal p_strCertNum As String, _
                                 ByVal p_strExtensionEn As String, _
                                 ByVal p_certificationTypeId As Integer) As String
    ''' <summary>
    ''' Method to Edit Material.
    ''' </summary>
    ''' <param name="p_intSkuID">SKU ID</param>
    ''' <param name="p_strSpeedrating">Speed Rating</param>
    ''' <returns>Boolean.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function EditMaterial(ByVal p_intSkuID As Integer, _
                          ByVal p_strSpeedrating As String) As Boolean

    ''' <summary>
    ''' Method to get Material Number.
    ''' </summary>
    ''' <param name="p_strMaterialNumber">Material Number</param>
    ''' <returns>Datatable.</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Function GetMaterial(ByVal p_strMaterialNumber As String) As DataTable

End Interface

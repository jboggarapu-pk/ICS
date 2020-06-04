Imports TRACSSharedDatasets
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

    Function GetRequestedTests(ByVal p_iCertificationTypeId As Integer, ByVal p_iTireTypeId As Integer, ByVal p_dstClientRequest As DataSet) As DataSet

    Function GetAuditLog() As DataSet

    Function GetAuditLogAfterDate(ByVal p_dtmChangeDateTime As DateTime) As DataSet

    Function GetApprovalReasons(ByVal p_intCertificationTypeId As Integer) As DataSet

    Function GetApprovedSubstitution(ByVal p_intCertificationTypeId As Integer, ByVal p_strField As String, ByVal p_sngValue As Single, ByVal p_intSKUID As Integer) As Single

    Function GetDefaultValues(ByVal p_strCertificateType As String, ByVal p_intCertificateNumberID As Integer, ByRef p_strCertificateNumber As String) As DataSet

    Function GetQueryControlGridSource() As DataTable

    Function CertificateDefaultvalueSave(ByVal certDeftValues As List(Of CertificationDefaultField)) As Boolean

    Function CertificateValueSave(ByVal p_certDeftValues As List(Of CertificationDefaultField), ByVal p_certificateNo As String) As Boolean

    Function UpdateAuditLogEntry(ByVal p_intChangeLogID As Integer, ByVal p_dtmChangeDateTime As DateTime, ByVal p_strApprovalStatus As String, ByVal p_strApprover As String) As Boolean

    Function GetProductData_SKUTRACS(ByVal strMatlNum As String) As SKUtoICSDataset

    Function GetTRACSData(ByVal intCertType As Integer, ByVal intTireType As Integer, ByVal strMatlNum As String, ByVal intManufacturingLocationId As Integer) As TRACStoICSDataset

    Function GetDataForEmarkPassengerReport(ByVal p_strCertificateNumber As String, _
                                            ByVal p_intCertificationTypeId As Integer, _
                                            ByVal p_strExtension As String, _
                                            ByVal p_intTireTypeId As Integer) As DataSet

    Function GetDataForEmarkE117Report(ByVal p_strCertificateNumber As String, _
                                            ByVal p_intCertificationTypeId As Integer, _
                                            ByVal p_strExtension As String, _
                                            ByVal p_intTireTypeId As Integer) As DataSet

    Function GetDataForEmarkReportWithTR(ByVal p_strCertificateNumber As String, _
                                         ByVal p_intTireTypeId As Integer) As Dataset

    Function GetDataForCCCReport(ByVal p_strCertificateNumber As String, _
                                 ByVal p_intCertificationTypeId As Integer, _
                                 ByVal p_strExtension As String) As CCCSequentialDataSet

    Function GetDataForCCCProductDescriptionReport(ByVal p_strCertificateNumber As String, _
                                 ByVal p_intCertificationTypeId As Integer, _
                                 ByVal p_strExtension As String) As CCCProductDescriptionDataSet

    ' Added p_strBrandLine parameter as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    Function GetDataForSKUCertification(ByVal p_strMatlNum As String, ByVal p_strBrand As String, ByVal p_strBrandLine As String, ByVal p_strCertType As String) As CertificateReport

    Function GetDataForImarkCertification(ByVal p_dtDateParam As DateTime) As ImarkCertificationDataSet

    Function GetDataForGSOPassengerReport(ByVal p_strCertificateNumber As String, _
                                          ByVal p_intCertificationTypeId As Integer, _
                                          ByVal p_strExtension As String, _
                                          ByVal p_intTireTypeId As Integer) As GSOCertificateDataSet

    Function GetDataForGSOConformityReport(ByVal p_strBatchNumber As String, _
                                       ByVal p_intCertificationTypeId As Integer, _
                                       ByVal p_intTireTypeId As Integer) As DataSet

    Function GetDataForImarkConformityReport() As DataSet

    ' Added p_strBrandLine parameter as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    Function GetDataForEmarkCertificationReport(ByVal p_strCertificationNo, ByVal p_strBrand, ByVal p_strBrandLine) As DataSet

    Function GetDataForImarkSamplingTireTestsReport(ByVal p_strMatlNum As String) As DataSet

    Function GetTraceabilityReportInfo(ByVal p_strCertificateNumber As String, ByVal p_intCertificationTypeID As Integer, ByVal p_strIncludeArchived As String) As Traceability

    Function GetExceptionReportInfo() As ExceptionReport_DataSet

    Function GetEmarkApplication(ByVal p_strCertificateNumber As String, ByVal p_intTireTypeID As Integer) As DataSet

    Function GetNomCertification(ByVal p_strCertificateNumber As String, ByVal p_intTireTypeID As Integer) As DataSet

    Function GetAuthenticityReport() As DataSet

    Function GetEmarkSimilarCertificateSearchReport(ByVal p_MatlNum As String) As DataSet

    Function GetCertificationRenewal(ByVal p_strCertificateNumber As String, ByVal p_intCertificationTypeID As Integer) As DataSet

    Function CheckSimilarTire(ByVal intCertType As Integer, ByVal strInMatlNum As String, ByRef p_strSimilarMatlNum As String, ByRef intImarkFamily As Integer, ByRef ECEReference As String, ByRef strMessage As String) As Integer

    'JBH_2.00 Project 5325: Added Mold Change Required and Operations Date Approved parameters
    'jeseitz 10/29/2016 added Additional Info field parameter
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

    Function BatchNumMassUpdate(ByVal p_strCertifName As String, _
                                ByVal p_strTempBatchNum As String, _
                                ByVal p_strGSOBatchNum As String, _
                                ByVal p_strUserName As String) As NameAid.SaveResult

    Function GetRegionCertStatus(ByVal p_strBrand As String, ByVal p_strBrandLine As String) As DataSet

    Function GetProductCertStatus(ByVal p_strBrand As String, ByVal p_strBrandLine As String) As DataSet

    Function GetCountries(ByVal p_strRegionName As String) As DataTable

    Function GetImporters() As DataTable

    Function GetCustomers() As DataTable

    Function SaveCertificationRequest(ByVal p_blnDeleteMe As Boolean, _
                                      ByVal p_strMatlNum As String, _
                                      ByVal p_intCountryID As Integer, _
                                      ByVal p_intSKUID As Integer) As Boolean

    Function SaveCertificationGroup(ByVal p_blnDeleteMe As Boolean, _
                                    ByVal p_strMatlNum As String, _
                                    ByVal p_intCertificationID As Integer, _
                                    ByVal p_intSKUID As Integer) As Boolean

    Function SaveRequestCert(ByVal p_blnDeleteMe As Boolean, _
                                ByVal p_strMatlNum As String, _
                                ByVal p_intCertificationID As Integer, _
                                ByVal p_intSKUID As Integer) As Boolean

    Function CheckMatlNumExists(ByVal p_strMatlNum As String) As Boolean

    Function RefreshProduct(ByVal p_strMatlNum As String, ByRef p_strErrorDesc As String) As Integer

    Function CheckIfCertificateNumberExists(ByVal p_strCertNum As String) As Boolean

    Function GetLatestImarkCertifId() As Integer

    Function RenewCertificate(ByVal p_intCertificateId As Integer, ByRef p_intNewCertificateID As Integer, ByVal p_strUserName As String) As NameAid.SaveResult

    Function GetCertifExtension(ByVal p_intImarkCertId As Integer) As String

    Function GetLatestGSOCertifNumber() As String

    Function SaveNewCertificate(ByVal p_strCertNum As String, _
                                ByVal p_intCertTypeId As Integer, _
                                ByVal p_strMatlNum As String, _
                                ByVal p_strImporter As String, _
                                ByVal p_strCustomer As String, _
                                ByVal p_strUserName As String, _
                                ByVal p_strExtension As String) As Boolean

    Function SaveNewCertificate(ByVal p_strCertNum As String, _
                               ByVal p_intCertTypeId As Integer, _
                               ByVal p_strMatlNum As String, _
                               ByVal p_strImporter As String, _
                               ByVal p_strCustomer As String, _
                               ByVal p_strUserName As String, _
                               ByVal p_strExtension As String, _
                               ByVal p_InsertPC As String, _
                               ByRef p_ErrorDesc As String) As Integer

    Function ArchiveCertification(ByVal p_strCertNum As String, _
                                  ByVal p_strUserName As String) As Boolean

    Function SaveAuditLogEntry(ByVal p_dteChangeDateTime As DateTime, _
                                ByVal m_strChangedBy As String, _
                                ByVal m_strArea As String, _
                                ByVal m_strChangedFieldElement As String, _
                                ByVal m_strOldValue As String, _
                                ByVal m_strNewValue As String, _
                                ByVal m_intReasonID As Integer, _
                                ByVal m_strNote As String) As System.Boolean

    Function GetCertifications() As DataSet

    Function GetSearchTypeResults() As DataSet

    Function GetManufacturingLocationsResults(ByVal p_strSize As String) As DataSet

    Function GetCompanyNameList() As DataSet

    Function GetCertificationSearchResults(ByVal p_strSearchCriteria As String, _
                                           ByVal p_strSearchType As String, _
                                           ByVal p_strExtensionNo As String, _
                                           ByVal p_strImarkFamily As String, _
                                           ByVal ps_BrandLine As String) As DataTable


    Function GetCertificate(ByVal ps_CertificatonNumber As String, ByVal ps_ExtensionNo As String, ByVal ps_CertificationTypeID As Integer, ByVal p_iSKUID As Integer, ByRef p_blnTRsExist As Boolean) As DataTable

    Function GetSimilarCertificate(ByVal p_iCertificationTypeID As Integer, ByVal p_strMatlNum As String, ByVal p_strCertificationNumber As String) As DataTable



    Function GetProductData(ByVal p_strMatlNum As String, ByVal p_iSKUID As Integer) As ICSDataSet.ProductDataDataTable


    Function GetTestResultData(ByVal p_strMatlNum As String, ByVal p_intSKUID As Integer, ByVal p_strCertificateNumber As String, ByVal p_intCertificateNumberID As Integer, ByVal p_intCertificationTypeId As Integer) As ICSDataSet


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
    Function GetCertificatesByType(ByVal p_certificationtypeid As Integer, ByVal p_all As String) As DataTable

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
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

    Function MeasurementDetail_Save(ByVal p_sngSectionWidth As Single, _
                                   ByVal p_sngOVERALLWIDTH As Single, _
                                   ByVal p_iMEASUREID As Integer, _
                                   ByVal p_sngITERATION As Single, _
                                   ByVal p_strUserName As String) As NameAid.SaveResult

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
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

    Function SavePlungerDetail(ByVal p_sngBREAKINGENERGY As Single, _
                              ByVal p_intPlungerID As Integer, _
                              ByVal p_sngIteration As Single, _
                              ByVal p_strUserName As String) As NameAid.SaveResult

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
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

    Function SaveTreadWearDetail(ByVal p_sngwearbarheight As Single, _
                                ByVal p_intTREADWEARID As Integer, _
                                ByVal p_sngIteration As Single, _
                                ByVal p_strOperatorName As String) As NameAid.SaveResult

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
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

    Function SaveBeadUnseatDetail(ByVal p_intBEADUNSEATID As Integer, _
                                 ByVal p_sngUNSEATFORCE As Single, _
                                 ByVal p_sngIteration As Single, _
                                 ByVal p_strOperatorName As String) As NameAid.SaveResult

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
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

    Function SaveHighSpeed_SpeedTestDetail(ByVal p_intHighSpeedID As Integer, _
                                          ByVal p_intIteration As Integer, _
                                          ByVal p_dteTime As DateTime, _
                                          ByVal p_sngSpeed As Single, _
                                          ByVal p_strUserName As String) As NameAid.SaveResult

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
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

    Function AddCustomer(ByVal p_intSKUId As Integer, _
                            ByVal p_strCustomer As String, _
                            ByVal p_strImporter As String, _
                            ByVal p_strImporterRepresentative As String, _
                            ByVal p_strImporterAddress As String, _
                            ByVal p_strCountryLocation As String) As NameAid.SaveResult

    Function GetCertificateID(ByVal p_strCertificateNumber As String, ByVal p_intCertificateTypeId As Integer, ByVal p_strExtensionNo As String) As Integer

    Function GetCertificationTypeID(ByVal p_strCertificationTypeName As String) As Integer

    'added - for generic certificate types - jeseitz 6/9/2016
    Function GetCertTemplate(ByVal p_strCertificationTypeName As String) As String

    'added - for generic certificate types - jeseitz 6/10/2016
    Function GetCertificationNameByID(ByVal p_intCertificationTypeID As Integer) As String

    ' Added GetMaterialAttribs function to retrieve the attributes for Material number from product data web service
    ' as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    Function GetMaterialAttribs(ByVal p_strMatlNum As String) As DataTable

    ' Added GetBrands function to retrieve the brands for Material number from product data web service
    ' as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    Function GetBrands(ByVal p_strBrand As String) As DataTable

    ' Added GetBrandLines function to retrieve the brand lines for Material number from product data web service
    'as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    Function GetBrandLines(ByVal p_strBrandLine As String) As DataTable

    ' Added as per project 2706 technical specification
    Function GetCertifiedMaterialCount(ByVal p_intCertificationTypeId As Integer, _
                                       ByVal p_strCertificateNumber As String, _
                                       ByVal p_strCertificateExtension As String) As Integer

    Function RenameCertificate(ByVal p_intCertificationTypeId As Integer, _
                               ByVal p_strOldCertificateNumber As String, _
                               ByVal p_strOldCertificateExtension As String, _
                               ByVal p_strNewCertificateNumber As String, _
                               ByVal p_strNewCertificateExtension As String, _
                               ByVal p_strUserName As String) As Boolean

    Function DeleteCertificate(ByVal p_intCertificationTypeId As Integer, _
                               ByVal p_strCertificateNumber As String, _
                               ByVal p_strCertificateExtension As String, _
                               ByVal p_strUserName As String) As Boolean

    Function GetCertificateMaterials(ByVal p_intCertificationTypeId As Integer, _
                                     ByVal p_strCertificateNumber As String, _
                                     ByVal p_strCertificateExtension As String) As DataTable

    Function DetachCertificate(ByVal p_intSkuId As Integer, _
                               ByVal p_intCertificateId As Integer) As Boolean

    Function MoveCertificate(ByVal p_intCertificationTypeId As Integer, _
                             ByVal p_strNewCertificateNumber As String, _
                             ByVal p_strNewCertificateExtension As String, _
                             ByVal p_intSkuId As Integer, _
                             ByVal p_intCertificateId As Integer, _
                             ByVal p_strUserName As String) As Boolean

    Function GetDuplicateCertificates(ByVal p_strMaterialNumber As String, _
                                      ByVal p_strSpeedRating As String) As DataTable

    Function DeleteDuplicateCertificates(ByVal p_intSkuId As Integer) As Boolean

    ''' <summary>
    ''' Check whether the Family id exists or not and return Family Desc
    ''' </summary>
    ''' <param name="p_intFamilyId">Family Id</param>
    ''' <param name="p_strFamilyDesc">Family Desc</param>
    ''' <returns>boolean value</returns>
    ''' <remarks></remarks>
    Function CheckIsFamilyIdExist(ByVal p_intCertificateid As Integer, ByVal p_intFamilyId As String, ByRef p_strFamilyDesc As String) As Boolean

    ''' <summary>
    ''' Gets type's of Tyres.
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Function GetTireType() As DataTable

    ''' <summary>
    ''' Inserts/updates the record in Imark family table
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
    ''' <returns>boolean value</returns>
    ''' <remarks></remarks>
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
    ''' Delete the record from Imark family table for the given family id
    ''' </summary>
    ''' <param name="p_intFamilyId">FamilyId</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function Deletefamily(ByVal p_intCertificateid As Integer, ByVal p_intFamilyId As Integer) As Boolean

    ''' <summary>
    ''' Get the records from Imark Family table
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Function GetFamilies(ByVal p_intCertificateid As Integer) As DataTable

    ''' <summary>
    ''' Copy Certification
    ''' </summary>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function CopyCertification(ByVal p_strMatlNum As String) As Boolean

    ''' <summary>
    ''' Attach Certification.
    ''' </summary>
    ''' <param name="p_skuid">Sku Id</param>
    ''' <param name="p_strCertNum">Certificate Number</param>
    ''' <param name="p_strExtensionEn">Extension Number</param>
    ''' <param name="p_certificationTypeId">Certificate type id</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function AttachCertification(ByVal p_skuid As Integer, _
                                 ByVal p_strCertNum As String, _
                                 ByVal p_strExtensionEn As String, _
                                 ByVal p_certificationTypeId As Integer) As String

    ''' <summary>
    ''' Update Speedrating of a Material
    ''' </summary>
    ''' <param name="p_intSkuID"></param>
    ''' <param name="p_strSpeedrating"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function EditMaterial(ByVal p_intSkuID As Integer, _
                          ByVal p_strSpeedrating As String) As Boolean

    ''' <summary>
    ''' Get material Details
    ''' </summary>
    ''' <param name="p_strMaterialNumber"></param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Function GetMaterial(ByVal p_strMaterialNumber As String) As DataTable

End Interface

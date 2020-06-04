Imports CooperTire.ICS.DomainEntities
Imports CooperTire.ICS.Common

''' <summary>
''' Interface for test result view
''' </summary>
''' <remarks></remarks>
Public Interface ITestResultsView
    Inherits IView

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

    Property InfoText() As String
    Property ErrorText() As String

    Property IsVisible() As Boolean

    ' Added properties as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    Property MaterialNumber() As String

    Property CertificationTypeId() As Integer
    Property SKUID() As Integer
    Property TireTypeId() As Integer

    ' Changed as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    Property SimilarTireMatlNum() As String

    Property SimilarTireSKUID() As Integer

    Property CertificateNumber() As String
    Property ExtensionNo() As String
    Property CertificateNumberID() As Integer

    Property ManufacturingLocationId() As Integer
    Property ClientRequest() As DataTable

    Event DoLoadViewDataEvent As CustomEvents.PlainEventHandler
    Event DoLoadViewBlankEvent As CustomEvents.PlainEventHandler
    Event GetRequestedTests As CustomEvents.PlainEventHandler

    Sub DoLoadViewData()
    Sub DoLoadViewBlank()
    Sub AdjustViewToCertificationType()

    Sub SetTRProductSectionData(ByVal p_objTRPSD As TRProductSectionData)
    Sub SetTRMeasureSectionData(ByVal p_objTRMSD As TRMeasurementSectionData)
    Sub SetTRProjectSectionData(ByVal p_ObjTRPSD As TRProjectSectionData)

    Sub SetTREnduranceBeforeSectionData(ByVal p_objTRESD As TREnduranceTestGeneralBeforeSectionData)
    Sub SetTREnduranceSectionData(ByVal p_objTRESD As TREnduranceSectionData)
    Sub SetTREnduranceAfterSectionData(ByVal p_objTRESD As TREnduranceTestGeneralAfterSectionData)

    Sub SetTRHighSpeedBeforeSectionData(ByVal p_objTRHSSD As TRHighSpeedTestGeneralBeforeSectionData)
    Sub SetTRHighSpeedSectionData(ByVal p_objTRHSSD As TRHighSpeedSectionData)
    Sub SetTRHighSpeedAfterSectionData(ByVal p_objTRHSSD As TRHighSpeedTestGeneralAfterSectionData)

    Sub SetTRSoundWetSectionData(ByVal p_objTRSWSD As TRSoundWetSectionData)

    Function GetTRProductSectionData() As TRProductSectionData
    Function GetTRMeasurementSectionData() As TRMeasurementSectionData
    Function GetTRProjectSectionData() As TRProjectSectionData

    Function GetTREnduranceBeforeSectionData() As DomainEntities.TREnduranceTestGeneralBeforeSectionData
    Function GetTREnduranceSectionData() As DomainEntities.TREnduranceSectionData
    Function GetTREnduranceAfterSectionData() As DomainEntities.TREnduranceTestGeneralAfterSectionData

    Function GetTRHighSpeedBeforeSectionData() As DomainEntities.TRHighSpeedTestGeneralBeforeSectionData
    Function GetTRHighSpeedSectionData() As DomainEntities.TRHighSpeedSectionData
    Function GetTRHighSpeedAfterSectionData() As DomainEntities.TRHighSpeedTestGeneralAfterSectionData

    Function GetTRSoundWetSectionData() As DomainEntities.TRSoundWetSectionData

End Interface

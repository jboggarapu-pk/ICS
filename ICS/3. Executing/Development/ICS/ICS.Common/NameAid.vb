
''' <summary>
''' Constants definitions
''' </summary>
''' <remarks>
''' <list type="table">
''' <listheader>
''' <term>Author</term>
''' <description>Description</description>
''' </listheader>
''' <item>
''' <term>NA</term>
''' <description>
''' <para>NA</para>
''' <para>Original Code.</para>
''' </description>
''' </item>
''' <item>
''' <term>Srinivas S</term>
''' <description>
''' <para>09/18/2019</para>
''' <para>Implemented code standardization.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class NameAid

#Region "NameAid Enum Variable Declaration"
    ' Added constants in Column class as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 

    ''' <summary>
    ''' Enum to hold TireType Ids
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TireType
        ''' <summary>
        ''' Passenger type
        ''' </summary>
        Passenger = 1
        ''' <summary>
        ''' Light Truck
        ''' </summary>
        LightTruck = 3
        ''' <summary>
        ''' Specialty Light Truck
        ''' </summary>
        SpecialtyLightTruck = 4
        ''' <summary>
        ''' Medium Truck
        ''' </summary>
        MediumTruck = 7
    End Enum
  
    ''' <summary>
    ''' Enum to hold Report names
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum Report
        CCCSequentialReport
        CCCProductDescriptionReport
        EmarkCertification
        EmarkPassengerApplication
        EmarkLightTruckApplication
        EmarkPassengerCertification
        EmarkLightTruckCertification
        EmarkE117Certification
        EmarkMSRPassengerReport
        EmarkMSRTruckReport
        EmarkMETruckReport
        EmarkMelkshamTestReport
        EmarkSimilarCertificateSearch
        GSOPassengerCertification
        GSOTruckCertification
        GSOConformityCertificateReport
        ImarkCertification
        ImarkConformityMarkReport
        ImarkSamplingAndTireTests
        ImarkECEAuthenticityReport
        NOMPassengerCertification
        NOMLightTruckCertification
        SKUCertification
        TraceabilityReport
        ExceptionReport
        CertificationRenewalReport
    End Enum

    ''' <summary>
    ''' Enum that indicates the result of the saving operation
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SaveResult
        ''' <summary>
        ''' All was well and info was saved
        ''' </summary>
        Sucess = 1
        ''' <summary>
        ''' There was problems with validation
        ''' </summary>
        ValidationError = 2
        ''' <summary>
        ''' There was problems with the saving operation
        ''' </summary>
        SaveError = 3
        ''' <summary>
        ''' Attempt to add duplicate entity
        ''' </summary>
        ''' <remarks></remarks>
        DuplicationError = 4
    End Enum

#End Region

#Region "CommonUtility Class holding Constants"

    ''' <summary>
    ''' Holding Column names with constants
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/18/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Class Column
        Public Const SKU As String = "SKU"
        Public Const SKUID As String = "SKUID"

        ' Added constants to retrieve material attributes as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
        Public Const MaterialNumber As String = "MATL_NUM"
        Public Const AttributeName As String = "ATTRIB_NAME"
        Public Const AttributeValue As String = "ATTRIB_VALUE"
        Public Const SingleLoadIndex As String = "SINGLOADINDEX"
        Public Const DualLoadIndex As String = "DUALLOADINDEX"
        Public Const SpeedRating As String = "SPEEDRATING"

        Public Const Size As String = "Size"
        Public Const CountryID As String = "CountryID"
        Public Const CountryName As String = "CountryName"
        Public Const RegionName As String = "RegionName"
        Public Const SizeStamp As String = "SizeStamp"
        Public Const State As String = "State"
        Public Const CertificationTypeName As String = "CertificationTypeName"
        Public Const CertificationId As String = "CERTIFICATIONID"
        Public Const CertificationName As String = "CERTIFICATIONNAME"
        Public Const FieldId As String = "fieldid"
        Public Const FieldName As String = "FIELDNAME"
        Public Const FieldText As String = "fieldtext"
        Public Const FieldValue As String = "FIELDVALUE"
        Public Const ReportName As String = "REPORTNAME"
        Public Const CertificationTypeId As String = "CERTIFICATIONTYPEID"
        Public Const CertificateNo As String = "CERTIFICATENUMBER"
        Public Const ChangeLogId As String = "CHANGELOGID"
        Public Const ChangedDateTime As String = "CHANGEDATETIME"
        Public Const ChangedBy As String = "CHANGEDBY"
        Public Const Area As String = "AREA"
        Public Const ChangedFiledElement As String = "CHANGEDFILED_ELEMENT"
        Public Const OldValue As String = "OLDVALUE"
        Public Const NewValue As String = "NEWVALUE"
        Public Const ApprovalStatus As String = "APPROVALSTATUS"
        Public Const Approver As String = "APPROVER"
        Public Const CertificateID As String = "CERTIFICATEID"
        Public Const Customer As String = "CUSTOMER"
        Public Const Note As String = "NOTE"
    End Class

    ''' <summary>
    ''' Marketing request status for certification
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/18/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Class MarketingStatus
        Public Const Uncertified As String = "Uncertified"
        Public Const Requested As String = "Requested"
        Public Const InProgress As String = "InProgress"
        Public Const Approved As String = "Approved"
    End Class
#End Region

End Class

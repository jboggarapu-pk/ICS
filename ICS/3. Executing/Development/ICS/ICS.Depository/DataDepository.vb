Imports System.Data
Imports CooperTire.ICS.DataAccess
Imports CooperTire.ICS.DomainEntities
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.Datasets

''' <summary>
''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
''' <term>Vinay Chowdavarapu</term>
''' <description>
''' <para>09/10/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class DataDepository
    Implements IDepository

    ' Modified as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ' Used Material Number instead of SKU, PSN instead of NPRId, TPN instead of PPN and Brand, Brand Line instead of Brand Code.
    ' Added Operation as paramter for HDR save methods.

#Region " Public Methods"

    ''' <summary>
    ''' Pass the call to respective data source by calling the TRACStoICSDataset.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification type id.</param>
    ''' <param name="p_intTireTypeId">Tire type id.</param>
    ''' <param name="p_dstClientRequest">Client request data.</param>
    ''' <returns>TRACS to ICSDataset.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>1. Implemented Coding Standards.</para>
    ''' <para>2. Implemented exception Handling.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetRequestedTests(ByVal p_intCertificationTypeId As Integer, ByVal p_intTireTypeId As Integer, ByVal p_dstClientRequest As DataSet) As DataSet Implements IDepository.GetRequestedTests
        '' Parameter validation Not required
        Dim dsTRACStoICSDataset As ICS.Datasets.TRACStoICSDataset = Nothing
        Dim objTRACSSupportBus As Business = Nothing
        Dim blnSuccess As Boolean = False

        'Added Properties as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation.
        Dim strUseSap As String = String.Empty
        Dim strUseTracs As String = String.Empty
        Try
            objTRACSSupportBus = New Business
            p_dstClientRequest = TryCast(p_dstClientRequest, ICS.Datasets.ClientRequest)
            strUseSap = CooperTire.ICS.Common.AppSettingsAid.GetUseSAP()
            strUseTracs = CooperTire.ICS.Common.AppSettingsAid.GetUseTracs()
            dsTRACStoICSDataset = New ICS.Datasets.TRACStoICSDataset
            dsTRACStoICSDataset = objTRACSSupportBus.GetClientTests(p_intCertificationTypeId, p_intTireTypeId, strUseSap, strUseTracs, CType(p_dstClientRequest, ClientRequest), blnSuccess)
        Catch
            Throw
        End Try
        Return dsTRACStoICSDataset

    End Function

    ''' <summary>
    ''' Used to getting audit logs.
    ''' </summary>
    ''' <returns>Audit logs</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>1. Implemented exception Handling.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetAuditLog() As DataSet Implements IDepository.GetAuditLog
        Dim dsResults As DataSet = Nothing
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                dsResults = New DataSet
                dsResults = objCertDalc.GetAuditLog()
            End Using
        Catch
            Throw
        End Try
        Return dsResults

    End Function

    ''' <summary>
    ''' Using Get Approval reasons for certification type id.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification type id.</param>
    ''' <returns>Approval reasons.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>1. Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetApprovalReasons(ByVal p_intCertificationTypeId As Integer) As DataSet Implements IDepository.GetApprovalReasons
        Dim dsApprovalReasons As DataSet = Nothing
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                dsApprovalReasons = New DataSet
                dsApprovalReasons = objCertDalc.GetApprovalReasons(p_intCertificationTypeId)
            End Using
        Catch
            Throw
        End Try
        Return dsApprovalReasons

    End Function

    ''' <summary>
    ''' Using to Approved Substitution
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification type id.</param>
    ''' <param name="p_strField">Field value.</param>
    ''' <param name="p_sngValue">Single value.</param>
    ''' <param name="p_intSKUID">SKU ID.</param>
    ''' <returns>Approved substitution value.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>1. Implemented Coding Standards.</para>
    ''' <para>2. Implemented exception handling.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetApprovedSubstitution(ByVal p_intCertificationTypeId As Integer,
                                            ByVal p_strField As String,
                                            ByVal p_sngValue As Single,
                                            ByVal p_intSKUID As Integer) As Single Implements IDepository.GetApprovedSubstitution
        Dim intNewValue As Single = 0
        Try
            If p_sngValue > 0 Then
                Using objCertDalc As CertificationDalc = New CertificationDalc
                    intNewValue = objCertDalc.GetApprovedSubstitution(p_intCertificationTypeId, p_strField, p_sngValue, p_intSKUID)
                End Using
            Else
                intNewValue = p_sngValue
            End If
        Catch
            Throw
        End Try
        Return intNewValue

    End Function

    ''' <summary>
    ''' Get audit logs after the given date.
    ''' </summary>
    ''' <param name="p_dtmChangeDateTime">Date change time.</param>
    ''' <returns>Audit logs.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' <para>2. Implemented exception handling.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetAuditLogAfterDate(ByVal p_dtmChangeDateTime As Date) As System.Data.DataSet Implements IDepository.GetAuditLogAfterDate
        Dim dstResults As New DataSet
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                dstResults = objCertDalc.GetAuditLogAfterDate(p_dtmChangeDateTime)
            End Using
        Catch
            Throw
        End Try
        Return dstResults

    End Function

    ''' <summary>
    ''' Get grid source
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetQueryControlGridSource() As DataTable Implements IDepository.GetQueryControlGridSource
        Dim dtGridSource As DataTable = Nothing
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                dtGridSource = New DataTable
                dtGridSource = objCertDalc.GetQueryControlGridSource()
            End Using
        Catch
            Throw
        End Try
        Return dtGridSource
    End Function

    ''' <summary>
    ''' Update audit log
    ''' </summary>
    ''' <param name="p_intChangeLogID">Change Log ID</param>
    ''' <param name="p_dtmChangeDateTime">Change date time</param>
    ''' <param name="p_strApprovalStatus">Approved status</param>
    ''' <param name="p_strApprover">Approver</param>
    ''' <returns></returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function UpdateAuditLogEntry(ByVal p_intChangeLogID As Integer,
                                        ByVal p_dtmChangeDateTime As DateTime,
                                        ByVal p_strApprovalStatus As String,
                                        ByVal p_strApprover As String) As Boolean Implements IDepository.UpdateAuditLogEntry
        Dim blnSaved As Boolean = False
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                blnSaved = objCertDalc.UpdateAuditLogEntry(p_intChangeLogID, p_dtmChangeDateTime, p_strApprovalStatus, p_strApprover)
            End Using
        Catch
            Throw
        End Try
        Return blnSaved

    End Function

    ''' <summary>
    ''' Pass the call to respective data source to retrieve product info
    ''' </summary>
    ''' <param name="p_strMatlNum">Material Number.</param>
    ''' <returns>Returns data set containing product info.</returns>
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
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas.S</term>
    ''' <description>
    ''' <para>10/11/2019</para>
    ''' <para>Modify code to fetch product data through procedure.</para>
    ''' </description>
    ''' </item>    
    ''' </list>
    ''' </remarks>
    Public Function GetProductData_SKUTRACS(ByVal p_strMatlNum As String) As ICS.Datasets.SKUtoICSDataset Implements IDepository.GetProductData_SKUTRACS

        Dim objSKUtoICSDataset As ICS.Datasets.SKUtoICSDataset = Nothing
        Dim blnSuccess As Boolean = False

        Using objCertificationDalc As CertificationDalc = New CertificationDalc
            objSKUtoICSDataset = New ICS.Datasets.SKUtoICSDataset
            objSKUtoICSDataset = objCertificationDalc.GetProductInfo(p_strMatlNum, blnSuccess)
        End Using
        Return objSKUtoICSDataset

    End Function

    ''' <summary>
    ''' Get TRACS DATA.
    ''' </summary>
    '''  <param name="p_intCertType">certificate type</param>
    ''' <param name="p_intTireType">Tire type</param>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <returns>Data</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetTRACSData(ByVal p_intCertType As Integer, ByVal p_intTireType As Integer, ByVal p_strMatlNum As String, ByVal intManufacturingLocationId As Integer) As ICS.Datasets.TRACStoICSDataset Implements IDepository.GetTRACSData
        'Dim dstTRACStoICSDataset As New ICS.Datasets.TRACStoICSDataset

        Dim objTRACStoICSDataset As ICS.Datasets.TRACStoICSDataset = Nothing
        Dim objTRACSSupportBus As Business = Nothing
        Dim blnSuccess As Boolean = False        'Added Properties as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation.
        Dim strUseSap As String = String.Empty
        Dim strUseTracs As String = String.Empty

        Try
            objTRACSSupportBus = New Business
            strUseSap = CooperTire.ICS.Common.AppSettingsAid.GetUseSAP()
            strUseTracs = CooperTire.ICS.Common.AppSettingsAid.GetUseTracs()
            objTRACStoICSDataset = objTRACSSupportBus.GetTRACSData(p_intCertType, p_intTireType, p_strMatlNum, intManufacturingLocationId, strUseSap, strUseTracs, blnSuccess)
        Catch
            Throw
        End Try
        Return objTRACStoICSDataset
    End Function

    ''' <summary>
    ''' Pass the call to respective data source
    ''' </summary>
    ''' <param name="p_intCertType">Certificate type</param>
    ''' <param name="p_strInMatlNum">Material Num</param>
    ''' <param name="p_strSimilarMatlNum">Similiar Material Num</param>
    ''' <param name="p_strMessage">Message</param>
    ''' <param name="p_intImarkFamily">Imark family.</param>
    ''' <param name="p_strECEReference">Reference.</param>
    ''' <returns>Results</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function CheckSimilarTire(ByVal p_intCertType As Integer,
                                     ByVal p_strInMatlNum As String,
                                     ByRef p_strSimilarMatlNum As String,
                                     ByRef p_intImarkFamily As Integer,
                                     ByRef p_strECEReference As String,
                                     ByRef p_strMessage As String) As Integer Implements IDepository.CheckSimilarTire

        Dim intResult As Integer = 0
        Try
            Using objCertificationDalc As CertificationDalc = New CertificationDalc
                intResult = objCertificationDalc.CheckSimilarTire(p_intCertType, p_strInMatlNum, p_strSimilarMatlNum, p_intImarkFamily, p_strECEReference, p_strMessage)
            End Using
        Catch
            Throw
        End Try
        Return intResult

    End Function

    ''' <summary>
    ''' Get Default values
    ''' </summary>
    ''' <param name="p_strCertificateType">Certificate type</param>
    ''' <param name="p_intCertificateNumberID">Certificate Number Key.</param>
    ''' <param name="p_strCertificateNumber">Certificate Number.</param>
    ''' <returns>Results</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDefaultValues(ByVal p_strCertificateType As String,
                                     ByVal p_intCertificateNumberID As Integer,
                                     ByRef p_strCertificateNumber As String) As System.Data.DataSet Implements IDepository.GetDefaultValues

        Dim dsResults As DataSet = Nothing
        Try
            Using objDefaultValuesDalc As DefaultValuesDalc = New DefaultValuesDalc
                dsResults = New DataSet
                dsResults = objDefaultValuesDalc.GetDefaultValues(p_strCertificateType, p_intCertificateNumberID, p_strCertificateNumber)
            End Using
        Catch
            Throw
        End Try
        Return dsResults

    End Function

    ''' <summary>
    ''' Get certificate default values.
    ''' </summary>
    ''' <param name="p_certDeftValues">Certificate values.</param>
    ''' <returns>Results</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function CertificationTypeDefaultValueSave(ByVal p_certDeftValues As List(Of CertificationDefaultField)) As Boolean Implements IDepository.CertificateDefaultvalueSave

        Dim blnSaved As Boolean = False
        Try
            Using objDefaultValuesDalc As DefaultValuesDalc = New DefaultValuesDalc
                blnSaved = objDefaultValuesDalc.CertificationTypeDefaultValueSave(p_certDeftValues)
            End Using
        Catch
            Throw
        End Try
        Return blnSaved

    End Function

    ''' <summary>
    ''' Save certificate default values.
    ''' </summary>
    ''' <param name="p_certDeftValues">Certificate values.</param>
    ''' <returns>Results</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function CertificateDefaultValueSave(ByVal p_certDeftValues As List(Of CertificationDefaultField), ByVal p_certificateNo As String) As Boolean Implements IDepository.CertificateValueSave

        Dim blnSaved As Boolean = False
        Try
            Using objDefaultValuesDalc As DefaultValuesDalc = New DefaultValuesDalc
                blnSaved = objDefaultValuesDalc.CertificateDefaultValueSave(p_certDeftValues, p_certificateNo)
            End Using
        Catch
            Throw
        End Try
        Return blnSaved

    End Function

    ''' <summary>
    ''' Gets the data for emark passenger report.
    ''' </summary>
    ''' <param name="p_strCertificateNumber">The certificate number.</param>
    ''' <param name="p_intCertificationTypeId">The certification type id.</param>
    ''' <param name="p_strExtension">The extension.</param>
    ''' <param name="p_intTireTypeID">tire type ID.</param>
    ''' <returns>Passenger Report Data</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDataForEmarkPassengerReport(ByVal p_strCertificateNumber As String, _
                                                ByVal p_intCertificationTypeId As Integer, _
                                                ByVal p_strExtension As String, _
                                                ByVal p_intTireTypeID As Integer) As DataSet Implements IDepository.GetDataForEmarkPassengerReport

        Dim dsResults As DataSet = Nothing
        Try
            Using objReportsDalc As ReportsDalc = New ReportsDalc
                dsResults = New DataSet
                dsResults = objReportsDalc.GetDataForEmarkPassengerReport(p_strCertificateNumber, p_intCertificationTypeId, p_strExtension, p_intTireTypeID)
            End Using
        Catch
            Throw
        End Try
        Return dsResults

    End Function

    ''' <summary>
    ''' Get EmarkE117Report.
    ''' </summary>
    ''' <param name="p_strCertificateNumber">The certificate number.</param>
    ''' <param name="p_intCertificationTypeId">The certification type id.</param>
    ''' <param name="p_strExtension">The extension.</param>
    ''' <param name="p_intTireTypeID">tire type ID.</param>
    ''' <returns>E117Report Data</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDataForEmarkE117Report(ByVal p_strCertificateNumber As String, _
                                        ByVal p_intCertificationTypeId As Integer, _
                                        ByVal p_strExtension As String, _
                                        ByVal p_intTireTypeID As Integer) As DataSet Implements IDepository.GetDataForEmarkE117Report

        Dim dsResults As New DataSet
        Try

            Using objReportsDalc As ReportsDalc = New ReportsDalc
                dsResults = objReportsDalc.GetDataForEmarkE117Report(p_strCertificateNumber, p_intCertificationTypeId, p_strExtension, p_intTireTypeID)
            End Using
        Catch
            Throw
        End Try
        Return dsResults

    End Function

    ''' <summary>
    ''' Get EmarkReportWithTR Data
    ''' </summary>
    ''' <param name="p_strCertificateNumber">Certificate number.</param>
    ''' <param name="p_intTireTypeID">tire type ID.</param>
    ''' <returns>EmarkReportWithTR Data</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDataForEmarkReportWithTR(ByVal p_strCertificateNumber As String, _
                                                ByVal p_intTireTypeID As Integer) As DataSet Implements IDepository.GetDataForEmarkReportWithTR

        Dim dsResults As New DataSet
        Try
            Using objReportsDalc As ReportsDalc = New ReportsDalc
                dsResults = objReportsDalc.GetDataForEmarkWithTR(p_strCertificateNumber, CShort(p_intTireTypeID))
            End Using
        Catch
            Throw
        End Try
        Return dsResults

    End Function

    ''' <summary>
    ''' Get CCC Sequential Report data
    ''' </summary>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <param name="p_intCertificationTypeId">Certificate Type ID</param>
    ''' <param name="p_strExtension">Extension</param>
    ''' <returns></returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDataForCCCReport(ByVal p_strCertificateNumber As String, _
                                        ByVal p_intCertificationTypeId As Integer, _
                                        ByVal p_strExtension As String) As CCCSequentialDataSet Implements IDepository.GetDataForCCCReport

        Dim dsResults As New CCCSequentialDataSet
        Try
            Using objReportsDalc As ReportsDalc = New ReportsDalc
                dsResults = objReportsDalc.GetDataForCCCReport(p_strCertificateNumber, p_intCertificationTypeId, p_strExtension)
            End Using
        Catch
            Throw
        End Try
        Return dsResults

    End Function

    ''' <summary>
    ''' Get CCC Product Description Report data
    ''' </summary>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <param name="p_intCertificationTypeId">Certificate Type ID</param>
    ''' <param name="p_strExtension">Extension</param>
    ''' <returns>CCC Report data</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDataForCCCProductDescriptionReport(ByVal p_strCertificateNumber As String, _
                                        ByVal p_intCertificationTypeId As Integer, _
                                        ByVal p_strExtension As String) As CCCProductDescriptionDataSet Implements IDepository.GetDataForCCCProductDescriptionReport

        Dim dsResults As New CCCProductDescriptionDataSet
        Try
            Using objReportsDalc As ReportsDalc = New ReportsDalc
                dsResults = objReportsDalc.GetDataForCCCProductDescriptionReport(p_strCertificateNumber, p_intCertificationTypeId, p_strExtension)
            End Using
        Catch
            Throw
        End Try
        Return dsResults

    End Function

    ''' <summary>
    ''' Gets the data for GSO passenger report.
    ''' </summary>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <param name="p_intCertificationTypeId">Certificate Type ID</param>
    ''' <param name="p_strExtension">Extension</param>
    ''' <param name="p_intTireTypeId">Type type ID</param>
    ''' <returns>GSO passenger report data</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDataForGSOPassengerReport(ByVal p_strCertificateNumber As String, _
                                            ByVal p_intCertificationTypeId As Integer, _
                                            ByVal p_strExtension As String, _
                                            ByVal p_intTireTypeId As Integer) As GSOCertificateDataSet Implements IDepository.GetDataForGSOPassengerReport

        Dim dsResults As GSOCertificateDataSet = Nothing
        Try
            Using objReportsDalc As ReportsDalc = New ReportsDalc
                dsResults = New GSOCertificateDataSet
                dsResults = objReportsDalc.GetDataForGSOPassengerReport(p_strCertificateNumber, p_intCertificationTypeId, p_strExtension, p_intTireTypeId)
            End Using
        Catch
            Throw
        End Try
        Return dsResults

    End Function

    ''' <summary>
    '''  Get GSO Conformity Report Data.
    ''' </summary>
    ''' <param name="p_strBatchNumber">Batch number.</param>
    ''' <param name="p_intCertificationTypeId">Certification type ID.</param>
    ''' <param name="p_intTireTypeId">Tire Type ID.</param>
    ''' <returns>GSO Conformity Report Data.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDataForGSOConformityReport(ByVal p_strBatchNumber As String, _
                                            ByVal p_intCertificationTypeId As Integer, _
                                            ByVal p_intTireTypeId As Integer) As DataSet Implements IDepository.GetDataForGSOConformityReport

        Dim dstResults As New DataSet
        Try
            Using ctdReportsDalc As ReportsDalc = New ReportsDalc
                dstResults = ctdReportsDalc.GetDataForGSOConformityReport(p_strBatchNumber, p_intCertificationTypeId, p_intTireTypeId)
            End Using
        Catch
            Throw
        End Try
        Return dstResults

    End Function

    ''' <summary>
    ''' Gets the data for Imark report.
    ''' </summary>
    ''' <returns>Imark report data.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDataForImarkConformityReport() As DataSet Implements IDepository.GetDataForImarkConformityReport

        Dim dstResults As New DataSet
        Try
            Using ctdReportsDalc As ReportsDalc = New ReportsDalc
                dstResults = ctdReportsDalc.GetDataForIMARKConformityReport()
            End Using
        Catch
            Throw
        End Try
        Return dstResults

    End Function

    ''' <summary>
    ''' Get Imark Sampling Report data.
    ''' </summary>
    ''' <param name="p_strMatlNum">Material Number.</param>
    ''' <returns>Imark Sampling Report data.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDataForImarkSamplingTireTestsReport(ByVal p_strMatlNum As String) As DataSet Implements IDepository.GetDataForImarkSamplingTireTestsReport

        Dim dsResults As New DataSet
        Try
            Using objRptDal As ReportsDalc = New ReportsDalc
                dsResults = objRptDal.GetDataForImarkSamplingTireTestsReport(p_strMatlNum)
            End Using
        Catch
            Throw
        End Try
        Return dsResults

    End Function

    ' Changed as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDataForSKUCertification(ByVal p_strMatlNum As String,
                                               ByVal p_strBrand As String,
                                               ByVal p_strBrandLine As String,
                                               ByVal p_strCertType As String) As CertificateReport Implements IDepository.GetDataForSKUCertification

        Dim objResults As New CertificateReport
        Try
            Using ctdReportsDalc As ReportsDalc = New ReportsDalc
                objResults = ctdReportsDalc.GetSKUCertificateReportInfo(p_strMatlNum, p_strBrand, p_strBrandLine, p_strCertType)
            End Using
        Catch
            Throw
        End Try
        Return objResults

    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDataForImarkCertification(ByVal p_dtDateParam As DateTime) As ImarkCertificationDataSet Implements IDepository.GetDataForImarkCertification

        Dim dstResults As New ImarkCertificationDataSet
        Try

            Using ctdReportsDalc As ReportsDalc = New ReportsDalc
                dstResults = ctdReportsDalc.GetDataForImarkCertification(p_dtDateParam)
            End Using
        Catch
            Throw
        End Try
        Return dstResults

    End Function

    'JBH_2.00 Project 5325 - App Mold Change Required and Operations Approval Date Parameters
    'JESEITZ 10/292016 - REQ 203625 AddInfo field

    ''' <summary>
    ''' Get audit logs after the given date.
    ''' </summary>
    ''' <param name="p_iSKUId">SKU Id</param>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <param name="p_blnRemoveMatlNum">Remove Material Number</param>
    ''' <param name="p_strCertificationTypeName">CertificationTypeName</param>
    ''' <param name="p_strCERTIFICATENUMBER">CERTIFICATENUMBER</param>
    ''' <param name="p_dteCertDateSubmitted">CertDateSubmitted</param>
    ''' <param name="p_dteCertDateApproved_CEGI">CertDateApproved</param>
    ''' <param name="p_dteDATESUBMITED">DATESUBMITED</param>
    ''' <param name="pc_ACTIVESTATUS">ACTIVESTATUS</param>
    ''' <param name="p_dteDATEASSIGNED_EGI">DATEASSIGNED</param>
    ''' <param name="p_dteDATEAPROVED_CEGI">DATEAPROVED</param>
    ''' <param name="pc_RENEWALREQUIRED_CGIN">RENEWALREQUIRED</param>
    ''' <param name="p_strJOBREPORTNUMBER_CEN">JOBREPORTNUMBER</param>
    ''' <param name="p_strEXTENSION_EN">EXTENSION</param>
    ''' <param name="p_strSUPPLEMENTALMOLDSTAMPING_E">SUPPLEMENTALMOLDSTAMPING</param>
    ''' <param name="p_strEMARKREFERENCE_I">EMARKREFERENCE</param>
    ''' <param name="p_dteEXPIRYDATE_I">EXPIRYDATE</param>
    ''' <param name="p_strFAMILY_I">FAMILY</param>
    ''' <param name="p_strPRODUCTLOCATION">PRODUCTLOCATION</param>
    ''' <param name="p_strCOUNTRYOFMANUFACTURE_N">COUNTRYOFMANUFACTURE</param>
    ''' <param name="p_blnAddNewCustomer">AddNewCustomer</param>
    ''' <param name="p_strActSigReq">ActSigReq</param>
    ''' <param name="p_intCustomerId">CustomerId</param>
    ''' <param name="p_strCUSTOMER_N">CUSTOMER</param>
    ''' <param name="p_strCustomerAddress">CustomerAddress</param>
    ''' <param name="p_strCUSTOMERSPECIFIC_N">CUSTOMERSPECIFIC</param>
    ''' <param name="p_blnAddNewImporter">AddNewImporter</param>
    ''' <param name="p_intImporterId">ImporterId</param>
    ''' <param name="p_strImporter">Importer</param>
    ''' <param name="p_strImporterAddress">ImporterAddress</param>
    ''' <param name="p_strImporterRepresentative">ImporterRepresentative</param>
    ''' <param name="p_strCOUNTRYLOCATION_N">COUNTRYLOCATION</param>
    ''' <param name="p_strBATCHNUMBER_G">BATCHNUMBER</param>
    ''' <param name="p_dteSUPPLEMENTALASSIGNED">SUPPLEMENTALASSIGNED</param>
    ''' <param name="p_dteSUPPLEMENTALSUBMITTED">SUPPLEMENTALSUBMITTED</param>
    ''' <param name="p_dteSUPPLEMENTALAPPROVED">UPPLEMENTALAPPROVED</param>
    ''' <param name="p_strCompanyName">CompanyName</param>
    ''' <param name="p_strUserName">UserName</param>
    ''' <param name="p_intCertificateNumberID">CertificateNumberID</param>
    ''' <param name="p_strFamilyDesc">FamilyDesc</param>
    ''' <param name="p_blnMoldChgRequired">MoldChgRequired</param>
    ''' <param name="p_dteOperDateApproved">Date Approved</param>
    ''' <param name="p_strAddInfo">Add Info</param>
    ''' <returns>Audit logs.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function SaveCertificate(ByVal p_iSKUId As Integer,
                                    ByVal p_strMatlNum As String,
                                    ByVal p_blnRemoveMatlNum As Boolean,
                                    ByVal p_strCertificationTypeName As String,
                                    ByVal p_strCERTIFICATENUMBER As String,
                                    ByVal p_dteCertDateSubmitted As Date,
                                    ByVal p_dteCertDateApproved_CEGI As Date,
                                    ByVal p_dteDATESUBMITED As Date,
                                    ByVal pc_ACTIVESTATUS As String,
                                    ByVal p_dteDATEASSIGNED_EGI As Date,
                                    ByVal p_dteDATEAPROVED_CEGI As Date,
                                    ByVal pc_RENEWALREQUIRED_CGIN As Char,
                                    ByVal p_strJOBREPORTNUMBER_CEN As String,
                                    ByVal p_strEXTENSION_EN As String,
                                    ByVal p_strSUPPLEMENTALMOLDSTAMPING_E As String,
                                    ByVal p_strEMARKREFERENCE_I As String,
                                    ByVal p_dteEXPIRYDATE_I As Date,
                                    ByVal p_strFAMILY_I As String,
                                    ByVal p_strPRODUCTLOCATION As String,
                                    ByVal p_strCOUNTRYOFMANUFACTURE_N As String,
                                    ByVal p_blnAddNewCustomer As Boolean,
                                    ByVal p_strActSigReq As String,
                                    ByVal p_intCustomerId As Integer,
                                    ByVal p_strCUSTOMER_N As String,
                                    ByVal p_strCustomerAddress As String,
                                    ByVal p_strCUSTOMERSPECIFIC_N As String,
                                    ByVal p_blnAddNewImporter As Boolean,
                                    ByVal p_intImporterId As Integer,
                                    ByVal p_strImporter As String,
                                    ByVal p_strImporterAddress As String,
                                    ByVal p_strImporterRepresentative As String,
                                    ByVal p_strCOUNTRYLOCATION_N As String,
                                    ByVal p_strBATCHNUMBER_G As String,
                                    ByVal p_dteSUPPLEMENTALASSIGNED As Date,
                                    ByVal p_dteSUPPLEMENTALSUBMITTED As Date,
                                    ByVal p_dteSUPPLEMENTALAPPROVED As Date,
                                    ByVal p_strCompanyName As String,
                                    ByVal p_strUserName As String,
                                    ByRef p_intCertificateNumberID As Integer,
                                    ByVal p_strFamilyDesc As String,
                                    ByVal p_blnMoldChgRequired As Boolean,
                                    ByVal p_dteOperDateApproved As DateTime,
                                    ByVal p_strAddInfo As String) As Common.NameAid.SaveResult Implements IDepository.SaveCertificate
        Dim enuSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Try

            Using objCertDalc As CertificationDalc = New CertificationDalc
                enuSaveResult = objCertDalc.SaveCertificate(p_iSKUId, p_strMatlNum, p_blnRemoveMatlNum, p_strCertificationTypeName, p_strCERTIFICATENUMBER, p_dteCertDateSubmitted, p_dteCertDateApproved_CEGI, p_dteDATESUBMITED, pc_ACTIVESTATUS, p_dteDATEASSIGNED_EGI, p_dteDATEAPROVED_CEGI, pc_RENEWALREQUIRED_CGIN, p_strJOBREPORTNUMBER_CEN, p_strEXTENSION_EN, p_strSUPPLEMENTALMOLDSTAMPING_E, p_strEMARKREFERENCE_I, p_dteEXPIRYDATE_I, p_strFAMILY_I, p_strPRODUCTLOCATION, p_strCOUNTRYOFMANUFACTURE_N, p_blnAddNewCustomer, p_strActSigReq, p_intCustomerId, p_strCUSTOMER_N, p_strCustomerAddress, p_strCUSTOMERSPECIFIC_N, p_blnAddNewImporter, p_intImporterId, p_strImporter, p_strImporterAddress, p_strImporterRepresentative, p_strCOUNTRYLOCATION_N, p_strBATCHNUMBER_G, p_dteSUPPLEMENTALASSIGNED, p_dteSUPPLEMENTALSUBMITTED, p_dteSUPPLEMENTALAPPROVED, p_strCompanyName, p_strUserName, p_intCertificateNumberID, p_strFamilyDesc, p_blnMoldChgRequired, p_dteOperDateApproved, p_strAddInfo)
            End Using
        Catch
            Throw
        End Try
        Return enuSaveResult

    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function BatchNumMassUpdate(ByVal p_strCertifName As String, ByVal p_strTempBatchNum As String, ByVal p_strGSOBatchNum As String, ByVal p_strUserName As String) As Common.NameAid.SaveResult Implements IDepository.BatchNumMassUpdate

        Dim enuSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                enuSaveResult = objCertDalc.BatchNumMassUpdate(p_strCertifName, p_strTempBatchNum, p_strGSOBatchNum, p_strUserName)
            End Using
        Catch
            Throw
        End Try
        Return enuSaveResult

    End Function

    ' Changed as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Gets the data for Emark report.
    ''' </summary>
    ''' <param name="p_strCertificationNo">Date change time.</param>
    ''' <param name="p_strBrand">Date change time.</param>
    ''' <param name="p_strBrandLine">Brand Line</param>
    ''' <returns>Audit logs.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDataForEmarkCertificationReport(ByVal p_strCertificationNo As String,
                                                       ByVal p_strBrand As String, ByVal p_strBrandLine As String) As DataSet Implements IDepository.GetDataForEmarkCertificationReport
        Dim dstResults As New DataSet
        Try
            Using objRptDal As ReportsDalc = New ReportsDalc
                dstResults = objRptDal.GetDataForEmarkCertificationReport(p_strCertificationNo, p_strBrand, p_strBrandLine)
            End Using
        Catch
            Throw
        End Try
        Return dstResults
    End Function

    ''' <summary>
    ''' Get traceability report info.
    ''' </summary>
    ''' <param name="p_strCertificateNumber"></param>
    ''' <param name="p_intCertificationTypeID"></param>
    ''' <param name="p_strIncludeArchived">Include Archived certificates.</param>
    ''' <returns></returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetTraceabilityReportInfo(ByVal p_strCertificateNumber As String,
                                              ByVal p_intCertificationTypeID As Integer,
                                              ByVal p_strIncludeArchived As String) As Traceability Implements IDepository.GetTraceabilityReportInfo
        Dim objResults As New Traceability
        Try

            Using objRptDal As ReportsDalc = New ReportsDalc
                objResults = objRptDal.GetTraceabilityReportInfo(p_strCertificateNumber, p_intCertificationTypeID, p_strIncludeArchived)
            End Using
        Catch
            Throw
        End Try
        Return objResults

    End Function

    ''' <summary>
    ''' Get Exception report info data.
    ''' </summary>
    ''' <returns>Audit logs.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetExceptionReportInfo() As ExceptionReport_DataSet Implements IDepository.GetExceptionReportInfo

        Dim dstResults As New ExceptionReport_DataSet
        Try

            Using objRptDal As ReportsDalc = New ReportsDalc
                dstResults = objRptDal.GetExceptionReportInfo()
            End Using
        Catch
            Throw
        End Try
        Return dstResults

    End Function

    ''' <summary>
    '''  Get Emark Application report info data.
    ''' </summary>
    ''' <param name="p_strCertificateNumber">Date change time.</param>
    ''' <param name="p_intTireTypeID">Date change time.</param>
    ''' <returns>Audit logs.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetEmarkApplication(ByVal p_strCertificateNumber As String, _
                                         ByVal p_intTireTypeID As Integer) As DataSet Implements IDepository.GetEmarkApplication

        Dim dstResults As New DataSet
        Try

            Using objRptDal As ReportsDalc = New ReportsDalc
                dstResults = objRptDal.GetEmarkApplication(p_strCertificateNumber, p_intTireTypeID)
            End Using
        Catch
            Throw
        End Try
        Return dstResults

    End Function

    ''' <summary>
    ''' Get Nom Certification report info data.
    ''' </summary>
    ''' <param name="p_strCertificateNumber">Date change time.</param>
    ''' <param name="p_intTireTypeID">Date change time.</param>
    ''' <returns>Audit logs.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetNomCertification(ByVal p_strCertificateNumber As String,
                                        ByVal p_intTireTypeID As Integer) As DataSet Implements IDepository.GetNomCertification

        Dim dstResults As New DataSet
        Try

            Using objRptDal As ReportsDalc = New ReportsDalc
                dstResults = objRptDal.GetNomCertification(p_strCertificateNumber, p_intTireTypeID)
            End Using
        Catch
            Throw
        End Try
        Return dstResults

    End Function

    ''' <summary>
    ''' Get Imark Authenticity report info data.
    ''' </summary>
    ''' <returns>Audit logs.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetAuthenticity() As DataSet Implements IDepository.GetAuthenticityReport

        Dim dstResults As New DataSet
        Try

            Using objRptDal As ReportsDalc = New ReportsDalc
                dstResults = objRptDal.GetAuthenticityReport
            End Using
        Catch
            Throw
        End Try
        Return dstResults

    End Function

    ''' <summary>
    ''' Get EmarkSimilarCertificateSearchReport data.
    ''' </summary>
    ''' <param name="p_MatlNum">Date change time.</param>
    ''' <returns>Audit logs.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetEmarkSimilarCertificateSearchReport(ByVal p_MatlNum As String) As DataSet Implements IDepository.GetEmarkSimilarCertificateSearchReport

        Dim dstResults As New DataSet
        Try

            Using objRptDal As ReportsDalc = New ReportsDalc
                dstResults = objRptDal.GetEmarkSimilarCertificateSearchReport(p_MatlNum)
            End Using
        Catch
            Throw
        End Try
        Return dstResults

    End Function

    ''' 
    ''' 
    ''' <summary>
    ''' Get Certification Renewal report info data.
    ''' </summary>
    ''' <param name="p_strCertificateNumber">Date change time.</param>
    ''' <param name="p_intCertificationTypeID">Date change time.</param>
    ''' <returns>Audit logs.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetCertificationRenewal(ByVal p_strCertificateNumber As String,
                                            ByVal p_intCertificationTypeID As Integer) As DataSet Implements IDepository.GetCertificationRenewal

        Dim dstResults As New DataSet
        Try

            Using objRptDal As ReportsDalc = New ReportsDalc
                dstResults = objRptDal.GetCertificationRenewal(p_strCertificateNumber, p_intCertificationTypeID)
            End Using
        Catch
            Throw
        End Try
        Return dstResults

    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function SaveAuditLogEntry(ByVal p_dteChangeDateTime As Date,
                                      ByVal m_strChangedBy As String,
                                      ByVal m_strArea As String,
                                      ByVal m_strChangedFieldElement As String,
                                      ByVal m_strOldValue As String,
                                      ByVal m_strNewValue As String,
                                      ByVal m_intReasonID As Integer,
                                      ByVal m_strNote As String) As Boolean Implements IDepository.SaveAuditLogEntry

        Dim blnSaved As Boolean = False
        Try

            Using objCertDalc As CertificationDalc = New CertificationDalc
                blnSaved = objCertDalc.SaveAuditLogEntry(p_dteChangeDateTime, m_strChangedBy, m_strArea, m_strChangedFieldElement, m_strOldValue, m_strNewValue, m_intReasonID, m_strNote)
            End Using
        Catch
            Throw
        End Try
        Return blnSaved

    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Function GetProductCertStatus(ByVal p_strBrand As String,
                                  ByVal p_strBrandLine As String) As DataSet Implements IDepository.GetProductCertStatus
        Dim dstResults As New DataSet
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                dstResults = objCertDalc.GetProductCertStatus(p_strBrand, p_strBrandLine)
            End Using
        Catch
            Throw
        End Try
        Return dstResults

    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetRegionCertStatus(ByVal p_strBrand As String,
                                        ByVal p_strBrandLine As String) As DataSet Implements IDepository.GetRegionCertStatus

        Dim dstResults As New DataSet
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                dstResults = objCertDalc.GetRegionCertStatus(p_strBrand, p_strBrandLine)
            End Using
        Catch
            Throw
        End Try
        Return dstResults

    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetCountries(ByVal p_strRegionName As String) As DataTable Implements IDepository.GetCountries

        Dim dstResults As New DataSet
        Try

            Using objCertDalc As CertificationDalc = New CertificationDalc
                dstResults = objCertDalc.GetCountries(p_strRegionName)
            End Using
        Catch
            Throw
        End Try
        Return dstResults.Tables(0)

    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetImporters() As DataTable Implements IDepository.GetImporters

        Dim dstResults As New DataSet
        Try

            Using objCertDalc As CertificationDalc = New CertificationDalc
                dstResults = objCertDalc.GetImporters()
            End Using
        Catch
            Throw
        End Try
        Return dstResults.Tables(0)

    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetCustomers() As DataTable Implements IDepository.GetCustomers

        Dim dstResults As New DataSet
        Try

            Using objCertDalc As CertificationDalc = New CertificationDalc
                dstResults = objCertDalc.GetCustomers()
            End Using
        Catch
            Throw
        End Try
        Return dstResults.Tables(0)

    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function SaveCertificationRequest(ByVal p_blnDeleteMe As Boolean, ByVal p_strMatlNum As String, ByVal p_intCountryID As Integer, ByVal p_intSKUID As Integer) As Boolean Implements IDepository.SaveCertificationRequest
        ' old marketing screen
        Dim blnDone As Boolean
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                blnDone = objCertDalc.SaveCertificationRequest(p_blnDeleteMe, p_strMatlNum, p_intCountryID, p_intSKUID)
            End Using
        Catch
            Throw
        End Try
        Return blnDone

    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function SaveCertificationGroup(ByVal p_blnDeleteMe As Boolean, ByVal p_strMatlNum As String, ByVal p_intCertificationID As Integer, ByVal p_intSKUID As Integer) As Boolean Implements IDepository.SaveCertificationGroup
        ' old marketing screen
        Dim blnDone As Boolean
        Try

            Using objCertDalc As CertificationDalc = New CertificationDalc
                blnDone = objCertDalc.SaveCertificationGroup(p_blnDeleteMe, p_strMatlNum, p_intCertificationID, p_intSKUID)
            End Using
        Catch
            Throw
        End Try
        Return blnDone

    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function SaveRequestCert(ByVal p_blnDeleteMe As Boolean, ByVal p_strMatlNum As String, ByVal p_intCertificationID As Integer, ByVal p_intSKUID As Integer) As Boolean Implements IDepository.SaveRequestCert
        ' jeseitz 6/2/16 - added for MarketingNew screen
        Dim blnDone As Boolean
        Try

            Using objCertDalc As CertificationDalc = New CertificationDalc
                blnDone = objCertDalc.SaveRequestCert(p_blnDeleteMe, p_strMatlNum, p_intCertificationID, p_intSKUID)
            End Using
        Catch
            Throw
        End Try
        Return blnDone

    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function CheckMatlNumExists(ByVal p_strMatlNum As String) As Boolean Implements IDepository.CheckMatlNumExists

        Dim blnExists As Boolean
        Try

            Using objCertDalc As CertificationDalc = New CertificationDalc
                blnExists = objCertDalc.CheckIfMatlNumExists(p_strMatlNum)
            End Using
        Catch
            Throw
        End Try
        Return blnExists

    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function CheckIfCertificateNumberExists(ByVal p_strCertNum As String) As Boolean Implements IDepository.CheckIfCertificateNumberExists

        Dim blnExists As Boolean
        Try

            Using objCertDalc As CertificationDalc = New CertificationDalc
                blnExists = objCertDalc.CheckIfCertificateNumberExists(p_strCertNum)
            End Using
        Catch
            Throw
        End Try
        Return blnExists

    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function RenewCertificate(ByVal p_intCertificateId As Integer, ByRef p_intNewCertificateID As Integer, ByVal p_strUserName As String) As NameAid.SaveResult Implements IDepository.RenewCertificate

        Dim enumSaveResult As NameAid.SaveResult
        Try

            Using objCertDalc As CertificationDalc = New CertificationDalc
                enumSaveResult = objCertDalc.RenewCertificate(p_intCertificateId, p_intNewCertificateID, p_strUserName)
            End Using
        Catch
            Throw
        End Try
        Return enumSaveResult

    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetLatestImarkCertifId() As Integer Implements IDepository.GetLatestImarkCertifId

        Dim strImarkCertId As Integer
        Try

            Using objCertDalc As CertificationDalc = New CertificationDalc
                strImarkCertId = objCertDalc.GetLatestImarkCertifId()
            End Using
        Catch
            Throw
        End Try
        Return strImarkCertId

    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetCertifExtension(ByVal p_intImarkCertId As Integer) As String Implements IDepository.GetCertifExtension

        Dim strImarkCertExtension As String = String.Empty
        Try

            Using objCertDalc As CertificationDalc = New CertificationDalc
                strImarkCertExtension = objCertDalc.GetCertifExtension(p_intImarkCertId)
            End Using
        Catch
            Throw
        End Try
        Return strImarkCertExtension

    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetLatestGSOCertifNumber() As String Implements IDepository.GetLatestGSOCertifNumber

        Dim strGSOTempCertNumber As String = String.Empty
        Try

            Using objCertDalc As CertificationDalc = New CertificationDalc
                strGSOTempCertNumber = objCertDalc.GetLatestGSOCertifNumber()
            End Using
        Catch
            Throw
        End Try
        Return strGSOTempCertNumber

    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function SaveNewCertificate(ByVal p_strCertNum As String, ByVal p_intCertTypeId As Integer, ByVal p_strMatlNum As String, ByVal p_strImporter As String, ByVal p_strCustomer As String, ByVal p_strUserName As String, ByVal p_strExtension As String) As Boolean Implements IDepository.SaveNewCertificate

        Dim blnDone As Boolean
        Try

            Using objCertDalc As CertificationDalc = New CertificationDalc
                blnDone = objCertDalc.SaveNewCertificate(p_strCertNum, p_intCertTypeId, p_strMatlNum, p_strImporter, p_strCustomer, p_strUserName, p_strExtension)
            End Using
        Catch
            Throw
        End Try
        Return blnDone

    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function SaveNewCertificate(ByVal p_strCertNum As String, ByVal p_intCertTypeId As Integer, ByVal p_strMatlNum As String, ByVal p_strImporter As String, ByVal p_strCustomer As String, ByVal p_strUserName As String, ByVal p_strExtension As String, ByVal p_InsertPC As String, ByRef p_ErrorDesc As String) As Integer Implements IDepository.SaveNewCertificate

        Dim resultNum As Integer
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                resultNum = objCertDalc.SaveNewCertificate(p_strCertNum, p_intCertTypeId, p_strMatlNum, p_strImporter, p_strCustomer, p_strUserName, p_strExtension, p_InsertPC, p_ErrorDesc)
            End Using
        Catch
            Throw
        End Try
        Return resultNum

    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function ArchiveCertification(ByVal p_strCertNum As String, ByVal p_strUserName As String) As Boolean Implements IDepository.ArchiveCertification

        Dim blnDone As Boolean
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                blnDone = objCertDalc.ArchiveCertification(p_strCertNum, p_strUserName)
            End Using
        Catch
            Throw
        End Try
        Return blnDone

    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetCertifications() As DataSet Implements IDepository.GetCertifications
        Dim dstResults As New DataSet
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                dstResults = objCertDalc.GetCertifications()
            End Using
        Catch
            Throw
        End Try
        Return dstResults
    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetSearchTypeResults() As DataSet Implements IDepository.GetSearchTypeResults
        Dim dstResults As New DataSet
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                dstResults = objCertDalc.GetSearchTypeResults()
            End Using
        Catch
            Throw
        End Try
        Return dstResults
    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetManufacturingLocationsResults(ByVal p_strSize As String) As DataSet Implements IDepository.GetManufacturingLocationsResults
        Dim dstResults As New DataSet
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                dstResults = objCertDalc.GetManufacturingLocationsResults(p_strSize)
            End Using
        Catch
            Throw
        End Try
        Return dstResults
    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetCompanyNameList() As DataSet Implements IDepository.GetCompanyNameList

        Dim dstResults As New DataSet
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                dstResults = objCertDalc.GetCompanyNameList()
            End Using
        Catch
            Throw
        End Try
        Return dstResults
    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetCertificationSearchResults(ByVal p_strSearchCriteria As String, ByVal p_strSearchType As String, ByVal p_strExtensionNo As String, ByVal p_strImarkFamily As String, ByVal ps_BrandLine As String) As DataTable Implements IDepository.GetCertificationSearchResults
        Dim dtbResults As New DataTable
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                dtbResults = objCertDalc.GetCertificationSearchResults(p_strSearchCriteria, p_strSearchType, p_strExtensionNo, p_strImarkFamily, ps_BrandLine)
            End Using
        Catch
            Throw
        End Try
        Return dtbResults
    End Function

    ' Added GetBrands function as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="p_strBrand"></param>
    ''' <returns></returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetBrands(ByVal p_strBrand As String) As DataTable Implements IDepository.GetBrands
        Dim dtbResults As New DataTable
        Try

            Using objCertDalc As CertificationDalc = New CertificationDalc
                dtbResults = objCertDalc.GetBrands(p_strBrand)
            End Using
        Catch
            Throw
        End Try
        Return dtbResults
    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetBrandLines(ByVal p_strBrandLine As String) As DataTable Implements IDepository.GetBrandLines
        Dim dtbResults As New DataTable
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                dtbResults = objCertDalc.GetBrandLines(p_strBrandLine)
            End Using
        Catch
            Throw
        End Try
        Return dtbResults
    End Function

    ''' <summary>
    ''' Handles the passing of call to appropriate data source - ICS or SKUTRACS
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Function GetMaterialAttribs(ByVal p_strMatlNum As String) As DataTable Implements IDepository.GetMaterialAttribs
        Dim dtbResults As New DataTable
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                dtbResults = objCertDalc.GetMaterialAttribs(p_strMatlNum)
            End Using
        Catch
            Throw
        End Try
        Return dtbResults
    End Function

    ''' <summary>
    ''' GetCertificate data from database
    ''' </summary>
    ''' <param name="ps_CertificationNumber">Certificate Number</param>
    ''' <param name="ps_ExtensionNo">Extension Number</param>
    ''' <param name="ps_CertificationTypeID">Certification Type Id</param>
    ''' <param name="p_iSKUID">SKU ID</param>
    ''' <param name="p_blnTRsExist">TRs Exist</param>
    ''' <returns>dtbResults</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetCertificate(ByVal ps_CertificationNumber As String, ByVal ps_ExtensionNo As String, ByVal ps_CertificationTypeID As Integer, ByVal p_iSKUID As Integer, ByRef p_blnTRsExist As Boolean) As DataTable Implements IDepository.GetCertificate
        Dim dtbResults As New DataTable
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                dtbResults = objCertDalc.GetCertificate(ps_CertificationNumber, ps_ExtensionNo, ps_CertificationTypeID, p_iSKUID, p_blnTRsExist)
            End Using
        Catch
            Throw
        End Try
        Return dtbResults
    End Function

    ''' <summary>
    ''' GetCertificate
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetSimilarCertificate(ByVal p_iCertificationTypeID As Integer, ByVal p_strMatlNum As String, ByVal p_strCertificationNumber As String) As DataTable Implements IDepository.GetSimilarCertificate
        Dim dtbResults As New DataTable
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                dtbResults = objCertDalc.GetSimilarCertificate(p_iCertificationTypeID, p_strMatlNum, p_strCertificationNumber)
            End Using
        Catch
            Throw
        End Try
        Return dtbResults
    End Function

    ''' <summary>
    ''' Get Product Data
    ''' </summary>
    ''' <param name="p_strMatlNum">Material number</param>
    ''' <param name="p_intSKUID">SKU id</param>
    ''' <returns></returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetProductData(ByVal p_strMatlNum As String, ByVal p_intSKUID As Integer) As ICSDataSet.ProductDataDataTable Implements IDepository.GetProductData
        Dim objResult As New ICSDataSet.ProductDataDataTable
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                objResult = objCertDalc.GetProductData(p_strMatlNum, p_intSKUID)
            End Using
        Catch
            Throw
        End Try
        Return objResult
    End Function

    ''' <summary>
    ''' Get Certificates By Type
    ''' </summary>
    ''' <param name="p_intCertificationtypeid">Certification type id</param>
    ''' <param name="p_strAll">All</param>
    ''' <returns>Certificates</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetCertificatesByType(ByVal p_intCertificationtypeid As Integer, ByVal p_strAll As String) As DataTable Implements IDepository.GetCertificatesByType
        Dim dtResult As New DataTable
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                dtResult = objCertDalc.GetCertificatesByType(p_intCertificationtypeid, p_strAll)
            End Using
        Catch
            Throw
        End Try
        Return dtResult
    End Function

    ''' <summary>
    ''' Get TestResult Data
    ''' </summary>
    ''' <param name="p_strMatlNum">Material number</param>
    ''' <param name="p_intSKUID">SKU ID</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <param name="p_intCertificateNumberID">Certificate Number ID</param>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTestResultData(ByVal p_strMatlNum As String, ByVal p_intSKUID As Integer, ByVal p_strCertificateNumber As String, ByVal p_intCertificateNumberID As Integer, ByVal p_intCertificationTypeId As Integer) As ICSDataSet Implements IDepository.GetTestResultData
        Dim dsResults As New ICSDataSet
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                dsResults = objCertDalc.GetTestResultData(p_strMatlNum, p_intSKUID, p_strCertificateNumber, p_intCertificateNumberID, p_intCertificationTypeId)
            End Using
        Catch
            Throw
        End Try
        Return dsResults
    End Function

    ''' <summary>
    ''' Save Product Information
    ''' </summary>
    ''' <param name="p_iSKUID"></param>
    ''' <param name="p_strMatlNum"></param>
    ''' <param name="p_strBrand"></param>
    ''' <param name="p_strBrandLine"></param>
    ''' <param name="p_iTireTypeId"></param>
    ''' <param name="p_strPSN"></param>
    ''' <param name="p_strSizeStamp"></param>
    ''' <param name="p_dteDiscontinuedDate"></param>
    ''' <param name="p_strSPECNUMBER"></param>
    ''' <param name="p_strSPEEDRATING"></param>
    ''' <param name="p_strSINGLOADINDEX"></param>
    ''' <param name="p_strDUALLOADINDEX"></param>
    ''' <param name="p_strBELTEDRADIALYN"></param>
    ''' <param name="p_strTUBElESSYN"></param>
    ''' <param name="p_strREINFORCEDYN"></param>
    ''' <param name="p_strEXTRALOADYN"></param>
    ''' <param name="p_strUTQGTREADWEAR"></param>
    ''' <param name="p_strUTQGTRACTION"></param>
    ''' <param name="p_strUTQGTEMP"></param>
    ''' <param name="p_strMUDSNOWYN"></param>
    ''' <param name="p_iRIMDIAMETER"></param>
    ''' <param name="p_dteSerialDate"></param>
    ''' <param name="p_strBrandDesc"></param>
    ''' <param name="p_strMeaRimWidth"></param>
    ''' <param name="p_strLoadRange"></param>
    ''' <param name="p_strRegroovableInd"></param>
    ''' <param name="p_strPlantProduced"></param>
    ''' <param name="p_dteMostRecentTestDate"></param>
    ''' <param name="p_strIMark"></param>
    ''' <param name="p_strInformeNumber"></param>
    ''' <param name="p_dteFechaDate"></param>
    ''' <param name="p_strTreadPattern"></param>
    ''' <param name="p_strSpecialProtectiveBand"></param>
    ''' <param name="p_strNominalTireWidth"></param>
    ''' <param name="p_strAspectRadio"></param>
    ''' <param name="p_strTreadwearIndicators"></param>
    ''' <param name="p_strNameOfManufacturer"></param>
    ''' <param name="p_strFamily"></param>
    ''' <param name="p_strDOTSerialNumber"></param>
    ''' <param name="p_strTPN"></param>
    ''' <param name="p_strUserName"></param>
    ''' <param name="p_strSEVEREWEATHERIND"></param>
    ''' <param name="p_strMFGWWYY"></param>
    ''' <returns>Saved Details</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function Save_Product(ByVal p_iSKUID As Integer, _
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
                                 ByVal p_strMFGWWYY As String) As NameAid.SaveResult Implements IDepository.Save_Product
        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                enumSaveResult = objCertDalc.Save_Product(p_iSKUID, _
                                                          p_strMatlNum, _
                                                          p_strBrand, _
                                                           p_strBrandLine, _
                                                          p_iTireTypeId, _
                                                          p_strPSN, _
                                                          p_strSizeStamp, _
                                                          p_dteDiscontinuedDate, _
                                                          p_strSPECNUMBER, _
                                                          p_strSPEEDRATING, _
                                                          p_strSINGLOADINDEX, _
                                                          p_strDUALLOADINDEX, _
                                                          p_strBELTEDRADIALYN, _
                                                          p_strTUBElESSYN, _
                                                          p_strREINFORCEDYN, _
                                                          p_strEXTRALOADYN, _
                                                          p_strUTQGTREADWEAR, _
                                                          p_strUTQGTRACTION, _
                                                          p_strUTQGTEMP, _
                                                          p_strMUDSNOWYN, _
                                                          p_iRIMDIAMETER, _
                                                          p_dteSerialDate, _
                                                          p_strBrandDesc, _
                                                          p_strMeaRimWidth, _
                                                          p_strLoadRange, _
                                                          p_strRegroovableInd, _
                                                          p_strPlantProduced, _
                                                          p_dteMostRecentTestDate, _
                                                          p_strIMark, _
                                                          p_strInformeNumber, _
                                                          p_dteFechaDate, _
                                                          p_strTreadPattern, _
                                                          p_strSpecialProtectiveBand, _
                                                          p_strNominalTireWidth, _
                                                          p_strAspectRadio, _
                                                          p_strTreadwearIndicators, _
                                                          p_strNameOfManufacturer, _
                                                          p_strFamily, _
                                                          p_strDOTSerialNumber, _
                                                          p_strTPN, _
                                                          p_strUserName, _
                                                          p_strSEVEREWEATHERIND, _
                                                          p_strMFGWWYY)
            End Using
        Catch
            Throw
        End Try
        Return enumSaveResult
    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ''' <summary>
    ''' Save Measurement Details
    ''' </summary>
    ''' <param name="p_strPROJECTNUMBER"></param>
    ''' <param name="p_sngTIRENUMBER"></param>
    ''' <param name="p_strTESTSPEC"></param>
    ''' <param name="p_dteCOMPLETIONDATE"></param>
    ''' <param name="p_sngINFLATIONPRESSURE"></param>
    ''' <param name="p_strMOLDDESIGN"></param>
    ''' <param name="p_sngRIMWIDTH"></param>
    ''' <param name="p_strDOTSERIALNUMBER"></param>
    ''' <param name="p_sngDIAMETER"></param>
    ''' <param name="p_sngAVGSECTIONWIDTH"></param>
    ''' <param name="p_sngAVGOVERALLWIDTH"></param>
    ''' <param name="p_sngMAXOVERALLWIDTH"></param>
    ''' <param name="p_sngSIZEFACTOR"></param>
    ''' <param name="p_dteMOUNTTIME"></param>
    ''' <param name="p_sngMOUNTTEMP"></param>
    ''' <param name="p_intSKUID"></param>
    ''' <param name="p_intCertType"></param>
    ''' <param name="p_strCERTIFICATENUMBER"></param>
    ''' <param name="p_intMEASUREID"></param>
    ''' <param name="p_dteSerialDate"></param>
    ''' <param name="p_dteEndTime"></param>
    ''' <param name="p_sngActSizeFactor"></param>
    ''' <param name="p_srtSTARTINFLATIONPRESSURE"></param>
    ''' <param name="p_srtENDINFLATIONPRESSURE"></param>
    ''' <param name="p_strADJUSTMENT"></param>
    ''' <param name="p_sngCIRCUNFERENCE"></param>
    ''' <param name="p_sngNOMINALDIAMETER"></param>
    ''' <param name="p_sngNOMINALWIDTH"></param>
    ''' <param name="p_strNOMINALWIDTHPASSFAIL"></param>
    ''' <param name="p_sngNOMINALWIDTHDIFERENCE"></param>
    ''' <param name="p_sngNOMINALWIDTHTOLERANCE"></param>
    ''' <param name="p_sngMAXOVERALLDIAMETER"></param>
    ''' <param name="p_sngMINOVERALLDIAMETER"></param>
    ''' <param name="p_strOVERALLWIDTHPASSFAIL"></param>
    ''' <param name="p_strOVERALLDIAMETERPASSFAIL"></param>
    ''' <param name="p_sngDIAMETERDIFERENCE"></param>
    ''' <param name="p_sngDIAMETERTOLERANCE"></param>
    ''' <param name="p_strTEMPRESISTANCEGRADING"></param>
    ''' <param name="p_sngTENSILESTRENGHT1"></param>
    ''' <param name="p_sngTENSILESTRENGHT2"></param>
    ''' <param name="p_sngELONGATION1"></param>
    ''' <param name="p_sngELONGATION2"></param>
    ''' <param name="p_sngTENSILESTRENGHTAFTERAGE1"></param>
    ''' <param name="p_sngTENSILESTRENGHTAFTERAGE2"></param>
    ''' <param name="p_strOperatorName"></param>
    ''' <param name="p_intCertificateID"></param>
    ''' <param name="p_strMatlNum"></param>
    ''' <param name="p_strOperation"></param>
    ''' <param name="p_strGTSpecMeasurement"></param>
    ''' <param name="p_strMFGWWYY"></param>
    ''' <returns>Saved info</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function SaveMeasurement(ByVal p_strPROJECTNUMBER As String, _
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
                                    ByVal p_strMFGWWYY As String) As NameAid.SaveResult Implements IDepository.SaveMeasurement
        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                enumSaveResult = objCertDalc.SaveMeasurement(p_strPROJECTNUMBER, _
                                                             p_sngTIRENUMBER, _
                                                             p_strTESTSPEC, _
                                                             p_dteCOMPLETIONDATE, _
                                                             p_sngINFLATIONPRESSURE, _
                                                             p_strMOLDDESIGN, _
                                                             p_sngRIMWIDTH, _
                                                             p_strDOTSERIALNUMBER, _
                                                             p_sngDIAMETER, _
                                                             p_sngAVGSECTIONWIDTH, _
                                                             p_sngAVGOVERALLWIDTH, _
                                                             p_sngMAXOVERALLWIDTH, _
                                                             p_sngSIZEFACTOR, _
                                                             p_dteMOUNTTIME, _
                                                             p_sngMOUNTTEMP, _
                                                             p_intSKUID, _
                                                             p_intCertType, _
                                                             p_strCERTIFICATENUMBER, _
                                                             p_intMEASUREID, _
                                                             p_dteSerialDate, _
                                                             p_dteEndTime, _
                                                             p_sngActSizeFactor, _
                                                             p_srtSTARTINFLATIONPRESSURE, _
                                                             p_srtENDINFLATIONPRESSURE, _
                                                             p_strADJUSTMENT, _
                                                             p_sngCIRCUNFERENCE, _
                                                             p_sngNOMINALDIAMETER, _
                                                             p_sngNOMINALWIDTH, _
                                                             p_strNOMINALWIDTHPASSFAIL, _
                                                             p_sngNOMINALWIDTHDIFERENCE, _
                                                             p_sngNOMINALWIDTHTOLERANCE, _
                                                             p_sngMAXOVERALLDIAMETER, _
                                                             p_sngMINOVERALLDIAMETER, _
                                                             p_strOVERALLWIDTHPASSFAIL, _
                                                             p_strOVERALLDIAMETERPASSFAIL, _
                                                             p_sngDIAMETERDIFERENCE, _
                                                             p_sngDIAMETERTOLERANCE, _
                                                             p_strTEMPRESISTANCEGRADING, _
                                                             p_sngTENSILESTRENGHT1, _
                                                             p_sngTENSILESTRENGHT2, _
                                                             p_sngELONGATION1, _
                                                             p_sngELONGATION2, _
                                                             p_sngTENSILESTRENGHTAFTERAGE1, _
                                                             p_sngTENSILESTRENGHTAFTERAGE2, _
                                                             p_strOperatorName, _
                                                             p_intCertificateID, _
                                                             p_strMatlNum, _
                                                             p_strOperation, _
                                                             p_strGTSpecMeasurement, _
                                                             p_strMFGWWYY)
            End Using
        Catch
            Throw
        End Try
        Return enumSaveResult
    End Function

    ''' <summary>
    ''' Save Measurement Detail
    ''' </summary>
    ''' <param name="p_sngSectionWidth">Section Width</param>
    ''' <param name="p_sngOVERALLWIDTH">Over all Width</param>
    ''' <param name="p_iMEASUREID">Measure ID</param>
    ''' <param name="p_sngITERATION">Iteration</param>
    ''' <param name="p_strUserName">User name</param>
    ''' <returns></returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function MeasurementDetail_Save(ByVal p_sngSectionWidth As Single, _
                                           ByVal p_sngOVERALLWIDTH As Single, _
                                           ByVal p_iMEASUREID As Integer, _
                                           ByVal p_sngITERATION As Single, _
                                           ByVal p_strUserName As String) As NameAid.SaveResult Implements IDepository.MeasurementDetail_Save
        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                enumSaveResult = objCertDalc.MeasurementDetail_Save(p_sngSectionWidth, _
                                                                    p_sngOVERALLWIDTH, _
                                                                    p_iMEASUREID, _
                                                                    p_sngITERATION, _
                                                                    p_strUserName)
            End Using
        Catch
            Throw
        End Try
        Return enumSaveResult
    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ''' <summary>
    ''' Save Plunger details
    ''' </summary>
    ''' <param name="p_strPROJECTNUMBER"></param>
    ''' <param name="p_sngTIRENUMBER"></param>
    ''' <param name="p_strTESTSPEC"></param>
    ''' <param name="p_dteCOMPLETIONDATE"></param>
    ''' <param name="p_strDOTSERIALNUMBER"></param>
    ''' <param name="p_sngAVGBREAKINGENERGY"></param>
    ''' <param name="p_strPASSYN"></param>
    ''' <param name="p_intSKUID"></param>
    ''' <param name="p_intCertType"></param>
    ''' <param name="p_strCERTIFICATENUMBER"></param>
    ''' <param name="p_intPLUNGERID"></param>
    ''' <param name="p_dteSerialDate"></param>
    ''' <param name="p_sngMinPlunger"></param>
    ''' <param name="p_strUserName"></param>
    ''' <param name="p_intCertificateNumberID"></param>
    ''' <param name="p_strMatlNum"></param>
    ''' <param name="p_strOperation"></param>
    ''' <param name="p_strGTSpecPlunger"></param>
    ''' <param name="p_strMFGWWYY"></param>
    ''' <returns>Saved Information</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function SavePlunger(ByVal p_strPROJECTNUMBER As String, _
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
                                ByVal p_strMFGWWYY As String) As NameAid.SaveResult Implements IDepository.SavePlunger
        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                enumSaveResult = objCertDalc.SavePlunger(p_strPROJECTNUMBER, _
                                                         p_sngTIRENUMBER, _
                                                         p_strTESTSPEC, _
                                                         p_dteCOMPLETIONDATE, _
                                                         p_strDOTSERIALNUMBER, _
                                                         p_sngAVGBREAKINGENERGY, _
                                                         p_strPASSYN, _
                                                         p_intSKUID, _
                                                         p_intCertType, _
                                                         p_strCERTIFICATENUMBER, _
                                                         p_intPLUNGERID, _
                                                         p_dteSerialDate, _
                                                         p_sngMinPlunger, _
                                                         p_strUserName, _
                                                         p_intCertificateNumberID, _
                                                         p_strMatlNum, _
                                                         p_strOperation, _
                                                         p_strGTSpecPlunger, _
                                                         p_strMFGWWYY)
            End Using
        Catch
            Throw
        End Try
        Return enumSaveResult
    End Function

    ''' <summary>
    ''' Save plunger details
    ''' </summary>
    ''' <param name="p_sngBREAKINGENERGY">Break information</param>
    ''' <param name="p_intPlungerID">Plunger ID</param>
    ''' <param name="p_sngIteration">Iteration</param>
    ''' <param name="p_strUserName">User Name</param>
    ''' <returns>Saved information</returns>
    '''  <remarks>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function SavePlungerDetail(ByVal p_sngBREAKINGENERGY As Single, _
                                      ByVal p_intPlungerID As Integer, _
                                      ByVal p_sngIteration As Single, _
                                      ByVal p_strUserName As String) As NameAid.SaveResult Implements IDepository.SavePlungerDetail
        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                enumSaveResult = objCertDalc.SavePlungerDetail(p_sngBREAKINGENERGY, _
                                                               p_intPlungerID, _
                                                               p_sngIteration, _
                                                               p_strUserName)
            End Using
        Catch
            Throw
        End Try
        Return enumSaveResult
    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ''' <summary>
    ''' Saving tread wear
    ''' </summary>
    ''' <param name="p_strPROJECTNUMBER"></param>
    ''' <param name="p_sngTIRENUMBER"></param>
    ''' <param name="p_strTESTSPEC"></param>
    ''' <param name="p_dteCOMPLETIONDATE"></param>
    ''' <param name="p_strDOTSERIALNUMBER"></param>
    ''' <param name="p_sngLOWESTWEARBAR"></param>
    ''' <param name="p_strPassyn"></param>
    ''' <param name="p_intSKUID"></param>
    ''' <param name="p_intCertType"></param>
    ''' <param name="p_strCERTIFICATENUMBER"></param>
    ''' <param name="p_intTREADWEARID"></param>
    ''' <param name="p_dteSERIALDATE"></param>
    ''' <param name="p_strOperatorName"></param>
    ''' <param name="p_sngINDICATORSREQUIREMENT"></param>
    ''' <param name="p_intCertificateID"></param>
    ''' <param name="p_strMatlNum"></param>
    ''' <param name="p_strOperation"></param>
    ''' <param name="p_strGTSpecTreadWear"></param>
    ''' <param name="p_strMFGWWYY"></param>
    ''' <returns>Saved information</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function SaveTreadWear(ByVal p_strPROJECTNUMBER As String, _
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
                                  ByVal p_strMFGWWYY As String) As NameAid.SaveResult Implements IDepository.SaveTreadWear
        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                enumSaveResult = objCertDalc.SaveTreadWear(p_strPROJECTNUMBER, _
                                                           p_sngTIRENUMBER, _
                                                           p_strTESTSPEC, _
                                                           p_dteCOMPLETIONDATE, _
                                                           p_strDOTSERIALNUMBER, _
                                                           p_sngLOWESTWEARBAR, _
                                                           p_strPassyn, _
                                                           p_intSKUID, _
                                                           p_intCertType, _
                                                           p_strCERTIFICATENUMBER, _
                                                           p_intTREADWEARID, _
                                                           p_dteSERIALDATE, _
                                                           p_strOperatorName, _
                                                           p_sngINDICATORSREQUIREMENT, _
                                                           p_intCertificateID, _
                                                           p_strMatlNum, _
                                                           p_strOperation, _
                                                           p_strGTSpecTreadWear, _
                                                           p_strMFGWWYY)
            End Using
        Catch
            Throw
        End Try
        Return enumSaveResult
    End Function

    ''' <summary>
    ''' Saving tread wear details
    ''' </summary>
    ''' <param name="p_sngwearbarheight">Bar height</param>
    ''' <param name="p_intTREADWEARID">Tread Wear id</param>
    ''' <param name="p_sngIteration">Iteration</param>
    ''' <param name="p_strOperatorName">Operator name</param>
    ''' <returns>Saved information</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function SaveTreadWearDetail(ByVal p_sngwearbarheight As Single, _
                                        ByVal p_intTREADWEARID As Integer, _
                                        ByVal p_sngIteration As Single, _
                                        ByVal p_strOperatorName As String) As NameAid.SaveResult Implements IDepository.SaveTreadWearDetail
        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                enumSaveResult = objCertDalc.SaveTreadWearDetail(p_sngwearbarheight, _
                                                                 p_intTREADWEARID, _
                                                                 p_sngIteration, _
                                                                 p_strOperatorName)
            End Using
        Catch
            Throw
        End Try
        Return enumSaveResult
    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ''' <summary>
    ''' Save bead unseat
    ''' </summary>
    ''' <param name="p_strPROJECTNUMBER"></param>
    ''' <param name="p_sngTIRENUMBER"></param>
    ''' <param name="p_strTESTSPEC"></param>
    ''' <param name="p_dteCOMPLETIONDATE"></param>
    ''' <param name="p_strDOTSERIALNUMBER"></param>
    ''' <param name="p_sngLOWESTUNSEATVALUE"></param>
    ''' <param name="p_strPassyn"></param>
    ''' <param name="p_intCertType"></param>
    ''' <param name="p_strCERTIFICATENUMBER"></param>
    ''' <param name="p_intBeadUnseatID"></param>
    ''' <param name="p_dteSerialDate"></param>
    ''' <param name="p_sngMINBEADUNSEAT"></param>
    ''' <param name="p_strTESTPASSFAIL"></param>
    ''' <param name="p_strOperatorName"></param>
    ''' <param name="p_intCertificateID"></param>
    ''' <param name="p_strMatlNum"></param>
    ''' <param name="p_strOperation"></param>
    ''' <param name="p_strGTSpecBeadUnseat"></param>
    ''' <param name="p_strMFGWWYY"></param>
    ''' <returns>Saved information</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function SaveBeadUnseat(ByVal p_strPROJECTNUMBER As String, _
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
                                   ByVal p_strMFGWWYY As String) As NameAid.SaveResult Implements IDepository.SaveBeadUnseat
        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                enumSaveResult = objCertDalc.SaveBeadUnseat(p_strPROJECTNUMBER, _
                                                            p_sngTIRENUMBER, _
                                                            p_strTESTSPEC, _
                                                            p_dteCOMPLETIONDATE, _
                                                            p_strDOTSERIALNUMBER, _
                                                            p_sngLOWESTUNSEATVALUE, _
                                                            p_strPassyn, _
                                                            p_intCertType, _
                                                            p_strCERTIFICATENUMBER, _
                                                            p_intBeadUnseatID, _
                                                            p_dteSerialDate, _
                                                            p_sngMINBEADUNSEAT, _
                                                            p_strTESTPASSFAIL, _
                                                            p_strOperatorName, _
                                                            p_intCertificateID, _
                                                            p_strMatlNum, _
                                                            p_strOperation, _
                                                            p_strGTSpecBeadUnseat, _
                                                            p_strMFGWWYY)
            End Using
        Catch
            Throw
        End Try
        Return enumSaveResult
    End Function

    ''' <summary>
    ''' Band unseat details saving
    ''' </summary>
    ''' <param name="p_intBEADUNSEATID">Bead id</param>
    ''' <param name="p_sngUNSEATFORCE">Seat force</param>
    ''' <param name="p_sngIteration">Iteration</param>
    ''' <param name="p_strOperatorName">Operator name</param>
    ''' <returns></returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function SaveBeadUnseatDetail(ByVal p_intBEADUNSEATID As Integer, _
                                         ByVal p_sngUNSEATFORCE As Single, _
                                         ByVal p_sngIteration As Single, _
                                         ByVal p_strOperatorName As String) As NameAid.SaveResult Implements IDepository.SaveBeadUnseatDetail
        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                enumSaveResult = objCertDalc.SaveBeadUnseatDetail(p_intBEADUNSEATID, _
                                                                  p_sngUNSEATFORCE, _
                                                                  p_sngIteration, _
                                                                  p_strOperatorName)
            End Using
        Catch
            Throw
        End Try
        Return enumSaveResult
    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ''' <summary>
    ''' Save endurance Information
    ''' </summary>
    ''' <param name="p_intENDURANCEID"></param>
    ''' <param name="p_strProjectNumber"></param>
    ''' <param name="p_intTireNumber"></param>
    ''' <param name="p_strTESTSPEC"></param>
    ''' <param name="p_dteCOMPLETIONDATE"></param>
    ''' <param name="p_strDOTSERIALNUMBER"></param>
    ''' <param name="p_dtePRECONDSTARTDATE"></param>
    ''' <param name="p_sngPRECONDSTARTTEMP"></param>
    ''' <param name="p_sngRIMDIAMETER"></param>
    ''' <param name="p_sngRIMWIDTH"></param>
    ''' <param name="p_dtePRECONDENDDATE"></param>
    ''' <param name="p_intPRECONDENDTEMP"></param>
    ''' <param name="p_intINFLATIONPRESSURE"></param>
    ''' <param name="p_sngBEFOREDIAMETER"></param>
    ''' <param name="p_sngAFTERDIAMETER"></param>
    ''' <param name="p_intBEFOREINFLATION"></param>
    ''' <param name="p_intAFTERINFLATION"></param>
    ''' <param name="p_intWHEELPOSITION"></param>
    ''' <param name="p_intWHEELNUMBER"></param>
    ''' <param name="p_intFINALTEMP"></param>
    ''' <param name="p_sngFINALDISTANCE"></param>
    ''' <param name="p_intFINALINFLATION"></param>
    ''' <param name="p_dtePOSTCONDSTARTDATE"></param>
    ''' <param name="p_dtePOSTCONDENDDATE"></param>
    ''' <param name="p_intPOSTCONDENDTEMP"></param>
    ''' <param name="p_strPASSYN"></param>
    ''' <param name="p_intCertificationTypeID"></param>
    ''' <param name="p_strCERTIFICATENUMBER"></param>
    ''' <param name="p_dteSerialDate"></param>
    ''' <param name="p_sngPostCondTime"></param>
    ''' <param name="p_sngPreCondTime"></param>
    ''' <param name="p_sngDIAMETERTESTDRUM"></param>
    ''' <param name="p_sngPRECONDTEMP"></param>
    ''' <param name="p_sngINFLATIONPRESSUREREADJUSTED"></param>
    ''' <param name="p_sngCIRCUNFERENCEBEFORETEST"></param>
    ''' <param name="p_strRESULTPASSFAIL"></param>
    ''' <param name="p_sngENDURANCEHOURS"></param>
    ''' <param name="p_strPOSSIBLEFAILURESFOUND"></param>
    ''' <param name="p_sngCIRCUNFERENCEAFTERTEST"></param>
    ''' <param name="p_sngOUTERDIAMETERDIFERENCE"></param>
    ''' <param name="p_sngODDIFERENCETOLERANCE"></param>
    ''' <param name="p_strSERIENOM"></param>
    ''' <param name="p_strFINALJUDGEMENT"></param>
    ''' <param name="p_strAPPROVER"></param>
    ''' <param name="p_strOperatorName"></param>
    ''' <param name="p_intCertificateNumberID"></param>
    ''' <param name="p_strMatlNum"></param>
    ''' <param name="p_sngLowInfStartInflation"></param>
    ''' <param name="p_sngLowInfEndInflation"></param>
    ''' <param name="p_intLowInfEndTemp"></param>
    ''' <param name="p_strOperation"></param>
    ''' <param name="p_strGTSpecEndurance"></param>
    ''' <param name="p_strMFGWWYY"></param>
    ''' <returns>Saved information</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function SaveEndurance(ByRef p_intENDURANCEID As Integer, _
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
                                  ByVal p_strMFGWWYY As String) As NameAid.SaveResult Implements IDepository.SaveEndurance
        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                enumSaveResult = objCertDalc.SaveEndurance(p_intENDURANCEID, _
                                                           p_strProjectNumber, _
                                                           p_intTireNumber, _
                                                           p_strTESTSPEC, _
                                                           p_dteCOMPLETIONDATE, _
                                                           p_strDOTSERIALNUMBER, _
                                                           p_dtePRECONDSTARTDATE, _
                                                           p_sngPRECONDSTARTTEMP, _
                                                           p_sngRIMDIAMETER, _
                                                           p_sngRIMWIDTH, _
                                                           p_dtePRECONDENDDATE, _
                                                           p_intPRECONDENDTEMP, _
                                                           p_intINFLATIONPRESSURE, _
                                                           p_sngBEFOREDIAMETER, _
                                                           p_sngAFTERDIAMETER, _
                                                           p_intBEFOREINFLATION, _
                                                           p_intAFTERINFLATION, _
                                                           p_intWHEELPOSITION, _
                                                           p_intWHEELNUMBER, _
                                                           p_intFINALTEMP, _
                                                           p_sngFINALDISTANCE, _
                                                           p_intFINALINFLATION, _
                                                           p_dtePOSTCONDSTARTDATE, _
                                                           p_dtePOSTCONDENDDATE, _
                                                           p_intPOSTCONDENDTEMP, _
                                                           p_strPASSYN, _
                                                           p_intCertificationTypeID, _
                                                           p_strCERTIFICATENUMBER, _
                                                           p_dteSerialDate, _
                                                           p_sngPostCondTime, _
                                                           p_sngPreCondTime, _
                                                           p_sngDIAMETERTESTDRUM, _
                                                           p_sngPRECONDTEMP, _
                                                           p_sngINFLATIONPRESSUREREADJUSTED, _
                                                           p_sngCIRCUNFERENCEBEFORETEST, _
                                                           p_strRESULTPASSFAIL, _
                                                           p_sngENDURANCEHOURS, _
                                                           p_strPOSSIBLEFAILURESFOUND, _
                                                           p_sngCIRCUNFERENCEAFTERTEST, _
                                                           p_sngOUTERDIAMETERDIFERENCE, _
                                                           p_sngODDIFERENCETOLERANCE, _
                                                           p_strSERIENOM, _
                                                           p_strFINALJUDGEMENT, _
                                                           p_strAPPROVER, _
                                                           p_strOperatorName, _
                                                           p_intCertificateNumberID, _
                                                           p_strMatlNum, _
                                                           p_sngLowInfStartInflation, _
                                                           p_sngLowInfEndInflation, _
                                                           p_intLowInfEndTemp, _
                                                           p_strOperation, _
                                                           p_strGTSpecEndurance, _
                                                           p_strMFGWWYY)
            End Using
        Catch
            Throw
        End Try
        Return enumSaveResult
    End Function

    ''' <summary>
    ''' Save Endurance Details
    ''' </summary>
    ''' <param name="p_intTESTSTEP"></param>
    ''' <param name="p_intTIMEINMIN"></param>
    ''' <param name="p_intSpeed"></param>
    ''' <param name="p_sngTOTMILES"></param>
    ''' <param name="p_sngtLOAD"></param>
    ''' <param name="p_sngLOADPERCENT"></param>
    ''' <param name="p_intSETINFLATION"></param>
    ''' <param name="p_intAMBTEMP"></param>
    ''' <param name="p_intINFPRESSURE"></param>
    ''' <param name="p_dteSTEPCOMPLETIONDATE"></param>
    ''' <param name="p_intENDURANCEID"></param>
    ''' <returns>Saved information </returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function SaveEnduranceDetail(ByVal p_intTESTSTEP As Integer, _
                                        ByVal p_intTIMEINMIN As Integer, _
                                        ByVal p_intSpeed As Integer, _
                                        ByVal p_sngTOTMILES As Single, _
                                        ByVal p_sngtLOAD As Single, _
                                        ByVal p_sngLOADPERCENT As Single, _
                                        ByVal p_intSETINFLATION As Integer, _
                                        ByVal p_intAMBTEMP As Integer, _
                                        ByVal p_intINFPRESSURE As Integer, _
                                        ByVal p_dteSTEPCOMPLETIONDATE As DateTime, _
                                        ByVal p_intENDURANCEID As Integer) As NameAid.SaveResult Implements IDepository.SaveEnduranceDetail
        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                enumSaveResult = objCertDalc.SaveEnduranceDetail(p_intTESTSTEP, _
                                                                 p_intTIMEINMIN, _
                                                                 p_intSpeed, _
                                                                 p_sngTOTMILES, _
                                                                 p_sngtLOAD, _
                                                                 p_sngLOADPERCENT, _
                                                                 p_intSETINFLATION, _
                                                                 p_intAMBTEMP, _
                                                                 p_intINFPRESSURE, _
                                                                 p_dteSTEPCOMPLETIONDATE, _
                                                                 p_intENDURANCEID)
            End Using
        Catch
            Throw
        End Try
        Return enumSaveResult
    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ''' <summary>
    ''' Save high speed information
    ''' </summary>
    ''' <param name="p_intHighSpeedID"></param>
    ''' <param name="p_strPROJECTNUMBER"></param>
    ''' <param name="p_intTIRENUM"></param>
    ''' <param name="p_strTESTSPEC"></param>
    ''' <param name="p_dteCOMPETIONDATE"></param>
    ''' <param name="p_strDOTSERIALNUMBER"></param>
    ''' <param name="p_strMFGWWYY"></param>
    ''' <param name="p_dtePRECONDSTARTDATE"></param>
    ''' <param name="p_intPRECONDSARTTEMP"></param>
    ''' <param name="p_sngRIMDIAMETER"></param>
    ''' <param name="p_sngRIMWIDTH"></param>
    ''' <param name="p_dtePRECONDENDDATE"></param>
    ''' <param name="p_intPRECONDENDTEMP"></param>
    ''' <param name="p_intINFLATIONPRESSURE"></param>
    ''' <param name="p_sngBEFOREDIAMETER"></param>
    ''' <param name="p_sngAFTERDIAMETER"></param>
    ''' <param name="p_intBEFOREINFLATION"></param>
    ''' <param name="p_intAFTERINFLATION"></param>
    ''' <param name="p_intWHEELPOSITION"></param>
    ''' <param name="p_intWHEELNUMBER"></param>
    ''' <param name="p_intFINALTEMP"></param>
    ''' <param name="p_sngFINALDISTANCE"></param>
    ''' <param name="p_intFINALINFLATION"></param>
    ''' <param name="p_dtePOSTCONDSTARTDATE"></param>
    ''' <param name="p_dtePOSTCONDENDDATE"></param>
    ''' <param name="p_intPOSTCONDENDTEMP"></param>
    ''' <param name="p_sngPRECONDTIME"></param>
    ''' <param name="p_sngPOSTCONDTIME"></param>
    ''' <param name="p_strPASSYN"></param>
    ''' <param name="p_dteSERIALDATE"></param>
    ''' <param name="p_intCERTIFICATIONTYPEID"></param>
    ''' <param name="p_strCERTIFICATENUMBER"></param>
    ''' <param name="p_sngDIAMETERTESTDRUM"></param>
    ''' <param name="p_sngPRECONDTEMP"></param>
    ''' <param name="p_sngINFLATIONPRESSUREREADJUSTED"></param>
    ''' <param name="p_sngCIRCUNFERENCEBEFORETEST"></param>
    ''' <param name="p_sngWHEELSPEEDRPM"></param>
    ''' <param name="p_sngWHEELSPEEDKMH"></param>
    ''' <param name="p_sngCIRCUNFERENCEAFTERTEST"></param>
    ''' <param name="p_sngODDIFERENCE"></param>
    ''' <param name="p_sngODDIFERENCETOLERANCE"></param>
    ''' <param name="p_strSERIENOM"></param>
    ''' <param name="p_strFINALJUDGEMENT"></param>
    ''' <param name="p_strAPPROVER"></param>
    ''' <param name="p_sngPASSATKMH"></param>
    ''' <param name="p_strSPEEDTTESTPASSFAIL"></param>
    ''' <param name="p_sngSPEEDTOTALTIME"></param>
    ''' <param name="p_sngMAXSPEED"></param>
    ''' <param name="p_sngMAXLOAD"></param>
    ''' <param name="p_strOperatorName"></param>
    ''' <param name="p_intCertificateNumberID"></param>
    ''' <param name="p_strMatlNum"></param>
    ''' <param name="p_strOperation"></param>
    ''' <param name="p_strGTSpecHighSpeed"></param>
    ''' <returns>Saved details</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function SaveHighSpeed(ByRef p_intHighSpeedID As Integer, _
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
                                  ByVal p_strGTSpecHighSpeed As String) As NameAid.SaveResult Implements IDepository.SaveHighSpeed
        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                enumSaveResult = objCertDalc.SaveHighSpeed(p_intHighSpeedID, _
                                                           p_strPROJECTNUMBER, _
                                                           p_intTIRENUM, _
                                                           p_strTESTSPEC, _
                                                           p_dteCOMPETIONDATE, _
                                                           p_strDOTSERIALNUMBER, _
                                                           p_strMFGWWYY, _
                                                           p_dtePRECONDSTARTDATE, _
                                                           p_intPRECONDSARTTEMP, _
                                                           p_sngRIMDIAMETER, _
                                                           p_sngRIMWIDTH, _
                                                           p_dtePRECONDENDDATE, _
                                                           p_intPRECONDENDTEMP, _
                                                           p_intINFLATIONPRESSURE, _
                                                           p_sngBEFOREDIAMETER, _
                                                           p_sngAFTERDIAMETER, _
                                                           p_intBEFOREINFLATION, _
                                                           p_intAFTERINFLATION, _
                                                           p_intWHEELPOSITION, _
                                                           p_intWHEELNUMBER, _
                                                           p_intFINALTEMP, _
                                                           p_sngFINALDISTANCE, _
                                                           p_intFINALINFLATION, _
                                                           p_dtePOSTCONDSTARTDATE, _
                                                           p_dtePOSTCONDENDDATE, _
                                                           p_intPOSTCONDENDTEMP, _
                                                           p_sngPRECONDTIME, _
                                                           p_sngPOSTCONDTIME, _
                                                           p_strPASSYN, _
                                                           p_dteSERIALDATE, _
                                                           p_intCERTIFICATIONTYPEID, _
                                                           p_strCERTIFICATENUMBER, _
                                                           p_sngDIAMETERTESTDRUM, _
                                                           p_sngPRECONDTEMP, _
                                                           p_sngINFLATIONPRESSUREREADJUSTED, _
                                                           p_sngCIRCUNFERENCEBEFORETEST, _
                                                           p_sngWHEELSPEEDRPM, _
                                                           p_sngWHEELSPEEDKMH, _
                                                           p_sngCIRCUNFERENCEAFTERTEST, _
                                                           p_sngODDIFERENCE, _
                                                           p_sngODDIFERENCETOLERANCE, _
                                                           p_strSERIENOM, _
                                                           p_strFINALJUDGEMENT, _
                                                           p_strAPPROVER, _
                                                           p_sngPASSATKMH, _
                                                           p_strSPEEDTTESTPASSFAIL, _
                                                           p_sngSPEEDTOTALTIME, _
                                                           p_sngMAXSPEED, _
                                                           p_sngMAXLOAD, _
                                                           p_strOperatorName, _
                                                           p_intCertificateNumberID, _
                                                           p_strMatlNum, _
                                                           p_strOperation, _
                                                           p_strGTSpecHighSpeed)
            End Using
        Catch
            Throw
        End Try
        Return enumSaveResult
    End Function

    ''' <summary>
    ''' Save high speed details
    ''' </summary>
    ''' <param name="p_intHighSpeedID">High speed id</param>
    ''' <param name="p_strOperatorId">Operator</param>
    ''' <param name="p_intTESTSTEP">Step num</param>
    ''' <param name="p_intTimeMin">Min time</param>
    ''' <param name="p_sngSpeed">Speed</param>
    ''' <param name="p_sngTotMiles">miles</param>
    ''' <param name="p_sngLoad">Load</param>
    ''' <param name="p_intLoadPercent">Load percent</param>
    ''' <param name="p_intSetInflation">Inflation</param>
    ''' <param name="p_intAmbTemp">Temp</param>
    ''' <param name="p_intInfPressure">Pressure</param>
    ''' <param name="p_dteStepCompletionDate">date of completion</param>
    ''' <returns>Save results</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function SaveHighSpeedDetail(ByVal p_intHighSpeedID As Integer, _
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
                                        ByVal p_dteStepCompletionDate As DateTime) As NameAid.SaveResult Implements IDepository.SaveHighSpeedDetail
        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                enumSaveResult = objCertDalc.SaveHighSpeedDetail(p_intHighSpeedID, _
                                                                 p_strOperatorId, _
                                                                 p_intTESTSTEP, _
                                                                 p_intTimeMin, _
                                                                 p_sngSpeed, _
                                                                 p_sngTotMiles, _
                                                                 p_sngLoad, _
                                                                 p_intLoadPercent, _
                                                                 p_intSetInflation, _
                                                                 p_intAmbTemp, _
                                                                 p_intInfPressure, _
                                                                 p_dteStepCompletionDate)
            End Using

        Catch
            Throw
        End Try
        Return enumSaveResult
    End Function

    ''' <summary>
    ''' Save speed test details
    ''' </summary>
    ''' <param name="p_intHighSpeedID">High speed info</param>
    ''' <param name="p_intIteration">Iteration</param>
    ''' <param name="p_dteTime">Date time of saving</param>
    ''' <param name="p_sngSpeed">Sng speed info</param>
    ''' <param name="p_strUserName">user name</param>
    ''' <returns>Save result</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function SaveHighSpeed_SpeedTestDetail(ByVal p_intHighSpeedID As Integer, _
                                                  ByVal p_intIteration As Integer, _
                                                  ByVal p_dteTime As DateTime, _
                                                  ByVal p_sngSpeed As Single, _
                                                  ByVal p_strUserName As String) As NameAid.SaveResult Implements IDepository.SaveHighSpeed_SpeedTestDetail
        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                enumSaveResult = objCertDalc.SaveHighSpeed_SpeedTestDetail(p_intHighSpeedID, _
                                                                           p_intIteration, _
                                                                           p_dteTime, _
                                                                           p_sngSpeed, _
                                                                           p_strUserName)
            End Using
        Catch
            Throw
        End Try
        Return enumSaveResult
    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ''' <summary>
    ''' Save sound information
    ''' </summary>
    ''' <param name="p_strUserId">User id</param>
    ''' <param name="p_intSoundID">sound id</param>
    ''' <param name="p_strPROJECTNUMBER">Project number</param>
    ''' <param name="p_intTIRENUM">Iteration number</param>
    ''' <param name="p_strTESTSPEC">test spec</param>
    ''' <param name="p_strTESTREPORTNUMBER">test report number</param>
    ''' <param name="p_strMANUFACTUREANDBRAND">Brand</param>
    ''' <param name="p_strTIRECLASS">Tire class</param>
    ''' <param name="p_strCATEGORYOFUSE">Use information</param>
    ''' <param name="p_dteDATEOFTEST">Test date</param>
    ''' <param name="p_strTESTVEHICULE">Vehicle info</param>
    ''' <param name="p_strTESTVEHICULEWHEELBASE">Wheel base info</param>
    ''' <param name="p_strLOCATIONOFTESTTRACK">Test track info</param>
    ''' <param name="p_dteDATETRACKCERTIFTOISO"></param>
    ''' <param name="p_strTIRESIZEDESIGNATION"></param>
    ''' <param name="p_strTIRESERVICEDESCRIPTION"></param>
    ''' <param name="p_strREFERENCEINFLATIONPRESSURE"></param>
    ''' <param name="p_strTESTMASS_FRONTL"></param>
    ''' <param name="p_strTESTMASS_FRONTR"></param>
    ''' <param name="p_strTESTMASS_REARL"></param>
    ''' <param name="p_strTESTMASS_REARR"></param>
    ''' <param name="p_strTIRELOADINDEX_FRONTL"></param>
    ''' <param name="p_strTIRELOADINDEX_FRONTR"></param>
    ''' <param name="p_strTIRELOADINDEX_REARL"></param>
    ''' <param name="p_strTIRELOADINDEX_REARR"></param>
    ''' <param name="p_strINFLATIONPRESSURECO_FRONTL"></param>
    ''' <param name="p_strINFLATIONPRESSURECO_FRONTR"></param>
    ''' <param name="p_strINFLATIONPRESSURECO_REARL"></param>
    ''' <param name="p_strINFLATIONPRESSURECO_REARR"></param>
    ''' <param name="p_strTESTRIMWIDTHCODE"></param>
    ''' <param name="p_strTEMPMEASURESENSORTYPE"></param>
    ''' <param name="p_intCERTIFICATIONTYPEID"></param>
    ''' <param name="p_strCERTIFICATENUMBER"></param>
    ''' <param name="p_intSKUID"></param>
    ''' <param name="p_intCertificateNUmberID"></param>
    ''' <param name="p_strOperation"></param>
    ''' <param name="p_strGTSpecSound"></param>
    ''' <param name="p_strMFGWWYY"></param>
    ''' <returns>Save result</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function SaveSound(ByVal p_strUserId As String, _
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
                              ByVal p_strMFGWWYY As String) As NameAid.SaveResult Implements IDepository.SaveSound
        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                enumSaveResult = objCertDalc.SaveSound(p_strUserId, _
                                                       p_intSoundID, _
                                                       p_strPROJECTNUMBER, _
                                                       p_intTIRENUM, _
                                                       p_strTESTSPEC, _
                                                       p_strTESTREPORTNUMBER, _
                                                       p_strMANUFACTUREANDBRAND, _
                                                       p_strTIRECLASS, _
                                                       p_strCATEGORYOFUSE, _
                                                       p_dteDATEOFTEST, _
                                                       p_strTESTVEHICULE, _
                                                       p_strTESTVEHICULEWHEELBASE, _
                                                       p_strLOCATIONOFTESTTRACK, _
                                                       p_dteDATETRACKCERTIFTOISO, _
                                                       p_strTIRESIZEDESIGNATION, _
                                                       p_strTIRESERVICEDESCRIPTION, _
                                                       p_strREFERENCEINFLATIONPRESSURE, _
                                                       p_strTESTMASS_FRONTL, _
                                                       p_strTESTMASS_FRONTR, _
                                                       p_strTESTMASS_REARL, _
                                                       p_strTESTMASS_REARR, _
                                                       p_strTIRELOADINDEX_FRONTL, _
                                                       p_strTIRELOADINDEX_FRONTR, _
                                                       p_strTIRELOADINDEX_REARL, _
                                                       p_strTIRELOADINDEX_REARR, _
                                                       p_strINFLATIONPRESSURECO_FRONTL, _
                                                       p_strINFLATIONPRESSURECO_FRONTR, _
                                                       p_strINFLATIONPRESSURECO_REARL, _
                                                       p_strINFLATIONPRESSURECO_REARR, _
                                                       p_strTESTRIMWIDTHCODE, _
                                                       p_strTEMPMEASURESENSORTYPE, _
                                                       p_intCERTIFICATIONTYPEID, _
                                                       p_strCERTIFICATENUMBER, _
                                                       p_intSKUID, _
                                                       p_intCertificateNUmberID, _
                                                       p_strOperation, _
                                                       p_strGTSpecSound, _
                                                       p_strMFGWWYY)
            End Using
        Catch
            Throw
        End Try
        Return enumSaveResult
    End Function

    ''' <summary>
    ''' Save sound details information
    ''' </summary>
    ''' <param name="p_strUserId">User id</param>
    ''' <param name="p_intITERATION">Iteration Number</param>
    ''' <param name="p_strTESTSPEED">Speed</param>
    ''' <param name="p_strDIRECTIONOFRUN">Direction</param>
    ''' <param name="p_strSOUNDLEVELLEFT">Left</param>
    ''' <param name="p_strSOUNDLEVELRIGHT">Right</param>
    ''' <param name="p_strAIRTEMP">Air temperature</param>
    ''' <param name="p_strTRACKTEMP">Track temperature</param>
    ''' <param name="p_strSOUNDLEVELLEFT_TEMPCOR">Left correction</param>
    ''' <param name="p_strSOUNDLEVELRIGHT_TEMPCOR">Right Correction</param>
    ''' <param name="p_intSoundID"></param>
    ''' <returns>Save result status</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>   

    Public Function SaveSoundDetail(ByVal p_strUserId As String, _
                                    ByVal p_intITERATION As Integer, _
                                    ByVal p_strTESTSPEED As String, _
                                    ByVal p_strDIRECTIONOFRUN As String, _
                                    ByVal p_strSOUNDLEVELLEFT As String, _
                                    ByVal p_strSOUNDLEVELRIGHT As String, _
                                    ByVal p_strAIRTEMP As String, _
                                    ByVal p_strTRACKTEMP As String, _
                                    ByVal p_strSOUNDLEVELLEFT_TEMPCOR As String, _
                                    ByVal p_strSOUNDLEVELRIGHT_TEMPCOR As String, _
                                    ByVal p_intSoundID As Integer) As NameAid.SaveResult Implements IDepository.SaveSoundDetail
        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Try

            Using objCertDalc As CertificationDalc = New CertificationDalc
                enumSaveResult = objCertDalc.SaveSoundDetail(p_strUserId, _
                                                             p_intITERATION, _
                                                             p_strTESTSPEED, _
                                                             p_strDIRECTIONOFRUN, _
                                                             p_strSOUNDLEVELLEFT, _
                                                             p_strSOUNDLEVELRIGHT, _
                                                             p_strAIRTEMP, _
                                                             p_strTRACKTEMP, _
                                                             p_strSOUNDLEVELLEFT_TEMPCOR, _
                                                             p_strSOUNDLEVELRIGHT_TEMPCOR, _
                                                             p_intSoundID)
            End Using
        Catch
            Throw
        End Try
        Return enumSaveResult
    End Function

    'Added Operation as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation
    ''' <summary>
    ''' Save Wet Grip.
    ''' </summary>
    ''' <param name="p_strUserId"></param>
    ''' <param name="p_intWetGripID"></param>
    ''' <param name="p_strPROJECTNUMBER"></param>
    ''' <param name="p_intTIRENUM"></param>
    ''' <param name="p_strTESTSPEC"></param>
    ''' <param name="p_dteDATEOFTEST"></param>
    ''' <param name="p_strTESTVEHICLE"></param>
    ''' <param name="p_strLOCATIONOFTESTTRACK"></param>
    ''' <param name="p_strTESTTRACKCHARACTERISTICS"></param>
    ''' <param name="p_strISSUEBY"></param>
    ''' <param name="p_strMETHODOFCERTIFICATION"></param>
    ''' <param name="p_strTESTTIREDETAILS"></param>
    ''' <param name="p_strTIRESIZEANDSERVICEDESC"></param>
    ''' <param name="p_strTIREBRANDANDTRADEDESC"></param>
    ''' <param name="p_strREFERENCEINFLATIONPRESSURE"></param>
    ''' <param name="p_strTESTRIMWITHCODE"></param>
    ''' <param name="p_strTEMPMEASURESENSORTYPE"></param>
    ''' <param name="p_strIDENTIFICATIONSRTT"></param>
    ''' <param name="p_strTESTTIRELOAD_SRTT"></param>
    ''' <param name="p_strTESTTIRELOAD_CANDIDATE"></param>
    ''' <param name="p_strTESTTIRELOAD_CONTROL"></param>
    ''' <param name="p_strWATERDEPTH_SRTT"></param>
    ''' <param name="p_strWATERDEPTH_CANDIDATE"></param>
    ''' <param name="p_strWATERDEPTH_CONTROL"></param>
    ''' <param name="p_strWETTEDTRACKTEMPAVG"></param>
    ''' <param name="p_intCERTIFICATIONTYPEID"></param>
    ''' <param name="p_strCERTIFICATENUMBER"></param>
    ''' <param name="p_intSKUID"></param>
    ''' <param name="p_intCertificateNUmberID"></param>
    ''' <param name="p_strOperation"></param>
    ''' <param name="p_strGTSpecWetGrip"></param>
    ''' <param name="p_strMFGWWYY"></param>
    ''' <returns>Status</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function SaveWetGrip(ByVal p_strUserId As String, _
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
                                ByVal p_strMFGWWYY As String) As NameAid.SaveResult Implements IDepository.SaveWetGrip
        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                enumSaveResult = objCertDalc.SaveWetGrip(p_strUserId, _
                                                         p_intWetGripID, _
                                                         p_strPROJECTNUMBER, _
                                                         p_intTIRENUM, _
                                                         p_strTESTSPEC, _
                                                         p_dteDATEOFTEST, _
                                                         p_strTESTVEHICLE, _
                                                         p_strLOCATIONOFTESTTRACK, _
                                                         p_strTESTTRACKCHARACTERISTICS, _
                                                         p_strISSUEBY, _
                                                         p_strMETHODOFCERTIFICATION, _
                                                         p_strTESTTIREDETAILS, _
                                                         p_strTIRESIZEANDSERVICEDESC, _
                                                         p_strTIREBRANDANDTRADEDESC, _
                                                         p_strREFERENCEINFLATIONPRESSURE, _
                                                         p_strTESTRIMWITHCODE, _
                                                         p_strTEMPMEASURESENSORTYPE, _
                                                         p_strIDENTIFICATIONSRTT, _
                                                         p_strTESTTIRELOAD_SRTT, _
                                                         p_strTESTTIRELOAD_CANDIDATE, _
                                                         p_strTESTTIRELOAD_CONTROL, _
                                                         p_strWATERDEPTH_SRTT, _
                                                         p_strWATERDEPTH_CANDIDATE, _
                                                         p_strWATERDEPTH_CONTROL, _
                                                         p_strWETTEDTRACKTEMPAVG, _
                                                         p_intCERTIFICATIONTYPEID, _
                                                         p_strCERTIFICATENUMBER, _
                                                         p_intSKUID, _
                                                         p_intCertificateNUmberID, _
                                                         p_strOperation, _
                                                         p_strGTSpecWetGrip, _
                                                         p_strMFGWWYY)
            End Using
        Catch
            Throw
        End Try
        Return enumSaveResult
    End Function

    ''' <summary>
    ''' Get Certificate
    ''' </summary>
    ''' <param name="p_strUserId">User ID</param>
    ''' <param name="p_intITERATION">Iteration</param>
    ''' <param name="p_strTESTSPEED">Test speed</param>
    ''' <param name="p_strDIRECTIONOFRUN">Direction</param>
    ''' <param name="p_strSRTT">SRTT</param>
    ''' <param name="p_strCANDIDATETIRE">Candidate tire</param>
    ''' <param name="p_strPEAKBREAKFORCECOEFICIENT"></param>
    ''' <param name="p_strMEANFULLYDEVDECELERATION"></param>
    ''' <param name="p_strWETGRIPINDEX"></param>
    ''' <param name="p_strCOMMENTS">Comments</param>
    ''' <param name="p_intWetGripID">GripID</param>
    ''' <returns></returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function SaveWetGripDetail(ByVal p_strUserId As String, _
                                      ByVal p_intITERATION As Integer, _
                                      ByVal p_strTESTSPEED As String, _
                                      ByVal p_strDIRECTIONOFRUN As String, _
                                      ByVal p_strSRTT As String, _
                                      ByVal p_strCANDIDATETIRE As String, _
                                      ByVal p_strPEAKBREAKFORCECOEFICIENT As String, _
                                      ByVal p_strMEANFULLYDEVDECELERATION As String, _
                                      ByVal p_strWETGRIPINDEX As String, _
                                      ByVal p_strCOMMENTS As String, _
                                      ByVal p_intWetGripID As Integer) As NameAid.SaveResult Implements IDepository.SaveWetGripDetail
        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Try

            Using objCertDalc As CertificationDalc = New CertificationDalc
                enumSaveResult = objCertDalc.SaveWetGripDetail(p_strUserId, _
                                                               p_intITERATION, _
                                                               p_strTESTSPEED, _
                                                               p_strDIRECTIONOFRUN, _
                                                               p_strSRTT, _
                                                               p_strCANDIDATETIRE, _
                                                               p_strPEAKBREAKFORCECOEFICIENT, _
                                                               p_strMEANFULLYDEVDECELERATION, _
                                                               p_strWETGRIPINDEX, _
                                                               p_strCOMMENTS, _
                                                               p_intWetGripID)
            End Using

        Catch
            Throw
        End Try
        Return enumSaveResult
    End Function

    ''' <summary>
    ''' Add Customer
    ''' </summary>
    ''' <param name="p_intSKUId"> SKU ID</param>
    ''' <param name="p_strCustomer">Customer</param>
    ''' <param name="p_strImporter">Importer</param>
    ''' <param name="p_strImporterRepresentative">Representative</param>
    ''' <param name="p_strImporterAddress">Address</param>
    ''' <param name="p_strCountryLocation">Location</param>
    ''' <returns>Result</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function AddCustomer(ByVal p_intSKUId As Integer, _
                                ByVal p_strCustomer As String, _
                                ByVal p_strImporter As String, _
                                ByVal p_strImporterRepresentative As String, _
                                ByVal p_strImporterAddress As String, _
                                ByVal p_strCountryLocation As String) As NameAid.SaveResult Implements IDepository.AddCustomer

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                enumSaveResult = objCertDalc.AddCustomer(p_intSKUId, _
                                                         p_strCustomer, _
                                                         p_strImporter, _
                                                         p_strImporterRepresentative, _
                                                         p_strImporterAddress, _
                                                         p_strCountryLocation)
            End Using
        Catch
            Throw
        End Try
        Return enumSaveResult

    End Function

    ''' <summary>
    ''' Get Certificate ID
    ''' </summary>
    ''' <param name="p_strCertificateNumber">Certificate Numer.</param>
    ''' <param name="p_intCertificateTypeId">Certificate type ID.</param>
    ''' <param name="p_strExtensionNo">Extension Number.</param>
    ''' <returns>Certificate ID.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetCertificateIDByNumber(ByVal p_strCertificateNumber As String,
                                             ByVal p_intCertificateTypeId As Integer,
                                             ByVal p_strExtensionNo As String) As Integer Implements IDepository.GetCertificateID

        Dim intCertificateID As Integer = 0
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                intCertificateID = objCertDalc.GetCertificateID(p_strCertificateNumber, p_intCertificateTypeId, p_strExtensionNo)
            End Using
        Catch
            Throw
        End Try
        Return intCertificateID

    End Function

    ''' <summary>
    ''' Get CertificationType ID
    ''' </summary>
    ''' <param name="p_strCertificationTypeName">Date change time.</param>
    ''' <returns>CertificationType ID.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetCertificationTypeID(ByVal p_strCertificationTypeName As String) As Integer Implements IDepository.GetCertificationTypeID
        Dim intCertificationTypeID As Integer = 0
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                intCertificationTypeID = objCertDalc.GetCertificationTypeID(p_strCertificationTypeName)
            End Using
        Catch
            Throw
        End Try
        Return intCertificationTypeID

    End Function

    ' added for generic certifcation types 6/9/2016 - jeseitz
    ''' <summary>
    ''' Get Certificate template.
    ''' </summary>
    ''' <param name="p_strCertificationTypeName">Date change time.</param>
    ''' <returns>Template Name.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetCertTemplate(ByVal p_strCertificationTypeName As String) As String Implements IDepository.GetCertTemplate
        Dim strCertTemplate As String = String.Empty
        Try

            Using objCertDalc As CertificationDalc = New CertificationDalc
                strCertTemplate = objCertDalc.GetCertTemplate(p_strCertificationTypeName)
            End Using
        Catch
            Throw
        End Try
        Return strCertTemplate

    End Function

    ''' <summary>
    ''' Get Certificate Name
    ''' </summary>
    ''' <param name="p_intCertificationTypeID">Certificate Extension</param>
    ''' <returns>Count</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Function GetCertificationNameByID(ByVal p_intCertificationTypeID As Integer) As String Implements IDepository.GetCertificationNameByID
        Dim strCertificationName As String = String.Empty
        Try

            Using objCertDalc As CertificationDalc = New CertificationDalc
                strCertificationName = objCertDalc.GetCertificationNameByID(p_intCertificationTypeID)
            End Using

        Catch
            Throw
        End Try
        Return strCertificationName

    End Function

    ' Added as per project 2706 technical specification
    ''' <summary>
    ''' Get Certified Material Count
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certificate Type ID</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <param name="p_strCertificateExtension">Certificate Extension</param>
    ''' <returns>Count</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetCertifiedMaterialCount(ByVal p_intCertificationTypeId As Integer, _
                                              ByVal p_strCertificateNumber As String, _
                                              ByVal p_strCertificateExtension As String) As Integer Implements IDepository.GetCertifiedMaterialCount

        Dim intCertificateID As Integer = 0
        Using objCertDalc As CertificationDalc = New CertificationDalc
            intCertificateID = objCertDalc.GetCertifiedMaterialCount(p_intCertificationTypeId, _
                                                                     p_strCertificateNumber, _
                                                                     p_strCertificateExtension)
        End Using

        Return intCertificateID

    End Function

    ''' <summary>
    ''' Rename Certificate
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certificate Type ID</param>
    ''' <param name="p_strOldCertificateNumber">Certificate Number</param>
    ''' <param name="p_strOldCertificateExtension">Certificate Extension</param>
    ''' <param name="p_strNewCertificateNumber">New Certificate Number</param>
    ''' <param name="p_strNewCertificateExtension">New Certificate Extension</param>
    ''' <param name="p_strUserName"></param>
    ''' <returns></returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function RenameCertificate(ByVal p_intCertificationTypeId As Integer, _
                                      ByVal p_strOldCertificateNumber As String, _
                                      ByVal p_strOldCertificateExtension As String, _
                                      ByVal p_strNewCertificateNumber As String, _
                                      ByVal p_strNewCertificateExtension As String, _
                                      ByVal p_strUserName As String) As Boolean Implements IDepository.RenameCertificate

        Dim blnDone As Boolean = False
        Try


            Using objCertDalc As CertificationDalc = New CertificationDalc
                blnDone = objCertDalc.RenameCertificate(p_intCertificationTypeId, _
                                                        p_strOldCertificateNumber, _
                                                        p_strOldCertificateExtension, _
                                                        p_strNewCertificateNumber, _
                                                        p_strNewCertificateExtension, _
                                                        p_strUserName)
            End Using
        Catch
            Throw
        End Try
        Return blnDone

    End Function

    ''' <summary>
    ''' Delete Certificate
    ''' </summary>
    ''' <param name="p_intCertificationTypeId"> Certificate type ID</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <param name="p_strCertificateExtension">Certificate Extension</param>
    ''' <param name="p_strUserName">User Name</param>
    ''' <returns>Status</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function DeleteCertificate(ByVal p_intCertificationTypeId As Integer, _
                                      ByVal p_strCertificateNumber As String, _
                                      ByVal p_strCertificateExtension As String, _
                                      ByVal p_strUserName As String) As Boolean Implements IDepository.DeleteCertificate

        Dim blnDone As Boolean = False
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                blnDone = objCertDalc.DeleteCertificate(p_intCertificationTypeId, _
                                                        p_strCertificateNumber, _
                                                        p_strCertificateExtension, _
                                                        p_strUserName)
            End Using
        Catch
            Throw
        End Try
        Return blnDone

    End Function

    ''' <summary>
    ''' Get Certificate materials
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certificate type ID</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <param name="p_strCertificateExtension">Certificate extension</param>
    ''' <returns>Status</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetCertificateMaterials(ByVal p_intCertificationTypeId As Integer, _
                                            ByVal p_strCertificateNumber As String, _
                                            ByVal p_strCertificateExtension As String) As DataTable Implements IDepository.GetCertificateMaterials
        Dim dtCertificateMaterials As DataTable = Nothing
        Try
            dtCertificateMaterials = New DataTable
            Using objCertDalc As CertificationDalc = New CertificationDalc
                dtCertificateMaterials = objCertDalc.GetCertificateMaterials(p_intCertificationTypeId, _
                                                                             p_strCertificateNumber, _
                                                                             p_strCertificateExtension)
            End Using
        Catch
            Throw
        End Try
        Return dtCertificateMaterials
    End Function

    ''' <summary>
    ''' Detach Certificate
    ''' </summary>
    ''' <param name="p_intSkuId">SKU ID</param>
    ''' <param name="p_intCertificateId">Certificate ID</param>
    ''' <returns>Status</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function DetachCertificate(ByVal p_intSkuId As Integer, _
                                      ByVal p_intCertificateId As Integer) As Boolean Implements IDepository.DetachCertificate

        Dim blnDone As Boolean = False
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                blnDone = objCertDalc.DetachCertificate(p_intSkuId, _
                                                        p_intCertificateId)
            End Using
        Catch
            Throw
        End Try
        Return blnDone

    End Function

    ''' <summary>
    ''' Move Certificate
    ''' </summary>
    ''' <param name="p_intCertificationTypeId"> Certificate type ID</param>
    ''' <param name="p_strNewCertificateNumber"> Certificate number</param>
    ''' <param name="p_strNewCertificateExtension">New certificate extension</param>
    ''' <param name="p_intSkuId">SKU iD</param>
    ''' <param name="p_intCertificateId">Certificate ID</param>
    ''' <param name="p_strUserName">User Name</param>
    ''' <returns>Status</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    ''' 
    Public Function MoveCertificate(ByVal p_intCertificationTypeId As Integer, _
                                    ByVal p_strNewCertificateNumber As String, _
                                    ByVal p_strNewCertificateExtension As String, _
                                    ByVal p_intSkuId As Integer, _
                                    ByVal p_intCertificateId As Integer, _
                                    ByVal p_strUserName As String) As Boolean Implements IDepository.MoveCertificate

        Dim blnDone As Boolean = False
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                blnDone = objCertDalc.MoveCertificate(p_intCertificationTypeId, _
                                                      p_strNewCertificateNumber, _
                                                      p_strNewCertificateExtension, _
                                                      p_intSkuId, _
                                                      p_intCertificateId, _
                                                      p_strUserName)
            End Using
        Catch
            Throw
        End Try
        Return blnDone

    End Function

    ''' <summary>
    ''' Get duplicate Certificate
    ''' </summary>
    ''' <param name="p_strMaterialNumber">SKU ID</param>
    ''' <param name="p_strSpeedRating">SKU ID</param>
    ''' <returns>set duplicate Certificate</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDuplicateCertificates(ByVal p_strMaterialNumber As String, _
                                             ByVal p_strSpeedRating As String) As DataTable Implements IDepository.GetDuplicateCertificates

        Dim dtResults As DataTable = Nothing
        Try
            dtResults = New DataTable
            Using objCertDalc As CertificationDalc = New CertificationDalc
                dtResults = objCertDalc.GetDuplicateCertificates(p_strMaterialNumber, _
                                                                 p_strSpeedRating)
            End Using
        Catch
            Throw
        End Try
        Return dtResults

    End Function

    ''' <summary>
    ''' Delete duplicate certificates
    ''' </summary>
    ''' <param name="p_intSkuId">SKU ID</param>
    ''' <returns>status</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function DeleteDuplicateCertificates(ByVal p_intSkuId As Integer) As Boolean Implements IDepository.DeleteDuplicateCertificates

        Dim blnDeleted As Boolean = False
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                blnDeleted = objCertDalc.DeleteDuplicateCertificates(p_intSkuId)
            End Using
        Catch
            Throw
        End Try
        Return blnDeleted

    End Function

    ''' <summary>
    ''' Check whether the family Id exists or not and get the Family Desc
    ''' </summary>
    ''' <remarks>
    ''' <param name="p_intFamilyId">FamilyID</param>
    ''' <param name="p_strFamilyDesc">Family Desc</param>
    ''' <returns>status</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>    
    Public Function CheckIsFamilyIdExist(ByVal p_intcertificateid As Integer, _
                                        ByVal p_intFamilyId As String, _
                                        ByRef p_strFamilyDesc As String) As Boolean Implements IDepository.CheckIsFamilyIdExist
        Dim blnExists As Boolean = False
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                blnExists = objCertDalc.CheckIsFamilyIdExist(p_intcertificateid, p_intFamilyId, p_strFamilyDesc)
            End Using
        Catch
            Throw
        End Try
        Return blnExists

    End Function

    ''' <summary>
    ''' Gets type's of Tyres.
    ''' </summary>
    ''' <returns>Tire types.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetTireType() As DataTable Implements IDepository.GetTireType
        Dim dtTireType As DataTable = Nothing

        Try
            dtTireType = New DataTable
            Using objCertDalc As CertificationDalc = New CertificationDalc
                dtTireType = objCertDalc.GetTireType()
            End Using
        Catch
            Throw
        End Try
        Return dtTireType
    End Function

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
    ''' <returns>Status.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function SaveFamily(ByVal p_intCertificateid As Integer, _
                                ByVal p_intFamilyID As Integer, _
                                ByVal p_strFamilyCode As String, _
                                ByVal p_strFamilyDesc As String, _
                                ByVal p_strApplicationCat As String, _
                                ByVal p_strConstructionType As String, _
                                ByVal p_strStructureType As String, _
                                ByVal p_strMountingType As String, _
                                ByVal p_strAspectRatioCat As String, _
                                ByVal p_strSpeedRatingCat As String, _
                                ByVal p_strLoadIndexCat As String, _
                                ByVal p_strUserName As String) As System.Boolean Implements IDepository.SaveFamily

        Dim blnSaved As Boolean = False

        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                blnSaved = objCertDalc.SaveFamily(p_intCertificateid, _
                                                 p_intFamilyID, _
                                                 p_strFamilyCode, _
                                                 p_strFamilyDesc, _
                                                 p_strApplicationCat, _
                                                 p_strConstructionType, _
                                                 p_strStructureType, _
                                                 p_strMountingType, _
                                                 p_strAspectRatioCat, _
                                                 p_strSpeedRatingCat, _
                                                 p_strLoadIndexCat, _
                                                 p_strUserName)

            End Using
        Catch
            Throw
        End Try
        Return blnSaved

    End Function

    ''' <summary>
    ''' Deletes the data from Imark family table.
    ''' </summary>
    ''' <param name="p_intFamilyId">FamilyID</param>
    ''' <returns>Status.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function Deletefamily(ByVal p_intCertificateid As Integer, _
                                 ByVal p_intFamilyId As Integer) As Boolean Implements IDepository.Deletefamily

        Dim blnDeleted As Boolean = False
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                blnDeleted = objCertDalc.Deletefamily(p_intCertificateid, p_intFamilyId)
            End Using
        Catch
            Throw
        End Try

        Return blnDeleted
    End Function

    ''' <summary>
    ''' Get All Families from database
    ''' </summary>
    ''' <param name="pn_certificateid">Certificate ID.</param>
    ''' <returns>Audit logs.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetFamilies(ByVal pn_certificateid As Integer) As DataTable Implements IDepository.GetFamilies
        Dim dtResults As DataTable = Nothing

        Try
            dtResults = New DataTable
            Using objCertDalc As CertificationDalc = New CertificationDalc
                dtResults = objCertDalc.GetFamilies(pn_certificateid)
            End Using
        Catch
            Throw
        End Try
        Return dtResults
    End Function

    ''' <summary>
    ''' Copy Certification.
    ''' </summary>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <returns>Status.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function CopyCertification(ByVal p_strMatlNum As String) As Boolean Implements IDepository.CopyCertification
        Dim blnCopied As Boolean = False
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                blnCopied = objCertDalc.CopyCertification(p_strMatlNum)
            End Using
        Catch
            Throw
        End Try
        Return blnCopied
    End Function

    ''' <summary>
    ''' Attach Certification.
    ''' </summary>
    ''' <param name="p_skuid">Sku Id</param>
    ''' <param name="p_strCertNum">Certificate Number</param>
    ''' <param name="p_strExtensionEn">Extension Number</param>
    ''' <param name="p_certificationtypeid">Certificate Type Id</param>
    ''' <returns>Error Message.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Function AttachCertification(ByVal p_skuid As Integer, _
                                 ByVal p_strCertNum As String, _
                                 ByVal p_strExtensionEn As String, _
                                 ByVal p_certificationtypeid As Integer) As String Implements IDepository.AttachCertification
        Dim strResult As String = String.Empty
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                strResult = objCertDalc.AttachCertification(p_skuid, p_strCertNum, p_strExtensionEn, p_certificationtypeid)
            End Using
        Catch
            Throw
        End Try
        Return strResult
    End Function

    ''' <summary>
    ''' Update Speedrating of a Material.
    ''' </summary>
    ''' <param name="p_intSkuID">SKU ID </param>
    ''' <param name="p_strSpeedrating">Spped threading info</param>
    ''' <returns>Audit logs.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function EditMaterial(ByVal p_intSkuID As Integer, _
                                 ByVal p_strSpeedrating As String) As Boolean Implements IDepository.EditMaterial
        Dim blnSaved As Boolean = False
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                blnSaved = objCertDalc.EditMaterial(p_intSkuID, p_strSpeedrating)
            End Using
        Catch
            Throw
        End Try
        Return blnSaved
    End Function

    ''' <summary>
    ''' Get Material Details
    ''' </summary>
    ''' <param name="p_strMaterialNumber"></param>
    ''' <returns>Set of materials.</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>1.Implemented Coding Standards.</para>
    ''' <para>2. Implemented exception Handling.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetMaterial(ByVal p_strMaterialNumber As String) As DataTable Implements IDepository.GetMaterial
        Dim dtResults As DataTable = Nothing
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                dtResults = New DataTable
                dtResults = objCertDalc.GetMaterial(p_strMaterialNumber)
            End Using
        Catch
            Throw
        End Try
        Return dtResults
    End Function

    ''' <summary>
    ''' Refreshes Product data.
    ''' </summary>
    ''' <param name="p_strMaterialNumber">Material Number</param>
    ''' <param name="p_strErrorDesc">Error Description</param>
    ''' <returns>Error Number</returns>
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function RefreshProduct(ByVal p_strMaterialNumber As String, ByRef p_strErrorDesc As String) As Integer Implements IDepository.RefreshProduct
        Dim intErrNumber As Integer = 0
        Try
            Using objCertDalc As CertificationDalc = New CertificationDalc
                intErrNumber = objCertDalc.RefreshProduct(p_strMaterialNumber, p_strErrorDesc)
            End Using
        Catch
            Throw
        End Try
        Return intErrNumber
    End Function

#End Region

#Region " IDisposable Support "

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    ''' <summary>
    ''' Disposing and freeing memory
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
    ''' <term>Vinay Chowdavarapu</term>
    ''' <description>
    ''' <para>09/10/2019</para>
    ''' <para>1.Implemented Coding Standards.</para>
    ''' <para>2. Implemented exception Handling.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class

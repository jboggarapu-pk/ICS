Imports System.Reflection
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities
Imports CooperTire.ICS.DepositoryTender


''' <summary>
''' Change Audit process model 
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
''' <para>08/01/2019</para>
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
Public Class AuditLog(Of T)

#Region "Members"

    ''' <summary>
    ''' Variable to hold object Old
    ''' </summary>
    ''' <remarks></remarks>
    Private m_objOld As T

    ''' <summary>
    ''' Variable to hold object New
    ''' </summary>
    ''' <remarks></remarks>
    Private m_objNew As T

    ''' <summary>
    ''' Variable to hold Type
    ''' </summary>
    ''' <remarks></remarks>
    Private m_type As Type

    ''' <summary>
    ''' Variable to hold list of audit log entry
    ''' </summary>
    ''' <remarks></remarks>
    Private m_listLastAuditEntries As List(Of AuditLogEntry) = New List(Of AuditLogEntry)

#End Region

#Region "Properties"
    ''' <summary>
    ''' Gets or sets List of last Audit Results.
    ''' </summary>
    ''' <value>List</value>
    ''' <returns>Last Audit Results.</returns>
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
    ''' <para>10/29/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Public ReadOnly Property LastAuditResults() As List(Of AuditLogEntry)
        Get
            Return m_listLastAuditEntries
        End Get
    End Property

#End Region

#Region "Constructors / Destructor's"

    ''' <summary>
    '''  Constructor with parameters.
    ''' </summary>
    ''' <param name="p_objNew">object New</param>
    ''' <param name="p_objOld">object Old</param>
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
    Public Sub New(ByVal p_objOld As T, ByVal p_objNew As T)

        m_objOld = p_objOld
        m_objNew = p_objNew
        m_type = GetType(T)

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    '''  Method to Run objects audit.
    ''' </summary>
    ''' <param name="p_strAreaOfChange">Area of change</param>
    ''' <returns>List of Audit entries</returns> 
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
    Public Function RunAudit(ByVal p_strAreaOfChange As String) As List(Of AuditLogEntry)
        m_listLastAuditEntries = New List(Of AuditLogEntry)

        Try

            'This is good because if there is no original certificate then don't audit
            If m_objOld Is Nothing OrElse m_objNew Is Nothing Then
                Return m_listLastAuditEntries
            End If

            Dim aleEntry As AuditLogEntry
            Dim piProps As PropertyInfo() = m_type.GetProperties()
            For Each pi As PropertyInfo In piProps

                Dim objPropValOld As Object = m_type.GetProperty(pi.Name).GetValue(m_objOld, Nothing)
                Dim objPropValNew As Object = m_type.GetProperty(pi.Name).GetValue(m_objNew, Nothing)

                'If old value is nothing then continue for.
                If objPropValOld Is Nothing Then Continue For

                If objPropValOld.GetType.Name = "String" Then
                    If objPropValOld Is String.Empty Then Continue For
                End If

                If objPropValOld Is Nothing And objPropValNew Is Nothing Then Continue For
                If Not IsAuditable(pi) Then Continue For

                If Not objPropValOld Is Nothing And Not objPropValNew Is Nothing Then
                    If objPropValOld Is objPropValNew Then Continue For
                End If

                aleEntry = New AuditLogEntry()
                aleEntry.ChangeDateTime = DateTime.Now
                aleEntry.ChangedBy = SecurityModel.GetUserName
                aleEntry.Area = p_strAreaOfChange

                If InStr(p_strAreaOfChange.ToUpper(), "CERTIFICATION") <> 0 Then
                    aleEntry.ChangedFieldElement = pi.Name
                    If objPropValOld Is Nothing Then
                        aleEntry.OldValue = "<null>"
                    Else
                        aleEntry.OldValue = objPropValOld.ToString()
                    End If

                    If objPropValNew Is Nothing Then
                        aleEntry.NewValue = "<null>"
                    Else
                        aleEntry.NewValue = objPropValNew.ToString()
                    End If
                Else
                    aleEntry.ChangedFieldElement = "Marketing Request"
                End If

                m_listLastAuditEntries.Add(aleEntry)

                'Once we find the first change then exit the loop
                Exit For
            Next
        Catch
            Throw
        End Try
        Return m_listLastAuditEntries

    End Function

    ''' <summary>
    '''  Method to save Audit marketing objects
    ''' </summary>
    ''' <param name="p_strAreaOfChange">Area of change</param>
    ''' <returns>Boolean</returns> 
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
    Public Function RunAuditAndSaveResultsMarketing(ByVal p_strAreaOfChange As String) As Boolean
        Dim blnDone As Boolean = True
        Try
            m_listLastAuditEntries = RunAudit(p_strAreaOfChange)

            For Each aleEntry As AuditLogEntry In m_listLastAuditEntries
                blnDone = Depository.Current.SaveAuditLogEntry(aleEntry.ChangeDateTime, aleEntry.ChangedBy, aleEntry.Area, aleEntry.ChangedFieldElement, aleEntry.OldValue, aleEntry.NewValue, 1,
                                                               "Marketing Change")
            Next
        Catch
            Throw
        End Try
        Return blnDone

    End Function


    ''' <summary>
    '''  Method to Get last audit results as text
    ''' </summary> 
    ''' <returns>Last Audit Results as String</returns> 
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
    Public Function GetLastAuditResultsAsText() As String

        Dim strResults As String = String.Empty
        Dim strDelim As String = ";"
        Try

            For Each aleEntry As AuditLogEntry In m_listLastAuditEntries
                strResults &= aleEntry.Area & strDelim
                strResults &= aleEntry.ChangeDateTime.ToShortTimeString() & strDelim
                strResults &= aleEntry.ChangedBy & strDelim
                strResults &= aleEntry.ChangedFieldElement & strDelim
                strResults &= aleEntry.OldValue & strDelim
                strResults &= aleEntry.NewValue & strDelim
                strResults &= Environment.NewLine
            Next
        Catch
            Throw
        End Try

        Return strResults

    End Function


    ''' <summary>
    '''  Method to check Is the property type auditable?
    ''' </summary>
    ''' <param name="p_piPropInfo">Prop Info</param> 
    ''' <returns>Boolean Value</returns> 
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
    Private Function IsAuditable(ByVal p_piPropInfo As PropertyInfo) As Boolean

        Dim blnIsAuditable As Boolean = False
        Try
            'JBH_2.00 Project 5325 - Added Mold Change Required and Operations Approval Dates to Non-Auditable Fields
            Select Case p_piPropInfo.Name
                'Specific fields that we do not need audited
                Case "Iteration", _
                    "Circumference", _
                    "CompletionDate", _
                    "ProductLocation", _
                    "AddNewImporter", _
                    "AddNewCustomer", _
                    "CircumferenceBeforeTesting", _
                    "OuterDiameterDifference", _
                    "DOTSerialNumber", _
                    "CountryOfManufacture_N", _
                    "lblSizeStamp", _
                    "lblSingLoadIndex", _
                    "lblDualLoadIndex", _
                    "lblBrandDesc", _
                    "lblSpeedRating", _
                    "lblTubelessYN", _
                    "ActSigReq", _
                    "CertDateSubmitted", _
                    "DateAssigned_EGI", _
                    "DateSubmitted", _
                    "DateApproved_CEGI", _
                    "TPN", _
                    "DiscontinuedDate", _
                    "CertificateNumber", _
                    "BatchNumber_G", _
                    "ExpiryDate_I", _
                    "Family", _
                    "Family_I", _
                    "MoldStamping", _
                    "SerialDate", _
                    "MoldChgRequired", _
                    "OperDateApproved"
                    Return blnIsAuditable
                Case Else
                    Select Case p_piPropInfo.PropertyType.Name
                        Case GetType(String).Name, _
                            GetType(Integer).Name, _
                            GetType(Boolean).Name, _
                            GetType(Short).Name, _
                            GetType(Double).Name, _
                            GetType(Single).Name, _
                            GetType(AuditLogEntry.StatusOfChange).Name, _
                            GetType(DateTime).Name
                            blnIsAuditable = True
                        Case Else
                            ' default
                            Debug.WriteLine("Non-auditable type - " & p_piPropInfo.PropertyType.Name)
                    End Select
            End Select
        Catch
            Throw
        End Try
        Return blnIsAuditable

    End Function

#End Region

End Class

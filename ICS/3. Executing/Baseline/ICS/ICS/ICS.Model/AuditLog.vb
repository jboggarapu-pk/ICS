Imports System.Reflection
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities
Imports CooperTire.ICS.DepositoryTender

''' <summary>
''' Change Audit process model
''' </summary>
''' <remarks></remarks>
Public Class AuditLog(Of T)

#Region "Members"

    Private m_objOld As T
    Private m_objNew As T
    Private m_type As Type

    Private m_listLastAuditEntries As List(Of AuditLogEntry) = New List(Of AuditLogEntry)

#End Region

#Region "Properties"

    Public ReadOnly Property LastAuditResults() As List(Of AuditLogEntry)
        Get
            Return m_listLastAuditEntries
        End Get
    End Property

#End Region

#Region "Constructors / Destructors"

    Public Sub New(ByVal p_objOld As T, ByVal p_objNew As T)

        m_objOld = p_objOld
        m_objNew = p_objNew
        m_type = GetType(T)

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Run objects audit
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RunAudit(ByVal p_strAreaOfChange As String) As List(Of AuditLogEntry)

        m_listLastAuditEntries = New List(Of AuditLogEntry)

        'This is good becaues if there is no original certificate then don't audit
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
                If objPropValOld = String.Empty Then Continue For
            End If

            If objPropValOld Is Nothing And objPropValNew Is Nothing Then Continue For
            If Not IsAuditable(pi) Then Continue For

            If Not objPropValOld Is Nothing And Not objPropValNew Is Nothing Then
                If objPropValOld = objPropValNew Then Continue For
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

        Return m_listLastAuditEntries

    End Function

    ''' <summary>
    ''' Audit marketing objects
    ''' </summary>
    ''' <param name="p_strAreaOfChange"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RunAuditAndSaveResultsMarketing(ByVal p_strAreaOfChange As String) As Boolean

        m_listLastAuditEntries = RunAudit(p_strAreaOfChange)
        Dim blnDone As Boolean = True

        For Each aleEntry As AuditLogEntry In m_listLastAuditEntries
            blnDone = Depository.Current.SaveAuditLogEntry(aleEntry.ChangeDateTime, aleEntry.ChangedBy, aleEntry.Area, aleEntry.ChangedFieldElement, aleEntry.OldValue, aleEntry.NewValue, 1, "Marketing Change")
        Next

        Return blnDone

    End Function

    ''' <summary>
    ''' Get last audit results as text
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetLastAuditResultsAsText() As String

        Dim strResults As String = String.Empty
        Dim strDelim As String = ";"

        For Each aleEntry As AuditLogEntry In m_listLastAuditEntries
            strResults &= aleEntry.Area & strDelim
            strResults &= aleEntry.ChangeDateTime.ToShortTimeString() & strDelim
            strResults &= aleEntry.ChangedBy & strDelim
            strResults &= aleEntry.ChangedFieldElement & strDelim
            strResults &= aleEntry.OldValue & strDelim
            strResults &= aleEntry.NewValue & strDelim
            strResults &= Environment.NewLine
        Next

        Return strResults

    End Function

    ''' <summary>
    ''' Is the property type auditable?
    ''' </summary>
    ''' <param name="p_piPropInfo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsAuditable(ByVal p_piPropInfo As PropertyInfo) As Boolean

        Dim blnIsAuditable As Boolean = False

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

        Return blnIsAuditable

    End Function

#End Region

End Class

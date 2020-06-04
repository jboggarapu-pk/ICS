Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.Validation
Imports Microsoft.Practices.EnterpriseLibrary.Validation.Validators
Imports CooperTire.ICS.Common

''' <summary>
''' AuditLogEntry file to handle log entry related operations
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
''' <para>09/25/2019</para>
''' <para>Implemented code standarization.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class AuditLogEntry

    ''' <summary>
    ''' AuditLogEntry activity area
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum AreaOfChange
        Marketing
        Certification
        Approval
    End Enum

    ''' <summary>
    ''' AuditLogEntry change status
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum StatusOfChange
        Pending = 1
        Approved = 2
        Deny = 3
    End Enum

#Region "Members"

    Private m_intChangeLogID As Integer
    Private m_dteChangeDateTime As DateTime
    Private m_strChangedBy As String
    Private m_strArea As String
    Private m_strChangedFieldElement As String
    Private m_strOldValue As String
    Private m_strNewValue As String
    Private m_strApprover As String
    Private m_enuApprovalStatus As StatusOfChange
    Private m_intReasonID As Integer
    Private m_strNote As String

#End Region

#Region "Properties"

    ''' <summary>
    ''' Gets or sets Change Log Id value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>ChangeLogId value.</returns>
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
    ''' <para>09/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ChangeLogID() As Integer
        Get
            Return m_intChangeLogID
        End Get
        Set(ByVal value As Integer)
            m_intChangeLogID = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Reason Id value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>ReasonId value.</returns>
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
    ''' <para>09/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ReasonID() As Integer
        Get
            Return m_intReasonID
        End Get
        Set(ByVal value As Integer)
            m_intReasonID = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Change DateTime value.
    ''' </summary>
    ''' <value>DateTime</value>
    ''' <returns>ChangeDateTime value.</returns>
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
    ''' <para>09/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ChangeDateTime() As DateTime
        Get
            Return m_dteChangeDateTime
        End Get
        Set(ByVal value As DateTime)
            m_dteChangeDateTime = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Changed by Id value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>ChangedBy value.</returns>
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
    ''' <para>09/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ChangedBy() As String
        Get
            Return m_strChangedBy
        End Get
        Set(ByVal value As String)
            m_strChangedBy = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Area value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Area value.</returns>
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
    ''' <para>09/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property Area() As String
        Get
            Return m_strArea
        End Get
        Set(ByVal value As String)
            m_strArea = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Change field element value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>ChangedFieldElement value.</returns>
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
    ''' <para>09/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ChangedFieldElement() As String
        Get
            Return m_strChangedFieldElement
        End Get
        Set(ByVal value As String)
            m_strChangedFieldElement = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Old value value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>OldValue value.</returns>
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
    ''' <para>09/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property OldValue() As String
        Get
            Return m_strOldValue
        End Get
        Set(ByVal value As String)
            m_strOldValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets New value value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>NewValue value.</returns>
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
    ''' <para>09/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property NewValue() As String
        Get
            Return m_strNewValue
        End Get
        Set(ByVal value As String)
            m_strNewValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets approval status value.
    ''' </summary>
    ''' <value>StatusOfChange</value>
    ''' <returns>ApprovalStatus value.</returns>
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
    ''' <para>09/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ApprovalStatus() As StatusOfChange
        Get
            Return m_enuApprovalStatus
        End Get
        Set(ByVal value As StatusOfChange)
            m_enuApprovalStatus = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Approver value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Approver value.</returns>
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
    ''' <para>09/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property Approver() As String
        Get
            Return m_strApprover
        End Get
        Set(ByVal value As String)
            m_strApprover = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets note value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Note value.</returns>
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
    ''' <para>09/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property Note() As String
        Get
            Return m_strNote
        End Get
        Set(ByVal value As String)
            m_strNote = value
        End Set
    End Property

#End Region

#Region "Constructors"

    ''' <summary>
    ''' Defaullt Constructor to initialize class members.
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
    ''' <para>09/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Sub New()
    End Sub

    ''' <summary>
    ''' Custom Constructor to initialize class members.
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
    ''' <para>09/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Sub New(ByVal p_objAuditLogEntry As AuditLogEntry)

        m_intChangeLogID = p_objAuditLogEntry.ChangeLogID
        m_dteChangeDateTime = p_objAuditLogEntry.ChangeDateTime
        m_strChangedBy = p_objAuditLogEntry.ChangedBy
        m_strArea = p_objAuditLogEntry.Area
        m_strChangedFieldElement = p_objAuditLogEntry.ChangedFieldElement
        m_strOldValue = p_objAuditLogEntry.OldValue
        m_strNewValue = p_objAuditLogEntry.NewValue
        m_enuApprovalStatus = p_objAuditLogEntry.ApprovalStatus
        m_strApprover = p_objAuditLogEntry.Approver

    End Sub

    ''' <summary>
    ''' Custom Constructor to initialize class members.
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
    ''' <para>09/25/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Sub New(ByVal p_dteChangeDateTime As DateTime, ByVal p_strChangedBy As String, _
                        ByVal p_strArea As String, ByVal p_strChangedFieldElement As String, _
                        ByVal p_strOldValue As String, ByVal p_strNewValue As String, _
                        ByVal p_enuApprovalStatus As AuditLogEntry.StatusOfChange, ByVal p_strApprover As String)

        m_dteChangeDateTime = p_dteChangeDateTime
        m_strChangedBy = p_strChangedBy
        m_strArea = p_strArea
        m_strChangedFieldElement = p_strChangedFieldElement
        m_strOldValue = p_strOldValue
        m_strNewValue = p_strNewValue
        m_enuApprovalStatus = p_enuApprovalStatus
        m_strApprover = p_strApprover

    End Sub

#End Region

#Region "Methods"
#End Region

End Class

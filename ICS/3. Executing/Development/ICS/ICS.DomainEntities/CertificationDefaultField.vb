''' <summary>
''' Class contains Properties of Certification Default Field.
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
''' <term>Sujitha</term>
''' <description>
''' <para>10/17/2019</para>
''' <para>Implemented code standarization.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class CertificationDefaultField

#Region "Members"

    ''' <summary>
    ''' variable to hold Field Label.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strFieldLabel As String = String.Empty

    ''' <summary>
    ''' variable to hold Field Value.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strFieldValue As String = String.Empty

    ''' <summary>
    ''' variable to hold Field Name.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strFieldName As String = String.Empty

    ''' <summary>
    ''' variable to hold Report Name.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strReportName As String = String.Empty

    ''' <summary>
    ''' variable to hold Certification Number.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strCertificateNo As String = String.Empty

    ''' <summary>
    ''' variable to hold Id.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intId As Integer = 0

    ''' <summary>
    ''' variable to hold Certification Type Id.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intCertificateTypeId As Integer = 0

    ''' <summary>
    ''' variable to hold Certification Id.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intCertificateId As Integer = 0

#End Region

#Region "Properties"

    ''' <summary>
    ''' Gets or sets Certificate Number value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Certificate Number value.</returns>
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
    ''' <para>10/17/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property CertificateNo() As String
        Get
            Return m_strCertificateNo.Trim
        End Get
        Set(ByVal value As String)
            m_strCertificateNo = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Field Name value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Field Name value.</returns>
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
    ''' <para>10/17/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property Name() As String
        Get
            Return m_strFieldName.Trim
        End Get
        Set(ByVal value As String)
            m_strFieldName = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Report Name.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Report Name value.</returns>
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
    ''' <para>10/17/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property Report() As String
        Get
            Return m_strReportName.Trim
        End Get
        Set(ByVal value As String)
            m_strReportName = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certificate Type Id.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>Certificate Type Id value.</returns>
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
    ''' <para>10/17/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property CertificateTypeId() As Integer
        Get
            Return m_intCertificateTypeId
        End Get
        Set(ByVal value As Integer)
            m_intCertificateTypeId = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Certificate Id.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>BarCode value.</returns>
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
    ''' <para>10/17/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property CertificateId() As Integer
        Get
            Return m_intCertificateId
        End Get
        Set(ByVal value As Integer)
            m_intCertificateId = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Id.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>Id value.</returns>
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
    ''' <para>10/17/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ID() As Integer
        Get
            Return m_intId
        End Get
        Set(ByVal value As Integer)
            m_intId = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Field label.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Field label value.</returns>
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
    ''' <para>10/17/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property Text() As String
        Get
            Return m_strFieldLabel.Trim
        End Get
        Set(ByVal value As String)
            m_strFieldLabel = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Field Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Field value.</returns>
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
    ''' <para>10/17/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property Value() As String
        Get
            Return m_strFieldValue.Trim
        End Get
        Set(ByVal value As String)
            m_strFieldValue = value
        End Set
    End Property

#End Region

End Class

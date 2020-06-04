
''' <summary>
''' Class contains status values for audit purposes.
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
''' <para>10/28/2019</para>
''' <para>Implemented code standarization.</para>
''' </description>
''' </item>
''' </list>
''' </remarks> 
Public Class ProductCountryCertReqStatus

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation

#Region "Members"

    ''' <summary>
    ''' variable to hold SKU Id.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intSKUID As Integer = 0

    ''' <summary>
    ''' variable to hold Material Num.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strMaterialNum As String = String.Empty

    ''' <summary>
    ''' variable to hold Size Stamp.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strSizeStamp As String = String.Empty

    ''' <summary>
    ''' variable to hold Country Id.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intCountryID As Integer = 0

    ''' <summary>
    ''' variable to hold Country Name.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strCountryName As String = String.Empty

    ''' <summary>
    ''' variable to hold CertType Name.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strCertTypeName As String = String.Empty

    ''' <summary>
    ''' variable to hold Req Status.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_blnReqStatus As Boolean = False

#End Region

#Region "Properties"

    ''' <summary>
    ''' Gets or sets SKU Id value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>SKU Id.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SKUID() As Integer
        Get
            Return m_intSKUID
        End Get
        Set(ByVal value As Integer)
            m_intSKUID = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Material Number value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Material Number.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property MaterialNumber() As String
        Get
            Return m_strMaterialNum
        End Get
        Set(ByVal value As String)
            m_strMaterialNum = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Size Stamp value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Size Stamp.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SizeStamp() As String
        Get
            Return m_strSizeStamp
        End Get
        Set(ByVal value As String)
            m_strSizeStamp = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Country Id value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>Country Id.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property CountryID() As Integer
        Get
            Return m_intCountryID
        End Get
        Set(ByVal value As Integer)
            m_intCountryID = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Country Name value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Country Name .</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property CountryName() As String
        Get
            Return m_strCountryName
        End Get
        Set(ByVal value As String)
            m_strCountryName = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets P Cert type Name value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Cert Type Name.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property CertTypeName() As String
        Get
            Return m_strCertTypeName
        End Get
        Set(ByVal value As String)
            m_strCertTypeName = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Req Status value.
    ''' </summary>
    ''' <value>Boolean</value>
    ''' <returns>Req Status.</returns>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ReqStatus() As Boolean
        Get
            Return m_blnReqStatus
        End Get
        Set(ByVal value As Boolean)
            m_blnReqStatus = value
        End Set
    End Property

#End Region

#Region "Constructors"

    ''' <summary>
    ''' Constructor for this class with parameters.
    ''' </summary>
    ''' <param name="p_blnReqStatus">Req Status</param>
    ''' <param name="p_intCountryID">Country Id</param>
    ''' <param name="p_intSKUID">SKU ID</param>
    ''' <param name="p_strCertTypeName">CertType Name</param>
    ''' <param name="p_strCountryName">Country name</param>
    ''' <param name="p_strMatlNum">Material Number</param>
    ''' <param name="p_strSizeStamp">Size Stamp</param>
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
    ''' <para>10/28/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Sub New(ByVal p_intSKUID As Integer, ByVal p_strMatlNum As String, ByVal p_strSizeStamp As String, _
                                    ByVal p_intCountryID As Integer, ByVal p_strCountryName As String, _
                                    ByVal p_strCertTypeName As String, ByVal p_blnReqStatus As Boolean)

        m_intSKUID = p_intSKUID
        m_strMaterialNum = p_strMatlNum
        m_strSizeStamp = p_strSizeStamp
        m_intCountryID = p_intCountryID
        m_strCountryName = p_strCountryName
        m_strCertTypeName = p_strCertTypeName
        m_blnReqStatus = p_blnReqStatus

    End Sub

#End Region

End Class

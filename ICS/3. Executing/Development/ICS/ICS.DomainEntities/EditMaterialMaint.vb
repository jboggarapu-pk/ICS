''' <summary>
''' Class contains Edit Material Maint properties,constructors.
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
Public Class EditMaterialMaint

#Region "Members"
    ''' <summary>
    ''' variable to hold SKU Id.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intSKUID As Integer

    ''' <summary>
    ''' variable to hold SKU.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strSKU As String = String.Empty

    ''' <summary>
    ''' variable to hold Speed Rating.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strSpeedrating As String = String.Empty

    ''' <summary>
    ''' variable to hold material Number.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strMaterialNumber As String = String.Empty

#End Region

#Region "Properties"

    ''' <summary>
    ''' Gets or sets SKU ID Value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>SKU ID value.</returns>
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
    Public Property SKUID() As Integer
        Get
            Return m_intSKUID
        End Get
        Set(ByVal value As Integer)
            m_intSKUID = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets SKU Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>SKU value.</returns>
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
    Public Property SKU() As String
        Get
            Return m_strSKU
        End Get
        Set(ByVal value As String)
            m_strSKU = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Speed Rating Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Speed Rating value.</returns>
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
    Public Property Speedrating() As String
        Get
            Return m_strSpeedrating
        End Get
        Set(ByVal value As String)
            m_strSpeedrating = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Material Number Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Material Number value.</returns>
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
    Public Property MaterialNumber() As String
        Get
            Return m_strMaterialNumber
        End Get
        Set(ByVal value As String)
            m_strMaterialNumber = value
        End Set
    End Property

#End Region

#Region "Constructors"

    ''' <summary>
    ''' Default Constructor.
    ''' </summary>
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
    Public Sub New()
    End Sub

    ''' <summary>
    ''' Constructor with Parameters.
    ''' </summary>
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
    Public Sub New(ByVal p_objEditMaterialMaint As EditMaterialMaint)
        m_intSKUID = p_objEditMaterialMaint.SKUID
        m_strSKU = p_objEditMaterialMaint.SKU
        m_strSpeedrating = p_objEditMaterialMaint.Speedrating
        m_strMaterialNumber = p_objEditMaterialMaint.MaterialNumber
    End Sub

    ''' <summary>
    ''' Constructor with Parameters.
    ''' </summary>
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
    Public Sub New(ByVal p_skuId As Integer, ByVal p_sku As String, _
                        ByVal p_speedrating As String, ByVal p_materialNumber As String)
        m_intSKUID = p_skuId
        m_strSKU = p_sku
        m_strSpeedrating = p_speedrating
        m_strMaterialNumber = p_materialNumber
    End Sub
#End Region

End Class

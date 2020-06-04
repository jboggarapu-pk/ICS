Public Class IMarkFamily

#Region "Members"
    ''' <summary>
    ''' variable to hold Family ID.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_intFamilyID As Integer

    ''' <summary>
    ''' variable to hold Family Code.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strFamilyCode As String

    ''' <summary>
    ''' variable to hold Family Desc.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strFamilyDesc As String

    ''' <summary>
    ''' variable to hold ApplicationCat.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strApplicationCat As String

    ''' <summary>
    ''' variable to hold Construction Type.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strConstructionType As String

    ''' <summary>
    ''' variable to hold Structure Type.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strStructureType As String

    ''' <summary>
    ''' variable to hold Mounting Type.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strMountingType As String

    ''' <summary>
    ''' variable to hold AspectrationCat.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strAspectratioCat As String

    ''' <summary>
    ''' variable to hold Speed ratingCat.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strSpeedratingCat As String

    ''' <summary>
    ''' variable to hold Load Index Cat.
    ''' </summary>
    ''' <remarks></remarks>
    Private m_strLoadindexCat As String

#End Region

#Region "Properties"

    ''' <summary>
    ''' Gets or sets Family Id value.
    ''' </summary>
    ''' <value>Integer</value>
    ''' <returns>Family ID.</returns>
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
    ''' <para>10/18/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property FamilyID() As Integer
        Get
            Return m_intFamilyID
        End Get
        Set(ByVal value As Integer)
            m_intFamilyID = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Family Code value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Family Code.</returns>
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
    ''' <para>10/18/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property FamilyCode() As String
        Get
            Return m_strFamilyCode
        End Get
        Set(ByVal value As String)
            m_strFamilyCode = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Family Desc Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Family Desc value.</returns>
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
    ''' <para>10/18/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property FamilyDesc() As String
        Get
            Return m_strFamilyDesc
        End Get
        Set(ByVal value As String)
            m_strFamilyDesc = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets ApplicationCat Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>ApplicationCat value.</returns>
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
    ''' <para>10/18/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ApplicationCat() As String
        Get
            Return m_strApplicationCat
        End Get
        Set(ByVal value As String)
            m_strApplicationCat = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Construction Type Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Construction Type value.</returns>
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
    ''' <para>10/18/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property ConstructionType() As String
        Get
            Return m_strConstructionType
        End Get
        Set(ByVal value As String)
            m_strConstructionType = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Structure Type Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Structure Type value.</returns>
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
    ''' <para>10/18/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property StructureType() As String
        Get
            Return m_strStructureType
        End Get
        Set(ByVal value As String)
            m_strStructureType = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Mounting Type Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Mounting Type value.</returns>
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
    ''' <para>10/18/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property MountingType() As String
        Get
            Return m_strMountingType
        End Get
        Set(ByVal value As String)
            m_strMountingType = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets AspectrationCat Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>AspectrationCat value.</returns>
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
    ''' <para>10/18/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property AspectratioCat() As String
        Get
            Return m_strAspectratioCat
        End Get
        Set(ByVal value As String)
            m_strAspectratioCat = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Speed ratingCat Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Speed ratingCat value.</returns>
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
    ''' <para>10/18/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property SpeedratingCat() As String
        Get
            Return m_strSpeedratingCat
        End Get
        Set(ByVal value As String)
            m_strSpeedratingCat = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets Load indexCat Value.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>Load indexCat value.</returns>
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
    ''' <para>10/18/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Property LoadindexCat() As String
        Get
            Return m_strLoadindexCat
        End Get
        Set(ByVal value As String)
            m_strLoadindexCat = value
        End Set
    End Property

#End Region


#Region "Constructors"

    Public Sub New()
    End Sub

    Public Sub New(ByVal p_objIMarkFamily As IMarkFamily)

        m_intFamilyID = p_objIMarkFamily.FamilyID
        m_strFamilyCode = p_objIMarkFamily.FamilyCode
        m_strFamilyDesc = p_objIMarkFamily.FamilyDesc
        m_strApplicationCat = p_objIMarkFamily.ApplicationCat
        m_strConstructionType = p_objIMarkFamily.ConstructionType
        m_strStructureType = p_objIMarkFamily.StructureType
        m_strMountingType = p_objIMarkFamily.MountingType
        m_strAspectratioCat = p_objIMarkFamily.AspectratioCat
        m_strSpeedratingCat = p_objIMarkFamily.SpeedratingCat
        m_strLoadindexCat = p_objIMarkFamily.LoadindexCat
    End Sub

    Public Sub New(ByVal p_familyID As Integer, ByVal p_familyCode As String, _
                        ByVal p_familyDesc As String, ByVal p_applicationCat As String, _
                        ByVal p_constructionType As String, ByVal p_structureType As String, _
                        ByVal p_mountingType As String, ByVal p_aspectratioCat As String, _
                        ByVal p_speedratingCat As String, ByVal p_loadindexCat As String)

        m_intFamilyID = p_familyID
        m_strFamilyCode = p_familyCode
        m_strFamilyDesc = p_familyDesc
        m_strApplicationCat = p_applicationCat
        m_strConstructionType = p_constructionType
        m_strStructureType = p_structureType
        m_strMountingType = p_mountingType
        m_strAspectratioCat = p_aspectratioCat
        m_strSpeedratingCat = p_speedratingCat
        m_strLoadindexCat = p_loadindexCat

    End Sub
#End Region

End Class
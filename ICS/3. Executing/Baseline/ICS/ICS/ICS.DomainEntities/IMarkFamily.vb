Public Class IMarkFamily

#Region "Members"

    Private m_intFamilyID As Integer
    Private m_strFamilyCode As String
    Private m_strFamilyDesc As String
    Private m_strApplicationCat As String
    Private m_strConstructionType As String
    Private m_strStructureType As String
    Private m_strMountingType As String
    Private m_strAspectratioCat As String
    Private m_strSpeedratingCat As String
    Private m_strLoadindexCat As String

#End Region

#Region "Properties"

    Public Property FamilyID() As Integer
        Get
            Return m_intFamilyID
        End Get
        Set(ByVal value As Integer)
            m_intFamilyID = value
        End Set
    End Property

    Public Property FamilyCode() As String
        Get
            Return m_strFamilyCode
        End Get
        Set(ByVal value As String)
            m_strFamilyCode = value
        End Set
    End Property

    Public Property FamilyDesc() As String
        Get
            Return m_strFamilyDesc
        End Get
        Set(ByVal value As String)
            m_strFamilyDesc = value
        End Set
    End Property

    Public Property ApplicationCat() As String
        Get
            Return m_strApplicationCat
        End Get
        Set(ByVal value As String)
            m_strApplicationCat = value
        End Set
    End Property

    Public Property ConstructionType() As String
        Get
            Return m_strConstructionType
        End Get
        Set(ByVal value As String)
            m_strConstructionType = value
        End Set
    End Property

    Public Property StructureType() As String
        Get
            Return m_strStructureType
        End Get
        Set(ByVal value As String)
            m_strStructureType = value
        End Set
    End Property

    Public Property MountingType() As String
        Get
            Return m_strMountingType
        End Get
        Set(ByVal value As String)
            m_strMountingType = value
        End Set
    End Property

    Public Property AspectratioCat() As String
        Get
            Return m_strAspectratioCat
        End Get
        Set(ByVal value As String)
            m_strAspectratioCat = value
        End Set
    End Property

    Public Property SpeedratingCat() As String
        Get
            Return m_strSpeedratingCat
        End Get
        Set(ByVal value As String)
            m_strSpeedratingCat = value
        End Set
    End Property

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
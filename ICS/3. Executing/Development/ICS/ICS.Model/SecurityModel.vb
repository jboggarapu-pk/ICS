Imports System.Security.Principal

Imports CooperTire.Security
Imports CooperTire.ICS.Common

''' <summary>
''' Security Model to support ICS resource authorization to the current user.
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
''' <term>Sujitha</term>
''' <description>
''' <para>10/04/2019</para>
''' <para>Implemented Coding Standards</para>
''' </description>
''' </item> 
''' </list>
''' </remarks> 
Public Class SecurityModel

    ''' <summary>
    '''  Configured AD user group names to support ICS authorization.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    
    Public Class GroupName

        Public Shared ReadOnly MarketingUser As String = "MarketingUser"
        Public Shared ReadOnly QualityUser As String = "QualityUser"
        Public Shared ReadOnly QualityManager As String = "QualityManager"

        Public Shared ReadOnly StrictlyInquiryUser As String = "StrictlyInquiryUser"
        Public Shared ReadOnly ITUser As String = "ITUser"

        Public Shared Items() As String = Nothing

        Shared Sub New()

            ' Get user groups configured for ICS:

            If Not AppSettingsAid.MarketingUserGroup = String.Empty Then
                MarketingUser = AppSettingsAid.MarketingUserGroup
            End If
            If Not AppSettingsAid.QualityUserGroup = String.Empty Then
                QualityUser = AppSettingsAid.QualityUserGroup
            End If
            If Not AppSettingsAid.QualityManagerGroup = String.Empty Then
                QualityManager = AppSettingsAid.QualityManagerGroup
            End If
            If Not AppSettingsAid.StrictlyInquiryGroup = String.Empty Then
                StrictlyInquiryUser = AppSettingsAid.StrictlyInquiryGroup
            End If

            If Not AppSettingsAid.ITGroup = String.Empty Then
                ITUser = AppSettingsAid.ITGroup
            End If

            Items = New String() {ITUser, QualityManager, QualityUser, MarketingUser, StrictlyInquiryUser}

        End Sub

    End Class

#Region "Members"


    ''' <summary>
    ''' Variable to hold Oracle login.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared m_objOraLogin As GenericOraLoginProvider = Nothing


    ''' <summary>
    ''' Variable to hold Username.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared m_strUserName As String = String.Empty

    ''' <summary>
    ''' Variable to hold Group Name.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared m_strGroupName As String = String.Empty


    ''' <summary>
    ''' Variable to hold Dictionary Views group.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared m_dictNoGoGroupToViews As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))


    ''' <summary>
    ''' Variable to hold Dictionary menus group.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared m_dictNoGoGroupToMenus As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

    'NOTE - TEST ONLY

    ''' <summary>
    ''' Variable to hold boolean test.
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared blnTest As Boolean = False

#End Region

#Region "Constructors / Destructors"

    ''' <summary>
    '''  Constructor to initiate objects.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>12/19/2019</para>
    ''' <para>Fixed ICS Issues</para>
    ''' </description>
    ''' </item>  
    ''' </list>
    ''' </remarks>
    Shared Sub New()

        FillNoGoGroupToViews()
        FillNoGoGroupToMenus()

        If blnTest Then
            m_strUserName = GetUserName()

        Else
            m_objOraLogin = New GenericOraLoginProvider()
            m_strUserName = m_objOraLogin.ActiveDirectoryProvider.UserName

        End If
        m_strGroupName = GetGroupName()


    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Variable to hold object username.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property UserName() As String
        Get
            Return m_strUserName
        End Get
    End Property

    ''' <summary>
    ''' Variable to hold user group.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property UserGroup() As String
        Get
            Return m_strGroupName
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    '''  Method to Check if the user is in group.
    ''' </summary>
    ''' <returns>Boolean</returns> 
    ''' <param name="p_strGroupName">Group name.</param>    
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>12/19/2019</para>
    ''' <para>Fixed ICS Issues</para>
    ''' </description>
    ''' </item>  
    ''' </list>
    ''' </remarks>    
    Private Shared Function IsInGroup(ByVal p_strGroupName As String) As Boolean

        Dim blnIsInGroup As Boolean = False
        'NOTE - TEST ONLY
        If blnTest Then
            blnIsInGroup = True
        Else
            blnIsInGroup = m_objOraLogin.ActiveDirectoryProvider.IsInGroup(p_strGroupName)
        End If

        Return blnIsInGroup

    End Function

    ''' <summary>
    '''  Method to Get the user group name.
    ''' </summary>
    ''' <returns>string</returns> 
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>    
    Public Shared Function GetGroupName() As String

        Dim strGroupName As String = String.Empty
        If blnTest Then
            strGroupName = "APP_ICS_IT"
        Else
            For Each name As String In GroupName.Items
                If Not IsInGroup(name) Then Continue For

                strGroupName = name
                Exit For
            Next
        End If
        Return strGroupName

    End Function

    ''' <summary>
    '''  Method to Get username.
    ''' </summary>
    ''' <returns>string</returns> 
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Shared Function GetUserName() As String

        Dim strUserName As String = String.Empty

        If blnTest Then
            strUserName = "PROKARMA"
        Else
            Dim objOraLogin As New GenericOraLoginProvider()
            strUserName = objOraLogin.ActiveDirectoryProvider.UserName
        End If
        Return strUserName
    End Function

    ''' <summary>
    '''  Method to  Is user authorized to access the view.
    ''' </summary>
    ''' <returns>Boolean</returns> 
    ''' <param name="p_strViewName">View Name.</param>
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>12/19/2019</para>
    ''' <para>Fixed ICS Issues</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>    
    Public Shared Function IsUserAuthorized(ByVal p_strViewName As String) As Boolean

        Dim blnIsAuthorized As Boolean = False

        If blnTest Then
            blnIsAuthorized = True
        Else
            If m_dictNoGoGroupToViews.ContainsKey(GetGroupName()) AndAlso Not m_dictNoGoGroupToViews(GetGroupName()).Contains(p_strViewName) Then
                blnIsAuthorized = True
            End If
        End If
        Return blnIsAuthorized
    End Function

    ''' <summary>
    '''  Method to  Get no-go-list from coma separated value string, e.g., app setting.
    ''' </summary>
    ''' <returns>Dataset</returns> 
    ''' <param name="p_strComaSeparatedValues">Comma seperated values.</param>
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Shared Function GetNoGoList(ByVal p_strComaSeparatedValues As String) As List(Of String)

        Dim listValues As List(Of String) = New List(Of String)

        Dim values As String = p_strComaSeparatedValues
        Dim split As String() = values.Split(New [Char]() {CChar(",")})

        For Each val As String In split
            If val.Trim() = String.Empty Then Continue For

            listValues.Add(val)
        Next val

        Return listValues

    End Function

    ''' <summary>
    '''  Method to Fill list of groups and its not authorized views.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>    
    Private Shared Sub FillNoGoGroupToViews()

        Dim listNoGoViewNames As List(Of String)

        listNoGoViewNames = GetNoGoList(AppSettingsAid.MarketingUserGroup_NoGoViews)
        m_dictNoGoGroupToViews.Add(GroupName.MarketingUser, listNoGoViewNames)

        listNoGoViewNames = GetNoGoList(AppSettingsAid.QualityUserGroup_NoGoViews)
        m_dictNoGoGroupToViews.Add(GroupName.QualityUser, listNoGoViewNames)

        listNoGoViewNames = GetNoGoList(AppSettingsAid.QualityManagerGroup_NoGoViews)
        m_dictNoGoGroupToViews.Add(GroupName.QualityManager, listNoGoViewNames)

        listNoGoViewNames = GetNoGoList(AppSettingsAid.StrictlyInquiryGroup_NoGoViews)
        m_dictNoGoGroupToViews.Add(GroupName.StrictlyInquiryUser, listNoGoViewNames)

        listNoGoViewNames = New List(Of String)
        m_dictNoGoGroupToViews.Add(GroupName.ITUser, listNoGoViewNames)

    End Sub

    ''' <summary>
    '''  Method to Fill list of groups and its not authorized mkenu items.
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Shared Sub FillNoGoGroupToMenus()

        Dim listNoGoMenuItemValues As List(Of String)

        listNoGoMenuItemValues = GetNoGoList(AppSettingsAid.MarketingUserGroup_NoGoMenus)
        m_dictNoGoGroupToMenus.Add(GroupName.MarketingUser, listNoGoMenuItemValues)

        listNoGoMenuItemValues = GetNoGoList(AppSettingsAid.QualityUserGroup_NoGoMenus)
        m_dictNoGoGroupToMenus.Add(GroupName.QualityUser, listNoGoMenuItemValues)

        listNoGoMenuItemValues = GetNoGoList(AppSettingsAid.QualityManagerGroup_NoGoMenus)
        m_dictNoGoGroupToMenus.Add(GroupName.QualityManager, listNoGoMenuItemValues)

        listNoGoMenuItemValues = GetNoGoList(AppSettingsAid.StrictlyInquiryGroup_NoGoMenus)
        m_dictNoGoGroupToMenus.Add(GroupName.StrictlyInquiryUser, listNoGoMenuItemValues)

        listNoGoMenuItemValues = New List(Of String)
        m_dictNoGoGroupToMenus.Add(GroupName.ITUser, listNoGoMenuItemValues)

    End Sub

    ''' <summary>
    '''  Method to Get user's unauthorized menu items.
    ''' </summary>
    ''' <returns>List of strings</returns> 
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Shared Function GetUnauthorizedMenuItems() As List(Of String)

        Dim listUnauthorizedMenuItems As List(Of String) = m_dictNoGoGroupToMenus(GetGroupName())
        Return listUnauthorizedMenuItems

    End Function

#End Region

End Class

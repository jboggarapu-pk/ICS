Imports System.Configuration

''' <summary>
''' Contains getting configuration values.
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
''' <para>09/17/2019</para>
''' <para>Implemented code standardization.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class AppSettingsAid

#Region "AppSettingsAid Constants"

    ''' <summary>
    ''' Constant to hold string Quality.
    ''' </summary>
    ''' <remarks></remarks>
    Private Const QualityText As String = "Quality"

#End Region

#Region "Application Settings"

    ''' <summary>
    ''' Gets the environment setting from the Web.Config
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String (Environment)</returns>
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
    ''' <para>09/17/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>   
    Public Shared ReadOnly Property Environment() As String
        Get
            Const EnvironmentText As String = "Environment"
            Dim strEnvironment As String = ConfigurationManager.AppSettings(EnvironmentText)
            If Not strEnvironment Is Nothing Then
                Return strEnvironment
            Else
                Return String.Empty
            End If
        End Get
    End Property

    ''' <summary>
    ''' Gets the SMTP host server name from the Web.Config to use for e-mail notification
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String (SMTP Host)</returns>
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
    ''' <para>09/17/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Shared ReadOnly Property SmtpHost() As String
        Get
            Const SmtpHostText As String = "SmtpHost"
            Dim strSmptHost As String = ConfigurationManager.AppSettings(SmtpHostText)
            If Not strSmptHost Is Nothing Then
                Return strSmptHost
            Else
                Return String.Empty
            End If
        End Get
    End Property

    ''' <summary>
    ''' Gets the correct e-mail address from the Web.Config to use as the "From" address for email notification.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String (E-mail address)</returns>
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
    ''' <para>09/17/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Shared ReadOnly Property EmailFrom(ByVal p_strFunction As String) As String
        Get
            Const EmailFromText As String = "EmailFrom"
            Const EmailFromMktText As String = "EmailFromMkt"

            If p_strFunction = QualityText Then
                Dim strFromAddress As String = ConfigurationManager.AppSettings(EmailFromText)
                If Not strFromAddress Is Nothing Then
                    Return strFromAddress
                Else
                    Return String.Empty
                End If
            Else 'Marketing
                Dim strFromMktAddress As String = ConfigurationManager.AppSettings(EmailFromMktText)
                If Not strFromMktAddress Is Nothing Then
                    Return strFromMktAddress
                Else
                    Return String.Empty
                End If
            End If
        End Get
    End Property

    ''' <summary>
    ''' Gets the correct e-mail address from the Web.Config to use for email notification.
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String (E-mail address)</returns>
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
    ''' <para>09/17/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Shared ReadOnly Property EmailTo(ByVal p_strFunction As String) As String
        Get
            Const ProductionText As String = "PRODUCTION"
            Const EmailToText As String = "EmailTo"
            Const EmailToMktText As String = "EmailToMkt"
            Const IcsSupportEmail As String = "ICS_SUPPORT@COOPERTIRE.COM"

            Dim strEnvironment As String = Environment()
            If Not strEnvironment = String.Empty Then
                If strEnvironment.Trim().ToUpper() = ProductionText Then
                    If p_strFunction = QualityText Then
                        Dim strAddress As String = ConfigurationManager.AppSettings(EmailToText)
                        If Not strAddress Is Nothing Then
                            Return strAddress
                        Else
                            Return String.Empty
                        End If
                    Else 'Marketing
                        Dim strAddress As String = ConfigurationManager.AppSettings(EmailToMktText)
                        If Not strAddress Is Nothing Then
                            Return strAddress
                        Else
                            Return String.Empty
                        End If
                    End If
                Else
                    Return IcsSupportEmail
                End If
            Else
                If p_strFunction = QualityText Then
                    Dim strAddress As String = ConfigurationManager.AppSettings(EmailToText)
                    If Not strAddress Is Nothing Then
                        Return strAddress
                    Else
                        Return String.Empty
                    End If
                Else  'Marketing
                    Dim strAddress As String = ConfigurationManager.AppSettings(EmailToMktText)
                    If Not strAddress Is Nothing Then
                        Return strAddress
                    Else
                        Return String.Empty
                    End If
                End If
            End If
        End Get
    End Property

    ''' <summary>
    ''' Get report file name
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String (Report name)</returns>
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
    ''' <para>09/17/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Shared ReadOnly Property GetReportFileName(ByVal p_enuReport As NameAid.Report) As String
        Get
            Dim strReport As String = ConfigurationManager.AppSettings(p_enuReport.ToString())
            If Not strReport Is Nothing Then
                Return strReport
            Else
                Return String.Empty
            End If
        End Get
    End Property

    ''' <summary>
    ''' Gets the URL to re-direct to when the session expires from the Web.Config
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String (URL)</returns>
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
    ''' <para>09/17/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Shared ReadOnly Property GetSessionExpiredRedirectUrl() As String
        Get
            Const SessionExpiredRedirectText As String = "SessionExpiredRedirect"
            Dim strSessionExpiredRedirectUrl As String = ConfigurationManager.AppSettings(SessionExpiredRedirectText)
            If Not strSessionExpiredRedirectUrl Is Nothing Then
                Return strSessionExpiredRedirectUrl
            Else
                Return String.Empty
            End If
        End Get
    End Property

    'Added GetUseSAP Property as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation.
    ''' <summary>
    ''' Gets UseSAP to pull SAP data from the Web.Config
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String (Y/N)</returns>
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
    ''' <para>09/17/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Shared ReadOnly Property GetUseSAP() As String
        Get
            Const UseSapText As String = "UseSap"
            Dim strUseSap As String = ConfigurationManager.AppSettings(UseSapText)
            If Not strUseSap Is Nothing Then
                Return strUseSap
            Else
                Return String.Empty
            End If
        End Get
    End Property

    'Added GetUseTracs Property as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation.
    ''' <summary>
    ''' Gets UseSAP to pull Tracs data from the Web.Config
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String (Y/N)</returns>
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
    ''' <para>09/17/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Shared ReadOnly Property GetUseTracs() As String
        Get
            Const UseTracsText As String = "UseTracs"
            Dim strUseTracs As String = ConfigurationManager.AppSettings(UseTracsText)
            If Not strUseTracs Is Nothing Then
                Return strUseTracs
            Else
                Return String.Empty
            End If
        End Get
    End Property
#End Region

#Region "Security"

    ''' <summary>
    ''' Get Marketing User AD group name
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String (Marketing User Group)</returns>
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
    ''' <para>09/17/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>    
    Public Shared ReadOnly Property MarketingUserGroup() As String
        Get
            Const MarketingUserGroupText As String = "MarketingUserGroup"
            Dim strMarketingUserGroup As String = ConfigurationManager.AppSettings(MarketingUserGroupText)
            If Not strMarketingUserGroup Is Nothing Then
                Return strMarketingUserGroup
            Else
                Return String.Empty
            End If
        End Get
    End Property

    ''' <summary>
    '''  Get configured AD group name
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String (Quality User Group)</returns>
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
    ''' <para>09/17/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Shared ReadOnly Property QualityUserGroup() As String
        Get
            Const QualityUserGroupText As String = "QualityUserGroup"
            Dim strSetting As String = String.Empty ' default
            Dim strQualityUserGroup As String = ConfigurationManager.AppSettings(QualityUserGroupText)
            If Not strQualityUserGroup Is Nothing Then
                strSetting = strQualityUserGroup
            End If
            Return strSetting
        End Get
    End Property

    ''' <summary>
    '''  Get configured AD group name
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String (Quality Manager Group)</returns>
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
    ''' <para>09/17/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Shared ReadOnly Property QualityManagerGroup() As String
        Get
            Const QualityManagerGroupText As String = "QualityManagerGroup"
            Dim strSetting As String = String.Empty ' default
            Dim strQualityManagerGroup As String = ConfigurationManager.AppSettings(QualityManagerGroupText)
            If Not strQualityManagerGroup Is Nothing Then
                strSetting = strQualityManagerGroup
            End If
            Return strSetting
        End Get
    End Property

    ''' <summary>
    '''  Get configured AD group name
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String (Strictly Inquiry Group)</returns>
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
    ''' <para>09/17/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Shared ReadOnly Property StrictlyInquiryGroup() As String
        Get
            Const StrictlyInquiryGroupText As String = "StrictlyInquiryGroup"
            Dim strSetting As String = String.Empty ' default
            Dim strStrictlyInquiryGroup As String = ConfigurationManager.AppSettings(StrictlyInquiryGroupText)
            If Not strStrictlyInquiryGroup Is Nothing Then
                strSetting = strStrictlyInquiryGroup
            End If
            Return strSetting
        End Get
    End Property

    ''' <summary>
    '''  Get configured AD group name
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String (IT Group)</returns>
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
    ''' <para>09/17/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Shared ReadOnly Property ITGroup() As String
        Get
            Const ITGroupText As String = "ITGroup"
            Dim strSetting As String = String.Empty ' default
            Dim strITGroup As String = ConfigurationManager.AppSettings(ITGroupText)
            If Not strITGroup Is Nothing Then
                strSetting = strITGroup
            End If
            Return strSetting
        End Get
    End Property

    ''' <summary>
    '''  Get configured marketing AD group views name
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
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
    ''' <para>09/17/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Shared ReadOnly Property MarketingUserGroup_NoGoViews() As String
        Get
            Const MarketingUserGroupNoGoViewsText As String = "MarketingUserGroup_NoGoViews"
            Dim strSetting As String = String.Empty ' default
            Dim strMarketingUserGroupNoGoViews As String = ConfigurationManager.AppSettings(MarketingUserGroupNoGoViewsText)
            If Not strMarketingUserGroupNoGoViews Is Nothing Then
                strSetting = strMarketingUserGroupNoGoViews
            End If
            Return strSetting
        End Get
    End Property

    ''' <summary>
    '''  Get configured marketing AD group menus name
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
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
    ''' <para>09/17/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Shared ReadOnly Property MarketingUserGroup_NoGoMenus() As String
        Get
            Const MarketingUserGroupNoGoMenusText As String = "MarketingUserGroup_NoGoMenus"
            Dim strSetting As String = String.Empty ' default
            Dim strMarketingUserGroupNoGoMenus As String = ConfigurationManager.AppSettings(MarketingUserGroupNoGoMenusText)
            If Not strMarketingUserGroupNoGoMenus Is Nothing Then
                strSetting = strMarketingUserGroupNoGoMenus
            End If
            Return strSetting
        End Get
    End Property

    ''' <summary>
    '''  Get configured Quantity AD group views name
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
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
    ''' <para>09/17/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Shared ReadOnly Property QualityUserGroup_NoGoViews() As String
        Get
            Const QualityUserGroupNoGoViewsText As String = "QualityUserGroup_NoGoViews"
            Dim strSetting As String = String.Empty ' default
            Dim strQualityUserGroupNoGoViews As String = ConfigurationManager.AppSettings(QualityUserGroupNoGoViewsText)
            If Not strQualityUserGroupNoGoViews Is Nothing Then
                strSetting = strQualityUserGroupNoGoViews
            End If
            Return strSetting
        End Get
    End Property

    ''' <summary>
    '''  Get configured Quantity AD group menus name
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
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
    ''' <para>09/17/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Shared ReadOnly Property QualityUserGroup_NoGoMenus() As String
        Get
            Const QualityUserGroupNoGoMenusText As String = "QualityUserGroup_NoGoMenus"
            Dim strSetting As String = String.Empty ' default
            Dim strQualityUserGroupNoGoMenus As String = ConfigurationManager.AppSettings(QualityUserGroupNoGoMenusText)
            If Not strQualityUserGroupNoGoMenus Is Nothing Then
                strSetting = strQualityUserGroupNoGoMenus
            End If
            Return strSetting
        End Get
    End Property

    ''' <summary>
    '''  Get configured Quantity manager AD group views name
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
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
    ''' <para>09/17/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Shared ReadOnly Property QualityManagerGroup_NoGoViews() As String
        Get
            Const QualityManagerGroupNoGoViewsText As String = "QualityManagerGroup_NoGoViews"
            Dim strSetting As String = String.Empty ' default
            Dim strQualityManagerGroupNoGoViews As String = ConfigurationManager.AppSettings(QualityManagerGroupNoGoViewsText)
            If Not strQualityManagerGroupNoGoViews Is Nothing Then
                strSetting = strQualityManagerGroupNoGoViews
            End If
            Return strSetting
        End Get
    End Property

    ''' <summary>
    '''  Get configured Quantity manager AD group menus name
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
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
    ''' <para>09/17/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Shared ReadOnly Property QualityManagerGroup_NoGoMenus() As String
        Get
            Const QualityManagerGroupNoGoMenusText As String = "QualityManagerGroup_NoGoMenus"
            Dim strSetting As String = String.Empty ' default
            Dim strQualityManagerGroupNoGoMenus As String = ConfigurationManager.AppSettings(QualityManagerGroupNoGoMenusText)
            If Not strQualityManagerGroupNoGoMenus Is Nothing Then
                strSetting = strQualityManagerGroupNoGoMenus
            End If
            Return strSetting
        End Get
    End Property

    ''' <summary>
    '''  Get configured Strictly Inquiry AD group views name
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
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
    ''' <para>09/17/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Shared ReadOnly Property StrictlyInquiryGroup_NoGoViews() As String
        Get
            Const StrictlyInquiryGroupNoGoViewsText As String = "StrictlyInquiryGroup_NoGoViews"
            Dim strSetting As String = String.Empty ' default
            Dim strStrictlyInquiryGroupNoGoViews As String = ConfigurationManager.AppSettings(StrictlyInquiryGroupNoGoViewsText)
            If Not strStrictlyInquiryGroupNoGoViews Is Nothing Then
                strSetting = strStrictlyInquiryGroupNoGoViews
            End If
            Return strSetting
        End Get
    End Property

    ''' <summary>
    '''  Get configured Strictly Inquiry AD group menus name
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
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
    ''' <para>09/17/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Shared ReadOnly Property StrictlyInquiryGroup_NoGoMenus() As String
        Get
            Const StrictlyInquiryGroupNoGoMenusText As String = "StrictlyInquiryGroup_NoGoMenus"
            Dim strSetting As String = String.Empty ' default
            Dim strStrictlyInquiryGroupNoGoMenus As String = ConfigurationManager.AppSettings(StrictlyInquiryGroupNoGoMenusText)
            If Not strStrictlyInquiryGroupNoGoMenus Is Nothing Then
                strSetting = strStrictlyInquiryGroupNoGoMenus
            End If
            Return strSetting
        End Get
    End Property

    ''' <summary>
    '''  Get configured IT AD group views name
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
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
    ''' <para>09/17/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Shared ReadOnly Property ITGroup_NoGoViews() As String
        Get
            Const ITGroupNoGoViewsText As String = "ITGroup_NoGoViews"
            Dim strSetting As String = String.Empty ' default
            Dim strITGroupNoGoViews As String = ConfigurationManager.AppSettings(ITGroupNoGoViewsText)
            If Not strITGroupNoGoViews Is Nothing Then
                strSetting = strITGroupNoGoViews
            End If
            Return strSetting
        End Get
    End Property

    ''' <summary>
    '''  Get configured IT AD group menus name
    ''' </summary>
    ''' <value>String</value>
    ''' <returns>String</returns>
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
    ''' <para>09/17/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Shared ReadOnly Property ITGroup_NoGoMenus() As String
        Get
            Const ITGroupNoGoMenusText As String = "ITGroup_NoGoMenus"
            Dim strSetting As String = String.Empty ' default
            Dim strITGroupNoGoMenus As String = ConfigurationManager.AppSettings(ITGroupNoGoMenusText)
            If Not strITGroupNoGoMenus Is Nothing Then
                strSetting = strITGroupNoGoMenus
            End If
            Return strSetting
        End Get
    End Property

#End Region

End Class

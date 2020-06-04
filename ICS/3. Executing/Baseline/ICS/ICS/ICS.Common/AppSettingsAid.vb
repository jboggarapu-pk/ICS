Imports System.Configuration

Public Class AppSettingsAid

#Region "Application Settings"
    ''' <summary>
    ''' Gets the environment setting from the Web.Config
    ''' </summary>
    ''' <value></value>
    ''' <returns>String (Environment)</returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property Environment() As String
        Get
            Dim strEnvironment As String = ConfigurationManager.AppSettings("Environment")
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
    ''' <value></value>
    ''' <returns>String (SMTP Host)</returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property SmtpHost() As String
        Get
            Dim strHost As String = ConfigurationManager.AppSettings("SmtpHost")
            If Not strHost Is Nothing Then
                Return strHost
            Else
                Return String.Empty
            End If
        End Get
    End Property

    ''' <summary>
    ''' Gets the correct e-mail address from the Web.Config to use as the "From" address for email notification.
    ''' </summary>
    ''' <value></value>
    ''' <returns>String (E-mail address)</returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property EmailFrom(ByVal p_strFunction As String) As String
        Get
            If p_strFunction = "Quality" Then
                Dim strAddress As String = ConfigurationManager.AppSettings("EmailFrom")
                If Not strAddress Is Nothing Then
                    Return strAddress
                Else
                    Return String.Empty
                End If
            Else 'Marketing
                Dim strAddress As String = ConfigurationManager.AppSettings("EmailFromMkt")
                If Not strAddress Is Nothing Then
                    Return strAddress
                Else
                    Return String.Empty
                End If
            End If
        End Get
    End Property

    ''' <summary>
    ''' Gets the correct e-mail address from the Web.Config to use for email notification.
    ''' </summary>
    ''' <value></value>
    ''' <returns>String (E-mail address)</returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property EmailTo(ByVal p_strFunction As String) As String
        Get
            Dim strEnvironment As String = Environment()
            If Not strEnvironment = String.Empty Then
                If strEnvironment.Trim().ToUpper() = "PRODUCTION" Then
                    If p_strFunction = "Quality" Then
                        Dim strAddress As String = ConfigurationManager.AppSettings("EmailTo")
                        If Not strAddress Is Nothing Then
                            Return strAddress
                        Else
                            Return String.Empty
                        End If
                    Else 'Marketing
                        Dim strAddress As String = ConfigurationManager.AppSettings("EmailToMkt")
                        If Not strAddress Is Nothing Then
                            Return strAddress
                        Else
                            Return String.Empty
                        End If
                    End If
                Else
                    'Return "JLCZERNIAK@COOPERTIRE.COM"
                    Return "ICS_SUPPORT@COOPERTIRE.COM"
                End If
            Else
                If p_strFunction = "Quality" Then
                    Dim strAddress As String = ConfigurationManager.AppSettings("EmailTo")
                    If Not strAddress Is Nothing Then
                        Return strAddress
                    Else
                        Return String.Empty
                    End If
                Else  'Marketing
                    Dim strAddress As String = ConfigurationManager.AppSettings("EmailToMkt")
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
    ''' <param name="p_enuReport"></param>
    ''' <returns>String (Report name)</returns>
    ''' <remarks></remarks>
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
    ''' <returns>String (URL)</returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property GetSessionExpiredRedirectUrl() As String
        Get
            Dim strUrl As String = ConfigurationManager.AppSettings("SessionExpiredRedirect")
            If Not strUrl Is Nothing Then
                Return strUrl
            Else
                Return String.Empty
            End If
        End Get
    End Property

    'Added GetUseSAP Property as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation.
    ''' <summary>
    ''' Gets UseSAP to pull SAP data from the Web.Config
    ''' </summary>
    ''' <returns>String (Y/N)</returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property GetUseSAP() As String
        Get
            Dim strUseSap As String = ConfigurationManager.AppSettings("UseSap")
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
    ''' <returns>String (Y/N)</returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property GetUseTracs() As String
        Get
            Dim strUseTracs As String = ConfigurationManager.AppSettings("UseTracs")
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
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property MarketingUserGroup() As String
        Get
            Dim strGroup As String = ConfigurationManager.AppSettings("MarketingUserGroup")
            If Not strGroup Is Nothing Then
                Return strGroup
            Else
                Return String.Empty
            End If
        End Get
    End Property

    ''' <summary>
    ''' Get configured AD group name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property QualityUserGroup() As String
        Get
            Dim strSetting As String = String.Empty ' default
            Dim str As String = ConfigurationManager.AppSettings("QualityUserGroup")
            If Not str Is Nothing Then
                strSetting = str
            End If
            Return strSetting
        End Get
    End Property

    ''' <summary>
    ''' Get configured AD group name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property QualityManagerGroup() As String
        Get
            Dim strSetting As String = String.Empty ' default
            Dim str As String = ConfigurationManager.AppSettings("QualityManagerGroup")
            If Not str Is Nothing Then
                strSetting = str
            End If
            Return strSetting
        End Get
    End Property

    ''' <summary>
    ''' Get configured AD group name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property StrictlyInquiryGroup() As String
        Get
            Dim strSetting As String = String.Empty ' default
            Dim str As String = ConfigurationManager.AppSettings("StrictlyInquiryGroup")
            If Not str Is Nothing Then
                strSetting = str
            End If
            Return strSetting
        End Get
    End Property

    ''' <summary>
    ''' Get configured AD group name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property ITGroup() As String
        Get
            Dim strSetting As String = String.Empty ' default
            Dim str As String = ConfigurationManager.AppSettings("ITGroup")
            If Not str Is Nothing Then
                strSetting = str
            End If
            Return strSetting
        End Get
    End Property

    Public Shared ReadOnly Property MarketingUserGroup_NoGoViews() As String
        Get
            Dim strSetting As String = String.Empty ' default
            Dim str As String = ConfigurationManager.AppSettings("MarketingUserGroup_NoGoViews")
            If Not str Is Nothing Then
                strSetting = str
            End If
            Return strSetting
        End Get
    End Property

    Public Shared ReadOnly Property MarketingUserGroup_NoGoMenus() As String
        Get
            Dim strSetting As String = String.Empty ' default
            Dim str As String = ConfigurationManager.AppSettings("MarketingUserGroup_NoGoMenus")
            If Not str Is Nothing Then
                strSetting = str
            End If
            Return strSetting
        End Get
    End Property

    Public Shared ReadOnly Property QualityUserGroup_NoGoViews() As String
        Get
            Dim strSetting As String = String.Empty ' default
            Dim str As String = ConfigurationManager.AppSettings("QualityUserGroup_NoGoViews")
            If Not str Is Nothing Then
                strSetting = str
            End If
            Return strSetting
        End Get
    End Property

    Public Shared ReadOnly Property QualityUserGroup_NoGoMenus() As String
        Get
            Dim strSetting As String = String.Empty ' default
            Dim str As String = ConfigurationManager.AppSettings("QualityUserGroup_NoGoMenus")
            If Not str Is Nothing Then
                strSetting = str
            End If
            Return strSetting
        End Get
    End Property

    Public Shared ReadOnly Property QualityManagerGroup_NoGoViews() As String
        Get
            Dim strSetting As String = String.Empty ' default
            Dim str As String = ConfigurationManager.AppSettings("QualityManagerGroup_NoGoViews")
            If Not str Is Nothing Then
                strSetting = str
            End If
            Return strSetting
        End Get
    End Property

    Public Shared ReadOnly Property QualityManagerGroup_NoGoMenus() As String
        Get
            Dim strSetting As String = String.Empty ' default
            Dim str As String = ConfigurationManager.AppSettings("QualityManagerGroup_NoGoMenus")
            If Not str Is Nothing Then
                strSetting = str
            End If
            Return strSetting
        End Get
    End Property

    Public Shared ReadOnly Property StrictlyInquiryGroup_NoGoViews() As String
        Get
            Dim strSetting As String = String.Empty ' default
            Dim str As String = ConfigurationManager.AppSettings("StrictlyInquiryGroup_NoGoViews")
            If Not str Is Nothing Then
                strSetting = str
            End If
            Return strSetting
        End Get
    End Property

    Public Shared ReadOnly Property StrictlyInquiryGroup_NoGoMenus() As String
        Get
            Dim strSetting As String = String.Empty ' default
            Dim str As String = ConfigurationManager.AppSettings("StrictlyInquiryGroup_NoGoMenus")
            If Not str Is Nothing Then
                strSetting = str
            End If
            Return strSetting
        End Get
    End Property

    Public Shared ReadOnly Property ITGroup_NoGoViews() As String
        Get
            Dim strSetting As String = String.Empty ' default
            Dim str As String = ConfigurationManager.AppSettings("ITGroup_NoGoViews")
            If Not str Is Nothing Then
                strSetting = str
            End If
            Return strSetting
        End Get
    End Property

    Public Shared ReadOnly Property ITGroup_NoGoMenus() As String
        Get
            Dim strSetting As String = String.Empty ' default
            Dim str As String = ConfigurationManager.AppSettings("ITGroup_NoGoMenus")
            If Not str Is Nothing Then
                strSetting = str
            End If
            Return strSetting
        End Get
    End Property

#End Region
End Class

Imports CooperTire.ICS.Presenter
Imports CooperTire.ICS.Common

' Implements IHomeView to present ICS starting point
Partial Public Class Home
    Inherits BasePage
    Implements IHomeView

#Region "Members"
    ''' <summary>
    '''  Enter marketing event
    ''' </summary>
    Public Event EnterMarketing As EventHandler Implements IHomeView.EnterMarketing
    ''' <summary>
    '''  Enter marketing new event
    ''' </summary>
    Public Event EnterMarketingNew As EventHandler Implements IHomeView.EnterMarketingNew
    ''' <summary>
    '''  Enter quality event
    ''' </summary>
    Public Event EnterQuality As EventHandler Implements IHomeView.EnterQuality
    ''' <summary>
    '''  Home view presenter
    ''' </summary>
    Private m_presenter As HomePresenter

#End Region

#Region "Properties"
    ''' <summary>
    '''  Gets or sets Info text value.
    ''' </summary>
    ''' <returns>
    ''' Info text 
    ''' </returns>
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/14/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property InfoText() As String Implements IHomeView.InfoText
        Get
            Return lblInfo.Text
        End Get
        Set(ByVal value As String)
            lblInfo.Text = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Pending Approval count value.
    ''' </summary>
    ''' <returns>
    ''' pending Approval count 
    ''' </returns>
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/14/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property PendingApprovalCount() As Integer Implements Presenter.IHomeView.PendingApprovalCount
        Get
            If Session(Me.GetType().Name & "PendingApprovalCount") Is Nothing Then
                Session(Me.GetType().Name & "PendingApprovalCount") = -1
            End If
            Return CInt(Session(Me.GetType().Name & "PendingApprovalCount"))
        End Get
        Set(ByVal value As Integer)
            Session(Me.GetType().Name & "PendingApprovalCount") = value
        End Set
    End Property

    ''' <summary>
    '''  Gets or sets Is UserName Shown value.
    ''' </summary>
    ''' <returns>
    ''' Is user name shown 
    ''' </returns>
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/14/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Property IsUserNameShown() As Boolean Implements Presenter.IHomeView.IsUserNameShown
        Get
            If Session(Me.GetType().Name & "IsUserNameShown") Is Nothing Then
                Session(Me.GetType().Name & "IsUserNameShown") = True
            End If
            Return CBool(Session(Me.GetType().Name & "IsUserNameShown"))
        End Get
        Set(ByVal value As Boolean)
            Session(Me.GetType().Name & "IsUserNameShown") = value
        End Set
    End Property

#End Region

#Region "Constructors"
    ''' <summary>
    '''  Default Constructor to initialize class members.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/14/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New()

        m_presenter = New HomePresenter(Me)

    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Raise appropriate event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/14/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_btnMarketing(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMarketing.Click
        Try
            RaiseEvent EnterMarketing(sender, e)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Raise appropriate event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/14/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_btnMarketingNew(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMarketingNew.Click
        Try
            RaiseEvent EnterMarketingNew(sender, e)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Raise appropriate event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/14/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Click_btnQuality(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuality.Click
        Try
            RaiseEvent EnterQuality(sender, e)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Setup start properties according to operationmode
    ''' </summary>
    ''' <param name="p_enuOperationMode">Operation Mode</param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/14/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub SetupStartProperties(ByVal p_enuOperationMode As IHomeView.OperationMode) Implements IHomeView.SetupStartProperties
        Try
            Dim masterICS As ICS = CType(Master, ICS)
            masterICS.EffectiveMenu = ICS.EffectiveMenuEnum.None

            btnMarketing.Enabled = (p_enuOperationMode = IHomeView.OperationMode.Marketing OrElse p_enuOperationMode = IHomeView.OperationMode.Both)
            btnMarketingNew.Enabled = (p_enuOperationMode = IHomeView.OperationMode.Marketing OrElse p_enuOperationMode = IHomeView.OperationMode.Both)
            btnQuality.Enabled = (p_enuOperationMode = IHomeView.OperationMode.Quality OrElse p_enuOperationMode = IHomeView.OperationMode.Both)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Show default marketing form
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/14/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub ShowMarketingForm() Implements IHomeView.ShowMarketingForm
        Const MarketingText As String = "Marketing.aspx"
        Try
            Dim masterICS As ICS = CType(Master, ICS)
            masterICS.EffectiveMenu = ICS.EffectiveMenuEnum.Marketing
            Response.Redirect(MarketingText)
        Catch
            Throw
        End Try
    End Sub


    ''' <summary>
    ''' Show default marketingnew form
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/14/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub ShowMarketingNewForm() Implements IHomeView.ShowMarketingNewForm
        Const MarketingNewText As String = "MarketingNew.aspx"
        Try
            Dim masterICS As ICS = CType(Master, ICS)
            masterICS.EffectiveMenu = ICS.EffectiveMenuEnum.MarketingNew
            Response.Redirect(MarketingNewText)
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Show default certification form
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/14/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub ShowCertificationForm() Implements IHomeView.ShowCertificationForm
        Const CertificationSearchExText As String = "CertificationSearchEx.aspx"
        Try
            Dim masterICS As ICS = CType(Master, ICS)
            masterICS.EffectiveMenu = ICS.EffectiveMenuEnum.Quality
            Response.Redirect(CertificationSearchExText)
        Catch
            Throw
        End Try
    End Sub

#End Region

End Class
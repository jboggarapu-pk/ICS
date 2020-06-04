''' <summary>
''' This is specific to HomeView.
''' </summary>
''' <remarks></remarks>
Public Interface IHomeView
    Inherits IView

    ''' <summary>
    ''' Used to setup view properties accordingly
    ''' </summary>
    ''' <remarks></remarks>
    Enum OperationMode
        None
        Marketing
        MarketingNew
        Quality
        Both
    End Enum

    Event EnterMarketing As EventHandler
    Event EnterMarketingNew As EventHandler
    Event EnterQuality As EventHandler

    Property InfoText() As String
    Property PendingApprovalCount() As Integer
    Property IsUserNameShown() As Boolean

    Sub SetupStartProperties(ByVal p_enuOperationMode As IHomeView.OperationMode)
    Sub ShowMarketingForm()
    Sub ShowMarketingNewForm()
    Sub ShowCertificationForm()

End Interface

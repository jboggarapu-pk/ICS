''' <summary>
''' Interface specific to HomeView..
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
''' <para>11/12/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Interface IHomeView
    Inherits IView

    ''' <summary>
    ''' Enum Used to setup view properties accordingly
    ''' </summary>
    ''' <remarks></remarks>
    Enum OperationMode
        None
        Marketing
        MarketingNew
        Quality
        Both
    End Enum

    ''' <summary>
    ''' Event to Enter Marketing.
    ''' </summary>
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Event EnterMarketing As EventHandler

    ''' <summary>
    ''' Event to Enter Marketing New.
    ''' </summary>
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Event EnterMarketingNew As EventHandler

    ''' <summary>
    ''' Event to Enter Quality.
    ''' </summary>
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Event EnterQuality As EventHandler

    ''' <summary>
    ''' Variable to hold Info Text.
    ''' </summary>
    ''' <remarks></remarks>
    Property InfoText() As String

    ''' <summary>
    ''' Variable to hold Pending Approval Count.
    ''' </summary>
    ''' <remarks></remarks>
    Property PendingApprovalCount() As Integer

    ''' <summary>
    ''' Variable to hold Is UserName Shown.
    ''' </summary>
    ''' <remarks></remarks>
    Property IsUserNameShown() As Boolean

    ''' <summary>
    ''' Method to Setup Start Properties.
    ''' </summary>
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Sub SetupStartProperties(ByVal p_enuOperationMode As IHomeView.OperationMode)

    ''' <summary>
    ''' Method to Show Marketing Form.
    ''' </summary>
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Sub ShowMarketingForm()

    ''' <summary>
    ''' Method to Show Marketing New Form.
    ''' </summary>
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Sub ShowMarketingNewForm()

    ''' <summary>
    ''' Function to Show Certification Form.
    ''' </summary>
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Sub ShowCertificationForm()

End Interface

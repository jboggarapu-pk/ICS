''' <summary>
''' Interface common to all views.
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
Public Interface IView
    ''' <summary>
    '''  CustomEvents.PlainEventHandler event.
    ''' </summary>
    ''' <remarks></remarks>
    Event InitView As CustomEvents.PlainEventHandler

    ''' <summary>
    '''  Load View event.
    ''' </summary>
    ''' <remarks></remarks>
    Event LoadView As EventHandler

    ''' <summary>
    '''  Unload View event.
    ''' </summary>
    ''' <remarks></remarks>
    Event UnloadView As EventHandler

    ''' <summary>
    '''  IsPostBackView Property.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property IsPostBackView() As Boolean

    ''' <summary>
    '''  Used to View Databind.
    ''' </summary>
    ''' <remarks></remarks>
    Sub DataBindView()

    ''' <summary>
    '''  Used to Show Main View. 
    ''' </summary>
    ''' <remarks></remarks>
    Sub ShowMainView()

    ''' <summary>
    ''' Used to Hide User Menu Items.
    ''' </summary>
    ''' <param name="listMenuItemValues">List of Menu Item Values</param>
    ''' <remarks></remarks>
    Sub HideUserMenuItems(ByVal listMenuItemValues As List(Of String))

End Interface

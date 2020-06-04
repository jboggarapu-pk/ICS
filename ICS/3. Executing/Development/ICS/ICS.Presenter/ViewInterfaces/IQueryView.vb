''' <summary>
''' Interface to Query view.
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
Public Interface IQueryView
    Inherits IView
    ''' <summary>
    ''' Event to ReLoad View Data.
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
    Event ReloadViewData As CustomEvents.PlainEventHandler

    ''' <summary>
    ''' Variable to hold Grid Source.
    ''' </summary>
    ''' <remarks></remarks>
    Property GridSource() As DataTable

    ''' <summary>
    ''' Variable to hold Filter Column Source.
    ''' </summary>
    ''' <remarks></remarks>
    Property FilterColumnSource() As List(Of String)

    ''' <summary>
    ''' Variable to hold Filter Source.
    ''' </summary>
    ''' <remarks></remarks>
    Property FilterSource() As List(Of String)

    ''' <summary>
    ''' Variable to hold Error Text.
    ''' </summary>
    ''' <remarks></remarks>
    Property ErrorText() As String
End Interface

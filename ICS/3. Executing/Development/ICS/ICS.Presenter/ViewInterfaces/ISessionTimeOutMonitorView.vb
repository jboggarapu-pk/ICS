''' <summary>
''' Interface to NOM Certification User control view.
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
Public Interface ISessionTimeOutMonitorView
    Inherits IView
    ''' <summary>
    ''' Variable to hold Timer Interval.
    ''' </summary>
    ''' <remarks></remarks>
    Property TimerInterval() As Integer

    ''' <summary>
    ''' Variable to hold Session Timeout.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property SessionTimeout() As Integer

End Interface

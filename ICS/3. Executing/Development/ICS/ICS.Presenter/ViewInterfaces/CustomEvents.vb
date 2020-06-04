''' <summary>
''' Custom events
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
''' <para>11/11/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class CustomEvents

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

#Region "Member"
    ''' <summary>
    '''  handlers
    ''' </summary>
    ''' <remarks></remarks>
    Private handlers As New ArrayList()
    ''' <summary>
    '''  PlainEventHandler
    ''' </summary>
    ''' <remarks></remarks>
    Public Delegate Sub PlainEventHandler()
    ''' <summary>
    '''  PlainArgumentEventHandler
    ''' </summary>
    ''' <remarks></remarks>
    Public Delegate Sub PlainArgumentEventHandler(ByVal p_object As Object)
    ''' <summary>
    '''  Plain2ArgumentEventHandler
    ''' </summary>
    ''' <remarks></remarks>
    Public Delegate Sub Plain2ArgumentEventHandler(ByVal p_object1 As Object, ByVal p_object2 As Object)
    ''' <summary>
    '''  StatusChangedEventHandler
    ''' </summary>
    ''' <remarks></remarks>
    Public Delegate Sub StatusChangedEventHandler(ByVal p_strMatlNum As String, ByVal p_strCountryId As String, ByVal p_blnChecked As Boolean)
    ''' <summary>
    '''  SelectAllBySKUEventHandler
    ''' </summary>
    ''' <remarks></remarks>
    Public Delegate Sub SelectAllBySKUEventHandler(ByVal p_strMatlNum As String, ByVal p_blnChecked As Boolean)
    ''' <summary>
    '''  SelectAllByCountryEventHandler
    ''' </summary>
    ''' <remarks></remarks>
    Public Delegate Sub SelectAllByCountryEventHandler(ByVal p_strCountryId As String, ByVal p_blnChecked As Boolean)
    ''' <summary>
    '''  CheckSKUExistEventHandler
    ''' </summary>
    ''' <remarks></remarks>
    Public Delegate Sub CheckSKUExistEventHandler(ByVal p_strMatlNum As String, ByVal p_intIndex As Integer)

#End Region

#Region "Custom Event Handler"

    ''' <summary>
    ''' Plain event with no arguments.
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
    ''' <para>11/11/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Custom Event PlainEvent As PlainEventHandler
        AddHandler(ByVal value As PlainEventHandler)
            handlers.Add(value)
        End AddHandler
        RemoveHandler(ByVal value As PlainEventHandler)
            handlers.Remove(value)
        End RemoveHandler
        RaiseEvent()
            For Each handler As PlainEventHandler In handlers
                handler.Invoke()
            Next
        End RaiseEvent
    End Event

    ''' <summary>
    ''' Event with one argument.
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
    ''' <para>11/11/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Custom Event PlainArgumentEvent As PlainArgumentEventHandler
        AddHandler(ByVal value As PlainArgumentEventHandler)
            handlers.Add(value)
        End AddHandler
        RemoveHandler(ByVal value As PlainArgumentEventHandler)
            handlers.Remove(value)
        End RemoveHandler
        RaiseEvent(ByVal p_object As Object)
            For Each handler As PlainArgumentEventHandler In handlers
                handler.Invoke(p_object)
            Next
        End RaiseEvent
    End Event

    ''' <summary>
    ''' Custom event to handle check box click event.
    ''' </summary>
    ''' <param name="p_strMatlNum"></param>
    ''' <param name="p_strCountryId"></param>
    ''' <param name="p_blnChecked"></param>
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
    ''' <para>11/11/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Custom Event StatusChanged As StatusChangedEventHandler
        AddHandler(ByVal value As StatusChangedEventHandler)
            handlers.Add(value)
        End AddHandler
        RemoveHandler(ByVal value As StatusChangedEventHandler)
            handlers.Remove(value)
        End RemoveHandler
        RaiseEvent(ByVal p_strMatlNum As String, ByVal p_strCountryId As String, ByVal p_blnChecked As Boolean)
            For Each handler As StatusChangedEventHandler In handlers
                handler.Invoke(p_strMatlNum, p_strCountryId, p_blnChecked)
            Next
        End RaiseEvent
    End Event

    ''' <summary>
    ''' Custom handler for selectAll / deselectAll by SKU event.
    ''' </summary>
    ''' <param name="p_strMatlNum"></param>
    ''' <param name="p_blnChecked"></param>
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
    ''' <para>11/11/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Custom Event SelectAllBySKU As SelectAllBySKUEventHandler
        AddHandler(ByVal value As SelectAllBySKUEventHandler)
            handlers.Add(value)
        End AddHandler
        RemoveHandler(ByVal value As SelectAllBySKUEventHandler)
            handlers.Remove(value)
        End RemoveHandler
        RaiseEvent(ByVal p_strMatlNum As String, ByVal p_blnChecked As Boolean)
            For Each handler As SelectAllBySKUEventHandler In handlers
                handler.Invoke(p_strMatlNum, p_blnChecked)
            Next
        End RaiseEvent
    End Event

    ''' <summary>
    ''' Custom handler for selectAll / deselectAll by country event.
    ''' </summary>
    ''' <param name="p_strCountryId"></param>
    ''' <param name="p_blnChecked"></param>
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
    ''' <para>11/11/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Custom Event SelectAllByCountry As SelectAllByCountryEventHandler
        AddHandler(ByVal value As SelectAllByCountryEventHandler)
            handlers.Add(value)
        End AddHandler
        RemoveHandler(ByVal value As SelectAllByCountryEventHandler)
            handlers.Remove(value)
        End RemoveHandler
        RaiseEvent(ByVal p_strCountryId As String, ByVal p_blnChecked As Boolean)
            For Each handler As SelectAllByCountryEventHandler In handlers
                handler.Invoke(p_strCountryId, p_blnChecked)
            Next
        End RaiseEvent
    End Event

    ''' <summary>
    ''' Custom handler for check event.
    ''' </summary>
    ''' <param name="p_strMatlNum"></param>
    ''' <param name="p_intIndex"></param>
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
    ''' <para>11/11/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Custom Event CheckSKUExist As CheckSKUExistEventHandler
        AddHandler(ByVal value As CheckSKUExistEventHandler)
            handlers.Add(value)
        End AddHandler
        RemoveHandler(ByVal value As CheckSKUExistEventHandler)
            handlers.Remove(value)
        End RemoveHandler
        RaiseEvent(ByVal p_strMatlNum As String, ByVal p_intIndex As Integer)
            For Each handler As CheckSKUExistEventHandler In handlers
                handler.Invoke(p_strMatlNum, p_intIndex)
            Next
        End RaiseEvent
    End Event
#End Region

End Class
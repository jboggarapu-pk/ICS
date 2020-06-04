''' <summary>
''' NOM interface to the NOM Certification User control view
''' </summary>
Public Interface ISessionTimeOutMonitorView
    Inherits IView

    Property TimerInterval() As Integer
    ReadOnly Property SessionTimeout() As Integer

End Interface

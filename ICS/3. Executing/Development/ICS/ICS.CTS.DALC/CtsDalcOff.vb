Imports System.Data
Imports System.Data.OracleClient
Imports System.Configuration
Imports CooperTire.Security


''' <summary>
''' Example code to illustrate common properties and methods that will allow an easy plugging in of CTS 
''' during the development of a project
''' </summary>
''' <remarks>
'''   NOTE:  This DALC is decorated as not inheritable....this is not always necessary depending on the
'''          particular requirements of the project being worked on
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
''' <para>01/07/2020</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class CtsDalcOff
    ''Public Class CtsDalc
    Implements IDisposable

#Region " Members "

    Private m_blnUseCooperTireSecurity As Boolean = False

    ''' <summary>
    ''' Oracle connection object used prior to CTS being integrated in application
    ''' </summary>
    ''' <remarks>
    '''   DELETE AFTER CTS INTEGRATION
    '''   and change associated Connection(), Connect() and Disconnect() methods
    ''' </remarks>
    Private m_oraConn As OracleConnection               'TODO:  Delete after CTS has been integrated

    ''' <summary>
    '''Set temporary Connection string ...     DELETE AFTER CTS INTEGRATION''' </summary>
    ''' <remarks></remarks>
    Private m_strTempConnString As String = System.Configuration.ConfigurationManager.ConnectionStrings("ICSOracleConnection").ConnectionString
    '    Private m_strTempConnString As String = "SET YOUR CONNECTION STRING HERE"

    'TODO:  delete after CTS integration


    ''' <summary>
    ''' Oracle connection object used after CTS has been integrated in application
    ''' </summary>
    ''' <remarks>
    '''   This is the GenericOraLoginProvider exposed via CTS
    ''' </remarks>
    Private m_clsCts As CooperTire.Security.GenericOraLoginProvider

#End Region

#Region " Properties "


    ''' <summary>
    ''' Example property to expose connection
    ''' </summary>
    ''' <value></value>
    ''' <returns>Oracle connection</returns>
    ''' <remarks>
    '''    Prior to integrating CTS return the OracleConnection (m_oraConn)
    '''    Once CTS is integrated return the OracleConnection exposed in CTS (commented out below)
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
    ''' <para>01/07/2020</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected ReadOnly Property Connection() As OracleConnection

        Get
            'TODO:  Delete conditional test after CTS has been integrated
            If m_blnUseCooperTireSecurity Then
                Return m_clsCts.Connection
            Else
                Return m_oraConn
            End If
        End Get

    End Property

#End Region

#Region " Connection helper methods "

    ''' <summary>
    ''' Generic connect helper method
    ''' </summary>
    ''' <remarks>
    '''   During development (prior to CTS integration) use the m_oraConn object
    '''   After CTS has been implemented remove that code and use the m_clsCTS object
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
    ''' <para>01/07/2020</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Connect()
        Try
            'TODO:  Delete conditional test after CTS has been integrated
            If m_blnUseCooperTireSecurity Then
                ConnectCTS()
            Else
                'TODO:  Delete after CTS has been integrated
                'use prior to CTS integration...delete after CTS integration
                ConnectNonCTS()
            End If
        Catch excConnect As Exception
            Throw excConnect
        End Try
    End Sub


    ''' <summary>
    ''' Generic disconnect helper method
    ''' </summary>
    ''' <remarks>
    '''   During development (prior to CTS integration) use the m_oraConn object
    '''   After CTS has been implemented remove that code and use the m_clsCTS object
    ''' 
    '''   This method will be used whenever disconnection from the DB
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
    ''' <para>01/07/2020</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Disconnect()
        Try
            'TODO:  Delete conditional test after CTS has been integrated
            If m_blnUseCooperTireSecurity Then
                DisconnectCTS()
            Else
                'TODO:  Delete after CTS has been integrated
                'use prior to CTS integration...delete after CTS integration
                DisconnectNonCTS()
            End If
        Catch excDisconnect As Exception
            Throw excDisconnect
        End Try
    End Sub


    ''' <summary>
    ''' Open connection to DB using CTS
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
    ''' <para>01/07/2020</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub ConnectCTS()
        If m_clsCts.Connection.State = ConnectionState.Closed Then
            m_clsCts.Connect()
        End If
    End Sub


    ''' <summary>
    ''' Open connection to DB using generic OracleConnection (non-CTS)
    ''' </summary>
    ''' <remarks>
    '''         DELETE AFTER CTS INTEGRATION
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
    ''' <para>01/07/2020</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub ConnectNonCTS()
        'TODO:  Delete after CTS has been integrated
        'Connection for native OracleConnection object
        If m_oraConn.State = ConnectionState.Closed Then
            m_oraConn.ConnectionString = m_strTempConnString
            m_oraConn.Open()
        End If
    End Sub


    ''' <summary>
    ''' Close CTS connection
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
    ''' <para>01/07/2020</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub DisconnectCTS()
        If m_clsCts.Connection.State = ConnectionState.Open Then
            m_clsCts.Disconnect()
        End If
    End Sub


    ''' <summary>
    ''' Close non-CTS connection
    ''' </summary>
    ''' <remarks>
    '''         DELETE AFTER CTS INTEGRATION
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
    ''' <para>01/07/2020</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub DisconnectNonCTS()
        'TODO:  Delete after CTS has been integrated
        'Use prior to CTS integration....remove after CTS integration
        If m_oraConn.State = ConnectionState.Open Then
            m_oraConn.Close()
        End If
    End Sub

#End Region

#Region " Constructor / Dispose "


    ''' <summary>
    ''' Default constructor
    ''' </summary>
    ''' <remarks>
    '''   Use m_oraConn prior to CTS integration
    '''   After CTS has been integrated delete m_oraConn and uncomment m_clsCts
    '''   NOTE:  You will need to uncomment the m_clsCts object in the Dispose method as well
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
    ''' <para>01/07/2020</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New()

        'TODO:  Delete conditional test after CTS has been integrated
        If m_blnUseCooperTireSecurity Then
            ' CTS settings will be extracted from web.config or app.config
            m_clsCts = New GenericOraLoginProvider
        Else
            'TODO:  Delete after CTS has been integrated
            m_oraConn = New OracleConnection(m_strTempConnString)
        End If

    End Sub


#Region " IDisposable Support "

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    ''' <summary>
    ''' Dispose
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
    ''' <para>01/07/2020</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then

                'TODO:  Delete conditional test after CTS has been integrated
                If m_blnUseCooperTireSecurity Then
                    ' free managed resources when explicitly called
                    m_clsCts.Dispose()
                End If
            End If
            'free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    ''' <summary>
    ''' Disposable pattern
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
    ''' <para>01/07/2020</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region


#End Region

End Class

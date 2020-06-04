﻿Imports System.Data
Imports System.Data.OracleClient
Imports CooperTire.Security

''' <summary>
''' This class is being used to manage the Database Connections
''' </summary>
''' <remarks>
'''   NOTE:  This class is being inherited by all DAL Classes to connect/disconnect Database Connections using CTS/Non CTS.
''' </remarks>
Public Class CtsDalc
    Implements IDisposable

#Region "Members"

    ''' <summary>
    ''' CooperTireSecurity Status
    ''' </summary>
    ''' <remarks></remarks>
    Private ReadOnly _isUseCooperTireSecurityUsed As Boolean = False

    ''' <summary>
    ''' Oracle connection object used prior to CTS being integrated in application
    ''' </summary>
    ''' <remarks>
    ''' DELETE AFTER CTS INTEGRATION
    ''' and change associated Connection(), Connect() and Disconnect() methods
    ''' </remarks>
    Private ReadOnly _oraConn As OracleConnection

    

    'Private ReadOnly _tempConnString As String = "Data Source=tns:KRU_CUR_DEVL.world;User ID=CURBATCH_PROCS;Password=cur1ng234;Persist Security Info=True;"
    ''' <summary>
    ''' Set temporary Connection string
    ''' </summary>
    ''' DELETE AFTER CTS INTEGRATION
    ''' <remarks></remarks>
    Private ReadOnly _tempConnString As String = "Data Source=tns:TECH10.world;User ID=ICS_PROCS;Password=1234;Persist Security Info=True;"
    'Private ReadOnly _tempConnString As String = "Data Source=tns:FIN_LMAT_QA.world;User ID=LMAT_PROCS;Password=lmat1;Persist Security Info=True;"
    'Private ReadOnly _tempConnString As String = "Data Source=tns:FIN_LMAT_QA.world;User ID=LMAT_PROCS;Password=lmat1;Persist Security Info=True;"

    ''' <summary>
    ''' Oracle connection object used after CTS has been integrated in application
    ''' </summary>
    ''' <remarks>
    ''' This is the GenericOraLoginProvider exposed via CTS
    ''' </remarks>
    Private ReadOnly _clsCts As GenericOraLoginProvider

#End Region

#Region "Properties"

    ''' <summary>
    ''' This is the GenericOraLoginProvider property
    ''' </summary>
    ''' <value></value>
    ''' <returns>GenericOraLoginProvider</returns>
    ''' Causes WARNING - Return type of function 'GenericOraLoginProvider' is not CLS-compliant.
    ''' DO NOT USE. This property will be REMOVED in future.
    ''' <remarks></remarks>
    <ObsoleteAttribute("Replaced with methods Connect(), Disconnect() and Connection property. " & _
        "EXAMPLE: See GetLMATData() method of LMATQueryDALC.LMATQueryDALC.vb in CommonClassLibrary.")> _
    Protected ReadOnly Property GenericOraLoginProvider() As GenericOraLoginProvider
        Get
            Return _clsCts
        End Get
    End Property

    ''' <summary>
    ''' Example property to expose connection
    ''' </summary>
    ''' <value></value>
    ''' <returns>Oracle connection</returns>
    ''' <remarks>
    '''    Prior to integrating CTS return the OracleConnection (m_oraConn)
    '''    Once CTS is integrated return the OracleConnection exposed in CTS
    ''' </remarks>
    Protected ReadOnly Property Connection() As OracleConnection

        Get
            If _isUseCooperTireSecurityUsed Then
                Return _clsCts.Connection
            Else
                'Use prior to CTS integration...delete after CTS integration
                Return _oraConn
            End If
        End Get

    End Property

    ''' <summary>
    ''' Property to expose logged in UserName
    ''' </summary>
    ''' <value></value>
    ''' <returns>User name as string</returns>
    ''' <remarks></remarks>
    Protected ReadOnly Property UserName() As String
        Get
            Return _clsCts.ActiveDirectoryProvider.UserName
        End Get
    End Property

#End Region

#Region "Connection helper methods"

    ''' <summary>
    ''' Generic connect helper method
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Protected Sub Connect()
        Try
            If _isUseCooperTireSecurityUsed Then
                ConnectCTS()
            Else
                'Use prior to CTS integration...delete after CTS integration
                ConnectNonCTS()
            End If
        Catch
            Throw
        End Try
    End Sub


    ''' <summary>
    ''' Generic disconnect helper method
    ''' </summary>
    ''' <remarks>
    '''   This method will be used whenever disconnection from the DB
    ''' </remarks>
    Protected Sub Disconnect()
        Try

            If _isUseCooperTireSecurityUsed Then
                DisconnectCTS()
            Else
                'Use prior to CTS integration...delete after CTS integration
                DisconnectNonCTS()
            End If
        Catch
            Throw
        End Try
    End Sub


    ''' <summary>
    ''' Open connection to DB using CTS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConnectCTS()
        If _clsCts.Connection.State = ConnectionState.Closed Then
            _clsCts.Connect()
        End If
    End Sub


    ''' <summary>
    ''' Open connection to DB using generic OracleConnection (non-CTS)
    ''' </summary>
    ''' <remarks>
    ''' DELETE AFTER CTS INTEGRATION
    ''' </remarks>
    Private Sub ConnectNonCTS()
        If _oraConn.State = ConnectionState.Closed Then
            _oraConn.ConnectionString = _tempConnString
            _oraConn.Open()
        End If
    End Sub


    ''' <summary>
    ''' Close CTS connection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DisconnectCTS()
        If _clsCts.Connection.State = ConnectionState.Open Then
            _clsCts.Disconnect()
        End If
    End Sub


    ''' <summary>
    ''' Close non-CTS connection
    ''' </summary>
    ''' <remarks>
    ''' DELETE AFTER CTS INTEGRATION
    ''' </remarks>
    Private Sub DisconnectNonCTS()
        If _oraConn.State = ConnectionState.Open Then
            _oraConn.Close()
        End If
    End Sub

    ''' <summary>
    ''' IsInGroup Method
    ''' </summary>
    ''' <param name="roleName">Check role is in authorized groups</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Protected Function IsInGroup(ByVal roleName As String) As Boolean
        If _isUseCooperTireSecurityUsed Then
            Return _clsCts.ActiveDirectoryProvider.IsInGroup(roleName)
        Else
            'Use prior to CTS integration...delete after CTS integration
            Return True
        End If
    End Function

    ''' <summary>
    ''' GetAttributeValue Method
    ''' </summary>
    ''' <param name="attribute">Get attribute value</param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Protected Function GetAttributeValue(ByVal attribute As String) As String
        Return _clsCts.ActiveDirectoryProvider.GetAttributeValue(attribute)
    End Function

    ''' <summary>
    ''' GetGroups Method
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Protected Function GetGroups() As DataTable
        Return _clsCts.ActiveDirectoryProvider.GetGroups()
    End Function

#End Region

#Region "Constructor / Dispose"

    ''' <summary>
    ''' Default constructor
    ''' </summary>
    ''' <remarks>
    ''' Use m_oraConn prior to CTS integration
    ''' </remarks>
    Public Sub New()

        If _isUseCooperTireSecurityUsed Then
            ' CTS settings will be extracted from web.config or app.config
            _clsCts = New GenericOraLoginProvider
        Else
            _oraConn = New OracleConnection(_tempConnString)
        End If

    End Sub

    ''' <summary>
    ''' Parameterized constructor for app name and environment.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>Aslam Basha</term>
    ''' <description>
    ''' <para>12/02/2015</para>
    ''' <para>1.Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New(ByVal appName As String, ByVal environment As String)
        ' CTS settings with app name and environment.
        _clsCts = New GenericOraLoginProvider(appName, environment)
    End Sub
#Region "IDisposable Support"

    ' To detect redundant calls
    Private _disposedValue As Boolean = False

    ''' <summary>
    ''' Dispose Method.
    ''' </summary>
    ''' <param name="disposing">Check to Dispose</param>
    ''' <remarks></remarks>
    Protected Sub Dispose(ByVal disposing As Boolean)
        If Not _disposedValue Then
            If disposing Then
                If _isUseCooperTireSecurityUsed Then
                    ' free managed resources when explicitly called
                    _clsCts.Dispose()
                End If
            End If
            'free shared unmanaged resources
        End If
        _disposedValue = True
    End Sub


    ' This code added by Visual Basic to correctly implement the disposable pattern.
    ''' <summary>
    ''' This Method implements IDisposable.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

#End Region

End Class
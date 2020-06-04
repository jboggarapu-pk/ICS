Imports System.Configuration

''' <summary>
''' Handles depository construction depending on Depository Mode
''' </summary>
''' <remarks></remarks>
Public Class Depository

    Public Enum DepositoryMode
        None
        Database
        Mock
    End Enum

    Private Shared currentDepository As IDepository = Nothing
    Private Shared enuDepositoryMode As DepositoryMode = DepositoryMode.None

    Public Shared ReadOnly Property CurrentDepositoryMode() As DepositoryMode
        Get
            Return enuDepositoryMode
        End Get
    End Property

    ''' <summary>
    ''' Current depository
    ''' </summary>
    ''' <value></value>
    ''' <returns>depository currently in use</returns>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property Current() As IDepository
        Get
            If currentDepository Is Nothing Then

                currentDepository = New DataDepository()
                
            End If

            Return currentDepository
        End Get
    End Property

End Class

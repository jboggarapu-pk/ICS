Imports System.Data
Imports System.Data.OracleClient
Imports System.Configuration
Imports CooperTire.ICS.Common

''' <summary>
''' ParametersHelper Class having parameter helping method for command object
''' </summary>
''' <remarks>
''' <list type="table">
''' <listheader>
''' <term>Author</term>
''' <description>Description</description>
''' </listheader>
''' <item>
''' <term>NA</term>
''' <description>
''' <para>NA</para>
''' <para>Original Code.</para>
''' </description>
''' </item>
''' <item>
''' <term>Srinivas S</term>
''' <description>
''' <para>09/24/2019</para>
''' <para>Implemented code standardization.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class ParametersHelper

    ''' <summary>
    ''' Adds the parameters to command.
    ''' </summary>
    ''' <param name="strParameterName">Name of the STR parameter.</param>
    ''' <param name="parameterDirection">The parameter direction.</param>
    ''' <param name="oraType">Type of the ora.</param>
    ''' <param name="strParameterValue">The STR parameter value.</param>
    ''' <param name="oCMD">The o CMD.</param>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/24/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>    
    Public Shared Sub AddParametersToCommand(ByVal strParameterName As String, ByVal parameterDirection As ParameterDirection, ByVal oraType As OracleType, _
                                             ByVal strParameterValue As Object, ByRef oCMD As OracleCommand)

        Try

            If oraType.Equals(OracleType.Char) Then
                Throw New Exception(My.Resources.OracleTypeError)
            End If

            Dim param As New OracleParameter()
            'Set the Type
            param.OracleType = oraType
            'Set the parameter name
            param.ParameterName = strParameterName
            'Set the parameter direction (In,Out,In Out)
            param.Direction = parameterDirection
            'set's the parameter value
            If strParameterValue IsNot Nothing Then
                Select Case oraType
                    Case OracleType.DateTime
                        If CType(strParameterValue, DateTime).Equals(DateTime.MinValue) Then
                            param.Value = DBNull.Value
                        Else
                            param.Value = CType(strParameterValue, DateTime)
                        End If
                    Case OracleType.VarChar
                        If strParameterValue.Equals(DBNull.Value) Then
                            param.Value = DBNull.Value
                        Else
                            If Not String.IsNullOrEmpty(CStr(strParameterValue)) Then
                                param.Value = CType(strParameterValue, String)
                            Else
                                param.Value = String.Empty
                            End If
                        End If
                    Case OracleType.Number
                        If CDbl(strParameterValue) < 0 Then
                            param.Value = DBNull.Value
                        Else
                            param.Value = CType(strParameterValue, Single)
                        End If
                    Case OracleType.Int16
                        If CDbl(strParameterValue) < 0 Then
                            param.Value = DBNull.Value
                        Else
                            param.Value = CType(strParameterValue, Integer)
                        End If
                    Case OracleType.Int32
                        If CDbl(strParameterValue) < 0 Then
                            param.Value = DBNull.Value
                        Else
                            param.Value = CType(strParameterValue, Integer)
                        End If
                    Case OracleType.Double
                        If CDbl(strParameterValue) < 0 Then
                            param.Value = DBNull.Value
                        Else
                            param.Value = CType(strParameterValue, Integer)
                        End If
                End Select
            Else
                param.Value = DBNull.Value
            End If

            'Adds the parameter to the command
            oCMD.Parameters.Add(param)

        Catch exAddParametersToCommand As Exception
            Throw
        End Try

    End Sub

    ''' <summary>
    ''' Adds the parameters to command.
    ''' </summary>
    ''' <param name="strParameterName">Name of the STR parameter.</param>
    ''' <param name="parameterDirection">The parameter direction.</param>
    ''' <param name="oraType">Type of the ora.</param>
    ''' <param name="p_intSize">Size of the the string parameter</param>
    ''' <param name="strParameterValue">The STR parameter value.</param>
    ''' <param name="oCMD">The o CMD.</param>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>   
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>    
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/24/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Shared Sub AddParametersToCommand(ByVal strParameterName As String, ByVal parameterDirection As ParameterDirection, _
                                             ByVal oraType As OracleType, ByVal p_intSize As Integer, ByVal strParameterValue As Object, ByRef oCMD As OracleCommand)

        Try
            If oraType.Equals(OracleType.Char) Then
                Throw New Exception(My.Resources.OracleTypeError)
            End If

            Dim param As New OracleParameter()
            'Set the Type
            param.OracleType = oraType
            'Set the parameter name
            param.ParameterName = strParameterName
            'Set the parameter direction (In,Out,In Out)
            param.Direction = parameterDirection
            'Set the parameter size 
            param.Size = p_intSize
            'set's the parameter value
            If strParameterValue IsNot Nothing Then
                Select Case oraType
                    Case OracleType.DateTime
                        If CType(strParameterValue, DateTime).Equals(DateTime.MinValue) Then
                            param.Value = DBNull.Value
                        Else
                            param.Value = CType(strParameterValue, DateTime)
                        End If
                    Case OracleType.VarChar
                        If Not String.IsNullOrEmpty(CStr(strParameterValue)) Then
                            param.Value = CType(strParameterValue, String)
                        Else
                            param.Value = DBNull.Value
                        End If
                    Case OracleType.Number
                        If CDbl(strParameterValue) < 0 Then
                            param.Value = DBNull.Value
                        Else
                            param.Value = CType(strParameterValue, Integer)
                        End If
                    Case OracleType.Int16
                        If CDbl(strParameterValue) < 0 Then
                            param.Value = DBNull.Value
                        Else
                            param.Value = CType(strParameterValue, Integer)
                        End If
                    Case OracleType.Int32
                        If CDbl(strParameterValue) < 0 Then
                            param.Value = DBNull.Value
                        Else
                            param.Value = CType(strParameterValue, Integer)
                        End If
                    Case OracleType.Number
                        If CDbl(strParameterValue) < 0 Then
                            param.Value = DBNull.Value
                        Else
                            param.Value = CType(strParameterValue, Single)
                        End If
                    Case OracleType.Double
                        If CDbl(strParameterValue) < 0 Then
                            param.Value = DBNull.Value
                        Else
                            param.Value = CType(strParameterValue, Integer)
                        End If
                End Select
            Else
                param.Value = DBNull.Value
            End If

            'Adds the parameter to the command
            oCMD.Parameters.Add(param)

        Catch exAddParametersToCommand As Exception
            Throw
        End Try

    End Sub
End Class

Imports System.Net.Mail

Imports CooperTire.ICS.Common

''' <summary>
''' Supports outgoing e-mails from preconfigured SMTP servver
''' </summary>
''' <remarks></remarks>
Public Class Emailer

    ''' <summary>
    ''' Result of the e-mail notification
    ''' </summary>
    Public Enum SendResult
        Sucess
        Failure
        Disabled
    End Enum

#Region "Members"

    Shared m_strSmtpServerName As String = String.Empty
    Shared m_strFrom As String = String.Empty
    Shared m_strTo As String = String.Empty

#End Region

#Region "Constructors / Destructors"

    Shared Sub New()

        m_strSmtpServerName = AppSettingsAid.SmtpHost

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Send e-mail from current user to configurable address
    ''' </summary>
    ''' <param name="p_strSubject"></param>
    ''' <param name="p_strBody"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Send(ByVal p_strSubject As String, ByVal p_strBody As String) As SendResult

        Dim enuSendResult As SendResult = SendResult.Failure

        If m_strSmtpServerName = String.Empty Then
            enuSendResult = SendResult.Disabled
            Return enuSendResult
        End If

        Try

            Using message As MailMessage = New MailMessage(AppSettingsAid.EmailFrom("Quality"), AppSettingsAid.EmailTo("Quality"), p_strSubject, p_strBody)
                message.IsBodyHtml = True
                ' SmtpClient to send e-mail
                Dim mailClient As SmtpClient = New SmtpClient(m_strSmtpServerName)
                ' UseDefaultCredentials = Windows credentials of user account
                mailClient.UseDefaultCredentials = True
                ' Dim cred As Object = mailClient.Credentials
                mailClient.DeliveryMethod = SmtpDeliveryMethod.Network
                ' Send the message to mail server
                mailClient.Send(message)
                ' Message sent
                enuSendResult = SendResult.Sucess
            End Using

        Catch exp As Exception      '' FormatException SmtpException
            enuSendResult = SendResult.Failure
            EventLogger.Enter(exp)
        End Try

        Return enuSendResult

    End Function

    ''' <summary>
    ''' Send e-mail from current user to configurable address for marketing function
    ''' </summary>
    ''' <param name="p_strSubject"></param>
    ''' <param name="p_strBody"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SendMarketing(ByVal p_strSubject As String, ByVal p_strBody As String) As SendResult

        Dim enuSendResult As SendResult = SendResult.Failure

        If m_strSmtpServerName = String.Empty Then
            enuSendResult = SendResult.Disabled
            Return enuSendResult
        End If

        Try

            Using message As MailMessage = New MailMessage(AppSettingsAid.EmailFrom("Marketing"), AppSettingsAid.EmailTo("Marketing"), p_strSubject, p_strBody)
                ' SmtpClient to send e-mail
                message.IsBodyHtml = True
                Dim mailClient As SmtpClient = New SmtpClient(m_strSmtpServerName)
                ' UseDefaultCredentials = Windows credentials of user account
                'mailClient.UseDefaultCredentials = True
                ' Dim cred As Object = mailClient.Credentials
                mailClient.DeliveryMethod = SmtpDeliveryMethod.Network
                ' Send the message to mail server
                mailClient.Send(message)
                ' Message sent
                enuSendResult = SendResult.Sucess
            End Using

        Catch exp As Exception      '' FormatException SmtpException
            enuSendResult = SendResult.Failure
            EventLogger.Enter(exp)
        End Try

        Return enuSendResult

    End Function

#End Region

End Class

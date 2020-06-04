Imports System.Net.Mail
Imports CooperTire.ICS.Common

''' <summary>
''' Supports outgoing e-mails from pre-configured SMTP server
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
''' <para>09/18/2019</para>
''' <para>Implemented code standardization.</para>
''' </description>
''' </item> 
''' </list>
''' </remarks>
Public Class Emailer

    ''' <summary>
    ''' Result of the e-mail notification
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SendResult
        Sucess
        Failure
        Disabled
    End Enum

#Region "Members"

    ''' <summary>
    ''' Smtp Server name to hold empty string
    ''' </summary>
    ''' <remarks></remarks>
    Shared m_strSmtpServerName As String = String.Empty

    ''' <summary>
    ''' From address name to hold empty string
    ''' </summary>
    ''' <remarks></remarks>
    Shared m_strFrom As String = String.Empty

    ''' <summary>
    ''' From to name to hold empty string
    ''' </summary>
    ''' <remarks></remarks>
    Shared m_strTo As String = String.Empty

#End Region

#Region "Constructors/Destructor's"

    ''' <summary>
    ''' Constructor to initialize the class members.
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/18/2019</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Shared Sub New()
        m_strSmtpServerName = AppSettingsAid.SmtpHost
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Send e-mail from current user to configurable address
    ''' </summary>
    ''' <param name="p_strSubject">Subject of the email.</param>
    ''' <param name="p_strBody">Body of the email.</param>
    ''' <returns>Returns SendResult type success.</returns>
    ''' <exception cref="Exception">
    ''' Returns Failure indicator and logs the event
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
    ''' <para>09/18/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Shared Function Send(ByVal p_strSubject As String, ByVal p_strBody As String) As SendResult
        Const QualityText As String = "Quality"
        Dim enuSendResult As SendResult = SendResult.Failure

        If m_strSmtpServerName = String.Empty Then
            enuSendResult = SendResult.Disabled
            Return enuSendResult
        End If

        Try
            Using message As MailMessage = New MailMessage(AppSettingsAid.EmailFrom(QualityText), AppSettingsAid.EmailTo(QualityText), p_strSubject, p_strBody)
                message.IsBodyHtml = True
                ' SmtpClient to send e-mail
                Dim mailClient As SmtpClient = New SmtpClient(m_strSmtpServerName)
                mailClient.UseDefaultCredentials = True
                mailClient.DeliveryMethod = SmtpDeliveryMethod.Network
                ' Send the message to mail server
                mailClient.Send(message)
                ' Message sent
                enuSendResult = SendResult.Sucess
            End Using

        Catch expSend As Exception
            enuSendResult = SendResult.Failure
            EventLogger.Enter(expSend)
        End Try

        Return enuSendResult

    End Function

    ''' <summary>
    ''' Send e-mail from current user to configurable address for marketing function
    ''' </summary>
    ''' <param name="p_strSubject">Subject of the email.</param>
    ''' <param name="p_strBody">Body of the email.</param>
    ''' <returns>Returns SendResult type success.</returns>
    ''' <exception cref="Exception">
    ''' Returns Failure indicator and logs the event
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
    ''' <para>09/18/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Shared Function SendMarketing(ByVal p_strSubject As String, ByVal p_strBody As String) As SendResult

        Const MarketingText As String = "Marketing"
        Dim enuSendResult As SendResult = SendResult.Failure

        If m_strSmtpServerName = String.Empty Then
            enuSendResult = SendResult.Disabled
            Return enuSendResult
        End If

        Try

            Using message As MailMessage = New MailMessage(AppSettingsAid.EmailFrom(MarketingText), AppSettingsAid.EmailTo(MarketingText), p_strSubject, p_strBody)
                ' SmtpClient to send e-mail
                message.IsBodyHtml = True
                Dim mailClient As SmtpClient = New SmtpClient(m_strSmtpServerName)
                mailClient.DeliveryMethod = SmtpDeliveryMethod.Network
                ' Send the message to mail server
                mailClient.Send(message)
                ' Message sent
                enuSendResult = SendResult.Sucess
            End Using

        Catch expSendMarketing As Exception
            enuSendResult = SendResult.Failure
            EventLogger.Enter(expSendMarketing)
        End Try

        Return enuSendResult
    End Function

#End Region

End Class

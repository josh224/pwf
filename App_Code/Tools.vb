Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Web
Imports System.Net.Mail
Imports System

'Tools for use in error logging throughout the website
Public Class Tools
    'Log an error message in a txt file in teh root of the website
    'usage is Tools.Log("Error Messgae", ex)
    'you can call SendMail to email the webadmin if required
    Public Shared Sub Log(ByVal message As String)
        Log(message, Nothing)
    End Sub

    ''' <summary>
    ''' Puts any error messages in a file on the server - use admin pages to read/ edit logs
    ''' </summary>
    ''' <param name="message">Put user friendly message here</param>
    ''' <param name="ex">Trapped exception</param>
    ''' <param name="LZCError">Set to true if error generated in LZC software</param>
    ''' <remarks></remarks>
    Public Shared Sub Log(ByVal message As String, ByVal ex As Exception, Optional ByVal LZCError As Boolean = False)
        'Dim fileName As String = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "\Files\Error.log")
        Dim fileName As String
        If LZCError Then
            fileName = System.Web.HttpContext.Current.Server.MapPath("~/Files/LzcError.log")
        Else
            fileName = System.Web.HttpContext.Current.Server.MapPath("~/Files/Error.log")
        End If

        Using LogFile As New StreamWriter(fileName, True)


            If ex IsNot Nothing Then
                LogFile.WriteLine("<error time=""{0}:"" message=""{1}"">{2}</error>", DateTime.Now, message, ex.ToString())
            Else
                LogFile.WriteLine("<error time=""{0}:"" message=""{1}""></error>", DateTime.Now, message)
            End If
            LogFile.Close()
        End Using
    End Sub

    Public Shared Sub SendMail(ByVal message As String, ByVal ex As Exception)
        'in case 'hoarelea\sp_crawl goes wrong again
        If message.Contains("hoarelea\sp_crawl") Then Exit Sub
        Using msg As New MailMessage("website@10.10.2.32", "joshjones@hoarelea.com")
            msg.Subject = "Web Site Error at " & HttpContext.Current.Request.RawUrl

            If ex Is Nothing Then
                msg.Body = "There was an unknown error at the website - " & HttpContext.Current.Request.Params.ToString
            Else
                msg.Body = "Web Site Error at: " & HttpContext.Current.Request.RawUrl & "\n"
                msg.Body &= vbCrLf & vbCrLf & message
                msg.Body &= vbCrLf & vbCrLf & "Error Message:" & vbCrLf & ex.ToString
                msg.Body &= vbCrLf & vbCrLf & "webmaster@knowledge-base"

            End If

            Try
                Dim client As New SmtpClient("mail.hoarelea.local")
                client.UseDefaultCredentials = True
                client.Send(msg)
            Catch e As Exception
            End Try
        End Using
    End Sub

    Public Shared Sub ErrorEMailMe(ByVal message As String, Optional ByVal page As String = "")
        Using msg As New MailMessage("website@10.10.2.32", "joshjones@hoarelea.com")
            msg.Subject = "Web Site Error at " & HttpContext.Current.Request.RawUrl
            Dim client As New SmtpClient("mail.hoarelea.local")
            client.UseDefaultCredentials = True
            client.Send(msg)
        End Using
    End Sub

    Public Shared Sub editFileEmailMe(ByVal message As String)
        Using msg As New MailMessage("website@10.10.2.32", "joshjones@hoarelea.com")
            msg.Subject = "File edit "
            msg.Body = message
            Dim client As New SmtpClient("mail.hoarelea.local")
            client.UseDefaultCredentials = True
            client.Send(msg)
        End Using
    End Sub
End Class

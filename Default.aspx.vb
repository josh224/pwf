Imports System.Data.SqlClient
Partial Class _Default
    Inherits System.Web.UI.Page
    Public jsQuestions As StringBuilder
    Public userId As String = "0000"
    Public userName As String = "unknown"
    Public userOffice As String = "unknown"
    Public userEmailAddress As String = ""
    Public userJson As string = ""

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load


        Dim hltsUser As New hltsUserInfo
        If Not hltsUser.errorFlag Then
            userId = hltsUser.Contact_ID
            userName = hltsUser.Fullname
            userEmailAddress = hltsUser.EmailAddress
            userOffice = hltsUser.Office








            'get user history
            Using conn As New SqlConnection(Application("ConnectionStringHLTS"))
                Using cmd As New SqlCommand("SELECT json from pwfUserInfo where userId=" & hltsUser.Contact_ID, conn)
                    conn.Open()
                    Dim reader As SqlDataReader = cmd.ExecuteReader
                    If reader.HasRows  Then
                        reader.Read
                        userJson = reader.Item("json")                        
                    End If
                End Using
            End Using
            If string.IsNullOrEmpty(userJson) Then
                'userJson="date:null;"
            End If
            'PWF:get user project history from db
            

'TODO:
        End If

    End Sub
End Class

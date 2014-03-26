Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization
Imports Aspose.Cells
Imports System.Data.SqlClient
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Data
Imports System.IO
Imports Newtonsoft.Json.Converters

'Imports Aspose.Cells



' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.

<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://hlts.tcgroup.jj.pwf/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class PwfService
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function GetProjectsFromServer() As String
        Return "Hello GetProjectsFromServer()"
    End Function
    <WebMethod()> _
    Public Function GetQuestionsFromServer() As String
        Return "Hello GetQuestionsFromServer()"
    End Function
    '<WebMethod()> _
    'Public Function GetRolesFromServer() As String
    '    Return "Hello GetRolesFromServer"
    'End Function
    <WebMethod()> _
    Public Function GetProjectDataFromSever(ByVal text As String) As String
        Return "Hello GetProjectDataFromSever"
    End Function

    <WebMethod()> _
    Public Function GetUserDetailsFromSever() As String
        Try
            Dim hltsUser As New hltsUserInfo

            Dim serializer As New JavaScriptSerializer


            Return serializer.Serialize(hltsUser)
        Catch e As Exception
            'add manual for now  -testing
            Dim jj As New Object
            jj.Contact_ID = 5732
            jj.Fullname = "Josh Jones"
            jj.CostCentre = "Technical Control"
            jj.Job_Title = "Technical Author"
            jj.Office = "Cardiff"
            Return jj
        Finally

        End Try
    End Function

    <WebMethod()> _
    Public Function GetRolesFromSever() As String()

        Dim returnArray As New List(Of String)
        Try
            Dim sqlCommandString As String = "SELECT Role FROM pwfRoles ORDER BY Role"
            Using conn As New SqlConnection(HttpContext.Current.Application("ConnectionStringHLTS"))
                Using cmd As New SqlCommand(sqlCommandString, conn)
                    conn.Open()
                    Dim reader As SqlDataReader = cmd.ExecuteReader
                    If reader.HasRows Then
                        While reader.Read
                            returnArray.Add(reader.GetString(0))
                        End While
                    End If
                End Using
            End Using
        Catch e As Exception
            returnArray.Add("Unknown Role")
        End Try

        Return returnArray.ToArray
    End Function

    '''
    '''
    '''
    <WebMethod()> _
    Public Function StoreUserDetailsOnSever(ByVal userId As String, ByVal jsonString As String) As String
        Try
            userId = CInt(userId)
            Dim foo As New clsUserData
            'testing json
            Dim json As JObject = JObject.Parse(jsonString)
            Dim jdata As Newtonsoft.Json.Linq.JToken = json("data")
            If jdata.HasValues Then
                foo = jdata.ToObject(Of clsUserData)()
            End If

            If Not String.IsNullOrEmpty(jsonString) Then
                'upload to server
                Dim sqlCommandString As String = String.Format("SELECT * FROM pwfUserInfo WHERE userId={0}", userId)
                Using conn As New SqlConnection(HttpContext.Current.Application("ConnectionStringHLTS"))
                    Using cmd As New SqlCommand(sqlCommandString, conn)
                        conn.Open()
                        Dim reader As SqlDataReader = cmd.ExecuteReader
                        If reader.RecordsAffected > 0 Then
                            'Update required
                            sqlCommandString = String.Format("UPDATE pwfUserInfo SET json='{1}' WHERE userId={0}", userId, jsonString)
                            Using cmd1 As New SqlCommand(sqlCommandString, conn)
                                reader.Close()
                                reader = cmd1.ExecuteReader
                                If reader.RecordsAffected > 0 Then
                                    Return True
                                Else
                                    Throw New Exception(String.Format("Unable to update {0} with {1}", userId, jsonString))
                                End If
                            End Using
                        Else
                            'insert required
                            sqlCommandString = String.Format("INSERT INTO pwfUserInfo VALUES ({0}, '{1}')", userId, jsonString)
                            Using cmd1 As New SqlCommand(sqlCommandString, conn)
                                reader.Close()
                                reader = cmd1.ExecuteReader
                                If reader.RecordsAffected > 0 Then
                                    Return True
                                Else
                                    Throw New Exception(String.Format("Unable to insert {0} and {1}", userId, jsonString))
                                End If
                            End Using
                        End If
                    End Using
                End Using
            End If

            Return False

        Catch e As Exception
            Return e.Message & "0"
        Finally
        End Try
    End Function

    '''
    '''
    '''
    <WebMethod()> _
    Public Function streamRiskRegisterToUser(projectDetails As String) As String

        Try
            Dim serializer As New JavaScriptSerializer
            Dim proj As pwfProject = serializer.Deserialize(Of pwfProject)(projectDetails)
            'Dim proj As List(Of pwfProject) = serializer.Deserialize(projectDetails, GetType(List(Of pwfProject)))

            Dim returnString As String = String.Format("proj.name = {0} and number = {1} and user = {2}", proj.name, proj.number, proj.user)
            ' The path to the documents directory.

            Dim dataDir As String = Server.MapPath("/Standard IM Templates/")
            '''''Server.MapPath("~");
            ' 1.
            ' Opening through Path
            'Creating a Workbook object and opening an Excel file using its file path
            Dim workbook1 As New Workbook(AppDomain.CurrentDomain.BaseDirectory & "IMF 48 Risk Register.xlsx")
            Console.WriteLine("Workbook opened using path successfully!")
            'Getting the specified named range
            Dim range As Range = workbook1.Worksheets.GetRangeByName("name")
            range(0, 0).PutValue("USA")
            workbook1.Save(HttpContext.Current.Response, "output.xlsx", ContentDisposition.Attachment, New XlsSaveOptions(SaveFormat.Auto))
            Return returnString
        Catch e As Exception
            Return e.Message
        Finally
        End Try
    End Function

    '''
    '''
    '''
    <WebMethod()> _
    Public Function WebMethod_AddEntryToActivityLog(projectNumber As String, projectName As String, userName As String, userId As String, header As String, activity As String, tags As String) As String
        Try
            If userId <> "" Then
                If projectNumber <> "" Then
                    If activity <> "" Then
                        userId = CInt(userId)
                        projectNumber = CInt(projectNumber)

                        'upload to server
                        Dim sqlCommandString As String = "INSERT INTO pwfActivityLog (projectNumber, projectName, userName, userId, header, activity, date, tags) VALUES (@projectNumber, @projectName, @userName, @userId, @header, @activity, @date, @tags)"
                        Using conn As New SqlConnection(HttpContext.Current.Application("ConnectionStringHLTS"))
                            Using cmd As New SqlCommand(sqlCommandString, conn)
                                cmd.Parameters.Add("@projectNumber", SqlDbType.BigInt).Value = projectNumber
                                cmd.Parameters.Add("@projectName", SqlDbType.VarChar).Value = projectName
                                cmd.Parameters.Add("@userId", SqlDbType.BigInt).Value = userId
                                cmd.Parameters.Add("@userName", SqlDbType.VarChar).Value = userName
                                cmd.Parameters.Add("@header", SqlDbType.VarChar).Value = header
                                cmd.Parameters.Add("@activity", SqlDbType.VarChar).Value = activity
                                cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = Date.Now.ToString
                                cmd.Parameters.Add("@tags", SqlDbType.VarChar).Value = tags
                                ''cmd.Parameters.Add("@extraInformation", SqlDbType.Text).Value = extraInformation
                                conn.Open()
                                Dim reader As SqlDataReader = cmd.ExecuteReader
                                If reader.RecordsAffected < 1 Then

                                    Throw New Exception("Unable to add Activity, sql string = " & cmd.CommandText)

                                End If
                                Return True
                            End Using
                        End Using



                    Else
                        Throw New Exception("No activity")
                    End If
                Else
                    Throw New Exception("No projectNumber")
                End If
            Else
                Throw New Exception("No userId")
            End If

        Catch e As Exception
            Return e.Message
        End Try
    End Function


    '''
    '''
    '''

    <WebMethod()> _
    Public Function WebMethod_GetActivitiesLogFromSever(ByVal userId As String, ByVal projectNumber As String) As String

        Dim sqlCommandString As String = ""
        userId = CInt(userId)
        projectNumber = CInt(projectNumber)
        If (userId > 0) Then
            sqlCommandString = "SELECT activityId, projectNumber, projectName, userName, header, activity, date, tags FROM pwfActivityLog WHERE userId=" + userId + " ORDER BY date DESC"
        ElseIf projectNumber > 0 Then
            sqlCommandString = "SELECT activityId, projectNumber, projectName, userName, header, activity, date, tags  FROM pwfActivityLog WHERE projectNumber=" + projectNumber + " ORDER BY date DESC"
        Else
            sqlCommandString = "SELECT activityId, projectNumber, projectName, userName, header, activity, date, tags  FROM pwfActivityLog ORDER BY date DESC"
        End If
        Dim returnArray As New List(Of String)
        Try
            Using conn As New SqlConnection(HttpContext.Current.Application("ConnectionStringHLTS"))
                Using cmd As New SqlCommand(sqlCommandString, conn)
                    conn.Open()
                    Dim adapter As New SqlDataAdapter(cmd)
                    Dim data As New DataTable()
                    adapter.Fill(data)
                    Dim json As New Newtonsoft.Json.JsonSerializer
                    json.NullValueHandling = NullValueHandling.Ignore
                    json.ObjectCreationHandling = Newtonsoft.Json.ObjectCreationHandling.Replace
                    json.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore
                    json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    json.Converters.Add(New DataTableConverter)
                    Dim sw As New StringWriter
                    Dim writer = New JsonTextWriter(sw)
                    writer.Formatting = Formatting.None
                    json.Serialize(writer, data)
                    Dim output As String = sw.ToString()
                    writer.Close()

                    sw.Close()

                    Return output
                End Using
            End Using
        Catch e As Exception
            Return e.Message
        End Try

        'Return returnArray.ToArray










        'Using conn As New SqlConnection(HttpContext.Current.Application("ConnectionStringHLTS"))
        '    Using cmd As New SqlCommand(sqlCommandString, conn)
        '        conn.Open()
        '        Dim adapter As New SqlDataAdapter(cmd)
        '        Dim data As New DataTable()
        '        adapter.Fill(data)
        '        Dim json As New Newtonsoft.Json.JsonSerializer
        '        json.NullValueHandling = NullValueHandling.Ignore
        '        json.ObjectCreationHandling = Newtonsoft.Json.ObjectCreationHandling.Replace
        '        json.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore
        '        json.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        '        json.Converters.Add(New DataTableConverter)
        '        Dim sw As New StringWriter
        '        Dim writer = New JsonTextWriter(sw)
        '        writer.Formatting = Formatting.None
        '        json.Serialize(writer, data)
        '        Dim output As String = sw.ToString()
        '        writer.Close()

        '        sw.Close()

        '        Return output





        'writer.QuoteChar = '\"'

        ' | If reader.HasRows Then                                                                                                                                                                                                                                                                             |
        ' | returnArray.Clear()                                                                                                                                                                                                                                                                                |
        ' | Dim first = True                                                                                                                                                                                                                                                                                   |
        ' | While reader.Read                                                                                                                                                                                                                                                                                  |
        ' |     If first Then                                                                                                                                                                                                                                                                                  |
        ' |         first = False                                                                                                                                                                                                                                                                              |
        ' |     Else                                                                                                                                                                                                                                                                                           |
        ' |         foostr.Append(", ")                                                                                                                                                                                                                                                                        |
        ' |     End If                                                                                                                                                                                                                                                                                         |
        ' |                                                                                                                                                                                                                                                                                                    |
        '''' |         'foostr.Append('{"activityId": "' & reader("activityId") & '", "projectNumber": ' & reader("projectNumber") & '", "projectName": ' & reader("projectName")", "userName": "reader("userName")", "header": "reader("header")", "activity": "reader("activity")", "date": "reader("date")"'))  |
        ' |     'returnArray.Add(foostr)                                                                                                                                                                                                                                                                       |
        ' | End While                                                                                                                                                                                                                                                                                          |
        ' |     foostr=foostr.                                                                                                                                                                                                                                                                                 |
        ' | End If                                                                                                                                                                                                                                                                                             |
        ' •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
        ' End Using
        'End Using


    End Function








End Class


'''
'''
'''

Public Class pwfProject
    Private _name As String
    Public Property name As String
        Get
            Return _name
        End Get
        Set(ByVal Value As String)
            _name = Value
        End Set
    End Property
    Private _number As String
    Public Property number As String
        Get
            Return _number
        End Get
        Set(ByVal Value As String)
            _number = Value
        End Set
    End Property

    Private _user As String
    Public Property user As String
        Get
            Return _user
        End Get
        Set(ByVal Value As String)
            _user = Value
        End Set
    End Property


End Class

Public Structure clsProject
    Private _activityId As String
    Public Property activityId As String
        Get
            Return _activityId
        End Get
        Set(ByVal Value As String)
            _activityId = Value
        End Set
    End Property
    Private _projectNumber As String
    Public Property projectNumber As String
        Get
            Return _projectNumber
        End Get
        Set(ByVal Value As String)
            _projectNumber = Value
        End Set
    End Property
    Private _projectName As String
    Public Property projectName As String
        Get
            Return _projectName
        End Get
        Set(ByVal Value As String)
            _projectName = Value
        End Set
    End Property
    Private _userName As String
    Public Property userName As String
        Get
            Return _userName
        End Get
        Set(ByVal Value As String)
            _userName = Value
        End Set
    End Property
    Private _header As String
    Public Property header As String
        Get
            Return _header
        End Get
        Set(ByVal Value As String)
            _header = Value
        End Set
    End Property
    Private _activity As String
    Public Property activity As String
        Get
            Return _activity
        End Get
        Set(ByVal Value As String)
            _activity = Value
        End Set
    End Property
    Private _date As Date
    Public Property [Date] As Date
        Get
            Return _date
        End Get
        Set(ByVal Value As Date)
            _date = Value
        End Set
    End Property


End Structure





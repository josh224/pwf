Imports System.Web.Script.Serialization
Imports System.IO
Imports Aspose.Cells

Partial Class pop
    Inherits System.Web.UI.Page

    Public Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Try
            If Not Me.IsPostBack Then
                If Not String.IsNullOrEmpty(Request.Form("name")) AndAlso Not String.IsNullOrEmpty(Request.Form("technology")) Then
                    Response.Write("<u>Values using Form Post</u><br /><br />")
                    Response.Write(("<b>Name:</b> " + Request.Form("name") & " <b>Technology:</b> ") + Request.Form("technology"))
                End If
            End If


            Dim dataDir As String = Server.MapPath("~/")
            '''''Server.MapPath("~");
            ' 1.
            ' Opening through Path
            'Creating a Workbook object and opening an Excel file using its file path
            'have 3 workbooks in case ???
            Try
                Dim workbook1 As Workbook

                For i As Integer = 1 To 5
                    Try
                        'pause and try again in
                        workbook1 = New Workbook(dataDir & "IMF 48 Risk Register.xlsx")
                        Exit For
                        'in case someone  else is using it
                    Catch ex As Exception
                        Console.WriteLine("Workbook failed to open " & i & " number of times")
                        System.Threading.Thread.Sleep(1000)

                        Tools.Log("error @ pop.aspx open file" & ex.Message)

                    End Try
                Next

                Console.WriteLine("Workbook opened using path successfully!")
                'Getting the specified named range
                Dim xlRangeNames As String() = {"name", "number", "office", "revision", "userName", "reviewer", "date"}
                For Each rangeName As String In xlRangeNames
                    If Not String.IsNullOrEmpty(Request.Form(rangeName)) Then
                        Try
                            Dim range As Range = workbook1.Worksheets.GetRangeByName(rangeName)
                            range(0, 0).PutValue(Request.Form(rangeName))
                        Catch ex As Exception
                            'TODO:
                            Tools.Log("error @ pop.aspx range names" & ex.Message)

                        End Try
                    End If
                Next
                'now add risks
                If Not String.IsNullOrEmpty(Request.Form("arraySize")) Then
                    Dim saveAs As String = "xlsx"
                    Dim saveFormat As Aspose.Cells.SaveFormat = saveFormat.Xlsx
                    Try
                        Dim arrayElements As Integer = CInt(Request.Form("arraySize"))
                        If Not String.IsNullOrEmpty(Request.Form("saveAs")) Then
                            saveAs = Request.Form("saveAs")
                            If saveAs = "pdf" Then
                                saveFormat = Aspose.Cells.SaveFormat.Pdf
                            Else
                                saveAs = "xlsx"

                            End If

                        End If

                        If arrayElements > 0 Then
                            Dim range As Range = workbook1.Worksheets.GetRangeByName("risk")
                            Dim rowcount As Integer = 0
                            For i = 1 To arrayElements
                                If Not String.IsNullOrEmpty(Request.Form("array" & i)) Then
                                    Try
                                        Dim riskValues As String() = Split(Request.Form("array" & i), "/###/")
                                        Dim adjusted As Integer = 0  'compensate for an extra merged column
                                        For j = 0 To 6
                                            If j >= 3 Then
                                                adjusted = j + 1
                                            Else
                                                adjusted = j
                                            End If
                                            '0-6 represents the seven columns in the register
                                            If Not String.IsNullOrEmpty(riskValues(j)) Then
                                                range(rowcount, adjusted).PutValue(riskValues(j))
                                            End If
                                        Next j
                                        rowcount += 1

                                    Catch ex As Exception
                                        'TODO: 
                                    End Try

                                End If
                            Next  'arrayElement

                        End If


                    Catch ex As Exception

                        Tools.Log("error @ pop.aspx range names1" & ex.Message)

                    End Try
                    
                    workbook1.Save(HttpContext.Current.Response, "RiskRegister." & saveAs, ContentDisposition.Attachment, New XlsSaveOptions(saveFormat))

                End If
            Catch ex As Exception

                'TODO:

                Tools.Log("error @ pop.aspx range names" & ex.Message)

            End Try


        Catch ex As Exception
            Tools.Log("error @ pop.aspx range names" & ex.Message)
        End Try



    End Sub
End Class

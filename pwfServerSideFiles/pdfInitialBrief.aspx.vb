Imports System.Web.Script.Serialization
Imports System.IO
Imports Aspose.Cells

Partial Class pdfInitialBrief
Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

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
            Dim workbook1 As Workbook
            Try
                

                For i As Integer = 1 To 5
                    Try
                        'pause and try again in
                        workbook1 = New Workbook(dataDir & "ibr.xlsx")
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
                Dim xlRangeNames As String() = {"name", "number", "office", "author", "date"}
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
                'now add q&a
                If Not String.IsNullOrEmpty(Request.Form("arraySize")) Then
                    Dim saveAs As String = "pdf"
                    Dim saveFormat As Aspose.Cells.SaveFormat = Aspose.Cells.SaveFormat.Pdf

                    Try
                        Dim arrayElements As Integer = CInt(Request.Form("arraySize"))
                        If Not String.IsNullOrEmpty(Request.Form("saveAs")) Then
                            If Request.Form("saveAs") = "xlsx" Then
                                saveAs = "xlsx"
                                saveFormat = Aspose.Cells.SaveFormat.Xlsx
                            End If
                                


                            End If

                            If arrayElements > 0 Then
                                Dim range As Range = workbook1.Worksheets.GetRangeByName("risk")
                                Dim rowcount As Integer = 0
                                For i = 1 To arrayElements
                                    If Not String.IsNullOrEmpty(Request.Form("array" & i)) Then
                                        Try
                                            Dim riskValues As String() = Split(Request.Form("array" & i), "/###/")

                                            If Not String.IsNullOrEmpty(riskValues(0)) Then
                                                range(rowcount, 0).PutValue("Question " & i)
                                                range(rowcount, 1).PutValue(riskValues(0))
                                                rowcount += 1
                                                range(rowcount, 0).PutValue("ans:")
                                                range(rowcount, 1).PutValue(riskValues(1))
                                                rowcount += 1


                                            End If






                                        Catch ex As Exception
                                            'TODO: 
                                        End Try

                                    End If
                                Next  'arrayElement
                            End If


                    Catch ex As Exception

                        Tools.Log("error @ pop.aspx range names1" & ex.Message)

                    End Try
                    workbook1.Save(HttpContext.Current.Response, "InitialBrief." & saveAs, ContentDisposition.Attachment, New XlsSaveOptions(saveFormat))

                End If
            Catch ex As Exception

                'TODO:

                Tools.Log("error @ pdfInitialBrief range names" & ex.Message)

            End Try


        Catch ex As Exception
            Tools.Log("error @ pdfInitialBrief.aspx range names" & ex.Message)
        End Try



    End Sub
End Class

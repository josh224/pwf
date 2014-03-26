Imports Microsoft.VisualBasic

Public Class JSONHandler
    Implements IHttpHandler

    Public ReadOnly Property IsReusable() As Boolean Implements System.Web.IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    Public Sub ProcessRequest(ByVal context As System.Web.HttpContext) Implements System.Web.IHttpHandler.ProcessRequest

        context.Response.Write(System.IO.File.ReadAllText(context.Request.PhysicalPath))

    End Sub
End Class
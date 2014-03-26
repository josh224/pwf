Imports Microsoft.VisualBasic 

Class clsUserData 
   


    Private _projectName As String
Public Property ProjectName As String
    Get
        Return _projectName 
    End Get
    Set(ByVal value As String)
        _projectName = value
    End Set
End Property
    Private _projectNumber As String
Public Property ProjectNumber() As String
    Get
        Return _projectNumber 
    End Get
    Set(ByVal value As String)
        _projectNumber = value
    End Set
End Property
    Private _projectHistory As List(of String)
Public Property ProjectHistory() As List(of String)
    Get
        Return _projectHistory 
    End Get
    Set(ByVal value As List(of String))
        _projectHistory = value
    End Set
End Property

End Class 

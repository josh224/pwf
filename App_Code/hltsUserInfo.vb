Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Data
''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
<Serializable()> Public Class hltsUserInfo
    '#### you can change job roles here
    'leave if deleted
    'change if changed
    <Flags()> _
    Enum EsoRoles As Integer
        All = 0
        Partner = 1
        Associate = 2
        ElectricalEngineer = 4
        MechanicalEngineer = 8
        Cad = 16
        Admin = 32
        Acoustics = 64
        BusinessDevelopment = 128
        Communications = 256
        EngineeringManagement = 512
        ExpertWitness = 1024
        Fire = 2048
        IMS = 4096
        LegalAndRiskManagement = 8192
        Lighting = 16384
        Marketing = 32768
        OperationalEngineering = 65536
        Production = 131072
        ProfessionalDevelopment = 262144
        PropertyServices = 524288
        PublicHealth = 1048576
        RandD = 2097152
        Refrigeration = 4194304
        Security = 8388608
        Sustainability = 16777216
        TechnicalControl = 33554432
        VerticalTransportation = 67108864
        VirtualEngineering = 134217728
    End Enum





#Region "Declarations"
    Private _discipline As String
    Private _grade As String
    Public errorFlag As Boolean
    Public errorMessage As String
    Private allOk As Boolean
    Public _NT_User As String

    Private _contact_ID As Integer
    Private PeriodicallyCheckRoles As Boolean

    Public Property Contact_ID As Integer
        Get
            Return _contact_ID
        End Get
        Set(ByVal Value As Integer)
            _contact_ID = Value
        End Set
    End Property
    Private _dateJoined As Date
    Public Property DateJoined As Date
        Get
            Return _dateJoined
        End Get
        Set(ByVal Value As Date)
            _dateJoined = Value
        End Set
    End Property

    Private _fullname As String
    Public Property Fullname As String
        Get
            Return _fullname
        End Get
        Set(ByVal Value As String)
            _fullname = Value
        End Set
    End Property
    Private _costCentre As String
    Public Property CostCentre As String
        Get
            Return _costCentre
        End Get
        Set(ByVal Value As String)
            _costCentre = Value
        End Set
    End Property
    Private _job_Title As String
    Public Property Job_Title As String
        Get
            Return _job_Title
        End Get
        Set(ByVal Value As String)
            _job_Title = Value
        End Set
    End Property
    Private _office As String
    Public Property Office As String
        Get
            Return _office
        End Get
        Set(ByVal Value As String)
            _office = Value
        End Set
    End Property

    Public Property Grade As String
        Get
            Return _grade
        End Get
        Set(ByVal Value As String)
            _grade = Value
        End Set
    End Property

    Public Property Discipline As String
        Get
            Return _discipline
        End Get
        Set(ByVal Value As String)
            _discipline = Value
        End Set
    End Property

    Private _dateOfLastVisit As Date
    Public Property DateOfLastVisit As Date
        Get
            Return _dateOfLastVisit
        End Get
        Set(ByVal Value As Date)
            _dateOfLastVisit = Value
        End Set
    End Property
    Private _role As EsoRoles
    Public Property Role As EsoRoles
        Get
            Return _role
        End Get
        Set(ByVal Value As EsoRoles)
            _role = Value
        End Set
    End Property
    Private _dateRoleLastAsked As Date
    Public Property DateRoleLastAsked As Date
        Get
            Return _dateRoleLastAsked
        End Get
        Set(ByVal Value As Date)
            _dateRoleLastAsked = Value
        End Set
    End Property
    Private _visits As Integer
    Public Property Visits As Integer
        Get
            Return _visits
        End Get
        Set(ByVal Value As Integer)
            _visits = Value
        End Set
    End Property
    Private _optionsShowToolTips As Boolean
    Public Property OptionsShowToolTips As Boolean
        Get
            Return _optionsShowToolTips
        End Get
        Set(ByVal Value As Boolean)
            _optionsShowToolTips = Value
        End Set
    End Property
    Private _emailAddress As String
    Public Property EmailAddress As String
        Get
            Return _emailAddress
        End Get
        Set(value As String)
            _emailAddress = value
        End Set
    End Property
#End Region
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="testUser">OPTIONAL e.g. hoarelea\horsleyk </param>
    ''' <remarks></remarks>
    Public Sub New(Optional ByVal testUser As String = "")

        '#### set to true for periodic review of roles ####
        PeriodicallyCheckRoles = True

        errorFlag = False
        errorMessage = ""
        allOk = True

        If Not testUser = "" Then
            _NT_User = testUser
        Else
            'get windows log-in name of user
            'add dummy data if dev machine
            If ConfigurationManager.AppSettings("where") = "dev" Then
                'comment out the users 
                _NT_User = "hoarelea\jonesj"
                '_NT_User = "hoarelea\driscollp"
                '_NT_User = "hoarelea\o'loughlinj"
                '_NT_User = "hoarelea\horsleyk"
                '_NT_User = "hoarelea\hartn"
            Else
                _NT_User = HttpContext.Current.Request.LogonUserIdentity.Name.ToLower
                'incase using home laptop to test (ie windows 7 home premium - only basic autehntication
                If _NT_User.Contains("jj_hp") Then
                    _NT_User = "hoarelea\jonesj"
                End If
                End If
        End If
        'step 1. Get employee info from Nexus DB
        'extra astep required for irish employees
        _NT_User = _NT_User.Replace("'", "''")
        allOk = _getNexusData()
        'step check data valid
        If allOk Then
            allOk = checkNexusData()
        Else
            '''''''''''''''##########################'''''''''''''''''
            'TODO:
        End If
        'step 2. Get/ Set HltsUser Info
        If allOk Then
            allOk = _getHLTSUserInfo()
        End If


    End Sub
    ''' <summary>
    ''' Depending on the user's log in name - get info from the nexus DB
    ''' </summary>
    ''' <remarks></remarks>
    Private Function _getNexusData() As Boolean
        Dim allOkinFunction As Boolean = True
        Contact_ID = 0
        Fullname = ""
        CostCentre = ""
        Job_Title = ""
        Office = ""
        Discipline = ""
        Grade = ""
        EmailAddress = ""
        DateJoined = #11/1/2010#
        If System.Configuration.ConfigurationManager.AppSettings("where") = "dev5555555555555555555555" Then
            Contact_ID = 5732
            Fullname = "Josh Jones"
            CostCentre = "Technical Control"
            Job_Title = "Technical Author"
            Office = "Cardiff"
            EmailAddress = "joshjones@hoarelea.com"
            Return allOkinFunction
        Else
            'union2ConnectionStringForHL-003089
            'Dim MyConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringNexus").ConnectionString)
            Dim MyConnection As New SqlConnection(HttpContext.Current.Application("ConnectionStringNexus"))



            Dim previousConnection As ConnectionState = MyConnection.State
            Dim MyCommand As SqlCommand
            Dim reader As SqlDataReader
            Dim sqlString As String
            Try
                If MyConnection.State = ConnectionState.Closed Then
                    MyConnection.Open()
                End If
                'NexusUserInfo
                sqlString = String.Format("SELECT Contact_ID, Fullname, CostCentre, Job_Title, Office, Grade, Discipline, Convert(varchar, Start_Date, 103) as oDate, Email FROM hlvw_Hoare_Lea_Employees WHERE NT_User = '{0}'", _NT_User)
                MyCommand = New SqlCommand(sqlString, MyConnection)
                reader = MyCommand.ExecuteReader
                If Not reader.HasRows Then
                    Throw New Exception("Nexus find failure :SQL = " & sqlString & vbCrLf & vbCrLf)
                End If
                While reader.Read

                    If Not IsDBNull(reader.GetValue(0)) Then
                        Contact_ID = reader.GetValue(0)
                    End If
                    If Not IsDBNull(reader.GetValue(1)) Then
                        Fullname = reader.GetValue(1)
                    End If
                    If Not IsDBNull(reader.GetValue(2)) Then
                        CostCentre = reader.GetValue(2)
                    End If
                    If Not IsDBNull(reader.GetValue(3)) Then
                        Job_Title = reader.GetValue(3)
                    End If
                    If Not IsDBNull(reader.GetValue(4)) Then
                        Office = reader.GetValue(4)
                    End If
                    If Not IsDBNull(reader.GetValue(5)) Then
                        Grade = reader.GetValue(5)
                    End If
                    If Not IsDBNull(reader.GetValue(6)) Then
                        Discipline = reader.GetValue(6)
                    End If
                    If Not IsDBNull(reader.GetValue(7)) Then
                        DateJoined = CType(reader.GetValue(7), Date)
                    end If
                    
                    If Not IsDBNull(reader.GetValue(8)) Then
                        EmailAddress =reader.GetValue(8)
                    End If
                End While
                reader.Close()
                MyCommand.Dispose()
            Catch ex As Exception
                'loging script needs to go here
                errorFlag = True
                errorMessage = ex.Message
                Tools.SendMail("Nexus Failure: Could not get user data from Nexus: " & vbCrLf & vbCrLf, ex)
                allOkinFunction = False
            Finally
                If previousConnection = ConnectionState.Closed Then
                    MyConnection.Close()
                    MyConnection.Dispose()
                End If
            End Try



        End If

        Return allOkinFunction
    End Function


    Private Function checkNexusData() As Boolean
        'Nexs should have Provided the following:
        'contact_ID
        'Fullname
        'CostCentre
        'Job_Title = reader.GetValue(3)
        'Office = reader.GetValue(4)
        Try
            If Job_Title = "" Then
                Job_Title = "associate"
            End If
        Catch ex As Exception
            Tools.SendMail("Nexus Job_Title Failure: " & Fullname & vbCrLf & vbCrLf, ex)
        End Try
        If Office = "" Then
            Office = "Unknown"
        End If
        Return True
    End Function
    ''' <summary>
    ''' If user has been to HLTS before then get info from DB - if not create new user
    ''' </summary>
    ''' <remarks></remarks>
    Private Function _getHLTSUserInfo() As Boolean
        'is there an entry for this user in the DB?
        'Dim MyConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionStringHLTS").ConnectionString)
        Dim MyConnection As New SqlConnection(HttpContext.Current.Application("ConnectionStringHLTS"))

        Dim previousConnection As ConnectionState = MyConnection.State
        Dim MyCommand As SqlCommand
        Dim reader As SqlDataReader
        Dim sqlString As String
        Dim result As Integer = 0
        Dim allOkInFunction As Boolean = True
        Try
            If MyConnection.State = ConnectionState.Closed Then
                MyConnection.Open()
            End If
            'NexusUserInfo
            sqlString = "SELECT count (*) from hltsUserInfo WHERE Contact_ID = " & _contact_ID
            MyCommand = New SqlCommand(sqlString, MyConnection)
            reader = MyCommand.ExecuteReader
            While reader.Read
                result = reader.GetValue(0)
            End While

            reader.Close()
            MyCommand.Dispose()
            'no result?
            If result = 0 Then
                'get info and put new entry into hltsUserInfo table
                'can we determine if partner/associate job_role?
                '####old###
                '_role = checkRole()
                _role = GetRole(Grade, Discipline, _job_Title, CostCentre)
                _dateRoleLastAsked = DateTime.Now()
                If _role = 9999 Then
                    'we don't know the user's job role so set time - 3 years to force pop-up
                    _dateRoleLastAsked = _dateRoleLastAsked.AddYears(-3)
                    _role = EsoRoles.Admin
                End If
                sqlString = "INSERT INTO  hltsUserInfo (Contact_ID, DateOfLastVisit, Role, DateRoleLastAsked, Visits, OptionsShowToolTips) " & _
                            "VALUES (@Contact_ID, @DateOfLastVisit, @Role, @DateRoleLastAsked, @Visits, @OptionsShowToolTips)"
                MyCommand = New SqlCommand(sqlString, MyConnection)
                MyCommand.Parameters.Add("@Contact_ID", SqlDbType.BigInt).Value = Contact_ID
                MyCommand.Parameters.Add("@DateOfLastVisit", SqlDbType.DateTime).Value = DateTime.Now().AddYears(-3).ToShortDateString
                MyCommand.Parameters.Add("@Role", SqlDbType.Int).Value = Role
                MyCommand.Parameters.Add("@DateRoleLastAsked", SqlDbType.DateTime).Value = DateRoleLastAsked.ToShortDateString
                MyCommand.Parameters.Add("@Visits", SqlDbType.Int).Value = 0
                MyCommand.Parameters.Add("@OptionsShowToolTips", SqlDbType.Bit).Value = 1
                reader = MyCommand.ExecuteReader
                result = reader.RecordsAffected
                If Not result > 0 Then
                    Throw New System.Exception("Could not add new user to hltsUserInfo Table & " & MyCommand.Parameters.ToString)
                End If

                reader.Close()
                MyCommand.Dispose()
            End If

            'now get userInfo
            sqlString = "SELECT DateOfLastVisit, Role, DateRoleLastAsked, Visits, OptionsShowToolTips  from hltsUserInfo WHERE Contact_ID = " & _contact_ID
            MyCommand = New SqlCommand(sqlString, MyConnection)
            reader = MyCommand.ExecuteReader
            If Not reader.HasRows Then
                Throw New System.Exception("Could not find user in HltsUserInfo & " & MyCommand.Parameters.ToString)
            End If
            While reader.Read
                DateOfLastVisit = reader.GetValue(0)
                Role = reader.GetValue(1)
                DateRoleLastAsked = reader.GetValue(2)
                Visits = reader.GetValue(3)
                OptionsShowToolTips = reader.GetValue(4)
            End While
            reader.Close()
            MyCommand.Dispose()
            'put date of last visit back into db and increment visit counter
            If PeriodicallyCheckRoles Then
                _role = GetRole(Grade, Discipline, _job_Title, CostCentre)
                sqlString = "UPDATE hltsUserInfo SET DateOfLastVisit = @DateOfLastVisit, Visits = @Visits, Role = @Role WHERE Contact_ID = " & _contact_ID
                MyCommand = New SqlCommand(sqlString, MyConnection)
                MyCommand.Parameters.Add("@DateOfLastVisit", SqlDbType.DateTime).Value = DateTime.Now()
                Visits += 1
                MyCommand.Parameters.Add("@Visits", SqlDbType.Int).Value = Visits
                MyCommand.Parameters.Add("@Role", SqlDbType.Int).Value = _role
            Else
                sqlString = "UPDATE hltsUserInfo SET DateOfLastVisit = @DateOfLastVisit, Visits = @Visits WHERE Contact_ID = " & _contact_ID
                MyCommand = New SqlCommand(sqlString, MyConnection)
                MyCommand.Parameters.Add("@DateOfLastVisit", SqlDbType.DateTime).Value = DateTime.Now()
                Visits += 1
                MyCommand.Parameters.Add("@Visits", SqlDbType.Int).Value = Visits
            End If

            reader = MyCommand.ExecuteReader
            If reader.RecordsAffected = 0 Then
                Throw New System.Exception("Could not add date of last visit/ number of visits to hltsUserInfo DB & " & MyCommand.Parameters.ToString)
            End If
            reader.Close()
            MyCommand.Dispose()

        Catch ex As Exception
            'loging script needs to go here
            errorFlag = True
            errorMessage = ex.Message
            allOkInFunction = False
        Finally
            If previousConnection = ConnectionState.Closed Then
                MyConnection.Close()
                MyConnection.Dispose()
            End If
        End Try
        Return allOkInFunction
    End Function

    

    Private Function GetRole(ByVal grade As String, ByVal discipline As String, ByVal jobTitle As String, ByVal costCentre As String) As EsoRoles

        Dim role As EsoRoles = EsoRoles.All

        Try
            CheckIfPartner(discipline, jobTitle, costCentre, grade, role)
            If role = EsoRoles.All Then
                CheckIfAssociate(discipline, jobTitle, costCentre, grade, role)
            End If
            'admin for partner and associate covered in individual methods
            If role = EsoRoles.All Then
                CheckIfAdmin(discipline, jobTitle, costCentre, grade, role)
            End If
            If role = EsoRoles.All Then
                CheckIfCAD(discipline, jobTitle, costCentre, grade, role)
            End If
            CheckIfSpecialist(discipline, jobTitle, costCentre, grade, role)
            If role = EsoRoles.All Then
                CheckIfMechOrElec(discipline, jobTitle, costCentre, grade, role)
            End If
            'if we have got to here than we cannot work out role so defaults to admin - must read management bulletins
            If role = EsoRoles.All Then
                UnableToDeterminRole(discipline, jobTitle, costCentre, grade, role)
            End If
        Catch ex As Exception
            UnableToDeterminRole(discipline, jobTitle, costCentre, grade, role, ex.Message)
        End Try



        Return role
    End Function

    Private Sub UnableToDeterminRole(ByVal discipline As String, ByVal jobTitle As String, ByVal costCentre As String, ByVal grade As String, ByRef role As EsoRoles, Optional ByVal exception As String = "")
        'default to admin - ie only management bulletins
        role = EsoRoles.Admin
        'TODO: Email me with details
    End Sub

    Private Shared Sub CheckIfMechOrElec(ByVal discipline As String, ByVal jobTitle As String, ByVal costCentre As String, ByVal grade As String, ByRef role As EsoRoles)
        'electrical engineer
        If discipline.ToLower.Contains("elec") Or grade.ToLower.Contains("elec") Or jobTitle.ToLower.Contains("elec") Then
            role = EsoRoles.ElectricalEngineer
            Exit Sub
        End If
        'mechanical engineer
        If discipline.ToLower.Contains("mech") Or grade.ToLower.Contains("mech") Or jobTitle.ToLower.Contains("mech") Then
            role = EsoRoles.MechanicalEngineer
        End If
    End Sub

    Private Sub CheckIfSpecialist(ByVal discipline As String, ByVal jobTitle As String, ByVal costCentre As String, ByVal grade As String, ByRef role As EsoRoles)
        'Check to see if speciality group
        'Acoustics
        If discipline.ToLower.Contains("acoustics") Or costCentre.ToLower.Contains("acoustics") Then
            If role = EsoRoles.All Then
                role = EsoRoles.Acoustics
            Else
                role = role Or EsoRoles.Acoustics
            End If
        End If
        'Business Development
        If discipline.ToLower.Contains("business development") Or costCentre.ToLower.Contains("business development") Then
            If role = EsoRoles.All Then
                role = EsoRoles.BusinessDevelopment
            Else
                role = role Or EsoRoles.BusinessDevelopment
            End If
        End If
        'Communications
        If discipline.ToLower.Contains("communications") Or costCentre.ToLower.Contains("communications") Then
            If role = EsoRoles.All Then
                role = EsoRoles.Communications
            Else
                role = role Or EsoRoles.Communications
            End If
        End If
        'Engineering Management
        If discipline.ToLower.Contains("engineering management") Or costCentre.ToLower.Contains("engineering management") Then
            If role = EsoRoles.All Then
                role = EsoRoles.EngineeringManagement
            Else
                role = role Or EsoRoles.EngineeringManagement
            End If
        End If
        'Expert Witness
        If discipline.ToLower.Contains("expert witness") Or costCentre.ToLower.Contains("expert witness") Then
            If role = EsoRoles.All Then
                role = EsoRoles.ExpertWitness
            Else
                role = role Or EsoRoles.ExpertWitness
            End If
        End If
        'Fire
        If discipline.ToLower.Contains("fire") Or costCentre.ToLower.Contains("fire") Then
            If role = EsoRoles.All Then
                role = EsoRoles.Fire
            Else
                role = role Or EsoRoles.Fire
            End If
        End If
        'IMS
        If discipline.ToLower.Contains("ims") Or costCentre.ToLower.Contains("ims") Then
            If role = EsoRoles.All Then
                role = EsoRoles.IMS
            Else
                role = role Or EsoRoles.IMS
            End If
        End If
        'Legal & Risk Management
        If discipline.ToLower.Contains("legal") Or costCentre.ToLower.Contains("legal") Then
            If role = EsoRoles.All Then
                role = EsoRoles.LegalAndRiskManagement
            Else
                role = role Or EsoRoles.LegalAndRiskManagement
            End If
        End If
        'Lighting
        If discipline.ToLower.Contains("lighting") Or costCentre.ToLower.Contains("lighting") Then
            If role = EsoRoles.All Then
                role = EsoRoles.Lighting
            Else
                role = role Or EsoRoles.Lighting
            End If
        End If
        'Marketing
        If discipline.ToLower.Contains("marketing") Or costCentre.ToLower.Contains("marketing") Then
            If role = EsoRoles.All Then
                role = EsoRoles.Marketing
            Else
                role = role Or EsoRoles.Marketing
            End If
        End If
        'OperationalEngineering
        If discipline.ToLower.Contains("operational engineering") Or costCentre.ToLower.Contains("operational engineering") Then
            If role = EsoRoles.All Then
                role = EsoRoles.OperationalEngineering
            Else
                role = role Or EsoRoles.OperationalEngineering
            End If
        End If
        'Production
        If discipline.ToLower.Contains("production") Or costCentre.ToLower.Contains("production") Then
            If role = EsoRoles.All Then
                role = EsoRoles.Production
            Else
                role = role Or EsoRoles.Production
            End If
        End If
        'Professional Development
        If discipline.ToLower.Contains("professional development") Or costCentre.ToLower.Contains("professional development") Then
            If role = EsoRoles.All Then
                role = EsoRoles.ProfessionalDevelopment
            Else
                role = role Or EsoRoles.ProfessionalDevelopment
            End If
        End If
        'PropertyServices
        If discipline.ToLower.Contains("property services") Or costCentre.ToLower.Contains("property services") Then
            If role = EsoRoles.All Then
                role = EsoRoles.PropertyServices
            Else
                role = role Or EsoRoles.PropertyServices
            End If
        End If
        'Public Health
        If discipline.ToLower.Contains("public health") Or costCentre.ToLower.Contains("public health") Then
            If role = EsoRoles.All Then
                role = EsoRoles.PublicHealth
            Else
                role = role Or EsoRoles.PublicHealth
            End If
        End If
        'R&D
        If discipline.ToLower.Contains("r&d") Or costCentre.ToLower.Contains("r&d") Then
            If role = EsoRoles.All Then
                role = EsoRoles.RandD
            Else
                role = role Or EsoRoles.RandD
            End If
        End If
        'Refrigeration
        If discipline.ToLower.Contains("refrigeration") Or costCentre.ToLower.Contains("refrigeration") Then
            If role = EsoRoles.All Then
                role = EsoRoles.Refrigeration
            Else
                role = role Or EsoRoles.Refrigeration
            End If
        End If
        'Security
        If discipline.ToLower.Contains("security") Or costCentre.ToLower.Contains("security") Then
            If role = EsoRoles.All Then
                role = EsoRoles.Security
            Else
                role = role Or EsoRoles.Security
            End If
        End If
        'Sustainability
        If discipline.ToLower.Contains("sustainability") Or costCentre.ToLower.Contains("sustainability") Or jobTitle.ToLower.Contains("sustainability") Then
            If role = EsoRoles.All Then
                role = EsoRoles.Sustainability
            Else
                role = role Or EsoRoles.Sustainability
            End If
        End If
        'Technical Control
        If discipline.ToLower.Contains("technical control") Or costCentre.ToLower.Contains("technical control") Then
            If role = EsoRoles.All Then
                role = EsoRoles.TechnicalControl
            Else
                role = role Or EsoRoles.TechnicalControl
            End If
        End If
        'Vertical Transportation
        If discipline.ToLower.Contains("vertical transportation") Or costCentre.ToLower.Contains("vertical transportation") Then
            If role = EsoRoles.All Then
                role = EsoRoles.VerticalTransportation
            Else
                role = role Or EsoRoles.VerticalTransportation
            End If
        End If
        'Virtual Engineering
        If discipline.ToLower.Contains("virtual engineering") Or costCentre.ToLower.Contains("virtual engineering") Then
            If role = EsoRoles.All Then
                role = EsoRoles.VirtualEngineering
            Else
                role = role Or EsoRoles.VirtualEngineering
            End If
        End If

    End Sub

    Private Sub CheckIfCAD(ByVal discipline As String, ByVal jobTitle As String, ByVal costCentre As String, ByVal grade As String, ByRef role As EsoRoles)
        'cad
        If grade.ToLower.Contains("cad") OrElse jobTitle.ToLower.Contains("cad") OrElse discipline.ToLower.Contains("cad") Then
            role = EsoRoles.Cad
        End If
    End Sub



    Private Sub CheckIfAdmin(ByVal discipline As String, ByVal jobTitle As String, ByVal costCentre As String, ByVal grade As String, ByRef role As EsoRoles)
        'Admin
        If costCentre.ToLower.Contains("central finance") OrElse costCentre.ToLower.Contains("central it") OrElse costCentre.ToLower.Contains("human resources") OrElse costCentre.ToLower.Contains("property services") Then
            role = EsoRoles.Admin
        End If
        If grade.ToLower.Contains("admin") Or jobTitle.ToLower.Contains("admin") Then
            role = EsoRoles.Admin
        End If

    End Sub

    Private Sub CheckIfAssociate(ByVal discipline As String, ByVal jobTitle As String, ByVal costCentre As String, ByVal grade As String, ByRef role As EsoRoles)
        'is user an associate?
        If grade.ToLower.Contains("associate") Or jobTitle.ToLower.Contains("associate") Then
            role = EsoRoles.Associate
            'check to see if they are engineers - we do have non-engineering
            '################## add to this if required #######################
            Dim disciplineExclusions() As String = {"central it", "central finance", "human resources"}
            For Each exclusion As String In disciplineExclusions
                If discipline.ToLower.Contains(exclusion) Then
                    'admin associate
                    role = role Or EsoRoles.Admin
                    Exit Sub
                End If
            Next
        End If
    End Sub

    Private Sub CheckIfPartner(ByVal discipline As String, ByVal jobTitle As String, ByVal costCentre As String, ByVal grade As String, ByRef role As EsoRoles)
        'is user a partner?
        If grade.ToLower.Contains("partner") Or jobTitle.ToLower.Contains("partner") Then
            'check for partner's pa
            If grade.ToLower.Contains("admin") Then
                role = EsoRoles.Admin
                Exit Sub
            End If
            role = EsoRoles.Partner
        End If
    End Sub

    
End Class

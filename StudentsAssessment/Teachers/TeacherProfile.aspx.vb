Imports System.Data.SqlClient

Public Class TeacherProfile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SiteHeader1.Pagetitle = "TEACHER INTERFACE"
        SiteHeader1.PageDesc = "A place to modify teacher's profile"
        'Make sure they are logged in.  Otherwise, forece them to log in
        If Session("LoggedinTeacherID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinTeacherID")) Then
            If Session("LoggedinTeacherID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinTeacherID")) Then
                If Session("IsAdmin") IsNot Nothing AndAlso Session("IsAdmin") = True Then
                    'ADMIN has logged in
                    pnlAdminProfile.Visible = True
                    LoadTeachers()
                Else
                    pnlAdminProfile.Visible = False
                End If
                divNotLoggedIn1.Visible = False
                TeachersPanel1.Visible = True
                TeachersPanel1.Name = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
            Else
                TeachersPanel1.Visible = False
                pnlAdminProfile.Visible = False
                divNotLoggedIn1.Visible = True
            End If
            lblTeacherName.Text = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
            lblTeachername1.Text = lblTeacherName.Text
            lblTeachername2.Text = lblTeacherName.Text
            lblTeachername3.Text = lblTeacherName.Text
            If Not IsPostBack Then
                LoadProfile(Integer.Parse(Session("LoggedinTeacherID")))
                LoadClassesTaughtBy(Integer.Parse(Session("LoggedinTeacherID")))
                LoadSubjectsTaughtby(Integer.Parse(Session("LoggedinTeacherID")))
                LoadStudentsTaughtBy(Integer.Parse(Session("LoggedinTeacherID")))
            Else
                'Admin has logged in and picked a teacher.
                If Session("AdminPickedTeacher") IsNot Nothing AndAlso IsNumeric(Session("AdminPickedTeacher")) Then
                    LoadClassesTaughtBy(Integer.Parse(Session("AdminPickedTeacher")))
                    LoadSubjectsTaughtby(Session("AdminPickedTeacher"))
                    LoadStudentsTaughtBy(Integer.Parse(Session("AdminPickedTeacher")))
                    'LoadProfile(Integer.Parse(Session("AdminPickedTeacher")))
                    lblTeacherName.Text = GetTeacherName(Convert.ToInt32(Session("AdminPickedTeacher")))
                    lblTeachername1.Text = lblTeacherName.Text
                    lblTeachername2.Text = lblTeacherName.Text
                    lblTeachername3.Text = lblTeacherName.Text
                Else
                    LoadClassesTaughtBy(Integer.Parse(Session("LoggedinTeacherID")))
                    LoadSubjectsTaughtby(Session("LoggedinTeacherID"))
                    LoadStudentsTaughtBy(Integer.Parse(Session("LoggedinTeacherID")))
                    'LoadProfile(Integer.Parse(Session("LoggedinTeacherID")))
                    lblTeacherName.Text = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
                    lblTeachername1.Text = lblTeacherName.Text
                    lblTeachername2.Text = lblTeacherName.Text
                    lblTeachername3.Text = lblTeacherName.Text
                End If
            End If
            DisableNonModifiableFields()
        Else
            Response.Redirect("~/TeachersLogin.aspx")
        End If
    End Sub

    Private Sub DisableNonModifiableFields()
        If Session("IsAdmin") IsNot Nothing AndAlso Session("IsAdmin") = False Then
            'NON-ADMINS can only edit description and their part-time/fulltime status
            txtFirstname.Enabled = False
            txtLastname.Enabled = False
            txtUsername.Enabled = False
            txtPassword.Enabled = False
            txtJoindate.Enabled = False
            txtTermdate.Enabled = False
            chkblActive.Enabled = False
            chkblAdmin.Enabled = False
        Else
            'ADMINS can edit entire profile except the name of course
            txtFirstname.Enabled = False
            txtLastname.Enabled = False
        End If
    End Sub
    Private Sub LoadProfile(ByVal iTeacherID As Integer)
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Dim objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim strSQL As String = "SELECT * FROM StaffMembers WHERE StaffID=@TeacherID"
            Dim objCommand As New SqlCommand(strSQL, objConn)
            objCommand.Parameters.AddWithValue("@TeacherID", iTeacherID)
            Dim objReader As SqlDataReader = objCommand.ExecuteReader
            If objReader.Read() Then
                txtFirstname.Text = objReader("Firstname").ToString()
                txtLastname.Text = objReader("Lastname").ToString()
                txtDesc.Text = objReader("Description").ToString()
                txtUsername.Text = objReader("Username").ToString()
                txtPassword.Text = objReader("Password").ToString()
                txtJoindate.Text = DateTime.Parse(objReader("Joindate").ToString).ToShortDateString()
                If Not IsDBNull(objReader("Termdate")) Then
                    txtTermdate.Text = DateTime.Parse(objReader("Joindate").ToString).ToShortDateString()
                Else
                    txtTermdate.Text = ""
                End If
                If objReader("Parttime") = True Then
                    chkblPT.Items(0).Selected = True
                Else
                    chkblPT.Items(1).Selected = True
                End If

                If objReader("ISAdmin") = True Then
                    chkblAdmin.Items(0).Selected = True
                Else
                    chkblAdmin.Items(1).Selected = True
                End If

                If objReader("Active") = True Then
                    chkblActive.Items(0).Selected = True
                Else
                    chkblActive.Items(1).Selected = True
                End If
                lblStatus.Text = ""
            Else
                'Some problem loading profile
                lblStatus.Text = "Some problem loading teacher profile.  Please try later"
            End If

            objReader.Close()
            objReader = Nothing
            objCommand.Dispose()
            objCommand = Nothing
            objConn.Dispose()
            objConn.Close()
            objConn = Nothing
            SqlConnection.ClearAllPools()
        Catch ex As Exception
            lblStatus.Text = "Problem with system.  Please let administrator know.  And please retry at a later time"
        End Try
    End Sub

    Protected Sub lnkReturn_Click(sender As Object, e As EventArgs) Handles lnkReturn.Click
        Response.Redirect("~/default.aspx")
    End Sub

    Protected Sub lnkSaveProfile_Click(sender As Object, e As EventArgs) Handles lnkSaveProfile.Click
        If Validdata() Then
            If Session("AdminPickedTeacher") IsNot Nothing AndAlso IsNumeric(Session("AdminPickedTeacher")) Then
                SaveProfile(Integer.Parse(Session("AdminPickedTeacher")))
            Else
                SaveProfile(Integer.Parse(Session("LoggedinTeacherID")))
            End If

        End If
    End Sub

    Private Function Validdata() As Boolean
        Dim bValid As Boolean = True
        If txtDesc.Text.Trim <> "" Then
            If txtDesc.Text.Length > 150 Then
                lblStatus.Text = "Description cannot be more than 150 characters long"
                bValid = False
            End If
        End If
        Return bValid
    End Function
    Private Sub SaveProfile(ByVal iTeacherID As Integer)
        Dim strConn As String = GetConnectStringFromWebConfig()
        Dim objConn As New SqlConnection(strConn)
        Dim bValid As Boolean = False
        'Dim iTeacherID As Integer = Session("LoggedinTeacherID")
        Try
            objConn.Open()
            Dim strSQL As String = "UPDATE StaffMembers SET Description=@Desc, Parttime=@Parttime WHERE StaffID=@TeacherID"
            Dim objCommand As New SqlCommand(strSQL, objConn)
            objCommand.Parameters.AddWithValue("@TeacherID", iTeacherID)
            objCommand.Parameters.AddWithValue("@Desc", txtDesc.Text)
            If chkblPT.Items(0).Selected = True Then
                objCommand.Parameters.AddWithValue("@Parttime", True)
            ElseIf chkblPT.Items(1).Selected = True Then
                objCommand.Parameters.AddWithValue("@Parttime", False)
            End If
            If objCommand.ExecuteNonQuery() <= 0 Then
                lblStatus.Text = "Problem updating profile"
            Else
                lblStatus.Text = "Successfully updated profile"
            End If

            objCommand.Dispose()
            objCommand = Nothing
            objConn.Dispose()
            objConn.Close()
            objConn = Nothing
            SqlConnection.ClearAllPools()
        Catch ex As Exception
            lblStatus.Text = "System Problem.  Please try again later."
        End Try
    End Sub

    Private Sub LoadClassesTaughtBy(ByVal iTeacherID As Integer)
        Dim strClassesBuilder As New StringBuilder
        strClassesBuilder.Append("<ul>")
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Dim objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim strSQL As String = "SELECT [EntryID],TeachersClasses.ClassID,[Classes].Classname FROM [AssessmentDB].[dbo].[TeachersClasses] 
            INNER JOIN Classes on TeachersClasses.ClassID=Classes.ClassID WHERE TeachersClasses.TeacherID=@TeacherID"
            Dim objCommand As New SqlCommand(strSQL, objConn)
            objCommand.Parameters.AddWithValue("@TeacherID", iTeacherID)
            Dim objReader As SqlDataReader = objCommand.ExecuteReader
            Dim bTeachingClasses As Boolean = False
            Dim strDiv As String = ""
            While objReader.Read()
                strClassesBuilder.Append("<li>")
                strClassesBuilder.Append(objReader("Classname"))
                strClassesBuilder.Append("</li>")
                bTeachingClasses = True
            End While
            strClassesBuilder.Append("</ul>")
            ClassesHolder.Controls.Clear()
            If bTeachingClasses = False Then
                ClassesHolder.Controls.Add(New LiteralControl("This teacher currently does not teach any classes"))
            Else
                ClassesHolder.Controls.Add(New LiteralControl(strClassesBuilder.ToString()))
            End If

            objReader.Close()
            objReader = Nothing
            objCommand.Dispose()
            objCommand = Nothing
            objConn.Dispose()
            objConn.Close()
            objConn = Nothing
            SqlConnection.ClearAllPools()
        Catch ex As Exception
            lblStatus.Text = "Problem with system.  Please let administrator know.  And please retry at a later time"
        End Try
    End Sub

    Private Sub LoadStudentsTaughtBy(ByVal iTeacherID As Integer)
        Dim strClassesBuilder As New StringBuilder
        strClassesBuilder.Append("<ul>")
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Dim objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim strSQL As String = "Select TOP 1000 [EntryID],Classname,Firstname + ' ' +Lastname AS Studentname,[TeacherID] FROM [AssessmentDB].[dbo].[ClassesStudents] 
            INNER JOIN Students ON ClassesStudents.StudentID=Students.StudentID INNER JOIN Classes ON ClassesStudents.ClassID=Classes.ClassId 
            WHERE TeacherID=@TeacherID"
            Dim objCommand As New SqlCommand(strSQL, objConn)
            objCommand.Parameters.AddWithValue("@TeacherID", iTeacherID)
            Dim objReader As SqlDataReader = objCommand.ExecuteReader
            Dim bTeachingClasses As Boolean = False
            Dim strDiv As String = ""
            While objReader.Read()
                strClassesBuilder.Append("<li>")
                strClassesBuilder.Append(objReader("Studentname") + " - " + objReader("Classname"))
                strClassesBuilder.Append("</li>")
                bTeachingClasses = True
            End While
            strClassesBuilder.Append("</ul>")
            StudentsHolder1.Controls.Clear()
            If bTeachingClasses = False Then
                StudentsHolder1.Controls.Add(New LiteralControl("This teacher currently does not teach any students in any class"))
            Else
                StudentsHolder1.Controls.Add(New LiteralControl(strClassesBuilder.ToString()))
            End If

            objReader.Close()
            objReader = Nothing
            objCommand.Dispose()
            objCommand = Nothing
            objConn.Dispose()
            objConn.Close()
            objConn = Nothing
            SqlConnection.ClearAllPools()
        Catch ex As Exception
            lblStatus.Text = "Problem with system.  Please let administrator know.  And please retry at a later time"
        End Try
    End Sub

    Private Sub LoadSubjectsTaughtby(ByVal iTeacherID As Integer)
        Dim strSubjectsTaughtBuilder As New StringBuilder
        strSubjectsTaughtBuilder.Append("<ul>")
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Dim objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim strSQL As String = "SELECT EntryID, Classname,Subjectname,[TeacherID] FROM [AssessmentDB].[dbo].[ClassesSubjects] 
            inner join Subjects on ClassesSubjects.subjectid=Subjects.subjectid 
            inner join classes on ClassesSubjects.ClassID=Classes.ClassID where teacherid=@TeacherID"
            Dim objCommand As New SqlCommand(strSQL, objConn)
            objCommand.Parameters.AddWithValue("@TeacherID", iTeacherID)
            Dim objReader As SqlDataReader = objCommand.ExecuteReader
            Dim bTeachingClasses As Boolean = False
            While objReader.Read()
                bTeachingClasses = True
                strSubjectsTaughtBuilder.Append("<li>")
                strSubjectsTaughtBuilder.Append(objReader("Classname").ToString + " - " + objReader("Subjectname").ToString)
                strSubjectsTaughtBuilder.Append("</li>")
            End While
            strSubjectsTaughtBuilder.Append("</ul>")
            SubjectsHolder1.Controls.Clear()
            If bTeachingClasses = False Then
                SubjectsHolder1.Controls.Add(New LiteralControl("This teacher does not teach any subjects already"))
            Else
                SubjectsHolder1.Controls.Add(New LiteralControl(strSubjectsTaughtBuilder.ToString()))
            End If
            objReader.Close()
            objReader = Nothing
            objCommand.Dispose()
            objCommand = Nothing
            objConn.Dispose()
            objConn.Close()
            objConn = Nothing
            SqlConnection.ClearAllPools()
        Catch ex As Exception
            lblStatus.Text = "Problem with system.  Please let administrator know.  And please retry at a later time"
        End Try


    End Sub

    Private Sub LoadTeachers()
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Dim objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim strSQL As String = "SELECT StaffID, Firstname, Lastname FROM StaffMembers WHERE 
            Active>0"
            Dim objCommand As New SqlCommand(strSQL, objConn)
            Dim objReader As SqlDataReader = objCommand.ExecuteReader
            Dim rblClasses As New RadioButtonList
            rblClasses.ID = "ListofTeachers"
            Dim liClass As ListItem
            Dim strDiv As String = ""
            Dim bTeacherFound As Boolean = False
            While objReader.Read()
                bTeacherFound = True
                liClass = New ListItem
                With liClass
                    .Value = objReader("StaffID").ToString
                    .Text = objReader("Firstname").ToString + " " + objReader("Lastname").ToString()
                End With
                'AddHandler lnkClass.Click, AddressOf ListStudents
                rblClasses.Items.Add(liClass)
                lblStatus.Text = ""
            End While
            TeachersHolder.Controls.Clear()
            If bTeacherFound = False Then
                TeachersHolder.Controls.Add(New LiteralControl("No teachers are in the system currently"))
            Else
                TeachersHolder.Controls.Add(rblClasses)
            End If

            objReader.Close()
            objReader = Nothing
            objCommand.Dispose()
            objCommand = Nothing
            objConn.Dispose()
            objConn.Close()
            objConn = Nothing
            SqlConnection.ClearAllPools()
        Catch ex As Exception
            lblStatus.Text = "Problem with system.  Please let administrator know.  And please retry at a later time"
        End Try
    End Sub

    Protected Sub lnkLoadTeacherProfile_Click(sender As Object, e As EventArgs) Handles lnkLoadTeacherProfile.Click
        Dim ctrlControl As Control
        Dim iTeacherID As Integer = 0
        ctrlControl = ClassesHolder.FindControl("ListOfTeachers")
        If ctrlControl IsNot Nothing Then
            Dim rblList As RadioButtonList = CType(ctrlControl, RadioButtonList)
            iTeacherID = rblList.SelectedItem.Value
        Else
            iTeacherID = 0
        End If
        'If a teacher is not (or could not be) selected, then there is problem
        If iTeacherID = 0 Then
            lblStatus.Text = "Problem"
            Session("AdminPickedTeacher") = 0
        Else
            Session("AdminPickedTeacher") = iTeacherID
            LoadClassesTaughtBy(iTeacherID)
            LoadSubjectsTaughtby(iTeacherID)
            LoadStudentsTaughtBy(iTeacherID)
            LoadProfile(iTeacherID)
            lblTeacherName.Text = GetTeacherName(iTeacherID)
            lblTeachername1.Text = lblTeacherName.Text
            lblTeachername2.Text = lblTeacherName.Text
            lblTeachername3.Text = lblTeacherName.Text
        End If
    End Sub
End Class
Imports System.Data.SqlClient

Public Class TeacherClass
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SiteHeader1.Pagetitle = "TEACHER INTERFACE"
        SiteHeader1.PageDesc = "A place to add/remove students to/from class taught by teacher"
        'Make sure they are logged in.  Otherwise, forece them to log in
        If Session("LoggedinTeacherID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinTeacherID")) Then
            If Session("LoggedinTeacherID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinTeacherID")) Then
                If Session("IsAdmin") IsNot Nothing AndAlso Session("IsAdmin") = True Then
                    'ADMIN has logged in
                    divAdminPanel.Visible = True
                    divAdminPanelButton.Visible = True
                    LoadTeachers()
                Else
                    divAdminPanel.Visible = False
                    divAdminPanelButton.Visible = False
                End If
                divNotLoggedIn1.Visible = False
                TeachersPanel1.Visible = True
                TeachersPanel1.Name = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
            Else
                TeachersPanel1.Visible = False
                divNotLoggedIn1.Visible = True
            End If
            If Not IsPostBack Then
                LoadClasses(Integer.Parse(Session("LoggedinTeacherID")))
                LoadStudentsTaughtbyTeacher(Session("LoggedinTeacherID"))
                lblTeacherName.Text = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
                lblTeachername2.Text = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
            Else
                'Admin has logged in and picked a teacher.
                If Session("AdminPickedTeacher") IsNot Nothing AndAlso IsNumeric(Session("AdminPickedTeacher")) Then
                    LoadClasses(Integer.Parse(Session("AdminPickedTeacher")))
                    LoadStudentsTaughtbyTeacher(Session("AdminPickedTeacher"))
                    lblTeacherName.Text = GetTeacherName(Convert.ToInt32(Session("AdminPickedTeacher")))
                    lblTeachername2.Text = GetTeacherName(Convert.ToInt32(Session("AdminPickedTeacher")))
                Else
                    LoadClasses(Integer.Parse(Session("LoggedinTeacherID")))
                    LoadStudentsTaughtbyTeacher(Session("LoggedinTeacherID"))
                    lblTeacherName.Text = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
                    lblTeachername2.Text = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))

                End If
            End If
            LoadStudents()
        Else
                Response.Redirect("~/TeachersLogin.aspx")
        End If
    End Sub

    Private Sub LoadClasses(ByVal iTeacherID As Integer)
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Dim objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim strSQL As String = "SELECT [EntryID],TeachersClasses.ClassID,[Classes].Classname FROM [AssessmentDB].[dbo].[TeachersClasses] 
            INNER JOIN Classes on TeachersClasses.ClassID=Classes.ClassID WHERE TeachersClasses.TeacherID=@TeacherID"
            Dim objCommand As New SqlCommand(strSQL, objConn)
            objCommand.Parameters.AddWithValue("@TeacherID", iTeacherID)
            Dim objReader As SqlDataReader = objCommand.ExecuteReader
            Dim rblClasses As New RadioButtonList
            rblClasses.ID = "ListofClasses"
            Dim liClass As ListItem
            Dim bTeachingClasses As Boolean = False
            Dim strDiv As String = ""
            While objReader.Read()
                liClass = New ListItem
                bTeachingClasses = True
                With liClass
                    .Value = objReader("ClassID").ToString
                    .Text = objReader("Classname").ToString
                End With
                'AddHandler lnkClass.Click, AddressOf ListStudents
                rblClasses.Items.Add(liClass)
                lblStatus.Text = ""
            End While
            ClassesHolder.Controls.Clear()
            If bTeachingClasses = False Then
                ClassesHolder.Controls.Add(New LiteralControl("This teacher currently does not teach any classes"))
            Else
                ClassesHolder.Controls.Add(rblClasses)
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

    Private Sub LoadStudents()
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Dim objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim strSQL As String = "SELECT StudentID, Firstname, Lastname FROM Students WHERE Active>0"
            Dim objCommand As New SqlCommand(strSQL, objConn)
            Dim objReader As SqlDataReader = objCommand.ExecuteReader
            Dim rblClasses As New RadioButtonList
            rblClasses.ID = "ListofStudents"
            Dim liClass As ListItem
            Dim bTeachingClasses As Boolean = False
            Dim strDiv As String = ""
            While objReader.Read()
                liClass = New ListItem
                bTeachingClasses = True
                With liClass
                    .Value = objReader("StudentID").ToString
                    .Text = objReader("Firstname").ToString + " " + objReader("Lastname").ToString()
                End With
                'AddHandler lnkClass.Click, AddressOf ListStudents
                rblClasses.Items.Add(liClass)
                lblStatus.Text = ""
            End While
            StudentsHolder.Controls.Clear()
            If bTeachingClasses = False Then
                StudentsHolder.Controls.Add(New LiteralControl("No students are in the system currently"))
            Else
                StudentsHolder.Controls.Add(rblClasses)
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
            Active>0 AND IsAdmin=0"
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

    Private Sub LoadStudentsTaughtbyTeacher(ByVal iTeacherID As Integer)
        Dim strStudentsTaughtbyBuilder As New StringBuilder
        strStudentsTaughtbyBuilder.Append("<ul>")
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Dim objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim strSQL As String = "Select EntryID,Classname,Firstname+' '+Lastname AS name,[TeacherID] FROM [AssessmentDB].[dbo].[ClassesStudents] 
            INNER JOIN Students on ClassesStudents.StudentID=Students.StudentID 
            INNER JOIN classes ON ClassesStudents.ClassID=Classes.ClassID WHERE teacherid=@TeacherID"
            Dim objCommand As New SqlCommand(strSQL, objConn)
            objCommand.Parameters.AddWithValue("@TeacherID", iTeacherID)
            Dim objReader As SqlDataReader = objCommand.ExecuteReader
            Dim bTeachingClasses As Boolean = False
            While objReader.Read()
                strStudentsTaughtbyBuilder.Append("<li>")
                strStudentsTaughtbyBuilder.Append(objReader("name").ToString + " - " + objReader("classname").ToString)
                strStudentsTaughtbyBuilder.Append("</li>")
                bTeachingClasses = True
                lblStatus.Text = ""
            End While
            strStudentsTaughtbyBuilder.Append("</ul>")
            StudentsHolder1.Controls.Clear()
            If bTeachingClasses = False Then
                StudentsHolder1.Controls.Add(New LiteralControl("This teacher does not teach any student already"))
            Else
                StudentsHolder1.Controls.Add(New LiteralControl(strStudentsTaughtbyBuilder.ToString()))
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

    Protected Sub lnkAddStudents_Click(sender As Object, e As EventArgs) Handles lnkAddStudents.Click
        Dim ctrlControl As Control
        Dim iClassID As Integer = 0
        Dim iStudentID As Integer = 0
        ctrlControl = ClassesHolder.FindControl("ListofClasses")
        Try
            If ctrlControl IsNot Nothing Then
                Dim rblList As RadioButtonList = CType(ctrlControl, RadioButtonList)
                If rblList.SelectedItem IsNot Nothing Then
                    iClassID = rblList.SelectedItem.Value
                    ctrlControl = StudentsHolder.FindControl("ListOfStudents")
                    If ctrlControl IsNot Nothing Then
                        rblList = CType(ctrlControl, RadioButtonList)
                        If rblList.SelectedItem IsNot Nothing Then
                            iStudentID = rblList.SelectedItem.Value
                        Else
                            lblStatus.Text = "Please select the student who you want to add to the class"
                            Exit Sub
                        End If
                    Else
                        iStudentID = 0
                    End If
                Else
                    lblStatus.Text = "Please select the class to which you want to add a student"
                    Exit Sub
                End If
            Else
                iClassID = 0
            End If
            'If either selected class or selected student could not be determined
            If iClassID = 0 Or iStudentID = 0 Then
                lblStatus.Text = "Problem.  Please contact administrator"
            Else
                If Not StudentinClass(iClassID, iStudentID) Then
                    If Session("IsAdmin") IsNot Nothing AndAlso Session("IsAdmin") = True Then
                        'ADMIN has logged in
                        AddStudentToClass(iClassID, iStudentID, Integer.Parse(Session("AdminPickedTeacher")))

                        LoadStudentsTaughtbyTeacher(Integer.Parse(Session("AdminPickedTeacher"))) 'Refresh that list
                    Else
                        AddStudentToClass(iClassID, iStudentID, Integer.Parse(Session("LoggedinTeacherID")))
                        LoadStudentsTaughtbyTeacher(Integer.Parse(Session("LoggedinTeacherID"))) 'Refresh that list
                    End If
                Else
                    lblStatus.Text = "Selected Student already in Selected class"
                End If

            End If
        Catch ex As Exception
            lblStatus.Text = "Problem.  Please contact administrator"
        End Try

    End Sub

    Private Sub AddStudentToClass(ByVal iClassID As Integer, iStudentID As Integer, iTeacherID As Integer)
        Dim strConn As String = GetConnectStringFromWebConfig()
        Dim objConn As New SqlConnection(strConn)
        Dim bValid As Boolean = False
        Try
            objConn.Open()
            Dim strSQL As String = "INSERT INTO ClassesStudents (ClassID,StudentID,TeacherID) VALUES(
            @ClassID,@StudentID,@TeacherID)"
            Dim objCommand As New SqlCommand(strSQL, objConn)
            objCommand.Parameters.AddWithValue("@ClassID", iClassID)
            objCommand.Parameters.AddWithValue("@StudentID", iStudentID)
            objCommand.Parameters.AddWithValue("@TeacherID", iTeacherID)
            If objCommand.ExecuteNonQuery() <= 0 Then
                lblStatus.Text = "Problem adding student to class"
            Else
                lblStatus.Text = "Successfully added student to class"
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

    Private Sub RemoveStudentFromClass(ByVal iClassID As Integer, iStudentID As Integer, ByVal iTeacherID As Integer)
        Dim strConn As String = GetConnectStringFromWebConfig()
        Dim objConn As New SqlConnection(strConn)
        Dim bValid As Boolean = False
        Try
            objConn.Open()
            Dim strSQL As String = "DELETE FROM ClassesStudents WHERE ClassID=@ClassID AND
            StudentID=@StudentID AND TeacherId=@TeacherId"
            Dim objCommand As New SqlCommand(strSQL, objConn)
            objCommand.Parameters.AddWithValue("@ClassID", iClassID)
            objCommand.Parameters.AddWithValue("@StudentID", iStudentID)
            objCommand.Parameters.AddWithValue("@TeacherID", iTeacherID)
            If objCommand.ExecuteNonQuery() <= 0 Then
                lblStatus.Text = "Problem removing student from class"
            Else
                lblStatus.Text = "Successfully removed student from class"
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

    Private Function StudentinClass(ByVal iClassID As Integer, iStudentID As Integer) As Boolean
        Dim bInClass As Boolean = False
        Dim iCnt As Integer = 0
        Dim strConn As String = GetConnectStringFromWebConfig()
        Dim objConn As New SqlConnection(strConn)
        Dim bValid As Boolean = False
        Try
            objConn.Open()
            Dim strSQL As String = "SELECT COUNT(*) FROM ClassesStudents WHERE ClassID=@ClassID AND
            StudentID=@StudentID"
            Dim objCommand As New SqlCommand(strSQL, objConn)
            objCommand.Parameters.AddWithValue("@ClassID", iClassID)
            objCommand.Parameters.AddWithValue("@StudentID", iStudentID)
            iCnt = objCommand.ExecuteScalar()
            bInClass = iCnt > 0
            objCommand.Dispose()
            objCommand = Nothing
            objConn.Dispose()
            objConn.Close()
            objConn = Nothing
            SqlConnection.ClearAllPools()
        Catch ex As Exception
            lblStatus.Text = "System Problem.  Please try again later."
            bInClass = False
        End Try
        Return bInClass
    End Function

    Protected Sub lnkRemoveStudents_Click(sender As Object, e As EventArgs) Handles lnkRemoveStudents.Click
        Dim ctrlControl As Control
        Dim iClassID As Integer = 0
        Dim iStudentID As Integer = 0
        ctrlControl = ClassesHolder.FindControl("ListOfClasses")
        Try
            If ctrlControl IsNot Nothing Then
                Dim rblList As RadioButtonList = CType(ctrlControl, RadioButtonList)
                If rblList.SelectedItem IsNot Nothing Then
                    iClassID = rblList.SelectedItem.Value
                    ctrlControl = StudentsHolder.FindControl("ListOfStudents")
                    If ctrlControl IsNot Nothing Then
                        rblList = CType(ctrlControl, RadioButtonList)
                        If rblList.SelectedItem IsNot Nothing Then
                            rblList = CType(ctrlControl, RadioButtonList)
                            iStudentID = rblList.SelectedItem.Value
                        Else
                            lblStatus.Text = "Please select the student who you want to remove from the class"
                            Exit Sub
                        End If
                    Else
                        iStudentID = 0
                    End If
                Else
                    lblStatus.Text = "Please select the class from which you want to remove a student"
                    Exit Sub
                End If

            Else
                iClassID = 0
            End If
            'If either selected class or selected student could not be determined
            If iClassID = 0 Or iStudentID = 0 Then
                lblStatus.Text = "Problem.  Please contact administrator"
            Else
                If StudentinClass(iClassID, iStudentID) Then
                    If Session("IsAdmin") IsNot Nothing AndAlso Session("IsAdmin") = True Then
                        'ADMIN has logged in
                        RemoveStudentFromClass(iClassID, iStudentID, Integer.Parse(Session("AdminPickedTeacher")))
                        LoadStudentsTaughtbyTeacher(Integer.Parse(Session("AdminPickedTeacher"))) 'Refresh that list
                    Else
                        RemoveStudentFromClass(iClassID, iStudentID, Integer.Parse(Session("LoggedinTeacherID")))
                        LoadStudentsTaughtbyTeacher(Integer.Parse(Session("LoggedinTeacherID"))) 'Refresh that list
                    End If

                Else
                    lblStatus.Text = "Selected Student NOT in Selected class"
                End If

            End If
        Catch ex As Exception
            lblStatus.Text = "Problem.  Please contact administrator"
        End Try

    End Sub

    Protected Sub lnkLoadTeachers_Click(sender As Object, e As EventArgs) Handles lnkLoadTeachers.Click
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
            LoadClasses(iTeacherID)
            LoadStudentsTaughtbyTeacher(iTeacherID)
            lblTeacherName.Text = GetTeacherName(iTeacherID)
            lblTeachername2.Text = GetTeacherName(iTeacherID)
        End If
    End Sub
End Class
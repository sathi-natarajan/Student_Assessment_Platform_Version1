Imports System.Data.SqlClient

Public Class TeacherSubject
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SiteHeader1.Pagetitle = "TEACHER INTERFACE"
        SiteHeader1.PageDesc = "A place to add/remove subjects to/from class taught by teacher"
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
                lblTeacherName.Text = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
                lblTeachername1.Text = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
                LoadClasses(Integer.Parse(Session("LoggedinTeacherID")))
                LoadSubjectsTaughtbyTeacher(Session("LoggedinTeacherID"))
                LoadSubjects()
            Else
                'Admin has logged in and picked a teacher.
                If Session("AdminPickedTeacher") IsNot Nothing AndAlso IsNumeric(Session("AdminPickedTeacher")) Then
                    LoadClasses(Integer.Parse(Session("AdminPickedTeacher")))
                    LoadSubjectsTaughtbyTeacher(Session("AdminPickedTeacher"))
                    lblTeacherName.Text = GetTeacherName(Convert.ToInt32(Session("AdminPickedTeacher")))
                    lblTeachername1.Text = GetTeacherName(Convert.ToInt32(Session("AdminPickedTeacher")))
                Else
                    LoadClasses(Integer.Parse(Session("LoggedinTeacherID")))
                    LoadSubjectsTaughtbyTeacher(Session("LoggedinTeacherID"))
                    lblTeacherName.Text = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
                    lblTeachername1.Text = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
                End If
                LoadSubjects()
            End If
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

    Private Sub LoadSubjectsTaughtbyTeacher(ByVal iTeacherID As Integer)
        Dim strConn As String = GetConnectStringFromWebConfig()
        Dim strSubjectsTaughtBuilder As New StringBuilder
        strSubjectsTaughtBuilder.Append("<ul>")
        Try
            Dim objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim strSQL As String = "SELECT EntryID, Classname,Subjectname,[TeacherID] FROM [AssessmentDB].[dbo].[ClassesSubjects] 
            inner join Subjects on ClassesSubjects.subjectid=Subjects.subjectid 
            inner join classes on ClassesSubjects.ClassID=Classes.ClassID where teacherid=@TeacherID"
            Dim objCommand As New SqlCommand(strSQL, objConn)
            objCommand.Parameters.AddWithValue("@TeacherID", iTeacherID)
            Dim objReader As SqlDataReader = objCommand.ExecuteReader
            Dim bTeachingSubjects As Boolean = False
            Dim strDiv As String = ""
            While objReader.Read()
                bTeachingSubjects = True
                strSubjectsTaughtBuilder.Append("<li>")
                strSubjectsTaughtBuilder.Append(objReader("Classname").ToString + " - " + objReader("Subjectname").ToString)
                strSubjectsTaughtBuilder.Append("</li>")
                lblStatus.Text = ""
            End While
            SubjectsHolder1.Controls.Clear()
            If bTeachingSubjects = False Then
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

    Private Sub LoadSubjects()
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Dim objConn As New SqlConnection(strConn)
            objConn.Open()
            Dim strSQL As String = "SELECT SubjectID, Subjectname FROM Subjects WHERE Active>0"
            Dim objCommand As New SqlCommand(strSQL, objConn)
            Dim objReader As SqlDataReader = objCommand.ExecuteReader
            Dim rblClasses As New RadioButtonList
            rblClasses.ID = "ListofSubjects"
            Dim liClass As ListItem
            Dim bTeachingClasses As Boolean = False
            Dim strDiv As String = ""
            While objReader.Read()
                liClass = New ListItem
                bTeachingClasses = True
                With liClass
                    .Value = objReader("SubjectID").ToString
                    .Text = objReader("Subjectname").ToString
                End With
                'AddHandler lnkClass.Click, AddressOf ListStudents
                rblClasses.Items.Add(liClass)
                lblStatus.Text = ""
            End While
            SubjectsHolder.Controls.Clear()
            If bTeachingClasses = False Then
                SubjectsHolder.Controls.Add(New LiteralControl("No ACTIVE subjects are in the system currently"))
            Else
                SubjectsHolder.Controls.Add(rblClasses)
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
            Active>0 and ISAdmin=0"
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
                TeachersHolder.Controls.Add(New LiteralControl("No ACTIVE teachers are in the system currently"))
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


    Protected Sub lnkReturn_Click(sender As Object, e As EventArgs) Handles lnkReturn.Click
        Response.Redirect("~/default.aspx")
    End Sub

    Protected Sub lnkAddSubjects_Click(sender As Object, e As EventArgs) Handles lnkAddSubjects.Click
        Dim ctrlControl As Control
        Dim iClassID As Integer = 0
        Dim iSubjectID As Integer = 0
        ctrlControl = ClassesHolder.FindControl("ListOfClasses")
        Try
            If ctrlControl IsNot Nothing Then
                Dim rblList As RadioButtonList = CType(ctrlControl, RadioButtonList)
                If rblList.SelectedItem IsNot Nothing Then
                    iClassID = rblList.SelectedItem.Value
                    ctrlControl = SubjectsHolder.FindControl("ListOfSubjects")
                    If ctrlControl IsNot Nothing Then
                        rblList = CType(ctrlControl, RadioButtonList)
                        If rblList.SelectedItem IsNot Nothing Then
                            iSubjectID = rblList.SelectedItem.Value
                        Else
                            lblStatus.Text = "Please select the subject to be added the class"
                            Exit Sub
                        End If

                    Else
                        iSubjectID = 0
                    End If
                Else
                    lblStatus.Text = "Please select the class to which you want to add a subject to be taught"
                    Exit Sub
                End If

            Else
                iClassID = 0
            End If
            'If either selected class or selected student could not be determined
            If iClassID = 0 Or iSubjectID = 0 Then
                lblStatus.Text = "Problem.  Please contact administrator"
            Else
                If Not SubjectTaughtinClass(iClassID, iSubjectID) Then
                    If Session("IsAdmin") IsNot Nothing AndAlso Session("IsAdmin") = True Then
                        'ADMIN has logged in
                        AddSubjectToClass(iClassID, iSubjectID, Integer.Parse(Session("AdminPickedTeacher")))
                        LoadSubjectsTaughtbyTeacher(Integer.Parse(Session("AdminPickedTeacher")))
                    Else
                        AddSubjectToClass(iClassID, iSubjectID, Integer.Parse(Session("LoggedinTeacherID")))
                        LoadSubjectsTaughtbyTeacher(Integer.Parse(Session("LoggedinTeacherID"))) 'Refresh that list
                    End If
                Else
                    lblStatus.Text = "Selected Subject already taught in selected class"
                End If

            End If
        Catch ex As Exception
            lblStatus.Text = "Problem.  Please contact administrator"
        End Try

    End Sub

    Private Sub AddSubjectToClass(ByVal iClassID As Integer, iSubjectID As Integer, iTeacherID As Integer)
        Dim strConn As String = GetConnectStringFromWebConfig()
        Dim objConn As New SqlConnection(strConn)
        Dim bValid As Boolean = False
        Try
            objConn.Open()
            Dim strSQL As String = "INSERT INTO ClassesSubjects (ClassID,SubjectID,TeacherID) VALUES(
            @ClassID,@SubjectID,@TeacherID)"
            Dim objCommand As New SqlCommand(strSQL, objConn)
            objCommand.Parameters.AddWithValue("@ClassID", iClassID)
            objCommand.Parameters.AddWithValue("@SubjectID", iSubjectID)
            objCommand.Parameters.AddWithValue("@TeacherID", iTeacherID)
            If objCommand.ExecuteNonQuery() <= 0 Then
                lblStatus.Text = "Problem adding subject to class"
            Else
                lblStatus.Text = "Successfully added subject to class.  Use RETURN button to go back to HOME - MAIN SCREEN"
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

    Private Sub RemoveSubjectFromClass(ByVal iClassID As Integer, iSubjectID As Integer)
        Dim strConn As String = GetConnectStringFromWebConfig()
        Dim objConn As New SqlConnection(strConn)
        Dim bValid As Boolean = False
        Try
            objConn.Open()
            Dim strSQL As String = "DELETE FROM ClassesSubjects WHERE ClassID=@ClassID AND
            SubjectID=@SubjectID"
            Dim objCommand As New SqlCommand(strSQL, objConn)
            objCommand.Parameters.AddWithValue("@ClassID", iClassID)
            objCommand.Parameters.AddWithValue("@SubjectID", iSubjectID)
            If objCommand.ExecuteNonQuery() <= 0 Then
                lblStatus.Text = "Problem removing subject from class"
            Else
                lblStatus.Text = "Successfully removed subject from class.  Use RETURN button to go back to HOME - MAIN SCREEN"
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

    Private Function SubjectTaughtinClass(ByVal iClassID As Integer, iSubjectID As Integer) As Boolean
        Dim bInClass As Boolean = False
        Dim iCnt As Integer = 0
        Dim strConn As String = GetConnectStringFromWebConfig()
        Dim objConn As New SqlConnection(strConn)
        Dim bValid As Boolean = False
        Try
            objConn.Open()
            Dim strSQL As String = "SELECT COUNT(*) FROM ClassesSubjects WHERE ClassID=@ClassID AND
            SubjectID=@SubjectID"
            Dim objCommand As New SqlCommand(strSQL, objConn)
            objCommand.Parameters.AddWithValue("@ClassID", iClassID)
            objCommand.Parameters.AddWithValue("@SubjectID", iSubjectID)
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

    Protected Sub lnkRemoveSubjects_Click(sender As Object, e As EventArgs) Handles lnkRemoveSubject.Click
        Dim ctrlControl As Control
        Dim iClassID As Integer = 0
        Dim iSubjectID As Integer = 0
        ctrlControl = ClassesHolder.FindControl("ListOfClasses")
        Try
            If ctrlControl IsNot Nothing Then
                Dim rblList As RadioButtonList = CType(ctrlControl, RadioButtonList)
                If rblList.SelectedItem IsNot Nothing Then
                    iClassID = rblList.SelectedItem.Value
                    ctrlControl = SubjectsHolder.FindControl("ListOfSubjects")
                    If ctrlControl IsNot Nothing Then
                        rblList = CType(ctrlControl, RadioButtonList)
                        If rblList.SelectedItem IsNot Nothing Then
                            iSubjectID = rblList.SelectedItem.Value
                        Else
                            lblStatus.Text = "Please select the subject you want to remove from this class"
                            Exit Sub
                        End If
                    Else
                        iSubjectID = 0
                    End If
                Else
                    lblStatus.Text = "Please select the class from which you want to remove a subject"
                    Exit Sub
                End If
            Else
                iClassID = 0
            End If
            'If either selected class or selected student could not be determined
            If iClassID = 0 Or iSubjectID = 0 Then
                lblStatus.Text = "Problem.  Please contact administrator"
            Else
                If SubjectTaughtinClass(iClassID, iSubjectID) Then
                    RemoveSubjectFromClass(iClassID, iSubjectID)
                    If Session("IsAdmin") IsNot Nothing AndAlso Session("IsAdmin") = True Then
                        'ADMIN has logged in
                        LoadSubjectsTaughtbyTeacher(Integer.Parse(Session("AdminPickedTeacher"))) 'Refresh that list
                    Else
                        LoadSubjectsTaughtbyTeacher(Integer.Parse(Session("LoggedinTeacherID"))) 'Refresh that list
                    End If
                Else
                    lblStatus.Text = "Selected subject NOT taught in Selected class"
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
            lblTeacherName.Text = GetTeacherName(iTeacherID)
            lblTeachername1.Text = GetTeacherName(iTeacherID)
            LoadSubjectsTaughtbyTeacher(iTeacherID)
        End If
    End Sub

End Class
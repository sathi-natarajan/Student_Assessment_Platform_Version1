Imports System.Data.SqlClient

Public Class TeachersLogin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SiteHeader1.Pagetitle = "TEACHER INTERFACE"
        SiteHeader1.PageDesc = "A place for teachers to log into system"
        txtUsername.Focus()
    End Sub

    Protected Sub lnkReturn_Click(sender As Object, e As EventArgs) Handles lnkReturn.Click
        Response.Redirect("default.aspx")
    End Sub

    Protected Sub lnkTeacherLogin_Click(sender As Object, e As EventArgs) Handles lnkTeacherLogin.Click
        Dim strConn As String = GetConnectString()
        Dim objConn As New SqlConnection(strConn)
        Dim bValid As Boolean = False
        Dim iTeacherID As Integer = 0
        Try
            objConn.Open()
            'Dim strSQL As String = "SELECT StaffID, Username, Password FROM StaffMembers WHERE Username='" + txtUsername.Text + "' AND Password='" + txtPassword.Text + "'"
            Dim strSQL As String = "SELECT StaffID, Username, Password,ISAdmin FROM StaffMembers WHERE Username=@Username AND Password=@Password AND Active>0"
            Dim objCommand As New SqlCommand(strSQL, objConn)
            objCommand.Parameters.AddWithValue("@Username", txtUsername.Text)
            objCommand.Parameters.AddWithValue("@Password", txtPassword.Text)
            Dim objReader As SqlDataReader = objCommand.ExecuteReader
            Dim strFullnameBuilder As New StringBuilder
            If objReader.Read() Then
                bValid = True
                If objReader("IsAdmin") = True Then
                    Session("IsAdmin") = True
                Else
                    Session("IsAdmin") = False
                End If
                iTeacherID = Convert.ToInt32(objReader("StaffID"))
            Else
                bValid = False
                iTeacherID = 0
                Session("IsAdmin") = False
            End If
            objReader.Close()
            objReader = Nothing
            objCommand.Dispose()
            objCommand = Nothing
            objConn.Dispose()
            objConn.Close()
            objConn = Nothing
            SqlConnection.ClearAllPools()
            If bValid = False Then
                lblStatus.Text = "Invalid Username or Password.  Please retry."
                Session("LoggedinTeacherID") = Nothing
                Session("IsAdmin") = False
                Session.Clear()
            Else
                Session("LoggedinTeacherID") = iTeacherID
                Response.Redirect("~/default.aspx")
            End If
        Catch ex As Exception
            lblStatus.Text = "System Problem.  Please try again later."
        End Try
    End Sub
End Class
Public Class MainLinksControl
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub lnkTakeTest_Click(sender As Object, e As EventArgs) Handles lnkTakeTest.Click
        Response.Redirect("~/TakeTest.aspx")
    End Sub

    Protected Sub lnkteachersLogin_Click(sender As Object, e As EventArgs) Handles lnkteachersLogin.Click
        Response.Redirect("~/TeachersLogin.aspx")
    End Sub

    Protected Sub Linkbutton1_Click(sender As Object, e As EventArgs) Handles Linkbutton1.Click
        Response.Redirect("~/StudentsLogin.aspx")
    End Sub
End Class
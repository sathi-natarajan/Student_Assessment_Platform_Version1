Public Class TeachersMenu
    Inherits System.Web.UI.UserControl

    Public Enum OptiontoHighlight
        Home
        MyQB
        CreateAssessment
        CommunityQB
        DataAnalytics
    End Enum
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public WriteOnly Property HiglightOption() As OptiontoHighlight
        Set(value As OptiontoHighlight)
            Select Case value
                Case OptiontoHighlight.Home
                    divOption1.Style.Add("background-color", "red")
                Case OptiontoHighlight.MyQB
                    divOption2.Style.Add("background-color", "red")
                Case OptiontoHighlight.CreateAssessment
                    divOption3.Style.Add("background-color", "red")
                Case OptiontoHighlight.CommunityQB
                    divOption4.Style.Add("background-color", "red")
                Case OptiontoHighlight.DataAnalytics
                    divOption5.Style.Add("background-color", "red")
                Case Else
                    divOption1.Style.Add("background-color", "red")
            End Select
        End Set
    End Property

    Protected Sub lnkHome_Click(sender As Object, e As EventArgs) Handles lnkHome.Click
        Response.Redirect("~/default.aspx")
    End Sub

    Protected Sub lnkMyQB_Click(sender As Object, e As EventArgs) Handles lnkMyQB.Click
        If Session("LoggedinTeacherID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinTeacherID")) Then
            Response.Redirect("~/MyQB.aspx")
        Else
            Response.Redirect("TeachersLogin.aspx")
        End If
    End Sub

    Protected Sub lnkCreateAssessmt_Click(sender As Object, e As EventArgs) Handles lnkCreateAssessmt.Click
        If Session("LoggedinTeacherID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinTeacherID")) Then
            Response.Redirect("~/Assessments.aspx")
        Else
            Response.Redirect("TeachersLogin.aspx")
        End If
    End Sub

    Protected Sub lnkCommunityQB_Click(sender As Object, e As EventArgs) Handles lnkCommunityQB.Click
        If Session("LoggedinTeacherID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinTeacherID")) Then
            Response.Redirect("~/CommunityQB.aspx")
        Else
            Response.Redirect("CommunityQB.aspx")
        End If
    End Sub

    Protected Sub lnkDataAnalytics_Click(sender As Object, e As EventArgs) Handles lnkDataAnalytics.Click
        Response.Redirect("~/DataAnalytics.aspx")
    End Sub
End Class
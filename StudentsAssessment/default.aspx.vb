Public Class _default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Show the teacher's section only when a teacher logs in.
        If Session("LoggedinTeacherID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinTeacherID")) Then
            divNotLoggedIn1.Visible = False
            TeachersPanel1.Visible = True
            TeachersPanel1.Name = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
        Else
            TeachersPanel1.Visible = False
            divNotLoggedIn1.Visible = True
        End If
        SiteHeader1.Pagetitle = "HOME - MAIN SCREEN"
        SiteHeader1.PageDesc = "A Student Assessment Web application for use in the field of education"
        LoadDashboard()
        LoadAnn1()
        LoadAnn2()
    End Sub

    Private Sub LoadDashboard()
        Dim strDashboardCBuilder As New StringBuilder
        strDashboardCBuilder.Append("<div>")
        strDashboardCBuilder.Append("<ul>")
        strDashboardCBuilder.Append("<li>Key Metric1</li>")
        strDashboardCBuilder.Append("<li>Key Metric2</li>")
        strDashboardCBuilder.Append("<li>Key Metric3</li>")
        strDashboardCBuilder.Append("<li>Key Metric4</li>")
        strDashboardCBuilder.Append("<li>Key Metric5</li>")
        strDashboardCBuilder.Append("</ul>")
        strDashboardCBuilder.Append("</div>")
        divDashboardC.InnerHtml = strDashboardCBuilder.ToString()
    End Sub

    Private Sub LoadAnn1()
        Dim strAnn1Builder As New StringBuilder
        strAnn1Builder.Append("<div>")
        strAnn1Builder.Append("<ul>")
        strAnn1Builder.Append("<li>Newsfeed items</li>")
        strAnn1Builder.Append("<li>Teacher Alerts</li>")
        strAnn1Builder.Append("<li>No. students yet to take recent test</li>")
        strAnn1Builder.Append("<li>Test Completion rate</li>")
        strAnn1Builder.Append("<li>Etc</li>")
        strAnn1Builder.Append("</ul>")
        strAnn1Builder.Append("</div>")
        divAnnouncement1C.InnerHtml = strAnn1Builder.ToString()
    End Sub

    Private Sub LoadAnn2()
        Dim strAnn2Builder As New StringBuilder
        strAnn2Builder.Append("<div>")
        strAnn2Builder.Append("<ul>")
        strAnn2Builder.Append("<li>New Announcements from admin</li>")
        strAnn2Builder.Append("<li>New Test anouncements</li>")
        strAnn2Builder.Append("<li>Info. on contents added to site</li>")
        strAnn2Builder.Append("</ul>")
        strAnn2Builder.Append("</div>")
        divAnnouncement2C.InnerHtml = strAnn2Builder.ToString()
    End Sub
End Class
Public Class SiteHeader
    Inherits System.Web.UI.UserControl

    Dim strPagetitle As String = ""
    Dim strPagedesc As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Public Property Pagetitle() As String
        Set(value As String)
            strPagetitle = value
            lblHeaderText.Text = value
        End Set
        Get
            Return strPagetitle
        End Get
    End Property

    Public Property PageDesc() As String
        Set(value As String)
            strPageDesc = value
            lblPageDesc.Text = value
        End Set
        Get
            Return strPagedesc
        End Get
    End Property
End Class
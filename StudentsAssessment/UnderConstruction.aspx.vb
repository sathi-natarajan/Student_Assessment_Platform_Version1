Imports System.Data.SqlClient

Public Class UnderConstruction
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SiteHeader1.Pagetitle = "SITE FEATURE UNDER CONSTRUCTION"
    End Sub

    Protected Sub lnkReturn_Click(sender As Object, e As EventArgs) Handles lnkReturn.Click
        Response.Redirect("default.aspx")
    End Sub

End Class
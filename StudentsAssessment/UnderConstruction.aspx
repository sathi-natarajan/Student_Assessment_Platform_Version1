<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UnderConstruction.aspx.vb" Inherits="StudentsAssessment.UnderConstruction" %>

<%@ Register TagPrefix="uc1" TagName="SiteHeader" Src="SiteHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteFooter" Src="SiteFooter.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Teachers Interface - Home</title>
    <link href="Styles.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <uc1:SiteHeader id="SiteHeader1" runat="server"></uc1:SiteHeader>
    <div id="Content">
        <div id="divConstructionMsg">
            This section of the application is currently under construction.  Please
            come back later.
        </div>
        <p>&nbsp;</p>
        <div style="height:20px;width:112px; text-align:center;background-color:darkgreen;margin-left:200px;">
                <asp:LinkButton ID="lnkReturn" runat="server" Font-bold="true" Font-Underline="false" ForeColor="White">RETURN</asp:LinkButton>
        </div> 
    </div>
        <p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p>
        <uc1:SiteFooter id="SiteFooter" runat="server"></uc1:SiteFooter>
    </form>
</body>
</html>

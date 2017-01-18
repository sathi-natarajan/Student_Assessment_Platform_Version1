<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="StudentsAssessment._default" %>
<%@ Register TagPrefix="uc1" TagName="TeachersPanel" Src="Teachers/TeachersLoggedInPanel.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TeachersMenu" Src="Teachers/TeachersMenu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MainLinksControl" Src="MainLinksControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteHeader" Src="SiteHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteFooter" Src="SiteFooter.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Assessment Platform - Home</title>
    <link href="Styles.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <uc1:SiteHeader id="SiteHeader1" runat="server"></uc1:SiteHeader>
    <div id="Content">
        <div id="divLoggedinPanel">
            <div id="divTeachersLoginInfo">
                <uc1:TeachersPanel id="TeachersPanel1" runat="server"></uc1:TeachersPanel>
	        </div>
            <div id="divNotLoggedIn1" runat="server" style="background-color:#000;color:#fff;font-weight:bold;">
                NO TEACHER OR ADMIN IS CURRENTLY LOGGED IN
            </div>
        </div>
        <br /><br /><br />
	<div id="divMainLinksControl">
		 <uc1:MainLinksControl id="MainLinksControl1" runat="server"></uc1:MainLinksControl>
	</div>
	<p>&nbsp;</p>
	<div id="divTeachersSidePanel">
		<uc1:TeachersMenu id="TeachersMenu1" runat="server"></uc1:TeachersMenu>
	</div>
    <table id="tblDynamicInfo" border="0" cellpadding="2" cellspacing="2">
        <colgroup>
            <col width="400" />
            <col width="400" />
        </colgroup>
        <tr>
            <td colspan="2">
                <div id="divMainDashboard">
                    <div id="divMainDashboard_heading">
                        <asp:Label ID="lblWelcomeH" runat="server" Font-Bold="true" ForeColor="white">
                            <strong>MAIN DASHBOARD - KEY METRICS</strong>
                        </asp:Label>
                    </div>   
                     <div id="divDashboardC" runat="server">

                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divAnnouncement1">
                    <div id="divAnnouncement1_heading">
                        <asp:Label ID="lblAnnouncementH" runat="server" Font-Bold="true" ForeColor="white">
                            <strong>NEWS FEED, ALERTS, TEST STATISTICS...</strong>
                        </asp:Label>
                    </div> 
                    <div id="divAnnouncement1C" runat="server">
                    </div>
	            </div>
            </td>
            <td>
                <div id="divAnnouncement2">
	                 <div id="divAnnouncement2_heading">
                        <asp:Label ID="lblLatestContentH" runat="server" Font-Bold="true" ForeColor="white">
                            <strong>MESSAGES FROM ADMIN</strong>
                        </asp:Label>
                    </div> 
                     <div id="divAnnouncement2C" runat="server">

                    </div>
	            </div>
            </td>
        </tr>
    </table>
        <p>&nbsp;</p>
    </div>
    <div id="divFooter">
        <uc1:SiteFooter id="SiteFooter" runat="server"></uc1:SiteFooter>
    </div>
        
    </form>
</body>
</html>

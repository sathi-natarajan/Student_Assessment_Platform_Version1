<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TeacherClass.aspx.vb" Inherits="StudentsAssessment.TeacherClass" %>

<%@ Register TagPrefix="uc1" TagName="TeachersPanel" Src="TeachersLoggedInPanel.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteHeader" Src="~/SiteHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteFooter" Src="~/SiteFooter.ascx" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Teachers Interface -Teacher Class</title>
    <link href="~/Styles.css" rel="stylesheet" />
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

        <p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p>

        <table id="tblTeacherClass" border="0" cellpadding="5" cellspacing="0">
            <tr>
                <td valign="top">
                     <div style="height:35px;background-color:mediumpurple;color:white;">
                            <strong>CLASSES TAUGHT BY <br /><asp:Label ID="lblTeacherName" runat="server"></asp:Label></strong>
                     </div>
                    <div id="divListClassesTaught_TeacherClass" >
                        <strong>Select a class to add a student to:<br />
                        </strong>
                        &nbsp;<asp:PlaceHolder ID="ClassesHolder" runat="server"></asp:PlaceHolder>
                    </div>
                     
                </td>
                <td valign="top">
                    <div style="height:35px;background-color:mediumpurple;color:white;">
                        <strong>AVAILABLE STUDENTS</strong>
                    </div>
                    <div id="divListStudents">
                        <strong>Select a student to add to selected class:<br />
                        </strong>
                        <asp:PlaceHolder ID="StudentsHolder" runat="server"></asp:PlaceHolder>
                    </div>
                </td>
                 <td valign="top">
                    <div style="height:45px;background-color:mediumpurple;color:white;">
                        <strong>STUDENTS ALREADY BEING TAUGHT BY <asp:Label ID="lblTeachername2" runat="server"></asp:Label></strong>
                    </div>
                    <div id="divListStudentsAlreadyTaughtBy">
                        <asp:PlaceHolder ID="StudentsHolder1" runat="server"></asp:PlaceHolder>
                    </div>
                </td>
                <td valign="top">
                    <div id="divAdminPanel" runat="server">
                         <div  style="height:35px;background-color:mediumpurple;color:white;">
                        <strong>ADMIN PANEL</strong>
                    </div>
                    <div id="divListOfTeachersinAdminPanel">
                        <strong>Select a teacher for whom to list the classes in the classes list:<br />
                        </strong>
                        <asp:PlaceHolder ID="TeachersHolder" runat="server"></asp:PlaceHolder><br />
                    </div>
                    </div>
                   
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblStatus" runat="server" CSSClass="Statusarea" Font-Bold="true"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tblButtons" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2">
                    <asp:LinkButton ID="lnkReturn" runat="server" Font-bold="true" Font-Underline="false" 
        ForeColor="White" BackColor="darkgreen">RETURN</asp:LinkButton>&nbsp;&nbsp;&nbsp;
                     <asp:LinkButton ID="lnkAddStudents" runat="server" Font-bold="true" Font-Underline="false" 
        ForeColor="White" BackColor="darkgreen">ADD STUDENT TO CLASS</asp:LinkButton>&nbsp;&nbsp;&nbsp;
                  <asp:LinkButton ID="lnkRemoveStudents" runat="server" Font-bold="true" Font-Underline="false" 
        ForeColor="White" BackColor="darkgreen">REMOVE STUDENT FROM CLASS</asp:LinkButton>&nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    <div id="divAdminPanelButton" runat="server">
                         <asp:LinkButton ID="lnkLoadTeachers" runat="server" Font-bold="true" Font-Underline="false" 
                     ForeColor="White" BackColor="darkgreen">&nbsp;&nbsp;&nbsp;LOAD CLASSES TAUGHT BY THIS TEACHER</asp:LinkButton>
                    </div>
                </td>
            </tr>
        </table>   
        <p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p>
    </div>
    <uc1:SiteFooter id="SiteFooter" runat="server"></uc1:SiteFooter>
    </form>
</body>
</html>

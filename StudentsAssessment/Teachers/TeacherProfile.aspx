<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TeacherProfile.aspx.vb" Inherits="StudentsAssessment.TeacherProfile" %>

<%@ Register TagPrefix="uc1" TagName="TeachersPanel" Src="TeachersLoggedInPanel.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteHeader" Src="~/SiteHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteFooter" Src="~/SiteFooter.ascx" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Teachers Interface -Teacher profile</title>
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
        <br /><br /><br />
        <table id="tblProfileButtons" border="0" cellspacing="2" cellpadding="2">
            <tr>
                <td>
                    &nbsp;</td>
                    <td>
                        <asp:LinkButton ID="lnkSaveProfile" runat="server" BackColor="darkgreen" Font-bold="true" Font-Underline="false"
                            ForeColor="White">SAVE PROFILE</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lnkReturn" runat="server" Font-bold="true" Font-Underline="false" 
                        ForeColor="White" BackColor="darkgreen">RETURN</asp:LinkButton>
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblStatus" runat="server" CSSClass="Statusarea" Font-Bold="true"></asp:Label>
                </td>
            </tr>
         </table>
        <div id="PnlTeacherProfile" style="background-color:lightgray;width:625px;margin-left:-50px;">
            <div style="height:25px;background-color:mediumpurple;color:white;width:625px;">
                <strong>TEACHER PROFILE FOR <asp:Label ID="lblTeacherName" runat="server"></asp:Label></strong>
            </div>
            <br />
            <br />
            <table id="tblProfile" cellpadding="3" cellspacing="3" border="0">
                <tr>
                    <td>First name: </td>
                    <td align="left"><asp:textbox ID="txtFirstname" runat="server" MaxLength="25" CssClass="ProfileTextboxes"></asp:textbox></td>
                </tr>
                <tr>
                  <td>Last name: </td>
                    <td align="left"><asp:textbox ID="txtLastname" runat="server" MaxLength="25" CssClass="ProfileTextboxes"></asp:textbox></td>
                </tr>
                <tr>
                  <td>Description:</td>
                    <td align="left">
                       <asp:textbox ID="txtDesc" runat="server" MaxLength="25" 
                        CssClass="ProfileTextboxes" TextMode="Multiline" Rows="5"></asp:textbox>
                    </td>
                </tr>
                <tr>
                  <td>User name: </td>
                  <td align="left"><asp:textbox ID="txtUsername" runat="server" MaxLength="25" CssClass="ProfileTextboxes"></asp:textbox></td>
                </tr>
                <tr>
                  <td>Password: </td>
                  <td align="left"><asp:textbox ID="txtPassword" runat="server" MaxLength="25" TextMode="Password" CssClass="ProfileTextboxes"></asp:textbox></td>
                </tr>
                <tr>
                    <td>Date of join:</td>
                  <td align="left"><asp:textbox ID="txtJoindate" runat="server" MaxLength="25"></asp:textbox></td>
                </tr>
                 <tr>
                    <td>Date of termination (if any): </td>
                    <td align="left"><asp:textbox ID="txtTermdate" runat="server" MaxLength="25"></asp:textbox></td>
                </tr>
                  <tr>
                    <td >Part-time staff?: </td>
                  <td align="left">
                      <asp:RadiobuttonList ID="chkblPT" runat="server">
                          <asp:ListItem Text="Yes" Value="yes"></asp:ListItem>
                          <asp:ListItem Text="No" Value="No"></asp:ListItem>
                      </asp:RadiobuttonList>
                  </td>
                </tr>
                  <tr>
                    <td>Is staff an admin?: </td>
                  <td align="left">
                       <asp:RadiobuttonList ID="chkblAdmin" runat="server">
                          <asp:ListItem Text="Yes" Value="yes"></asp:ListItem>
                          <asp:ListItem Text="No" Value="No"></asp:ListItem>
                      </asp:RadiobuttonList>
                  </td>
                </tr>
                 <tr>
                    <td>Is staff ACTIVE?: </td>
                  <td align="left">
                       <asp:RadiobuttonList ID="chkblActive" runat="server">
                          <asp:ListItem Text="Yes" Value="yes"></asp:ListItem>
                          <asp:ListItem Text="No" Value="No"></asp:ListItem>
                      </asp:RadiobuttonList>
                  </td>
                </tr>
                <tr>
                     <td colspan="3">
                        <div style="height:25px;background-color:mediumpurple;color:white;width:600px;">
                            <strong>CLASSES TAUGHT BY <asp:Label ID="lblTeachername1" runat="server"></asp:Label></strong>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="divListClassesTaught" >
                            &nbsp;<asp:PlaceHolder ID="ClassesHolder" runat="server"></asp:PlaceHolder>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div style="height:25px;background-color:mediumpurple;color:white;width:600px;">
                            <strong>SUBJECTS TAUGHT BY <asp:Label ID="lblTeachername2" runat="server"></asp:Label></strong>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="divListSubjectsTaught">
                            <asp:PlaceHolder ID="SubjectsHolder1" runat="server"></asp:PlaceHolder>
                        </div>
                    </td>
                </tr>
                 <tr>
                    <td colspan="3">
                        <div style="height:25px;background-color:mediumpurple;color:white;width:600px;">
                            <strong>STUDENTS TAUGHT BY <asp:Label ID="lblTeachername3" runat="server"></asp:Label></strong>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="divListStudentsTaught">
                            <asp:PlaceHolder ID="StudentsHolder1" runat="server"></asp:PlaceHolder>
                        </div>
                    </td>
                </tr>
            </table>
        <!--This panel is visible only to administrators-->
        <asp:Panel ID="pnlAdminProfile" CSSClass="pnlAdminProfile" runat="server">
            <div id="LoadTeacherProfileButton">
                <asp:LinkButton ID="lnkLoadTeacherProfile" runat="server" BackColor="darkgreen" Font-bold="true" Font-Underline="false"
                             ForeColor="White">LOAD THIS TEACHER PROFILE</asp:LinkButton><br />
                </div>
            <div style="height:25px;background-color:mediumpurple;text-align:center;border:solid 2px white">
             <asp:Label ID="lblWelcome" runat="server" Font-Bold="true" ForeColor="white">
                <strong>ADMIN PANEL</strong>
            </asp:Label>
            <div id="divTeacherProfileHolder">
                    <asp:PlaceHolder ID="TeachersHolder" runat="server"></asp:PlaceHolder><br />
            </div>
            </div>
            
        </asp:Panel>
       </div>
        <br />
    </div>
        <p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p>
        <uc1:SiteFooter id="SiteFooter" runat="server"></uc1:SiteFooter>
    </form>
</body>
</html>

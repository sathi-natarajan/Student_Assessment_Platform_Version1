<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MainLinksControl.ascx.vb" Inherits="StudentsAssessment.MainLinksControl" %>

        <div style="height:25px;background-color:mediumpurple;text-align:center;border:solid 2px white">
             <asp:Label ID="lblWelcome" runat="server" Font-Bold="true" ForeColor="white">
                <strong>WELCOME SCREEN</strong>
            </asp:Label>
        </div>
        <br />
        <br />
		<div style="height:25px;background-color:lightblue;
		text-align:center;
        border-top:solid 3px white;border-bottom:solid 3px white;">
                 <asp:Linkbutton ID="lnkTakeTest" Font-Underline="false" Font-Bold="true" ForeColor="white" runat="server"><strong>TAKE TEST</strong></asp:LinkButton>
        </div>
            <br />
            <br />
			
		<div style="height:25px;background-color:#ffd800;
		text-align:center;border-top:solid 3px white;border-bottom:solid 3px white;">
            <asp:Linkbutton ID="lnkteachersLogin" runat="server" Font-Underline="false" Font-Bold="true" ForeColor="white"><strong>TEACHER LOG-IN</strong></asp:LinkButton>
        </div>
            <br />
            <br />
			
		<div style="height:25px;background-color:green;
		text-align:center;border-top:solid 3px white;border-bottom:solid 3px white;">
                <asp:Linkbutton ID="Linkbutton1" Font-Underline="false" Font-Bold="true" ForeColor="white" runat="server"><strong>STUDENT LOG-IN</strong></asp:LinkButton>
        </div>
        <br />
        <br />
		


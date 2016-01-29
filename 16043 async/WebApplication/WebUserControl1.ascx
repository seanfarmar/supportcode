<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControl1.ascx.cs" Inherits="WebApplication.WebUserControl1" %>
    <div>
        <h1>Send via user control...</h1>
        Enter a number below and click "Go".<br />
        If the number is even, the result will be "Fail", otherwise it will be "None".
        <br /><br />
        
        <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Go" />
        <asp:Label ID="Label1" runat="server"></asp:Label>
    </div>
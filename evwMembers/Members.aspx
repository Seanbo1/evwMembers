<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Members.aspx.cs" Inherits="evwMembers.Members" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Experts Directory</title>
    <link rel="stylesheet" runat="server" href="~/evwStyles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Image ID="ImgLogo" runat="server" ImageUrl="~/logo.png" Width="300px" />
        </div>
        <div style="padding-bottom:20px;">
             <asp:Label ID="lblPageTitle" runat="server" Text="Experts Directory"></asp:Label>
        </div>

        <div>
            <asp:Button ID="btnAddMember" CSSClass="button" runat="server" Text="Add Expert" OnClick="btnAddMember_Click" />
        </div>

        <div>
            <asp:Literal ID="litMembers" runat="server"></asp:Literal>
        </div>
        
    </form>
</body>
</html>

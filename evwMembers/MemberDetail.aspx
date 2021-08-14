<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberDetail.aspx.cs" Inherits="evwMembers.MemberDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Expert Profile</title>
    <link rel="stylesheet" runat="server" href="~/evwStyles.css" />

    <script type="text/javascript">
       function goBack()
        {
          window.history.back()
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">

        <div>
            <asp:Image ID="ImgLogo" runat="server" ImageUrl="~/logo.png" Width="300px" />
        </div>

        <div style="padding-bottom:20px;">
             <asp:Label ID="lblPageTitle" runat="server" Text="Expert Profile"></asp:Label>
        </div>

        <div>
            <asp:Literal ID="litProfile" runat="server"></asp:Literal>
        </div>
        

        <div>
            <asp:Literal ID="litFriends" runat="server"></asp:Literal>
        </div>

        <div style="padding-bottom:20px;">
            <asp:Button ID="btnAddFriend" CSSClass="button" runat="server" Text="Add Friend" OnClick="btnAddFriend_Click" />
        </div>

        <div>
            <asp:Label ID="lblSearch" runat="server" Text="Search:"></asp:Label>
            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
            <asp:Button ID="btnSearch" CSSClass="button" runat="server" Text="Search" OnClick="btnSearch_Click" />
        </div>


        <div style="padding-top:20px;">
            <input type="button" value="Back" onclick="goBack()" />
        </div>
        <div style="padding-top:20px;">
             <asp:Button ID="btnDirectory" CSSClass="button" runat="server" Text="Directory" OnClick="btnDirectory_Click" />
        </div>
    </form>
</body>
</html>

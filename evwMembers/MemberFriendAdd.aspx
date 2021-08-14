<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberFriendAdd.aspx.cs" Inherits="evwMembers.MemberFriendAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add A Friend</title>
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
             <asp:Label ID="lblPageTitle" runat="server" Text="Add A Friend"></asp:Label>
        </div>

        <!-- Render a literal here containg a list of all the members who are not already friends -->
        <div>
            <asp:Literal ID="litFriendsToAdd" runat="server"></asp:Literal>
        </div>

         <div style="padding-top:20px;">
            <input type="button" value="Back" onclick="goBack()" />
        </div>
    </form>
</body>
</html>

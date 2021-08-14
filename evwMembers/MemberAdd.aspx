<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberAdd.aspx.cs" Inherits="evwMembers.MemberAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add an Expert</title>
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
             <asp:Label ID="lblPageTitle" runat="server" Text="Add An Expert"></asp:Label>
        </div>

        <div>
            <table width="50%">
                <tr>
                    <td><asp:Label ID="lblName" runat="server" Text="Full Name:"></asp:Label></td>
                    <td width="100%"><asp:TextBox ID="txtName" runat="server" Width="100%"></asp:TextBox></td>
                </tr>
            
                <tr>
                 <td><asp:Label ID="lblURL" runat="server" Text="Website:"></asp:Label></td>
                 <td width="100%"><asp:TextBox ID="txtURL" runat="server"  Width="100%"></asp:TextBox></td>
                </tr>
            </table>
        </div>

        <div style="padding-top:20px;">
            <asp:Button ID="btnAddMember" CSSClass="button" runat="server" Text="Save" OnClick="btnAddMember_Click" />
            <input type="button" value="Back" onclick="goBack()" />
        </div>

    </form>
</body>
</html>

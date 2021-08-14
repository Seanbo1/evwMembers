<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="evwMembers.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Oh No!</title>

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

        <div>
            <asp:Label ID="lblError" runat="server" Text="The page you requested cannot be displayed."></asp:Label>
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

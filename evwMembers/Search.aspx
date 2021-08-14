<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="evwMembers.Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search Results</title>

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
            <asp:Label ID="lblTitle" runat="server" Text="Search Results"></asp:Label>
        </div>

        <div>
            <asp:Literal ID="litSearchResults" runat="server"></asp:Literal>
        </div>

        <div style="padding-top:20px;">
            <input type="button" value="Back" onclick="goBack()" />
        </div>
    </form>
</body>
</html>

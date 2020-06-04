<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="WebApplication1.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TreeView ID="TreeView1" ExpandDepth="0" PopulateNodesFromClient="true" ShowLines="true" ShowExpandCollapse="true" runat="server"  OnTreeNodePopulate="TreeView1_TreeNodePopulate" Target="_Blank">
            </asp:TreeView>
            <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl=http://www.slide.com Target="_blank"></asp:HyperLink>--%>
        </div>
    </form>
</body>
</html>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<VideoStore.WebClient.ViewModels.ListReviewsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ListReviews
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Listing Reviews for <%: Model.Media.Title %>:</h2>
    <table>
    <tr>
            <th>Review Title: </th>
            <th>Comments: </th>
        </tr>
    <% foreach (var item in Model.Media.Review) { %>
        
        <tr>
        <td>
        
        <%: item.Title %>
        </td>
        <td>
        <%: item.Comments %>
        
        </td>
        </tr>

    <% } %>
    </table>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<VideoStore.WebClient.ViewModels.CatalogueViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ListMedia
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ListMedia</h2>

    <table>
        <tr>
            <th></th>
        </tr>

    <% foreach (var item in Model.MediaListPage) { %>
    
        <tr>
            <td>
                <%: item.Title %>
                Price: $<%: item.Price %>

                Stock Count: (<%: item.Stocks.Quantity %>)


                <% using(Html.BeginForm("AddToCart", "Cart")) { %>
                    <%= Html.Hidden("pMediaId", item.Id) %>
                    <%= Html.Hidden("pReturnUrl", ViewContext.HttpContext.Request.Url.PathAndQuery) %>
                    
                    <input type="submit" value="+ Add to Cart" />
                <%} %>

                 <% using(Html.BeginForm("ListReviews", "Reviews")) { %>
                    <%= Html.Hidden("pMediaId", item.Id) %>
                  
                    
                    <input type="submit" value="List Reviews" />
                <%} %>
            </td>
        </tr>
    
    <% } %>

    </table>

</asp:Content>


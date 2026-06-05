<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/MasterPages/Site.master"
    CodeBehind="Regions.aspx.cs"
    Inherits="AdventureHorizonsBD.Regions" %>

<asp:Content ID="TitleContent1"
    ContentPlaceHolderID="TitleContent"
    runat="server">

    Regions - Adventure Horizons BD

</asp:Content>

<asp:Content ID="MainContent1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <main>

        <section class="section" style="padding-top: 5rem;">

            <div class="container">

                <p class="eyebrow">
                    Explore Bangladesh
                </p>

                <h2>
                    Adventure Regions Across the Country
                </h2>

                <div class="grid-regions">
                    <asp:Repeater ID="rptRegions" runat="server">
                        <ItemTemplate>
                            <article class="region-card">
                                <h3><%# Eval("RegionName") %></h3>
                                <p><strong>Highlights:</strong> <%# Eval("Highlights") %></p>
                                <p><%# Eval("Description") %></p>
                                <p><strong>Popular Trips:</strong> <%# Eval("PopularTrips") %></p>
                            </article>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

            </div>

        </section>

    </main>

</asp:Content>
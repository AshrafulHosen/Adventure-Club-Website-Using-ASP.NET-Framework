<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/MasterPages/Site.master"
    CodeBehind="Gallery.aspx.cs"
    Inherits="AdventureHorizonsBD.Gallery" %>

<asp:Content ID="TitleContent1"
    ContentPlaceHolderID="TitleContent"
    runat="server">

    Gallery - Adventure Horizons BD

</asp:Content>

<asp:Content ID="MainContent1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <main>

        <section class="section" style="padding-top: 5rem;">

            <div class="container">

                <p class="eyebrow">
                    Past Expeditions
                </p>

                <h2>
                    Photo Gallery
                </h2>
            <div class="gallery-grid" id="galleryGrid">
                <asp:Repeater ID="rptGallery" runat="server">
                    <ItemTemplate>
                        <figure class="gallery-item">
                            <img src='<%# Eval("ImageURL") %>' alt='<%# Eval("Title") %>' />
                            <figcaption>
                                <%# Eval("Title") %>
                            </figcaption>
                        </figure>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

        </section>

    </main>

    <!-- Image Modal -->

    <div id="imageModal" class="modal">

        <span class="close">&times;</span>

        <img class="modal-content"
             id="modalImage" />

        <div id="caption"></div>

    </div>

</asp:Content>
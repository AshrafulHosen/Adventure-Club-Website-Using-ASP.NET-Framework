<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="Contact.aspx.cs"
    Inherits="AdventureHorizonsBD.Contact" %>

    <asp:Content ID="TitleContent1" ContentPlaceHolderID="TitleContent" runat="server">

        Contact - Adventure Horizons BD

    </asp:Content>

    <asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">

        <main>

            <section class="section" style="padding-top:5rem;">

                <div class="container">

                    <p class="eyebrow">
                        Get In Touch
                    </p>

                    <h2>
                        Contact Us
                    </h2>

                    <p style="
                    color:var(--muted);
                    margin-bottom:2rem;
                    max-width:600px;">

                        Have questions about our expeditions or want to join
                        Adventure Horizons BD? Fill out the form below or
                        reach out directly using the contact information provided.

                    </p>

                    <asp:Panel ID="pnlContactForm" runat="server" CssClass="contact-form" DefaultButton="btnSendMsg">
                    <div class="form-row">
                        <div class="form-group">
                            <label>Name</label>
                            <asp:TextBox ID="txtContactName" runat="server" placeholder="Your name" required="required"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Email</label>
                            <asp:TextBox ID="txtContactEmail" runat="server" TextMode="Email" placeholder="your@email.com" required="required"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label>Subject</label>
                        <asp:TextBox ID="txtContactSubject" runat="server" placeholder="How can we help you?" required="required"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Message</label>
                        <asp:TextBox ID="txtContactMessage" runat="server" TextMode="MultiLine" Rows="5" placeholder="Your message here..." required="required"></asp:TextBox>
                    </div>
                    <asp:Label ID="lblContactMessage" runat="server" Font-Bold="true"></asp:Label>
                    <asp:Button ID="btnSendMsg" runat="server" Text="Send Message" CssClass="btn btn-primary" OnClick="btnSendMsg_Click" />
                </asp:Panel>

                </div>

            </section>

        </main>

    </asp:Content>
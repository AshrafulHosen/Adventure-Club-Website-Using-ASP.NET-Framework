<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="Membership.aspx.cs"
    Inherits="AdventureHorizonsBD.Membership" %>

    <asp:Content ID="TitleContent1" ContentPlaceHolderID="TitleContent" runat="server">

        Membership - Adventure Horizons BD

    </asp:Content>

    <asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">

        <main>

            <section class="section section-muted" style="padding-top: 5rem;">

                <div class="container split-layout">

                    <div>

                        <p class="eyebrow">
                            Membership
                        </p>

                        <h2>
                            Become Part of the Expedition
                        </h2>

                        <p>
                            Whether you're a complete beginner or an experienced
                            explorer, our plans are designed to help you learn,
                            grow, and enjoy the waterways and coast.
                        </p>

                        <ul class="benefits">

                            <li>Access to monthly guided trips and workshops</li>
                            <li>Priority registration for limited-seat events</li>
                            <li>Safety and outdoor skills training</li>
                            <li>Community meetups and volunteer opportunities</li>
                            <li>Exclusive member discounts on gear and equipment</li>
                            <li>Newsletter with expedition insights and travel tips</li>

                        </ul>

                        <p style="margin-top:1.5rem;">

                            Joining Adventure Horizons BD means becoming
                            part of a vibrant community of like-minded
                            adventurers. Benefits include access to
                            exclusive expeditions, expert guidance,
                            and lifelong friendships forged on the trail.

                        </p>

                    </div>

                    <article class="pricing-card" aria-label="Membership plans">

                        <h3>
                            Membership Plans
                        </h3>

                        <p>
                            <strong>Student:</strong><br />
                            500 BDT / month<br />
                            <small>Perfect for students on a budget</small>
                        </p>

                        <p>
                            <strong>Regular:</strong><br />
                            800 BDT / month<br />
                            <small>Full access to all trips and workshops</small>
                        </p>

                        <p>
                            <strong>Family:</strong><br />
                            1500 BDT / month<br />
                            <small>Up to 4 family members</small>
                        </p>

                        <button type="button" class="btn btn-primary" onclick="openMembershipForm()">

                            Apply Now

                        </button>

                    </article>

                </div>

            </section>

            <!-- Membership Form -->

            <section class="section" style="background: linear-gradient(
            135deg,
            rgba(255,69,0,.05),
            rgba(255,179,0,.05));">

                <div class="container">

                    <p class="eyebrow" style="text-align:center;">

                        Ready to Join?

                    </p>

                    <h2 style="text-align:center;">
                        Apply for Membership
                    </h2>

                    <p style="
                    text-align:center;
                    color:var(--muted);
                    margin-bottom:2rem;">

                        Fill out the form below to apply for membership.
                        Our admin team will review your application.

                    </p>

                    <div class="membership-form-container">

                        <asp:Panel ID="pnlMembershipForm" runat="server" DefaultButton="btnSubmitApp">

                            <div class="form-row">
                                <div class="form-group">
                                    <label for="txtAppName">Full Name *</label>
                                    <asp:TextBox ID="txtAppName" runat="server" placeholder="Your full name" required="required"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="txtAppEmail">Email Address *</label>
                                    <asp:TextBox ID="txtAppEmail" runat="server" TextMode="Email" placeholder="your@email.com" required="required"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="form-group">
                                    <label for="txtAppPhone">Phone Number *</label>
                                    <asp:TextBox ID="txtAppPhone" runat="server" TextMode="Phone" placeholder="+880 1XXX-XXXXXX" required="required"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="txtAppPassword">Password *</label>
                                    <asp:TextBox ID="txtAppPassword" runat="server" TextMode="Password" placeholder="Enter your password (min 8 chars)" required="required"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-row">
                                <div class="form-group">
                                    <label for="ddlAppPlan">Select Plan *</label>
                                    <asp:DropDownList ID="ddlAppPlan" runat="server" required="required">
                                        <asp:ListItem Value="" Text="Choose a membership plan"></asp:ListItem>
                                        <asp:ListItem Value="Student" Text="Student - 500 BDT/month"></asp:ListItem>
                                        <asp:ListItem Value="Regular" Text="Regular - 800 BDT/month"></asp:ListItem>
                                        <asp:ListItem Value="Family" Text="Family - 1500 BDT/month"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="ddlAppExperience">Your Adventure Experience *</label>
                                <asp:DropDownList ID="ddlAppExperience" runat="server" required="required">
                                    <asp:ListItem Value="" Text="Select your experience level"></asp:ListItem>
                                    <asp:ListItem Value="Beginner" Text="Beginner - First time adventuring"></asp:ListItem>
                                    <asp:ListItem Value="Intermediate" Text="Intermediate - Some experience"></asp:ListItem>
                                    <asp:ListItem Value="Advanced" Text="Advanced - Experienced adventurer"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label for="txtAppMessage">Tell us about yourself (optional)</label>
                                <asp:TextBox ID="txtAppMessage" runat="server" TextMode="MultiLine" Rows="4" placeholder="What adventures are you excited about? Any specific interests?"></asp:TextBox>
                            </div>

                            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="true" style="display:block; margin-bottom:1rem; text-align:center;"></asp:Label>
                            
                            <asp:Button ID="btnSubmitApp" runat="server" Text="Submit Application" CssClass="btn btn-primary" style="width:100%;" OnClick="btnSubmitApp_Click" />

                            <p style="text-align:center; color:var(--muted); margin-top:1rem; font-size:.9rem;">
                                ✓ You will receive an email once your membership is approved
                            </p>

                        </asp:Panel>

                    </div>

                </div>

            </section>

        </main>

    </asp:Content>
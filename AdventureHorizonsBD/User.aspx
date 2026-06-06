<%@ Page Language="C#" MasterPageFile="~/MasterPages/Site.master" AutoEventWireup="true" CodeBehind="User.aspx.cs"
    Inherits="AdventureHorizonsBD.User" EnableEventValidation="false" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">

        User Dashboard - Adventure Horizons BD

    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <main class="user-container">

            <!-- User Login Form -->
            <asp:Panel ID="pnlUserLoginSection" runat="server">
                <section id="userLoginSection" class="user-section login-section">

                    <div class="login-card">

                        <h2>Member Login</h2>

                        <asp:Panel ID="pnlUserLoginForm" runat="server" DefaultButton="btnLogin">

                            <div class="form-group">
                                <label for="txtUserEmail">Email Address:</label>
                                <asp:TextBox ID="txtUserEmail" runat="server" TextMode="Email" placeholder="your@email.com"
                                    required="required" ClientIDMode="Static"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label for="txtUserPassword">Password:</label>
                                <asp:TextBox ID="txtUserPassword" runat="server" TextMode="Password"
                                    placeholder="Enter your password" required="required" ClientIDMode="Static">
                                </asp:TextBox>
                            </div>

                            <asp:Label ID="lblLoginMessage" runat="server" ForeColor="Red" Font-Bold="true"
                                style="display:block; margin-bottom:1rem;"></asp:Label>

                            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary"
                                OnClick="btnLogin_Click" />

                            <p class="login-hint">
                                Not a member yet? <br />
                                <a href="Membership.aspx" class="btn btn-primary"
                                    style="margin-top: 0.5rem; display: inline-block;">Become a member</a>
                            </p>

                        </asp:Panel>

                    </div>

                </section>
            </asp:Panel>

            <!-- User Dashboard -->
            <asp:Panel ID="pnlUserDashboard" runat="server" CssClass="user-dashboard">

                <div class="user-nav">

                    <div class="container">

                        <div class="header-content">

                            <div>

                                <h2>
                                    Welcome,
                                    <span>
                                        <asp:Literal ID="litMemberName" runat="server"></asp:Literal>
                                    </span>
                                    👋
                                </h2>

                                <p class="member-info">
                                    <asp:Literal ID="litMemberStatus" runat="server"></asp:Literal>
                                </p>

                            </div>

                        </div>

                        <div class="user-tabs">

                            <button type="button" class="user-tab active" onclick="switchUserTab('gallery')">

                                📷 Upload Photos

                            </button>

                            <button type="button" class="user-tab" onclick="switchUserTab('review')">

                                ⭐ Submit Review

                            </button>

                            <button type="button" class="user-tab" onclick="switchUserTab('myUploads')">

                                📁 My Uploads

                            </button>

                            <button type="button" class="user-tab" onclick="switchUserTab('bookings')">

                                🎫 My Bookings

                            </button>

                            <asp:Button ID="btnLogout" runat="server" Text="🚪 Logout" CssClass="user-tab"
                                OnClick="btnLogout_Click" CausesValidation="false" UseSubmitBehavior="false" />

                        </div>

                    </div>

                </div>

                <!-- Gallery Upload Tab -->

                <section id="galleryTab" class="user-tab-content active">

                    <div class="container">

                        <h3>
                            Upload Your Adventure Photos
                        </h3>

                        <p class="section-subtitle">
                            Share your best moments from our expeditions
                        </p>

                        <div class="user-panel">

                            <div class="user-form-section">

                                <asp:Panel ID="pnlUserGalleryForm" runat="server">

                                    <div class="form-group">
                                        <label>Photo Title:</label>
                                        <asp:TextBox ID="txtUserPhotoTitle" runat="server" required="required">
                                        </asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Upload Image:</label>
                                        <div class="upload-method-content active">
                                            <asp:FileUpload ID="fileUserPhoto" runat="server" accept="image/*" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label>Description:</label>
                                        <asp:TextBox ID="txtUserPhotoDescription" runat="server" TextMode="MultiLine"
                                            Rows="3"></asp:TextBox>
                                    </div>

                                    <asp:Label ID="lblGalleryMsg" runat="server" ForeColor="Green" Font-Bold="true">
                                    </asp:Label>

                                    <asp:Button ID="btnUserUploadPhoto" runat="server" Text="Upload Photo"
                                        CssClass="btn btn-primary" OnClick="btnUserUploadPhoto_Click"
                                        UseSubmitBehavior="true" />

                                </asp:Panel>

                            </div>

                        </div>

                    </div>

                </section>

                <!-- Review Tab -->

                <section id="reviewTab" class="user-tab-content">

                    <div class="container">

                        <h3>Share Your Experience</h3>

                        <asp:Panel ID="pnlUserReviewForm" runat="server" CssClass="user-form-section" style="max-width:600px;">

                            <div class="form-group">
                                <label>Event Name:</label>
                                <asp:TextBox ID="txtReviewEvent" runat="server" placeholder="Event Name"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label>Review Title:</label>
                                <asp:TextBox ID="txtReviewTitle" runat="server" placeholder="Review Title"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label>Your Review:</label>
                                <asp:TextBox ID="txtReviewText" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
                            </div>

                            <asp:HiddenField ID="hdnReviewRating" runat="server" Value="5" ClientIDMode="Static" />
                            
                            <div class="form-group">
                                <label>Rating:</label>
                                <div class="rating-input">
                                    <div class="stars">
                                        <span class="star active" onclick="setRating(1)">★</span>
                                        <span class="star active" onclick="setRating(2)">★</span>
                                        <span class="star active" onclick="setRating(3)">★</span>
                                        <span class="star active" onclick="setRating(4)">★</span>
                                        <span class="star active" onclick="setRating(5)">★</span>
                                    </div>
                                    <span id="ratingDisplay" class="rating-text">5 Stars</span>
                                </div>
                            </div>
                            
                            <asp:Label ID="lblReviewMsg" runat="server" Font-Bold="true" style="display:block; margin-bottom:1rem;"></asp:Label>

                            <asp:Button ID="btnSubmitReview" runat="server" Text="Submit Review" CssClass="btn btn-primary" OnClick="btnSubmitReview_Click" />

                        </asp:Panel>

                    </div>

                </section>

                <!-- My Uploads -->

                <section id="myUploadsTab" class="user-tab-content">

                    <div class="container">

                        <h3>My Contributions</h3>

                        <div class="user-panel-full">

                            <div class="uploads-section">

                                <h4>
                                    📷 My Uploaded Photos
                                </h4>

                                <div class="user-items-list">
                                    <asp:Repeater ID="rptUserPhotos" runat="server">
                                        <HeaderTemplate>
                                            <div
                                                style="display: grid; grid-template-columns: repeat(auto-fill, minmax(200px, 1fr)); gap: 1rem;">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="user-item">
                                                <h4>
                                                    <%# Eval("Title") %>
                                                </h4>
                                                <p class="user-item-meta">Uploaded: <%#
                                                        Eval("UploadDate", "{0:MMM dd, yyyy}" ) %>
                                                </p>
                                                <p class="user-item-meta">Status: <%#
                                                        Convert.ToBoolean(Eval("IsApproved"))
                                                        ? "<span style='color:green;'>Approved</span>"
                                                        : "<span style='color:orange;'>Pending</span>" %>
                                                </p>
                                                <img src='<%# Eval("ImageURL") %>' alt='<%# Eval("Title") %>'
                                                    class="user-item-preview" style="max-width:100%; height:auto;" />
                                            </div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                </div>
                                </FooterTemplate>
                                </asp:Repeater>
                                <asp:Label ID="lblNoPhotos" runat="server" CssClass="no-items"
                                    Text="No photos uploaded yet" Visible="false"></asp:Label>
                            </div>

                        </div>

                        <div class="uploads-section">

                            <h4>
                                ⭐ My Reviews
                            </h4>

                            <div id="userReviewsList" class="user-items-list">
                                <asp:Repeater ID="rptUserReviews" runat="server">
                                    <ItemTemplate>
                                        <article class="testimonial-card">
                                          <div class="stars"><%# new string('★', Convert.ToInt32(Eval("Rating"))) %><%# new string('☆', 5 - Convert.ToInt32(Eval("Rating"))) %></div>
                                          <p style="margin: 1rem 0 0.5rem; font-weight: 600; font-size: 1.1rem;">"<%# Eval("Title") %>"</p>
                                          <p style="color: var(--muted); font-size: 0.9rem; margin: 0.5rem 0;">📍 <%# Eval("EventName") %></p>
                                          <p style="margin: 1rem 0; line-height: 1.6;"><%# Eval("ReviewText") %></p>
                                          <p style="color: var(--muted); font-size: 0.8rem; margin: 0.5rem 0;">Status: <%# Convert.ToBoolean(Eval("IsApproved")) ? "<span style='color:green;'>Approved</span>" : "<span style='color:orange;'>Pending</span>" %></p>
                                        </article>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Label ID="lblNoReviews" runat="server" CssClass="no-items" Text="No reviews submitted yet" Visible="false"></asp:Label>
                            </div>

                        </div>

                    </div>

                </section>

                <!-- My Bookings -->

                <section id="bookingsTab" class="user-tab-content">

                    <div class="container">

                        <h3>My Trip Bookings</h3>

                        <div class="user-items-list">
                            <asp:Repeater ID="rptUserBookings" runat="server">
                                <ItemTemplate>
                                    <div class="user-item">
                                        <div class="user-item-header">
                                            <div>
                                                <h4>
                                                    🎫 <%# Eval("EventTitle") %>
                                                </h4>
                                                <p class="user-item-meta">📅 Date: <%# Eval("EventDate") %>
                                                </p>
                                                <p class="user-item-meta">Booked On: <%#
                                                        Eval("RegistrationDate", "{0:MMM dd, yyyy}" ) %>
                                                </p>
                                            </div>
                                            <span
                                                style='padding: 0.25rem 0.75rem; border-radius: 4px; font-weight: bold; font-size: 0.85rem; <%# Eval("Status").ToString() == "Approved" ? "background-color: rgba(0, 128, 0, 0.1); color: green;" : (Eval("Status").ToString() == "Pending" ? "background-color: rgba(255, 165, 0, 0.1); color: orange;" : "background-color: rgba(255, 0, 0, 0.1); color: red;") %>'>
                                                <%# Eval("Status") %>
                                            </span>
                                        </div>
                                        <p class="user-item-description">Participants: <%# Eval("NumberOfParticipants")
                                                %>
                                        </p>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Label ID="lblNoBookings" runat="server" CssClass="no-items" Visible="false">
                                No bookings yet. <a href="Events.aspx">Browse and book a trip</a>
                            </asp:Label>
                        </div>

                    </div>

                </section>

            </asp:Panel>

        </main>

        <asp:HiddenField ID="hdnActiveUserTab" runat="server" Value="gallery" ClientIDMode="Static" />

        <script src="JS/user.js"></script>

    </asp:Content>

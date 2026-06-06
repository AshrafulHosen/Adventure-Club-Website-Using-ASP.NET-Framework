<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="Admin.aspx.cs"
    Inherits="AdventureHorizonsBD.Admin" EnableEventValidation="false" %>

    <asp:Content ID="TitleContent1" ContentPlaceHolderID="TitleContent" runat="server">

        Admin Panel - Adventure Horizons BD

    </asp:Content>

    <asp:Content ID="MainContent1" ContentPlaceHolderID="MainContent" runat="server">

        <main class="admin-container">

            <!-- Login Form -->
            <asp:Panel ID="pnlAdminLoginSection" runat="server">
                <section id="loginSection" class="admin-section login-section">

                    <div class="login-card">

                        <h2>Admin Login</h2>

                        <asp:Panel ID="pnlAdminLoginForm" runat="server" DefaultButton="btnAdminLogin">

                            <div class="form-group">
                                <label for="txtAdminEmail">Email Address:</label>
                                <asp:TextBox ID="txtAdminEmail" runat="server" TextMode="Email" placeholder="admin@test.com"
                                    required="required" ClientIDMode="Static"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label for="txtAdminPassword">Password:</label>
                                <asp:TextBox ID="txtAdminPassword" runat="server" TextMode="Password"
                                    placeholder="Enter your password" required="required" ClientIDMode="Static">
                                </asp:TextBox>
                            </div>

                            <asp:Label ID="lblAdminLoginMsg" runat="server" ForeColor="Red" Font-Bold="true"
                                style="display:block; margin-bottom:1rem;"></asp:Label>

                            <asp:Button ID="btnAdminLogin" runat="server" Text="Login to Dashboard"
                                CssClass="btn btn-primary" OnClick="btnAdminLogin_Click" />

                            <p class="login-hint">Demo Login: admin@test.com / admin123</p>

                        </asp:Panel>

                    </div>

                </section>
            </asp:Panel>

            <!-- Admin Dashboard -->
            <asp:Panel ID="pnlAdminDashboard" runat="server" CssClass="admin-dashboard">

                <!-- Navigation Tabs -->
                <div class="admin-nav">
                    <div class="container">
                        <ul class="admin-tabs">
                            <li class="admin-nav-item active" onclick="switchAdminTab('overview')">
                                📊 Overview
                            </li>
                            <li class="admin-nav-item" onclick="switchAdminTab('members')">
                                👥 Members
                            </li>
                            <li class="admin-nav-item" onclick="switchAdminTab('events')">
                                📅 Events
                            </li>
                            <li class="admin-nav-item" onclick="switchAdminTab('gallery')">
                                📷 Gallery
                            </li>
                            <li class="admin-nav-item" onclick="switchAdminTab('messages')">
                                ✉️ Messages
                            </li>
                            <li class="admin-nav-item" onclick="switchAdminTab('bookings')">
                                🎫 Bookings
                            </li>
                            <li class="admin-nav-item" onclick="switchAdminTab('reviews')">
                                ⭐ Reviews
                            </li>
                            <li class="admin-nav-item">
                                <asp:LinkButton ID="btnAdminLogout" runat="server" Text="🚪 Logout"
                                    OnClick="btnAdminLogout_Click" CausesValidation="false"
                                    style="color:var(--text); text-decoration:none;" />
                            </li>
                        </ul>
                    </div>
                </div>

                <!-- Overview Tab -->
                <section id="overview" class="admin-tab-content active">
                    <div class="container">
                        <h2>Dashboard Overview</h2>
                        <div class="stats-grid">
                            <div class="stat-card">
                                <div class="stat-icon">👥</div>
                                <div class="stat-value">
                                    <asp:Literal ID="litStatMembers" runat="server" Text="0"></asp:Literal>
                                </div>
                                <h3>Total Members</h3>
                            </div>

                            <div class="stat-card">
                                <div class="stat-icon">⏳</div>
                                <div class="stat-value">
                                    <asp:Literal ID="litStatPending" runat="server" Text="0"></asp:Literal>
                                </div>
                                <h3>Pending Requests</h3>
                            </div>

                            <div class="stat-card">
                                <div class="stat-icon">📅</div>
                                <div class="stat-value">
                                    <asp:Literal ID="litStatEvents" runat="server" Text="0"></asp:Literal>
                                </div>
                                <h3>Total Events</h3>
                            </div>

                            <div class="stat-card">
                                <div class="stat-icon">✉️</div>
                                <div class="stat-value">
                                    <asp:Literal ID="litStatMessages" runat="server" Text="0"></asp:Literal>
                                </div>
                                <h3>Unread Messages</h3>
                            </div>
                        </div>
                    </div>
                </section>

                <!-- Members Tab -->
                <section id="members" class="admin-tab-content">
                    <div class="container">
                        <h2>Membership Approvals</h2>
                        <p class="admin-info-note">ℹ️ Members apply through the Membership page.</p>
                        <div class="admin-panel">
                            <div class="admin-list-section">

                                <h3>Pending Membership Requests</h3>
                                <div class="admin-table-container">
                                    <table class="admin-table">
                                        <thead>
                                            <tr>
                                                <th>Name</th>
                                                <th>Email</th>
                                                <th>Plan</th>
                                                <th>Status</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptMembershipRequests" runat="server"
                                                OnItemCommand="rptMembershipRequests_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <%# Eval("FullName") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("Email") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("MembershipType") %>
                                                        </td>
                                                        <td><span class="status-badge status-pending">Pending</span></td>
                                                        <td>
                                                            <asp:Button ID="btnApprove" runat="server" CommandName="Approve"
                                                                CommandArgument='<%# Eval("RequestID") %>' Text="✓ Approve"
                                                                CssClass="btn btn-success btn-sm" />
                                                            <asp:Button ID="btnReject" runat="server" CommandName="Reject"
                                                                CommandArgument='<%# Eval("RequestID") %>' Text="✗ Reject"
                                                                CssClass="btn btn-danger btn-sm" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <tr id="rowNoRequests" runat="server" visible="false">
                                                <td colspan="5" style="text-align:center;">No pending requests.</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>

                            </div>

                            <div class="admin-list-section">

                                <h3>Approved Members</h3>
                                <div class="admin-table-container">
                                    <table class="admin-table">
                                        <thead>
                                            <tr>
                                                <th>Name</th>
                                                <th>Email</th>
                                                <th>Plan</th>
                                                <th>Joined</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptApprovedMembers" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Eval("FullName") %></td>
                                                        <td><%# Eval("Email") %></td>
                                                        <td><%# Eval("MembershipPlan") %></td>
                                                        <td><%# Eval("RegistrationDate", "{0:MMM dd, yyyy}") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                    <asp:Label ID="lblNoApprovedMembers" runat="server" CssClass="no-items"
                                        Text="No approved members yet" Visible="false"></asp:Label>
                                </div>

                            </div>

                        </div>

                    </div>

                </section>

                <!-- Events Tab -->
                <section id="events" class="admin-tab-content">

                    <div class="container">

                        <h2>Events Management</h2>

                        <div class="admin-panel">

                            <div class="admin-form-section">

                                <h3>Add New Event</h3>

                                <div class="form-group">
                                    <label>Event Title:</label>
                                    <asp:TextBox ID="txtAdminEventTitle" runat="server" placeholder="e.g., Mountain Trek Adventure"></asp:TextBox>
                                </div>

                                <div class="form-row">
                                    <div class="form-group">
                                        <label>Date(s):</label>
                                        <asp:TextBox ID="txtAdminEventDate" runat="server" placeholder="e.g., June 15-17, 2026"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Duration:</label>
                                        <asp:TextBox ID="txtAdminEventDuration" runat="server" placeholder="e.g., 3 days"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>Region:</label>
                                    <asp:TextBox ID="txtAdminEventRegion" runat="server" placeholder="e.g., Eastern Hills"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <label>Description:</label>
                                    <asp:TextBox ID="txtAdminEventDescription" runat="server" TextMode="MultiLine" Rows="4"
                                        placeholder="Describe the event details..."></asp:TextBox>
                                </div>

                                <asp:Label ID="lblAdminEventMsg" runat="server" Font-Bold="true"
                                    style="display:block; margin-bottom:1rem;"></asp:Label>

                                <asp:Button ID="btnAdminAddEvent" runat="server" Text="Add Event"
                                    CssClass="btn btn-primary" OnClick="btnAdminAddEvent_Click" />

                            </div>

                            <asp:Panel ID="pnlEditEvent" runat="server" Visible="false"
                                style="background:var(--surface,#1a1a2e);border:2px solid #1565c0;border-radius:12px;padding:2rem;margin-top:1.5rem;grid-column:1/-1;">
                                <h3 style="color:#42a5f5;margin-bottom:1.5rem;">✏️ Edit Event</h3>
                                <asp:HiddenField ID="hdnEditEventID" runat="server" />
                                <div class="form-group">
                                    <label>Event Title:</label>
                                    <asp:TextBox ID="txtEditEventTitle" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-row">
                                    <div class="form-group">
                                        <label>Date(s):</label>
                                        <asp:TextBox ID="txtEditEventDate" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Duration:</label>
                                        <asp:TextBox ID="txtEditEventDuration" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label>Region:</label>
                                    <asp:TextBox ID="txtEditEventRegion" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Description:</label>
                                    <asp:TextBox ID="txtEditEventDescription" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                </div>
                                <asp:Label ID="lblEditEventMsg" runat="server" Font-Bold="true"
                                    style="display:block;margin-bottom:1rem;"></asp:Label>
                                <div style="display:flex;gap:1rem;">
                                    <asp:Button ID="btnSaveEvent" runat="server" Text="💾 Save Changes"
                                        CssClass="btn btn-primary" OnClick="btnSaveEvent_Click" />
                                    <asp:Button ID="btnCancelEditEvent" runat="server" Text="✕ Cancel"
                                        CssClass="btn btn-sm" style="background:#555;color:white;"
                                        OnClick="btnCancelEditEvent_Click" CausesValidation="false" />
                                </div>
                            </asp:Panel>

                            <div class="admin-list-section">

                                <h3>Current Events</h3>

                                <div class="admin-items-list">
                                    <asp:Repeater ID="rptAdminEvents" runat="server"
                                        OnItemCommand="rptAdminEvents_ItemCommand">
                                        <ItemTemplate>
                                            <div class="admin-item">
                                                <div class="admin-item-header">
                                                    <div>
                                                        <h4><%# Eval("Title") %></h4>
                                                        <p class="admin-item-meta">📅 <%# Eval("EventDate") %> | <%# Eval("EventDuration") %></p>
                                                        <p class="admin-item-meta">📍 <%# Eval("Region") %></p>
                                                    </div>
                                                    <div class="admin-item-actions">
                                                        <asp:Button ID="btnEditEvent" runat="server" CommandName="EditEvent"
                                                            CommandArgument='<%# Eval("EventID") %>' Text="✏️ Edit"
                                                            CssClass="btn btn-sm" style="background:#1565c0;color:white;" />
                                                        <asp:Button ID="btnDeleteEvent" runat="server" CommandName="Delete"
                                                            CommandArgument='<%# Eval("EventID") %>' Text="🗑️ Delete"
                                                            CssClass="btn btn-danger btn-sm"
                                                            OnClientClick="return confirm('Delete this event and all its registrations?');" />
                                                    </div>
                                                </div>
                                                <p class="admin-item-description"><%# Eval("Description") %></p>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Label ID="lblNoEvents" runat="server" CssClass="no-items"
                                        Text="No events added yet" Visible="false"></asp:Label>
                                </div>

                            </div>

                        </div>

                    </div>

                </section>

                <!-- Gallery Tab -->
                <section id="gallery" class="admin-tab-content">

                    <div class="container">

                        <h2>Gallery Management</h2>

                        <div class="admin-panel">

                            <div class="admin-form-section">

                                <h3>Add New Photo</h3>

                                <div class="form-group">
                                    <label>Photo Title:</label>
                                    <asp:TextBox ID="txtAdminPhotoTitle" runat="server"
                                        placeholder="e.g., Mountain Peak Adventure"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <label>Upload Method:</label>
                                    <div class="upload-method-tabs">
                                        <button type="button" class="upload-method-btn active"
                                            onclick="switchUploadMethod('file')">
                                            📁 Upload File
                                        </button>
                                        <button type="button" class="upload-method-btn"
                                            onclick="switchUploadMethod('url')">
                                            🔗 Image URL
                                        </button>
                                    </div>
                                </div>

                                <div id="fileUploadMethod" class="upload-method-content active">
                                    <div class="form-group">
                                        <label>Select Image File:</label>
                                        <asp:FileUpload ID="fileAdminPhoto" runat="server" accept="image/*" />
                                    </div>
                                </div>

                                <div id="urlUploadMethod" class="upload-method-content">
                                    <div class="form-group">
                                        <label>Image URL:</label>
                                        <asp:TextBox ID="txtAdminPhotoUrl" runat="server"
                                            placeholder="https://example.com/image.jpg"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label>Description:</label>
                                    <asp:TextBox ID="txtAdminPhotoDescription" runat="server"
                                        TextMode="MultiLine" Rows="3"
                                        placeholder="Brief description of the photo..."></asp:TextBox>
                                </div>

                                <asp:Label ID="lblAdminGalleryMsg" runat="server" Font-Bold="true"
                                    style="display:block; margin-bottom:1rem;"></asp:Label>

                                <asp:Button ID="btnAdminAddPhoto" runat="server" Text="Add Photo to Gallery"
                                    CssClass="btn btn-primary" OnClick="btnAdminAddPhoto_Click" />

                            </div>

                            <asp:Panel ID="pnlEditImage" runat="server" Visible="false"
                                style="background:var(--surface,#1a1a2e);border:2px solid #1565c0;border-radius:12px;padding:2rem;margin-top:1.5rem;grid-column:1/-1;">
                                <h3 style="color:#42a5f5;margin-bottom:1.5rem;">✏️ Edit Photo</h3>
                                <asp:HiddenField ID="hdnEditImageID" runat="server" />
                                <asp:HiddenField ID="hdnEditImageURL" runat="server" />
                                <div class="form-group">
                                    <label>Photo Title:</label>
                                    <asp:TextBox ID="txtEditImageTitle" runat="server"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Description:</label>
                                    <asp:TextBox ID="txtEditImageDescription" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Replace Photo (optional — leave blank to keep existing):</label>
                                    <asp:FileUpload ID="fileEditPhoto" runat="server" accept="image/*" />
                                    <asp:Image ID="imgEditPreview" runat="server"
                                        style="display:block;max-width:200px;max-height:150px;margin-top:0.75rem;border-radius:6px;border:1px solid #333;"
                                        AlternateText="Current photo" />
                                </div>
                                <asp:Label ID="lblEditImageMsg" runat="server" Font-Bold="true"
                                    style="display:block;margin-bottom:1rem;"></asp:Label>
                                <div style="display:flex;gap:1rem;">
                                    <asp:Button ID="btnSaveImage" runat="server" Text="💾 Save Changes"
                                        CssClass="btn btn-primary" OnClick="btnSaveImage_Click" />
                                    <asp:Button ID="btnCancelEditImage" runat="server" Text="✕ Cancel"
                                        CssClass="btn btn-sm" style="background:#555;color:white;"
                                        OnClick="btnCancelEditImage_Click" CausesValidation="false" />
                                </div>
                            </asp:Panel>

                            <div class="admin-list-section">

                                <h3>All Gallery Photos</h3>

                                <div class="admin-items-list">
                                    <asp:Repeater ID="rptAdminGallery" runat="server"
                                        OnItemCommand="rptAdminGallery_ItemCommand">
                                        <ItemTemplate>
                                            <div class="admin-item">
                                                <div class="admin-item-header">
                                                    <div>
                                                        <h4><%# Eval("Title") %></h4>
                                                        <p class="admin-item-meta">
                                                            By: <%# Eval("UploadedByUserName") %> |
                                                            Status: <%# Convert.ToBoolean(Eval("IsApproved")) ? "<span style='color:#4caf50;'>Approved</span>" : "<span style='color:orange;'>Pending</span>" %>
                                                        </p>
                                                    </div>
                                                    <div class="admin-item-actions">
                                                        <asp:Button ID="btnApproveImg" runat="server" CommandName="Approve"
                                                            CommandArgument='<%# Eval("ImageID") %>' Text="Approve"
                                                            CssClass="btn btn-success btn-sm"
                                                            Visible='<%# !Convert.ToBoolean(Eval("IsApproved")) %>' />
                                                        <asp:Button ID="btnEditImg" runat="server" CommandName="EditImage"
                                                            CommandArgument='<%# Eval("ImageID") %>' Text="✏️ Edit"
                                                            CssClass="btn btn-sm" style="background:#1565c0;color:white;" />
                                                        <asp:Button ID="btnDeleteImg" runat="server" CommandName="Delete"
                                                            CommandArgument='<%# Eval("ImageID") %>' Text="🗑️ Delete"
                                                            CssClass="btn btn-danger btn-sm"
                                                            OnClientClick="return confirm('Delete this photo?');" />
                                                    </div>
                                                </div>
                                                <img src='<%# Eval("ImageURL") %>' alt='<%# Eval("Title") %>'
                                                    class="admin-item-preview"
                                                    onerror="this.style.display='none';" />
                                                <p class="admin-item-description"><%# Eval("Description") %></p>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Label ID="lblNoGallery" runat="server" CssClass="no-items"
                                        Text="No photos in gallery yet" Visible="false"></asp:Label>
                                </div>

                            </div>

                        </div>

                    </div>

                </section>

                <!-- Messages Tab -->
                <section id="messages" class="admin-tab-content">
                    <div class="container">
                        <h2>Contact Messages</h2>
                        <div class="admin-panel" style="grid-template-columns: 1fr;">
                            <div class="admin-list-section" style="grid-column: 1/-1;">
                                <div class="admin-table-container">
                                    <table class="admin-table">
                                        <thead>
                                            <tr>
                                                <th>Date</th>
                                                <th>Email</th>
                                                <th>Message</th>
                                                <th>Status</th>
                                                <th>Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptContactMessages" runat="server"
                                                OnItemCommand="rptContactMessages_ItemCommand">
                                                <ItemTemplate>
                                                    <tr
                                                        style='<%# Convert.ToBoolean(Eval("IsRead")) ? "opacity:0.7;" : "font-weight:bold;" %>'>
                                                        <td>
                                                            <%# Eval("SentDate", "{0:MMM dd, yyyy}" ) %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("Email") %>
                                                        </td>
                                                        <td class="message-cell">
                                                            <%# Eval("Message") %>
                                                        </td>
                                                        <td>
                                                            <span class='status-badge <%# Convert.ToBoolean(Eval("IsRead")) ? "status-read" : "status-new" %>'>
                                                                <%# Convert.ToBoolean(Eval("IsRead")) ? "Read" : "New" %>
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnMarkRead" runat="server"
                                                                CommandName="MarkRead"
                                                                CommandArgument='<%# Eval("MessageID") %>' Text="Mark Read"
                                                                Visible='<%# !Convert.ToBoolean(Eval("IsRead")) %>'
                                                                CssClass="btn btn-primary btn-sm" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>

                <!-- Bookings Tab -->
                <section id="bookings" class="admin-tab-content">

                    <div class="container">

                        <h2>Trip Bookings Management</h2>

                        <div class="admin-panel" style="grid-template-columns: 1fr;">
                            <div class="admin-list-section" style="grid-column: 1/-1;">

                                <h3>All Bookings</h3>
                                <div class="admin-table-container">
                                    <table class="admin-table">
                                        <thead>
                                            <tr>
                                                <th>Event</th>
                                                <th>Member</th>
                                                <th>Email</th>
                                                <th>Participants</th>
                                                <th>Status</th>
                                                <th>Booked On</th>
                                                <th>Actions</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptAdminBookings" runat="server" OnItemCommand="rptAdminBookings_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Eval("EventTitle") %></td>
                                                        <td><%# Eval("UserFullName") %></td>
                                                        <td><%# Eval("UserEmail") %></td>
                                                        <td><%# Eval("NumberOfParticipants") %></td>
                                                        <td>
                                                            <span class='status-badge <%# Eval("Status").ToString() == "Approved" ? "status-approved" : (Eval("Status").ToString() == "Rejected" ? "status-new" : "status-pending") %>'>
                                                                <%# Eval("Status") %>
                                                            </span>
                                                        </td>
                                                        <td><%# Eval("RegistrationDate", "{0:MMM dd, yyyy}") %></td>
                                                        <td>
                                                            <asp:Button ID="btnApproveBooking" runat="server" CommandName="Approve" CommandArgument='<%# Eval("RegistrationID") %>' Text="Approve" CssClass="btn btn-primary btn-sm" Visible='<%# Eval("Status").ToString() == "Pending" %>' />
                                                            <asp:Button ID="btnRejectBooking" runat="server" CommandName="Reject" CommandArgument='<%# Eval("RegistrationID") %>' Text="Reject" CssClass="btn btn-sm" style="background-color:#d32f2f; color:white;" Visible='<%# Eval("Status").ToString() == "Pending" %>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                    <asp:Label ID="lblNoBookings2" runat="server" CssClass="no-items"
                                        Text="No bookings yet" Visible="false"></asp:Label>
                                </div>

                            </div>

                        </div>

                    </div>

                </section>

            
                <!-- Reviews Tab -->
                <section id="reviews" class="admin-tab-content">
                    <div class="container">
                        <h2>Member Reviews</h2>
                        <div class="admin-panel" style="grid-template-columns:1fr;">
                            <div class="admin-list-section" style="grid-column:1/-1;">
                                <h3>Pending &amp; Approved Reviews</h3>
                                <div class="admin-table-container">
                                    <table class="admin-table">
                                        <thead>
                                            <tr>
                                                <th>Member</th><th>Event</th><th>Title</th>
                                                <th>Review</th><th>Rating</th><th>Date</th>
                                                <th>Status</th><th>Actions</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptAdminReviews" runat="server"
                                                OnItemCommand="rptAdminReviews_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Eval("UserFullName") %></td>
                                                        <td><%# Eval("EventName") %></td>
                                                        <td><%# Eval("Title") %></td>
                                                        <td style="max-width:240px;word-wrap:break-word;"><%# Eval("ReviewText") %></td>
                                                        <td style="color:#FFB300;"><%# new string('\u2605', Convert.ToInt32(Eval("Rating"))) %></td>
                                                        <td><%# Eval("ReviewDate", "{0:MMM dd, yyyy}") %></td>
                                                        <td><%# Convert.ToBoolean(Eval("IsApproved"))
                                                            ? "<span style='color:#4caf50;font-weight:bold;'>Approved</span>"
                                                            : "<span style='color:orange;font-weight:bold;'>Pending</span>" %></td>
                                                        <td>
                                                            <asp:Button ID="btnApproveReview" runat="server"
                                                                CommandName="Approve"
                                                                CommandArgument='<%# Eval("ReviewID") %>'
                                                                Text="Approve" CssClass="btn btn-success btn-sm"
                                                                Visible='<%# !Convert.ToBoolean(Eval("IsApproved")) %>' />
                                                            <asp:Button ID="btnUnapproveReview" runat="server"
                                                                CommandName="Reject"
                                                                CommandArgument='<%# Eval("ReviewID") %>'
                                                                Text="Unapprove" CssClass="btn btn-sm"
                                                                style="background:#e65100;color:white;"
                                                                Visible='<%# Convert.ToBoolean(Eval("IsApproved")) %>'
                                                                OnClientClick="return confirm('Remove this review from public view?');" />
                                                            <asp:Button ID="btnDeleteReview" runat="server"
                                                                CommandName="Delete"
                                                                CommandArgument='<%# Eval("ReviewID") %>'
                                                                Text="Delete" CssClass="btn btn-danger btn-sm"
                                                                OnClientClick="return confirm('Permanently delete this review?');" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                                <asp:Label ID="lblNoAdminReviews" runat="server" CssClass="no-items"
                                    Text="No reviews submitted yet." Visible="false"></asp:Label>
                            </div>
                        </div>
                    </div>
                </section>

            </asp:Panel>

        </main>

        <asp:HiddenField ID="hdnActiveAdminTab" runat="server" Value="overview" ClientIDMode="Static" />

        <script src="JS/admin.js"></script>

    </asp:Content>

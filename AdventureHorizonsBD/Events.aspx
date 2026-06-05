<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/MasterPages/Site.master"
    CodeBehind="Events.aspx.cs"
    Inherits="AdventureHorizonsBD.Events" %>

<asp:Content ID="TitleContent1"
    ContentPlaceHolderID="TitleContent"
    runat="server">

    Events - Adventure Horizons BD

</asp:Content>

<asp:Content ID="MainContent1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <main>

        <section class="section section-muted" style="padding-top: 5rem;">

            <div class="container">

                <p class="eyebrow">
                    Upcoming Expeditions
                </p>

                <h2>
                    Trips and Activities
                </h2>

                <div class="grid-events-expanded">
                    <asp:Repeater ID="rptEvents" runat="server">
                        <ItemTemplate>
                            <article class="event-card">
                                <p class="event-date"><%# Eval("EventDate") %></p>
                                <h3><%# Eval("Title") %></h3>
                                <p><strong>Region:</strong> <%# Eval("Region") %> | <%# Eval("EventDuration") %></p>
                                <p><%# Eval("Description") %></p>
                                <button type="button" class="btn btn-primary"
                                    onclick='openBookingModal(<%# Eval("EventID") %>, "<%# Eval("Title") %>")'>
                                    Book Now
                                </button>
                            </article>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

            </div>

        </section>

    </main>

    <!-- Booking Modal -->

    <div id="bookingModal" class="modal" style="display:none; position:fixed; top:0; left:0; width:100%; height:100%; background:rgba(0,0,0,0.8); z-index:9999; align-items:center; justify-content:center;">
        <div class="modal-content" style="background:var(--surface); padding:2.5rem; border-radius:var(--radius); max-width:500px; width:90%; position:relative; box-shadow:0 10px 30px rgba(0,0,0,0.5); border:1px solid rgba(255,255,255,0.1);">
            <span class="close-modal" onclick="closeBookingModal()" style="position:absolute; right:1.5rem; top:1.5rem; cursor:pointer; font-size:1.8rem; color:var(--muted); transition:0.3s; line-height:1;">&times;</span>
            <h2 style="margin-top:0; border-bottom:1px solid rgba(255,255,255,0.1); padding-bottom:1rem; margin-bottom:1.5rem;">Book <span id="modalEventName" style="color:var(--primary);"></span></h2>
            
            <asp:Panel ID="pnlBookingForm" runat="server" DefaultButton="btnSubmitBooking">
                <asp:HiddenField ID="hdnEventID" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnEventTitle" runat="server" ClientIDMode="Static" />

                <div class="form-group" style="margin-bottom:1.5rem;">
                    <label for="txtBookingParticipants" style="display:block; margin-bottom:0.5rem; color:#fff;">Number of Participants *</label>
                    <asp:TextBox ID="txtBookingParticipants" runat="server" TextMode="Number" min="1" max="10" Text="1" required="required" style="width:100%; padding:0.8rem; border-radius:4px; background:rgba(255,255,255,0.05); border:1px solid rgba(255,255,255,0.1); color:#fff;"></asp:TextBox>
                </div>

                <div class="form-group" style="margin-bottom:1.5rem;">
                    <label for="txtBookingRequests" style="display:block; margin-bottom:0.5rem; color:#fff;">Special Requests/Dietary Requirements</label>
                    <asp:TextBox ID="txtBookingRequests" runat="server" TextMode="MultiLine" Rows="3" style="width:100%; padding:0.8rem; border-radius:4px; background:rgba(255,255,255,0.05); border:1px solid rgba(255,255,255,0.1); color:#fff;"></asp:TextBox>
                </div>

                <asp:Label ID="lblBookingMsg" runat="server" ForeColor="#ff5252" Font-Bold="true" style="display:block; margin-bottom:1rem;"></asp:Label>
                
                <asp:Button ID="btnSubmitBooking" runat="server" Text="Confirm Booking" CssClass="btn btn-primary" OnClick="btnSubmitBooking_Click" style="width:100%; padding:1rem; font-size:1.1rem; border-radius:4px;" />
            </asp:Panel>
        </div>
    </div>

    <script type="text/javascript">
        function openBookingModal(eventId, eventTitle) {
            document.getElementById('modalEventName').innerText = eventTitle;
            document.getElementById('hdnEventID').value = eventId;
            document.getElementById('hdnEventTitle').value = eventTitle;
            document.getElementById('bookingModal').style.display = 'block';
        }

        function closeBookingModal() {
            document.getElementById('bookingModal').style.display = 'none';
        }
    </script>

</asp:Content>
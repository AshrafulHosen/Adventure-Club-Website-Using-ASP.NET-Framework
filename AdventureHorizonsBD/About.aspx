<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/MasterPages/Site.master"
    CodeBehind="About.aspx.cs"
    Inherits="AdventureHorizonsBD.About" %>

<asp:Content ID="TitleContent1"
    ContentPlaceHolderID="TitleContent"
    runat="server">

    About - Adventure Horizons BD

</asp:Content>

<asp:Content ID="MainContent1"
    ContentPlaceHolderID="MainContent"
    runat="server">

    <main>

        <section class="section" style="padding-top: 5rem;">

            <div class="container split-layout">

                <div>

                    <p class="eyebrow">
                        About the Club
                    </p>

                    <h2>
                        Our Mission
                    </h2>

                    <p>
                        Our mission is to create safe, responsible,
                        and exciting maritime-inspired adventures
                        for people of all skill levels. We believe
                        exploration builds confidence, strengthens
                        community, and deepens respect for
                        Bangladesh's rivers and coastline.
                    </p>

                    <p>
                        From the Sundarbans to Cox's Bazar and
                        across the river routes of the delta,
                        we organize meaningful expeditions that
                        combine exploration, learning, and teamwork.
                    </p>

                    <p style="margin-top: 1.5rem;">

                        With over a decade of experience organizing
                        adventures across Bangladesh,
                        Adventure Horizons BD has built a reputation
                        for safety, environmental stewardship,
                        and unforgettable experiences.

                        Our team of trained guides and expedition
                        leaders are passionate about sharing
                        the natural beauty of our country with
                        adventurers of all backgrounds and skill levels.

                    </p>

                </div>

                <div class="info-cards" aria-label="Club values">

                    <article class="card">
                        <h3>Safety First</h3>
                        <p>
                            Guided plans, tide checks, trained leads,
                            and practical checklists on every trip.
                        </p>
                    </article>

                    <article class="card">
                        <h3>Eco Friendly</h3>
                        <p>
                            Leave-no-trace values to protect coasts,
                            islands, mangroves, and rivers for everyone.
                        </p>
                    </article>

                    <article class="card">
                        <h3>Inclusive Community</h3>
                        <p>
                            Welcoming students, professionals,
                            and families who love the water
                            and the outdoors.
                        </p>
                    </article>

                    <article class="card">
                        <h3>Experienced Leaders</h3>
                        <p>
                            Certified guides with extensive
                            knowledge of Bangladesh's regions
                            and their ecosystems.
                        </p>
                    </article>

                    <article class="card">
                        <h3>Quality Equipment</h3>
                        <p>
                            Well-maintained camping gear,
                            safety equipment, and navigation
                            tools on all expeditions.
                        </p>
                    </article>

                    <article class="card">
                        <h3>Cultural Respect</h3>
                        <p>
                            Engaging respectfully with local
                            communities and supporting sustainable
                            tourism practices.
                        </p>
                    </article>

                </div>

            </div>

        </section>

        <!-- Community Voices -->

        <section class="section community-section"
            style="background: linear-gradient(135deg,
            rgba(255,69,0,.05),
            rgba(255,179,0,.05));">

            <div class="container">

                <p class="eyebrow"
                    style="text-align:center;">

                    Member Experiences

                </p>

                <h2 style="text-align:center;">
                    Community Voices
                </h2>

                <p style="
                    text-align:center;
                    color:var(--muted);
                    margin-bottom:3rem;
                    font-size:1.1rem;">

                    Read what our members have to say
                    about their adventures with us

                </p>

                <div class="testimonials-grid">

                    <asp:Repeater ID="rptCommunityReviews" runat="server">
                        <ItemTemplate>
                            <article class="testimonial-card">
                                <div class="stars"><%# new string('★', Convert.ToInt32(Eval("Rating"))) %><%# new string('☆', 5 - Convert.ToInt32(Eval("Rating"))) %></div>
                                <p style="margin: 1rem 0 0.5rem; font-weight: 600; font-size: 1.1rem;">"<%# Eval("Title") %>"</p>
                                <p style="color: var(--muted); font-size: 0.9rem; margin: 0.5rem 0;">📍 <%# Eval("EventName") %></p>
                                <p style="margin: 1rem 0; line-height: 1.6;"><%# Eval("ReviewText") %></p>
                                <footer class="author">— <%# Eval("UserFullName") %></footer>
                            </article>
                        </ItemTemplate>
                    </asp:Repeater>

                    <asp:Label ID="lblNoReviews" runat="server"
                        Text="No community reviews yet. Be the first to share your experience!"
                        CssClass="no-reviews"
                        Visible="false">
                    </asp:Label>

                </div>

                <div style="
                    text-align:center;
                    margin-top:2rem;">

                    <asp:HyperLink
                        ID="lnkShareStory"
                        runat="server"
                        CssClass="btn btn-primary"
                        NavigateUrl="~/User.aspx">

                        Share Your Story

                    </asp:HyperLink>

                </div>

            </div>

        </section>

    </main>

</asp:Content>
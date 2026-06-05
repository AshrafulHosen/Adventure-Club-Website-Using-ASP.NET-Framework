<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Site.master" CodeBehind="Default.aspx.cs"
    Inherits="AdventureHorizonsBD.Default" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">

        Adventure Horizons BD | Conquer the Wilderness

    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <main>

            <section id="home" class="hero">

                <div class="hero-overlay"></div>

                <div class="container hero-content" style="display:flex;
flex-direction:column;
align-items:center;
text-align:center;">

                    <span style="
display:inline-block;
background:rgba(255,69,0,.15);
color:var(--primary);
padding:.5rem 1.2rem;
border-radius:50px;
font-weight:700;
margin-bottom:1.5rem;
border:1px solid rgba(255,69,0,.3);">

                        🔥 NEW EXPEDITIONS ADDED FOR 2026

                    </span>

                    <h1 style="
font-size:clamp(3rem,6vw,5.5rem);
line-height:1.1;
margin-bottom:1.5rem;">

                        Conquer the

                        <br />

                        <span class="highlight-text">
                            Wilderness
                        </span>

                    </h1>

                    <p style="
font-size:1.25rem;
color:var(--muted);
margin:0 auto 2.5rem;
max-width:700px;">

                        Push your limits with Adventure Horizons BD.
                        Experience raw, unfiltered nature across
                        the most breathtaking and challenging
                        landscapes in Bangladesh.

                    </p>

                    <div class="hero-actions" style="justify-content:center;">

                        <asp:HyperLink ID="lnkExplore" runat="server" CssClass="btn btn-primary"
                            NavigateUrl="~/Regions.aspx">

                            Start Exploring

                        </asp:HyperLink>

                        <asp:HyperLink ID="lnkTrips" runat="server" CssClass="btn btn-secondary"
                            NavigateUrl="~/Events.aspx">

                            View Upcoming Trips

                        </asp:HyperLink>

                    </div>

                    <div class="hero-stats" style="margin-top:3.5rem;">

                        <div>

                            <strong>28+</strong>

                            <span>Trips Per Year</span>

                        </div>

                        <div>

                            <strong>10</strong>

                            <span>Districts Conquered</span>

                        </div>

                        <div>

                            <strong>100%</strong>

                            <span>Safety Guaranteed</span>

                        </div>

                    </div>

                </div>

            </section>

            <section class="section why-section" style="background:
linear-gradient(
135deg,
rgba(255,69,0,.05),
rgba(255,179,0,.05));">

                <div class="container">

                    <p class="eyebrow" style="text-align:center;">

                        Why Choose Us

                    </p>

                    <h2 style="text-align:center;">
                        The Adventure Horizons Difference
                    </h2>

                    <div class="why-grid">

                        <article class="why-card">
                            <div class="why-icon">🏆</div>
                            <h3>Expert Leadership</h3>
                            <p>
                                Experienced guides with deep knowledge
                                of Bangladesh's geography, culture,
                                and ecosystems.
                            </p>
                        </article>

                        <article class="why-card">
                            <div class="why-icon">🛡️</div>
                            <h3>Safety Certified</h3>
                            <p>
                                All expeditions follow international
                                safety standards with emergency
                                protocols and trained guides.
                            </p>
                        </article>

                        <article class="why-card">
                            <div class="why-icon">🌿</div>
                            <h3>Eco Conscious</h3>
                            <p>
                                Leave-no-trace practices to preserve
                                Bangladesh's natural beauty.
                            </p>
                        </article>

                        <article class="why-card">
                            <div class="why-icon">👥</div>
                            <h3>Community Focused</h3>
                            <p>
                                Build lasting friendships with
                                adventurers across all experience levels.
                            </p>
                        </article>

                        <article class="why-card">
                            <div class="why-icon">💰</div>
                            <h3>Affordable Pricing</h3>
                            <p>
                                Flexible membership plans designed
                                for students and professionals.
                            </p>
                        </article>

                        <article class="why-card">
                            <div class="why-icon">🗺️</div>
                            <h3>Diverse Routes</h3>
                            <p>
                                Adventures across mountains,
                                beaches and river deltas.
                            </p>
                        </article>

                    </div>

                </div>

            </section>

            <section class="section featured-destinations">

                <div class="container">

                    <p class="eyebrow">
                        Explore Bangladesh
                    </p>

                    <h2>
                        Featured Destinations
                    </h2>

                    <div class="destinations-grid">

                        <div class="destination-card">

                            <div class="destination-header">
                                🏔️
                            </div>

                            <h3>
                                Sylhet Hills
                            </h3>

                            <p>
                                Tea gardens, waterfalls and lush landscapes
                            </p>

                            <asp:HyperLink runat="server" CssClass="explore-link" NavigateUrl="~/Regions.aspx">

                                Explore →

                            </asp:HyperLink>

                        </div>

                        <div class="destination-card">

                            <div class="destination-header">
                                🏖️
                            </div>

                            <h3>
                                Cox's Bazar Coast
                            </h3>

                            <p>
                                Longest beach with pristine waters
                                and islands
                            </p>

                            <asp:HyperLink runat="server" CssClass="explore-link" NavigateUrl="~/Regions.aspx">

                                Explore →

                            </asp:HyperLink>

                        </div>

                        <div class="destination-card">

                            <div class="destination-header">
                                🌳
                            </div>

                            <h3>
                                Sundarbans
                            </h3>

                            <p>
                                Mangrove forest and unmatched biodiversity
                            </p>

                            <asp:HyperLink runat="server" CssClass="explore-link" NavigateUrl="~/Regions.aspx">

                                Explore →

                            </asp:HyperLink>

                        </div>

                        <div class="destination-card">

                            <div class="destination-header">
                                💧
                            </div>

                            <h3>
                                River Deltas
                            </h3>

                            <p>
                                Historic waterways and pastoral villages
                            </p>

                            <asp:HyperLink runat="server" CssClass="explore-link" NavigateUrl="~/Regions.aspx">

                                Explore →

                            </asp:HyperLink>

                        </div>

                    </div>

                </div>

            </section>

            <section class="section how-it-works-section">

                <div class="container">

                    <p class="eyebrow">
                        Getting Started
                    </p>

                    <h2>
                        How It Works
                    </h2>

                    <div class="steps-grid">

                        <div class="step-card">
                            <div class="step-number">1</div>
                            <h3>Browse Adventures</h3>
                            <p>Explore our upcoming expeditions.</p>
                        </div>

                        <div class="step-card">
                            <div class="step-number">2</div>
                            <h3>Choose Your Trip</h3>
                            <p>Select an adventure matching your interests.</p>
                        </div>

                        <div class="step-card">
                            <div class="step-number">3</div>
                            <h3>Complete Registration</h3>
                            <p>Join membership and create your profile.</p>
                        </div>

                        <div class="step-card">
                            <div class="step-number">4</div>
                            <h3>Prepare & Depart</h3>
                            <p>Meet your team and begin the adventure.</p>
                        </div>

                    </div>

                </div>

            </section>

            <section class="section testimonials-section">

                <div class="container">

                    <p class="eyebrow">
                        Community Voices
                    </p>

                    <h2>
                        What Adventurers Say
                    </h2>

                    <div class="testimonials-grid">

                        <article class="testimonial-card">
                            <div class="stars">⭐⭐⭐⭐⭐</div>
                            <p class="quote">
                                Adventure Horizons BD completely changed
                                how I experience Bangladesh.
                            </p>
                            <p class="author">
                                — Sarah M., Dhaka
                            </p>
                        </article>

                        <article class="testimonial-card">
                            <div class="stars">⭐⭐⭐⭐⭐</div>
                            <p class="quote">
                                The team was supportive and patient.
                            </p>
                            <p class="author">
                                — Rahman K., Chittagong
                            </p>
                        </article>

                        <article class="testimonial-card">
                            <div class="stars">⭐⭐⭐⭐⭐</div>
                            <p class="quote">
                                Best adventure club I have ever joined.
                            </p>
                            <p class="author">
                                — Aisha L., Sylhet
                            </p>
                        </article>

                    </div>

                </div>

            </section>

            <section class="section cta-section" style="
background:
linear-gradient(
130deg,
#CC3700 0%,
#FF4500 100%);
color:white;
padding:6rem 0;
border-radius:24px;">

                <div class="container">

                    <div style="text-align:center;">

                        <p class="eyebrow" style="color:#FFE866;">

                            Ready for Your Adventure?

                        </p>

                        <h2 style="color:white;">
                            Start Your Journey Today
                        </h2>

                        <p style="
max-width:600px;
margin:1rem auto 2.5rem;">

                            Join hundreds of adventurers exploring
                            the natural beauty of Bangladesh.

                        </p>

                        <div style="
display:flex;
gap:1rem;
justify-content:center;">

                            <asp:HyperLink runat="server" CssClass="btn btn-primary" NavigateUrl="~/Events.aspx">

                                View Upcoming Trips

                            </asp:HyperLink>

                            <asp:HyperLink runat="server" CssClass="btn btn-secondary" NavigateUrl="~/Contact.aspx">

                                Get In Touch

                            </asp:HyperLink>

                        </div>

                    </div>

                </div>

            </section>

        </main>

    </asp:Content>
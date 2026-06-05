-- Create Database if not exists
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'AdventureClubDB')
BEGIN
    CREATE DATABASE AdventureClubDB;
END
GO

USE AdventureClubDB;
GO

-- 1. Users Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        UserID INT IDENTITY(1,1) PRIMARY KEY,
        FullName NVARCHAR(100) NOT NULL,
        Email NVARCHAR(255) UNIQUE NOT NULL,
        PasswordHash NVARCHAR(255) NOT NULL,
        Phone NVARCHAR(20) NULL,
        MembershipPlan NVARCHAR(50) NULL,
        ExperienceLevel NVARCHAR(50) NULL,
        Role NVARCHAR(20) DEFAULT 'Member' CHECK (Role IN ('Admin', 'Member')),
        IsApproved BIT DEFAULT 0,
        RegistrationDate DATETIME DEFAULT GETDATE()
    );
END
GO

-- 2. MembershipRequests Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MembershipRequests')
BEGIN
    CREATE TABLE MembershipRequests (
        RequestID INT IDENTITY(1,1) PRIMARY KEY,
        UserID INT FOREIGN KEY REFERENCES Users(UserID),
        MembershipType NVARCHAR(50) NOT NULL,
        Status NVARCHAR(20) DEFAULT 'Pending' CHECK (Status IN ('Pending', 'Approved', 'Rejected')),
        RequestDate DATETIME DEFAULT GETDATE(),
        ReviewDate DATETIME NULL
    );
END
GO

-- 3. Events Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Events')
BEGIN
    CREATE TABLE Events (
        EventID INT IDENTITY(1,1) PRIMARY KEY,
        Title NVARCHAR(200) NOT NULL,
        EventDate NVARCHAR(100) NOT NULL,
        EventDuration NVARCHAR(50) NOT NULL,
        Region NVARCHAR(100) NOT NULL,
        Description NVARCHAR(MAX) NULL,
        CreatedDate DATETIME DEFAULT GETDATE()
    );
END
GO

-- 4. EventRegistrations Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'EventRegistrations')
BEGIN
    CREATE TABLE EventRegistrations (
        RegistrationID INT IDENTITY(1,1) PRIMARY KEY,
        UserID INT FOREIGN KEY REFERENCES Users(UserID),
        EventID INT FOREIGN KEY REFERENCES Events(EventID),
        NumberOfParticipants INT DEFAULT 1,
        SpecialRequests NVARCHAR(500) NULL,
        Status NVARCHAR(20) DEFAULT 'Pending',
        RegistrationDate DATETIME DEFAULT GETDATE()
    );
END
GO

-- 5. Regions Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Regions')
BEGIN
    CREATE TABLE Regions (
        RegionID INT IDENTITY(1,1) PRIMARY KEY,
        RegionName NVARCHAR(100) NOT NULL,
        Highlights NVARCHAR(300) NULL,
        Description NVARCHAR(MAX) NULL,
        PopularTrips NVARCHAR(300) NULL
    );
END
GO

-- 6. Gallery Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Gallery')
BEGIN
    CREATE TABLE Gallery (
        ImageID INT IDENTITY(1,1) PRIMARY KEY,
        Title NVARCHAR(200) NOT NULL,
        ImageURL NVARCHAR(500) NOT NULL,
        Description NVARCHAR(500) NULL,
        UploadedByUserID INT FOREIGN KEY REFERENCES Users(UserID) NULL,
        IsApproved BIT DEFAULT 0,
        UploadDate DATETIME DEFAULT GETDATE()
    );
END
GO

-- 7. ContactMessages Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ContactMessages')
BEGIN
    CREATE TABLE ContactMessages (
        MessageID INT IDENTITY(1,1) PRIMARY KEY,
        Email NVARCHAR(255) NOT NULL,
        Message NVARCHAR(MAX) NOT NULL,
        IsRead BIT DEFAULT 0,
        SentDate DATETIME DEFAULT GETDATE()
    );
END
GO

-- ==============================================
-- SEED DATA
-- ==============================================

-- Seed Admin User
-- Password is 'Admin@123' (We will generate proper hash later in code, but for now we put a placeholder)
-- Actually, let's leave PasswordHash as a known value or we can update it via app later. 
-- For now, I will use a dummy hash and we will use the application to register the admin properly or we'll generate a hash.
-- To make it simple, let's insert a dummy admin and we'll implement a RegisterAdmin page or logic if needed.
-- Wait, the C# hashing will use PBKDF2. It's better to NOT seed the admin password here, or seed a plain one and we hash on first login if it matches.
-- Let's just create an admin with a known hash of 'Admin@123'. 
-- PBKDF2 hash of Admin@123: 10000 iterations, let's just insert 'admin123' for now and update our auth logic to handle it if needed.
-- Actually I will generate the hash in C# and insert it. Let's just insert basic non-hashed 'Admin@123' and I'll update it later.

IF NOT EXISTS (SELECT * FROM Users WHERE Email = 'admin@test.com')
BEGIN
    -- Plain text password: the AuthBLL.Login() fallback will match this and allow login.
    INSERT INTO Users (FullName, Email, PasswordHash, Role, IsApproved)
    VALUES ('System Admin', 'admin@test.com', 'admin123', 'Admin', 1);
END
GO

-- Seed Events
IF NOT EXISTS (SELECT * FROM Events)
BEGIN
    INSERT INTO Events (Title, EventDate, EventDuration, Region, Description) VALUES 
    ('Teknaf Coastal Camp', 'May 3-4, 2026', '2 days', 'Southeast Coast', 'Two-day shoreline camp with beach walk planning, night sky viewing, and group meals.'),
    ('Sylhet Tea Garden Trek', 'May 10-12, 2026', '3 days', 'North Bengal', 'Guided trek through tea gardens and waterfalls in Sylhet with local homestay experience.'),
    ('River Navigation Workshop', 'May 18, 2026', '1 day', 'Central Bangladesh', 'Beginner-friendly route reading, knot practice, and safety training near Dhaka rivers.'),
    ('Kaptai Lake Expedition', 'May 25-28, 2026', '4 days', 'Eastern Hills', 'Multi-day lake and mountain exploration with camping and hiking in the Chittagong Hill Tracts.'),
    ('Sundarbans Eco-Cruise', 'June 7-9, 2026', '3 days', 'Sundarbans', 'Guided eco-cruise through mangrove channels with birdwatching and field research activities.'),
    ('St. Martin''s Island Tour', 'June 14-16, 2026', '3 days', 'Southeast Coast', 'Island camping and snorkeling adventure to Bangladesh''s only coral island with pristine waters.');
END
GO

-- Seed Regions
IF NOT EXISTS (SELECT * FROM Regions)
BEGIN
    INSERT INTO Regions (RegionName, Highlights, Description, PopularTrips) VALUES
    ('North Bengal', 'Hill treks, tea gardens, bamboo forests', 'Explore Sylhet''s tea estates, Jaintia Hills, and hidden waterfalls. Perfect for trekking and nature photography.', 'Jaflong Trek, Ratargul Swamp Expedition'),
    ('Southeast Coast', 'Sandy beaches, coral islands, water activities', 'Cox''s Bazar, St. Martin''s Island, and Teknaf offer pristine beaches and island exploration. Ideal for coastal camps.', 'Teknaf Beach Camp, St. Martin''s Island Tour'),
    ('Sundarbans Region', 'Mangroves, river routes, wildlife viewing', 'The world''s largest mangrove forest offers eco-cruises, boat expeditions, and unique ecosystem exploration.', 'Sundarbans Eco-Cruise, Tiger Reserve Journey'),
    ('Central Bangladesh', 'River systems, pastoral villages, cultural sites', 'Navigate the Meghna and Padma rivers, explore village life, and discover ancient historical monuments.', 'Padma River Navigation, Rural Village Tours'),
    ('Northwest Region', 'Ancient ruins, agricultural lands, seasonal wetlands', 'Bogra, Naogaon, and Rajshahi offer historical exploration and seasonal bird-watching adventures.', 'Historical Route Trek, Seasonal Wetland Safari'),
    ('Eastern Hills', 'Mountain terrain, tribal villages, scenic landscapes', 'Chittagong Hill Tracts present challenging treks, cultural encounters, and pristine natural beauty.', 'Hill Tract Trek, Kaptai Lake Expedition');
END
GO

-- Seed Gallery
IF NOT EXISTS (SELECT * FROM Gallery)
BEGIN
    INSERT INTO Gallery (Title, ImageURL, Description, IsApproved) VALUES
    ('Forest Trail Expedition', 'images/pic-1.jpg', 'Hikers moving through a forest trail', 1),
    ('Sunrise in the Hills', 'images/pic-2.jpg', 'Mountain view during sunrise trek', 1),
    ('Campfire Night', 'images/pic-3.jpg', 'Campfire gathering at night', 1),
    ('Cycling Adventure', 'images/pic-4.jpg', 'Cyclists on an open road', 1),
    ('Weekend Camping', 'images/pic-5.jpg', 'Tent camp beside green hills', 1),
    ('Waterfall View', 'images/pic-6.jpg', 'Group photo beside water and rocks', 1);
END
GO

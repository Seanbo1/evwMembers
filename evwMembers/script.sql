USE [master]
GO
/****** Object:  Database [evwMembers]    Script Date: 8/14/2021 11:08:18 AM ******/
CREATE DATABASE [evwMembers]
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [evwMembers].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [evwMembers] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [evwMembers] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [evwMembers] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [evwMembers] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [evwMembers] SET ARITHABORT OFF 
GO
ALTER DATABASE [evwMembers] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [evwMembers] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [evwMembers] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [evwMembers] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [evwMembers] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [evwMembers] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [evwMembers] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [evwMembers] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [evwMembers] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [evwMembers] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [evwMembers] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [evwMembers] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [evwMembers] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [evwMembers] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [evwMembers] SET  MULTI_USER 
GO
ALTER DATABASE [evwMembers] SET DB_CHAINING OFF 
GO
ALTER DATABASE [evwMembers] SET ENCRYPTION ON
GO
ALTER DATABASE [evwMembers] SET QUERY_STORE = ON
GO
ALTER DATABASE [evwMembers] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 7), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 10, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
USE [evwMembers]
GO
/****** Object:  Table [dbo].[MemberFriendsTable]    Script Date: 8/14/2021 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemberFriendsTable](
	[objectId] [int] IDENTITY(1,1) NOT NULL,
	[relParent] [int] NOT NULL,
	[relChild] [int] NOT NULL
)

GO
/****** Object:  Table [dbo].[MembersTable]    Script Date: 8/14/2021 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MembersTable](
	[objectId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](80) NULL,
	[website] [nvarchar](2083) NULL,
	[shortURL] [nvarchar](500) NULL,
	[h1] [nvarchar](max) NULL,
	[h2] [nvarchar](max) NULL,
	[h3] [nvarchar](max) NULL,
	[keywords] [nvarchar](max) NULL,
	[friends] [int] NULL
)

GO
/****** Object:  StoredProcedure [dbo].[InsertMember]    Script Date: 8/14/2021 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sean Luck
-- Description:	Adds a member to the database.
-- =============================================
CREATE PROCEDURE [dbo].[InsertMember]
	@name nvarchar(80), 
	@website nvarchar(2083),
	@shortURL nvarchar(500),
	@h1 nvarchar(MAX),
	@h2 nvarchar(MAX),
	@h3 nvarchar(MAX),
	@keywords nvarchar(MAX),
	@friends int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO dbo.MembersTable ([name], website, shortURL, h1, h2, h3, keywords, friends) VALUES (@name, @website, @shortURL, @h1, @h2, @h3, @keywords, @friends)
END


GO
/****** Object:  StoredProcedure [dbo].[InsertMemberFriend]    Script Date: 8/14/2021 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		Sean Luck
-- Description:	Adds a member friend relationship to the database.
-- Notes: Two relationship links are created. The first if from the Parent to the Child. The second is from the Child to the Parent.
-- This allows for easier traversals when searching the relationships.
-- =============================================
CREATE PROCEDURE [dbo].[InsertMemberFriend]
	@relParent int, 
	@relChild int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO dbo.MemberFriendsTable (relParent,relChild) VALUES (@relParent, @relChild); -- Build Parent ==> Child Relationship.
	INSERT INTO dbo.MemberFriendsTable (relParent,relChild) VALUES (@relChild, @relParent); -- Build Child ==> Parent Relationship.

	-- Update the static friends counters....
	UPDATE MembersTable Set friends = (friends + 1) WHERE objectId = @relParent
	UPDATE MembersTable Set friends = (friends + 1) WHERE objectId = @relChild
END






GO
/****** Object:  StoredProcedure [dbo].[SelectEdges]    Script Date: 8/14/2021 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sean Luck
-- Description:	Selects the graph edges.
-- =============================================
CREATE PROCEDURE [dbo].[SelectEdges]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Select DISTINCT relParent,relChild FROM MemberFriendsTable ORDER BY relParent;
END


GO
/****** Object:  StoredProcedure [dbo].[SelectMemberFriends]    Script Date: 8/14/2021 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sean Luck
-- Description:	Selects the friends of a member from the database.
-- =============================================
CREATE PROCEDURE [dbo].[SelectMemberFriends]
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT F.relChild, M.[name], M.website, M.shortURL, M.friends FROM dbo.MemberFriendsTable AS F
	LEFT JOIN MembersTable as M ON
	M.objectId = F.relChild
	
	WHERE F.relParent = @Id and F.relChild <> @Id  -- additional logic to prevent being freinds with yourself.
	
	ORDER BY M.[name]
END



GO
/****** Object:  StoredProcedure [dbo].[SelectMemberFriendsToAdd]    Script Date: 8/14/2021 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sean Luck
-- Description:	Selects a list of potential friends to add to a member in the database.
-- =============================================
CREATE PROCEDURE [dbo].[SelectMemberFriendsToAdd]
	@Id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT objectId,[name],website,shortURL,friends FROM dbo.MembersTable 
	WHERE objectId <> @Id AND 
	objectId NOT IN(Select relChild As ObjectId from MemberFriendsTable where relParent = @Id) 
	ORDER BY [name]
END


GO
/****** Object:  StoredProcedure [dbo].[SelectMemberProfile]    Script Date: 8/14/2021 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sean Luck
-- Description:	Selects a member profile from the database.
-- =============================================
CREATE PROCEDURE [dbo].[SelectMemberProfile]
@id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT * FROM MembersTable WHERE ObjectId = @id
END



GO
/****** Object:  StoredProcedure [dbo].[SelectMembers]    Script Date: 8/14/2021 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sean Luck
-- Description:	Selects the members from the database.
-- =============================================
CREATE PROCEDURE [dbo].[SelectMembers]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT ObjectId, [Name], Website, ShortURL, Friends FROM MembersTable ORDER BY [Name]
END



GO
/****** Object:  StoredProcedure [dbo].[SelectNodeHits]    Script Date: 8/14/2021 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Sean Luck
-- Description:	Gets all the graph nodes containing a keyword match.
-- =============================================
CREATE PROCEDURE [dbo].[SelectNodeHits]
	@Id int, 
	@Keywords nvarchar(255)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT ObjectId, [Name], Keywords FROM MembersTable WHERE 
	keywords LIKE '%' + @Keywords + '%' AND 
	ObjectId <> @Id And 
	ObjectId NOT IN (SELECT relChild AS ObjectId FROM MemberFriendsTable WHERE relParent = @Id) 
	ORDER BY ObjectId;

END



GO
/****** Object:  StoredProcedure [dbo].[SelectNodes]    Script Date: 8/14/2021 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Sean Luck
-- Description:	Selects the nodes from the database.
-- =============================================
CREATE PROCEDURE [dbo].[SelectNodes]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT ObjectId, [Name], Keywords FROM MembersTable ORDER BY ObjectId;
END




GO
USE [master]
GO
ALTER DATABASE [evwMembers] SET  READ_WRITE 
GO

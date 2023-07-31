
USE [DemoSchool]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 07/31/2023 12:05:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StudentId] [int] IDENTITY(1,1) NOT NULL,
	[StudentName] [varchar](100) NOT NULL,
	[GaurdianName] [varchar](100) NOT NULL,
	[Gender] [int] NOT NULL,
	[DOB] [datetime] NULL,
	[CountryCode] [int] NULL,
	[PhoneNumber] [varchar](10) NULL,
	[Email] [varchar](100) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[Oid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentAddress]    Script Date: 07/31/2023 12:05:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentAddress](
	[StudentAddressId] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NULL,
	[AddressLine1] [varchar](100) NULL,
	[AddressLine2] [varchar](100) NULL,
	[AddressLine3] [varchar](100) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[Country] [varchar](50) NULL,
	[ZipCode] [varchar](10) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[Oid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_StudentAddress] PRIMARY KEY CLUSTERED 
(
	[StudentAddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Student] ADD  CONSTRAINT [DF_Student_CreatedBy]  DEFAULT ((0)) FOR [CreatedBy]
GO
ALTER TABLE [dbo].[Student] ADD  CONSTRAINT [DF_Student_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Student] ADD  CONSTRAINT [DF_Student_Oid]  DEFAULT (newid()) FOR [Oid]
GO
ALTER TABLE [dbo].[StudentAddress] ADD  CONSTRAINT [DF_StudentAddress_CreatedBy]  DEFAULT ((0)) FOR [CreatedBy]
GO
ALTER TABLE [dbo].[StudentAddress] ADD  CONSTRAINT [DF_StudentAddress_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[StudentAddress] ADD  CONSTRAINT [DF_StudentAddress_Oid]  DEFAULT (newid()) FOR [Oid]
GO
ALTER TABLE [dbo].[StudentAddress]  WITH CHECK ADD  CONSTRAINT [FK_StudentAddress_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([StudentId])
GO
ALTER TABLE [dbo].[StudentAddress] CHECK CONSTRAINT [FK_StudentAddress_Student]
GO
/****** Object:  StoredProcedure [dbo].[CreateStudent]    Script Date: 07/31/2023 12:05:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateStudent]
(
	@StudentName varchar(100)
	, @GaurdianName varchar(100)
	, @Gender int
	, @DOB datetime
	, @CountryCode int
	, @PhoneNumber varchar(10)
	, @Email varchar(100)
	, @CreatedBy int
)
AS
BEGIN

	SET NOCOUNT ON;

	INSERT INTO [dbo].[Student]
	(
		[StudentName]
		, [GaurdianName]
		, [Gender]
		, [DOB]
		, [CountryCode]
		, [PhoneNumber]
		, [Email]
		, [CreatedBy]
	)
	VALUES
	(
		@StudentName
		, @GaurdianName
		, @Gender
		, @DOB
		, @CountryCode
		, @PhoneNumber
		, @Email
		, @CreatedBy
	)

END
GO
/****** Object:  StoredProcedure [dbo].[CreateStudentAddress]    Script Date: 07/31/2023 12:05:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateStudentAddress]
(
	@StudentId INT
	, @AddressLine1 varchar(100)
	, @AddressLine2 varchar(100)
	, @AddressLine3 varchar(100)
	, @City varchar(50)
	, @State varchar(50)
	, @Country varchar(50)
	, @ZipCode varchar(10)
	, @CreatedBy INT
)
AS
BEGIN

	SET NOCOUNT ON;
	
	INSERT INTO [dbo].[StudentAddress]
	(
		[StudentId]
		, [AddressLine1]
		, [AddressLine2]
		, [AddressLine3]
		, [City]
		, [State]
		, [Country]
		, [ZipCode]
		, [CreatedBy]
	)
	VALUES
	(
		@StudentId
		, @AddressLine1
		, @AddressLine2
		, @AddressLine3
		, @City
		, @State
		, @Country
		, @ZipCode
		, @CreatedBy
	)

END
GO
/****** Object:  StoredProcedure [dbo].[GetStudentAddressById]    Script Date: 07/31/2023 12:05:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetStudentAddressById] @StudentAddressId int
AS
BEGIN

	SET NOCOUNT ON;

	SELECT	[StudentAddressId]
			, [StudentId]
			, [AddressLine1]
			, [AddressLine2]
			, [AddressLine3]
			, [City]
			, [State]
			, [Country]
			, [ZipCode]
			, [CreatedBy]
			, [CreatedDate]
			, [ModifiedBy]
			, [ModifiedDate]
			, [Oid]

	FROM	[dbo].[StudentAddress] AS SA 
	WHERE	SA.StudentAddressId = @StudentAddressId

END
GO
/****** Object:  StoredProcedure [dbo].[GetStudentAddressByOid]    Script Date: 07/31/2023 12:05:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetStudentAddressByOid] @Oid UNIQUEIDENTIFIER
AS
BEGIN

	SET NOCOUNT ON;

	SELECT	[StudentAddressId]
			, [StudentId]
			, [AddressLine1]
			, [AddressLine2]
			, [AddressLine3]
			, [City]
			, [State]
			, [Country]
			, [ZipCode]
			, [CreatedBy]
			, [CreatedDate]
			, [ModifiedBy]
			, [ModifiedDate]
			, [Oid]

	FROM	[dbo].[StudentAddress] AS SA 
	WHERE	SA.Oid = @Oid

END
GO
/****** Object:  StoredProcedure [dbo].[GetStudentAddressByStudentId]    Script Date: 07/31/2023 12:05:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetStudentAddressByStudentId] @StudentId INT
AS
BEGIN

	SET NOCOUNT ON;

	SELECT	[StudentAddressId]
			, [StudentId]
			, [AddressLine1]
			, [AddressLine2]
			, [AddressLine3]
			, [City]
			, [State]
			, [Country]
			, [ZipCode]
			, [CreatedBy]
			, [CreatedDate]
			, [ModifiedBy]
			, [ModifiedDate]
			, [Oid]

	FROM	[dbo].[StudentAddress] AS SA 
	WHERE	SA.StudentId = @StudentId

END
GO
/****** Object:  StoredProcedure [dbo].[GetStudentById]    Script Date: 07/31/2023 12:05:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetStudentById] @StudentId int
AS
BEGIN

	SET NOCOUNT ON;

	select	[StudentId]
			, [StudentName]
			, [GaurdianName]
			, [Gender]
			, [DOB]
			, [CountryCode]
			, [PhoneNumber]
			, [Email]
			, [CreatedBy]
			, [CreatedDate]
			, [ModifiedBy]
			, [ModifiedDate]
			, [Oid]

	from	[dbo].[Student] AS S
	where	S.StudentId = @StudentId

END
GO
/****** Object:  StoredProcedure [dbo].[GetStudentByOid]    Script Date: 07/31/2023 12:05:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetStudentByOid] @Oid uniqueidentifier
AS
BEGIN

	SET NOCOUNT ON;

	select	[StudentId]
			, [StudentName]
			, [GaurdianName]
			, [Gender]
			, [DOB]
			, [CountryCode]
			, [PhoneNumber]
			, [Email]
			, [CreatedBy]
			, [CreatedDate]
			, [ModifiedBy]
			, [ModifiedDate]
			, [Oid]

	from	[dbo].[Student] AS S
	where	S.Oid = @Oid

END
GO
/****** Object:  StoredProcedure [dbo].[GetStudents]    Script Date: 07/31/2023 12:05:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetStudents]
AS
BEGIN

	SET NOCOUNT ON;

	select	[StudentId]
			, [StudentName]
			, [GaurdianName]
			, [Gender]
			, [DOB]
			, [CountryCode]
			, [PhoneNumber]
			, [Email]
			, [CreatedBy]
			, [CreatedDate]
			, [ModifiedBy]
			, [ModifiedDate]
			, [Oid]

	from	[dbo].[Student] AS S

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateStudentAddressByOid]    Script Date: 07/31/2023 12:05:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateStudentAddressByOid]
(
	@Oid UNIQUEIDENTIFIER
	, @StudentId INT
	, @AddressLine1 varchar(100)
	, @AddressLine2 varchar(100)
	, @AddressLine3 varchar(100)
	, @City varchar(50)
	, @State varchar(50)
	, @Country varchar(50)
	, @ZipCode varchar(10)
	, @ModifiedBy INT
)
AS
BEGIN

	SET NOCOUNT ON;
	
	UPDATE	SA
	SET		SA.[StudentId] = @StudentId
			, SA.[AddressLine1] = @AddressLine1
			, SA.[AddressLine2] = @AddressLine2
			, SA.[AddressLine3] = @AddressLine3
			, SA.[City] = @City
			, SA.[State] = @State
			, SA.[Country] = @Country
			, SA.[ZipCode] = @ZipCode
			, SA.[ModifiedBy] = @ModifiedBy

	FROM	[dbo].[StudentAddress] AS SA
	WHERE	SA.Oid = @Oid

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateStudentByOid]    Script Date: 07/31/2023 12:05:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateStudentByOid]
(
	@Oid uniqueidentifier
	, @StudentName varchar(100)
	, @GaurdianName varchar(100)
	, @Gender int
	, @DOB datetime
	, @CountryCode int
	, @PhoneNumber varchar(10)
	, @Email varchar(100)
	, @ModifiedBy int
)
AS
BEGIN

	SET NOCOUNT ON;

	update	S

	set		S.StudentName = @StudentName
			, S.GaurdianName = @GaurdianName
			, S.Gender = @Gender
			, S.DOB = @DOB
			, S.CountryCode = @CountryCode
			, S.PhoneNumber = @PhoneNumber
			, S.Email = @Email
			, S.ModifiedBy = @ModifiedBy
			, S.ModifiedDate = GETDATE()

	from	[dbo].[Student] as S
	where	S.Oid = @Oid

END
GO
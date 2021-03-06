USE [master]
GO
/****** Object:  Database [UniversityCourseResultManagementSystem]    Script Date: 5/19/2016 4:22:46 AM ******/
CREATE DATABASE [UniversityCourseResultManagementSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'UniversityCourseResultManagementSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\UniversityCourseResultManagementSystem.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'UniversityCourseResultManagementSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\UniversityCourseResultManagementSystem_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [UniversityCourseResultManagementSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET  MULTI_USER 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [UniversityCourseResultManagementSystem]
GO
/****** Object:  StoredProcedure [dbo].[ProcedureViewCourseStatistics]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ProcedureViewCourseStatistics] AS
SELECT d.Id as DepartmentId,COALESCE(c.Code,'Not Assigned yet') AS Code,
COALESCE(c.Name,'Not Assigned yet') AS Name,COALESCE(s.SemesterName,'Not Assigned yet') as Semester,
COALESCE(t.Name,'Not Assigned yet')  as Teacher FROM  Departments d  
LEFT OUTER JOIN Courses  c  ON d.Id=c.DepartmentId 
LEFT OUTER JOIN Semesters s ON c.SemesterId=s.Id  
LEFT OUTER JOIN CourseAssignToTeacher Ct  ON (c.Id=Ct.CourseId AND Ct.IsActive=1) 
LEFT OUTER JOIN Teachers t ON t.Id=Ct.TeacherId ORDER BY c.Code

GO
/****** Object:  StoredProcedure [dbo].[spGetCoursesTakenByaStudent]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetCoursesTakenByaStudent]
  @StudentId int
  AS
  BEGIN
  SELECT c.Id,c.Code,c.Name,c.Credit,c.Description,c.DepartmentId,c.SemesterId FROM Courses c INNER JOIN EnrollCourse r ON (c.Id=r.CourseId AND r.StudentId=@StudentId AND r.IsStudentActive=1)
  END

GO
/****** Object:  StoredProcedure [dbo].[spGetStudentInformationById]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [dbo].[spGetStudentInformationById]
  @Id int
  AS
  BEGIN

  SELECT s.Id,s.RegistrationNo,s.Name,s.Email,s.ContactNo,s.RegistrationDate,s.Address,d.Name as Department FROM Students s INNER JOIN Departments d ON s.DepartmentId=d.Id AND s.Id=@Id
  END

GO
/****** Object:  StoredProcedure [dbo].[spGetStudentResult]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetStudentResult]
@studentId int
AS
BEGIN
SELECT en.StudentId, c.Code,c.Name,COALESCE(r.Grade,'Not Graded yet') as Grade FROM StudentResult r RIGHT OUTER JOIN ( SELECT e.Id,e.StudentId,e.CourseId FROM EnrollCourse e WHERE e.StudentId=@studentId AND e.IsStudentActive=1) en ON r.CourseId=en.CourseId AND r.StudentId=en.StudentId INNER JOIN Courses c ON en.CourseId=c.Id
END


GO
/****** Object:  Table [dbo].[AllocateClassRoom]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AllocateClassRoom](
	[AllocatedRoomId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[RoomId] [int] NOT NULL,
	[DayId] [int] NOT NULL,
	[FromTime] [varchar](50) NOT NULL,
	[ToTime] [varchar](50) NOT NULL,
	[AllocateStatus] [varchar](50) NULL,
 CONSTRAINT [PK_AllocateClassRoom] PRIMARY KEY CLUSTERED 
(
	[AllocatedRoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CourseAssignToTeacher]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseAssignToTeacher](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[TeacherId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_CourseAssignToTeacher] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Courses]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Courses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[Credit] [float] NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[SemesterId] [int] NOT NULL,
 CONSTRAINT [PK_Courses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Days]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Days](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Days] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Departments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Code] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Designations]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Designations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DesignationName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Designations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EnrollCourse]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EnrollCourse](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[EnrollDate] [date] NOT NULL,
	[IsStudentActive] [bit] NULL,
 CONSTRAINT [PK_EnrollCourse] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Rooms](
	[RoomId] [int] IDENTITY(1,1) NOT NULL,
	[RoomNumber] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Semesters]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Semesters](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SemesterName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Semesters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StudentResult]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StudentResult](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
	[Grade] [varchar](50) NOT NULL,
 CONSTRAINT [PK_StudentResult] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Students]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Students](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RegistrationNo] [varchar](50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[ContactNo] [varchar](50) NOT NULL,
	[RegistrationDate] [date] NOT NULL,
	[Address] [varchar](50) NOT NULL,
	[DepartmentId] [int] NOT NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Teachers]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Teachers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Address] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[ContactNo] [varchar](50) NOT NULL,
	[DesignationId] [int] NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[CreditToBeTaken] [float] NOT NULL,
	[CreditTaken] [float] NOT NULL,
 CONSTRAINT [PK_Teachers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[CourseWithRoom]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[CourseWithRoom] AS
SELECT        a.DepartmentId, a.CourseId, c.Code, c.Name, a.RoomId, r.RoomNumber, d.Name Day, a.FromTime, a.ToTime, a.AllocateStatus
FROM            dbo.AllocateClassRoom AS a INNER JOIN
                         dbo.Courses AS c ON a.CourseId = c.Id INNER JOIN
                         dbo.Rooms AS r ON a.RoomId = r.RoomId INNER JOIN
						 dbo.Days AS d ON a.DayId=d.Id




GO
/****** Object:  View [dbo].[ClassRoomSchedule]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ClassRoomSchedule] AS
SELECT        DepartmentId, CourseId, Code, Name, CONCAT('R. No: ', RoomNumber, ', ', Day, ', ', Fromtime, ' - ', Totime, '<br/>')  AS ScheduleInfo, AllocateStatus
FROM            dbo.CourseWithRoom


GO
/****** Object:  View [dbo].[ScheduleList]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[ScheduleList] AS
SELECT        t1.CourseId, Units = REPLACE
                             ((SELECT        ScheduleInfo AS [data()]
                                 FROM            ClassRoomSchedule t2
                                 WHERE        t2.CourseId = t1.CourseId AND t2.AllocateStatus='Allocated'
                                 ORDER BY ScheduleInfo FOR XML PATH('')), ' ', '')
FROM            ClassRoomSchedule t1

GROUP BY CourseId;


GO
/****** Object:  View [dbo].[CourseSchedule]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[CourseSchedule] AS
SELECT        C.DepartmentId, S.CourseId, C.Name, C.Code, S.Units
FROM            dbo.ScheduleList AS S INNER JOIN
                         dbo.Courses AS C ON S.CourseId = C.Id


GO
/****** Object:  View [dbo].[ViewCourseAllocation]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ViewCourseAllocation] AS
SELECT DISTINCT C.DepartmentId, C.CourseId, C.Name, C.Code, C.Units, A.AllocateStatus
FROM            dbo.CourseSchedule AS C INNER JOIN
                         dbo.AllocateClassRoom AS A ON C.CourseId = A.CourseId AND C.DepartmentId = A.DepartmentId
						  WHERE A.AllocateStatus='Allocated'


GO
/****** Object:  View [dbo].[FinalSchedule]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[FinalSchedule] AS
SELECT        C.DepartmentId, C.ID, C.Name, C.Code, V.Units AS Schedule, V.AllocateStatus
FROM            dbo.Courses AS C LEFT OUTER JOIN
                         dbo.ViewCourseAllocation AS V ON C.ID = V.CourseId

GO
/****** Object:  View [dbo].[DayName]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DayName] AS
SELECT d.Name FROM Days d JOIN AllocateClassRoom a ON d.Id=a.DayId

GO
/****** Object:  View [dbo].[viewCourseEnrollCourse]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[viewCourseEnrollCourse]
AS
SELECT c.Id,c.Name,c.Code,c.Credit,c.Description,c.DepartmentId,c.SemesterId 
FROM Courses c INNER JOIN EnrollCourse ec
ON c.Id = ec.CourseId

GO
/****** Object:  View [dbo].[viewStudentResultSave]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create View [dbo].[viewStudentResultSave]
AS

SELECT        s.Id, s.RegistrationNo, s.Name AS StudentName, s.Email,c.Id as CourseId,
             d.Name AS DepartmentName, c.Name AS CourseName
FROM            dbo.Students AS s INNER JOIN
                         dbo.Departments AS d ON s.DepartmentId = d.Id INNER JOIN
                         dbo.EnrollCourse AS ec ON s.Id = ec.StudentId INNER JOIN
                         dbo.Courses AS c ON ec.CourseId = c.Id

GO
/****** Object:  View [dbo].[viewStudentsDepartments]    Script Date: 5/19/2016 4:22:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[viewStudentsDepartments]
AS
SELECT        s.Id, s.RegistrationNo, s.Name AS StudentName, s.Email, s.ContactNo, s.RegistrationDate, s.Address, s.DepartmentId, d.Name AS DepartmentName
FROM            dbo.Students AS s INNER JOIN
                         dbo.Departments AS d ON s.DepartmentId = d.Id

GO
ALTER TABLE [dbo].[AllocateClassRoom]  WITH CHECK ADD  CONSTRAINT [FK_AllocateClassRoom_Courses] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Courses] ([Id])
GO
ALTER TABLE [dbo].[AllocateClassRoom] CHECK CONSTRAINT [FK_AllocateClassRoom_Courses]
GO
ALTER TABLE [dbo].[AllocateClassRoom]  WITH CHECK ADD  CONSTRAINT [FK_AllocateClassRoom_Days] FOREIGN KEY([DayId])
REFERENCES [dbo].[Days] ([Id])
GO
ALTER TABLE [dbo].[AllocateClassRoom] CHECK CONSTRAINT [FK_AllocateClassRoom_Days]
GO
ALTER TABLE [dbo].[AllocateClassRoom]  WITH CHECK ADD  CONSTRAINT [FK_AllocateClassRoom_Departments] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([Id])
GO
ALTER TABLE [dbo].[AllocateClassRoom] CHECK CONSTRAINT [FK_AllocateClassRoom_Departments]
GO
USE [master]
GO
ALTER DATABASE [UniversityCourseResultManagementSystem] SET  READ_WRITE 
GO

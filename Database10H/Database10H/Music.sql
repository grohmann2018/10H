CREATE TABLE [dbo].[Music]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [title] NVARCHAR(50) NULL, 
    [artist] NVARCHAR(50) NULL, 
    [album] NVARCHAR(50) NULL, 
    [release_date] DATETIME NULL, 
    [type] NVARCHAR(50) NULL, 
    [price] FLOAT NULL
)

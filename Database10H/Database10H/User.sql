CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [first_name] NVARCHAR(50) NULL, 
    [last_name] NVARCHAR(50) NULL, 
    [email] NVARCHAR(50) NULL, 
    [password] NVARCHAR(MAX) NULL, 
    [adress] NTEXT NULL
)

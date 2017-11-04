﻿CREATE TABLE [dbo].[Task]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWSEQUENTIALID(), 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [StatusId] UNIQUEIDENTIFIER NOT NULL,
	FOREIGN KEY (StatusId) REFERENCES TaskStatus(Id)
)

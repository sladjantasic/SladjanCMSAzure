CREATE TABLE Devices (
    [Id]          INT            NOT NULL PRIMARY KEY,
    [Name]        NVARCHAR (30)  NOT NULL,
    [Description] NVARCHAR (300) NULL,
    [IsConnected] BIT            NOT NULL,
    [UserId]      NVARCHAR (450) NULL
)
GO
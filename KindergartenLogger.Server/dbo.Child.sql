CREATE TABLE [dbo].[Child] (
    [ChildID]        INT         NOT NULL,
    [ChildFirstName] NCHAR (100) NOT NULL,
    [ChildLastName]  NCHAR (20)  NOT NULL,
    [ChildOIB]       INT         NOT NULL,
    [ChildGroup]     NCHAR (20)  NULL,
    PRIMARY KEY CLUSTERED ([ChildID] ASC)
);


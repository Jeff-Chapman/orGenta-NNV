CREATE TABLE Sysvars(
	ParameterName VARCHAR(50) NOT NULL COLLATE NOCASE,
	ParameterValue VARCHAR(255) NULL) 
GO

CREATE TABLE Categories(
	CategoryID INTEGER PRIMARY KEY NOT NULL,
	Category VARCHAR(30) NOT NULL COLLATE NOCASE,
	SortSeq SHORT NULL,
	ParentID SHORT NULL,
	isFrozen BIT NULL,
	isDeleted BIT NULL,
	ManualAssignOnly BIT NULL)
GO

CREATE TABLE Items(
	ItemID INTEGER PRIMARY KEY NOT NULL,
	ItemDesc VARCHAR(255) NOT NULL COLLATE NOCASE,
	hasNote BIT NULL,
	ItemSeq SHORT NULL,
	isDeleted BIT NULL,
	DateCreated DATETIME DEFAULT CURRENT_TIMESTAMP)
GO

CREATE TABLE Notes(
	NoteID INTEGER PRIMARY KEY NOT NULL,
	ItemID INTEGER NULL,
	NoteValue LONGTEXT NULL COLLATE NOCASE)
GO


CREATE TABLE Rels(
	RelID INTEGER PRIMARY KEY NOT NULL,
	CategoryID SHORT NULL,
	ItemID SHORT NULL,
	isDeleted BIT NULL)
GO



CREATE VIEW vw_Get_Categories
AS
SELECT        CategoryID, Category, SortSeq, ParentID, isFrozen, isDeleted, ManualAssignOnly
FROM            Categories
WHERE        (isDeleted = 0) OR
                         (isDeleted IS NULL)

GO


CREATE VIEW vw_Get_Categories_v2
AS
SELECT C.CategoryID, C.Category, C.SortSeq, C.ParentID, C.isFrozen, C.isDeleted, C.ManualAssignOnly, CP.ParentID AS pPid
FROM   (Categories C LEFT OUTER JOIN Categories CP 
        ON CP.CategoryID = C.ParentID)
WHERE (C.isDeleted = 0) OR (C.isDeleted IS NULL)

GO


CREATE VIEW vw_AssignCount AS
SELECT        COUNT(AA.CategoryID) AS AssignCount, AA.ItemID
FROM            Rels AS AA INNER JOIN
                Items ON AA.ItemID = Items.ItemID
WHERE        (AA.isDeleted = 0) AND (Items.isDeleted = 0)
GROUP BY AA.ItemID

GO


CREATE VIEW vw_Highest_Rel_Duplicate AS
SELECT        MAX(RelID) AS MaxRel, CategoryID, ItemID, COUNT(*) AS howMany
FROM            Rels
WHERE        (isDeleted = 0)
GROUP BY CategoryID, ItemID
HAVING        (COUNT(*) > 1)

GO


CREATE VIEW vw_Get_Solo_Items
AS
SELECT        ItemID, ItemDesc
FROM            Items
WHERE        (isDeleted = 0)

GO


CREATE VIEW vw_Get_Items_Distinct
AS
SELECT        hasNote, ItemDesc, DateCreated, ItemID
FROM            Items AS I
WHERE        (isDeleted = 0)

GO


CREATE VIEW vw_Get_Items
AS
SELECT        I.hasNote, I.ItemDesc, I.DateCreated, I.ItemID, R.CategoryID
FROM            Rels AS R INNER JOIN
                         Items AS I ON R.ItemID = I.ItemID
WHERE        (R.isDeleted = 0) AND (I.isDeleted = 0)

GO

CREATE VIEW vw_GetCatsForItem
AS
SELECT        R.ItemID, C.Category, C.CategoryID
FROM            Rels AS R INNER JOIN
                         Categories AS C ON R.CategoryID = C.CategoryID
WHERE        (R.isDeleted = 0) AND (C.isDeleted = 0)

GO

INSERT INTO Sysvars (ParameterName, ParameterValue) VALUES ('Database Version', '2.2')

GO

INSERT INTO Categories (CategoryID, Category, SortSeq, ParentID, isFrozen, isDeleted, ManualAssignOnly) VALUES (1, 'Main', 1, 0, 0, 0, 1);
INSERT INTO Categories (CategoryID, Category, SortSeq, ParentID, isFrozen, isDeleted, ManualAssignOnly) VALUES (2, 'Unassigned', NULL, 1, 0, 0, 0);
INSERT INTO Categories (CategoryID, Category, SortSeq, ParentID, isFrozen, isDeleted, ManualAssignOnly) VALUES (3, 'TrashCan', 9999, 1, 0, 0, 0)

GO


CREATE TABLE Sysvars(
	ParameterName VARCHAR(50) NOT NULL,
	ParameterValue VARCHAR(255) NULL) 

CREATE TABLE Categories(
	CategoryID COUNTER NOT NULL,
	Category VARCHAR(30) NOT NULL,
	SortSeq SHORT NULL,
	ParentID SHORT NULL,
	isFrozen BIT NULL,
	isDeleted BIT NULL,
	ManualAssignOnly BIT NULL)
ALTER TABLE Categories ADD PRIMARY KEY (CategoryID)

CREATE TABLE Items(
	ItemID COUNTER NOT NULL,
	ItemDesc VARCHAR(255) NOT NULL,
	hasNote BIT NULL,
	ItemSeq SHORT NULL,
	isDeleted BIT NULL,
	DateCreated DATETIME NULL)
ALTER TABLE Items ADD PRIMARY KEY (ItemID)
ALTER TABLE Items ALTER COLUMN DateCreated SET DEFAULT NOW


CREATE TABLE Notes(
	NoteID COUNTER NOT NULL,
	ItemID SHORT NULL,
	NoteValue LONGTEXT NULL)
ALTER TABLE Notes ADD PRIMARY KEY (NoteID)


CREATE TABLE Rels(
	RelID COUNTER NOT NULL,
	CategoryID SHORT NULL,
	ItemID SHORT NULL,
	isDeleted BIT NULL)
ALTER TABLE Rels ADD PRIMARY KEY (RelID)


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
FROM   (Categories CP RIGHT OUTER JOIN
             Categories C ON CP.CategoryID = C.ParentID)
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


INSERT INTO Sysvars (ParameterName, ParameterValue) VALUES ('Database Version', '2.1')

GO

SET IDENTITY_INSERT Categories ON 

INSERT INTO Categories (CategoryID, Category, SortSeq, ParentID, isFrozen, isDeleted, ManualAssignOnly) VALUES (1, 'Main', 1, 0, 0, 0, 1)
INSERT INTO Categories (CategoryID, Category, SortSeq, ParentID, isFrozen, isDeleted, ManualAssignOnly) VALUES (2, 'Unassigned', NULL, 1, 0, 0, 0)
INSERT INTO Categories (CategoryID, Category, SortSeq, ParentID, isFrozen, isDeleted, ManualAssignOnly) VALUES (3, 'TrashCan', 9999, 1, 0, 0, 0)

SET IDENTITY_INSERT Categories OFF

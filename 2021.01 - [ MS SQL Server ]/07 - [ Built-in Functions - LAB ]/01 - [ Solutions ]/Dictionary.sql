USE DictionaryENBG

CREATE TABLE Words
(
	ID		INT PRIMARY KEY,
	[Name]	VARCHAR(50) UNIQUE NOT NULL
)

CREATE TABLE WordsTranslate
(
	ID			INT IDENTITY PRIMARY KEY,
	[Translate]	NVARCHAR(200) NOT NULL,
	WordID		INT NOT NULL FOREIGN KEY REFERENCES Words (ID)
)

CREATE TABLE Phrases
(
	ID		INT PRIMARY KEY,
	[Name]	VARCHAR(200) NOT NULL,
	WordID	INT NOT NULL FOREIGN KEY REFERENCES Words (ID)
)

CREATE TABLE PhrasesTranslate
(
	ID			INT IDENTITY PRIMARY KEY,
	[Translate] NVARCHAR(300) NOT NULL,
	PhraseID	INT NOT NULL FOREIGN KEY REFERENCES Phrases (ID)
)

CREATE TABLE Sentances
(
	ID		INT PRIMARY KEY,
	[Name]	VARCHAR(300) NOT NULL,
	WordID	INT NOT NULL FOREIGN KEY REFERENCES Words (ID)
)

CREATE TABLE SentancesTranslate
(
	ID			INT IDENTITY PRIMARY KEY,
	[Translate] NVARCHAR(300) NOT NULL,
	SentanceID	INT NOT NULL FOREIGN KEY REFERENCES Sentances (ID)
)

INSERT INTO Words
(ID ,[Name])
VALUES
(1, 'Order'),
(2, 'However'),
(3, 'Addition')

INSERT INTO WordsTranslate
([Translate], WordID)
VALUES
('�������', 1),
('���', 1),
('�������', 1),
('�����', 2),
('������� ����', 2),
('����������', 3)

INSERT INTO Phrases
(ID, [Name], WordID)
VALUES
(1, 'In order to [...]', 1),
(2, 'However you are free to [...]', 2),
(3, 'In addition to [...]', 3),
(4, '�ddition and subtraction', 3)

INSERT INTO PhrasesTranslate
([Translate], PhraseID)
VALUES
('�� ��', 1),
('O���� ��� ��������', 2),
('� ���������� ���', 3),
('�������� � ���������', 3)

INSERT INTO Sentances
(ID, [Name], WordID)
VALUES
(1, 'You have to [...] in order to [...]', 1),
(2, 'However you are free to add more methods', 2),
(3, 'This method should in addition increase the size of the structure', 3)

INSERT INTO SentancesTranslate
([Translate], SentanceID)
VALUES
('������ �� [...], �� �� [...]', 1),
('��, ������ �� �������� ��� ������', 2),
('���� ����� ������ ������������ �� ������� ������� �� �����������', 3)

SELECT w.[Name], wt.[Translate], p.[Name], pt.[Translate], s.[Name], st.[Translate]
FROM Words w
INNER JOIN WordsTranslate wt ON wt.WordID = w.ID
INNER JOIN Phrases p ON p.WordID = w.ID
INNER JOIN PhrasesTranslate pt ON pt.PhraseID = p.ID
INNER JOIN Sentances s ON s.WordID = w.ID
INNER JOIN SentancesTranslate st ON st.SentanceID = s.ID
WHERE w.[Name] = 'Addition'

--DROP TABLE Phrases
--DROP TABLE PhrasesTranslate
--DROP TABLE Sentances
--DROP TABLE SentancesTranslate
--DROP TABLE Words
--DROP TABLE WordsTranslate

CREATE TABLE Photos
(
	ID INT PRIMARY KEY IDENTITY(1,1),
	TimeMaking DATETIME,
	TimeLoad DATETIME NOT NULL,
	Name NVARCHAR(100) NOT NULL,
	CommentUser NVARCHAR(3000),
	Model NVARCHAR(200),
	GPSlat REAL,
	GPSlon REAL,
	AlbumID INT,
	UserID INT NOT NULL,
	AlbumQueue INT,
	FilePhoto VARBINARY(max) NOT NULL,
	GUID NVARCHAR(100)
)

CREATE TABLE Albums
(
	ID INT PRIMARY KEY IDENTITY(1,1),
	UserID INT  NOT NULL,
	Name NVARCHAR(100) NOT NULL,
	Description NVARCHAR(3000),
	GUID NVARCHAR(100)
)

CREATE TABLE Users
(
	ID INT PRIMARY KEY IDENTITY(1,1),
	Email NVARCHAR(100) NOT NULL UNIQUE,
	Password NCHAR(100) NOT NULL,
	Salt NVARCHAR(100) NOT NULL,
	FirstName VARBINARY(100),
	SecondName VARBINARY(100), 
	ThirdName VARBINARY(100),
	BirthDate VARBINARY(100),
	Description VARBINARY(3000),
	Role VARBINARY(100),
	CrKey VARBINARY(100) NOT NULL,
	CrIV VARBINARY(100) NOT NULL
)

CREATE TABLE Comments
(
	ID INT PRIMARY KEY IDENTITY(1,1),
	UserID INT NULL,
	PhotoID INT NOT NULL,
	Text NVARCHAR(3000) NOT NULL
)

ALTER TABLE Albums ADD CONSTRAINT AlbumsUserID FOREIGN KEY (UserID) REFERENCES Users(ID); 
ALTER TABLE Comments ADD CONSTRAINT CommUserID FOREIGN KEY (UserID) REFERENCES Users(ID); 
ALTER TABLE Comments ADD CONSTRAINT CommPhotoID FOREIGN KEY  (PhotoID)  REFERENCES Photos(ID); 
ALTER TABLE Photos ADD CONSTRAINT PhotosAlbumIS FOREIGN KEY (AlbumID) REFERENCES Albums(ID); 
ALTER TABLE Photos ADD CONSTRAINT PhotosUserIDFK FOREIGN KEY (UserID) REFERENCES Users(ID); 

CREATE TABLE Profile (
	Id int identity(1,1) Not NULL Primary key,
    Name VARCHAR(50) NOT NULL, 
    Description VARCHAR(255) NOT NULL,
    Active Bit not null,
	CreateDate datetime NOT NULL,
	UpdateDate datetime NULL,
)
Go


create TABLE Users (
Id int identity(1,1) Not NULL Primary key,
Name varchar(200) NOT Null,
Cpf varchar(11) NOT NULL,
Birthday datetime NOT NUll,
Phone varchar(20) NOT NULL,
Email varchar(100) NOT null,
Password varchar(100) NOT NULL,
Active bit not null,
ProfileId int not null,
CreateDate datetime NOT NULL,
UpdateDate datetime NULL,

)
GO

ALTER TABLE [dbo].[Users] WITH CHECK ADD CONSTRAINT FK_User_Profile FOREIGN KEY(ProfileId)
REFERENCES Profile (id)
GO

create table Address(
Id int identity(1,1)NOT NULL Primary key,
Street varchar (200) Not NULL,
Number varchar(20) NOT null,
District varchar(200) not null,
City varchar(200) not null,
State varchar(100) not null,
PostalCode varchar(8) not null,
AddressName varchar(200) not null,
UserId int NOT NULL,
CreateDate datetime NOT NULL,
UpdateDate datetime NULL) 

ALTER TABLE Address  WITH CHECK ADD CONSTRAINT FK_Address_User FOREIGN KEY(UserId)
REFERENCES Users (id)
Go


CREATE TABLE Permission (
	Id int identity(1,1) Not NULL Primary key,
    Name VARCHAR(50) NOT NULL,
    Description VARCHAR(255) NULL, 
    Active Bit not null,
	CreateDate datetime NOT NULL,
	UpdateDate datetime NULL,
)
Go


CREATE TABLE ProfilePermission (
	Id int identity(1,1) Not NULL Primary key,
	ProfileId int not null,
	PermissionId int not null, 
	CreateDate datetime NOT NULL,
	UpdateDate datetime NULL,
)

GO

ALTER TABLE ProfilePermission  WITH CHECK ADD CONSTRAINT FK_ProfilePermission_Profile FOREIGN KEY([ProfileId])
REFERENCES Profile (Id)
Go

ALTER TABLE ProfilePermission  WITH CHECK ADD CONSTRAINT FK_ProfilePermission_Pemission FOREIGN KEY([PermissionId])
REFERENCES Permission (Id)
Go




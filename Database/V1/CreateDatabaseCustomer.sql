
CREATE TABLE Profile (
	Id int identity(1,1) Not NULL Primary key,
    Name VARCHAR(50) NOT NULL, 
    Description VARCHAR(255) NOT NULL,
    Active Bit,
	Create_Date datetime NOT NULL,
	Update_Date datetime NULL,
)
Go


create TABLE User_ (
Id int identity(1,1) Not NULL Primary key,
Name varchar(200) NOT Null,
Cpf varchar(11) NOT NULL,
Date_Birth datetime NOT NUll,
Phone varchar(20) NOT NULL,
Email varchar(100) NOT null,
Password varchar(100) NOT NULL,
Active varchar (50) not null,
Create_Date datetime NOT NULL,
Update_Date datetime NULL,
Id_Profile int null
)
GO

ALTER TABLE [dbo].[User_] WITH CHECK ADD CONSTRAINT FK_User_Profile FOREIGN KEY(Id_Profile)
REFERENCES Profile (id)
GO

create table Address(
Id int identity(1,1)NOT NULL Primary key,
Street varchar (200) Not NULL,
Number varchar(20) NOT null,
District varchar(200) not null,
City varchar(200) not null,
State_ varchar(100) not null,
Postal_Code varchar(8) not null,
Address_Name varchar(200) not null,
Id_User int NOT NULL,
Create_Date datetime NOT NULL,
Update_Date datetime NULL) 

ALTER TABLE Address  WITH CHECK ADD CONSTRAINT FK_Address_User FOREIGN KEY(Id_User)
REFERENCES User_ (id)
Go


CREATE TABLE Permission (
	Id int identity(1,1) Not NULL Primary key,
    Name VARCHAR(50) NOT NULL,
    Description VARCHAR(255) NULL, 
    Active Bit,
	Create_Date datetime NOT NULL,
	Update_Date datetime NULL,
)
Go


CREATE TABLE Profile_Permission (
	Id int identity(1,1) Not NULL Primary key,
	Id_Profile int not null,
	Id_Permission int not null, 
)

GO

ALTER TABLE Profile_Permission  WITH CHECK ADD CONSTRAINT FK_ProfilePermission_Profile FOREIGN KEY([Id_Profile])
REFERENCES Profile (Id)
Go

ALTER TABLE Profile_Permission  WITH CHECK ADD CONSTRAINT FK_ProfilePermission_Pemission FOREIGN KEY([Id_Permission])
REFERENCES Permission (Id)
Go




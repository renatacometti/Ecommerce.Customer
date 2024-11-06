create table Usuario(
Id int identity(1,1) Not NULL Primary key,
Nome varchar(200) NOT Null,
Cpf varchar(11) NOT NULL,
Data_Nascimento datetime NOT NUll,
Telefone varchar(20) NOT NULL,
Email varchar(100) NOT null,
Senha varchar(100) NOT NULL,
Ativo varchar (50) not null,
Create_Date datetime NOT NULL,
Update_Date datetime NOT NULL,
Id_Perfil int null)
GO

ALTER TABLE [dbo].[Usuario] WITH CHECK ADD CONSTRAINT FK_Usuario_Perfil FOREIGN KEY(Id_Perfil)
REFERENCES Perfil (id)
GO

create table Endereco(
Id int identity(1,1)NOT NULL Primary key,
Rua varchar (200) Not NULL,
Numero varchar(20) NOT null,
Bairro varchar(200) not null,
Cidade varchar(200) not null,
Uf varchar(100) not null,
Cep varchar(8) not null,
Nome_Endereco varchar(200) not null,
Id_Cliente int NOT NULL,
Create_Date datetime NOT NULL,
Update_Date datetime NOT NULL) 

ALTER TABLE Endereco  WITH CHECK ADD CONSTRAINT FK_Endereco_Cliente FOREIGN KEY(Id_Cliente)
REFERENCES Cliente (id)
Go


CREATE TABLE Permissao (
	Id int identity(1,1) Not NULL Primary key,
    Nome VARCHAR(50) NOT NULL,
    Descricao VARCHAR(255) NULL, 
    Ativo Bit
)
Go

CREATE TABLE Perfil (
	Id int identity(1,1) Not NULL Primary key,
    Nome VARCHAR(50) NOT NULL, 
    Descricao VARCHAR(255) NULL,
    Ativo Bit
)
Go

CREATE TABLE Perfil_Permissao (
	Id int identity(1,1) Not NULL Primary key,
	Id_Perfil int not null,
	Id_Permissao int not null, 
)

GO

ALTER TABLE Perfil_Permissao  WITH CHECK ADD CONSTRAINT FK_PerfilPermissao_Perfil FOREIGN KEY([Id_Perfil])
REFERENCES Perfil (Id)
Go

ALTER TABLE Perfil_Permissao  WITH CHECK ADD CONSTRAINT FK_PerfilPermissao_Pemissao FOREIGN KEY([Id_Permissao])
REFERENCES Permissao (Id)
Go




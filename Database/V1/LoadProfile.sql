/****** Script do comando SelectTopNRows de SSMS  ******/

insert into [dbo].[Profile]([name], [Description], [Active], [Create_Date],[Update_Date])
values
('administrador', 'Administrador Sistema', 1, GETDATE(), null),
('cliente', 'Cliente e-comerce', 1, GETDATE(), null);

/****** Script do comando SelectTopNRows de SSMS  ******/

insert into [dbo].[Profile]([name], [Description], [Active], [CreateDate],[UpdateDate])
values
('administrador', 'Administrador Sistema', 1, GETDATE(), null),
('cliente', 'Cliente e-comerce', 1, GETDATE(), null);

/****** Script do comando SelectTopNRows de SSMS  ******/

insert into [dbo].[Permission]([name], [Description], [Active], [Create_Date], [Update_Date])
values
('listar_perfil', 'Listar Perfil', 1, GetDate(), null),
('salvar_perfil', 'Salvar Perfil', 1, GetDate(), null),
('alterar_perfil', 'Alterar Perfil', 1, GetDate(), null),
('remover_perfil', 'Remover Perfil', 1, GetDate(), null),
('vizualizar_perfil', 'Vizualizar Perfil', 1, GetDate(), null);

/****** Script do comando SelectTopNRows de SSMS  ******/

insert into [dbo].[Permissao]([nome], [Descricao], [Ativo])
values
('listar_perfil', 'Listar Perfil', 1),
('salvar_perfil', 'Salvar Perfil', 1),
('alterar_perfil', 'Alterar Perfil', 1),
('remover_perfil', 'Remover Perfil', 1),
('vizualizar_perfil', 'Vizualizar Perfil', 1);

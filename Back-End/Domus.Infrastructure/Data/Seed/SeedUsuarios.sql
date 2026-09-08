-- =========================================================
-- Popula 3 usuários de teste: Locador, Locatario, Administrador
-- Senha para todos: Senha123!
-- =========================================================

DECLARE @SenhaHashPadrao NVARCHAR(255) = '$2b$11$KU29fjTSnogr5EFeg3rwDOGhXcnhnoOaniXWRj8v3soMz9QTHh.o2';

DECLARE @Agora DATETIME2 = GETUTCDATE();

DECLARE @LocadorId UNIQUEIDENTIFIER = NEWID();
DECLARE @LocatarioId UNIQUEIDENTIFIER = NEWID();
DECLARE @AdminId UNIQUEIDENTIFIER = NEWID();

-- 1) Garante que as 3 funções existem na tabela Funcoes
IF NOT EXISTS (SELECT 1 FROM [Funcoes] WHERE [Nome] = 'Locador')
    INSERT INTO [Funcoes] ([Funcao_ID], [Nome]) VALUES (NEWID(), 'Locador');

IF NOT EXISTS (SELECT 1 FROM [Funcoes] WHERE [Nome] = 'Locatario')
    INSERT INTO [Funcoes] ([Funcao_ID], [Nome]) VALUES (NEWID(), 'Locatario');

IF NOT EXISTS (SELECT 1 FROM [Funcoes] WHERE [Nome] = 'Administrador')
    INSERT INTO [Funcoes] ([Funcao_ID], [Nome]) VALUES (NEWID(), 'Administrador');

-- 2) Inserir os usuários (ativos e com e-mail já confirmado)
INSERT INTO [Usuarios]
    ([Usuario_ID], [Endereco_ID], [Nome], [Email], [CPF], [Celular],
     [Ativo], [SenhaHash], [TokenConfirmaEmail], [TokenEmailExpire],
     [EmailAConfirmar], [EmailConfirmado], [CriadoEm], [ExcluidoEm])
VALUES
    (@LocadorId,   NULL, N'Locador',       N'locador@domus.com',       NULL, NULL,
     1, @SenhaHashPadrao, '00000000-0000-0000-0000-000000000000', '0001-01-01',
     N'locador@domus.com',       1, @Agora, NULL),

    (@LocatarioId, NULL, N'Locatario',     N'locatario@domus.com',     NULL, NULL,
     1, @SenhaHashPadrao, '00000000-0000-0000-0000-000000000000', '0001-01-01',
     N'locatario@domus.com',     1, @Agora, NULL),

    (@AdminId,     NULL, N'Administrador', N'admin@domus.com',         NULL, NULL,
     1, @SenhaHashPadrao, '00000000-0000-0000-0000-000000000000', '0001-01-01',
     N'admin@domus.com',         1, @Agora, NULL);

-- 3) Vincular cada usuário à função de mesmo nome
INSERT INTO [UsuarioFuncoes] ([UsuarioFuncao_ID], [Funcao_ID], [Usuario_ID], [CriadoEm])
SELECT NEWID(), f.[Funcao_ID], @LocadorId, @Agora
FROM [Funcoes] f WHERE f.[Nome] = 'Locador'
UNION ALL
SELECT NEWID(), f.[Funcao_ID], @LocatarioId, @Agora
FROM [Funcoes] f WHERE f.[Nome] = 'Locatario'
UNION ALL
SELECT NEWID(), f.[Funcao_ID], @AdminId, @Agora
FROM [Funcoes] f WHERE f.[Nome] = 'Administrador';
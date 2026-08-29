-- =========================================================
-- Popula 5 imóveis de teste, todos pertencentes ao usuário Locador
-- =========================================================

DECLARE @LocadorId UNIQUEIDENTIFIER;
SELECT @LocadorId = [Usuario_ID] FROM [Usuarios] WHERE [Email] = N'locador@domus.com';

IF @LocadorId IS NULL
BEGIN
    RAISERROR('Usuário Locador (locador@domus.com) não encontrado. Rode o script de seed de usuários primeiro.', 16, 1);
    RETURN;
END

DECLARE @Agora DATETIME2 = GETUTCDATE();

DECLARE @Endereco1 UNIQUEIDENTIFIER = NEWID();
DECLARE @Endereco2 UNIQUEIDENTIFIER = NEWID();
DECLARE @Endereco3 UNIQUEIDENTIFIER = NEWID();
DECLARE @Endereco4 UNIQUEIDENTIFIER = NEWID();
DECLARE @Endereco5 UNIQUEIDENTIFIER = NEWID();

-- 1) Endereços (1:1 com Imovel, então precisa de um por imóvel)
INSERT INTO [Enderecos] ([Endereco_ID], [CEP], [UF], [Cidade], [Bairro], [Rua], [Numero], [Complemento])
VALUES
    (@Endereco1, '01001000', 'SP', N'São Paulo',      N'Sé',            N'Praça da Sé',           '100',  NULL),
    (@Endereco2, '20040020', 'RJ', N'Rio de Janeiro',  N'Centro',        N'Av. Rio Branco',        '250',  N'Sala 12'),
    (@Endereco3, '30130000', 'MG', N'Belo Horizonte',  N'Funcionários',  N'Rua da Bahia',          '500',  NULL),
    (@Endereco4, '80010000', 'PR', N'Curitiba',        N'Centro',        N'Rua XV de Novembro',    '789',  N'Apto 302'),
    (@Endereco5, '90010000', 'RS', N'Porto Alegre',    N'Cidade Baixa',  N'Av. José Bonifácio',    '45',   NULL);

-- 2) Imóveis
INSERT INTO [Imoveis]
    ([Imovel_ID], [Usuario_ID], [Endereco_ID], [Titulo], [Descricao], [Tipo],
     [MetrosQuadrados], [Comodos], [Banheiros], [Status], [ValorAluguel],
     [CriadoEm], [Aprovado], [Avaliado], [ExcluidoEm])
VALUES
    (NEWID(), @LocadorId, @Endereco1, N'Casa térrea no centro de São Paulo',
     N'Casa ampla e reformada, próxima ao metrô, ideal para famílias.',
     'Casa', 120.00, 4, 2, 'Disponivel', 3500.00, @Agora, 1, 1, NULL),

    (NEWID(), @LocadorId, @Endereco2, N'Apartamento com vista para a Baía de Guanabara',
     N'Apartamento moderno, mobiliado, próximo à orla e ao Centro do Rio.',
     'Apartamento', 75.50, 2, 1, 'Disponivel', 2800.00, @Agora, 1, 1, NULL),

    (NEWID(), @LocadorId, @Endereco3, N'Studio compacto nos Funcionários',
     N'Studio novo, ótimo para estudantes e profissionais, mobília inclusa.',
     'Studio', 32.00, 1, 1, 'Alugado', 1600.00, @Agora, 1, 1, NULL),

    (NEWID(), @LocadorId, @Endereco4, N'Kitnet econômica no Centro de Curitiba',
     N'Kitnet simples, ideal para quem busca praticidade e baixo custo.',
     'KitNet', 22.00, 1, 1, 'Indisponivel', 950.00, @Agora, 0, 0, NULL),

    (NEWID(), @LocadorId, @Endereco5, N'Sobrado espaçoso na Cidade Baixa',
     N'Sobrado com quintal, 3 quartos, garagem para 2 carros.',
     'Sobrado', 180.00, 5, 3, 'Disponivel', 4200.00, @Agora, 1, 0, NULL);
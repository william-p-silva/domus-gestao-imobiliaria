# 🏠 DOMUS — Plataforma de Gestão de Locação Residencial

**DOMUS** é uma plataforma web para automatizar a relação entre **locadores** (proprietários) e **locatários** (inquilinos), centralizando em um único ambiente a divulgação de imóveis, a formalização de contratos, a comunicação entre as partes e o faturamento das locações — incluindo pagamentos via **Pix**.

A motivação do projeto é reduzir a burocracia tradicional do processo de locação, unindo em uma só ferramenta etapas que hoje costumam ficar espalhadas entre WhatsApp, planilhas, papel e transferências bancárias avulsas.

> Projeto acadêmico/pessoal desenvolvido por **William José Pereira da Silva**.

---

## 🎯 Visão do produto

O ecossistema é dividido em três grandes áreas:

| Área | Público | O que oferece |
|---|---|---|
| **Vitrine Digital** | Público em geral | Busca e filtro de imóveis disponíveis, avaliações peer-to-peer e chat em tempo real para tirar dúvidas antes de fechar negócio |
| **Painel do Locatário** | Inquilinos | Acompanhamento do contrato, histórico de faturas, envio de comprovantes de pagamento e recibos gerados dinamicamente |
| **Painel do Locador** | Proprietários | Cadastro e gestão de imóveis, upload de minutas de contrato, aprovação de comprovantes e acompanhamento financeiro |

**Diferenciais técnicos:**
- 💳 Integração com **Pix** (geração dinâmica de payload "Copia e Cola" e QR Code) para pagamento das parcelas.
- 💬 **Chat em tempo real** vinculado a cada imóvel, via WebSockets (SignalR).
- 🔔 Serviço de **notificações automáticas** em segundo plano (vencimento de parcelas, contratos próximos do fim, pagamentos pendentes).
- 🛡️ Moderação administrativa: todo imóvel passa por aprovação antes de aparecer na vitrine pública.
- 📊 Dashboard com métricas financeiras (arrecadação, inadimplência, contratos ativos).

---

## 🧱 Arquitetura e stack tecnológica

O repositório é um monorepo dividido em `Back-End`, `Front-End` e `Documentos`.

### Back-end — `Back-End/`
- **.NET 9**, organizado em **Clean Architecture**, com separação clara de responsabilidades:
  - `Domus.Domain` — entidades e regras de domínio
  - `Domus.Application` — casos de uso, DTOs, interfaces (repositórios, segurança, notificações)
  - `Domus.Infrastructure` — implementações concretas (EF Core, e-mail, hashing de senha)
  - `Domus.WebApi` — API REST (endpoints, autenticação, documentação via Swagger/Scalar)
  - `Domus.UnitTests` — testes unitários
- **Entity Framework Core 9** com **SQL Server** como banco relacional
- Autenticação via **JWT**
- Senhas com hash via **BCrypt**
- Envio de e-mails via **MailKit** (ex.: recuperação de senha)
- Documentação de API via **Swagger** / **Scalar**

### Front-end — `Front-End/Domus.Vue/`
- **Vue 3** + **TypeScript**, com **Vite** como build tool
- **Pinia** para gerenciamento de estado
- **Vue Router** para navegação
- **Tailwind CSS 4** para estilização
- **Zod** para validação de dados
- **@microsoft/signalr** (cliente) para o chat em tempo real
- **Vitest** para testes unitários

> ⚠️ **Nota sobre a documentação de requisitos:** o documento de requisitos anexado ao projeto (`Documentos/Docs/`) cita **Next.js** como tecnologia de front-end. Esse dado está desatualizado — o front-end efetivamente implementado no repositório usa **Vue.js**, não Next.js. As demais tecnologias descritas (`.NET`, Clean Architecture, SignalR, EF Core) conferem com o que está implementado.

---

## 📁 Estrutura do repositório

```
domus-gestao-imobiliaria/
├── Back-End/
│   ├── Domus.Domain/          # Entidades e regras de negócio
│   ├── Domus.Application/     # Casos de uso, DTOs, interfaces
│   ├── Domus.Infrastructure/  # EF Core, e-mail, segurança
│   ├── Domus.WebApi/          # API REST (.NET 9)
│   └── Domus.UnitTests/       # Testes unitários
├── Front-End/
│   └── Domus.Vue/             # SPA em Vue 3 + Vite + TypeScript
└── Documentos/
    ├── Banco-de-Dados/        # Modelo Entidade-Relacionamento (MER)
    ├── Docs/                  # Documento de requisitos (PDF/ODT)
    ├── Excel/                 # Planilha de requisitos
    ├── UML/                   # Casos de uso e diagrama de classes (PlantUML)
    └── PaletaDeCores.jpeg     # Identidade visual do projeto
```

---

## 🧩 Principais funcionalidades

- Cadastro de usuários com múltiplos perfis (locador, locatário, administrador) por e-mail único
- Login com JWT e recuperação de senha por e-mail
- Cadastro, edição, status (alugado / disponível / em manutenção) e upload de múltiplas fotos de imóveis
- Moderação/aprovação de imóveis antes da publicação na vitrine
- Busca e filtro de imóveis por proximidade geográfica
- Geração de contratos vinculando imóvel, locador e locatário, com renovação e encerramento mediante confirmação de ambas as partes
- Geração automática de parcelas a partir do contrato
- Integração Pix (QR Code + payload) associada a cada parcela
- Upload de comprovantes de pagamento pelo locatário e aprovação pelo locador
- Chat em tempo real por imóvel
- Avaliações de imóveis (uma por usuário, editável)
- Abertura e encerramento de reclamações sobre imóveis
- Notificações automáticas de vencimento, pendências e fim de contrato
- Dashboard estatístico para locadores e administradores

Para o detalhamento completo (RF, RN, RNF e requisitos de segurança), veja o documento em `Documentos/Docs/`.

---

## 🗃️ Modelo de dados

O banco de dados é relacional (SQL Server) e está organizado em quatro núcleos principais:

1. **Controle de Acesso e Perfis (RBAC)** — `Usuario`, `Funcao`, `Usuario_Funcao`
2. **Catálogo e Avaliação de Imóveis** — `Imovel`, `Endereco`, `Imovel_Foto`, `Avaliacao`
3. **Contratos, Parcelas e Faturamento** — `Contrato`, `Parcela_Aluguel`, `Comprovante_Recibo`
4. **Comunicação e Mensageria** — `Chat`, `Usuario_Chat`, `Mensagem`

Também há suporte a `Notificacao`, `Reclamacao` e `Mensagem_Reclamacao`. O diagrama completo (MER) está em `Documentos/Banco-de-Dados/MER.png` / `MER.drawio`.

---

## 🔒 Segurança

- Senhas armazenadas com hash (BCrypt), nunca em texto plano
- Comunicação cliente-servidor sobre HTTPS/WSS
- Controle de acesso baseado em papéis (RBAC)
- Restrição de dados sensíveis (CPF, e-mail, endereço, dados bancários) a usuários autenticados e autorizados
- Validação de entradas para reduzir riscos de injeção e upload de arquivos inválidos

---

## 🚀 Como rodar o projeto localmente

### Pré-requisitos
- [.NET 9 SDK](https://dotnet.microsoft.com/)
- [Node.js](https://nodejs.org/) `^22.18.0` ou `>=24.12.0`
- Uma instância de **SQL Server** (local ou em container)

### Back-end
```bash
cd Back-End
dotnet restore
dotnet ef database update --project Domus.Infrastructure --startup-project Domus.WebApi
dotnet run --project Domus.WebApi
```
Configure a connection string, o segredo do JWT e as credenciais de e-mail em `Domus.WebApi/appsettings.Development.json` (ou via *user-secrets*).

### Front-end
```bash
cd Front-End/Domus.Vue
npm install
npm run dev
```

> Ajuste a URL da API consumida pelo front-end conforme o ambiente (arquivo de configuração/variáveis de ambiente do Vite).

---

## 📚 Documentação adicional

- `Documentos/Docs/` — documento de requisitos completo (funcionais, de negócio, não funcionais e de segurança)
- `Documentos/Banco-de-Dados/` — Modelo Entidade-Relacionamento
- `Documentos/UML/` — casos de uso e diagrama de classes
- `Documentos/Excel/` — planilha de rastreamento de requisitos

---

## 👤 Autor

**William José Pereira da Silva** — Franco da Rocha, SP

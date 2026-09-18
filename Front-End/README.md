# 🎨 DOMUS — Front-End (`Domus.Vue`)

Este documento detalha a arquitetura, as tecnologias e as convenções do front-end do DOMUS, uma **SPA (Single Page Application)** construída em **Vue 3**, responsável pela vitrine pública de imóveis, autenticação, painéis de locador/locatário e chat em tempo real.

---

## 🧰 Stack tecnológica

| Categoria | Tecnologia | Uso no projeto |
|---|---|---|
| Framework | **Vue 3** (Composition API) | Base da aplicação |
| Linguagem | **TypeScript** | Tipagem estática em todo o código |
| Build tool | **Vite** | Dev server, bundling e build de produção |
| Roteamento | **Vue Router** | Navegação SPA, layouts aninhados e guards de rota |
| Estado global | **Pinia** | Store de autenticação (`useAuthStore`) |
| Estilização | **Tailwind CSS v4** (via `@tailwindcss/vite`) | Utilitários de CSS + alguns arquivos `.css` de apoio |
| Validação | **Zod** | Schemas de formulário e de contrato de dados |
| Tempo real | **@microsoft/signalr** | Cliente do chat em tempo real (Hub do back-end .NET) |
| Ícones | **lucide-vue** | Ícones da interface |
| Testes | **Vitest** + **@vue/test-utils** + **jsdom** | Testes unitários de componentes |
| Qualidade de tipos | **vue-tsc** | Checagem de tipos no build (`type-check`) |

> Node.js exigido: `^22.18.0` ou `>=24.12.0` (definido em `engines` no `package.json`).

---

## 🏗️ Arquitetura geral: organização por *features* (screaming architecture)

Em vez de agrupar arquivos por tipo técnico (todos os componentes juntos, todos os serviços juntos), o projeto agrupa por **domínio de negócio**. Isso deixa a intenção da pasta evidente ao primeiro olhar — o código "grita" o que ele faz.

```
src/
├── app/            # Camada de composição: views e rotas
│   ├── router/     # Definição de rotas e guards
│   └── view/       # "Páginas" que a rota realmente monta
│
├── core/           # Infraestrutura transversal da aplicação
│   ├── http/       # Cliente HTTP genérico (fetch wrapper)
│   └── configuration/  # Store e serviço de autenticação
│
├── features/       # Um diretório por domínio de negócio
│   ├── auth/
│   ├── chat/
│   ├── imoveis/
│   └── landingPage/
│
├── shared/         # Componentes, layouts, hooks e utils reutilizáveis
│   ├── components/ # Botões, inputs, cards, header, footer...
│   ├── layouts/    # Layouts (App, Auth, Chat, Master)
│   ├── UI/icons/
│   ├── hooks/
│   ├── types/
│   └── utils/
│
├── App.vue         # Componente raiz (apenas <RouterView />)
├── main.ts         # Bootstrap (Pinia + Router)
└── global.css      # Estilos globais / Tailwind entrypoint
```

### Anatomia de uma *feature*

Cada feature (`auth`, `chat`, `imoveis`, `landingPage`) segue o mesmo padrão interno, o que facilita a navegação entre módulos diferentes:

```
features/<nome>/
├── components/   # Componentes visuais específicos da feature
├── hooks/        # Composables com estado e lógica (o "cérebro" da tela)
├── services/     # Chamadas HTTP para a API .NET
├── scheme/       # Schemas de validação (Zod)
├── types/        # Contratos de request/response (TypeScript)
├── pages/        # Componente de página, monta os sub-componentes
├── store/        # Store Pinia local, quando necessário (ex: auth)
└── hubs/         # Cliente SignalR (apenas em `chat`)
```

**Fluxo típico de uma tela** (exemplo: login):

1. A rota (`app/router/index.ts`) monta o layout (`authLayout.vue`) e a view (`app/view/auth/AuthView.vue`).
2. A view renderiza a `page` da feature (`features/auth/components/login/loginPage.vue`), composta por componentes menores (`loginForm.vue`, `authInput.vue`, etc.).
3. O componente usa um **hook/composable** (`features/auth/hooks/useLogin.ts`) que concentra estado reativo (`isLoading`, `errorLogin`) e a lógica de submissão.
4. O hook chama um **service** (`features/auth/services/loginService.ts`), que usa o `HttpService` genérico para falar com a API.
5. O `service`, ao receber sucesso, atualiza a **store global de autenticação** (`useAuthStore`).
6. Antes do envio, os dados passam por um **schema Zod** (`features/auth/scheme/schemeLogin.ts`), que valida formato de e-mail, tamanho de senha, etc.

Essa separação (View → Page → Componentes → Hook → Service → HTTP) mantém a camada visual "burra" (sem lógica de negócio) e concentra as regras em hooks e services, o que facilita testes e manutenção.

---

## 🧭 Roteamento e layouts

O roteamento usa **rotas aninhadas com layout como componente-pai**, um padrão comum em Vue Router:

```ts
{
  path: "/locador",
  component: () => import("@/shared/layouts/appLayout.vue"),
  meta: { requiresAuth: true, roles: ["Locador"] },
  children: [
    { path: "dashboard", component: () => import("@/app/view/locador/HomeLocadorView.vue") }
  ]
}
```

Layouts disponíveis em `shared/layouts/`:
- **`appLayout.vue`** — layout padrão (header + footer) usado na landing page e nos painéis autenticados
- **`authLayout.vue`** — layout enxuto para telas de login/cadastro
- **`masterLayout.vue`** — usado nas páginas de busca/detalhe de imóveis
- **`chatLayout.vue`** — layout específico da tela de chat

Todas as rotas usam **lazy loading** (`() => import(...)`), o que gera *code splitting* automático pelo Vite — cada view só é baixada quando o usuário navega até ela.

### Proteção de rotas (guards)

O `router.beforeEach` centraliza três regras de acesso, lidas do `meta` de cada rota:
- **`requiresAuth`** — bloqueia usuários não logados, redirecionando para `Login` (com `redirect` de volta na query string)
- **`requiresGuest`** — bloqueia usuários já logados em telas como login/cadastro
- **`roles`** — verifica se o perfil do usuário logado (`Locador`, `Locatario`, etc., vindo da store) tem permissão para aquela rota

Antes de qualquer verificação, o guard aguarda `authStore.checkAuth()` caso a sessão ainda esteja sendo validada (`isCheckingAuth`), evitando redirecionamentos incorretos no primeiro carregamento da página.

---

## 🌐 Comunicação com a API

Toda chamada à API .NET passa por uma classe única, `HttpService` (`core/http/httpService.ts`), que encapsula o `fetch` nativo:

- Base URL configurável via variável de ambiente **`VITE_API_URL_BASE`**
- `credentials: "include"` em todas as requisições — a autenticação é feita via **cookie** (compatível com o JWT emitido pelo back-end), não via header manual
- Métodos tipados por *generics*: `GetAsync<T>`, `PostAsync<TResponse, TRequest>`, `PutAsync`, `DeleteAsync`
- Padroniza o **envelope de resposta** da API (`ResponseSuccess<T>` / `ResponseError`), lançando uma exceção com status, título e detalhe quando a API retorna erro

Cada feature tem sua própria classe de `service` (ex.: `LoginService`, `ChatService`, `ImovelService`) que **instancia o `HttpService`** e conhece os endpoints específicos daquele domínio — o componente nunca fala com `fetch` diretamente.

---

## 🔐 Autenticação

- `core/configuration/authentication.ts` define a store Pinia **`useAuthStore`**, com estado `isLogged`, `isCheckingAuth` e `userLogged` (id, nome, e-mail, perfis).
- `core/configuration/authService.ts` chama `auth/me` para verificar se o cookie de sessão ainda é válido (`checkAuth`).
- Como o back-end usa cookies HttpOnly para o JWT, o front-end nunca manipula o token diretamente — apenas reage ao estado retornado pela API.
- O `perfil` (array de papéis do usuário) é o que alimenta o controle de rotas por `roles` descrito acima.

---

## 💬 Chat em tempo real (SignalR)

A feature `chat` encapsula a conexão WebSocket em uma classe dedicada, `ChatHub` (`features/chat/hubs/chatHub.ts`):

- Usa `HubConnectionBuilder` do `@microsoft/signalr`, com **reconexão automática** (`withAutomaticReconnect`)
- Expõe métodos de alto nível: `start`, `stop`, `joinGroupChat`, `leaveGroupChat`
- Usa um padrão de **callback/observer** (`onReceberMensagem` / `offReceberMensagem`) para que o composable `useChat.ts` reaja a novas mensagens sem acoplar a UI diretamente ao SignalR
- O composable `useChat` gerencia o chat ativo, a lista de conversas do usuário e a troca entre salas (`joinGroupChat`/`leaveGroupChat` ao trocar de imóvel)

---

## ✅ Validação de dados

Schemas **Zod** ficam co-localizados com a feature que os usa (`features/<nome>/scheme|schemas/`). Eles cumprem dois papéis:
1. Validar dados de formulário antes do envio (ex.: e-mail válido, senha mínima)
2. Descrever o formato esperado das respostas da API (ex.: `SchemeLoginResponse` valida UUID, nome, e-mail e perfis)

Isso dá uma camada extra de segurança de tipos em tempo de execução, complementando o TypeScript (que só garante tipos em tempo de compilação).

---

## 🎨 Estilização

- **Tailwind CSS v4**, integrado via plugin oficial do Vite (`@tailwindcss/vite`), sem arquivo `tailwind.config.js` tradicional — a configuração é feita via CSS (`global.css`)
- Estilos específicos de feature (ex.: `features/auth/styles/authStyle.css`) para casos que fogem do utilitário do Tailwind
- Ícones via `lucide-vue`, com um pequeno conjunto de ícones customizados em SVG (`shared/UI/icons/imovel/`)

---

## 🧪 Testes

- **Vitest** como test runner, com ambiente **jsdom**
- **@vue/test-utils** para montar e interagir com componentes Vue em teste
- Exemplo de teste em `src/__tests__/App.spec.ts`
- Comando: `npm run test:unit`

---

## 🚀 Scripts disponíveis

```bash
npm run dev          # Servidor de desenvolvimento (Vite)
npm run build         # Type-check + build de produção
npm run build-only    # Build sem checagem de tipos (mais rápido)
npm run type-check    # Checagem de tipos via vue-tsc
npm run preview       # Servir o build de produção localmente
npm run test:unit     # Executa os testes com Vitest
```

### Configuração de ambiente

Crie um arquivo `.env` (ou `.env.local`) na raiz de `Front-End/Domus.Vue` com a URL da API:

```
VITE_API_URL_BASE=http://localhost:5038
```

> A URL do hub de chat (SignalR) está atualmente fixada em `chatHub.ts` (`http://localhost:5038/domus`). Vale a pena migrar esse valor para variável de ambiente também, para facilitar deploy em outros ambientes.

---

## 📌 Observações e pontos de atenção

- O documento de requisitos do projeto menciona **Next.js** como tecnologia de front-end — isso está desatualizado; o front-end implementado é **Vue 3 + Vite**, não Next.js/React.
- A autenticação depende de cookies (`credentials: "include"`), então back-end e front-end precisam estar configurados com CORS compatível (`credentials`/`SameSite`) para funcionar em ambientes separados (ex.: portas diferentes em dev).
- A URL do SignalR hardcoded em `chatHub.ts` é um bom candidato a virar variável de ambiente, assim como a URL base da API.
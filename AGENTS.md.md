## Contexto e Objetivos do Projeto

- **Objetivo Principal:** Aprendizado ativo e aplicação prática de boas práticas de engenharia de software. O código deve ser correto, seguro e simples de manter.
- **Ecossistema:** Este é um backend em .NET C# que será consumido por uma aplicação frontend desenvolvida em **Next.js**.
- **Filosofia:** Simplicidade e Robustez. Não introduza complexidade desnecessária, over-engineering, ou padrões arquiteturais da moda se não resolverem um problema real do escopo.

## Arquitetura (Clean Architecture)

O projeto segue a Clean Architecture. As dependências apontam **sempre de fora para dentro** em direção ao `Domain`.

- **`Homely.Domain`**: Coração do negócio. Contém Entidades, Enums, Erros (fortemente tipados) e as abstrações de repositórios. **Regra:** Zero dependências de infraestrutura, banco de dados ou frameworks web.
- **`Homely.Application`**: Casos de uso da aplicação (CQRS). Orquestra o domínio. Contém as definições de interfaces (ex: `ITokenProvider`, `ICurrentUser`). **Regra:** Depende apenas do Domain.
- **`Homely.Infrastructure`**: Implementações técnicas. Entity Framework Core, acesso a dados (`Mappings`), geração de JWT, Hashing, implementações de repositórios. **Regra:** Depende do Application e Domain.
- **`Homely.Api`**: Camada de apresentação (Controllers). Responsável por receber requisições HTTP, despachar para a Application e traduzir o resultado. **Regra:** Não deve conter regras de negócio.

## Padrões Adotados (Não viole)

### 1. Result Pattern

**Nunca lance exceções para controle de fluxo de regras de negócio.**

- O projeto utiliza a classe `Result` (`Homely.Domain/Common/Result.cs`) para retornar sucesso ou falha.
- Utilize as classes de erro específicas (ex: `Homely.Domain/Errors/LoginErrors.cs`).
- A `Api` utiliza `ResultExtensions.cs` para traduzir o `Result` em códigos HTTP apropriados (ex: `Result.IsSuccess` -> 200 OK, `Result.IsFailure` -> 400 Bad Request, 404 Not Found, etc).

### 2. CQRS Simplificado

- Comandos (operações que alteram estado) e Consultas (operações de leitura) estão separados fisicamente no diretório `Homely.Application/UseCases/`.
- **Não** introduza bancos de dados separados de leitura/escrita ou barramentos de eventos complexos. Mantenha o CQRS restrito ao nível lógico da aplicação.

### 3. Persistência (Entity Framework Core)

- Mantenha a configuração das entidades separada das classes de domínio utilizando `IEntityTypeConfiguration` dentro da pasta `Homely.Infrastructure/Data/Mappings/`.
- Repositórios devem utilizar o `IUnitOfWork` para gerenciar transações (Commit), garantindo consistência.

## 4. Integração com Next.js

Como o frontend é em Next.js, as seguintes regras de API devem ser estritamente observadas:

- **Contratos (JSON):** As respostas devem ser previsíveis. Não altere DTOs (Request/Response) sem verificar o impacto no contrato do frontend.
- **HTTP Status Codes:** Use semanticamente (200 para sucesso, 201 para criação, 401 para não autenticado, 403 para sem permissão, 404 para não encontrado, 409 para conflitos).

## ⚠️ Problemas Conhecidos e Limitações (Para Revisão Futura)

Os pontos abaixo são deficiências arquiteturais ou de segurança já mapeadas na implementação atual e que exigem cautela ou refatoração estruturada.

### 1. Fluxo de Autenticação e Renovação de Tokens (Crítico)

- **Status Atual:** A aplicação emite Access Tokens (JWT) e Refresh Tokens (com hashing no banco, via `TokenHashMigration`). Existe um caso de uso `LoginResponse.cs`.
- **Problema Potencial (Risco de Segurança):** Se o Refresh Token estiver sendo retornado no corpo da resposta JSON (`LoginResponse`) para o Next.js, ele será vulnerável a ataques de XSS (Cross-Site Scripting), pois o frontend terá que armazená-lo no `localStorage` ou na memória.
- **Direção de Melhoria (Ação Futura):** O contrato da API no `AuthController` deve ser alterado para retornar o Access Token no JSON, mas o **Refresh Token deve ser retornado como um Cookie HTTP-Only e Secure**. O endpoint de `/refresh-token` deve ler o token diretamente do cookie, isolando-o do acesso via JavaScript no frontend.
- **Ordem:** Agentes de IA que modificarem a autenticação devem propor e implementar essa migração para `HttpOnly Cookies` se solicitado, ajustando os contratos adequadamente.

### 2. Tratamento Global de Exceções

- **Status Atual:** Existe um `ExceptionHandler.cs` na API.
- **Verificação Necessária:** Garantir que este handler esteja mascarando corretamente erros internos (500) para não vazar StackTraces para o Next.js em ambiente de produção (retornando o padrão genérico RFC 7807 _Problem Details_).

## Regras Operacionais para Agentes de IA

Sempre que você for encarregado de propor ou realizar alterações:

1. **Analise antes de agir:** Leia os arquivos da funcionalidade correspondente nas 4 camadas (Api, Application, Domain, Infrastructure).
2. **Respeite as Fronteiras:** Nunca injete `AppDbContext` em Controllers. Nunca adicione referências web/http na camada Domain.
3. **Seja Simples e Direto:** Se a solução exigir adicionar bibliotecas de terceiros complexas, pare e justifique tecnicamente por que o ferramental nativo do .NET não é suficiente.
4. **Comunique Decisões:** Quando implementar uma refatoração ou criar uma funcionalidade, explique brevemente (em 1 ou 2 parágrafos no chat) o _porquê_ da abordagem, visando o aprendizado do usuário.
5. **Testabilidade:** Sempre desenhe interfaces e injete dependências (DI) visando facilidade de mocks e testes unitários.
6. **Não oculte falhas:** Se você esbarrar em um código mal implementado ou que fere o Clean Architecture que não estava no seu escopo imediato, notifique o usuário sobre o problema antes de consertá-lo cegamente.

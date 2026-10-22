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
- A `Api` traduz o `Result` em códigos HTTP apropriados. `ResultExtensions.cs` oferece o helper `Match`; o `AuthController` faz essa tradução explicitamente. O helper não mapeia automaticamente tipos de erro para status HTTP.

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
- **Ambiente atual:** O frontend de desenvolvimento usa `http://localhost:3000`. Preserve essa origem; a configuração de produção será analisada futuramente.
- **Integração de autenticação:** Chamadas do navegador à API devem incluir credenciais para enviar cookies. Validar o fluxo no navegador, incluindo a combinação de HTTP/HTTPS e `SameSite`; sucesso no Postman não comprova essa integração. O frontend deve descartar o access token no logout e coordenar renovações simultâneas, inclusive entre abas.

## Autenticação: Estado Atual e Garantias

### 1. Contratos HTTP e Tokens

- `POST /api/auth/login` recebe JSON com `email` e `password`. Retorna `200` com `{ accessToken }` e define o cookie `refreshToken`; credenciais inválidas retornam `401`.
- `POST /api/auth/refresh-token` não exige corpo. Lê o refresh token do cookie, realiza a rotação e retorna `200` com `{ accessToken }` e um novo cookie. Token ausente, expirado, revogado ou inválido retorna `401`.
- `POST /api/auth/logout` lê o cookie, revoga a sessão associada e seus refresh tokens, apaga o cookie e retorna `204`. É idempotente, inclusive sem cookie ou com token desconhecido; falhas de persistência não devem ser ocultadas.
- O refresh token é aleatório e somente seu hash SHA-256 é persistido. Nunca o exponha no JSON público ou em logs. `LoginResponse` contém o token bruto apenas para comunicação interna entre Application e API.
- O cookie usa `HttpOnly`, `Secure`, `SameSite=Strict` e `Path=/api/auth`. A exclusão deve preservar os atributos de identificação do cookie.
- A configuração atual define access tokens de 15 minutos e refresh tokens de 7 dias. Cada rotação preenche `ExpiresAt` com uma nova validade configurada. Logout não invalida imediatamente um JWT já emitido; ele continua válido até expirar.
- Uma falha de refresh não apaga o cookie: uma resposta atrasada não deve remover o cookie emitido por outra requisição bem-sucedida.

### 2. Sessões e Concorrência

- Cada login cria uma `RefreshSession`; tokens sucessivos preservam o mesmo `SessionId`. Logout encerra somente essa sessão, preservando outros logins do usuário.
- `RotateAsync` e `RevokeAsync` atualizam primeiro a mesma linha da sessão dentro de transações PostgreSQL. Preserve essa ordem de bloqueio: sessão antes dos tokens.
- A atualização da sessão em `RotateAsync`, mesmo mantendo `IsRevoked=false`, adquire o bloqueio necessário. Não a remova como se fosse uma escrita redundante.
- Se o refresh concluir primeiro, o logout revoga também o sucessor. Se o logout concluir primeiro, a rotação falha. Revogação do token anterior e inserção do substituto devem continuar atômicas.
- A migration `20261022135506_RefreshSessions` cria as sessões e associa cada token preexistente a uma sessão própria antes de criar a chave estrangeira. Ela não reconstrói vínculos históricos entre tokens antigos. Sua aplicação no ambiente de desenvolvimento foi confirmada pelo usuário.

### 3. Validação Realizada

- Build da solução e verificação de correspondência entre modelo EF e migration passaram.
- `scripts/Test-AuthConcurrency.ps1` executou chamadas HTTP contra PostgreSQL: refresh primeiro retornou `200`, logout `204` e uso posterior do substituto `401`; logout primeiro retornou `204` e refresh `401`.
- O script requer API em execução, dados de seed, PowerShell e `curl.exe`. Os parâmetros `LogoutDelayMilliseconds` e `RefreshDelayMilliseconds` permitem favorecer cada ordem. Ele cria sessões para o usuário de desenvolvimento do seed e não é um teste de carga nem uma prova exaustiva de todas as intercalações.
- Respeite o rate limit de autenticação (10 requisições por minuto por IP na configuração atual). `429` invalida o resultado do cenário; não o interprete como revogação bem-sucedida.
- O usuário confirmou sucesso dos testes de login, refresh e logout no Postman. A integração com o Next.js no navegador ainda precisa ser validada.

## Problemas Conhecidos e Limitações

- **Responsabilidade transacional:** A regra de uso de `IUnitOfWork` permanece. Login a segue, mas `RotateAsync` e `RevokeAsync` ainda gerenciam transações diretamente no repositório. Essa divergência precisa de refatoração, preservando os bloqueios e a atomicidade descritos acima. `ExecuteUpdateAsync` persiste imediatamente; adicionar apenas `CommitAsync` não resolve a divergência.
- **Reutilização de tokens:** Tokens já consumidos são rejeitados, mas a reutilização não revoga automaticamente toda a sessão. Essa política ainda precisa ser definida considerando concorrência legítima entre chamadas e abas.
- **Cookies e CSRF:** Validar o comportamento real dos cookies no navegador e definir proteção contra CSRF antes de flexibilizar `SameSite`. CORS não substitui essa proteção. A configuração de produção permanece para revisão futura.
- **Exceções:** `ExceptionHandler` retorna mensagem genérica em `ProblemDetails` para erros `500` e registra detalhes nos logs. Outros status ainda usam `exception.Message`; revisar a exposição de detalhes e o mapeamento genérico de exceções conforme necessário.

## Regras Operacionais para Agentes de IA

Sempre que você for encarregado de propor ou realizar alterações:

1. **Analise antes de agir:** Leia os arquivos da funcionalidade correspondente nas 4 camadas (Api, Application, Domain, Infrastructure).
2. **Respeite as Fronteiras:** Nunca injete `AppDbContext` em Controllers. Nunca adicione referências web/http na camada Domain.
3. **Seja Simples e Direto:** Se a solução exigir adicionar bibliotecas de terceiros complexas, pare e justifique tecnicamente por que o ferramental nativo do .NET não é suficiente.
4. **Comunique Decisões:** Quando implementar uma refatoração ou criar uma funcionalidade, explique brevemente (em 1 ou 2 parágrafos no chat) o _porquê_ da abordagem, visando o aprendizado do usuário.
5. **Testabilidade:** Sempre desenhe interfaces e injete dependências (DI) visando facilidade de mocks e testes unitários.
6. **Não oculte falhas:** Se você esbarrar em um código mal implementado ou que fere o Clean Architecture que não estava no seu escopo imediato, notifique o usuário sobre o problema antes de consertá-lo cegamente.

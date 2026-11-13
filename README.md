# Desafio Selenium: Web Form

Projeto de testes automatizados de interface com **Selenium WebDriver**, **C#** e **MSTest**, desenvolvido para a disciplina de **Garantia da Qualidade de Software** (Gestão e Qualidade de Software), com o professor Daniel Henrique Matos de Paiva.

Os testes automatizam o formulário de demonstração do Selenium:
https://www.selenium.dev/selenium/web/web-form.html

## Tecnologias

- .NET
- C#
- MSTest
- Selenium WebDriver
- Google Chrome (ChromeDriver gerenciado automaticamente pelo Selenium Manager)

## Estrutura do projeto

```
SeuProjeto.Tests/
├── WebFormularioDesafioTest.cs
└── SeuProjeto.Tests.csproj
```

## Cenários de teste

A classe `WebFormularioDesafioTest` contém um `[TestMethod]` para cada exercício.

### Exercício 1: Cadastro completo (preenchimento misto)

| Passo | Ação |
|-------|------|
| 1 | Navegar até a URL do formulário |
| 2 | Preencher `my-text` com o nome completo |
| 3 | Preencher `my-password` com uma senha segura |
| 4 | Preencher `my-textarea` com uma descrição da experiência com testes |
| 5 | Validar que o campo readonly (`my-readonly`) contém `Readonly input` |
| 6 | Submeter o formulário e validar a mensagem `Received!` |

### Exercício 2: Seletores e listas inteligentes

| Passo | Ação |
|-------|------|
| 1 | Usar `SelectElement` para selecionar `Two` em `my-select` |
| 2 | Validar que a opção selecionada possui o valor `2` |
| 3 | Enviar `Seattle` para o campo `my-datalist` |
| 4 | Submeter o formulário e validar a mensagem `Received!` |

### Exercício 3: Estados, checkboxes, radios e color picker

| Passo | Ação |
|-------|------|
| 1 | Validar que `my-check-1` já vem marcado por padrão |
| 2 | Clicar em `my-check-2` para marcá-lo |
| 3 | Selecionar o rádio `my-radio-2` |
| 4 | Alterar o valor do color picker (`my-colors`) via JavaScript |
| 5 | Submeter o formulário e validar a mensagem `Received!` |

## Pré-requisitos

- [.NET SDK](https://dotnet.microsoft.com/download) instalado
- Google Chrome instalado
- Conexão com a internet (os testes acessam o site do Selenium)

## Instalação

Clone o repositório e restaure as dependências:

```bash
git clone <url-do-repositorio>
cd <pasta-do-projeto>
dotnet restore
```

Pacotes NuGet utilizados:

```bash
dotnet add package Selenium.WebDriver
dotnet add package MSTest.TestFramework
dotnet add package MSTest.TestAdapter
dotnet add package Microsoft.NET.Test.Sdk
```

## Como executar os testes

Rodar todos os testes:

```bash
dotnet test
```

Rodar com detalhes de cada teste:

```bash
dotnet test --logger "console;verbosity=detailed"
```

Rodar apenas um exercício:

```bash
dotnet test --filter "Name~Exercicio1"
```

Resultado esperado:

```
Aprovado! - Com falha: 0, Aprovado: 3, Ignorado: 0, Total: 3
```

## Decisões técnicas

- **`[TestInitialize]` e `[TestCleanup]`**: cada teste abre uma nova instância do Chrome e a encerra ao final, garantindo isolamento entre os cenários.
- **`WebDriverWait`**: usado na página de sucesso para aguardar a mensagem `Received!` de forma explícita.
- **Método auxiliar `SubmeterEValidarSucesso()`**: evita repetição de código na submissão e validação do formulário.
- **Color picker via JavaScript**: o `<input type="color">` abre um seletor nativo do navegador e não aceita `SendKeys`, por isso o valor é definido com `IJavaScriptExecutor`, disparando os eventos `input` e `change`.
- **Localizadores**: campos de texto, select e datalist usam `By.Name`; checkboxes e rádios usam `By.Id`, já que o atributo `name` é compartilhado entre eles.

## Solução de problemas

| Problema | Possível causa | Solução |
|----------|----------------|---------|
| `NoSuchElementException` | Seletor incorreto ou página não carregada | Conferir `name`/`id` com o inspetor (F12) |
| Erro de versão do ChromeDriver | Selenium desatualizado | Atualizar o pacote `Selenium.WebDriver` |
| Timeout na página de sucesso | Site fora do ar ou sem internet | Abrir a URL manualmente para confirmar |
| Falha no color picker | Valor em formato inválido | Usar hexadecimal minúsculo, como `#ff0000` |

## Autor

**Marlon Andrade Bartoli**
Ciência da Computação, UNA

# CustomTasks

## Menu de navegação 📍
1. [Introdução](#apresentando-o-customtasks-%EF%B8%8F)
   - [Resumo](#de-forma-sucinta)
   - [Funcionamento](#como-funciona)
   - [Estrutura de diretórios](#estrutura-de-diretórios)
3. [Como começar](#preparando-o-terreno-)
   - [Clonando o repositório](#clonando-o-repositório-%EF%B8%8F)
   - [Baixando dependências](#baixando-dependências-%EF%B8%8F)
     - [Dependências .NET](.)
     - [Dependências React](.)
   - [Configurando o DB](#configurando-o-banco-de-dados-)
4. Como usar (a ser feita)

## Apresentando o `CustomTasks` ✈️

### De forma sucinta...
<p align="justify">
O <code>CustomTasks</code> é um sistema 100% "<em>web</em>" com o objetivo de providenciar ao usuário uma interface visual agradável e limpa com a qual possa interagir e gerenciar suas tarefas de forma simples e descomplicada. Para ser mais específico, quando falamos em gerência nos referimos as seguintes capacidades: <strong>criação</strong>, <strong>visualização</strong>, <strong>modificação</strong> e <strong>exclusão</strong>. O <code>CustomTasks</code> ainda vai mais longe e busca ofertar um controle mais granular sobre as tarefas, permitindo que atribua-as etiquetas especialmente pensadas para torná-las mais fáceis de organizar e mais semânticas, habilitando também a realização de filtragens inteligentes que atendam suas necessidades. Finalizando a seção, as etiquetas, da mesma forma que as tarefas, são plenamente geríveis pelo usuário, em outras palavras, ele tem <strong>total</strong> controle sobre elas, podendo usá-las de modo que melhor lhe convenha.
</p>

### Como funciona?
<p align="justify">
Com a pergunta "O que é?" respondida podemos agora nos concentrar no funcionamento da aplicação, sendo breve, podemos quebrá-la essencialmente em dois principais componentes, são eles: um sítio virtual feito com base nos recursos disponíveis na biblioteca <a href="https://react.dev/"><em>React</em></a>, desenvolvida pelo conglomerado estadunidense denominado <strong><em>Meta</em></strong> juntamente com o superconjunto da linguagem de programação JavaScript (ou EcmaScript), a <a href="https://www.typescriptlang.org/"><em>Typescript</em></a>, e como segundo componente, uma "<em>API</em>" ("<em>Application programming interface</em>") programada usando da linguagem de programação C# (ou "<em>Csharp</em>"), projetada pela empresa, também estadunidense, <strong><em>Microsoft</em></strong>. Esses dois componentes citados anteriormente trabalham de maneira sincronizada para permitir que o sistema desempenhe as funcionalidades enumeradas e declaradas na seção acima e mais duas outras imprescindíveis para o seu bom funcionamento e uso: o <strong> sistema de cadastro</strong> e o de "<strong><em>login</em></strong>" (entrada), ambas exercem suas atividades sobre o usuário, composto de  três elementos: um <strong>nome</strong>, pelo qual será referenciado e servirá como um identificador único, um "<strong><em>email</em></strong>" e uma <strong>senha</strong>.
</p>

### Estrutura de diretórios
<p align="justify">
Finalizando nossa seção de introdução, é importante ressaltar bem como todo o projeto está organizado, haja visto que essa disposição dos diretórios e arquivos será de extrema importância e necessária em seções subsequentes, elas partirão do pressuposto que a estrutura que você possui localmente, isto é, em sua máquina, está de acordo com a seguinte:
</p>

```bash
CustomTasks/
├─ .gitignore
├─ CustomTasks/ # Onde se concentra todo o backend
│  ├─ CustomTasks.csproj
│  ├─ customtasks.db # Criado em seções posteriores
│  ├─ Models/ # Classes modelo para as tabelas
│  │  ├─ AppDataContext.cs
│  │  ├─ Label.cs
│  │  ├─ Task.cs
│  │  └─ Users.cs
│  ├─ Program.cs # Código fonte (.cs)
│  └─ Teste.http # Arquivo de teste da API
├─ CustomTasks.sln
├─ README.md # README do repositório
├─ website/ # Onde reside todo o frontend 
│   ├─ package-lock.json # Arquivo de dependências Node.js
│   ├─ package.json # Outro arquivo de dependências Node.js
│   ├─ public/ # Arquivos estáticos públicos
│   │  └─ index.html # Página index
│   ├─ src/ # Código fonte (.ts ou .tsx)
│   │  ├─ App.tsx
│   │  ├─ index.tsx
│   │  ├─ react-app-env.d.ts
│   └─ tsconfig.json
```

## Preparando o terreno 🌱
<p align="justify">
A aplicação <code>CustomTasks</code> faz extensivo uso dos recursos disponíveis no <a href="https://learn.microsoft.com/pt-br/aspnet/entity-framework"><em>EntityFramework</em></a>, disponível na já mencionada plataforma <a href="https://dotnet.microsoft.com/pt-br"><em>.NET</em></a> (ou "<em>Dotnet</em>" se preferir), sendo mais específico, falo das ferramentas nele presentes que possibilitam a integração com a biblioteca chamada <a href="https://www.sqlite.org/"><em>SQLite</em></a>, portanto, para que tudo funcione corretamente, vai ser necessário tê-la instalada na máquina onde pretender executar o sistema, não só o "<em>framework</em>", mas também terá de fazer algumas configurações relacionadas ao banco de dados em si, ponto esse que será abordado mais precisamente em um tópico específico dessa seção.
</p>

### Clonando o repositório ⛓️
<p align="justify">
Antes de qualquer configuração relacionada ao banco e às dependências do projeto, precisamos dele em si, ou seja, os arquivos e diretórios que compõem sua estrutura devem estar presentes em sua máquina, para isso, execute o seguinte comando:
</p>

```
git clone https://github.com/LuizLeineker/CustomTasks.git
```

### Baixando dependências ↩️
<p align="justify">
Caso não tenha ocorrido nenhum erro no passo anterior, podemos agora partir para a instalação das dependências já mencionadas. Primeiro, no mesmo diretório/pasta onde houve a clonagem deste repositório, mova-se para a pasta <code>CustomTasks</code> gerada pelo comando anterior, nessa mesma pasta, mova-se para o diretório interno de mesmo nome e execute o seguinte comando:
</p>

```
dotnet restore
```

<p align="justify">
Estamos quase totalmente "<em>setados</em>"! Esse último comando se encarregará de baixar e instalar todas as dependências necessárias definidas no <a href="https://github.com/LuizLeineker/CustomTasks/blob/main/CustomTasks/CustomTasks.csproj">arquivo de projeto</a> <code>(CustomTasks.csproj)</code>, assegurando que seu ambiente esteja pronto para compilar e executar a aplicação.
</p>

### Configurando o banco de dados 📦
<p align="justify">
Finalmente! A essa altura do campeonato estamos terminando o ajuste do ambiente no qual o sistema vai rodar, só precisamos agora estruturar, modelar nosso banco de dados, criar as tabelas que o constituirão, também já moldadas através das classes presentes nos arquivos <code>.cs</code> da <a href="https://github.com/LuizLeineker/CustomTasks/tree/main/CustomTasks/Models">pasta de modelos</a> <code>(CustomTasks/Models)</code>. Para que finalmente terminemos as configurações e possamos ver o <code>CustomTasks</code> em ação precisaremos rodar mais dois comandos, atenção, <strong>na mesma pasta que estávamos no passo anterior</strong>:
</p>

```
dotnet ef migrations add NomeDaMigracao
```

E, logo em seguida:

```
dotnet ef database update
```

<p align="justify">
Prontinho! Com o sucesso em todas as etapas até aqui o processo de configuração do ambiente está oficialmente completo, o que significa que finalmente estamos com todas as configurações necessárias para rodar o sistema, o que pode ser feito através do comando:
</p>

```
dotnet run
```
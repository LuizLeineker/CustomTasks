# CustomTasks

## Menu de navegação 📍
1. [Introdução](#apresentando-o-customtasks-%EF%B8%8F)
   - [Resumo](#de-forma-sucinta)
   - [Funcionamento]()
3. [Como começar](#preparando-o-terreno-)
   - [Clonando o repositório](#clonando-o-repositório-%EF%B8%8F)
   - [Baixando dependências](#baixando-dependências-%EF%B8%8F)
     - [Dependências .NET]()
     - [Dependências React](.)
   - [Configurando o DB](#configurando-o-banco-de-dados-)
4. Como usar (a ser feita)

## Apresentando o `CustomTasks` ✈️

### De forma sucinta...
<p align="justify">
O <code>CustomTasks</code> é um sistema 100% "<em>web</em>" com o objetivo de providenciar ao usuário uma interface gráfica agradável e limpa com a qual ele possa interagir e gerenciar suas tarefas de forma simples e descomplicada. Para ser mais específico, quando falamos em gerência nos referimos as seguintes capacidades: <strong>criação</strong>, <strong>visualização</strong>, <strong>modificação</strong> e <strong>exclusão</strong>. O <code>CustomTasks</code> ainda vai mais longe e busca ofertar ao usuário um controle mais granular sobre suas tarefas, permitindo que as atribua etiquetas, visando torná-las mais facilmente identificáveis, mais semânticas e permitir a realização de filtragens (caso necessárias). Finalizando a seção, as etiquetas, da mesma forma que as tarefas, também podem ser plenamente geridas pelo usuário da forma que melhor lhe convir.
</p>

### Como funciona?
<p align="justify">
Com a pergunta "O que é?" respondida podemos agora nos concentrar no funcionamento da aplicação, sendo breve, podemos quebrá-la essencialmente em dois principais componentes, são eles: um sítio virtual feito com base nos recursos disponíveis na biblioteca <a href="https://react.dev/">React</a>, desenvolvida pelo conglomerado estadunidense denominado <strong><em>Meta</em></strong> em conjunto com o superconjunto da linguagem de programação JavaScript (ou EcmaScript), a <a href="https://www.typescriptlang.org/">Typescript</a> e, como segundo componente, uma API programada usando da linguagem de programação <a href="https://learn.microsoft.com/pt-br/dotnet/csharp/">C#</a> (ou "<em>Csharp</em>"), desenvolvida pela empresa, também estadunidense, <strong><em>Microsoft</em></strong>. Esses dois componentes citados anteriormente trabalham de maneira sincronizada para permitir que a aplicação desempenhe as funcionalidades enumeradas e declaradas na seção acima e mais duas outras imprescindíveis para o bom funcionamento e uso: <strong> sistema de cadastro</strong> e "<strong><em>login</em></strong>"
</p>

## Preparando o terreno 🌱
<p align="justify">
A aplicação `CustomTasks` faz extensivo uso dos recursos disponíveis no framework <a href="https://learn.microsoft.com/pt-br/aspnet/entity-framework">EntityCore</a>, disponível na plataforma <a href="https://dotnet.microsoft.com/pt-br">.NET</a> (ou "<em>Dotnet</em>" se preferir), sendo mais específico, falo das ferramentas nele presentes que possibilitam a integração com a biblioteca chamada <a href="https://www.sqlite.org/">SQLite</a>, portanto, para que tudo funcione corretamente, vai ser necessário tê-la instalada na máquina onde pretender executar o sistema, não só o "<em>framework</em>", mas também terá de fazer algumas configurações relacionadas ao banco de dados em si, ponto esse que será abordado mais precisamente em um tópico específico dessa seção
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
Caso não tenha ocorrido nenhum erro no passo anterior, podemos agora partir para a instalação das dependências do nosso projeto já mencionadas. Primeiro, no mesmo diretório/pasta onde houve a clonagem deste repositório, mova-se para a pasta <code>CustomTasks</code> gerada pelo comando anterior, nessa mesma pasta, mova-se para o diretório interna de mesmo nome e execute o seguinte comando:
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

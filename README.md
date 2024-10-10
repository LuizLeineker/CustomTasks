# CustomTasks

## Menu de navegação 📍
1. Introdução (a ser feita)
2. [Como começar](#preparando-o-terreno-)
   - [Clonando o repositório](#clonando-o-repositório-%EF%B8%8F)
   - [Baixando dependências](#baixando-dependências-%EF%B8%8F)
   - [Configurando o DB](#configurando-o-banco-de-dados-)
3. Como usar (a ser feita)

## Preparando o terreno 🌱
A aplicação `CustomTasks` faz extensivo uso dos recursos disponíveis no framework [EntityCore](https://learn.microsoft.com/pt-br/aspnet/entity-framework), disponível na plataforma [.NET](https://dotnet.microsoft.com/pt-br/) (ou _"Dotnet"_ se preferir), sendo mais específico, falo das ferramentas nele presentes que possibilitam a integração com a biblioteca chamada [SQLite](https://www.sqlite.org/), portanto, para que tudo funcione corretamente, vai ser necessário tê-la instalada na máquina onde pretender executar o sistema, não só o framework, mas também terá de fazer algumas configurações relacionadas ao banco de dados em si, ponto esse que será abordado mais precisamente em um tópico específico dessa seção.

### Clonando o repositório ⛓️
Antes de qualquer configuração relacionada ao banco e as dependências do projeto, precisamos dele em si, ou seja, os arquivos e diretórios que compõem sua estrutura devem estar presentes em sua máquina, para isso, execute o seguinte comando:

```
git clone https://github.com/LuizLeineker/CustomTasks.git
```
### Baixando dependências ↩️
Bom, caso não tenha ocorrido nenhum erro no passo anterior, podemos agora partir para a instalação das dependências do nosso projeto já anteriormente citadas. Primeiro, no mesmo diretório/pasta onde houve a clonagem deste repositório, mova-se para a pasta `CustomTasks` gerada pelo comando anterior, nessa mesma pasta, mova-se para o diretório interna de mesmo nome e execute o seguinte comando:

```
dotnet restore
```

Estamos quase totalmente _"setados"_! Esse último comando se encarregará de baixar e instalar todas as dependências necessárias definidas no **[arquivo de projeto](https://github.com/LuizLeineker/CustomTasks/blob/main/CustomTasks/CustomTasks.csproj)** `(CustomTasks.csproj)`, assegurando que seu ambiente esteja pronto para compilar e executar a aplicação.

### Configurando o banco de dados 📦
Finalmente! A essa altura do campeonato estamos terminando o ajuste do ambiente no qual o sistema vai rodar, só precisamos agora estruturar, modelar nosso banco de dados, criar as tabelas que o constituirão, também já modeladas através das classes presentes nos arquivos `.cs` da [pasta de modelos](https://github.com/LuizLeineker/CustomTasks/tree/main/CustomTasks/Models) `(CustomTasks/Models)`. Para que finalmente terminemos as configurações e possamor ver o `CustomTasks` em ação precisaremos rodar mais dois comandos, atenção, **na mesma pasta que estávamos no passo anterior**:

```
dotnet ef migrations add NomeDaMigracao
```

E, logo em seguida:

```
dotnet ef database update
```

Prontinho! Com o sucesso em todas as etapas até aqui o processo de configuração do ambiente está oficialmente completo, o que significa que pode finalmente rodar o sistema com o comando:

```
dotnet run
```

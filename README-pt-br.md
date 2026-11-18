# 📋 Tasklist

Uma aplicação web desenvolvida com ASP.NET Core Razor Pages para gerenciar tarefas de forma simples, organizada e responsiva. O projeto foi criado para ajudar no controle de atividades diárias, com prioridades, prazos, filtros e um painel visual para acompanhamento do progresso.

## 📌 Sobre o projeto

O Tasklist é uma solução prática para quem deseja organizar compromissos, pendências e metas em um único lugar. A aplicação permite criar, editar, concluir e excluir tarefas, além de visualizar rapidamente o que precisa ser feito e o que já foi concluído. A aplicação foi construída com foco em usabilidade, simplicidade e boa experiência de navegação, mantendo a lógica de negócio organizada e a interface limpa.

## ✨ Principais funcionalidades

- ✅ Cadastro completo de tarefas
- ✅ Edição e exclusão de tarefas
- ✅ Marcar tarefas como concluídas ou pendentes
- ✅ Definição de prioridade: baixa, média, alta e urgente
- ✅ Cadastro de prazo para cada tarefa
- ✅ Dashboard com resumo visual de pendências, concluídas e próximas datas
- ✅ Filtros por busca, status, prioridade e intervalo de datas
- ✅ Ordenação das tarefas por diferentes critérios
- ✅ Persistência local com SQLite
- ✅ Interface responsiva para diferentes tamanhos de tela

## 🛠️ Tecnologias utilizadas

### Backend
- ASP.NET Core 10.0
- C#
- Razor Pages
- Entity Framework Core 10.0
- SQLite

### Frontend
- Razor Views
- HTML5
- CSS personalizado
- JavaScript

## 🧱 Arquitetura e estrutura do projeto

A aplicação segue uma estrutura organizada em camadas simples:

- Pages: páginas Razor para navegação e renderização das telas
- Models: entidades e modelos de entrada/saída
- Services: lógica de aplicação e regras de negócio
- Data: contexto do Entity Framework e migrations
- wwwroot: arquivos estáticos como CSS e JavaScript

## ⚙️ Requisitos do sistema

Antes de executar o projeto, certifique-se de ter instalado:

- .NET 10 SDK
- Git (opcional, para clonar o repositório)

## 🚀 Como executar localmente

### 1. Clone o repositório

```bash
git clone https://github.com/DiegoRamonSantos/TaskList.git
cd tasklist
```

Ou se preferir, baixe o repositório como ZIP e extraia na pasta desejada.

### 2. Abra o projeto no Visual Studio

Instale o Visual Studio 2026 com a carga de trabalho **ASP.NET e desenvolvimento Web** e o **.NET 10 SDK**. Abra o arquivo `Tasklist.slnx` na pasta do projeto. O Visual Studio restaurará as dependências NuGet ao carregar ou compilar a solução.

### 3. Execute a aplicação

No Visual Studio, selecione o perfil `https` na barra de execução e pressione **F5**. Para executar sem depuração, pressione **Ctrl+F5**.

A aplicação será aberta no navegador. Com o perfil `https`, os endereços configurados são:

- https://localhost:7226
- http://localhost:5142

### 4. Uso básico

1. Acesse a página inicial e escolha a opção para criar uma nova tarefa.
2. Preencha o título, descrição, prioridade e prazo, se necessário.
3. Salve a tarefa.
4. Utilize os filtros para encontrar tarefas específicas.
5. Marque as tarefas como concluídas conforme o progresso.

## 🗄️ Banco de dados

O projeto utiliza SQLite e o arquivo de banco é gerado automaticamente em:

- Data/taskflow.db

- As migrations são aplicadas automaticamente ao iniciar a aplicação.

## 📝 Observações

- A aplicação foi pensada para uso local e simples, sem necessidade de serviços externos.
- O projeto é ideal para estudos, portfólio e evolução para uma versão mais completa no futuro.

## 👨‍💻 Autor

- Diego Ramon 
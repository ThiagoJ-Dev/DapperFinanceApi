<h1 align="center">💰 Finance API - ASP.NET Core + Dapper</h1>

<p align="center">
API REST para gerenciamento financeiro desenvolvida com <b>ASP.NET Core</b> utilizando <b>Dapper</b> para acesso rápido ao banco de dados.
</p>

<p align="center">
Projeto focado em <b>boas práticas de backend</b>, organização de código e arquitetura em camadas.
</p>

<hr>

<h2>🚀 Tecnologias Utilizadas</h2>

<ul>
<li>C#</li>
<li>ASP.NET Core Web API</li>
<li>Dapper</li>
<li>SQL Server</li>
<li>ClosedXML (Exportação Excel)</li>
<li>Dependency Injection</li>
<li>DTO Pattern</li>
</ul>

<hr>

<h2>📂 Estrutura do Projeto</h2>

<pre>
DapperFinanceVideo
│
├── Controllers
│   ├── DashBoardController.cs
│   ├── ReportsController.cs
│   └── TransactionsController.cs
│
├── DTO
│   ├── CategoryTotalDto.cs
│   ├── DashboardDto.cs
│   └── TransactionCreateDto.cs
│
├── Models
│   ├── CategoryModel.cs
│   └── TransactionModel.cs
│
├── Repositories
│   ├── CategoryRepository.cs
│   ├── TransactionRepository.cs
│   ├── ICategoryRepository.cs
│   └── ITransactionRepository.cs
│
├── Services
│   ├── FinanceService.cs
│   └── IFinanceService.cs
│
├── appsettings.json
└── Program.cs
</pre>

<hr>

<h2>⚙️ Funcionalidades</h2>

<ul>
<li>✔ Criar transações financeiras</li>
<li>✔ Atualizar transações</li>
<li>✔ Deletar transações</li>
<li>✔ Buscar transação por ID</li>
<li>✔ Filtrar transações por mês e ano</li>
<li>✔ Dashboard financeiro</li>
<li>✔ Resumo por categoria</li>
<li>✔ Exportação de dados CSV</li>
<li>✔ Exportação de dados Excel</li>
</ul>

<hr>

<h2>📡 Endpoints da API</h2>

<h3>Transactions</h3>

<b>Listar transações</b>

<pre>GET /api/transactions</pre>

Parâmetros opcionais:

<ul>
<li><b>month</b> → filtrar por mês</li>
<li><b>year</b> → filtrar por ano</li>
<li><b>type</b> → tipo da transação</li>
</ul>

<b>Buscar por ID</b>

<pre>GET /api/transactions/{id}</pre>

<b>Criar nova transação</b>

<pre>POST /api/transactions</pre>

Exemplo de Body JSON:

<pre>
{
  "type": "E",
  "amount": 120.50,
  "date": "2025-03-01",
  "categoryId": 1,
  "description": "Salário"
}
</pre>

<b>Atualizar transação</b>

<pre>PUT /api/transactions/{id}</pre>

<b>Deletar transação</b>

<pre>DELETE /api/transactions/{id}</pre>

<hr>

<h3>Dashboard</h3>

<b>Resumo financeiro</b>

<pre>GET /api/dashboard/summary</pre>

Parâmetros:

<ul>
<li>month</li>
<li>year</li>
</ul>

<b>Resumo por categoria</b>

<pre>GET /api/dashboard/summarybycategory</pre>

<hr>

<h3>Relatórios</h3>

<b>Exportar CSV</b>

<pre>GET /api/reports/csv</pre>

Gera automaticamente:

<pre>transactions_month_year.csv</pre>

<b>Exportar Excel</b>

<pre>GET /api/reports/excel</pre>

Gera:

<pre>transactions_month_year.xlsx</pre>

Biblioteca utilizada:

<pre>ClosedXML</pre>

<hr>

<h2>🛠️ Configuração do Banco</h2>

No arquivo <b>appsettings.json</b> configure a conexão e altere para seu banco de dados:

<pre>
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=FinanceDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
</pre>
<hr>

<h2>▶️ Como Executar o Projeto</h2>

<h3>1️⃣ Clonar o repositório</h3>

<pre>
git clone https://github.com/seu-usuario/dapper-finance-api.git
</pre>

<h3>2️⃣ Abrir no Visual Studio</h3>

<pre>
DapperFinanceVideo.sln
</pre>

<h3>3️⃣ Restaurar dependências</h3>

<pre>
dotnet restore
</pre>

<h3>4️⃣ Executar o projeto</h3>

<pre>
dotnet run
</pre>

ou pressione <b>F5</b> no Visual Studio.

<hr>

<h2>🧠 Arquitetura</h2>

O projeto segue o padrão:

<b>Controller → Service → Repository</b>

<ul>
<li><b>Controllers</b> → recebem requisições HTTP</li>
<li><b>Services</b> → regras de negócio</li>
<li><b>Repositories</b> → acesso ao banco com Dapper</li>
<li><b>DTOs</b> → transferência de dados</li>
<li><b>Models</b> → entidades do sistema</li>
</ul>


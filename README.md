# 📊 Sistema Financeiro Monitor

<div align="center">

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)

**Sistema de monitoramento em tempo real de cotações de moedas e indicadores econômicos brasileiros com alertas inteligentes.**
</div>

---

## 📑 Índice

- [Sobre o Projeto](#-sobre-o-projeto)
- [Funcionalidades](#-funcionalidades)
- [Tecnologias](#-tecnologias)
- [Arquitetura](#-arquitetura)
- [Screenshots](#-screenshots)
- [Pré-requisitos](#-pré-requisitos)
- [Instalação](#-instalação)
- [Configuração](#-configuração)
- [Como Usar](#-como-usar)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [API Endpoints](#-api-endpoints)
- [Jobs Automatizados](#-jobs-automatizados)
- [Roadmap](#-roadmap)
- [Contribuindo](#-contribuindo)
- [Licença](#-licença)
- [Contato](#-contato)

---

## 🎯 Sobre o Projeto

O **Sistema Financeiro Monitor** é uma aplicação web desenvolvida para monitorar e analisar cotações de moedas (Dólar, Euro, Libra) e indicadores econômicos brasileiros (SELIC, IPCA, IGP-M) em tempo real. 

O sistema consome APIs públicas do Banco Central do Brasil, processa os dados em background e permite que usuários criem alertas personalizados, recebendo notificações por e-mail quando condições específicas são atendidas.

### 🎓 Objetivo Educacional

Este projeto foi desenvolvido aplicando os principais conceitos e padrões de arquitetura de software modernos:

- **Clean Architecture** - Separação clara de responsabilidades em camadas
- **Domain-Driven Design (DDD)** - Modelagem rica do domínio
- **SOLID Principles** - Código limpo e manutenível
- **Design Patterns** - Repository, Unit of Work, Dependency Injection

---

## ✨ Funcionalidades

### 📈 Monitoramento de Dados

- ✅ **Cotações em Tempo Real** - Dólar, Euro e Libra
- ✅ **Indicadores Econômicos** - SELIC, IPCA, IGP-M, CDI
- ✅ **Histórico Completo** - Dados históricos com filtros por período
- ✅ **Gráficos Interativos** - Visualizações com Chart.js

### 🔔 Sistema de Alertas

- ✅ **Alertas Configuráveis** - Defina condições personalizadas
- ✅ **Notificações por E-mail** - Receba alertas automaticamente
- ✅ **Múltiplos Tipos** - Cotação acima/abaixo de valor específico
- ✅ **Histórico de Disparos** - Acompanhe quando os alertas foram ativados
- ✅ **Gerenciamento Completo** - Pause, reative ou remova alertas

### 🤖 Processamento em Background

- ✅ **Atualização Automática** - Jobs recorrentes com Hangfire
- ✅ **Consumo de APIs** - Integração com Banco Central do Brasil
- ✅ **Agendamento Flexível** - Jobs configuráveis (diário, semanal, mensal)
- ✅ **Painel de Controle** - Dashboard Hangfire para monitoramento

### 📊 Dashboard e Relatórios

- ✅ **Dashboard Interativo** - Visão geral de todos os dados
- ✅ **Gráficos Dinâmicos** - Tendências e variações
- ✅ **Cards Informativos** - Indicadores principais em destaque
- ✅ **Painel Admin** - Ferramentas administrativas

---

## 🚀 Tecnologias

### Backend

- **[.NET 8](https://dotnet.microsoft.com/)** - Framework principal
- **[C# 12](https://docs.microsoft.com/pt-br/dotnet/csharp/)** - Linguagem de programação
- **[Entity Framework Core 8](https://docs.microsoft.com/pt-br/ef/core/)** - ORM
- **[SQL Server](https://www.microsoft.com/pt-br/sql-server)** - Banco de dados
- **[Hangfire](https://www.hangfire.io/)** - Background jobs
- **[AutoMapper](https://automapper.org/)** - Object mapping

### Frontend

- **[ASP.NET Core MVC](https://docs.microsoft.com/pt-br/aspnet/core/mvc/)** - Padrão MVC
- **[Bootstrap 5](https://getbootstrap.com/)** - Framework CSS
- **[Chart.js](https://www.chartjs.org/)** - Gráficos interativos
- **[jQuery](https://jquery.com/)** - Manipulação DOM e AJAX
- **[Font Awesome](https://fontawesome.com/)** - Ícones

### Arquitetura e Padrões

- **Clean Architecture** - Arquitetura em camadas
- **Domain-Driven Design (DDD)** - Modelagem do domínio
- **Repository Pattern** - Abstração de acesso a dados
- **SOLID Principles** - Princípios de design
- **Dependency Injection** - Inversão de controle

---

## 🏗️ Arquitetura

O projeto segue os princípios da **Clean Architecture** com separação clara em 4 camadas:
```
┌─────────────────────────────────────────────────────────┐
│                    Presentation Layer                    │
│                  (SistemaFinanceiro.Web)                │
│         Controllers • Views • ViewModels • JS           │
└─────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────┐
│                   Application Layer                      │
│              (SistemaFinanceiro.Application)            │
│          Services • DTOs • Interfaces • Mapping         │
└─────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────┐
│                     Domain Layer                         │
│                (SistemaFinanceiro.Domain)               │
│    Entities • Value Objects • Enums • Interfaces        │
└─────────────────────────────────────────────────────────┘
                            ↑
┌─────────────────────────────────────────────────────────┐
│                 Infrastructure Layer                     │
│            (SistemaFinanceiro.Infrastructure)           │
│  Repositories • EF Configs • External APIs • Email      │
└─────────────────────────────────────────────────────────┘
```

### Camadas

#### 🎯 Domain (Núcleo)
- **Entities**: Cotacao, IndicadorEconomico, Alerta, Usuario
- **Value Objects**: Email, ValorMonetario
- **Interfaces**: Contratos de repositórios e serviços
- **Sem dependências** de outras camadas

#### 📋 Application
- **Services**: Lógica de aplicação e casos de uso
- **DTOs**: Objetos de transferência de dados
- **Mapeamento**: Entity ↔ DTO
- **Depende**: Apenas do Domain

#### 🔧 Infrastructure
- **Repositories**: Implementação EF Core
- **External APIs**: Cliente Banco Central
- **Background Jobs**: Hangfire jobs
- **Email Service**: Envio de notificações
- **Depende**: Domain e Application

#### 🌐 Web (Presentation)
- **Controllers**: MVC e API Controllers
- **Views**: Razor Pages
- **ViewModels**: Modelos de apresentação
- **Assets**: CSS, JavaScript
- **Depende**: Application, Domain, Infrastructure

---

## 📸 Screenshots

<details>
<summary>📊 Dashboard Principal</summary>


![Dashboard]
> Visão geral com gráficos de cotações e indicadores
> <img width="1913" height="912" alt="2" src="https://github.com/user-attachments/assets/9a41f748-8e85-4088-8418-70fbe86009cf" />

</details>

<details>
<summary>💵 Cotações de Moedas</summary>

![Cotações]
> Acompanhamento de cotações em tempo real
<img width="1886" height="905" alt="3" src="https://github.com/user-attachments/assets/9999a990-3b01-44c1-919a-ea441f45b056" />
</details>

<details>
<summary>📈 Indicadores Econômicos</summary>

![Indicadores]
> Histórico de SELIC, IPCA e outros indicadores
<img width="1911" height="905" alt="4" src="https://github.com/user-attachments/assets/df3054bb-dae2-4d41-bbfe-2a150ce46c7c" />

</details>

<details>
<summary>🔔 Sistema de Alertas</summary>

![Alertas]
> Gestão completa de alertas configurados
<img width="1909" height="907" alt="5" src="https://github.com/user-attachments/assets/0d20079e-51f1-4fa1-afb7-971dd7c0874d" />

</details>

<details>
<summary>⚙️ Painel Admin</summary>

![Admin]
> Ferramentas administrativas e jobs

<img width="1875" height="834" alt="6" src="https://github.com/user-attachments/assets/fcd0902f-aeb1-4a03-ae9a-4a6ebd406b6e" />

</details>

---

## 📋 Pré-requisitos

Antes de começar, certifique-se de ter instalado:

- **[.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)** ou superior
- **[SQL Server 2019+](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)** ou SQL Server Express/LocalDB
- **[Visual Studio 2022](https://visualstudio.microsoft.com/)** (opcional, mas recomendado)
- **[Git](https://git-scm.com/)**

---

## 🔧 Instalação

### 1️⃣ Clone o Repositório
```bash
git clone https://github.com/seuusuario/SistemaFinanceiroMonitor.git
cd SistemaFinanceiroMonitor
```

### 2️⃣ Restaurar Pacotes
```bash
dotnet restore
```

### 3️⃣ Configurar Connection String

Edite o arquivo `SistemaFinanceiroMonitor.Web/appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=SistemaFinanceiroMonitor;Trusted_Connection=True;TrustServerCertificate=True;",
  "HangfireConnection": "Server=SEU_SERVIDOR;Database=SistemaFinanceiroMonitor;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 4️⃣ Criar o Banco de Dados
```bash
# Via Package Manager Console (Visual Studio)
Add-Migration InitialCreate -Project SistemaFinanceiroMonitor.Infrastructure -StartupProject SistemaFinanceiroMonitor.Web
Update-Database -Project SistemaFinanceiroMonitor.Infrastructure -StartupProject SistemaFinanceiroMonitor.Web

# Ou via CLI
dotnet ef migrations add InitialCreate --project SistemaFinanceiroMonitor.Infrastructure --startup-project SistemaFinanceiroMonitor.Web
dotnet ef database update --project SistemaFinanceiroMonitor.Infrastructure --startup-project SistemaFinanceiroMonitor.Web
```

### 5️⃣ Criar Usuário Inicial

Execute no SQL Server:
```sql
USE SistemaFinanceiroMonitor;
GO

INSERT INTO Usuarios (Nome, Email, DataCadastro, Ativo)
VALUES ('Seu Nome', 'seuemail@email.com', GETDATE(), 1);
GO
```

### 6️⃣ Executar o Projeto
```bash
dotnet run --project SistemaFinanceiroMonitor.Web
```

Ou pressione **F5** no Visual Studio.

Acesse: `https://localhost:5001`

---

## ⚙️ Configuração

### 📧 Configurar E-mail (Opcional)

Para habilitar notificações por e-mail, configure no `appsettings.json`:

#### Gmail:
```json
"EmailSettings": {
  "SmtpServer": "smtp.gmail.com",
  "SmtpPort": 587,
  "SenderName": "Sistema Financeiro Monitor",
  "SenderEmail": "seuemail@gmail.com",
  "Username": "seuemail@gmail.com",
  "Password": "suasenhadoapp",
  "EnableSsl": true
}
```

> **⚠️ Importante**: Use [Senha de App](https://myaccount.google.com/apppasswords) do Gmail, não a senha normal.

#### Outlook:
```json
"EmailSettings": {
  "SmtpServer": "smtp-mail.outlook.com",
  "SmtpPort": 587,
  "SenderEmail": "seuemail@outlook.com",
  "Username": "seuemail@outlook.com",
  "Password": "suasenha",
  "EnableSsl": true
}
```

### 🌐 APIs Externas

O sistema consome as seguintes APIs públicas:

- **[Banco Central do Brasil - PTAX](https://olinda.bcb.gov.br/olinda/servico/PTAX/)** - Cotações de moedas
- **[Banco Central - SGS](https://api.bcb.gov.br/)** - Indicadores econômicos (SELIC, IPCA)

> Não é necessário autenticação. As APIs são públicas e gratuitas.

---

## 💡 Como Usar

### 1. Popular o Banco com Dados

Acesse o painel administrativo: `https://localhost:5001/Admin`

Clique em:
- **"Atualizar Cotações"** - Importa cotações dos últimos dias
- **"Atualizar Indicadores"** - Importa SELIC e IPCA
- **"Popular Tudo"** - Executa ambos

### 2. Visualizar Dashboard

Acesse: `https://localhost:5001/Dashboard`

Veja gráficos e resumos de:
- Cotação do Dólar e Euro
- Taxa SELIC e IPCA
- Alertas ativos

### 3. Criar um Alerta

1. Acesse: `https://localhost:5001/Alertas/Criar`
2. Preencha:
   - **Tipo de Alerta**: Cotação Acima De
   - **Moeda**: Dólar
   - **Valor Gatilho**: 5.50
   - **Descrição**: "Me avise quando dólar passar de R$ 5,50"
3. Clique em **Criar Alerta**

### 4. Monitorar Jobs

Acesse o Hangfire Dashboard: `https://localhost:5001/hangfire`

Visualize e execute manualmente:
- **atualizar-cotacoes** - Atualiza cotações (seg-sex 19h)
- **atualizar-indicadores** - Atualiza indicadores (dia 1 do mês)
- **verificar-alertas** - Verifica e dispara alertas (a cada 30min)

---

## 📁 Estrutura do Projeto
```
SistemaFinanceiroMonitor/
├── src/
│   ├── SistemaFinanceiroMonitor.Domain/          # Camada de Domínio
│   │   ├── Entities/                             # Entidades
│   │   ├── ValueObjects/                         # Value Objects
│   │   ├── Enums/                                # Enumeradores
│   │   ├── Interfaces/                           # Contratos
│   │   └── Events/                               # Domain Events
│   │
│   ├── SistemaFinanceiroMonitor.Application/     # Camada de Aplicação
│   │   ├── DTOs/                                 # Data Transfer Objects
│   │   ├── Services/                             # Serviços de Aplicação
│   │   ├── Interfaces/                           # Contratos de Serviços
│   │   └── Mappings/                             # AutoMapper Profiles
│   │
│   ├── SistemaFinanceiroMonitor.Infrastructure/  # Camada de Infraestrutura
│   │   ├── Data/
│   │   │   ├── Context/                          # DbContext
│   │   │   ├── Configurations/                   # EF Configurations
│   │   │   └── Repositories/                     # Implementações
│   │   ├── ExternalServices/                     # APIs Externas
│   │   ├── EmailService/                         # Envio de E-mails
│   │   └── BackgroundJobs/                       # Hangfire Jobs
│   │
│   └── SistemaFinanceiroMonitor.Web/             # Camada de Apresentação
│       ├── Controllers/                          # MVC Controllers
│       ├── Views/                                # Razor Views
│       ├── Models/ViewModels/                    # ViewModels
│       ├── wwwroot/                              # Assets estáticos
│       │   ├── css/                              # Estilos
│       │   ├── js/                               # JavaScript
│       │   └── lib/                              # Bibliotecas
│       └── Program.cs                            # Configuração
│
├── tests/                                         # Testes (futuro)
├── .gitignore
├── README.md
└── SistemaFinanceiroMonitor.sln
```

---

## 🔌 API Endpoints

### Cotações
```http
GET /api/CotacoesApi/Ultima/{tipoMoeda}
GET /api/CotacoesApi/Historico/{tipoMoeda}?dias=30
GET /api/CotacoesApi/GraficoDolar
```

### Indicadores
```http
GET /api/IndicadoresApi/Ultimo/{tipoIndicador}
GET /api/IndicadoresApi/Historico/{tipoIndicador}?meses=12
GET /api/IndicadoresApi/GraficoSelic
```

### Dashboard
```http
GET /api/DashboardApi/Dados
```

---

## ⏰ Jobs Automatizados

| Job | Frequência | Descrição |
|-----|-----------|-----------|
| **atualizar-cotacoes** | Seg-Sex 19h | Atualiza cotações de moedas via API BCB |
| **atualizar-indicadores** | Dia 1 do mês 10h | Atualiza SELIC, IPCA via API BCB |
| **verificar-alertas** | A cada 30 min | Verifica e dispara alertas configurados |

> Configurados automaticamente no `HangfireConfig.cs`

---

## 🗺️ Roadmap

### ✅ Concluído

- [x] Clean Architecture com 4 camadas
- [x] DDD com Entities e Value Objects
- [x] Consumo de APIs do Banco Central
- [x] Sistema de alertas com e-mail
- [x] Dashboard com gráficos Chart.js
- [x] Background jobs com Hangfire
- [x] CRUD completo de alertas

### 🚧 Em Desenvolvimento

- [ ] Autenticação com ASP.NET Identity
- [ ] Testes unitários (xUnit)
- [ ] Testes de integração
- [ ] Documentação Swagger/OpenAPI
- [ ] Docker e Docker Compose

### 📝 Futuro

- [ ] Mais moedas (Bitcoin, Ethereum)
- [ ] Mais indicadores (IGP-M, CDI)
- [ ] Notificações push (SignalR)
- [ ] Exportação de relatórios (PDF, Excel)
- [ ] App Mobile (Xamarin/MAUI)
- [ ] Machine Learning para previsões
- [ ] Deploy em Azure/AWS

---

## 🤝 Contribuindo

Contribuições são bem-vindas! Siga os passos:

1. Faça um Fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/MinhaFeature`)
3. Commit suas mudanças (`git commit -m 'Adiciona MinhaFeature'`)
4. Push para a branch (`git push origin feature/MinhaFeature`)
5. Abra um Pull Request

### 📝 Padrões de Commit

- `feat:` Nova funcionalidade
- `fix:` Correção de bug
- `docs:` Documentação
- `style:` Formatação
- `refactor:` Refatoração
- `test:` Testes
- `chore:` Manutenção

---

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

## 👤 Contato

**Bruno Gonçalves**

- 💼 LinkedIn: [brunogoncalveslemos](https://linkedin.com/in/brunogoncalveslemos)
- 🐙 GitHub: [brunogoncalves99](https://github.com/brunogoncalves99)
- 📧 Email: bruno.goncalves1999@hotmail.com
- 🌐 Portfolio: [brunogoncalves](https://devbrunogoncalves.vercel.app/)

---

## 🙏 Agradecimentos

- [Banco Central do Brasil](https://www.bcb.gov.br/) - APIs públicas de dados financeiros
- [Hangfire](https://www.hangfire.io/) - Background processing
- [Chart.js](https://www.chartjs.org/) - Biblioteca de gráficos
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) - Uncle Bob

---

<div align="center">

⭐ **Se este projeto foi útil, considere dar uma estrela!** ⭐

Feito com ❤️ e ☕ por [Bruno Gonçalves](https://github.com/seuusuario)

</div>
```

---

## 📝 **ARQUIVOS ADICIONAIS RECOMENDADOS**

### **LICENSE** (MIT License)
```
MIT License

Copyright (c) 2024 Bruno Gonçalves

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

### **.gitignore** (Se ainda não tiver)
```
## Ignore Visual Studio temporary files, build results, and
## files generated by popular Visual Studio add-ons.

# User-specific files
*.suo
*.user
*.userosscache
*.sln.docstates

# Build results
[Dd]ebug/
[Dd]ebugPublic/
[Rr]elease/
[Rr]eleases/
x64/
x86/
build/
bld/
[Bb]in/
[Oo]bj/

# Visual Studio cache/options
.vs/

# MSTest test Results
[Tt]est[Rr]esult*/
[Bb]uild[Ll]og.*

# NuGet Packages
*.nupkg
**/packages/*

# Database files
*.mdf
*.ldf
*.db

# Sensitive data
appsettings.Development.json
appsettings.Production.json

# ✈️ Travel Route API

Uma API REST desenvolvida em ASP.NET Core para cadastro de rotas aéreas e cálculo do caminho mais barato entre dois aeroportos, independentemente do número de conexões.

---

## 📊 Arquitetura

O projeto segue o padrão **Clean Architecture**, dividido em 4 camadas:

```
TravelRouteAPI/
├── TravelRouteAPI.Api           # Interface REST (controllers, Swagger, DI)
├── TravelRouteAPI.Application   # Casos de uso, serviços e regras de negócio
├── TravelRouteAPI.Domain        # Entidades e interfaces puras (sem dependência externa)
├── TravelRouteAPI.Infrastructure# EF Core, banco de dados, repositórios
```

> A lógica de cálculo da melhor rota usa o algoritmo de Dijkstra e está encapsulada em `DijkstraRouteCalculator`.

---

## 💡 Tecnologias

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core + SQLite
* Docker / Docker Compose
* Swagger (Swashbuckle)
* Clean Architecture

---

## 🚀 Como Rodar

### 1. Pré-requisitos

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* [Docker](https://www.docker.com/products/docker-desktop)

---

### 2. Clonar o repositório

```bash
git clone https://github.com/seu-usuario/travel-route-api.git
cd travel-route-api
```

---

### 3. Rodar com Docker

```bash
docker-compose up --build
```

* A API ficará disponível em: `http://localhost:5000`
* Swagger: `http://localhost:5000/swagger`

> A base de dados (`routes.db`) será criada automaticamente com dados de exemplo.

---

### 4. Rodar localmente (sem Docker)

```bash
cd TravelRouteAPI.Api
dotnet ef database update
dotnet run
```

---

## 📆 Endpoints

| Método | Rota                           | Descrição                     |
| ------ | ------------------------------ | ----------------------------- |
| GET    | `/api/route`                   | Lista todas as rotas          |
| GET    | `/api/route/{id}`              | Consulta rota por ID          |
| POST   | `/api/route`                   | Cria nova rota                |
| PUT    | `/api/route/{id}`              | Atualiza rota                 |
| DELETE | `/api/route/{id}`              | Remove rota                   |
| GET    | `/api/bestroute?route=XXX-YYY` | Melhor rota entre dois pontos |

---

## 🔎 Exemplo de Consulta

```http
GET /api/bestroute?route=GRU-CDG

Resposta:
GRU - BRC - SCL - ORL - CDG ao custo de $40
```

---

## 📊 Testes

```bash
cd TravelRouteAPI.Tests
dotnet test
```

---

## 📁 Estrutura da Base de Dados

Tabela: `Routes`

| Campo         | Tipo    |
| ------------- | ------- |
| `Id`          | int     |
| `Origin`      | string  |
| `Destination` | string  |
| `Price`       | decimal |

---

## 📜 Licença

MIT License. Sinta-se livre para usar e contribuir.

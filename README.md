# Strategy Demo

Este projeto é uma API que demonstra a aplicação do padrão de design comportamental **Strategy** para calcular o custo de envio com base em diferentes estratégias de envio. A implementação segue os princípios da **Clean Architecture** e **SOLID**, garantindo um código modular, testável e de fácil manutenção.

## Sumário

- [Pré-requisitos](#pré-requisitos)
- [Como Executar](#como-executar)
  - [Clone o Projeto](#clone-o-projeto)
  - [Executar com Docker](#executar-com-docker)
  - [Executar Localmente com .NET SDK](#executar-localmente-com-net-sdk)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Como Funciona](#como-funciona)
- [Fluxo Simplificado](#fluxo-simplificado)
  - [Detalhes do fluxo](#detalhes-do-fluxo)
  - [Exemplo de requisição](#exemplo-de-requisição)
  - [Exemplo de resposta](#exemplo-de-resposta)

## Pré-requisitos

Escolha uma das seguintes opções para executar o projeto:

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/)
- [Postman](https://www.postman.com/) ou similar (para testar endpoints)

## Como Executar

Você pode executar o projeto de duas formas:

1. **Com Docker** (recomendado para evitar configurações locais)
2. **Localmente com .NET SDK** (caso já tenha o ambiente .NET configurado)

### Clone o Projeto

Clone este repositório em sua máquina local:

```bash
git clone https://github.com/kauatwn/StrategyDemo.git
```

### Executar com Docker

1. Navegue até a pasta raiz do projeto:

    ```bash
    cd StrategyDemo/
    ```

2. Construa a imagem Docker:

    ```bash
    docker build -t strategydemoapi:dev -f src/StrategyDemo.API/Dockerfile .
    ```

3. Execute o container:

    ```bash
    docker run --rm -it -p 5000:8080 --name StrategyDemo.API strategydemoapi:dev
    ```

Após executar os comandos acima, a API estará disponível em `http://localhost:5000`.

### Executar Localmente com .NET SDK

1. Navegue até o diretório da API:

    ```bash
    cd src/StrategyDemo.API/
    ```

2. Restaure as dependências do projeto:

    ```bash
    dotnet restore
    ```

3. Inicie a aplicação:

    ```bash
    dotnet run
    ```

Após rodar a aplicação, a API ficará acessível em `http://localhost:5197`.

## Estrutura do Projeto

O projeto está organizado da seguinte forma:

```plaintext
StrategyDemo/
├── src/
│   ├── StrategyDemo.API/
│   │   ├── Controllers/
│   │   │   └── OrdersController.cs
│   │   └── DTOs/
│   │       └── Responses/
│   │           └── ShippingCostResponse.cs
│   ├── StrategyDemo.Application/
│   │   ├── Contexts/
│   │   │   └── ShippingContext.cs
│   │   └── UseCases/
│   │       └── CalculateShippingCostUseCase.cs
│   ├── StrategyDemo.Domain/
│   │   ├── Entities/
│   │   │   └── Order.cs
│   │   └── Enums/
│   │       └── ShippingMethod.cs
│   └── StrategyDemo.Infrastructure/
│       ├── Factories/
│       │   └── ShippingStrategyFactory.cs
│       └── Strategies/
│           └── Shipping/
│               ├── ExpressShippingStrategy.cs
│               └── StandardShippingStrategy.cs
└── tests/
    ├── StrategyDemo.Application.Tests/
    └── StrategyDemo.Infrastructure.Tests/
```

## Como Funciona

O padrão **Strategy** encapsula algoritmos intercambiáveis, permitindo que o comportamento mude em tempo de execução. Neste projeto, ele é usado para calcular o custo de envio com base na estratégia escolhida.

1. **IShippingStrategy (Interface)**
    - Define o contrato `Calculate(Order order)` para todas as estratégias de envio.
    - Permite que qualquer estratégia concreta seja usada de forma polimórfica.

2. **StandardShippingStrategy (ConcreteStrategy)**
    - Implementa a estratégia de envio padrão com um custo fixo por peso e distância.
    - Exemplo: `custo = peso * 1.0 + distância * 0.5`

3. **ExpressShippingStrategy (ConcreteStrategy)**
    - Estratégia mais cara, porém mais rápida.
    - Exemplo: `custo = peso * 2.0 + distância * 1.0`

4. **ShippingContext (Contexto)**
    - Define qual estratégia será usada com base no método de envio informado.
    - Centraliza a lógica de seleção da estratégia e delega o cálculo à instância apropriada.

5. **ShippingStrategyFactory**
    - Responsável por criar a estratégia correta a partir de um delegate injetado.
    - A lógica de resolução por enum (`ShippingMethod`) é fornecida externamente via DI, o que mantém a fábrica simples e flexível.

## Fluxo Simplificado

1. Cálculo do Custo de Envio:

  ```mermaid
  sequenceDiagram
    participant Client
    participant API as OrdersController
    participant UseCase as CalculateShippingCostUseCase
    participant Context as ShippingContext
    participant Factory as ShippingStrategyFactory
    participant Strategy as IShippingStrategy

    Client->>+API: POST /api/Orders/CalculateShippingCost
    Note right of API: Order: {weight, distance,<br/>shippingMethod}
    
    API->>+UseCase: Execute(order)
    
    UseCase->>+Context: SetStrategy(method)
    Context->>+Factory: Create(method)
    alt Standard
        Factory-->>Context: StandardStrategy
    else Express
        Factory-->>Context: ExpressStrategy
    end
    Context-->>-UseCase: Retorna estratégia
    
    UseCase->>+Context: Calculate(order)
    Context->>+Strategy: Calculate(order)
    Strategy-->>-Context: Custo calculado
    Context-->>-UseCase: Resultado
    
    UseCase-->>-API: Retorna custo
    API-->>-Client: 200 OK
    Note left of Client: {message, cost,<br/>shippingMethod}
  ```

### Detalhes do Fluxo

1. Requisição do Cliente:
    - O cliente envia os dados do pedido (peso, distância e método de envio)

2. Seleção da Estratégia:
    - A factory cria a estratégia específica (Standard ou Express)
    - O contexto armazena a estratégia selecionada

3. Cálculo do Custo:
    - A estratégia aplica sua fórmula específica de cálculo
    - O resultado é retornado através do contexto

4. Resposta:
    - A API formata a resposta com o custo calculado
    - Inclui mensagem descritiva e método utilizado

### Exemplo de requisição

```http
POST /api/Orders/CalculateShippingCost
```

```json
{
  "weight": 100.0,
  "distance": 100.0,
  "shippingMethod": "Standard"
}
```

### Exemplo de resposta

```json
{
  "message": "The standard shipping cost is R$ 150,00",
  "cost": 150,
  "shippingMethod": "Standard"
}
```

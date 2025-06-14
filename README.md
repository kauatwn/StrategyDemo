# Strategy Demo

Este projeto é uma API que demonstra a aplicação do padrão de design comportamental **Strategy** para calcular o custo de envio com base em diferentes estratégias de envio. A implementação segue os princípios da **Clean Architecture** e **SOLID**, garantindo um código modular, testável e de fácil manutenção.

## Pré-requisitos

Escolha uma das seguintes opções para executar o projeto:

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/)
- [Postman](https://www.postman.com/) ou similar (para testar endpoints)

## Como Executar

### Com Docker

1. Navegue até a pasta raiz do projeto:

    ```bash
    cd Strategy_Demo/
    ```

2. Construa a imagem Docker:

    ```bash
    docker build -t strategydemoapi:dev -f src/Strategy_Demo.API/Dockerfile .
    ```

3. Execute o container:

    ```bash
    docker run -d -p 5000:8080 --name Strategy_Demo.API strategydemoapi:dev
    ```

Após executar os comandos acima, a API estará disponível em `http://localhost:5000`.

### Localmente com .NET SDK

1. Navegue até o diretório da API:

    ```bash
    cd src/Strategy_Demo.API/
    ```

2. Inicie a aplicação:

    ```bash
    dotnet run
    ```

Após rodar a aplicação, a API ficará acessível em `http://localhost:5197`.

## Estrutura do Projeto

O projeto está organizado da seguinte forma:

```plaintext
Strategy_Demo/
├── src/
│   ├── API/
│   │   ├── Controllers/
│   │   │   └── OrdersController.cs
│   │   └── DTOs/
│   │       └── Responses/
│   │           └── ShippingCostResponse.cs
│   ├── Application/
│   │   ├── Contexts/
│   │   │   └── ShippingContext.cs
│   │   └── UseCases/
│   │       └── CalculateShippingCostUseCase.cs
│   ├── Domain/
│   │   ├── Entities/
│   │   │   └── Order.cs
│   │   └── Enums/
│   │       └── ShippingMethod.cs
│   └── Infrastructure/
│       ├── Factories/
│       │   └── ShippingStrategyFactory.cs
│       └── Strategies/
│           └── Shipping/
│               ├── ExpressShippingStrategy.cs
│               └── StandardShippingStrategy.cs
└── tests/
    ├── Application.Tests/
    └── Infrastructure.Tests/
```

## Como Funciona

O padrão Strategy é utilizado para encapsular diferentes algoritmos de cálculo de custo de envio em classes separadas. Dependendo da estratégia escolhida pelo usuário, o sistema seleciona a estratégia apropriada e calcula o custo de envio.

### Exemplo de requisição

```http
POST /api/Orders/CalculateShippingCost
content-type: application/json

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

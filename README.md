# Strategy Demo

Este projeto é uma API que demonstra a aplicação do padrão de design comportamental **Strategy** para calcular o custo de envio com base em diferentes estratégias de envio. A implementação segue os princípios da **Clean Architecture** e **SOLID**, garantindo um código modular, testável e de fácil manutenção.

## Estrutura do Projeto

O projeto está organizado da seguinte forma:

```plaintext
Strategy_Demo/
├── src/
│   ├── API/
│   │   └── Controllers/
│   │       └── OrdersController.cs   # Endpoint da API
│   ├── Application/                  # Casos de uso e regras de negócio
│   │   ├── Contexts/
│   │   │   └── ShippingContext.cs    # Contexto do Strategy
│   │   ├── DTOs/
│   │   │   └── ShippingCostResponse.cs
│   │   └── UseCases/
│   │       └── CalculateShippingCostUseCase.cs
│   ├── Domain/                       # Entidades e interfaces core
│   └── Infrastructure/               # Implementações concretas
│       ├── Factories/
│       │   └── ShippingStrategyFactory.cs  # Factory das estratégias
│       └── Strategies/
│           └── Shipping/
│               ├── ExpressShippingStrategy.cs
│               └── StandardShippingStrategy.cs
└── tests/
    ├── Application.UnitTests/        # Testes dos casos de uso
    └── Infrastructure.UnitTests/     # Testes das estratégias e factory
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

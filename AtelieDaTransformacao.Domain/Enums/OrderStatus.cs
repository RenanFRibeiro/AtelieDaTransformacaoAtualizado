namespace AtelieDaTransformacao.Domain.Enums;

public enum OrderStatus
{
    Criado = 0,
    Pendente = 1,
    Aprovado = 2,
    Separacao = 3,
    Faturado = 4,
    Enviado = 5,
    Entregue = 6
}

public static class OrderStatusExtensions
{
    public static string ToDisplayName(this OrderStatus status)
    {
        return status switch
        {
            OrderStatus.Criado => "Criado",
            OrderStatus.Pendente => "Pendente",
            OrderStatus.Aprovado => "Aprovado",
            OrderStatus.Separacao => "Separação",
            OrderStatus.Faturado => "Faturado",
            OrderStatus.Enviado => "Enviado",
            OrderStatus.Entregue => "Entregue",
            _ => status.ToString()
        };
    }

    public static OrderStatus? GetNext(this OrderStatus status)
    {
        return status switch
        {
            OrderStatus.Criado => OrderStatus.Pendente,
            OrderStatus.Pendente => OrderStatus.Aprovado,
            OrderStatus.Aprovado => OrderStatus.Separacao,
            OrderStatus.Separacao => OrderStatus.Faturado,
            OrderStatus.Faturado => OrderStatus.Enviado,
            OrderStatus.Enviado => OrderStatus.Entregue,
            _ => null
        };
    }
}
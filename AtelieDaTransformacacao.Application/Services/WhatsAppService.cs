using System.Text;
using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Application.ViewModels;

namespace AtelieDaTransformacao.Application.Services;

public sealed class WhatsAppService : IWhatsAppService
{
    // Substitua pelo número real do vendedor: 55 + DDD + número, somente dígitos.
    private const string MerchantPhoneNumber = "5511999999999";

    public string GenerateProductInquiryLink(string productName, decimal price)
    {
        var message = $"Olá! Fiquei interessado no produto *{productName}* no valor de R$ {price:N2}. Gostaria de combinar o pagamento e a entrega!";
        return BuildLink(message);
    }

    public string GenerateCartLink(CartViewModel cart)
    {
        if (cart is null || cart.Items.Count == 0)
            return string.Empty;

        var message = new StringBuilder();
        message.AppendLine("Olá! Gostaria de realizar uma compra pelo Ateliê da Transformação.");
        message.AppendLine();
        message.AppendLine("*Produtos selecionados:*");
        message.AppendLine();

        foreach (var item in cart.Items)
        {
            message.AppendLine($"• {item.Title}");
            message.AppendLine($"  Quantidade: {item.Quantity}");
            message.AppendLine($"  Valor unitário: R$ {item.Price:N2}");
            message.AppendLine($"  Subtotal: R$ {item.Subtotal:N2}");
            message.AppendLine();
        }

        message.AppendLine($"*Total estimado: R$ {cart.Total:N2}*");
        message.AppendLine();
        message.AppendLine("Gostaria de continuar a compra e combinar pagamento e entrega.");

        return BuildLink(message.ToString());
    }

    private static string BuildLink(string message)
    {
        var encoded = Uri.EscapeDataString(message);
        return $"https://wa.me/{MerchantPhoneNumber}?text={encoded}";
    }
}

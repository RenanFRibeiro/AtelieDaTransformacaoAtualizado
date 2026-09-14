using AtelieDaTransformacao.Application.ViewModels;

namespace AtelieDaTransformacao.Application.Interfaces;

public interface IWhatsAppService
{
    string GenerateProductInquiryLink(string productName, decimal price);

    string GenerateCartLink(CartViewModel cart);

    string GenerateQuoteLink(
        string name,
        string phone,
        string productType,
        string? measurements,
        string? material,
        string description);
}
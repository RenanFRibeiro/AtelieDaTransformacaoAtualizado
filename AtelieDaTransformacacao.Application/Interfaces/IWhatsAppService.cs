using AtelieDaTransformacao.Application.ViewModels;

namespace AtelieDaTransformacao.Application.Interfaces;

public interface IWhatsAppService
{
    string GenerateProductInquiryLink(string productName, decimal price);
    string GenerateCartLink(CartViewModel cart);
}

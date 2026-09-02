using AtelieDaTransformacao.Domain.Entities;

namespace AtelieDaTransformacao.Domain.Interfaces;

public interface IFeedbackRepository
{
    Task<Feedback?> GetByUserOrderProductAsync(
        string usuarioId,
        int pedidoId,
        int produtoId);

    Task<IReadOnlyList<Feedback>> GetForOrderAsync(
        string usuarioId,
        int pedidoId);

    Task<IReadOnlyList<Feedback>> GetPublishedAsync(
        int limit = 12);

    Task AddAsync(Feedback feedback);
}
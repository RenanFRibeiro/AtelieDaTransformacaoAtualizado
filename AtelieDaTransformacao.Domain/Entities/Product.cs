using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AtelieDaTransformacao.Domain.Entities
{
    /// <summary>
    /// BASE - Entidade de Produtos
    /// </summary>
    public class Product
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Image { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public bool IsFeatured { get; set; }

        public int StockQuantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ProductCategory? Category { get; set; }

        [NotMapped]
        public bool IsAvailable =>
            StockQuantity > 0;
    }

    /// <summary>
    /// Entidade de clientes (representa a tabela Customers).
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
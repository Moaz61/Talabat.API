using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities.Basket;

namespace Talabat.APIs.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; } = null!;
        
        [Required]
        public List<BasketItemDto> Items { get; set; } = null!;
    }
}

using System.Collections.Generic;

namespace Server.DTOs
{
    public class CheckoutInitRequestDto
    {
        public string BasketId { get; set; }
        public List<CheckoutItemDto> Items { get; set; } = new();
        public string CustomerId { get; set; }
        public string Email { get; set; }
        public string GsmNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdentityNumber { get; set; }
        public AddressDto Billing { get; set; }
        public AddressDto Shipping { get; set; }
    }

    public class CheckoutItemDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category1 { get; set; }
        public string ItemType { get; set; } // "PHYSICAL" | "VIRTUAL"
        public decimal Price { get; set; }   // tekil kalem tutarı
    }

    public class AddressDto
    {
        public string ContactName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string ZipCode { get; set; }
    }

    public class CheckoutInitResponseDto
    {
        public string Status { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ConversationId { get; set; }
        public string Token { get; set; }
        public string CheckoutFormContent { get; set; }
    }
    public class CheckoutResultResponseDto
    {
        public string Status { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentId { get; set; }
        public string BasketId { get; set; }
        public string Price { get; set; }
        public string PaidPrice { get; set; }
        public string Installment { get; set; }
        public string CardAssociation {get; set; }
        public string CardFamily { get; set; }
    }
}

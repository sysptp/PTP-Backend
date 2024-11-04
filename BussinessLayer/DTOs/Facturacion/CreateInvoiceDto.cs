namespace BussinessLayer.DTOs.Facturacion
{
    public class CreateInvoiceDto
    {
        public int ClientId { get; set; }
        public string? Comment { get; set; }
        public int TransactionId { get; set; }
        public int PaymentMethodId { get; set; }
        public List<ProductInvoiceDto>? Products { get; set; }
        public int BussinesId { get; set; }
        public int UserId { get; set; }
        public int InvoiceTypeId { get; set; }
    }
    public class ProductInvoiceDto
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal? Itbis { get; set; }
    }
}

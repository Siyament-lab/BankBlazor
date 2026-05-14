namespace BankBlazor.API.DTOs
{
    public class TransferPostDto
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal Amount { get; set; }
    }
}

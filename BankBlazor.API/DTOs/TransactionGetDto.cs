namespace BankBlazor.API.DTOs
{
    public class TransactionGetDto
    {
        public int AccountId { get; set; }
        public DateOnly Date { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Operation { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
    }
}

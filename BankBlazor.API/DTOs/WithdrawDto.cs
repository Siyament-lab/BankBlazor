namespace BankBlazor.API.DTOs
{
    public class WithdrawDto
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Operation { get; set; } = "Withdrawal in Cash";
    }
}

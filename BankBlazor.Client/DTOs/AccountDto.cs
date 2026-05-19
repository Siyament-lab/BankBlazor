namespace BankBlazor.Client.DTOs
{
    public class AccountDto

    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public string Frequency { get; set; } = string.Empty;
        public DateOnly Created { get; set; }
    }
}

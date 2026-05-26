
namespace BankBlazor.API.Services
{
    public class TransactionValidationService
    {
        public string? ValidateAmount ( decimal amount )
        {
            if ( amount <= 0 )
                return "Amount must be greater than zero.";
            //Max gräns 10 000 / transaktion.
            if( amount > 10000 )
                return "Amount exceeds the maximum limit 10 000 kr.";
            return null;
        }
        public string? ValidateTransfer( int fromAccountId, int toAccountId )
        {
            if ( fromAccountId == toAccountId )
                return "Cannot transfer to the same account.";
            return null;
        }
        public string? ValidateOperation ( string operation )
        {
            if (!ValidWithdrawOpreation.Contains (operation))
                return "Invalid operation type";

            return null;
        }

        public static readonly string[] ValidWithdrawOpreation =
        {
             "Withdrawal in Cash",
             "Remittance to Another Bank",
            "Collection from Another Bank"
        };
    }
}

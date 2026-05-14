namespace BankBlazor.API.DTOs
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string Givenname { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Emailaddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Streetaddress { get; set; } = string.Empty;
        public string Zipcode { get; set; } = string.Empty;
        public string Telephonenumber { get; set; } = string.Empty;
        public DateOnly Birthday { get; set; }
        public List<AccountDto> Accounts { get; set; } = new List<AccountDto> ();
    }
}

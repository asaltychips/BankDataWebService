
using bankdatawebservice.Models;


namespace BankDataWebService.Services
{
    public class TemporaryAccountStore
    {
        public List<Account> Accounts { get; } = new();

        public int NextId { get; set; } = 1;
    }
}
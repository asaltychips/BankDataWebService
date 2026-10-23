
using System.ComponentModel.DataAnnotations;

namespace BankDataWebService.DTOs
{
    public class UpdateAccountDto
    {
        [Required]
        public string AccountType { get; set; } = "";
    }
}
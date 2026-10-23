
using System.ComponentModel.DataAnnotations;

namespace BankDataWebService.DTOs
{
    public class CreateAccountDto
    {
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        [Required]
        public string AccountType { get; set; } = "";
    }
}
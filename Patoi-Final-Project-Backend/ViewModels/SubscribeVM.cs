using System.ComponentModel.DataAnnotations;

namespace Patoi_Final_Project_Backend.ViewModels
{
    public class SubscribeVM
    {
        [Required(ErrorMessage = "Please enter your email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(maximumLength: 60)]
        public string Email { get; set; }
    }
}

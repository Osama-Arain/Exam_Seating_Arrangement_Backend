using System.ComponentModel.DataAnnotations;

namespace ESA.Views.Shared
{
    
    public class LoginPayloadViewModel {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        

    }
}
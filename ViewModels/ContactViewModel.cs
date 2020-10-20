using System.ComponentModel.DataAnnotations;

namespace DutchTreat.ViewModels {
    public class ContactViewModel {
        [Required (ErrorMessage = "A girl needs a name")]
        [MinLength (5)]
        public string Name { get; set; }

        [Required]
        [EmailAddress (ErrorMessage = "Must be an email")]
        public string Email { get; set; }

        [Required]
        [MinLength (10)]
        public string Subject { get; set; }

        [Required]
        [StringLength (250)]
        public string Message { get; set; }
    }
}
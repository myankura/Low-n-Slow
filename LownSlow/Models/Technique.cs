using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LownSlow.Models
{
    public class Technique
    {

        [Key]
        [Display(Name = "Technique Id")]
        public int TechniqueId { get; set; }

        [Required(ErrorMessage = "Please give the technique a name.")]
        [Display(Name = "Technique")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please give the technique a description")]
        public string Description { get; set; }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}

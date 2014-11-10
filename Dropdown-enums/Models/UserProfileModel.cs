using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Dropdowns.Models
{
    public class UserProfileModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        // This property will hold user-selected industry
        [Required]
        [Display(Name = "Industry")]
        public Industry Industry { get; set; }

        // This property will hold all available industries for selection
        public IEnumerable<SelectListItem> Industries { get; set; }
    }
}
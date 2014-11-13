using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Dropdowns.Models
{
    public class UserProfileModel
    {
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        // This property holds user-selected industry
        [Required]
        [Display(Name = "Industry")]
        public Industry Industry { get; set; }

        // This property holds all available industries for selection
        public IEnumerable<SelectListItem> Industries { get; set; }
    }
}
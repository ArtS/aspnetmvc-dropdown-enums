using System.Collections.Generic;
using System.Web.Mvc;
using Dropdowns.Models;

namespace Dropdowns.Controllers
{
    public class ProfileController : Controller
    {
        //
        // 1. Action method for displaying a 'User Profile' page
        //
        public ActionResult UserProfile()
        {
            var model = new UserProfileModel();

            // Create a list of SelectListItems from Industries so these can be rendered on the page
            // under the drop down
            model.Industries = GetSelectListItems();

            return View(model);
        }

        //
        // 2. Action method for handling user-entered data when 'Update' button is pressed.
        //
        [HttpPost]
        public ActionResult UserProfile(UserProfileModel model)
        {
            // Set these states on the model. We need to do this because
            // only selected in the DropDownList value is posted back, not the whole
            // list of states
            model.Industries = GetSelectListItems();

            // In case everything is fine - i.e. both "Name" and "Industry" are entered/selected,
            // redirect user to the "ViewProfile" page, and pass the user object along via Session
            if (ModelState.IsValid)
            {
                Session["UserProfileModel"] = model;
                return RedirectToAction("ViewProfile");
            }

            // Something is not right - so render the registration page again,
            // keeping the data user has entered by supplying the model.
            return View(model);
        }

        //
        // 3. Action method for displaying 'ViewProfile' page
        //
        public ActionResult ViewProfile()
        {
            // Get user profile information from the session
            var model = Session["UserProfileModel"] as UserProfileModel;

            // Display ViewProfile.html page that shows Name and selected state.
            return View(model);
        }

        // This is one of the most important parts in the whole example.
        // This function takes a list of strings and returns a list of SelectListItem objects.
        // These objects are going to be used later in the UserProfile.html template to render the
        // DropDownList.
        private IEnumerable<SelectListItem> GetSelectListItems()
        {
            // Create an empty list to hold result of the operation
            var selectList = new List<SelectListItem>();

            // For each string in the 'elements' variable, create a new SelectListItem object
            // that has both it's Value and Text properties set to a particular state.
            // This will result in MVC rendering each item as:
            //     <option value="State Name">State Name</option>
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element,
                    Text = element
                });
            }

            return selectList;
        }
    }
}

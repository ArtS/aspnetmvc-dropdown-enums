using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

            // In case everything is fine - i.e. both "FirstName" and "Industry" are entered/selected,
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

            // Display ViewProfile.html page that shows FirstName and selected state.
            return View(model);
        }

        private IEnumerable<SelectListItem> GetSelectListItems()
        {
            var selectList = new List<SelectListItem>();
            
            var enumType = typeof(Industry);
            var enumValues = Enum.GetValues(enumType) as Industry[];
            if (enumValues == null)
                return null;

            foreach (var enumValue in enumValues)
            {
                var memberInfo = enumType.GetMember(enumValue.ToString());
                if (memberInfo.Length != 1)
                    continue;
                
                var displayAttribute = memberInfo[0].GetCustomAttributes(typeof (DisplayAttribute), false) as DisplayAttribute[];
                if (displayAttribute == null || displayAttribute.Length != 1)
                    continue;

                selectList.Add(new SelectListItem
                {
                    Value = enumValue.ToString(),
                    Text = displayAttribute[0].Name
                });
            }

            return selectList;
        }
    }
}

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
            var model = Session["UserProfileModel"] ?? new UserProfileModel();

            return View(model);
        }

        //
        // 2. Action method for handling user-entered data when 'Update' button is pressed.
        //
        [HttpPost]
        public ActionResult UserProfile(UserProfileModel model)
        {
            // In case everything is fine - i.e. both "FirstName" and "Industry" are entered/selected,
            // redirect user to the "ViewProfile" page, and pass the user object along via Session
            if (ModelState.IsValid)
            {
                Session["UserProfileModel"] = model;
                return RedirectToAction("ViewProfile");
            }

            // Something is not right - so render the profile page again,
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
            if (model == null)
                return RedirectToAction("UserProfile");

            model.IndustryName = GetSelectedIndustryName(model.Industry);

            // Display ViewProfile.html page that shows FirstName and selected state.
            return View(model);
        }

        private string GetSelectedIndustryName(Industry industry)
        {
            var enumType = typeof(Industry);
            var enumValues = Enum.GetValues(enumType) as Industry[];
            if (enumValues == null)
                return null;

            var memberInfo = enumType.GetMember(industry.ToString());
            if (memberInfo.Length != 1)
                return null;

            var displayAttribute = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];
            if (displayAttribute == null || displayAttribute.Length != 1)
                return null;

            return displayAttribute[0].Name;
        }
    }
}

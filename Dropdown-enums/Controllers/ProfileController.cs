using System;
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
            // Get existing user profile object from the session or create a new one
            var model = Session["UserProfileModel"] ?? new UserProfileModel();

            return View(model);
        }

        //
        // 2. Action method for handling user-entered data when 'Update' button is pressed.
        //
        [HttpPost]
        public ActionResult UserProfile(UserProfileModel model)
        {
            // In case everything is fine - i.e. both "First name", "Last name" and "Industry" are entered/selected,
            // redirect user to the "ViewProfile" page, and pass the user profile object along via Session
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

            // Get the description of the currently selected industry from the 
            // [Display] attribute of Industry enum
            model.IndustryName = GetIndustryName(model.Industry);

            return View(model);
        }

        /// <summary>
        /// So we can show nicely formatted text in the UI this function retrieves the
        /// value from [Display(Name="Editorial & Writing")] attribute.
        /// </summary>
        /// <param name="value">Value from Industry enum</param>
        /// <returns>Value of the "Name" property on Display attribute</returns>
        private string GetIndustryName(Industry value)
        {
            // Get the MemberInfo object for supplied enum value
            var memberInfo = value.GetType().GetMember(value.ToString());
            if (memberInfo.Length != 1)
                return null;

            // Get DisplayAttibute on the supplied enum value
            var displayAttribute = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];
            if (displayAttribute == null || displayAttribute.Length != 1)
                return null;

            return displayAttribute[0].Name;
        }
    }
}
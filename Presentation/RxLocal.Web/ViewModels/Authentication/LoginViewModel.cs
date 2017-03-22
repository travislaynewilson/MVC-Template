using System.ComponentModel.DataAnnotations;
using RxLocal.Web.Framework.Mvc;

namespace RxLocal.Web.ViewModels.Authentication
{
    public class LoginViewModel : BaseViewModel
    {
        [Display(Name = "Username"), Required]
        public string LoginName { get; set; }

        [Display(Name = "Password"), Required]
        public string Password { get; set; }
    }
}
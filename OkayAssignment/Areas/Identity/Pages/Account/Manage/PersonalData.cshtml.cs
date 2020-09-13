using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OkayAssignment.Data;

namespace OkayAssignment.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        public bool IsAdmin { get; set; }
        private readonly UserManager<OkayAssignmentUser> _userManager;
        private readonly ILogger<PersonalDataModel> _logger;

        public PersonalDataModel(
            UserManager<OkayAssignmentUser> userManager,
            ILogger<PersonalDataModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            else
            {

                IsAdmin = user.IsAdmin;
            }

            return Page();
        }
    }
}
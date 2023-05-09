using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JordnærCase2023.Pages.Shift
{
    public class CreateModel : PageModel
    {
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _shiftService.CreateShiftAsync(ShiftCreate);
            return RedirectToPage("GetAllShifts");
        }
    }
}

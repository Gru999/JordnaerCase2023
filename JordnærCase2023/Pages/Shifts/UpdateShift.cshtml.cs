using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JordnærCase2023.Pages.Shift
{
    public class UpdateModel : PageModel
    {
        public void OnGet()
        {
        }
        public async Task OnPostAsync()
        {
            await _shiftService.UpdateShiftAsync(ShiftUpdate);
        }
    }
}

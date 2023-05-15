using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JordnærCase2023.Pages.Shifts
{
    public class UpdateModel : PageModel
    {
        [BindProperty]
        public Shift ShiftUpdate { get; set; }
        public IShiftService _shiftService { get; set; }
        public UpdateModel(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        public async Task OnGetAsync(int shiftId)
        {
            ShiftUpdate = await _shiftService.GetShiftsByIdAsync(shiftId);
        }
        public async Task<IActionResult> OnPostAsync(int shiftTypeId)
        {
            bool ok = await _shiftService.UpdateShiftAsync(ShiftUpdate, shiftTypeId);
            if (ok)
            {
                return RedirectToPage("GetAllShifts");
            }
            else
            {
                return Page();
            }
        }
    }
}

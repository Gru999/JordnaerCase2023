using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JordnærCase2023.Pages.Shifts
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Shift ShiftDelete { get; set; }
        public IShiftService _shiftService { get; set; }
        public DeleteModel(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        public async Task OnGetAsync(int shiftId)
        {
            ShiftDelete = await _shiftService.GetShiftsByIdAsync(shiftId);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _shiftService.DeleteShiftAsync(ShiftDelete.ShiftID);
            return RedirectToPage("GetAllShifts");
        }

    }
}

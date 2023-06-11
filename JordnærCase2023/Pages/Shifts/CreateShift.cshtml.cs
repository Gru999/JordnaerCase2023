using JordnærCase2023.Data;
using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JordnærCase2023.Pages.Shifts
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Shift ShiftCreate { get; set; }
        public IShiftService _shiftService { get; set; }
        public IShiftTypeService _shiftTypeService { get; set; }

        public CreateModel(IShiftService shiftService, IShiftTypeService shiftTypeService)
        {
            _shiftService = shiftService;
            _shiftTypeService = shiftTypeService;
        }
        public List<ShiftType> ShiftTypes { get; set; }
        [BindProperty]
        public int SelectedShiftType { get; set; }

        public void OnGet()
        {
            ShiftTypes = _shiftTypeService.GetAllShiftTypes();
        }
        public async Task<IActionResult> OnPostAsync(string Shift_Type)
        {
            ShiftCreate.ShiftType = SelectedShiftType;
            await _shiftService.CreateShiftAsync(ShiftCreate);
            return RedirectToPage("GetAllShifts");
        }
    }
}

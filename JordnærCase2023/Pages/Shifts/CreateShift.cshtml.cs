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
            //if (Shift_Type == "BagerVagt")
            //    ShiftCreate.ShiftType = 1;
            //if (Shift_Type == "CafeVagt")
            //    ShiftCreate.ShiftType = 2;
            //if (Shift_Type == "BagerVagtFøl")
            //    ShiftCreate.ShiftType = 3;
            //if (Shift_Type == "CafeVagtFøl")
            //    ShiftCreate.ShiftType = 4;
            await _shiftService.CreateShiftAsync(ShiftCreate);
            return RedirectToPage("GetAllShifts");
        }
    }
}

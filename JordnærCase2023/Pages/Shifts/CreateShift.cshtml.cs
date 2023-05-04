using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;

namespace JordnærCase2023.Pages.Shifts
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Shift ShiftCreate { get; set; }
        public IShiftService _shiftService { get; set; }
        public CreateModel(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        public void OnGet()
        {

        }
        public async Task OnPostAsync()
        {
            await _shiftService.CreateShiftAsync(ShiftCreate);
        }
    }
}

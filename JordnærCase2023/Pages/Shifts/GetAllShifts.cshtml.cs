using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JordnærCase2023.Pages.Shifts
{
    public class GetAllShiftsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        public List<Shift> Shifts { get; set; }
        public IShiftService _shiftService { get; set; }
        public GetAllShiftsModel(IShiftService shiftService)
        {
            //FilterCriteria = "";
            _shiftService = shiftService;
        }

        public async Task OnGetAsync()
        {
            //if ( !FilterCriteria == null)
            //{
            //    Shifts = await _shiftService.GetShiftsByIdAsync(FilterCriteria);
            //}
            //else
            {
                Shifts = await _shiftService.GetAllShiftsAsync();
            }
        }
    }
}

using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;
using JordnærCase2023.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;

namespace JordnærCase2023.Pages.Shifts
{
    public class GetAllShiftsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        public List<Shift> Shifts { get; set; }
        public IShiftService _shiftService { get; set; }
        public IShiftTypeService _shiftTypeService { get; set; }
        public IUserLoginService _userService { get; set; }
        private IHttpContextAccessor httpContext;
        public Member LoggedMember { get; set; }
        public GetAllShiftsModel(IShiftService shiftService, IShiftTypeService shiftTypeService, IUserLoginService userService, IHttpContextAccessor httpContext)
        {
            _shiftService = shiftService;
            _userService = userService;
            _shiftTypeService = shiftTypeService;
            this.httpContext = httpContext;
        }

        public async Task OnGetAsync()
        {
            string Email = httpContext.HttpContext.Session.GetString("Email");
            LoggedMember = _userService.GetLoggedMember(Email);
            Shifts = await _shiftService.GetAllShiftsAsync();

        }
    }
}

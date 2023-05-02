using JordnærCase2023.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JordnærCase2023.Pages.Shifts
{
    public class UpdateModel : PageModel
    {
        public Shift ShiftUpdate { get; set; }
        public void OnGet()
        {
        }
    }
}

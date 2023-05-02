using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;

namespace JordnærCase2023.Pages.Shifts
{
    public class CreateModel : PageModel
    {
        public Shift Shift { get; set; }

        public void OnGet()
        {

        }
    }
}

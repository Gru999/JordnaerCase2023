using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JordnærCase2023.Interfaces;
using JordnærCase2023.Models;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Logging;
using Azure;


namespace JordnærCase2023.Pages.Items
{
    public class CreateItemModel : PageModel
    {

        private IWebHostEnvironment webHostEnvironment;
        [BindProperty]
        public Item Item { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }




        private IItemService itemService;
        public CreateItemModel(IItemService itemservice, IWebHostEnvironment webHost)
        {
            webHostEnvironment = webHost;
            itemService = itemservice;
        }



        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
       
                if (Photo != null)
                {
                    if (Item.ItemImg != null)
                    {
                        string filePath = Path.Combine(webHostEnvironment.WebRootPath, "/images/ItemImages", Item.ItemImg);
                        System.IO.File.Delete(filePath);
                    }

                    Item.ItemImg = ProcessUploadedFile();
                }
                await itemService.CreateItemAsync(Item);
                return RedirectToPage("ShowAllItems");
          
           
        }




        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/ItemImg");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                   Photo.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }

    }
}


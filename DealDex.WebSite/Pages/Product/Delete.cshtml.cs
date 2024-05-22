using DealDex.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DealDex.WebSite.Services;
using DealDex.WebSite.Services.Interfaces;

namespace DealDex.WebSite.Pages.Product
{
    public class Delete : PageModel
    {
        private readonly IProductService _service;

        [BindProperty]
        public ProductCategoryDtoAdd ProductCategoryDto { get; set; }

        public List<string> Errors { get; set; } = new List<string>();

        public Delete(IProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            ProductCategoryDto = new ProductCategoryDtoAdd();
            var response = await _service.GetById(id);
            ProductCategoryDto = response.Data;

            if (ProductCategoryDto == null)
            {
                return RedirectToPage("/Error");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var response = await _service.DeleteAsync(ProductCategoryDto.id);
            return RedirectToPage("./List");
        }
    }
}
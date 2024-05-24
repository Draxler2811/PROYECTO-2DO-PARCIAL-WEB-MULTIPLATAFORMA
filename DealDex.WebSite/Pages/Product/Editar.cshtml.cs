using DealDex.Api.Dto;
using DealDex.Core.Http;
using DealDex.WebSite.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DealDex.WebSite.Pages.Product
{
    public class Editar : PageModel
    {
        [BindProperty] public ProductCategoryDtoAdd ProductCategoryDto { get; set; }

        public List<string> Errors { get; set; } = new List<string>();

        private readonly IProductService _service;

        public Editar(IProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            ProductCategoryDto = new ProductCategoryDtoAdd();
            if (id.HasValue)
            {
                var response = await _service.GetById(id.Value);
                ProductCategoryDto = response.Data;
            }

            if (ProductCategoryDto == null)
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Response<ProductCategoryDtoAdd> response;

            // Actualizacion
            if (ProductCategoryDto.id > 0)
            {
                response = await _service.UpdateAsync(ProductCategoryDto);
            }
            else
            {
                // Insercion
                response = await _service.SaveAsync(ProductCategoryDto);
            }

            Errors = response.Errors;

            if (Errors.Count > 0)
            {
                return Page();
            }

            return RedirectToPage("./List");
        }
    }
}
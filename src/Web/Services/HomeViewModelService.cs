using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Interfaces;

namespace Web.Services
{
    public class HomeViewModelService : IHomeViewModelService
    {
        private readonly IRepository<Product> _productRepo;
        private readonly IRepository<Category> _categoryRepo;
        private readonly IRepository<Brand> _brandRepo;

        public HomeViewModelService(IRepository<Product> productRepo, IRepository<Category> categoryRepo, IRepository<Brand> brandRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _brandRepo = brandRepo;
        }

        public async Task<HomeViewModel> GetHomeViewModelAsync(int? categoryId, int? brandId)
        {
            var specProducts = new ProductFilterSpecification(categoryId, brandId);
            var products = await _productRepo.GetAllAsync(specProducts);

            var vm = new HomeViewModel()
            {
                Products = products.Select(x => 
                    new ProductViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price,
                        PictureUri = x.PictureUri,
                    }
                ).ToList(),
                Categories = (await _categoryRepo.GetAllAsync()).Select(x => 
                    new SelectListItem(x.Name, x.Id.ToString())).ToList(),
                Brands = (await _brandRepo.GetAllAsync()).Select(x =>
                    new SelectListItem(x.Name, x.Id.ToString())).ToList(),
                CategoryId = categoryId,
                BrandId = brandId
            };

            return vm;
        }
    }
}

using Web.Interfaces;

namespace Web.Services
{
    public class HomeViewModelService : IHomeViewModelService
    {
        private readonly IRepository<Product> _productRepo;

        public HomeViewModelService(IRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<HomeViewModel> GetHomeViewModelAsync()
        {
            var vm = new HomeViewModel()
            {
                Products = (await _productRepo.GetAllAsync()).Select(x => 
                    new ProductViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price,
                        PictureUri = x.PictureUri,
                    }
                ).ToList()
            };

            return vm;
        }
    }
}

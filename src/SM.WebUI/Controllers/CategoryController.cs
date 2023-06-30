using Microsoft.AspNetCore.Mvc;
using SM.Application.Interfaces;

namespace SM.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IRepositoryWrapper _repository;

        public CategoryController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _repository.CategoryRepository.GetAllCategorysAsync();
            return View(result);
        }
    }
}

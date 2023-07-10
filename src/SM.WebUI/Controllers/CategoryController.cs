using Microsoft.AspNetCore.Mvc;
using SM.Application.Interfaces;
using SM.Domain.Entities;

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
            return View(await _repository.CategoryRepository.GetAllCategorysAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _repository.CategoryRepository.Add(category);
            await _repository.SaveAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


    }
}

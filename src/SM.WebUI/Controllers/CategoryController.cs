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

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _repository.CategoryRepository.Update(category);
            await _repository.SaveAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var category = await _repository.CategoryRepository.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                return View(category);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, $"Something went wrong inside GetOwnerById action: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _repository.CategoryRepository.Delete(category);
            await _repository.SaveAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var category = await _repository.CategoryRepository.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                _repository.CategoryRepository.Delete(category);
                await _repository.SaveAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong inside GetOwnerById action: {ex.Message}");
            }
        }
    }
}

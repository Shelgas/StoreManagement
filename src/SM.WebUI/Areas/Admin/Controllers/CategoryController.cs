using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SM.Application.Interfaces;
using SM.Application.Models.DTO;
using SM.Domain.Entities;

namespace SM.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public CategoryController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _repository.CategoryRepository.GetAllCategorysAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryDTO);
            }
            _repository.CategoryRepository.Add(_mapper.Map<Category>(categoryDTO));
            await _repository.SaveAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryDTO);
            }
            _repository.CategoryRepository.Update(_mapper.Map<Category>(categoryDTO));
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
                return View(_mapper.Map<CategoryDTO>(category));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong inside GetOwnerById action: {ex.Message}");
            }
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
                return StatusCode(500, $"Something went wrong inside GetCategoryById action: {ex.Message}");
            }
        }
    }
}

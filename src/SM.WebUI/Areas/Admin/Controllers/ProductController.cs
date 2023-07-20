using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SM.Application.Interfaces;
using SM.Application.Models.DTO;
using SM.Domain.Entities;

namespace SM.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public ProductController(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<List<ProductCreateDTO>>(await _repository.ProductRepository.GetAllProductsAsync()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDTO productDTO)
        {
            ViewBag.CategoryList = (await _repository.CategoryRepository.GetAllCategorysAsync()).Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            //if (!ModelState.IsValid)
            //    return View(productDTO);
            var product = _mapper.Map<Product>(productDTO);
        

            _repository.ProductRepository.Add(product);
            await _repository.SaveAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryList = (await _repository.CategoryRepository.GetAllCategorysAsync()).Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            _repository.ProductRepository.Update(product);
            await _repository.SaveAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var product = await _repository.ProductRepository.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong inside GetOwnerById action: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            _repository.ProductRepository.Delete(product);
            await _repository.SaveAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = await _repository.ProductRepository.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                _repository.ProductRepository.Delete(product);
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

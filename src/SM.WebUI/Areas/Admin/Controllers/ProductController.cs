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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IRepositoryWrapper repository, IMapper mapper,
            IWebHostEnvironment webHostEnvironment)
        {
            _repository = repository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _repository.ProductRepository.GetAllProductsAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDTO productDTO, IFormFile? file)
        {
            ViewBag.CategoryList = (await _repository.CategoryRepository.GetAllCategorysAsync()).Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });


            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\product");

                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                productDTO.ImgURL = @"\images\product\" + fileName;
            }

            if (!ModelState.IsValid)
            {
                var e = ModelState.Values.SelectMany(v => v.Errors);
                return View(productDTO);

            }
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
        public async Task<IActionResult> Edit(ProductCreateDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(productDTO);
            }
            _repository.ProductRepository.Update(_mapper.Map<Product>(productDTO));
            await _repository.SaveAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                ViewBag.CategoryList = (await _repository.CategoryRepository.GetAllCategorysAsync()).Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                var product = await _repository.ProductRepository.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return View(_mapper.Map<ProductCreateDTO>(product));
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
                return StatusCode(500, $"Something went wrong inside GetProductById action: {ex.Message}");
            }
        }
    }
}

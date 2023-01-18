using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBookWeb.Repository;
using BulkyBookWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{   
    public class ProductController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly IWebHostEnvironment _HostEnvironment;

        public ProductController(IUnitofWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _HostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {           
            return View();
        }
      

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var objCoverTypeList = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == Id, null);
            return View(objCoverTypeList);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var coverType = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == id, null);

            if(coverType == null)
            {
                return NotFound();
            }
           
            _unitOfWork.CoverType.Remove(coverType);
            _unitOfWork.Save();
            TempData["Success"] = "CoverType deleted successfully.";
            return RedirectToAction("Index");  
        }
     

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll(null).Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll(null).Select(i => new SelectListItem
                {
                    Text= i.Name,
                    Value = i.Id.ToString()
                })
            };

            if(id == null || id == 0)
            {
                //create product               
                return View(productVM);
            }
            else
            {
                //update product
            }
            
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _HostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    using (FileStream fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageURL = @"images\products\" + fileName + extension;
                }
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["Success"] = "Product created successfully.";
                return RedirectToAction("Index");

            }
            return View();  
        }

        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return Json(new { data = productList});
        }

        #endregion
    }
}

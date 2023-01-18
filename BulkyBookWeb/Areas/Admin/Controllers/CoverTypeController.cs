using BulkyBook.Models;
using BulkyBookWeb.Repository;
using BulkyBookWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{   
    public class CoverTypeController : Controller
    {
        private readonly IUnitofWork _unitOfWork;

        public CoverTypeController(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll(null);
            return View(objCoverTypeList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType covertype)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(covertype);
                _unitOfWork.Save();
                TempData["Success"] = "CoverType created successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var objCoverTypeList = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == Id,null);
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
     

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var coverType = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == id, null);
            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType coverType)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(coverType);
                _unitOfWork.Save();
                TempData["Success"] = "CoverType updated successfully.";
                return RedirectToAction("Index");
            }
            return View();  
        }
    }
}

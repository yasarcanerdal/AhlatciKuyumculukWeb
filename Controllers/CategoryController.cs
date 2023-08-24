using AhlatciKuyumculukWeb.Core.Entities;
using AhlatciKuyumculukWeb.Core.IRepository;
using AhlatciKuyumculukWeb.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AhlatciKuyumculukWeb.Controllers
{
	[Authorize(Roles = UserRoles.Role_Admin)]
	public class CategoryController : Controller
	{
		private readonly ICategoryRepository _categoryRepository;

		public CategoryController(ICategoryRepository context)
		{
			_categoryRepository = context;
		}


		// Veritabanından verileri çekme işlemi:
		public IActionResult Index()
		{
			List<Category> objCategoryList = _categoryRepository.GetAll().ToList();
			return View(objCategoryList);
		}

		// Yeni kategori ekleme işlemi:
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Add(Category category)
		{
			if (ModelState.IsValid) // Kullanıcı kontrol şartı
			{
				_categoryRepository.Add(category); // Veritabanına ekleme işlemi başlangıç
				_categoryRepository.Save(); // Veritabanına eklendi
				TempData["basarili"] = "Yeni kategori başarıyla oluşturuldu"; // Kullanıcıya bilgi mesajı
				return RedirectToAction("Index"); // Kayıt eklendikten sonra tekrar kategoriler sayfasına dönmek için
			}
			return View();
		}


		// kategori güncelleme işlemi:
		public IActionResult Update(int? id) // null da olabilir
		{
			if(id == null || id==0) // null gelirse kontrol (bu projede asla gelmez validation yaptım ama yinede kontrol ediyorum)
			{
				return NotFound();
			}
			Category? categoryVt = _categoryRepository.Get(x => x.Id == id);
			if (categoryVt == null)
			{
				return NotFound();
			}
			return View(categoryVt);
		}

		[HttpPost]
		public IActionResult Update(Category category)
		{
			if (ModelState.IsValid) // Kullanıcı kontrol şartı
			{
				_categoryRepository.Update(category); // Veritabanına ekleme işlemi başlangıç
				_categoryRepository.Save(); // Veritabanına eklendi
				TempData["basarili"] = "Kategori başarıyla güncellendi";
				return RedirectToAction("Index"); // Kayıt eklendikten sonra tekrar kategoriler sayfasına dönmek için
			}
			return View();
		}


		// kategori silme işlemi:
		//(Get Action)
		public IActionResult Delete(int? id) // null da olabilir
		{
			if (id == null || id == 0) // null gelirse kontrol (bu projede asla gelmez validation yaptım ama yinede kontrol ediyorum)
			{
				return NotFound();
			}
			Category? categoryVt = _categoryRepository.Get(x => x.Id == id);
			if (categoryVt == null)
			{
				return NotFound();
			}
			return View(categoryVt);
		}

		// Silme işlemi
		//(Post)
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePOST(int? id)
		{
			Category? category = _categoryRepository.Get(x => x.Id == id);
			if (category == null)
			{
				return NotFound();
			}
			_categoryRepository.Delete(category); // silme işlemi gerçekleşti
			_categoryRepository.Save();
			TempData["basarili"] = "Kategori başarıyla silindi";
			return RedirectToAction("Index");
		}
	}
}

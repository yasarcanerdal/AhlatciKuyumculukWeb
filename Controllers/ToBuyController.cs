using AhlatciKuyumculukWeb.Core.Entities;
using AhlatciKuyumculukWeb.Core.IRepository;
using AhlatciKuyumculukWeb.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace AhlatciKuyumculukWeb.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class ToBuyController : Controller
	{
		private readonly IToBuyRepository _toBuyRepository;
		private readonly IProductRepository _productRepository;
		public readonly IWebHostEnvironment _webHostEnvironment; // img için sistemde tanımlı bir yapı (dependency injection)
		public ToBuyController(IToBuyRepository toBuyRepository, IProductRepository productRepository, IWebHostEnvironment webHostEnvironment)
		{
			_toBuyRepository = toBuyRepository;
			_productRepository = productRepository;
			_webHostEnvironment = webHostEnvironment;
		}


		// Veritabanından verileri çekme işlemi:
		public IActionResult Index()
		{
			List<ToBuy> objToBuyList = _toBuyRepository.GetAll(includeProps:"Product").ToList();
			return View(objToBuyList);
		}

		// Yeni kategori ekleme ve Güncelleme işlemi:
		public IActionResult AddUpdate(int? id)
		{
			IEnumerable<SelectListItem> ProductList = _productRepository.GetAll() // ProductRepositoryden verileri çekme işlemi
				.Select(x => new SelectListItem // Seçeceğim kategori türü x
				{
					Text = x.Feature,
					Value = x.Id.ToString()
				});
			ViewBag.ProductList = ProductList; // Çektiği Kategorileri view katmanına aktarmak için ASP.Net Kütüphanesinin bize kolaylık sağladığı çözüm
			
			if(id == null || id == 0) // ekleme
			{
				return View();
			}
			else // güncelleme
			{
				ToBuy? toBuyVt = _toBuyRepository.Get(x => x.Id == id);
				if (toBuyVt == null)
				{
					return NotFound();
				}
				return View(toBuyVt);
			}
		}

		[HttpPost]
		public IActionResult AddUpdate(ToBuy toBuy)
		{
			if (ModelState.IsValid) // Kullanıcı kontrol şartı
			{

				if (toBuy.Id == 0)
				{
					_toBuyRepository.Add(toBuy);
					TempData["basarili"] = "Yeni satın alma kaydı başarıyla oluşturuldu"; // Kullanıcıya bilgi mesajı
				}
				else
				{
					_toBuyRepository.Update(toBuy);
					TempData["basarili"] = "Satın alma başarıyla güncellendi"; // Kullanıcıya bilgi mesajı
				}

				_toBuyRepository.Save(); // Veritabanına eklendi
				return RedirectToAction("Index","ToBuy"); // Kayıt eklendikten sonra tekrar kategoriler sayfasına dönmek için
			}
			return View();
		}


		// kategori silme işlemi:
		//(Get Action)
		public IActionResult Delete(int? id) // null da olabilir
		{

			IEnumerable<SelectListItem> ProductList = _productRepository.GetAll() // ProductRepositoryden verileri çekme işlemi
				.Select(x => new SelectListItem // Seçeceğim kategori türü x
				{
					Text = x.Feature,
					Value = x.Id.ToString()
				});
			ViewBag.ProductList = ProductList;


			if (id == null || id == 0) // null gelirse kontrol (bu projede asla gelmez validation yaptım ama yinede kontrol ediyorum)
			{
				return NotFound();
			}
			ToBuy? toBuytVt = _toBuyRepository.Get(x => x.Id == id);
			if (toBuytVt == null)
			{
				return NotFound();
			}
			return View(toBuytVt);
		}

		// Silme işlemi
		//(Post)
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePOST(int? id)
		{
			ToBuy? toBuy = _toBuyRepository.Get(x => x.Id == id);
			if (toBuy == null)
			{
				return NotFound();
			}
			_toBuyRepository.Delete(toBuy); // silme işlemi gerçekleşti
			_toBuyRepository.Save();
			TempData["basarili"] = "Ürün başarıyla silindi";
			return RedirectToAction("Index", "ToBuy");
		}
	}
}

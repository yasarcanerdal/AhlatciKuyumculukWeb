using AhlatciKuyumculukWeb.Core.Entities;
using AhlatciKuyumculukWeb.Core.IRepository;
using AhlatciKuyumculukWeb.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AhlatciKuyumculukWeb.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductRepository _productRepository;
		private readonly ICategoryRepository _categoryRepository; // CategoryRepositoryden verileri çekme işlemi için tanımlıyoruz
		public readonly IWebHostEnvironment _webHostEnvironment; // img için sistemde tanımlı bir yapı (dependency injection)
		public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment)
		{
			_productRepository = productRepository;
			_categoryRepository = categoryRepository;
			_webHostEnvironment = webHostEnvironment;
		}


        [Authorize(Roles = "Admin,Customer")]
        // Veritabanından verileri çekme işlemi:
        public IActionResult Index()
		{
			//List<Product> objProductList = _productRepository.GetAll().ToList();
			List<Product> objProductList = _productRepository.GetAll(includeProps:"Category").ToList();
			return View(objProductList);
		}


        [Authorize(Roles = UserRoles.Role_Admin)]
        // Yeni kategori ekleme ve Güncelleme işlemi:
        public IActionResult AddUpdate(int? id)
		{
			IEnumerable<SelectListItem> CategoryList = _categoryRepository.GetAll() // CategoryRepositoryden verileri çekme işlemi
				.Select(x => new SelectListItem // Seçeceğim kategori türü x
				{
					Text = x.Name,
					Value = x.Id.ToString()
				});
			ViewBag.CategoryList = CategoryList; // Çektiği Kategorileri view katmanına aktarmak için ASP.Net Kütüphanesinin bize kolaylık sağladığı çözüm
			
			if(id == null || id == 0) // ekleme
			{
				return View();
			}
			else // güncelleme
			{
				Product? productVt = _productRepository.Get(x => x.Id == id);
				if (productVt == null)
				{
					return NotFound();
				}
				return View(productVt);
			}
		}

		[HttpPost]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult AddUpdate(Product product, IFormFile? file) // IFormFile: Dosya ismi döneceği için hazır parametre yazdım.
		{
			if (ModelState.IsValid) // Kullanıcı kontrol şartı
			{
				//var errors = ModelState.Values.SelectMany(x => x.Errors); Hata bulmak için genelde kullandığım kod

				string wwwRootPath = _webHostEnvironment.WebRootPath; // wwwRoot un bulunduğu dizeyi veriyor
				string productPath = Path.Combine(wwwRootPath, @"img");

				if (file != null) // Güncelleme yapacağımız zaman img alanının dolu olmasını sağlıyoruz
				{
					using (var fileStream = new FileStream(Path.Combine(productPath, file.FileName), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}
					product.İmgUrl = @"\img\" + file.FileName;
				}

				if (product.Id == 0)
				{
					_productRepository.Add(product);
					TempData["basarili"] = "Yeni ürün başarıyla oluşturuldu"; // Kullanıcıya bilgi mesajı
				}
				else
				{
					_productRepository.Update(product);
					TempData["basarili"] = "Ürün başarıyla güncellendi"; // Kullanıcıya bilgi mesajı
				}

				_productRepository.Save(); // Veritabanına eklendi
				return RedirectToAction("Index"); // Kayıt eklendikten sonra tekrar kategoriler sayfasına dönmek için
			}
			return View();
		}


        //// kategori güncelleme işlemi:
        //public IActionResult Update(int? id) // null da olabilir
        //{
        //	if(id == null || id==0) // null gelirse kontrol (bu projede asla gelmez validation yaptım ama yinede kontrol ediyorum)
        //	{
        //		return NotFound();
        //	}
        //	Product? productVt = _productRepository.Get(x => x.Id == id);
        //	if (productVt == null)
        //	{
        //		return NotFound();
        //	}
        //	return View(productVt);
        //}

        //[HttpPost]
        //public IActionResult Update(Product product)
        //{
        //	if (ModelState.IsValid) // Kullanıcı kontrol şartı
        //	{
        //		_productRepository.Update(product); // Veritabanına ekleme işlemi başlangıç
        //		_productRepository.Save(); // Veritabanına eklendi
        //		TempData["basarili"] = "Ürün başarıyla güncellendi";
        //		return RedirectToAction("Index"); // Kayıt eklendikten sonra tekrar kategoriler sayfasına dönmek için
        //	}
        //	return View();
        //}



        [Authorize(Roles = UserRoles.Role_Admin)]
        // kategori silme işlemi:
        //(Get Action)
        public IActionResult Delete(int? id) // null da olabilir
		{
			if (id == null || id == 0) // null gelirse kontrol (bu projede asla gelmez validation yaptım ama yinede kontrol ediyorum)
			{
				return NotFound();
			}
			Product? productVt = _productRepository.Get(x => x.Id == id);
			if (productVt == null)
			{
				return NotFound();
			}
			return View(productVt);
		}

		// Silme işlemi
		//(Post)
		[HttpPost, ActionName("Delete")]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult DeletePOST(int? id)
		{
			Product? product = _productRepository.Get(x => x.Id == id);
			if (product == null)
			{
				return NotFound();
			}
			_productRepository.Delete(product); // silme işlemi gerçekleşti
			_productRepository.Save();
			TempData["basarili"] = "Ürün başarıyla silindi";
			return RedirectToAction("Index");
		}
	}
}

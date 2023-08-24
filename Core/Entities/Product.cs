using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhlatciKuyumculukWeb.Core.Entities
{
	public class Product
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Ürün özelliği boş bırakılamaz")]
		[DisplayName("Ürün Özelliği")] // Ekranda gözükecek label için
		public string Feature { get; set; } // Ürün Özellik

		[DisplayName("Açıklama")] // Ekranda gözükecek label için
		public string Description { get; set; }

		[Required(ErrorMessage = "Ürün fiyatı boş bırakılamaz")]
		[DisplayName("Ürün Fiyatı")] // Ekranda gözükecek label için
		public double Price { get; set; }

		// Foregin Key
		[ValidateNever]
		[DisplayName("Kategori")] // Ekranda gözükecek label için
		public int CategoryId { get; set; }
		[ForeignKey("CategoryId")]

		[ValidateNever]
		public Category Category { get; set; }

		// Resim Url ekleme
		[ValidateNever]
		public string İmgUrl { get; set; }	
	}
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AhlatciKuyumculukWeb.Core.Entities
{
	public class Category
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage ="Kategori adı boş bırakılamaz")] // kullanıcıya hata mesajı bilgisi
		[MaxLength(25)]
		[DisplayName("Yeni Kategori Adı")] // Ekranda gözükecek label için 
		public string Name { get; set; }
	}
}

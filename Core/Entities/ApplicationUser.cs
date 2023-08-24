using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AhlatciKuyumculukWeb.Core.Entities
{
	public class ApplicationUser : IdentityUser // ek alanlar ekleyebilmek için
	{
		[Required]
		public int CustomerId { get; set; }	
		public string? Address { get; set; }
	}
}

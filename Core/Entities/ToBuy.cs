using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AhlatciKuyumculukWeb.Core.Entities
{
	public class ToBuy
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int CustomerId { get; set; }



		// Foregin Key
		[ValidateNever]
		public int ProductId { get; set; }
		[ForeignKey(("ProductId"))]

		[ValidateNever]
		public Product Product { get; set; }

	}
}

using MovieTicketApp_MVC_project.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketApp_MVC_project.Models
{
    public class Order:IEntitybase
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        //public ApplicationUser User { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}

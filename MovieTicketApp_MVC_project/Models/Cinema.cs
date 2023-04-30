using System.ComponentModel.DataAnnotations;

namespace MovieTicketApp_MVC_project.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }
        public String Logo { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }

        public List<Movie> Movies { get; set; }

    }
}

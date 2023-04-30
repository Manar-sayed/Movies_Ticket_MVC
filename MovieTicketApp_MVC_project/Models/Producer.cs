using System.ComponentModel.DataAnnotations;

namespace MovieTicketApp_MVC_project.Models
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }
        public String ProfilepictureURL { get; set; }
        public String FullName { get; set; }
        public String Bio { get; set; }

        public List<Movie> movies { get; set;}
    }
}

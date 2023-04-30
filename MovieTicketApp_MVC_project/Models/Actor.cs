using System.ComponentModel.DataAnnotations;

namespace MovieTicketApp_MVC_project.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        public String ProfilepictureURL { get; set; }
        public String FullName { get; set; }
        public String Bio { get; set; }
        public List<Actor_Movie> Actors_Movies { get; set; }
    }

}

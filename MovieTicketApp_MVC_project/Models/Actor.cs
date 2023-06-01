using MovieTicketApp_MVC_project.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace MovieTicketApp_MVC_project.Models
{
    public class Actor:IEntitybase
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="ProfilepictureURL is Required!")] 
        public String ProfilepictureURL { get; set; }

        [Required(ErrorMessage = "Name is Required!"), MaxLength(10),MinLength(3,ErrorMessage ="Invalid name !")]
        public String FullName { get; set; }

        [Required(ErrorMessage = "Biography is Required!")]
        public String Bio { get; set; }
        public List<Actor_Movie> Actors_Movies { get; set; }
        
    }

}

﻿using MovieTicketApp_MVC_project.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieTicketApp_MVC_project.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        public MovieCategory MovieCategory { get; set; }


        [ForeignKey("CinemarId")]
        public int CinemaId { get; set; }
        public Cinema Cinema { get; set; }  
        [ForeignKey("ProducerId")]
        public int ProducerId { get; set; }
        public Producer Producer  { get; set; }
        public List<Actor_Movie> Actors_Movies { get; set; }



    }
}

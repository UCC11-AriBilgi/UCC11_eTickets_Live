﻿using eTickets.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public double? Price { get; set; }

        public string? ImageURL { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public MovieCategory? MovieCategory { get; set; } // Burayı besleyecek yer MovieCategory enum'ı

        // Relations
        // Many-to-Many

        public List<Actor_Movie> Actors_Movies { get; set; }

        // Cinema
        // One-to-Many
        public int CinemaId { get; set; }
        [ForeignKey("CinemaId")]
        public Cinema Cinema { get; set; }

        // Producer
        // One-to-Many
        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        public Producer Producer { get; set;}

    }
}

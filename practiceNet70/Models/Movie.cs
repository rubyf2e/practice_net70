using System;
using System.ComponentModel.DataAnnotations;

namespace practiceNet70.Models
{
	public class Movie
	{
        public int Id { get; set; }

        [Display(Name = "名稱")]
        public string? Title { get; set; }

        [Display(Name = "上映時間")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "類別")]
        public string? Genre { get; set; }

        [Display(Name = "價錢")]
        public decimal Price { get; set; }
    }
}


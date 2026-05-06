using System.ComponentModel.DataAnnotations;

namespace Record_Shop_Backend.MVC_Data_Models
{
    public class Album
    {
        public int AlbumId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Artist { get; set; }
        [Required]
        public string ReleaseYear { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Stock { get; set; }
    }
}

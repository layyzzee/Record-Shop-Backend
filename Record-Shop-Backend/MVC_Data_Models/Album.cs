using System.ComponentModel.DataAnnotations;

namespace Record_Shop_Backend.MVC_Data_Models
{
    public class Album
    {
        public int AlbumId { get; set; }
        [Required(ErrorMessage = "Name Is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Artist Is required")]
        public string Artist { get; set; }
        [Required(ErrorMessage = "ReleaseYear Is required")]
        public string ReleaseYear { get; set; }
        [Required(ErrorMessage = "Genre Is required")]
        public string Genre { get; set; }
        [Required(ErrorMessage = "Price Is required")]
        public double? Price { get; set; }
        [Required(ErrorMessage = "Stock Is required")]
        public int? Stock { get; set; }
    }
}

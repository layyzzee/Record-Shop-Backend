using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Record_Shop_Backend.MVC_Data_Models
{
    public class Album
    {
        [JsonPropertyName("id")]
        public int AlbumId { get; set; }
        [Required]
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [Required]
        [JsonPropertyName("artist")]
        public string Artist { get; set; }
        [Required]
        [JsonPropertyName("released")]
        public string ReleaseYear { get; set; }
        [Required]
        [JsonPropertyName("genre")]
        public string Genre { get; set; }
        [Required]
        [JsonPropertyName("price")]
        public double Price { get; set; }
        [Required]
        [JsonPropertyName("stock")]
        public int Stock { get; set; }
    }
}

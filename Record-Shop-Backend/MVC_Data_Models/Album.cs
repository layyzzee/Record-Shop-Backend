using System.Text.Json.Serialization;

namespace Record_Shop_Backend.MVC_Data_Models
{
    public class Album
    {
        [JsonPropertyName("id")]
        public int AlbumId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("released")]
        public string ReleaseYear { get; set; }
        [JsonPropertyName("genre")]
        public string Genre { get; set; }
        [JsonPropertyName("price")]
        public double Price { get; set; }
        [JsonPropertyName("stock")]
        public int Stock { get; set; }
    }
}

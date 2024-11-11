using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API2.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<Product> Products { get; } = new();
    }
}

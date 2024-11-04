using api_client_counter.Utils;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api_client_counter.Models.Entities
{
    [Index(nameof(Inn), IsUnique = true)]
    public class Founder
    {
        private long inn;

        [Key]
        public int Id { get; set; }
        public long Inn
        {
            get => inn; set => inn = value.ParseInn();
        }
        public string Name { get; set; }
        public DateTime DateAtCreated { get; set; }
        public DateTime DateAtUpdated { get; set; }
        [JsonIgnore]
        public Client? Client { get; set; } 
    }
}
using System.ComponentModel.DataAnnotations;
using api_client_counter.Models.Enums;
using api_client_counter.Utils;
using Microsoft.EntityFrameworkCore;

namespace api_client_counter.Models.Entities
{
    [Index(nameof(Inn), IsUnique = true)]
    public class Client
    {
        private long inn;

        [Key]
        public Guid Id { get; set; }
        public long Inn
        {
            get => inn; set => inn = value.ParseInn();
        }
        public ClientType Type { get; set; }
        public DateTime DateAtCreated { get; set; } 
        public DateTime DateAtUpdated { get; set; }
        public ICollection<Founder> Founders { get; set; }
    }
}
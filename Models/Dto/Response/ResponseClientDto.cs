using api_client_counter.Models.Entities;
using Microsoft.OpenApi.Extensions;

namespace api_client_counter.Models.Dto.Response
{
    public class ResponseClientDto
    {
        public long Inn { get; private set; }
        public string Type { get; private set; } 
        public DateTime DateAtCreated { get; private set; }
        public DateTime DateAtUpdated { get; private set; }
        public List<ResponseFounderDto> Founders { get; private set; }

        public ResponseClientDto(Client client)
        {
            Inn = client.Inn;
            Type = client.Type.GetDisplayName();
            DateAtCreated = client.DateAtCreated;
            DateAtUpdated = client.DateAtUpdated;
            Founders = new List<ResponseFounderDto>(client.Founders.Select(f => new ResponseFounderDto(f)));
        }
    }

    public class ResponseFounderDto
    {
        public long Inn { get; private set; }
        public string Name { get; private set; } 
        public DateTime DateAtCreated { get; private set; }
        public DateTime DateAtUpdated { get; private set; }

        public ResponseFounderDto(Founder founder)
        {
            Inn = founder.Inn;
            Name = founder.Name;
            DateAtCreated = founder.DateAtCreated;
            DateAtUpdated = founder.DateAtUpdated;
        }
    }
}
namespace api_client_counter.Models.Dto.Request
{
    public class RequestClientDto
    {
        public long Inn { get; set; }
        public string Type { get; set; }
        public List<RequestFounderDto> Founders { get; set; }
    }

    public class RequestFounderDto
    {
        public long Inn { get; set; }
        public string Name { get; set; }
    }
}

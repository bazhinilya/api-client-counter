using api_client_counter.Models.Dto.Request;
using api_client_counter.Models.Dto.Response;
using api_client_counter.Models.Entities;
using api_client_counter.Models.Enums;
using api_client_counter.Utils;
using Microsoft.EntityFrameworkCore;

namespace api_client_counter.Service
{
    public class AppService
    {
        private readonly Db.AppContext _applicationContext;
        public AppService(Db.AppContext context) => _applicationContext = context;

        public async Task<List<ResponseClientDto>> GetClients() =>
            new List<ResponseClientDto>(
                await _applicationContext.Clients
                        .Include(c => c.Founders)
                        .Select(c => new ResponseClientDto(c))
                        .ToListAsync()
            );

        public async Task<ResponseClientDto> GetClient(long inn) =>
            new ResponseClientDto(
                await _applicationContext.Clients
                        .Include(c => c.Founders)
                        .FirstOrDefaultAsync(c => c.Inn == inn)
                        ?? throw new ArgumentNullException($"Client with inn: {inn} not found")
            );

        public async Task AddClient(RequestClientDto clientDto)
        {
            var founders = new List<Founder>(
                    clientDto.Founders
                            .Select(f => new Founder
                            {
                                Inn = f.Inn,
                                Name = f.Name,
                                DateAtCreated = DateTime.UtcNow,
                                DateAtUpdated = DateTime.UtcNow
                            })
            );
            var client = new Client
            {
                Inn = clientDto.Inn,
                Type = clientDto.Type.ToClientType(),
                DateAtCreated = DateTime.UtcNow,
                DateAtUpdated = DateTime.UtcNow,
                Founders = founders
            };
            if (client.Type == ClientType.IP && founders.Count > 1)
                throw new Exception("Incorrect count founder to IP");
            await _applicationContext.Clients.AddAsync(client);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<ResponseClientDto> UpdateClient(long currentInn, RequestClientDto newClient)
        {
            var clientToUpdate = await _applicationContext.Clients.FirstOrDefaultAsync(c => c.Inn == currentInn)
                ?? throw new ArgumentNullException($"Client with inn: {currentInn} not found");
            clientToUpdate.Inn = newClient.Inn;
            clientToUpdate.Type = newClient.Type.ToClientType();
            var newFounders = newClient.Founders.Select(f => new Founder
            {
                Inn = f.Inn,
                Name = f.Name,
                DateAtCreated = DateTime.UtcNow,
                DateAtUpdated = DateTime.UtcNow
            });
            clientToUpdate.Founders = new List<Founder>(newFounders);
            if (clientToUpdate.Type == ClientType.IP && newFounders.Count() > 1)
                throw new Exception("Incorrect count founder to IP");
            _applicationContext.Clients.Update(clientToUpdate);
            await _applicationContext.SaveChangesAsync();
            return new ResponseClientDto(clientToUpdate);
        }

        public async Task RemoveClients(long[] inn)
        {
            var clientsToRemove = await _applicationContext.Clients.Where(c => inn.Contains(c.Inn)).ToListAsync();
            _applicationContext.Clients.RemoveRange(clientsToRemove);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
using RpgApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace RpgApi.Services;

public class PlayerService
{
    private readonly IMongoCollection<Player> _playerCollection;

    public PlayerService(
        IOptions<PlayerDatabaseSettings> playerDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            playerDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            playerDatabaseSettings.Value.DatabaseName);

        _playerCollection = mongoDatabase.GetCollection<Player>(
            playerDatabaseSettings.Value.PlayerCollectionName);
    }

    public async Task<List<Player>> GetAsync() =>
        await _playerCollection.Find(_ => true).ToListAsync();

    public async Task<Player?> GetAsync(int id) =>
        await _playerCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Player newPlayer) =>
        await _playerCollection.InsertOneAsync(newPlayer);

    public async Task UpdateAsync(int id, Player updatedBook) =>
        await _playerCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveAsync(int id) =>
        await _playerCollection.DeleteOneAsync(x => x.Id == id);
}
using MongoDB.Driver;
using ArulOliNagar.Model;

namespace ArulOliNagar.Services
{
    public class MembersService
    {

        private readonly IMongoCollection<Members> _memberdata;
        public MembersService( string ConnectionString , string DataBase , string CollectionName)
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase(DataBase);
            _memberdata = database.GetCollection<Members>(CollectionName);
        }


        public async Task InsertMemberDataAsync(List<Members> member_data)
        {
            await _memberdata.InsertManyAsync(member_data);
        }


        public async Task<List<Members>> GetAllMembersAsync()
        {  
        return await _memberdata.Find(_ => true).ToListAsync();
        }



   
    }

}

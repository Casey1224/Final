using System.Data;
using System.Linq;
using Dapper;
using Final.Models;

namespace Final.Repositories
{
    public class ProfilesRepository
    {

        private readonly IDbConnection _db;
        public ProfilesRepository(IDbConnection db)
        {
            _db = db;
        }
        internal Account GetOne(string id)
        {
            string sql = @"
            SELECT * FROM accounts WHERE id = @id;
            
            ";
            return _db.Query<Account>(sql, new { id }).FirstOrDefault();
        }
    }
}
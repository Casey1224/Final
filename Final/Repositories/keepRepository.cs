using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Final.Models;

namespace Final.Repositories
{
    public class keepRepository
    {
        private readonly IDbConnection _db;

        public keepRepository(IDbConnection db)
        {
            _db = db;
        }

        internal List<keep> getAllKeeps()
        {
            string sql = @"
          
          SELECT
          k.*,
          a.*
          FROM newKeep k
          JOIN accounts a
          WHERE a.id = k.creatorId;
          ";
            return _db.Query<keep, Account, keep>(sql, (k, a)
            =>
            {
                k.creator = a;
                return k;
            }
            ).ToList();
        }

        internal keep getKeepById(int id)
        {
            string sql = @"
          SELECT
          k.*,
          a.*
          FROM newKeep k
          JOIN accounts a ON k.creatorId = a.id
          WHERE k.id = @Id
         
         
         ";
            return _db.Query<keep, Account, keep>(sql, (k, a) =>
            {
                k.creator = a;
                return k;
            },
                new { id }).FirstOrDefault();
        }

        internal keep createKeep(keep keepers)
        {
            string sql = @"
           
           INSERT INTO newKeep
           (name, description, img, views, kept, creatorId)
           VALUES
           (@name, @description, @img, @views, @kept, @creatorId );
           SELECT LAST_INSERT_ID();
           ";
            int id = _db.ExecuteScalar<int>(sql, keepers);
            keepers.Id = id;
            return keepers;
        }
    }
}
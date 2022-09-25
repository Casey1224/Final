using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Final.Models;

namespace Final.Repositories
{
    public class vaultRepository
    {
        private readonly IDbConnection _db;

        public vaultRepository(IDbConnection db)
        {
            _db = db;
        }
        internal List<Vault> getAllVaults()
        {
            string sql = @"
           SELECT
           v.*,
           a.*
           FROM vault v
           JOIN accounts a
           WHERE a.id = v.creatorId;
           
           ";
            return _db.Query<Vault, Account, Vault>(sql, (v, a) =>
            {
                v.creator = a;
                return v;
            }
            ).ToList();
        }

        internal Vault getVaultById(int id)
        {
            string sql = @"
           SELECT 
           v.*,
           a.*
           FROM vault v
           JOIN accounts a
           ON v.creatorId = a.Id
           WHERE v.id = @id;
           ";
            return _db.Query<Vault, Account, Vault>(sql, (v, a) =>
            {
                v.creator = a;
                return v;
            },
            new { id }).FirstOrDefault();
        }

        internal Vault Update(Vault update)
        {
            string sql = @"
            UPDATE vault v SET
            name = @Name,
            description = @Description,
            isPrivate = @isPrivate

            WHERE v.id = @id;
            ";
            _db.Execute(sql, update);
            return update;
        }

        internal void Delete(int id)
        {
            string sql = @"
            DELETE FROM vault
            WHERE id = @id;
            
            ";
            _db.Execute(sql, new { id });
        }

        internal Vault createVault(Vault vaulters)
        {
            string sql = @"
           INSERT INTO vault
           (name, description, isPrivate, creatorId)
           VALUES
           (@name, @description, @isPrivate, @creatorId);
           SELECT LAST_INSERT_ID();
        ";
            int id = _db.ExecuteScalar<int>(sql, vaulters);
            vaulters.Id = id;
            return vaulters;
        }
    }
}
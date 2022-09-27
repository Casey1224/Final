using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Final.Models;

namespace Final.Repositories
{
    public class vaultKeepRepository
    {
        private readonly IDbConnection _db;

        public vaultKeepRepository(IDbConnection db)
        {
            _db = db;
        }


        internal VaultKeep createVaultKeep(VaultKeep vaultKeepers)
        {
            string sql = @"
           
           INSERT INTO newVaultKeep
           (creatorId, vaultId, keepId)
           VALUES
           (@creatorId, @vaultId, @keepId);
           SELECT LAST_INSERT_ID();
           
           ";
            int id = _db.ExecuteScalar<int>(sql, vaultKeepers);
            vaultKeepers.Id = id;
            return vaultKeepers;

        }

        internal VaultKeep getVaultKeepById(int id)
        {
            string sql = @"
        SELECT * FROM newVaultKeep
        WHERE id = @id;
    
         ";
            VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { id }).FirstOrDefault();
            return vaultKeep;
        }

        internal void Delete(int id)
        {
            string sql = @"
        DELETE FROM newVaultKeep 
         WHERE id = @id;
        ";

            _db.Execute(sql, new { id });
        }

        internal List<keepvm> getKeepsByVaultId(int id)
        {
            string sql = @"
            SELECT
            k.*,
            vk.id as vaultKeepId,
            a.*
            FROM newVaultKeep vk
            JOIN newKeep k ON k.id = vk.keepId
            JOIN accounts a ON a.id = k.creatorId
            WHERE vk.id = @id
            
            ";
            return _db.Query<keepvm, Account, keepvm>(sql, (kvm, a) =>
            {
                kvm.creator = a;
                return kvm;
            }, new { id }).ToList();
        }
    }
}
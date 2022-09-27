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
        JOIN accounts a 
        ON k.creatorId = a.Id
        WHERE k.id = @id;
       
        
         ";
            return _db.Query<keep, Account, keep>(sql, (k, a) =>
            {
                k.creator = a;
                return k;
            },
                new { id }).FirstOrDefault();
        }

        internal List<keep> getKeepByProfileId(string id)
        {
            string sql = @"
            SELECT 
            k.*,
            a.*
            FROM newKeep k
            JOIN accounts a ON k.creatorId = a.id
            WHERE k.creatorId = @id;
            ";
            return _db.Query<keep, Account, keep>(sql, (k, a) =>
            {
                k.creator = a;
                return k;
            }, new { id }).ToList();

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

        internal void Delete(int id)
        {
            string sql = @"
        DELETE FROM newKeep 
         WHERE id = @id;
        ";

            _db.Execute(sql, new { id });
        }

        internal keep Update(keep update)
        {
            string sql = @"
           UPDATE newKeep k SET
        name = @Name,
        img = @Img,
        description = @Description,
        views = @Views,
        kept = @Kept
        
        WHERE k.id = @id;
           ";
            _db.Execute(sql, update);
            return update;
        }

        internal List<keepvm> getAllVaultKeeps(int id)
        {
            string sql = @"
            SELECT 
            v.*,
            a.*,
            k.*,
            vk.*
            FROM vault v
            JOIN newVaultKeep vk ON v.id = vk.vaultId
            JOIN newKeep k ON vk.keepId = k.id
            JOIN accounts a ON k.creatorId = a.id
            WHERE v.id = @id;
            ";
            return _db.Query<Vault, Account, keepvm, keep, keepvm>(sql, (v, a, kvm, vk) =>
            {
                kvm.creator = a;
                kvm.vaultKeepId = vk.Id;
                return kvm;
            }, new { id }).ToList();
        }

        internal List<keepvm> getKeepsByVaultId(int vaultId)
        {



            string sql = @"
          SELECT 
          
          vk.*,
          k.*,
          a.*
          
         FROM newVaultKeep vk
         JOIN newKeep k
         ON vk.keepId = k.Id
         JOIN accounts a
         ON k.creatorId = a.Id
         WHERE vk.vaultId = @vaultId;
         

          ";

            List<keepvm> keeps = _db.Query<VaultKeep, keepvm, Account, keepvm>(sql, (vk, km, a) =>
            {
                km.creator = a;
                km.vaultKeepId = vk.Id;
                return km;
            }, new { vaultId }).ToList();

            return keeps;
        }
    }






}





using System;
using System.Collections.Generic;
using Final.Models;
using Final.Repositories;

namespace Final.Services
{
    public class vaultKeepService
    {
        private readonly vaultKeepRepository vkr;

        public vaultKeepService(vaultKeepRepository vkr)
        {
            this.vkr = vkr;
        }
        internal VaultKeep createVaultKeep(VaultKeep vaultKeepers)
        {
            return vkr.createVaultKeep(vaultKeepers);
        }



        internal List<VaultKeep> getAllVaultKeeps(int id1, string id2)
        {
            throw new NotImplementedException();
        }

        internal string Delete(int id, Account user)
        {
            VaultKeep original = getVaultKeepById(id, user.Id);
            if (original.creatorId != user.Id)
            {
                throw new Exception($"cannot delete not yours");
            }
            vkr.Delete(id);
            return $" was deleted.";
        }
        internal VaultKeep getVaultKeepById(int id, string userId)
        {
            VaultKeep vaultKeep = vkr.getVaultKeepById(id);
            if (vaultKeep == null)
            {
                throw new Exception($"No keep at id: {id}");
            }

            return vaultKeep;
        }

    }
}
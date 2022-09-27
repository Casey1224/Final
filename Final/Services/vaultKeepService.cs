using System;
using System.Collections.Generic;
using Final.Models;
using Final.Repositories;

namespace Final.Services
{
    public class vaultKeepService
    {
        private readonly vaultKeepRepository vkr;
        private readonly vaultRepository vr;
        private readonly keepRepository kr;

        public vaultKeepService(vaultKeepRepository vkr, vaultRepository vr, keepRepository kr)
        {
            this.vkr = vkr;
            this.vr = vr;
            this.kr = kr;
        }

        internal VaultKeep createVaultKeep(VaultKeep vaultKeepers)
        {
            Vault vault = vr.getVaultById(vaultKeepers.vaultId);
            if (vault.creatorId != vaultKeepers.creatorId)
            {
                throw new Exception("not accessable");
            }
            keep keep = kr.getKeepById(vaultKeepers.keepId);
            keep.kept++;
            kr.Update(keep);

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
        // internal List<keepvm> getAllKeepsByVaultId(int id)
        // {
        //     Vault vault = vr.getVaultById(id);
        //     return vkr.getAllKeepsByVaultId(vault.Id);
        // }

    }
}
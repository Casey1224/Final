using System;
using System.Collections.Generic;
using Final.Models;
using Final.Repositories;
using static Final.Models.keep;

namespace Final.Services
{
    public class vaultService
    {
        private readonly vaultRepository vr;
        private readonly keepRepository kr;

        public vaultService(vaultRepository vr, keepRepository kr)
        {
            this.vr = vr;
            this.kr = kr;
        }

        internal Vault createVault(Vault vaulters)
        {
            return vr.createVault(vaulters);

        }

        internal Vault getVaultById(int id, string userId)
        {
            Vault vault = vr.getVaultById(id);
            if (vault == null)
            {
                throw new Exception($"No Vault at that id: {id}");
            }
            return vault;
        }

        internal Vault Update(Vault update, Account user)
        {
            Vault original = vr.getVaultById(update.Id);
            if (original.creatorId != user.Id)
            {
                throw new Exception($"cannot update {original.name} you are not the creator");
            }
            original.name = update.name ?? original.name;
            original.description = update.description ?? original.description;
            original.isPrivate = update.isPrivate ?? original.isPrivate;
            return vr.Update(original);
        }

        internal string Delete(int id, Account user)
        {
            Vault original = getVaultById(id, user.Id);
            if (original.creatorId != user.Id)
            {
                throw new Exception($"cannot delete {original.name} not yours");

            }
            vr.Delete(id);
            return $"{original.name} was deleted.";
        }
        internal List<keepvm> getAllVaultKeeps(int id, string userId)
        {
            Vault vault = getVaultById(id, userId);
            if (vault.isPrivate == true && vault.creatorId != userId)
            {
                new Exception("this isnt ur vault");
            }
            return kr.getKeepsByVaultId(id);

        }

    }
}
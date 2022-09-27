using System;
using System.Collections.Generic;
using Final.Models;
using Final.Repositories;

namespace Final.Services
{
    public class vaultService
    {
        private readonly vaultRepository vr;
        private readonly keepRepository kr;
        private readonly vaultKeepRepository vkr;

        public vaultService(vaultRepository vr, keepRepository kr, vaultKeepRepository vkr)
        {
            this.vr = vr;
            this.kr = kr;
            this.vkr = vkr;
        }

        internal Vault createVault(Vault vaulters)
        {
            return vr.createVault(vaulters);

        }

        internal Vault getVaultById(int id, string userId)
        {
            Vault vault = vr.getVaultById(id);
            if (vault.isPrivate == true && vault.creatorId != userId)
            {
                throw new Exception("this is locked");
            }
            if (vault == null)
            {
                throw new Exception("no vault at this id");
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

        internal List<Vault> getAccountVaults(string id)
        {
            List<Vault> vaults = vr.getAccountVaults(id);
            return vaults;
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

        internal List<Vault> getVaultsByProfId(string id)
        {
            List<Vault> vaults = vr.getVaultsByProfId(id);
            List<Vault> privateVault = vaults.FindAll(v => v.isPrivate != true);
            return privateVault;
        }



        internal List<keepvm> getAllVaultKeeps(int id, string userId)
        {
            Vault vault = vr.getVaultById(id);
            if (vault.isPrivate == true && vault.creatorId != userId)
            {
                throw new Exception("this isnt ur vault");
            }
            return vkr.getKeepsByVaultId(id);

        }

    }
}
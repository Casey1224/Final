using System;
using Final.Models;
using Final.Repositories;

namespace Final.Services
{
    public class vaultService
    {
        private readonly vaultRepository vr;

        public vaultService(vaultRepository vr)
        {
            this.vr = vr;
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
    }
}
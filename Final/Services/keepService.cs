using System;
using System.Collections.Generic;
using Final.Models;
using Final.Repositories;

namespace Final.Services
{
    public class keepService
    {
        private readonly keepRepository kr;
        private readonly vaultRepository vr;

        public keepService(keepRepository kr, vaultRepository vr)
        {
            this.kr = kr;
            this.vr = vr;
        }

        public List<keep> getAllKeeps()
        {
            return kr.getAllKeeps();
        }

        internal keep getKeepById(int id, string userId)
        {
            keep keep = kr.getKeepById(id);
            if (keep == null)
            {
                throw new Exception($"No keep at id: {id}");
            }
            keep.views++;
            // kr.Update(keep);
            return keep;
        }

        internal keep createKeep(keep keepers)
        {
            return kr.createKeep(keepers);
        }

        internal keep Update(keep update, Account user)
        {
            keep original = kr.getKeepById(update.Id);
            if (original.creatorId != user.Id)
            {
                throw new Exception($"cannot update {original.name} you are not the creator");
            }
            original.name = update.name ?? original.name;
            original.img = update.img ?? original.img;
            original.description = update.description ?? original.description;
            return kr.Update(original);

        }

        internal List<keep> getKeepByProfileId(string id)
        {
            return kr.getKeepByProfileId(id);
        }

        internal List<keep> GetProfileKeeps(string id)
        {
            throw new NotImplementedException();
        }

        internal string Delete(int id, Account user)
        {
            keep original = getKeepById(id, user.Id);
            if (original.creatorId != user.Id)
            {
                throw new Exception($"cannot delete {original.name}, not yours");
            }
            kr.Delete(id);
            return $"{original.name} was deleted.";
        }

        internal List<keepvm> getAllVaultKeeps(int id, string userId)
        {
            Vault vault = vr.getVaultById(id);

            if (vault.isPrivate == true & vault.creatorId != userId)
            {
                throw new Exception("this is a private vault");
            }
            return kr.getAllVaultKeeps(id);
        }
    }
}
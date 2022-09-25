using System;
using System.Collections.Generic;
using Final.Models;
using Final.Repositories;

namespace Final.Services
{
    public class keepService
    {
        private readonly keepRepository kr;

        public keepService(keepRepository kr)
        {
            this.kr = kr;
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
            kr.Update(keep);
            return keep;
        }

        internal keep createKeep(keep keepers)
        {
            return kr.createKeep(keepers);
        }

        internal keep Update(keep update, Account user)
        {
            keep original = getKeepById(update.Id, user.Id);
            if (original.creatorId != user.Id)
            {
                throw new Exception($"cannot update {original.name} you are not the creator");
            }
            original.name = update.name ?? original.name;
            original.img = update.img ?? original.img;
            original.description = update.description ?? original.description;
            return kr.Update(original);

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
    }
}
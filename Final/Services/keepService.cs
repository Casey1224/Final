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

        internal keep getKeepById(int id)
        {
            keep keep = kr.getKeepById(id);
            if (keep == null)
            {
                throw new Exception($"No keep at id: {id}");
            }
            keep.views++;
            // kr.editKeep(keep);
            return keep;
        }

        internal keep createKeep(keep keepers)
        {
            return kr.createKeep(keepers);
        }
    }
}
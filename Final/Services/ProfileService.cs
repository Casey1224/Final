using System;
using Final.Models;
using Final.Repositories;

namespace Final.Services
{
    public class ProfileService
    {
        private readonly ProfilesRepository _pr;
        private readonly keepRepository _kr;
        public ProfileService(ProfilesRepository pr, keepRepository kr)
        {
            this._pr = pr;
            this._kr = kr;
        }

        // internal Account getKeepById(int id)
        // {
        //     Account profile = _kr.getKeepById(id);
        //     if (profile == null)
        //     {
        //         throw new Exception($"No Profile at id: {id}");
        //     }
        //     return profile;
        // }

        internal Account GetOne(string id, string userId)
        {
            Account profile = _pr.GetOne(id);
            if (profile == null)
            {
                throw new Exception($"no profile at that id");
            }
            return profile;
        }
    }

}

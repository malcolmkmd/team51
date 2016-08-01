﻿using System.Collections.Generic;
using Bursify.Data.EF.CampaignUser;
using Bursify.Data.EF.Uow;

namespace Bursify.Data.EF.Repositories
{
    public class AccountRepository : Repository<Account>
    {
        public AccountRepository(DataSession dataSession) : base(dataSession)
        {
        }

        public Account GetAccount(int id)
        {
            return FindSingle(account => account.ID == id);
        }

        public List<Account> GetAllAccounts()
        {
            return LoadAll();
        }
    }
}
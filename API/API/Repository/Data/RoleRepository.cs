﻿using API.Context;
using API.Models;


namespace API.Repository.Data
{
    public class RoleRepository : GeneralRepository<MyContext, Role, string>
    {
        private readonly MyContext myContext;

        public RoleRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
    }
}

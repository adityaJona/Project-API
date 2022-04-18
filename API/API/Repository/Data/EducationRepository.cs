using API.Context;
using API.Models;

namespace API.Repository.Data
{
    public class EducationRepository :GeneralRepository<MyContext, Education, int>
    {
        private readonly MyContext myContext;
        public EducationRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

    }
}

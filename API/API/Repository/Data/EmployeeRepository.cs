using API.Context;
using API.Models;
using API.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext myContext;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        
        public IEnumerable GetMasterData()
        {
            var data = (from emp in myContext.Employee 
                        join acc in myContext.Account on emp.NIK equals acc.NIK
                        join pro in myContext.Profiling on acc.NIK equals pro.NIK
                        join edu in myContext.Education on pro.Education_Id equals edu.Id
                        join uni in myContext.University on edu.University_Id equals uni.Id
                        select new
                        {
                            NIK = emp.NIK,
                            FullName = emp.FirstName + " " + emp.LastName,
                            Phone = emp.Phone,
                            Gender = ((Gender)emp.Gender).ToString(),
                            Email = emp.Email,
                            BirthDate = emp.BirthDate,
                            Salary = emp.Salary,
                            EducationId = pro.Education_Id,
                            GPA = edu.GPA,
                            UniversityName = uni.Name
                        }).ToList();
            return data;
        }



    }
}

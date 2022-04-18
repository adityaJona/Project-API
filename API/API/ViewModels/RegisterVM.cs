using System;

namespace API.ViewModels
{
    public class RegisterVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthdate { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }

        public string Password { get; set; }

        public string Degree { get; set; }
        public string GPA { get; set; }

        public int UniversityId { get; set; }
    }
}

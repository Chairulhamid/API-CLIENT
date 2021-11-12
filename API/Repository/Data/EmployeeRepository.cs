using API.Context;
using API.Models;
using API.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee , string>
    {
        private readonly MyContext myContext;
        private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }
        public EmployeeRepository(MyContext myContext) : base(myContext)
         {
            this.myContext = myContext;
         }

        public int Login(LoginVM loginVM)
        {
             Employee employee = new Employee();
            Account account = new Account();
            var checkEmail = myContext.Employees.Where(x => x.Email == loginVM.Email).FirstOrDefault();
            if (checkEmail == null)
            {
                return 2;
            }
            var checkNik = checkEmail.NIK;

            var checkPass = myContext.Accounts.Find(checkEmail.NIK);

            bool validPass = BCrypt.Net.BCrypt.Verify(loginVM.Password, checkPass.Password);
            if (validPass)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }
        public int SignManager(SignManagerVM signManagerVM)
        {
            Employee employee = new Employee();
         
            var checkdata = myContext.Employees.Find(signManagerVM.NIK);
            employee.NIK = signManagerVM.NIK;
            if (checkdata == null)
            {
                return 2;
            }
            AccountRole accountRole = new AccountRole();
            accountRole.NIK = signManagerVM.NIK;
            accountRole.Role_Id = 5;
            myContext.AccountRoles.Add(accountRole);
            var result = myContext.SaveChanges();
            return result;
        }
        public int Register(RegisterVM registerVM)
        {
            Employee employee = new Employee();
            University university = new University();

            var checkUniversity = myContext.Universities.Find(registerVM.UniversityId);
            var checkdata = myContext.Employees.Find(registerVM.NIK);

            var checkPhone = myContext.Employees.Where(x => x.Phone == registerVM.PhoneNumber).FirstOrDefault();
            var checkEmail = myContext.Employees.Where(x => x.Email == registerVM.Email).FirstOrDefault();
            employee.NIK = registerVM.NIK;
            if (checkdata != null)
            {
                return 2;
            }
            if (checkPhone != null)
            {
                return 3;
            }
            if (checkEmail != null)
            {
                return 4;
            }
            if (checkUniversity == null)
            {
                return 5;
            }
            employee.FirstName = registerVM.FirstName;
            employee.LastName = registerVM.LastName;
            employee.Phone = registerVM.PhoneNumber;
            employee.BirthDate = registerVM.BirthDate;
            employee.Gender = (Models.Gender)registerVM.Gender;
            employee.Salary = registerVM.Salary;
            employee.Email = registerVM.Email;
            myContext.Employees.Add(employee);
            myContext.SaveChanges();

            Account account = new Account();
            account.NIK = registerVM.NIK;
            account.Password = BCrypt.Net.BCrypt.HashPassword(registerVM.Password, GetRandomSalt());
            myContext.Accounts.Add(account);
            myContext.SaveChanges();

            Education education = new Education();
            education.Degree = registerVM.Degree;
            education.Gpa = registerVM.Gpa;
            education.University_id = registerVM.UniversityId;
            myContext.Educations.Add(education);
            myContext.SaveChanges();

            Profilling profiling = new Profilling();
            profiling.NIK = registerVM.NIK;
            profiling.Education_Id = education.Id;
            myContext.Profillings.Add(profiling);
            myContext.SaveChanges();

            AccountRole accountRole = new AccountRole();
            accountRole.NIK = account.NIK;
            accountRole.Role_Id = registerVM.Role_Id;
            myContext.AccountRoles.Add(accountRole);
            var result = myContext.SaveChanges();
            return result;
        }

        public IEnumerable<RegisterVM> GetProfile()
        {
            var query = (from e in myContext.Employees
                         join a in myContext.Accounts on e.NIK equals a.NIK
                         join p in myContext.Profillings on a.NIK equals p.NIK
                         join ed in myContext.Educations on p.Education_Id equals ed.Id
                         join u in myContext.Universities on ed.University_id equals u.Id
                         join ar in myContext.AccountRoles on a.NIK equals ar.NIK
                         join r in myContext.Roles on ar.Role_Id equals r.Role_Id
                         orderby e.NIK
                         select new RegisterVM
                         {
                             NIK = e.NIK,
                             FirstName = e.FirstName,
                             LastName = e.LastName,
                             PhoneNumber = e.Phone,
                             BirthDate = e.BirthDate,
                             Gender = (ViewModel.Gender)e.Gender,
                             Salary = e.Salary,
                             Email = e.Email,
                             Password = a.Password,
                             Degree = ed.Degree,
                             Gpa = ed.Gpa,
                             UniversityId = ed.University_id,
                             Role_Id = ar.Role_Id
                         }).ToList();
            return query;
        }
        public IEnumerable <RegisterVM> GetProfileNik(string NIK)
        {
            var query = (from e in myContext.Employees
                         join a in myContext.Accounts on e.NIK equals a.NIK
                         join p in myContext.Profillings on a.NIK equals p.NIK
                         join ed in myContext.Educations on p.Education_Id equals ed.Id
                         join u in myContext.Universities on ed.University_id equals u.Id
                         join ar in myContext.AccountRoles on a.NIK equals ar.NIK
                         join r in myContext.Roles on ar.Role_Id equals r.Role_Id
                         orderby e.NIK
                         select new RegisterVM
                         {
                             NIK = e.NIK,
                             FirstName = e.FirstName,
                             LastName = e.LastName,
                             PhoneNumber = e.Phone,
                             BirthDate = e.BirthDate,
                             Salary = e.Salary,
                             Email = e.Email,
                             Gender = (ViewModel.Gender)e.Gender,
                             Password = a.Password,
                             Degree = ed.Degree,
                             Gpa = ed.Gpa,
                             UniversityId = ed.University_id,
                             Role_Id = ar.Role_Id
                         }).Where(e => e.NIK == NIK).ToList(); 
            return query;
        }
        public IEnumerable GetGender()
        {
            var result = from emp in myContext.Employees
                         group emp by emp.Gender into x
                         select new 
                         {
                             gender = x.Key,
                             value = x.Count()
                         };
            return result;
        }
        public IEnumerable GetEmpRole()
        {
            var result = from role in myContext.AccountRoles
                         group role by role.Role_Id into x
                         select new
                         {
                             role_Id = x.Key,
                             value = x.Count()
                         };
            return result;
        }
 /*       public IEnumerable GetSalary()
        {
            var result = from sale in myContext.Employees
                         group sale by sale.Salary into x
                         select new
                         {
                             salary = x.Key,
                             value = x.Count()
                         };
            return result;
        }*/
        public object[] GetSalary2()
        {
            var label1 = (from emp in myContext.Employees
                          select new
                          {
                              label = "Rp.2.000.000 - Rp.5.000.000",
                              value = myContext.Employees.Where(a => a.Salary <= 5000000 && a.Salary >= 2000000).Count()
                          }).First();
            var label2 = (from emp in myContext.Employees
                          select new
                          {
                              label = "Rp.5.000.001 - Rp.10.000.000",
                              value = myContext.Employees.Where(a => a.Salary <= 10000000 && a.Salary >= 5000001).Count()
                          }).First();
            var label3 = (from emp in myContext.Employees
                          select new
                          {
                              label = "< Rp.2.000.000",
                              value = myContext.Employees.Where(a => a.Salary < 2000000).Count()
                          }).First();
            var label4 = (from emp in myContext.Employees
                          select new
                          {
                              label = "> Rp.10.000.000",
                              value = myContext.Employees.Where(a => a.Salary > 10000000).Count()
                          }).First();
            List<Object> result = new List<Object>();
            result.Add(label3);
            result.Add(label1);
            result.Add(label2);
            result.Add(label4);
            return result.ToArray();
        }
        public IEnumerable GetDegree()
        {
            var result = from degre in myContext.Educations
                         group degre by degre.Degree into x
                         select new
                         {
                             degree = x.Key,
                             value = x.Count()
                         };
            return result;
        }
    }
}

using Entities.Data;
using Entities.DTOs;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class EmployeeRepo
    {
        private readonly EmployeeManagementToolContext _context = new EmployeeManagementToolContext();

        public List<Entities.Models.Employee> GetInfo()
        {
            var employees = _context.Employee.Where(model => model.DeletedAt == null).ToList();
            List<Entities.Models.Employee> testDataList = new List<Entities.Models.Employee>();

            foreach (Entities.Models.Employee employee in employees)
            {
                Entities.Models.Employee testData = new Entities.Models.Employee
                {
                    EmpId = employee.EmpId,
                    Firstname = employee.Firstname,
                    Lastname = employee.Lastname,
                    Email = employee.Email,
                    Dob = employee.Dob,
                    Gender = employee.Gender,
                    Password = employee.Password,
                    IsLocked = employee.IsLocked

                };

                testDataList.Add(testData);
            }

            return testDataList;
        }
        public Entities.Models.Employee getInfoById(long Id)
        {
            var employee = _context.Employee.Find(Id);
            return employee;
        }
        public Entities.Models.Employee DeleteEmployee(long Id)
        {
            var EmployeeData = _context.Employee.Find(Id);
            if (EmployeeData != null)
            {
                EmployeeData.DeletedAt = DateTime.Now;
                _context.Employee.Update(EmployeeData);
                _context.SaveChanges();
                return EmployeeData;

            }
            else
            {
                return null;
            }
        }
        public Employee CreateEmployee(CreateEmployeeDTO model)
        {
            var employee = new Employee()
            {
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Email = model.Email,
                Dob = model.Dob,
                Gender = model.Gender,
                Password = model.Password,
            };
            _context.Employee.Add(employee);

            var expiryDay = _context.PasswordExpiry.FirstOrDefault().PasswordexpiryDay;
            var passwordStatus = new PasswordExpiry()
            {
                EmpId = employee.EmpId,
                PasswordexpiryDay = expiryDay,
                PasswordexpiryStatus = false,
                PasswordUpdated = DateTime.Now,
            };
            _context.PasswordExpiry.Add(passwordStatus);
            _context.SaveChanges();
            return employee;
        }
        public Employee ChangeLockStatus(long Id)
        {
            var EmployeeDetails = _context.Employee.FirstOrDefault(employee => employee.EmpId == Id);
            if (EmployeeDetails.IsLocked == true)
            {
                EmployeeDetails.IsLocked = false;
            }
            else
            {
                EmployeeDetails.IsLocked = true;
            }
            _context.Employee.Update(EmployeeDetails);
            _context.SaveChanges();
            return EmployeeDetails;
        }
        public GetConfigurationDTO getConfiguration()
        {
            var day = _context.PasswordExpiry.FirstOrDefault().PasswordexpiryDay;
            var totalAttempt = _context.Employee.FirstOrDefault().TotalAttempts;
            var employee = new GetConfigurationDTO()
            {
                PasswordexpiryDay = day,
                TotalAttempts = totalAttempt
            };
            return employee;
        }
        public bool UpdateConfiguration(GetConfigurationDTO model)
        {
            var Employees = _context.Employee.ToList();
            if (Employees != null)
            {

                foreach (var employee in Employees)
                {
                    employee.TotalAttempts = model.TotalAttempts;
                    _context.Employee.Update(employee);
                }
                var Expiryday = _context.PasswordExpiry.ToList();
                foreach (var exp in Expiryday)
                {
                    exp.PasswordexpiryDay = model.PasswordexpiryDay;
                    _context.PasswordExpiry.Update(exp);
                }
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Admin AdminDetails(AdminDTO _admin)
        {
            var AdminCredetial = _context.Admin.FirstOrDefault(admin => admin.Email == _admin.Email && admin.DeletedAt == null);
            return AdminCredetial;
        }
        public bool updateFailAttempts(Admin _admin)
        {
            var AdminCredetial = _context.Admin.FirstOrDefault(admin => admin.Email == _admin.Email && admin.DeletedAt == null);
            if (AdminCredetial != null)
            {
                AdminCredetial.Attempts = AdminCredetial.Attempts + 1;
                _context.Admin.Update(AdminCredetial);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool FullAttempts(Admin _admin)
        {
            var AdminCredetial = _context.Admin.FirstOrDefault(admin => admin.Email == _admin.Email && admin.DeletedAt == null);
            if (AdminCredetial != null)
            {
                AdminCredetial.IsLocked = true;
                _context.Admin.Update(AdminCredetial);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool RegisterAdmin(AdminDTO _admin)
        {
            if (_admin != null)
            {
                var AdminRegister = new Admin()
                {
                    Email = _admin.Email,
                    Password = _admin.Password,
                    Firstname = _admin.Firstname,
                    Lastname = _admin.Lastname,
                    Dob = _admin.Dob,
                    TotalAttempts = 3,
                    /*Gender = _admin.Gender,*/
                };
                _context.Add(AdminRegister);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Employee getByEmail(string EmailId)
        {
            var EmployeeCredential = _context.Employee.FirstOrDefault(employee => employee.Email == EmailId);
            return EmployeeCredential;
        }
        public bool UpdateAttempt(string Email)
        {
            var EmployeeCredetial = _context.Employee.FirstOrDefault(employee => employee.Email == Email);
            if (EmployeeCredetial != null)
            {
                EmployeeCredetial.Attempts = 0;
                _context.Employee.Update(EmployeeCredetial);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateAttempsForWrong(string Email)
        {
            var EmployeeCredetial = _context.Employee.FirstOrDefault(employee => employee.Email == Email);
            if (EmployeeCredetial != null)
            {

                EmployeeCredetial.Attempts = EmployeeCredetial.Attempts + 1;
                _context.Employee.Update(EmployeeCredetial);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateLockStatus(string Email)
        {
            var EmployeeCredetial = _context.Employee.FirstOrDefault(employee => employee.Email == Email);
            if (EmployeeCredetial != null)
            {
                EmployeeCredetial.IsLocked = true;
                _context.Employee.Update(EmployeeCredetial);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool StatusUpdate(long EmpId)
        {
            var date = _context.PasswordExpiry.FirstOrDefault(m => m.EmpId == EmpId);
            if (date != null)
            {
                if (date.PasswordexpiryStatus)
                {
                    date.PasswordexpiryStatus = false;
                }
                else
                {
                    date.PasswordexpiryStatus = true;
                }

                _context.PasswordExpiry.Update(date);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool UpdatePassword(long EmpId, PasswordDTO model)
        {
            var oldPassword = _context.Employee.FirstOrDefault(password => password.EmpId == EmpId);
            if (oldPassword != null)
            {
                oldPassword.Password = model.ConfirmNewPassword;
                _context.Employee.Update(oldPassword);

                var ExpiryStatus = _context.PasswordExpiry.Find(EmpId);
                ExpiryStatus.PasswordUpdated = DateTime.Now;
                _context.PasswordExpiry.Update(ExpiryStatus);

                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        public Employee CheckValidPassword(long EmpId)
        {
            var oldPassword = _context.Employee.FirstOrDefault(password => password.EmpId == EmpId);
            return oldPassword;
        }
        public bool PasswordAttempt(string Email)
        {
            var AdminCredetial = _context.Admin.FirstOrDefault(admin => admin.Email == Email && admin.DeletedAt == null);
            if (AdminCredetial != null)
            {
                AdminCredetial.Attempts = AdminCredetial.Attempts + 1;
                _context.Admin.Update(AdminCredetial);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool LockAdmin(string Email)
        {
            var AdminCredetial = _context.Admin.FirstOrDefault(admin => admin.Email == Email && admin.DeletedAt == null);
            if (AdminCredetial != null)
            {
                AdminCredetial.IsLocked = true;
                _context.Admin.Update(AdminCredetial);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}

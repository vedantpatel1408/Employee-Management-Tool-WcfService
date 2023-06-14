using ApiModels;
using Entities.Data;
using Entities.DTOs;
using Entities.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class Service1 : IService1
    {
        private readonly EmployeeManagementToolContext _context = new EmployeeManagementToolContext();
        private readonly EmployeeRepo _employeeRepo = new EmployeeRepo();
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }


        public ApiResponse<List<Entities.Models.Employee>> GetInfo()
        {
            List<Entities.Models.Employee> employees = _employeeRepo.GetInfo();
            var apiResponse = new ApiResponse<List<Entities.Models.Employee>>();
            return apiResponse.HandleResponse(employees);
            //return employees;
            /*var values = _context.Employees.Where(employee => employee.DeletedAt == null).ToList();
            return Ok(values);*/
        }
        public Entities.Models.Employee GetInfoById(long Id)
        {
            var EmployeeData = _employeeRepo.getInfoById(Id);
            return EmployeeData;
        }
        public ApiResponse<Employee> DeleteEmployee(long Id)
        {
            //var EmployeeData = _context.Employee.Find(Id);
            //EmployeeData.DeletedAt = DateTime.Now;
            //_context.Employee.Update(EmployeeData);
            //_context.SaveChanges();
            //return EmployeeData;
            var apiResponse = new ApiResponse<Employee>();
            var EmployeeData = _employeeRepo.DeleteEmployee(Id);
            if (EmployeeData != null)
            {
                return apiResponse.HandleResponse(EmployeeData);
            }
            else
            {
                return apiResponse.HandleException("Id Not Found");
            }
            
        }
        public ApiResponse<Employee> CreateEmployee(CreateEmployeeDTO model)
        {
            //var employee = new Employee()
            //{
            //    Firstname = model.Firstname,
            //    Lastname = model.Lastname,
            //    Email = model.Email,
            //    Dob = model.Dob,
            //    Gender = model.Gender,
            //    Password = model.Password,
            //};
            //_context.Employee.Add(employee);

            //var expiryDay = _context.PasswordExpiry.FirstOrDefault().PasswordexpiryDay;
            //var passwordStatus = new PasswordExpiry()
            //{
            //    EmpId = employee.EmpId,
            //    PasswordexpiryDay = expiryDay,
            //    PasswordexpiryStatus = false,
            //    PasswordUpdated = DateTime.Now,
            //};
            //_context.PasswordExpiry.Add(passwordStatus);
            //_context.SaveChanges();
            var apiResponse = new ApiResponse<Employee>();
            var employee = _employeeRepo.CreateEmployee(model);
            if(employee != null)
            {
                return apiResponse.HandleResponse(employee);
            }
            else
            {
                return apiResponse.HandleException("invalid data");
            }
            
        }
        public ApiResponse<Employee> ChangeLockStatus(long Id)
        {
            //var EmployeeDetails = _context.Employee.FirstOrDefault(employee => employee.EmpId == Id);
            //if (EmployeeDetails.IsLocked == true)
            //{
            //    EmployeeDetails.IsLocked = false;
            //}
            //else
            //{
            //    EmployeeDetails.IsLocked = true;
            //}
            //_context.Employee.Update(EmployeeDetails);
            //_context.SaveChanges();
            var apiResponse = new ApiResponse<Employee>();
            var EmployeeDetails = _employeeRepo.ChangeLockStatus(Id);
            if(EmployeeDetails != null)
            {
                return apiResponse.HandleResponse(EmployeeDetails);
            }
            else
            {
                return apiResponse.HandleException("Invalid Id");
            }
            
        }
        public ApiResponse<GetConfigurationDTO> getConfiguration()
        {
            //var day = _context.PasswordExpiry.FirstOrDefault().PasswordexpiryDay;
            //var totalAttempt = _context.Employee.FirstOrDefault().TotalAttempts;
            //var employee = new GetConfigurationDTO()
            //{
            //    PasswordexpiryDay = day,
            //    TotalAttempts = totalAttempt
            //};
            var apiResponse = new ApiResponse<GetConfigurationDTO>();
            var employee = _employeeRepo.getConfiguration();
            if(employee != null)
            {
                return apiResponse.HandleResponse(employee);
            }
            else
            {
                return apiResponse.HandleException("Invalid data");
            }
            
        }
        public ApiResponse<bool> UpdateConfiguration(GetConfigurationDTO model)
        {
            //var Employees = _context.Employee.ToList();
            //foreach (var employee in Employees)
            //{
            //    employee.TotalAttempts = model.TotalAttempts;
            //    _context.Employee.Update(employee);
            //}
            //var Expiryday = _context.PasswordExpiry.ToList();
            //foreach (var exp in Expiryday)
            //{
            //    exp.PasswordexpiryDay = model.PasswordexpiryDay;
            //    _context.PasswordExpiry.Update(exp);
            //}
            //_context.SaveChanges();
            var apiResponse = new ApiResponse<bool>();
            var status = _employeeRepo.UpdateConfiguration(model);
            if(status == true)
            {
                return apiResponse.HandleResponse(status);
            }
            else
            {
                return apiResponse.HandleException("invalid Data");
            }
        }
        public ApiResponse<Admin> AdminDetails(AdminDTO _admin)
        {
            //var AdminCredetial = _context.Admin.FirstOrDefault(admin => admin.Email == _admin.Email && admin.DeletedAt == null);
            var apiResponse = new ApiResponse<Admin>();
            var AdminCredetial = _employeeRepo.AdminDetails(_admin);
            if(AdminCredetial != null)
            {
                return apiResponse.HandleResponse(AdminCredetial);
            }
            else
            {
                return apiResponse.HandleException("Invalid Data");
            }
            
        }
        public ApiResponse<bool> updateFailAttempts(Admin _admin)
        {
            //var AdminCredetial = _context.Admin.FirstOrDefault(admin => admin.Email == _admin.Email && admin.DeletedAt == null);

            //AdminCredetial.Attempts = AdminCredetial.Attempts + 1;
            //_context.Admin.Update(AdminCredetial);
            //_context.SaveChanges();
            var apiResponse = new ApiResponse<bool>();
            var status = _employeeRepo.updateFailAttempts(_admin);
            if(status == true)
            {
                return apiResponse.HandleResponse(status);
            }
            else
            {
                return apiResponse.HandleException("Invalid Details");
            }

        }
        public ApiResponse<bool> FullAttempts(Admin _admin)
        {
            //var AdminCredetial = _context.Admin.FirstOrDefault(admin => admin.Email == _admin.Email && admin.DeletedAt == null);

            //AdminCredetial.IsLocked = true;
            //_context.Admin.Update(AdminCredetial);
            //_context.SaveChanges();
            var apiResponse = new ApiResponse<bool>();

            var status = _employeeRepo.FullAttempts(_admin);
            if(status == true)
            {
                return apiResponse.HandleResponse(status);
            }
            else
            {
                return apiResponse.HandleException("Invalid Data");
            }

        }
        public ApiResponse<bool> RegisterAdmin(AdminDTO _admin)
        {
            //var AdminRegister = new Admin()
            //{
            //    Email = _admin.Email,
            //    Password = _admin.Password,
            //    Firstname = _admin.Firstname,
            //    Lastname = _admin.Lastname,
            //    Dob = _admin.Dob,
            //    TotalAttempts = 3,
            //    /*Gender = _admin.Gender,*/
            //};
            //_context.Add(AdminRegister);
            //_context.SaveChanges();
            var apiResponse = new ApiResponse<bool>();
            var status = _employeeRepo.RegisterAdmin(_admin);
            if(status == true)
            {
                return apiResponse.HandleResponse(status);
            }
            else
            {
                return apiResponse.HandleException("Invalid Details");
            }
        }
        public ApiResponse<Employee> getByEmail(string EmailId)
        {
            //var EmployeeCredential = _context.Employee.FirstOrDefault(employee => employee.Email == EmailId);
            var apiResponse = new ApiResponse<Employee>();
            var EmployeeCredential = _employeeRepo.getByEmail(EmailId);
            if(EmployeeCredential != null)
            {
                return apiResponse.HandleResponse(EmployeeCredential);
            }
            else
            {
                return apiResponse.HandleException("Invalid Details");
            }
        }
        public ApiResponse<bool> UpdateAttempt(string Email)
        {
            //var EmployeeCredetial = _context.Employee.FirstOrDefault(employee => employee.Email == Email);
            //EmployeeCredetial.Attempts = 0;
            //_context.Employee.Update(EmployeeCredetial);
            //_context.SaveChanges();
            var apiResponse = new ApiResponse<bool>();

            var status = _employeeRepo.UpdateAttempt(Email);
            if (status == true)
            {
                return apiResponse.HandleResponse(status);
            }
            else
            {
                return apiResponse.HandleException("Invalid Email");
            }
        }
        public ApiResponse<bool> UpdateAttempsForWrong(string Email)
        {
            //var EmployeeCredetial = _context.Employee.FirstOrDefault(employee => employee.Email == Email);
            //EmployeeCredetial.Attempts = EmployeeCredetial.Attempts + 1;
            //_context.Employee.Update(EmployeeCredetial);
            //_context.SaveChanges();
            var apiResponse = new ApiResponse<bool>();
            var status =_employeeRepo.UpdateAttempsForWrong(Email);
            if(status == true)
            {
                return apiResponse.HandleResponse(status);
            }
            else
            {
                return apiResponse.HandleException("Invalid Details");
            }
        }
        public ApiResponse<bool> UpdateLockStatus(string Email)
        {
            //var EmployeeCredetial = _context.Employee.FirstOrDefault(employee => employee.Email == Email);
            //EmployeeCredetial.IsLocked = true;
            //_context.Employee.Update(EmployeeCredetial);
            //_context.SaveChanges();
            var apiResponse = new ApiResponse<bool>();
            var status = _employeeRepo.UpdateLockStatus(Email);
            if(status == true)
            {
                return apiResponse.HandleResponse(status);
            }
            else
            {
                return apiResponse.HandleException("Invalid Email");
            }
        }
        public ApiResponse<PasswordExpiry> CheckStatusOfExpiry(long Id)
        {
            var apiResponse = new ApiResponse<PasswordExpiry>();
            var date = _context.PasswordExpiry.FirstOrDefault(m => m.EmpId == Id);
            if(date != null)
            {
                return apiResponse.HandleResponse(date);
            }
            else
            {
                return apiResponse.HandleException("Id Not Found");
            }
            
        }
        public ApiResponse<bool> StatusUpdate(long EmpId)
        {
            //var date = _context.PasswordExpiry.FirstOrDefault(m => m.EmpId == EmpId);
            //if (date != null)
            //{
            //    if (date.PasswordexpiryStatus)
            //    {
            //        date.PasswordexpiryStatus = false;
            //    }
            //    else
            //    {
            //        date.PasswordexpiryStatus = true;
            //    }

            //    _context.PasswordExpiry.Update(date);
            //    _context.SaveChanges();
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            var apiResponse = new ApiResponse<bool>();
            bool date = _employeeRepo.StatusUpdate(EmpId);
            if(date == true)
            {
                return apiResponse.HandleResponse(date);
            }
            else
            {
                return apiResponse.HandleException("Id Not Found");
            }

        }
        public ApiResponse<bool> UpdatePassword(long EmpId, PasswordDTO model)
        {
            //var oldPassword = _context.Employee.FirstOrDefault(password => password.EmpId == EmpId);
            //if (oldPassword != null)
            //{
            //    oldPassword.Password = model.ConfirmNewPassword;
            //    _context.Employee.Update(oldPassword);

            //    var ExpiryStatus = _context.PasswordExpiry.Find(EmpId);
            //    ExpiryStatus.PasswordUpdated = DateTime.Now;
            //    _context.PasswordExpiry.Update(ExpiryStatus);

            //    _context.SaveChanges();
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            var apiResponse = new ApiResponse<bool>();
            bool passwordStatus = _employeeRepo.UpdatePassword(EmpId, model);
            if(passwordStatus == true)
            {
                return apiResponse.HandleResponse(passwordStatus);
            }
            else
            {
                return apiResponse.HandleException("Invalid Details");
            }

        }
        public ApiResponse<Employee> CheckValidPassword(long EmpId)
        {
            //var oldPassword = _context.Employee.FirstOrDefault(password => password.EmpId == EmpId);
            var apiResponse = new ApiResponse<Employee>();
            var oldPassword = _employeeRepo.CheckValidPassword(EmpId);
            if(oldPassword != null)
            {
                return apiResponse.HandleResponse(oldPassword);
            }
            else
            {
                return apiResponse.HandleException("Id Not Found");
            }
            
        }
        public ApiResponse<bool> PasswordAttempt(string Email)
        {
            var apiResponse = new ApiResponse<bool>();
            var status = _employeeRepo.PasswordAttempt(Email);
            if (status == true)
            {
                return apiResponse.HandleResponse(status);
            }
            else
            {
                return apiResponse.HandleException("Invalid Email");
            }
        }
        public ApiResponse<bool> LockAdmin(string Email)
        {
            var apiResponse = new ApiResponse<bool>();
            var status = _employeeRepo.LockAdmin(Email);
            if(status == true)
            {
                return apiResponse.HandleResponse(status);
            }
            else
            {
                return apiResponse.HandleException("Invalid Email");
            }
        }
        


    }
}

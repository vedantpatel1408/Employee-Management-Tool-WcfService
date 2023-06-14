using ApiModels;
using Entities.DTOs;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);
        // TODO: Add your service operations here

        [OperationContract]
        ApiResponse<List<Entities.Models.Employee>> GetInfo();
        //List<Entities.Models.Employee> GetInfo();

        [OperationContract]
        Entities.Models.Employee GetInfoById(long Id);
        [OperationContract]
        ApiResponse<Employee> DeleteEmployee(long Id);

        [OperationContract]
        ApiResponse<Employee> CreateEmployee(CreateEmployeeDTO model);
        [OperationContract]
        ApiResponse<Employee> ChangeLockStatus(long Id);
        [OperationContract]
        ApiResponse<GetConfigurationDTO> getConfiguration();
        [OperationContract]
        ApiResponse<bool> UpdateConfiguration(GetConfigurationDTO model);
        [OperationContract]
        ApiResponse<Admin> AdminDetails(AdminDTO model);
        [OperationContract]
        ApiResponse<bool> updateFailAttempts(Admin _admin);
        [OperationContract]
        ApiResponse<bool> FullAttempts(Admin _admin);
        [OperationContract]
        ApiResponse<bool> RegisterAdmin(AdminDTO _admin);
        [OperationContract]
        ApiResponse<Employee> getByEmail(string EmailId);
        [OperationContract]
        ApiResponse<bool> UpdateAttempt(string Email);
        [OperationContract]
        ApiResponse<bool> UpdateAttempsForWrong(string Email);
        [OperationContract]
        ApiResponse<bool> UpdateLockStatus(string Email);
        [OperationContract]
        ApiResponse<PasswordExpiry> CheckStatusOfExpiry(long Id);
        [OperationContract]
        ApiResponse<bool> StatusUpdate(long EmpId);
        [OperationContract]
        ApiResponse<bool> UpdatePassword(long EmpId, PasswordDTO model);
        [OperationContract]
        ApiResponse<Employee> CheckValidPassword(long EmpId);
        [OperationContract]
        ApiResponse<bool> PasswordAttempt(string Email);
        [OperationContract]
        ApiResponse<bool> LockAdmin(string Email);
        


    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
    
}

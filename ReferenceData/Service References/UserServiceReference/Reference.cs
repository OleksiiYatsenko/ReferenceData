﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReferenceData.UserServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UserServiceReference.IUsersService")]
    public interface IUsersService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersService/GetUsers", ReplyAction="http://tempuri.org/IUsersService/GetUsersResponse")]
        ReferenceData.Model.User[] GetUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersService/GetUsers", ReplyAction="http://tempuri.org/IUsersService/GetUsersResponse")]
        System.Threading.Tasks.Task<ReferenceData.Model.User[]> GetUsersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersService/AddOrUpdate", ReplyAction="http://tempuri.org/IUsersService/AddOrUpdateResponse")]
        ReferenceData.Model.User AddOrUpdate(ReferenceData.Model.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersService/AddOrUpdate", ReplyAction="http://tempuri.org/IUsersService/AddOrUpdateResponse")]
        System.Threading.Tasks.Task<ReferenceData.Model.User> AddOrUpdateAsync(ReferenceData.Model.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersService/GetItem", ReplyAction="http://tempuri.org/IUsersService/GetItemResponse")]
        ReferenceData.Model.User GetItem(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsersService/GetItem", ReplyAction="http://tempuri.org/IUsersService/GetItemResponse")]
        System.Threading.Tasks.Task<ReferenceData.Model.User> GetItemAsync(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUsersServiceChannel : ReferenceData.UserServiceReference.IUsersService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UsersServiceClient : System.ServiceModel.ClientBase<ReferenceData.UserServiceReference.IUsersService>, ReferenceData.UserServiceReference.IUsersService {
        
        public UsersServiceClient() {
        }
        
        public UsersServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UsersServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UsersServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UsersServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ReferenceData.Model.User[] GetUsers() {
            return base.Channel.GetUsers();
        }
        
        public System.Threading.Tasks.Task<ReferenceData.Model.User[]> GetUsersAsync() {
            return base.Channel.GetUsersAsync();
        }
        
        public ReferenceData.Model.User AddOrUpdate(ReferenceData.Model.User user) {
            return base.Channel.AddOrUpdate(user);
        }
        
        public System.Threading.Tasks.Task<ReferenceData.Model.User> AddOrUpdateAsync(ReferenceData.Model.User user) {
            return base.Channel.AddOrUpdateAsync(user);
        }
        
        public ReferenceData.Model.User GetItem(int id) {
            return base.Channel.GetItem(id);
        }
        
        public System.Threading.Tasks.Task<ReferenceData.Model.User> GetItemAsync(int id) {
            return base.Channel.GetItemAsync(id);
        }
    }
}

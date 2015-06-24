﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessSafe.WebSite.DocumentTypeService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DocumentTypeDto", Namespace="http://schemas.datacontract.org/2004/07/ClientDocumentation.Application.DataTrans" +
        "ferObjects.Documents")]
    [System.SerializableAttribute()]
    public partial class DocumentTypeDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private BusinessSafe.WebSite.DocumentTypeService.DepartmentDto DepartmentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<long> IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TitleField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public BusinessSafe.WebSite.DocumentTypeService.DepartmentDto Department {
            get {
                return this.DepartmentField;
            }
            set {
                if ((object.ReferenceEquals(this.DepartmentField, value) != true)) {
                    this.DepartmentField = value;
                    this.RaisePropertyChanged("Department");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<long> Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Title {
            get {
                return this.TitleField;
            }
            set {
                if ((object.ReferenceEquals(this.TitleField, value) != true)) {
                    this.TitleField = value;
                    this.RaisePropertyChanged("Title");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DepartmentDto", Namespace="http://schemas.datacontract.org/2004/07/ClientDocumentation.Application.DataTrans" +
        "ferObjects.Security")]
    [System.SerializableAttribute()]
    public partial class DepartmentDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private BusinessSafe.WebSite.DocumentTypeService.UserDto CreatedByField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> CreatedOnField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool DeletedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private BusinessSafe.WebSite.DocumentTypeService.DocumentTypeDto[] DocumentTypesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<long> IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string KeyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private BusinessSafe.WebSite.DocumentTypeService.UserDto LastModifiedByField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> LastModifiedOnField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public BusinessSafe.WebSite.DocumentTypeService.UserDto CreatedBy {
            get {
                return this.CreatedByField;
            }
            set {
                if ((object.ReferenceEquals(this.CreatedByField, value) != true)) {
                    this.CreatedByField = value;
                    this.RaisePropertyChanged("CreatedBy");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> CreatedOn {
            get {
                return this.CreatedOnField;
            }
            set {
                if ((this.CreatedOnField.Equals(value) != true)) {
                    this.CreatedOnField = value;
                    this.RaisePropertyChanged("CreatedOn");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Deleted {
            get {
                return this.DeletedField;
            }
            set {
                if ((this.DeletedField.Equals(value) != true)) {
                    this.DeletedField = value;
                    this.RaisePropertyChanged("Deleted");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public BusinessSafe.WebSite.DocumentTypeService.DocumentTypeDto[] DocumentTypes {
            get {
                return this.DocumentTypesField;
            }
            set {
                if ((object.ReferenceEquals(this.DocumentTypesField, value) != true)) {
                    this.DocumentTypesField = value;
                    this.RaisePropertyChanged("DocumentTypes");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<long> Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Key {
            get {
                return this.KeyField;
            }
            set {
                if ((object.ReferenceEquals(this.KeyField, value) != true)) {
                    this.KeyField = value;
                    this.RaisePropertyChanged("Key");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public BusinessSafe.WebSite.DocumentTypeService.UserDto LastModifiedBy {
            get {
                return this.LastModifiedByField;
            }
            set {
                if ((object.ReferenceEquals(this.LastModifiedByField, value) != true)) {
                    this.LastModifiedByField = value;
                    this.RaisePropertyChanged("LastModifiedBy");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> LastModifiedOn {
            get {
                return this.LastModifiedOnField;
            }
            set {
                if ((this.LastModifiedOnField.Equals(value) != true)) {
                    this.LastModifiedOnField = value;
                    this.RaisePropertyChanged("LastModifiedOn");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserDto", Namespace="http://schemas.datacontract.org/2004/07/ClientDocumentation.Application.DataTrans" +
        "ferObjects.Security")]
    [System.SerializableAttribute()]
    public partial class UserDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool DeletedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private BusinessSafe.WebSite.DocumentTypeService.DepartmentDto DepartmentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<long> IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string[] PermissionKeysField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private BusinessSafe.WebSite.DocumentTypeService.RoleDto RoleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UsernameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Deleted {
            get {
                return this.DeletedField;
            }
            set {
                if ((this.DeletedField.Equals(value) != true)) {
                    this.DeletedField = value;
                    this.RaisePropertyChanged("Deleted");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public BusinessSafe.WebSite.DocumentTypeService.DepartmentDto Department {
            get {
                return this.DepartmentField;
            }
            set {
                if ((object.ReferenceEquals(this.DepartmentField, value) != true)) {
                    this.DepartmentField = value;
                    this.RaisePropertyChanged("Department");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<long> Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] PermissionKeys {
            get {
                return this.PermissionKeysField;
            }
            set {
                if ((object.ReferenceEquals(this.PermissionKeysField, value) != true)) {
                    this.PermissionKeysField = value;
                    this.RaisePropertyChanged("PermissionKeys");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public BusinessSafe.WebSite.DocumentTypeService.RoleDto Role {
            get {
                return this.RoleField;
            }
            set {
                if ((object.ReferenceEquals(this.RoleField, value) != true)) {
                    this.RoleField = value;
                    this.RaisePropertyChanged("Role");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Username {
            get {
                return this.UsernameField;
            }
            set {
                if ((object.ReferenceEquals(this.UsernameField, value) != true)) {
                    this.UsernameField = value;
                    this.RaisePropertyChanged("Username");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RoleDto", Namespace="http://schemas.datacontract.org/2004/07/ClientDocumentation.Application.DataTrans" +
        "ferObjects.Security")]
    [System.SerializableAttribute()]
    public partial class RoleDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<long> IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private BusinessSafe.WebSite.DocumentTypeService.PermissionRoleDto[] PermissionRolesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RoleNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<long> Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public BusinessSafe.WebSite.DocumentTypeService.PermissionRoleDto[] PermissionRoles {
            get {
                return this.PermissionRolesField;
            }
            set {
                if ((object.ReferenceEquals(this.PermissionRolesField, value) != true)) {
                    this.PermissionRolesField = value;
                    this.RaisePropertyChanged("PermissionRoles");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RoleName {
            get {
                return this.RoleNameField;
            }
            set {
                if ((object.ReferenceEquals(this.RoleNameField, value) != true)) {
                    this.RoleNameField = value;
                    this.RaisePropertyChanged("RoleName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PermissionRoleDto", Namespace="http://schemas.datacontract.org/2004/07/ClientDocumentation.Application.DataTrans" +
        "ferObjects.Security")]
    [System.SerializableAttribute()]
    public partial class PermissionRoleDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<long> IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private BusinessSafe.WebSite.DocumentTypeService.PermissionDto PermissionField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<long> Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public BusinessSafe.WebSite.DocumentTypeService.PermissionDto Permission {
            get {
                return this.PermissionField;
            }
            set {
                if ((object.ReferenceEquals(this.PermissionField, value) != true)) {
                    this.PermissionField = value;
                    this.RaisePropertyChanged("Permission");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PermissionDto", Namespace="http://schemas.datacontract.org/2004/07/ClientDocumentation.Application.DataTrans" +
        "ferObjects.Security")]
    [System.SerializableAttribute()]
    public partial class PermissionDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<long> IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string KeyField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<long> Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Key {
            get {
                return this.KeyField;
            }
            set {
                if ((object.ReferenceEquals(this.KeyField, value) != true)) {
                    this.KeyField = value;
                    this.RaisePropertyChanged("Key");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DocumentTypeService.IDocumentTypeService")]
    public interface IDocumentTypeService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDocumentTypeService/GetAll", ReplyAction="http://tempuri.org/IDocumentTypeService/GetAllResponse")]
        BusinessSafe.WebSite.DocumentTypeService.DocumentTypeDto[] GetAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDocumentTypeService/GetForCurrentUser", ReplyAction="http://tempuri.org/IDocumentTypeService/GetForCurrentUserResponse")]
        BusinessSafe.WebSite.DocumentTypeService.DocumentTypeDto[] GetForCurrentUser();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDocumentTypeService/GetByDepartmentId", ReplyAction="http://tempuri.org/IDocumentTypeService/GetByDepartmentIdResponse")]
        BusinessSafe.WebSite.DocumentTypeService.DocumentTypeDto[] GetByDepartmentId(long departmentId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDocumentTypeService/GetByIds", ReplyAction="http://tempuri.org/IDocumentTypeService/GetByIdsResponse")]
        BusinessSafe.WebSite.DocumentTypeService.DocumentTypeDto[] GetByIds(long[] ids);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDocumentTypeServiceChannel : BusinessSafe.WebSite.DocumentTypeService.IDocumentTypeService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DocumentTypeServiceClient : System.ServiceModel.ClientBase<BusinessSafe.WebSite.DocumentTypeService.IDocumentTypeService>, BusinessSafe.WebSite.DocumentTypeService.IDocumentTypeService {
        
        public DocumentTypeServiceClient() {
        }
        
        public DocumentTypeServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DocumentTypeServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DocumentTypeServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DocumentTypeServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public BusinessSafe.WebSite.DocumentTypeService.DocumentTypeDto[] GetAll() {
            return base.Channel.GetAll();
        }
        
        public BusinessSafe.WebSite.DocumentTypeService.DocumentTypeDto[] GetForCurrentUser() {
            return base.Channel.GetForCurrentUser();
        }
        
        public BusinessSafe.WebSite.DocumentTypeService.DocumentTypeDto[] GetByDepartmentId(long departmentId) {
            return base.Channel.GetByDepartmentId(departmentId);
        }
        
        public BusinessSafe.WebSite.DocumentTypeService.DocumentTypeDto[] GetByIds(long[] ids) {
            return base.Channel.GetByIds(ids);
        }
    }
}
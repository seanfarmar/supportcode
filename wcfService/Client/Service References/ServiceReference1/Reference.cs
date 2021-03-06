﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CreateProductMessage", Namespace="http://schemas.datacontract.org/2004/07/Messages")]
    [System.SerializableAttribute()]
    public partial class CreateProductMessage : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid GuidIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal ProductNumberField;
        
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
        public System.Guid GuidId {
            get {
                return this.GuidIdField;
            }
            set {
                if ((this.GuidIdField.Equals(value) != true)) {
                    this.GuidIdField = value;
                    this.RaisePropertyChanged("GuidId");
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
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal ProductNumber {
            get {
                return this.ProductNumberField;
            }
            set {
                if ((this.ProductNumberField.Equals(value) != true)) {
                    this.ProductNumberField = value;
                    this.RaisePropertyChanged("ProductNumber");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.ICreateProductService")]
    public interface ICreateProductService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreateProductService/Create", ReplyAction="http://tempuri.org/ICreateProductService/CreateResponse")]
        Client.ServiceReference1.CreateResponse Create(Client.ServiceReference1.CreateRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICreateProductService/Create", ReplyAction="http://tempuri.org/ICreateProductService/CreateResponse")]
        System.Threading.Tasks.Task<Client.ServiceReference1.CreateResponse> CreateAsync(Client.ServiceReference1.CreateRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Create", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class CreateRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public Client.ServiceReference1.CreateProductMessage message;
        
        public CreateRequest(Client.ServiceReference1.CreateProductMessage message) {
            this.message = message;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="CreateResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class CreateResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public int CreateResult;
        
        public CreateResponse() {
        }
        
        public CreateResponse(int CreateResult) {
            this.CreateResult = CreateResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICreateProductServiceChannel : Client.ServiceReference1.ICreateProductService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CreateProductServiceClient : System.ServiceModel.ClientBase<Client.ServiceReference1.ICreateProductService>, Client.ServiceReference1.ICreateProductService {
        
        public CreateProductServiceClient() {
        }
        
        public CreateProductServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CreateProductServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CreateProductServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CreateProductServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Client.ServiceReference1.CreateResponse Create(Client.ServiceReference1.CreateRequest request) {
            return base.Channel.Create(request);
        }
        
        public System.Threading.Tasks.Task<Client.ServiceReference1.CreateResponse> CreateAsync(Client.ServiceReference1.CreateRequest request) {
            return base.Channel.CreateAsync(request);
        }
    }
}

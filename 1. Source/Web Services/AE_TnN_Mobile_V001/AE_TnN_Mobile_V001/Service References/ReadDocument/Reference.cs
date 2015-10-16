﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AE_TnN_Mobile_V001.ReadDocument {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ReadDocument.Service1Soap")]
    public interface Service1Soap {
        
        // CODEGEN: Parameter 'docbinaryarray' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ReadDocument", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AE_TnN_Mobile_V001.ReadDocument.ReadDocumentResponse ReadDocument(AE_TnN_Mobile_V001.ReadDocument.ReadDocumentRequest request);
        
        // CODEGEN: Parameter 'docbinaryarray' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/NeutralCheck", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        AE_TnN_Mobile_V001.ReadDocument.NeutralCheckResponse NeutralCheck(AE_TnN_Mobile_V001.ReadDocument.NeutralCheckRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ReadDocument", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class ReadDocumentRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string ItemCode;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] docbinaryarray;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public string Imgname;
        
        public ReadDocumentRequest() {
        }
        
        public ReadDocumentRequest(string ItemCode, byte[] docbinaryarray, string Imgname) {
            this.ItemCode = ItemCode;
            this.docbinaryarray = docbinaryarray;
            this.Imgname = Imgname;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ReadDocumentResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class ReadDocumentResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public System.Data.DataTable ReadDocumentResult;
        
        public ReadDocumentResponse() {
        }
        
        public ReadDocumentResponse(System.Data.DataTable ReadDocumentResult) {
            this.ReadDocumentResult = ReadDocumentResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="NeutralCheck", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class NeutralCheckRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string ItemCode;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] docbinaryarray;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public string Imgname;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=3)]
        public string CaseID;
        
        public NeutralCheckRequest() {
        }
        
        public NeutralCheckRequest(string ItemCode, byte[] docbinaryarray, string Imgname, string CaseID) {
            this.ItemCode = ItemCode;
            this.docbinaryarray = docbinaryarray;
            this.Imgname = Imgname;
            this.CaseID = CaseID;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="NeutralCheckResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class NeutralCheckResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public bool NeutralCheckResult;
        
        public NeutralCheckResponse() {
        }
        
        public NeutralCheckResponse(bool NeutralCheckResult) {
            this.NeutralCheckResult = NeutralCheckResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface Service1SoapChannel : AE_TnN_Mobile_V001.ReadDocument.Service1Soap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1SoapClient : System.ServiceModel.ClientBase<AE_TnN_Mobile_V001.ReadDocument.Service1Soap>, AE_TnN_Mobile_V001.ReadDocument.Service1Soap {
        
        public Service1SoapClient() {
        }
        
        public Service1SoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1SoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1SoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1SoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AE_TnN_Mobile_V001.ReadDocument.ReadDocumentResponse AE_TnN_Mobile_V001.ReadDocument.Service1Soap.ReadDocument(AE_TnN_Mobile_V001.ReadDocument.ReadDocumentRequest request) {
            return base.Channel.ReadDocument(request);
        }
        
        public System.Data.DataTable ReadDocument(string ItemCode, byte[] docbinaryarray, string Imgname) {
            AE_TnN_Mobile_V001.ReadDocument.ReadDocumentRequest inValue = new AE_TnN_Mobile_V001.ReadDocument.ReadDocumentRequest();
            inValue.ItemCode = ItemCode;
            inValue.docbinaryarray = docbinaryarray;
            inValue.Imgname = Imgname;
            AE_TnN_Mobile_V001.ReadDocument.ReadDocumentResponse retVal = ((AE_TnN_Mobile_V001.ReadDocument.Service1Soap)(this)).ReadDocument(inValue);
            return retVal.ReadDocumentResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        AE_TnN_Mobile_V001.ReadDocument.NeutralCheckResponse AE_TnN_Mobile_V001.ReadDocument.Service1Soap.NeutralCheck(AE_TnN_Mobile_V001.ReadDocument.NeutralCheckRequest request) {
            return base.Channel.NeutralCheck(request);
        }
        
        public bool NeutralCheck(string ItemCode, byte[] docbinaryarray, string Imgname, string CaseID) {
            AE_TnN_Mobile_V001.ReadDocument.NeutralCheckRequest inValue = new AE_TnN_Mobile_V001.ReadDocument.NeutralCheckRequest();
            inValue.ItemCode = ItemCode;
            inValue.docbinaryarray = docbinaryarray;
            inValue.Imgname = Imgname;
            inValue.CaseID = CaseID;
            AE_TnN_Mobile_V001.ReadDocument.NeutralCheckResponse retVal = ((AE_TnN_Mobile_V001.ReadDocument.Service1Soap)(this)).NeutralCheck(inValue);
            return retVal.NeutralCheckResult;
        }
    }
}

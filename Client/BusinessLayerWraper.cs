﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4927
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessLayer
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EventData", Namespace="http://schemas.datacontract.org/2004/07/BusinessLayer")]
    public partial class EventData : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private System.DateTime DebutField;
        
        private System.DateTime FinField;
        
        private string LieuField;
        
        private string MatiereField;
        
        private string NomField;
        
        private bool ObligatoireField;
        
        private string TypeField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Debut
        {
            get
            {
                return this.DebutField;
            }
            set
            {
                this.DebutField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Fin
        {
            get
            {
                return this.FinField;
            }
            set
            {
                this.FinField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Lieu
        {
            get
            {
                return this.LieuField;
            }
            set
            {
                this.LieuField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Matiere
        {
            get
            {
                return this.MatiereField;
            }
            set
            {
                this.MatiereField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Nom
        {
            get
            {
                return this.NomField;
            }
            set
            {
                this.NomField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Obligatoire
        {
            get
            {
                return this.ObligatoireField;
            }
            set
            {
                this.ObligatoireField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Type
        {
            get
            {
                return this.TypeField;
            }
            set
            {
                this.TypeField = value;
            }
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName="IBusinessLayer")]
public interface IBusinessLayer
{
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessLayer/getEventsByCampus", ReplyAction="http://tempuri.org/IBusinessLayer/getEventsByCampusResponse")]
    BusinessLayer.EventData[] getEventsByCampus(string CampusName, System.DateTime Start, System.DateTime Stop, System.DateTime LastUpdate);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessLayer/getEventsByUniversity", ReplyAction="http://tempuri.org/IBusinessLayer/getEventsByUniversityResponse")]
    BusinessLayer.EventData[] getEventsByUniversity(System.DateTime Start, System.DateTime Stop, System.DateTime LastUpdate);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessLayer/getEventsByPeriode", ReplyAction="http://tempuri.org/IBusinessLayer/getEventsByPeriodeResponse")]
    BusinessLayer.EventData[] getEventsByPeriode(string PeriodeName, System.DateTime Start, System.DateTime Stop, System.DateTime LastUpdate);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessLayer/getEventsByClass", ReplyAction="http://tempuri.org/IBusinessLayer/getEventsByClassResponse")]
    BusinessLayer.EventData[] getEventsByClass(string ClassName, System.DateTime Start, System.DateTime Stop, System.DateTime LastUpdate);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessLayer/getPrivateEvents", ReplyAction="http://tempuri.org/IBusinessLayer/getPrivateEventsResponse")]
    BusinessLayer.EventData[] getPrivateEvents(System.DateTime Start, System.DateTime Stop, System.DateTime LastUpdate);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessLayer/getCampusNames", ReplyAction="http://tempuri.org/IBusinessLayer/getCampusNamesResponse")]
    string[] getCampusNames();
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessLayer/getClassesNames", ReplyAction="http://tempuri.org/IBusinessLayer/getClassesNamesResponse")]
    string[] getClassesNames();
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessLayer/getPromotionsNames", ReplyAction="http://tempuri.org/IBusinessLayer/getPromotionsNamesResponse")]
    string[] getPromotionsNames();
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessLayer/getPeriodeNames", ReplyAction="http://tempuri.org/IBusinessLayer/getPeriodeNamesResponse")]
    string[] getPeriodeNames();
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessLayer/addUser", ReplyAction="http://tempuri.org/IBusinessLayer/addUserResponse")]
    string addUser(string UserName, string UserPassword, string UserClassName);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessLayer/addCampus", ReplyAction="http://tempuri.org/IBusinessLayer/addCampusResponse")]
    string addCampus(string CampusName);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessLayer/addClasse", ReplyAction="http://tempuri.org/IBusinessLayer/addClasseResponse")]
    string addClasse(string ClassName, string CampusName, string PeriodeName);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessLayer/addPromotion", ReplyAction="http://tempuri.org/IBusinessLayer/addPromotionResponse")]
    string addPromotion(string PromotionName);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessLayer/addMatiere", ReplyAction="http://tempuri.org/IBusinessLayer/addMatiereResponse")]
    string addMatiere(string MatiereName, int MatiereHours);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessLayer/addPeriode", ReplyAction="http://tempuri.org/IBusinessLayer/addPeriodeResponse")]
    string addPeriode(string PeriodeName, string PromoName, System.DateTime PeriodeStart, System.DateTime PeriodeEnd);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessLayer/grantNewRight", ReplyAction="http://tempuri.org/IBusinessLayer/grantNewRightResponse")]
    string grantNewRight(string UserName, int Type, string CampusName);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBusinessLayer/addEvent", ReplyAction="http://tempuri.org/IBusinessLayer/addEventResponse")]
    string addEvent(string EventName, System.DateTime Start, System.DateTime End, bool Obligatoire, string IntervenatName, string CampusName, string PeriodeName, string MatiereName, string Type, string Lieu);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
public interface IBusinessLayerChannel : IBusinessLayer, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
public partial class BusinessLayerClient : System.ServiceModel.ClientBase<IBusinessLayer>, IBusinessLayer
{
    
    public BusinessLayerClient()
    {
    }
    
    public BusinessLayerClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public BusinessLayerClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public BusinessLayerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public BusinessLayerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    public BusinessLayer.EventData[] getEventsByCampus(string CampusName, System.DateTime Start, System.DateTime Stop, System.DateTime LastUpdate)
    {
        return base.Channel.getEventsByCampus(CampusName, Start, Stop, LastUpdate);
    }
    
    public BusinessLayer.EventData[] getEventsByUniversity(System.DateTime Start, System.DateTime Stop, System.DateTime LastUpdate)
    {
        return base.Channel.getEventsByUniversity(Start, Stop, LastUpdate);
    }
    
    public BusinessLayer.EventData[] getEventsByPeriode(string PeriodeName, System.DateTime Start, System.DateTime Stop, System.DateTime LastUpdate)
    {
        return base.Channel.getEventsByPeriode(PeriodeName, Start, Stop, LastUpdate);
    }
    
    public BusinessLayer.EventData[] getEventsByClass(string ClassName, System.DateTime Start, System.DateTime Stop, System.DateTime LastUpdate)
    {
        return base.Channel.getEventsByClass(ClassName, Start, Stop, LastUpdate);
    }
    
    public BusinessLayer.EventData[] getPrivateEvents(System.DateTime Start, System.DateTime Stop, System.DateTime LastUpdate)
    {
        return base.Channel.getPrivateEvents(Start, Stop, LastUpdate);
    }
    
    public string[] getCampusNames()
    {
        return base.Channel.getCampusNames();
    }
    
    public string[] getClassesNames()
    {
        return base.Channel.getClassesNames();
    }
    
    public string[] getPromotionsNames()
    {
        return base.Channel.getPromotionsNames();
    }
    
    public string[] getPeriodeNames()
    {
        return base.Channel.getPeriodeNames();
    }
    
    public string addUser(string UserName, string UserPassword, string UserClassName)
    {
        return base.Channel.addUser(UserName, UserPassword, UserClassName);
    }
    
    public string addCampus(string CampusName)
    {
        return base.Channel.addCampus(CampusName);
    }
    
    public string addClasse(string ClassName, string CampusName, string PeriodeName)
    {
        return base.Channel.addClasse(ClassName, CampusName, PeriodeName);
    }
    
    public string addPromotion(string PromotionName)
    {
        return base.Channel.addPromotion(PromotionName);
    }
    
    public string addMatiere(string MatiereName, int MatiereHours)
    {
        return base.Channel.addMatiere(MatiereName, MatiereHours);
    }
    
    public string addPeriode(string PeriodeName, string PromoName, System.DateTime PeriodeStart, System.DateTime PeriodeEnd)
    {
        return base.Channel.addPeriode(PeriodeName, PromoName, PeriodeStart, PeriodeEnd);
    }
    
    public string grantNewRight(string UserName, int Type, string CampusName)
    {
        return base.Channel.grantNewRight(UserName, Type, CampusName);
    }
    
    public string addEvent(string EventName, System.DateTime Start, System.DateTime End, bool Obligatoire, string IntervenatName, string CampusName, string PeriodeName, string MatiereName, string Type, string Lieu)
    {
        return base.Channel.addEvent(EventName, Start, End, Obligatoire, IntervenatName, CampusName, PeriodeName, MatiereName, Type, Lieu);
    }
}

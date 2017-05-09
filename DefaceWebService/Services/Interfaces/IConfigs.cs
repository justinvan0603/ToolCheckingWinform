using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Xml.Linq;

namespace DefaceWebService.Services.Interfaces
{
    [ServiceContract]    
    public interface IConfigs
    {
        [OperationContract]
        IEnumerable<Configtype_ByUserResult> Configtype_ByUser(string user);

        [OperationContract]
        Userconfig_UpdResult Userconfig_Upd(string user, XElement lstConfig);

        [OperationContract]
        Userconfig_UpdResult Userconfig_Upd_A(string user, string lstConfig);
    }
}
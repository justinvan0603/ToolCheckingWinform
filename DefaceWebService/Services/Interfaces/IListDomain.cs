using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace DefaceWebService.Services.Interfaces
{
    [ServiceContract] 
    public interface IListDomain
    {
        [OperationContract]
        IEnumerable<Listdomain_SearchResult> Listdomain_Search(string user, string domain, DateTime? createDate, string recordStatus);

        [OperationContract]
        Listdomain_ByIdResult Listdomain_ById(int? id);

        [OperationContract]
        Listdomain_InsResult Listdomain_Ins(Listdomain_SearchResult data);

        [OperationContract]
        Listdomain_UpdResult Listdomain_Upd(Listdomain_SearchResult data);

        [OperationContract]
        Listdomain_DelResult Listdomain_Del(int? id);
    }
}
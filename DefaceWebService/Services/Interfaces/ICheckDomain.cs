using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace DefaceWebService.Services.Interfaces
{
    [ServiceContract]
    public interface ICheckDomain
    {
        [OperationContract]
        DomainChange_InsResult DomainChange_Ins(DomainChange_SearchResult data);

        [OperationContract]
        IEnumerable<DomainChange_SearchResult> DomainChange_Search(DomainChange_SearchResult data);

        [OperationContract]
        DomainProfile_InsResult DomainProfile_Ins(DomainProfile_SearchResult data);

        [OperationContract]
        IEnumerable<DomainProfile_SearchResult> DomainProfile_Search(DomainProfile_SearchResult data);

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Xml.Linq;

namespace DefaceWebService.Services.Interfaces
{
    [ServiceContract]
    public interface IOptions
    {
        [OperationContract]
        Options_UpdResult Options_Upd(Option_SearchResult data, XElement lstUser, string isEditUser, XElement lstLink, string isEditLink);

        [OperationContract]
        Options_UpdResult Options_Upd_A(Option_SearchResult data, string xmlUser, string isEditUser, string xmlLink, string isEditLink);

        [OperationContract]
        IEnumerable<Option_SearchResult> Option_Search(Option_SearchResult data);

        [OperationContract]
        IEnumerable<Optionlinks_SearchResult> Optionlinks_Search(string Domain);

        [OperationContract]
        IEnumerable<Optionsuser_SearchResult> Optionsuser_Search(string Domain, string User);
    }
}
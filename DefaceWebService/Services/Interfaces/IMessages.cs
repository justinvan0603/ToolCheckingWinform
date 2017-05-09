using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace DefaceWebService.Services.Interfaces
{
    [ServiceContract]
    public interface IMessages
    {
        [OperationContract]
        IEnumerable<Messages_SearchResult> Messages_Search(string user, string domain, string status);

        [OperationContract]
        Messages_ReadResult Messages_Read(int messageId);

        [OperationContract]
        Messages_InsResult Messages_Ins(string title, string content, string user, string domain, string createDate);

        [OperationContract]
        IEnumerable<Messages_NotifyInsResult> Messages_NotifyIns(string title, string content, string user, string domain, string link, string createDate, string type, string keyTerm, string icon);

        [OperationContract]
        Messages_ByIdResult Messages_ById(int messageId);
        [OperationContract]
        Messages_UpdFBIDResult Messages_UpdFBID(int messageId, string firebaseId);
        [OperationContract]
        Messages_ByFbIdResult Messages_ByFbId(string firebaseId);
    }
}
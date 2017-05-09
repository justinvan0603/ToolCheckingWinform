using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Xml.Linq;

namespace DefaceWebService.Services.Interfaces
{
    [ServiceContract]    
    public interface IUser
    {
        [OperationContract]
        Users_InsResult Users_Ins(string USERNAME, string FULLNAME, string PASSWORD, string EMAIL, int? PHONE, string PARENT_ID, string DESCRIPTION, string RECORD_STATUS, string AUTH_STATUS, string CREATE_DT, string APPROVE_DT, string EDIT_DT, string MAKER_ID, string CHECKER_ID, string EDITOR_ID, XElement xmlDOMAIN);

        [OperationContract]
        Users_InsResult Users_Ins_A(string USERNAME, string FULLNAME, string PASSWORD, string EMAIL, int? PHONE, string PARENT_ID, string DESCRIPTION, string RECORD_STATUS, string AUTH_STATUS, string CREATE_DT, string APPROVE_DT, string EDIT_DT, string MAKER_ID, string CHECKER_ID, string EDITOR_ID, string DOMAIN);

        [OperationContract]
        Users_UpdResult Users_Upd(string id, string USERNAME, string FULLNAME, string PASSWORD, string EMAIL, int? PHONE, string PARENT_ID, string DESCRIPTION, string RECORD_STATUS, string AUTH_STATUS, string CREATE_DT, string APPROVE_DT, string EDIT_DT, string MAKER_ID, string CHECKER_ID, string EDITOR_ID, XElement DOMAIN, string isEditDetail);

        [OperationContract]
        Users_UpdResult Users_Upd_A(string id, string USERNAME, string FULLNAME, string PASSWORD, string EMAIL, int? PHONE, string PARENT_ID, string DESCRIPTION, string RECORD_STATUS, string AUTH_STATUS, string CREATE_DT, string APPROVE_DT, string EDIT_DT, string MAKER_ID, string CHECKER_ID, string EDITOR_ID, string xmlDOMAIN, string isEditDetail);

        [OperationContract]
        Users_DelResult Users_Del(string id);

        [OperationContract]
        Users_CheckLoginResult Users_CheckLogin(string USERNAME, string PASSWORD, string APPTOKEN);

        [OperationContract]
        Users_ByIdResult Users_ById(string USERNAME, string USERID);

        [OperationContract]
        IEnumerable<Users_SearchResult> Users_Search(Users_SearchResult data, int top);

        [OperationContract]
        IEnumerable<Userdomain_SearchResult> Userdomain_Search(string user, string domain);

        [OperationContract]
        IEnumerable<Users_ByParentResult> Users_ByParent(string userID, string userName);

    }
}
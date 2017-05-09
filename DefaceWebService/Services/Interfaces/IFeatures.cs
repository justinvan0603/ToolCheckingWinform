using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace DefaceWebService.Services.Interfaces
{
    [ServiceContract]    
    public interface IFeatures
    {
        [OperationContract]
        Features_InsResult Features_Ins(Features_SearchResult data);

        [OperationContract]
        Features_InsResult Features_Ins_A(string FEA_TYPE, string CONTENTS, int? LEVEL, string RESOURCE, string RECORD_STATUS, string AUTH_STATUS,
                         DateTime? APPROVE_DT, DateTime? EDIT_DT, string CHECKER_ID, string EDITOR_ID, DateTime? CREATE_DT, string MAKER_ID);

        [OperationContract]
        Features_UpdResult Features_Upd(Features_SearchResult data);

        [OperationContract]
        Features_UpdResult Features_Upd_A(int? ID, string FEA_TYPE, string CONTENTS, int? LEVEL, string RESOURCE, string RECORD_STATUS, string AUTH_STATUS,
                         DateTime? APPROVE_DT, DateTime? EDIT_DT, string CHECKER_ID, string EDITOR_ID, DateTime? CREATE_DT, string MAKER_ID);

        [OperationContract]
        Features_DelResult Features_Del(int? id);

        [OperationContract]
        Features_ByIdResult Features_ById(int? id);
        
        [OperationContract]
        IEnumerable<Features_SearchResult> Features_Search(Features_SearchResult data);

        [OperationContract]
        IEnumerable<Features_SearchResult> Features_Search_A(string FEA_TYPE, string CONTENTS, int? LEVEL, string RESOURCE, string RECORD_STATUS, string AUTH_STATUS);
    }
}
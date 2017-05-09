using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace DefaceWebService.Services.Interfaces
{
    [ServiceContract] 
    public interface ISchedule
    {
        [OperationContract]
        Schedules_CalResult Schedules_Cal(string date, string username, DateTime? createDate);

        [OperationContract]
        IEnumerable<Schedules_GetByDateResult> Schedules_GetByDate(DateTime date);

        [OperationContract]
        IEnumerable<Schedules_GetLastResult> Schedules_GetLast(DateTime date, int? top);

        [OperationContract]
        IEnumerable<Schedules_DTResult> Schedules_DT(DateTime date, string term);

        [OperationContract]
        IEnumerable<Schedules_SearchResult> Schedules_Search(Schedules_SearchResult data, int? top);

        [OperationContract]
        ScheduleDt_UpdExecuteResult ScheduleDt_UpdExecute(DateTime date, string term, string linkid);

    }
}
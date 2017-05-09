using DefaceWebService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;

namespace DefaceWebService.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DFWService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DFWService.svc or DFWService.svc.cs at the Solution Explorer and start debugging.
    public class DFWService : IMessages, IUser, IListDomain, IConfigs, ICheckDomain, ISchedule, IOptions, IFeatures
    {
        public const string formatDate = "dd/MM/yyyy HH:mm:ss";

        public Messages_ByFbIdResult Messages_ByFbId(string firebaseId)
        {
            try
            { 
                using(var dataContext = new DFWdbDataContext())
                {
                    Messages_ByFbIdResult result = dataContext.Messages_ByFbId(firebaseId).SingleOrDefault();
                    return result;
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public Messages_UpdFBIDResult Messages_UpdFBID(int messageId, string firebaseId)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    Messages_UpdFBIDResult result = dataContext.Messages_UpdFBID(messageId, firebaseId).SingleOrDefault();
                    return result;
                }
            }
            catch(Exception ex)
            {
                return new Messages_UpdFBIDResult() {Result = "-1", ErrorDesc= ex.Message };
            }
        }
        public ScheduleDt_UpdExecuteResult ScheduleDt_UpdExecute(DateTime date, string term, string linkid)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    ScheduleDt_UpdExecuteResult result = dataContext.ScheduleDt_UpdExecute(date, term, linkid).SingleOrDefault();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new ScheduleDt_UpdExecuteResult() { Result = "-1", ErrorDesc = ex.Message };
            }
        }
        public IEnumerable<Messages_SearchResult> Messages_Search(string user, string domain, string status)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    var result = dataContext.Messages_Search(user, domain, status);
                    return result.ToList();
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public Users_InsResult Users_Ins(string USERNAME, string FULLNAME, string PASSWORD, string EMAIL, int? PHONE, string PARENT_ID, string DESCRIPTION, string RECORD_STATUS, string AUTH_STATUS, string CREATE_DT, string APPROVE_DT, string EDIT_DT, string MAKER_ID, string CHECKER_ID, string EDITOR_ID, XElement DOMAIN)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    Users_InsResult result = dataContext.Users_Ins(USERNAME, FULLNAME, PASSWORD, EMAIL, PHONE, PARENT_ID, DESCRIPTION, RECORD_STATUS, AUTH_STATUS, CREATE_DT, APPROVE_DT, EDIT_DT, MAKER_ID, CHECKER_ID, EDITOR_ID, DOMAIN).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception ex)
            {
                return new Users_InsResult() { Result = "-1", ErrorDesc = ex.Message };
            }
        }

        public Users_InsResult Users_Ins_A(string USERNAME, string FULLNAME, string PASSWORD, string EMAIL, int? PHONE, string PARENT_ID, string DESCRIPTION, string RECORD_STATUS, string AUTH_STATUS, string CREATE_DT, string APPROVE_DT, string EDIT_DT, string MAKER_ID, string CHECKER_ID, string EDITOR_ID, string xmlDOMAIN)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    XElement xml = XElement.Parse(xmlDOMAIN);
                    Users_InsResult result = dataContext.Users_Ins(USERNAME, FULLNAME, PASSWORD, EMAIL, PHONE, PARENT_ID, DESCRIPTION, RECORD_STATUS, AUTH_STATUS, CREATE_DT, APPROVE_DT, EDIT_DT, MAKER_ID, CHECKER_ID, EDITOR_ID, xml).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception ex)
            {
                return new Users_InsResult() { Result = "-1", ErrorDesc = ex.Message };
            }
        }

        public Users_UpdResult Users_Upd(string id, string USERNAME, string FULLNAME, string PASSWORD, string EMAIL, int? PHONE, string PARENT_ID, string DESCRIPTION, string RECORD_STATUS, string AUTH_STATUS, string CREATE_DT, string APPROVE_DT, string EDIT_DT, string MAKER_ID, string CHECKER_ID, string EDITOR_ID, XElement DOMAIN, string isEditDetail)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    Users_UpdResult result = dataContext.Users_Upd(id, USERNAME, FULLNAME, PASSWORD, EMAIL, PHONE, PARENT_ID, DESCRIPTION, RECORD_STATUS, AUTH_STATUS, CREATE_DT, APPROVE_DT, EDIT_DT, MAKER_ID, CHECKER_ID, EDITOR_ID, DOMAIN, isEditDetail).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception ex)
            {
                return new Users_UpdResult() { Result = "-1", ErrorDesc = ex.Message };
            }
        }

        public Users_UpdResult Users_Upd_A(string id, string USERNAME, string FULLNAME, string PASSWORD, string EMAIL, int? PHONE, string PARENT_ID, string DESCRIPTION, string RECORD_STATUS, string AUTH_STATUS, string CREATE_DT, string APPROVE_DT, string EDIT_DT, string MAKER_ID, string CHECKER_ID, string EDITOR_ID, string xmlDOMAIN, string isEditDetail)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    XElement xml = XElement.Parse(xmlDOMAIN);
                    Users_UpdResult result = dataContext.Users_Upd(id, USERNAME, FULLNAME, PASSWORD, EMAIL, PHONE, PARENT_ID, DESCRIPTION, RECORD_STATUS, AUTH_STATUS, CREATE_DT, APPROVE_DT, EDIT_DT, MAKER_ID, CHECKER_ID, EDITOR_ID, xml, isEditDetail).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception ex)
            {
                return new Users_UpdResult() { Result = "-1", ErrorDesc = ex.Message };
            }
        }


        public Users_CheckLoginResult Users_CheckLogin(string USERNAME, string PASSWORD, string APPTOKEN)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    Users_CheckLoginResult result = dataContext.Users_CheckLogin(USERNAME, PASSWORD, APPTOKEN).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception ex)
            {
                return new Users_CheckLoginResult() { Result = "-1", ErrorDesc = ex.Message };
            }
        }

        public IEnumerable<Users_ByParentResult> Users_ByParent(string userID, string userName)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    var result = dataContext.Users_ByParent(userID, userName);
                    return result.ToList();
                }
            }
            catch (Exception )
            {
                return null;
            }
        }

        public IEnumerable<Listdomain_SearchResult> Listdomain_Search(string user, string domain, DateTime? createDate, string recordStatus)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    var result = dataContext.Listdomain_Search(user, domain, recordStatus, createDate == null ? "" : createDate.Value.ToString(formatDate));
                    return result.ToList();
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public Messages_ReadResult Messages_Read(int messageId)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    var result = dataContext.Messages_Read(messageId).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception ex)
            {
                return new Messages_ReadResult() { Result = "-1", ErrorDesc = ex.Message };
            }
        }


        public Messages_InsResult Messages_Ins(string title, string content, string user, string domain, string createDate)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    var result = dataContext.Messages_Ins(title, content, user, domain, createDate).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception ex)
            {
                return new Messages_InsResult() { Result = "-1", ErrorDesc = ex.Message };
            }
        }


        public Messages_ByIdResult Messages_ById(int messageId)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    Messages_ByIdResult result = dataContext.Messages_ById(messageId).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public Users_ByIdResult Users_ById(string USERNAME, string USERID)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    var result = dataContext.Users_ById(USERNAME, USERID).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<Configtype_ByUserResult> Configtype_ByUser(string user)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {                    
                    var result = dataContext.Configtype_ByUser(user);
                    return result.ToList();
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public Userconfig_UpdResult Userconfig_Upd(string user, XElement lstConfig)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    var result = dataContext.Userconfig_Upd(user, lstConfig).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception ex)
            {
                return new Userconfig_UpdResult() { Result = "-1", ErrorDesc = ex.Message };
            }
        }

        public Userconfig_UpdResult Userconfig_Upd_A(string user, string lstConfig)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    XElement xml = XElement.Parse(lstConfig);
                    var result = dataContext.Userconfig_Upd(user, xml).SingleOrDefault();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new Userconfig_UpdResult() { Result = "-1", ErrorDesc = ex.Message };
            }
        }

        public DomainChange_InsResult DomainChange_Ins(DomainChange_SearchResult data)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    DomainChange_InsResult result = dataContext.DomainChange_Ins(data.DOMAIN_ID, data.DOMAIN, data.TYPE,
                        data.VALUES, data.CREATE_DT == null ? "" : data.CREATE_DT.Value.ToString(formatDate), data.MAKER_ID).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception ex)
            {
                return new DomainChange_InsResult() { Result = "-1", ErrorDesc = ex.Message };
            }
        }

        public IEnumerable<DomainChange_SearchResult> DomainChange_Search(DomainChange_SearchResult data)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    var result = dataContext.DomainChange_Search(data.DOMAIN_ID, data.DOMAIN, data.TYPE,
                        data.VALUES, data.CREATE_DT == null ? "" : data.CREATE_DT.Value.ToString(formatDate), data.MAKER_ID).ToList();
                    return result;
                }

            }
            catch (Exception)
            {
                return null;
            }
        }


        public DomainProfile_InsResult DomainProfile_Ins(DomainProfile_SearchResult data)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    DomainProfile_InsResult result = dataContext.DomainProfile_Ins(data.DOMAIN_ID, data.DOMAIN, data.TYPE,
                        data.VALUES, data.CREATE_DT == null ? "" : data.CREATE_DT.Value.ToString(formatDate), data.MAKER_ID).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception ex)
            {
                return new DomainProfile_InsResult() { Result = "-1", ErrorDesc = ex.Message };
            }
        }

        public IEnumerable<DomainProfile_SearchResult> DomainProfile_Search(DomainProfile_SearchResult data)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    var result = dataContext.DomainProfile_Search(data.DOMAIN_ID, data.DOMAIN, data.TYPE,
                        data.VALUES, data.CREATE_DT == null ? "" : data.CREATE_DT.Value.ToString(formatDate), data.MAKER_ID).ToList();
                    return result;
                }

            }
            catch (Exception)
            {
                return null;
            }
        }


        public IEnumerable<Messages_NotifyInsResult> Messages_NotifyIns(string title, string content, string user, string domain, string link, string createDate, string type, string keyTerm, string icon)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    var result = dataContext.Messages_NotifyIns(title, content, user, domain, link, createDate, type, keyTerm, icon);
                    return result.ToList();
                }

            }
            catch (Exception)
            {
                return null;
            }
        }


        public IEnumerable<Users_SearchResult> Users_Search(Users_SearchResult data, int top)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    var result = dataContext.Users_Search(data.UserName, data.FULLNAME, data.Email, data.PHONE, top).ToList();
                    return result;
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public Schedules_CalResult Schedules_Cal(string date, string username, DateTime? createDate)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    Schedules_CalResult result = dataContext.Schedules_Cal(date, username, createDate == null ? "" : createDate.Value.ToString(formatDate)).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception ex)
            {
                return new Schedules_CalResult() { Result = "-1", ErrorDesc = ex.Message };
            }
        }

        public IEnumerable<Schedules_GetByDateResult> Schedules_GetByDate(DateTime date)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    var result = dataContext.Schedules_GetByDate(date.ToString(formatDate));
                    return result.ToList();
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Schedules_GetLastResult> Schedules_GetLast(DateTime date, int? top)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    var result = dataContext.Schedules_GetLast(date.ToString(formatDate), top);
                    return result.ToList();
                }

            }
            catch (Exception)
            {
                return null;
            }
        }


        public IEnumerable<Schedules_DTResult> Schedules_DT(DateTime date, string term)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    var result = dataContext.Schedules_DT(date.ToString(formatDate), term);
                    return result.ToList();
                }

            }
            catch (Exception)
            {
                return null;
            }
        }


        public IEnumerable<Schedules_SearchResult> Schedules_Search(Schedules_SearchResult data, int? top)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    var result = dataContext.Schedules_Search(data.SCH_DATE == null ? "" : data.SCH_DATE.Value.ToString(formatDate), data.SCH_TERM,
                        data.CREATE_DT == null ? "" : data.CREATE_DT.Value.ToString(formatDate), data.PROCESS_STATUS, top);
                    return result.ToList();
                }

            }
            catch (Exception)
            {
                return null;
            }
        }


        public Listdomain_ByIdResult Listdomain_ById(int? id)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    Listdomain_ByIdResult result = dataContext.Listdomain_ById(id).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Listdomain_InsResult Listdomain_Ins(Listdomain_SearchResult data)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    Listdomain_InsResult result = dataContext.Listdomain_Ins(data.DOMAIN, data.USER_ID, data.USERNAME, data.DESCRIPTION, data.RECORD_STATUS, data.AUTH_STATUS,
                        data.CREATE_DT == null ? "" : data.CREATE_DT.Value.ToString(formatDate), data.APPROVE_DT == null ? "" : data.APPROVE_DT.Value.ToString(formatDate), data.EDIT_DT == null ? "" : data.EDIT_DT.Value.ToString(formatDate),
                        data.MAKER_ID, data.CHECKER_ID, data.EDITOR_ID).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception ex)
            {
                return new Listdomain_InsResult() { Result = "-1", ErrorDesc = ex.Message };
            }
        }

        public Listdomain_UpdResult Listdomain_Upd(Listdomain_SearchResult data)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    Listdomain_UpdResult result = dataContext.Listdomain_Upd(data.ID, data.DOMAIN, data.USER_ID, data.USERNAME, data.DESCRIPTION, data.RECORD_STATUS, data.AUTH_STATUS,
                        data.CREATE_DT == null ? "" : data.CREATE_DT.Value.ToString(formatDate), data.APPROVE_DT == null ? "" : data.APPROVE_DT.Value.ToString(formatDate), data.EDIT_DT == null ? "" : data.EDIT_DT.Value.ToString(formatDate),
                        data.MAKER_ID, data.CHECKER_ID, data.EDITOR_ID).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception ex)
            {
                return new Listdomain_UpdResult() { Result = "-1", ErrorDesc = ex.Message };
            }
        }

        public Listdomain_DelResult Listdomain_Del(int? id)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    Listdomain_DelResult result = dataContext.Listdomain_Del(id).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception ex)
            {
                return new Listdomain_DelResult() { Result = "-1", ErrorDesc = ex.Message };
            }
        }




        public Users_DelResult Users_Del(string id)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    Users_DelResult result = dataContext.Users_Del(id).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception ex)
            {
                return new Users_DelResult() { Result = "-1", ErrorDesc = ex.Message };
            }
        }


        public IEnumerable<Userdomain_SearchResult> Userdomain_Search(string user, string domain)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    var result = dataContext.Userdomain_Search(user, domain);
                    return result.ToList();
                }

            }
            catch (Exception )
            {
                return null;
            }
        }

        public Options_UpdResult Options_Upd(Option_SearchResult data, XElement lstUser, string isEditUser, XElement lstLink, string isEditLink)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    Options_UpdResult result = dataContext.Options_Upd(data.ID, data.DOMAIN_ID, data.IS_LIMIT, data.TIMES, data.DESCRIPTION,
                        data.RECORD_STATUS, data.AUTH_STATUS, data.CREATE_DT == null ? "" : data.CREATE_DT.Value.ToString(formatDate),
                        data.APPROVE_DT == null ? "" : data.APPROVE_DT.Value.ToString(formatDate), data.EDIT_DT == null ? "" : data.EDIT_DT.Value.ToString(formatDate),
                        data.MAKER_ID, data.CHECKER_ID, data.EDITOR_ID, lstLink, lstUser, isEditUser, isEditLink).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception ex)
            {
                return new Options_UpdResult() { Result = "-1", ErrorDesc = ex.Message };
            }
        }


        public Options_UpdResult Options_Upd_A(Option_SearchResult data, string xmlUser, string isEditUser, string xmlLink, string isEditLink)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    XElement _xmlUser = XElement.Parse(xmlUser);
                    XElement _xmlLink = XElement.Parse(xmlLink);
                    Options_UpdResult result = dataContext.Options_Upd(data.ID, data.DOMAIN_ID, data.IS_LIMIT, data.TIMES, data.DESCRIPTION,
                        data.RECORD_STATUS, data.AUTH_STATUS, data.CREATE_DT == null ? "" : data.CREATE_DT.Value.ToString(formatDate),
                        data.APPROVE_DT == null ? "" : data.APPROVE_DT.Value.ToString(formatDate), data.EDIT_DT == null ? "" : data.EDIT_DT.Value.ToString(formatDate),
                        data.MAKER_ID, data.CHECKER_ID, data.EDITOR_ID, _xmlLink, _xmlUser, isEditUser, isEditLink).SingleOrDefault();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new Options_UpdResult() { Result = "-1", ErrorDesc = ex.Message };
            }
        }

        public IEnumerable<Option_SearchResult> Option_Search(Option_SearchResult data)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    var result = dataContext.Option_Search(data.DOMAIN_ID, data.USERNAME, data.RECORD_STATUS, data.IS_LIMIT, data.TIMES);
                    return result.ToList();
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Optionlinks_SearchResult> Optionlinks_Search(string Domain)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    var result = dataContext.Optionlinks_Search(Domain);
                    return result.ToList();
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Optionsuser_SearchResult> Optionsuser_Search(string Domain, string User)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    var result = dataContext.Optionsuser_Search(User, Domain);
                    return result.ToList();
                }

            }
            catch (Exception )
            {
                return null;
            }
        }


        public Features_InsResult Features_Ins(Features_SearchResult data)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    Features_InsResult result = dataContext.Features_Ins(data.FEA_TYPE, data.CONTENTS, data.LEVEL, data.RESOURCE, data.RECORD_STATUS, data.AUTH_STATUS,
                        data.APPROVE_DT == null ? "" : data.APPROVE_DT.Value.ToString(formatDate), data.EDIT_DT == null ? "" : data.EDIT_DT.Value.ToString(formatDate),
                        data.CHECKER_ID, data.EDITOR_ID, data.CREATE_DT == null ? "" : data.CREATE_DT.Value.ToString(formatDate), data.MAKER_ID).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception ex)
            {
                return new Features_InsResult() { Result = "-1", ErrorDesc = ex.Message };
            }
        }
        public Features_InsResult Features_Ins_A(string FEA_TYPE, string CONTENTS, int? LEVEL, string RESOURCE, string RECORD_STATUS, string AUTH_STATUS, DateTime? APPROVE_DT, DateTime? EDIT_DT, string CHECKER_ID, string EDITOR_ID, DateTime? CREATE_DT, string MAKER_ID)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    Features_InsResult result = dataContext.Features_Ins(FEA_TYPE, CONTENTS, LEVEL, RESOURCE, RECORD_STATUS, AUTH_STATUS,
                        APPROVE_DT == null ? "" : APPROVE_DT.Value.ToString(formatDate), EDIT_DT == null ? "" : EDIT_DT.Value.ToString(formatDate),
                        CHECKER_ID, EDITOR_ID, CREATE_DT == null ? "" : CREATE_DT.Value.ToString(formatDate), MAKER_ID).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception ex)
            {
                return new Features_InsResult() { Result = "-1", ErrorDesc = ex.Message };
            }
        }

        public Features_UpdResult Features_Upd_A(int? ID, string FEA_TYPE, string CONTENTS, int? LEVEL, string RESOURCE, string RECORD_STATUS, string AUTH_STATUS, DateTime? APPROVE_DT, DateTime? EDIT_DT, string CHECKER_ID, string EDITOR_ID, DateTime? CREATE_DT, string MAKER_ID)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    Features_UpdResult result = dataContext.Features_Upd(ID, FEA_TYPE, CONTENTS, LEVEL, RESOURCE, RECORD_STATUS, AUTH_STATUS,
                        APPROVE_DT == null ? "" : APPROVE_DT.Value.ToString(formatDate), EDIT_DT == null ? "" : EDIT_DT.Value.ToString(formatDate),
                        CHECKER_ID, EDITOR_ID, CREATE_DT == null ? "" : CREATE_DT.Value.ToString(formatDate), MAKER_ID).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception ex)
            {
                return new Features_UpdResult() { Result = "-1", ErrorDesc = ex.Message };
            }
        }

        public Features_UpdResult Features_Upd(Features_SearchResult data)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    Features_UpdResult result = dataContext.Features_Upd(data.ID, data.FEA_TYPE, data.CONTENTS, data.LEVEL, data.RESOURCE, data.RECORD_STATUS, data.AUTH_STATUS,
                        data.APPROVE_DT == null ? "" : data.APPROVE_DT.Value.ToString(formatDate), data.EDIT_DT == null ? "" : data.EDIT_DT.Value.ToString(formatDate),
                        data.CHECKER_ID, data.EDITOR_ID, data.CREATE_DT == null ? "" : data.CREATE_DT.Value.ToString(formatDate), data.MAKER_ID).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception ex)
            {
                return new Features_UpdResult() { Result = "-1", ErrorDesc = ex.Message };
            }
        }

        public Features_DelResult Features_Del(int? id)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    Features_DelResult result = dataContext.Features_Del(id).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception ex)
            {
                return new Features_DelResult() { Result = "-1", ErrorDesc = ex.Message };
            }
        }

        public Features_ByIdResult Features_ById(int? id)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    Features_ByIdResult result = dataContext.Features_ById(id).SingleOrDefault();
                    return result;
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Features_SearchResult> Features_Search(Features_SearchResult data)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    var result = dataContext.Features_Search(data.FEA_TYPE, data.CONTENTS, data.LEVEL, data.RESOURCE, data.RECORD_STATUS, data.AUTH_STATUS);
                    return result.ToList();
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Features_SearchResult> Features_Search_A(string FEA_TYPE, string CONTENTS, int? LEVEL, string RESOURCE, string RECORD_STATUS, string AUTH_STATUS)
        {
            try
            {
                using (var dataContext = new DFWdbDataContext())
                {
                    var result = dataContext.Features_Search(FEA_TYPE, CONTENTS, LEVEL, RESOURCE, RECORD_STATUS, AUTH_STATUS);
                    return result.ToList();
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}

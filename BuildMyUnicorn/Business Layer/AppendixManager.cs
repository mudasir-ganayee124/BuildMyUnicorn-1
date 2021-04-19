using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuildMyUnicorn.Business_Layer
{
    public class AppendixManager
    {
        private readonly string SaveQuery = "INSERT into tbl_appdendixsearchlog ({0}) VALUES ({1})";
        public List<Appendix> SearchText(string searchWord)
        {
            var query = $@"SELECT Keyword,Category,Definition  FROM dbo.tbl_appendix
                            WHERE CONTAINS(Category,'""{searchWord}""') or CONTAINS(Keyword,'""{searchWord}""') ";
            var appendixlist = SharedManager.GetList<Appendix>(query).ToList();
            if (appendixlist.Count() == 0)
            {
                Save(searchWord, false);
            }
            else
            {
                Save(searchWord);

            }

            return appendixlist;
        }

        public void Save(string searchWord, bool IsFound = true)
        {
            AppendixSearchLog log = new AppendixSearchLog();
            log.AppdendixLogID = Guid.NewGuid();
            log.Keyword = searchWord;
            log.IsFound = IsFound;
            log.QueryDate = DateTime.Now.Date;
            log.AppendixType = AppendixType.Keyword;
            log.IsDeleted = false;
            log.ClientID = Guid.Parse(HttpContext.Current.User.Identity.Name);
            var AppendixSearchLog = getAppendixSearchLog();
            if (AppendixSearchLog.Where(x => x.Keyword == searchWord).Count() == 0)
                SharedManager.Save(log, SaveQuery);
        }
        public List<AppendixSearchLog> getAppendixSearchLog()
        {
            var query = $@"SELECT * from tbl_appdendixsearchlog where IsDeleted=0 ";
            return SharedManager.GetList<AppendixSearchLog>(query).ToList();
        }
        public void SaveAppendixSearchLog(AppendixSearchLog model)
        {
            model.AppdendixLogID = Guid.NewGuid();
            model.QueryDate = DateTime.Now;
            SharedManager.Save(model, SaveQuery);
        }
        public List<AppendixSearchLog> getAppendixSearchWord()
        {
            var query = $@"select * from tbl_appdendixsearchlog where AppendixType=1 and IsDeleted=0 ";
            return SharedManager.GetList<AppendixSearchLog>(query).ToList();
        }
        public List<AppendixSearchLog> getAppendixSearchMessage()
        {
            var query = $@"select * from tbl_appdendixsearchlog where AppendixType=2 and and IsDeleted=0 ";
            return SharedManager.GetList<AppendixSearchLog>(query).ToList();
        }
    }
}
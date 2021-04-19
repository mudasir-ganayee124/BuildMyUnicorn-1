using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Administration.Business_Layer
{
    public class DashboardManager
    {
        private readonly string SaveQuery = "INSERT into tbl_appdendixsearchlog ({0}) VALUES ({1})";

        public List<Country> GetCountryCustomerList()
        {
            var query = $@"SELECT COUNT(*) as Total, tbl_countries.CountryName, latitude, longitude FROM tbl_client INNER JOIN tbl_countries ON tbl_client.CountryID = tbl_countries.CountryID
                           GROUP BY tbl_countries.CountryName, latitude, longitude";
            var _CountryCustomerlist = SharedManager.GetList<Country>(query).ToList();
            return _CountryCustomerlist;
        }
        public List<Country> GetCountrySupplierList()
        {
            var query = $@"SELECT COUNT(*) as Total, tbl_countries.CountryName, latitude, longitude FROM tbl_supplier INNER JOIN tbl_countries ON tbl_supplier.CountryID = tbl_countries.CountryID
                             GROUP BY tbl_countries.CountryName, latitude, longitude";
            var _CountrySupplierlist = SharedManager.GetList<Country>(query).ToList();
            return _CountrySupplierlist;
        }

        public void Save(string searchWord, bool IsFound = true)
        {
            AppendixSearchLog log = new AppendixSearchLog();
            log.AppdendixLogID = Guid.NewGuid();
            log.Keyword = searchWord;
            log.IsFound = IsFound;
            log.QueryDate = DateTime.Now.Date;
            log.AppendixType = AppendixType.Keyword;
            var AppendixSearchLog = getAppendixSearchLog();
            if (AppendixSearchLog.Where(x => x.Keyword == searchWord).Count() == 0)
                SharedManager.Save(log, SaveQuery);
        }
        public List<AppendixSearchLog> getAppendixSearchLog()
        {
            var query = $@"SELECT * from tbl_appdendixsearchlog ";
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
            var query = $@"select * from tbl_appdendixsearchlog where AppendixType=1 ";
            return SharedManager.GetList<AppendixSearchLog>(query).ToList();
        }
        public List<AppendixSearchLog> getAppendixSearchMessage()
        {
            var query = $@"select * from tbl_appdendixsearchlog where AppendixType=2 ";
            return SharedManager.GetList<AppendixSearchLog>(query).ToList();
        }
    }
}
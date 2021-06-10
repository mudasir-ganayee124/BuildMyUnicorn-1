using Business_Model.Helper;
using Business_Model.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace Administration.Business_Layer
{
    public static class SharedManager
    {
        public static T GetSingle<T>(string sqlQuery) where T : class, new()
        {
            T obj = new T();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString))
            {
                connection.Open();

                obj = connection.QuerySingleOrDefault<T>(sqlQuery);
            }
            if (obj != null)
            {
                var property = obj.GetType().GetProperty(nameof(BaseObject.EntityState));
                if (property != null && property.PropertyType.IsEnum)
                    property.SetValue(obj, EntityState.Old);
            }

            return obj;
        }


        public static IEnumerable<T> GetList<T>(string sqlQuery, Action<T> action = null)
        {
            var list = new List<T>();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString))
            {
                connection.Open();

                list = connection.Query<T>(sqlQuery).ToList();

                if (list != null)
                {
                    foreach (var item in list)
                    {
                        var property = item.GetType().GetProperty(nameof(BaseObject.EntityState));
                        if (property != null && property.PropertyType.IsEnum)
                            property.SetValue(item, EntityState.Old);

                        if (action != null)
                            action.Invoke(item);
                    }
                }
            }

            return list;
        }


        public static ResponseModel Save<TModel>(TModel model, string query)
        {
            return Execute(model, query);
        }


        private static ResponseModel Execute<TModel>(TModel model, string query, bool isUpdate = false)
        {
            var responseModel = new ResponseModel();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString))
            {
                connection.Open();
                StringBuilder queryParameters = Setparameters(model, isUpdate);

                if (isUpdate)
                    query = string.Format(query, queryParameters, isUpdate);
                else
                    query = string.Format(query,
                            queryParameters.ToString().Replace("@", string.Empty), queryParameters);

                try
                {
                    responseModel.RecordsAffected = connection.Execute(query, model);
                }
                catch (Exception ex)
                {
                    responseModel.HasError = true;
                    responseModel.Error = ex.Message;
                }
            }
            return responseModel;
        }


        public static ResponseModel Update<TModel>(TModel model, string query)
        {
            return Execute(model, query, true);
        }


        public static int Delete(string query, object param)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString))
            {
                connection.Open();
                return connection.Execute(query, param);
            }
        }

        public static int ExecuteRaw(string query, object param = null)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString))
            {
                connection.Open();
                return connection.Execute(query, param);
            }
        }


        public static T ExecuteScalar<T>(string query, object param = null)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString))
            {
                connection.Open();
                return connection.ExecuteScalar<T>(query, param);
            }
        }


        public static IEnumerable<TReturn> GetListWithNavigation<TFirst, TSecond, TReturn>(string query, Func<TFirst, TSecond, TReturn> map, object param = null, string splitOn = null)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionBuildMyUnicorn"].ConnectionString))
            {
                connection.Open();
                return connection.Query(query, map, param, splitOn: splitOn);
            }
        }


        private static StringBuilder Setparameters<TModel>(TModel model, bool isUpdate = false)
        {
            var queryParameters = new StringBuilder();
            var props = model.GetType().GetProperties();
            foreach (var item in props)
            {
                if (item.IsDefined(typeof(IgnoreInsert)) || (item.IsDefined(typeof(IgnoreUpdate)) && isUpdate))
                    continue;

                if (isUpdate)
                    queryParameters.Append($", {item.Name} = @{item.Name}");
                else
                    queryParameters.Append($", @{item.Name}");
            };

            if (queryParameters.Length != 0)
                queryParameters = queryParameters.Remove(0, 1);
            return queryParameters;
        }


        public static void SetBasicProperties(this IBaseObject model)
        {
            if (model.EntityState == EntityState.New)
            {
                model.CreatedDateTime = DateTime.UtcNow;
                model.IsActive = true;
                model.CreatedBy = Guid.Parse(HttpContext.Current.User.Identity.Name);
            }
            else
            {
                model.ModifiedDateTime = DateTime.UtcNow;
                model.ModifiedBy = Guid.Parse(HttpContext.Current.User.Identity.Name);
            }
        }

    }
}
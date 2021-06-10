using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;


namespace Business_Model.Model
{
   public static class ContentFile
    {
        public static string SetHash(string rootRelativePath)
        {
            try
            {
                if (HttpRuntime.Cache[rootRelativePath] == null)
                {

                    var virtualDirectory = HttpRuntime.AppDomainAppVirtualPath;

                    if (rootRelativePath.StartsWith("~"))
                    {
                        rootRelativePath = virtualDirectory + rootRelativePath.Substring(1);
                    }
                    else
                    {
                        rootRelativePath += "~";
                        rootRelativePath = virtualDirectory + rootRelativePath.Substring(1);
                    }


                    //// Only application relative URLs (~/url) are allowed
                    // Bundle bundles = new Bundle("~/Scripts/CoreBundle");
                    // Bundle bundles = new Bundle("~/Scripts/CoreBundle", new JsMinify());
                    // bundles.Include(rootRelativePath);


                    var absolutePath = HostingEnvironment.MapPath(rootRelativePath);
                    if (absolutePath != null)
                    {
                        var lastChangedDateTime = File.GetLastWriteTime(absolutePath);
                        var versionedUrl = rootRelativePath + "?v=" + lastChangedDateTime.Ticks;

                        HttpRuntime.Cache.Insert(rootRelativePath, versionedUrl, new CacheDependency(absolutePath));
                        return HttpRuntime.Cache[rootRelativePath] as string; //Return path with hashcode appended
                    }
                }
                return rootRelativePath; //Return path with-out hashcode

            }
            catch (Exception) { return rootRelativePath; }
        }
    }
}

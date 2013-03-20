using System;
using System.Collections.Specialized;
using System.Web.Routing;

namespace CI3540.UI.Utils
{
    public static class NameValueCollectionExtensions
    {
        /// <summary>
        /// From StackOverflow
        /// http://stackoverflow.com/questions/5818065/how-to-pass-request-querystring-to-url-action
        /// </summary>
        /// <param name="col"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static RouteValueDictionary ToRouteValues(this NameValueCollection col, Object obj)
        {
            var values = obj == null ? new RouteValueDictionary() : new RouteValueDictionary(obj);

            if (col != null)
            {
                foreach (string key in col)
                {
                    //values passed in object override those already in collection
                    if (!values.ContainsKey(key)) 
                        values[key] = col[key];
                }
            }

            return values;
        }
    }
}
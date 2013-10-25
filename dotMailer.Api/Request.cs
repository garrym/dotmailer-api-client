using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dotMailer.Api
{
    internal class Request
    {
        private readonly string url;
        private readonly IDictionary<string, object> parameters = new Dictionary<string, object>();

        public Request(string url)
        {
            this.url = url;
        }

        public Request(string url, IDictionary<string, object> parameters)
            : this(url)
        {
            this.parameters = parameters;
        }

        public string Url
        {
            get
            {
                if (!parameters.Any())
                    return url;

                // Check if we've got a querystring, if we have and no parameters are provided then we need to strip the querystring params
                var uri = new Uri(Client.BaseAddress + url);
                var absolutePath = HttpUtility.UrlDecode(uri.AbsolutePath);
                var queryString = HttpUtility.ParseQueryString(uri.Query);

                foreach (var parameter in parameters)
                {
                    var isQueryStringParameter = queryString.AllKeys.Any(x => x.Equals(parameter.Key, StringComparison.OrdinalIgnoreCase));

                    if (isQueryStringParameter)
                    {
                        // If parameter doesn't have a value remove the querystring property
                        if (parameter.Value == null)
                            queryString.Remove(parameter.Key);
                        else
                            queryString[parameter.Key] = parameter.Value.ToString();
                    }
                    else
                    {
                        // Path parameter
                        absolutePath = absolutePath.Replace("{" + parameter.Key + "}", parameter.Value.ToString());
                    }
                }

                return string.IsNullOrEmpty(queryString.ToString()) ? absolutePath : string.Concat(absolutePath, "?", queryString);
            }
        }
    }
}

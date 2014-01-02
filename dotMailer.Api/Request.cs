using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dotMailer.Api
{
    internal class RequestParameter
    {
        public string Key { get; set; }
        public object Value { get; set; }
    }

    internal class Request
    {
        private readonly string baseAddress;
        private readonly string url;
        private readonly IDictionary<string, object> parameters = new Dictionary<string, object>();

        public Request(string baseAddress, string url)
        {
            this.baseAddress = baseAddress;
            this.url = url;
        }

        public Request(string baseAddress, string url, IDictionary<string, object> parameters)
            : this(baseAddress, url)
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
                var uri = new Uri(baseAddress + url);
                var absolutePath = HttpUtility.UrlDecode(uri.AbsolutePath);
                var queryString = HttpUtility.ParseQueryString(uri.Query);

                foreach (var parameter in parameters)
                {
                    var value = FormatParameterValue(parameter.Value);

                    var isQueryStringParameter = queryString.AllKeys.Any(x => x.Equals(parameter.Key, StringComparison.OrdinalIgnoreCase));

                    if (isQueryStringParameter)
                    {
                        // If parameter doesn't have a value remove the querystring property
                        if (string.IsNullOrEmpty(value))
                            queryString.Remove(parameter.Key);
                        else
                            queryString[parameter.Key] = value;
                    }
                    else
                    {
                        absolutePath = absolutePath.Replace("{" + parameter.Key + "}", value);
                    }
                }

                var returnUrl = string.IsNullOrEmpty(queryString.ToString()) ? absolutePath : string.Concat(absolutePath, "?", queryString);
                return returnUrl;
            }
        }

        private string FormatParameterValue(object value)
        {
            if (value == null)
                return null;

            if (value is DateTime)
                return ((DateTime)value).ToString("yyyy-MM-dd");
            return value.ToString();
        }
    }
}

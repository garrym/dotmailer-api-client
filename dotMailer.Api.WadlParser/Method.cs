using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotMailer.Api.WadlParser
{
    public class Method
    {
        public HttpMethod HttpMethod
        { get; set; }

        public string Path
        { get; set; }

        public string Name
        { get; set; }

        public string Id
        { get; set; }

        public string Description
        { get; set; }

        public readonly IList<Response> Responses = new List<Response>();

        public readonly IList<Parameter> Parameters = new List<Parameter>();

        public override string ToString()
        {
            var psb = new StringBuilder();
            foreach (var parameter in Parameters.OrderByDescending(x => x.Required))
            {
                psb.Append(parameter);
            }

            var sb = new StringBuilder();

            // Trim remaining comma-space
            var parameters = psb.ToString();
            if (!string.IsNullOrEmpty(parameters))
                parameters = parameters.Substring(0, parameters.Length - 2);

            if (!string.IsNullOrEmpty(Description))
            {
                sb.AppendLine("\t\t/// <summary>");
                sb.AppendLineFormat("\t\t/// {0}", Description);
                sb.AppendLine("\t\t/// </summary>");
            }
            sb.AppendLineFormat("\t\tpublic {0} {1}({2})", GetServiceResult(), Name, parameters);
            sb.AppendLine("\t\t{");

            sb.AppendLine(MethodCode());

            sb.AppendLine("\t\t}");
            return sb.ToString();
        }

        private string GetServiceResult()
        {
            var returnType = GetReturnType();
            return string.IsNullOrEmpty(returnType) ? "ServiceResult" : string.Format("ServiceResult<{0}>", returnType);
        }

        private string GetReturnType()
        {
            var response = Responses.SingleOrDefault(x => x.ReturnType != null);
            return response == null ? string.Empty : response.ReturnType;
        }

        private string MethodCode()
        {
            var returnType = GetReturnType();

            var code = string.Empty;

            var primitiveParameters = Parameters.Where(x =>
                x.DataType.Equals("string", StringComparison.OrdinalIgnoreCase)
                ||
                x.DataType.Equals("int", StringComparison.OrdinalIgnoreCase)
                ||
                x.DataType.Equals("bool", StringComparison.OrdinalIgnoreCase)
                ||
                x.DataType.Equals("guid", StringComparison.OrdinalIgnoreCase)
                ||
                x.DataType.Equals("datetime", StringComparison.OrdinalIgnoreCase)
            ).ToList();
            var complexParameters = Parameters.Where(x => !primitiveParameters.Contains(x)).ToList();
            if (primitiveParameters.Any())
            {
                code = string.Format("\t\t\tvar request = new Request(\"{0}\", ", Path) + Environment.NewLine;
                code += "\t\t\tnew Dictionary<string, object>" + Environment.NewLine;
                code += "\t\t\t{" + Environment.NewLine;

                foreach (var parameter in primitiveParameters)
                {
                    code += string.Format("\t\t\t\t{{ \"{0}\", {0} }}{1}", parameter.Name, parameter == Parameters.Last() ? "" : ",") + Environment.NewLine;
                }
                ;
                code += "\t\t\t});" + Environment.NewLine;
            }
            else
            {
                code = string.Format("\t\t\tvar request = new Request(\"{0}\");", Path) + Environment.NewLine;                
            }

            if (HttpMethod == HttpMethod.Post)
            {
                if (complexParameters.Any())
                {
                    var complexParameter = complexParameters.First();
                    if (string.IsNullOrEmpty(returnType))
                    {
                        code += string.Format("\t\t\treturn Post(request, {0});", complexParameter.Name);
                    }
                    else
                    {
                        if (complexParameter.DataType.Equals(returnType))
                            code += string.Format("\t\t\treturn Post<{0}>(request, {1});", returnType, complexParameter.Name);
                        else
                            code += string.Format("\t\t\treturn Post<{0}, {1}>(request, {2});", returnType, complexParameter.DataType, complexParameter.Name);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(returnType))
                    {
                        code += string.Format("\t\t\treturn Post(request);");
                    }
                    else
                    {
                        code += string.Format("\t\t\treturn Post<{0}>(request);", returnType);
                    }
                }
            }

            if (HttpMethod == HttpMethod.Get)
            {
                if (string.IsNullOrEmpty(returnType))
                {
                    code += string.Format("\t\t\treturn Get(request);");
                }
                else
                {
                    code += string.Format("\t\t\treturn Get<{0}>(request);", returnType);
                }
            }

            if (HttpMethod == HttpMethod.Delete)
            {
                if (string.IsNullOrEmpty(returnType))
                {
                    code += string.Format("\t\t\treturn Delete(request);");
                }
                else
                {
                    code += string.Format("\t\t\treturn Delete<{0}>(request);", returnType);
                }
            }

            if (HttpMethod == HttpMethod.Put)
            {
                if (complexParameters.Any())
                {
                    var complexParameter = complexParameters.First();
                    if (string.IsNullOrEmpty(returnType))
                    {
                        code += string.Format("\t\t\treturn Put(request, {0});", complexParameter.Name);
                    }
                    else
                    {
                        if (complexParameter.DataType.Equals(returnType))
                            code += string.Format("\t\t\treturn Put<{0}>(request, {1});", returnType, complexParameter.Name);
                        else
                            code += string.Format("\t\t\treturn Put<{0}, {1}>(request, {2});", returnType, complexParameter.DataType, complexParameter.Name);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(returnType))
                    {
                        code += string.Format("\t\t\treturn Put(request);");
                    }
                    else
                    {
                        code += string.Format("\t\t\treturn Put<{0}>(request);", returnType);
                    }
                }
            }

            return code;
        }
    }
}
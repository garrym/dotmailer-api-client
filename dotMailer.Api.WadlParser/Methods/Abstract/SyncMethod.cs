using System.Linq;
using System.Text;

namespace dotMailer.Api.WadlParser.Methods.Abstract
{
    public abstract class SyncMethod : Method
    {
        protected override void AppendMethod()
        {
            var serviceResult = string.IsNullOrEmpty(ReturnType) ? "ServiceResult" : string.Format("ServiceResult<{0}>", ReturnType);
            var parameters = RenderParameters();
            AddLine(2, "public {0} {1}({2})", serviceResult, Name, parameters);
            AddLine(2, "{");

            AppendMethodBody();

            AddLine(2, "}");
        }

        private void AppendMethodBody()
        {
            var asyncMethodName = string.Concat(Name, "Async");

            if (Parameters.Any())
            {
                var parameters = GetParameters();
                AddLine(3, "return {0}({1}).Result;", asyncMethodName, parameters);
            }
            else
            {
                AddLine(3, "return {0}().Result;", asyncMethodName);
            }
        }

        private string GetParameters()
        {
            var psb = new StringBuilder();
            foreach (var parameter in Parameters.OrderByDescending(x => x.Required))
                psb.Append(parameter.Name + ", ");

            var parameters = psb.ToString();
            if (!string.IsNullOrEmpty(parameters))
                parameters = parameters.Substring(0, parameters.Length - 2);

            return parameters;
        }
    }
}
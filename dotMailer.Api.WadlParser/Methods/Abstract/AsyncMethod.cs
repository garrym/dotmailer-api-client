using System.Linq;

namespace dotMailer.Api.WadlParser.Methods.Abstract
{
    public abstract class AsyncMethod : Method
    {
        protected override void AppendMethod()
        {
            var asyncMethodName = string.Concat(Name, "Async");

            var serviceResult = string.IsNullOrEmpty(ReturnType) ? "ServiceResult" : string.Format("ServiceResult<{0}>", ReturnType);
            var parameters = RenderParameters();
            AddLine(2, "public async Task<{0}> {1}({2})", serviceResult, asyncMethodName, parameters);
            AddLine(2, "{");

            AppendMethodBody();

            AddLine(2, "}");
        }

        private void AppendMethodBody()
        {
            if (PrimitiveParameters.Any())
            {
                AddLine(3, "var request = new Request(\"{0}\", ", Path);
                AddLine(3, "new Dictionary<string, object>");
                AddLine(3, "{");

                foreach (var parameter in PrimitiveParameters)
                    AddLine(4, "{{ \"{0}\", {0} }}{1}", parameter.Name, parameter == Parameters.Last() ? "" : ",");

                AddLine(3, "});");
            }
            else
            {
                AddLine(3, "var request = new Request(\"{0}\");", Path);
            }

            AppendMethodRequest();
        }

        protected abstract void AppendMethodRequest();
    }
}
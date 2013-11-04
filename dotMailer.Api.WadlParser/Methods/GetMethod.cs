using dotMailer.Api.WadlParser.Methods.Abstract;

namespace dotMailer.Api.WadlParser.Methods
{
    public class GetMethod : Method
    {
        protected override void AppendMethodRequest()
        {
            if (string.IsNullOrEmpty(ReturnType))
                AddLine(3, "return Get(request);");
            else
                AddLine(3, "return Get<{0}>(request);", ReturnType);
        }
    }
}
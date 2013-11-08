using dotMailer.Api.WadlParser.Methods.Abstract;

namespace dotMailer.Api.WadlParser.Methods
{
    public class GetAsyncMethod : AsyncMethod
    {
        protected override void AppendMethodRequest()
        {
            if (string.IsNullOrEmpty(ReturnType))
                AddLine(3, "return await GetAsync(request);");
            else
                AddLine(3, "return await GetAsync<{0}>(request);", ReturnType);
        }
    }
}
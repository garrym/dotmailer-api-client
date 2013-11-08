using dotMailer.Api.WadlParser.Methods.Abstract;

namespace dotMailer.Api.WadlParser.Methods
{
    public class DeleteAsyncMethod : AsyncMethod
    {
        protected override void AppendMethodRequest()
        {
            if (string.IsNullOrEmpty(ReturnType))
                AddLine(3, "return await DeleteAsync(request);");
            else
                AddLine(3, "return await DeleteAsync<{0}>(request);", ReturnType);
        }
    }
}
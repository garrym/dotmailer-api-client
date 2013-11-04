namespace dotMailer.Api.WadlParser
{
    public class DeleteMethod : Method
    {
        protected override void AppendMethodRequest()
        {
            if (string.IsNullOrEmpty(ReturnType))
                AddLine(3, "return Delete(request);");
            else
                AddLine(3, "return Delete<{0}>(request);", ReturnType);
        }
    }
}
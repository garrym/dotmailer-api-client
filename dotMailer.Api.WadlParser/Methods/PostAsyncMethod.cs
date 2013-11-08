using System.Linq;
using dotMailer.Api.WadlParser.Methods.Abstract;

namespace dotMailer.Api.WadlParser.Methods
{
    public class PostAsyncMethod : AsyncMethod
    {
        protected override void AppendMethodRequest()
        {
            if (ComplexParameters.Any())
            {
                var complexParameter = ComplexParameters.First();
                if (!string.IsNullOrEmpty(ReturnType))
                {
                    if (complexParameter.DataType.Equals(ReturnType))
                        AddLine(3, "return await PostAsync<{0}>(request, {1});", ReturnType, complexParameter.Name);
                    else
                        AddLine(3, "return await PostAsync<{0}, {1}>(request, {2});", ReturnType, complexParameter.DataType, complexParameter.Name);
                }
                else
                    AddLine(3, "return await PostAsync(request, {0});", complexParameter.Name);
            }
            else
            {
                if (string.IsNullOrEmpty(ReturnType))
                    AddLine(3, "return await PostAsync(request);");
                else
                    AddLine(3, "return await PostAsync<{0}>(request);", ReturnType);
            }
        }
    }
}
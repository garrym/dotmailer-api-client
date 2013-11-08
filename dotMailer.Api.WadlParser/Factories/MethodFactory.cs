using System.Linq;
using System.Xml.Linq;
using dotMailer.Api.WadlParser.Factories.Abstract;
using dotMailer.Api.WadlParser.Methods.Abstract;

namespace dotMailer.Api.WadlParser.Factories
{
    public abstract class MethodFactory : IMethodFactory
    {
        private readonly IParameterFactory parameterFactory = new ParameterFactory();
        private readonly IResponseFactory responseFactory = new ResponseFactory();

        public Method Build(XElement element)
        {
            var method = GetMethod(element);
            method.Name = element.Attribute("id").Value;
            method.Path = element.Parent.Attribute("path").Value;
            method.Id = element.Parent.Attribute("id").Value;

            var documentationNode = element.Elements().First(x => x.Name.LocalName.Equals("doc"));
            method.Description = documentationNode.Value;

            var requestNode = element.Elements().First(x => x.Name.LocalName.Equals("request"));
            foreach (var parameterNode in requestNode.Elements())
            {
                var parameter = parameterFactory.Build(parameterNode);
                method.Parameters.Add(parameter);
            }

            var responseNodes = element.Elements().Where(x => x.Name.LocalName.Equals("response"));
            foreach (var responseNode in responseNodes)
            {
                var response = responseFactory.Build(responseNode);
                method.Responses.Add(response);
            }
            return method;
        }

        protected abstract Method GetMethod(XElement element);
    }
}

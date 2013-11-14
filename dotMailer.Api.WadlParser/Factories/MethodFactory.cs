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
            var parameterNodes = requestNode.Elements();
            var responseNodes = element.Elements().Where(x => x.Name.LocalName.Equals("response"));

            foreach (var parameter in parameterNodes.Where(x => x.Name.LocalName.Equals("param") || (x.Name.LocalName.Equals("representation") && x.Attribute("mediaType").Value.Equals("application/json"))).Select(node => parameterFactory.Build(node)))
                method.Parameters.Add(parameter);

            foreach (var response in responseNodes.Select(node => responseFactory.Build(node)))
                method.Responses.Add(response);

            return method;
        }

        protected abstract Method GetMethod(XElement element);
    }
}

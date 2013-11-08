using System;
using System.Linq;
using System.Xml.Linq;
using dotMailer.Api.WadlParser.Factories.Abstract;
using dotMailer.Api.WadlParser.Methods;
using dotMailer.Api.WadlParser.Methods.Abstract;

namespace dotMailer.Api.WadlParser.Factories
{
    public class MethodFactory : IMethodFactory
    {
        private readonly IParameterFactory parameterFactory = new ParameterFactory();
        private readonly IResponseFactory responseFactory = new ResponseFactory();

        public Method Build(XElement element)
        {
            var path = element.Parent.Attribute("path").Value;
            var id = element.Parent.Attribute("id").Value;
            var method = GetMethod(element);
            method.Path = path;
            method.Id = id;

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

        private Method GetMethod(XElement element)
        {
            Method method;
            var httpMethod = element.Attribute("name").Value.ToLower();
            switch (httpMethod)
            {
                case "put":
                    method = new PutMethod();
                    break;
                case "get":
                    method = new GetMethod();
                    break;
                case "delete":
                    method = new DeleteMethod();
                    break;
                case "post":
                    method = new PostMethod();
                    break;
                default:
                    throw new Exception("Unknown method type");

            }
            method.Name = element.Attribute("id").Value;
            return method;
        }
    }
}

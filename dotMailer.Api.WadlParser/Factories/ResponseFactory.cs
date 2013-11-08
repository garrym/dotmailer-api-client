using System;
using System.Linq;
using System.Xml.Linq;
using dotMailer.Api.WadlParser.Factories.Abstract;

namespace dotMailer.Api.WadlParser.Factories
{
    public class ResponseFactory : IResponseFactory
    {
        public Response Build(XElement element)
        {
            var response = new Response();
            var statusCode = int.Parse(element.Attribute("status").Value);
            response.StatusCode = statusCode;
            var docNode = element.Elements().FirstOrDefault(x => x.Name.LocalName.Equals("doc"));
            if (docNode != null)
                response.Message = docNode.Value;

            var representationNodes = element.Elements().Where(x => x.Name.LocalName.Equals("representation", StringComparison.OrdinalIgnoreCase)).ToList();
            if (representationNodes.Any())
            {
                var typeAttribute = representationNodes.First().Attribute("element");
                if (typeAttribute != null)
                    response.ReturnType = Helpers.FormatClrType(typeAttribute.Value);
            }

            return response;
        }
    }
}
using System;
using System.Xml.Linq;
using dotMailer.Api.WadlParser.Methods;
using dotMailer.Api.WadlParser.Methods.Abstract;

namespace dotMailer.Api.WadlParser.Factories
{
    public class SyncMethodFactory : MethodFactory
    {
        protected override Method GetMethod(XElement element)
        {
            var httpMethod = element.Attribute("name").Value.ToLower();
            switch (httpMethod)
            {
                case "put":
                    return new PutMethod();
                case "get":
                    return new GetMethod();
                case "delete":
                    return new DeleteMethod();
                case "post":
                    return new PostMethod();
            }
            throw new Exception("Unknown method");
        }
    }
}
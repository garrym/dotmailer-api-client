using System;
using System.Xml.Linq;
using dotMailer.Api.WadlParser.Methods;
using dotMailer.Api.WadlParser.Methods.Abstract;

namespace dotMailer.Api.WadlParser.Factories
{
    public class AsyncMethodFactory : MethodFactory
    {
        protected  override Method GetMethod(XElement element)
        {
            var httpMethod = element.Attribute("name").Value.ToLower();
            switch (httpMethod)
            {
                case "put":
                    return new PutAsyncMethod();
                case "get":
                    return new GetAsyncMethod();
                case "delete":
                    return new DeleteAsyncMethod();
                case "post":
                    return new PostAsyncMethod();
            }
            throw new Exception("Unknown method");
        }
    }
}
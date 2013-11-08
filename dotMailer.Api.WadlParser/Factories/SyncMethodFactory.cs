using System.Xml.Linq;
using dotMailer.Api.WadlParser.Methods.Abstract;

namespace dotMailer.Api.WadlParser.Factories
{
    public class SyncMethodFactory : MethodFactory
    {
        protected override Method GetMethod(XElement element)
        {
            return new SyncMethod();
        }
    }
}
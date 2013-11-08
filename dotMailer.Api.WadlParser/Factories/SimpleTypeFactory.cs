using System.Linq;
using System.Xml.Linq;
using dotMailer.Api.WadlParser.Factories.Abstract;
using dotMailer.Api.WadlParser.Types;

namespace dotMailer.Api.WadlParser.Factories
{
    public class SimpleTypeFactory : ISimpleTypeFactory
    {
        public SimpleType Build(XElement element)
        {
            var simpleType = new SimpleType();
            foreach (var value in element.Elements().Where(x => x.Name.LocalName.Equals("restriction")).Elements().Select(x => x.Attribute("value").Value))
            {
                simpleType.Values.Add(value);
            }
            return simpleType;
        }
    }
}

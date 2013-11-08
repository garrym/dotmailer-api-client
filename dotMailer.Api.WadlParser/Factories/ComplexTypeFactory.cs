using System.Linq;
using System.Xml.Linq;
using dotMailer.Api.WadlParser.Types;

namespace dotMailer.Api.WadlParser.Factories
{
    public class ComplexTypeFactory : IComplexTypeFactory
    {
        public ComplexType Build(XElement element)
        {
            var complexType = new ComplexType();
            foreach (var propertyNode in element.Elements().Where(x => x.Name.LocalName.Equals("sequence")).Elements().ToList())
            {
                var property = new Property
                {
                    Name = Helpers.PascalCase(propertyNode.Attribute("name").Value)
                };

                var typeAttribute = propertyNode.Attribute("type");
                if (typeAttribute == null)
                    continue;

                property.DataType = Helpers.FormatClrType(typeAttribute.Value);
                property.IsCollection = !(propertyNode.Attribute("minOccurs") == null && propertyNode.Attribute("maxOccurs") == null);

                complexType.Properties.Add(property);
            }
            return complexType;
        }
    }
}

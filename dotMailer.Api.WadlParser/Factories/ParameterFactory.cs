using System.Xml.Linq;
using dotMailer.Api.WadlParser.Factories.Abstract;

namespace dotMailer.Api.WadlParser.Factories
{
    public class ParameterFactory : IParameterFactory
    {
        public Parameter Build(XElement element)
        {
            var parameter = new Parameter
            {
                Name = element.Attribute("name").Value,
                DataType = Helpers.FormatClrType(element.Attribute("type").Value),
                Required = bool.Parse(element.Attribute("required").Value)
            };
            return parameter;
        }
    }
}
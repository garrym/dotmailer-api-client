using System;
using System.Xml.Linq;
using dotMailer.Api.WadlParser.Factories.Abstract;

namespace dotMailer.Api.WadlParser.Factories
{
    public class ParameterFactory : IParameterFactory
    {
        public Parameter Build(XElement element)
        {
            switch (element.Name.LocalName.ToLower())
            {
                case "param":
                    return new Parameter
                    {
                        Name = Helpers.CamelCase(element.Attribute("name").Value),
                        DataType = Helpers.FormatClrType(element.Attribute("type").Value),
                        Required = bool.Parse(element.Attribute("required").Value)
                    };
                case "representation":
                    return new Parameter
                    {
                        Name = Helpers.CamelCase(element.Attribute("element").Value),
                        DataType = Helpers.FormatClrType(element.Attribute("element").Value),
                        Required = true // TODO: Check if this is necessary
                    };
            }
            throw new Exception("Unknown parameter");
        }
    }
}
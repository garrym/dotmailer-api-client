using System.Collections.Generic;
using dotMailer.Api.WadlParser.Methods.Abstract;
using dotMailer.Api.WadlParser.Types;

namespace dotMailer.Api.WadlParser
{
    public class RestDefinition
    {
        public string BaseAddress { get; set; }

        public readonly IList<ComplexType> ComplexTypes = new List<ComplexType>();

        public readonly IList<SimpleType> SimpleTypes = new List<SimpleType>();

        public readonly IList<Method> Methods = new List<Method>();
    }
}
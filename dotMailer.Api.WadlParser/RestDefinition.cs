using System.Collections.Generic;

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
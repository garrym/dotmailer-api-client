using System;
using System.Collections.Generic;
using System.Linq;

namespace dotMailer.Api.WadlParser
{
    public static class Helpers
    {
        public static readonly IList<TypeMapper> TypeMappers = new List<TypeMapper>()
        {
            new TypeMapper("dateTime", "DateTime"),
            new TypeMapper("boolean", "bool"),
            new TypeMapper("guid", "Guid")
        };

        public static string FormatClrType(string value)
        {
            value = value.Replace("xs:", "");

            var typeMapper = TypeMappers.SingleOrDefault(x => x.TypeName.Equals(value, StringComparison.OrdinalIgnoreCase));

            return typeMapper == null ? value : typeMapper.ResolutionTypeName;
        }

        public static string PascalCase(string value)
        {
            return char.ToUpper(value[0]) + value.Substring(1);
        }
    }
}
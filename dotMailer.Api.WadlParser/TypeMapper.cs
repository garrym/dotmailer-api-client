using System.Collections.Generic;

namespace dotMailer.Api.WadlParser
{
    /// <summary>
    /// Allows user to specify custom mapping from WADL type to CLR type
    /// </summary>
    public class TypeMapper
    {
        public TypeMapper(string typeName, string resolutionTypeName, IEnumerable<string> usings = null) // TODO: Support usings
        {
            TypeName = typeName;
            ResolutionTypeName = resolutionTypeName;

            if (usings != null)
                Usings.AddRange(usings);
        }

        public string TypeName
        { get; private set; }

        public string ResolutionTypeName
        { get; private set; }

        public readonly List<string> Usings = new List<string>();
    }
}

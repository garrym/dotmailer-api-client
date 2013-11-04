using System.Collections.Generic;
using System.Linq;

namespace dotMailer.Api.WadlParser.Types
{
    public class SimpleType : CodeBuilder
    {
        public string Name
        { get; set; }

        public readonly IList<string> Values = new List<string>();

        public override string ToString()
        {
            AddLine("namespace dotMailer.Api.Resources.Enums");
            AddLine("{");
            AddLine(1, "public enum {0}", Name);
            AddLine(1, "{");
            foreach (var value in Values)
            {
                var last = value == Values.Last();

                AddLine(2, "{0}{1}", value, last ? "" : ",");
            }
            AddLine(1, "}");
            AddLine("}");

            return base.ToString();
        }
    }
}
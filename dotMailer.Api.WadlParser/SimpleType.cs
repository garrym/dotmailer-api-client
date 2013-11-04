using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotMailer.Api.WadlParser
{
    public class SimpleType
    {
        public string Name
        { get; set; }

        public readonly IList<string> Values = new List<string>();

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLineFormat("namespace dotMailer.Api.Resources.Enums");
            sb.AppendLineFormat("{");
            sb.AppendLineFormat("\tpublic enum {0}", Name);
            sb.AppendLineFormat("\t{");
            foreach (var value in Values)
            {
                var last = value == Values.Last();

                sb.AppendLineFormat("\t\t{0}{1}", value, last ? "" : ",");
            }
            sb.AppendLineFormat("\t}");

            sb.AppendLineFormat("}");
            return sb.ToString();
        }
    }
}
using System.Linq;
using System.Text;

namespace dotMailer.Api.WadlParser
{
    public abstract class CodeBuilder
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();

        protected void AddLine(int indentation, string value, params object[] args)
        {
            var indents = "";
            for (var i = 0; i < indentation; i++)
                indents += "\t";

            value = indents + value;

            if (args.Any())
                value = string.Format(value, args);
            stringBuilder.AppendLine(value);
        }

        protected void AddEmptyLine()
        {
            stringBuilder.AppendLine();
        }

        public override string ToString()
        {
            return stringBuilder.ToString();
        }
    }
}

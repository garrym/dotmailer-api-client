using System.Linq;
using System.Text;

namespace dotMailer.Api.WadlParser
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendLineFormat(this StringBuilder stringBuilder, string value, params object[] args)
        {
            return stringBuilder.AppendLine(args.Any() ? string.Format(value, args) : value);
        }
    }
}
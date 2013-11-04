using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotMailer.Api.WadlParser
{
    public class ComplexType
    {
        public string Name
        { get; set; }

        public readonly IList<Property> Properties = new List<Property>();

        public bool IsUsingSimpleTypes { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            var usingsPresent = false;

            if (UsingSystem())
            {
                sb.AppendLine("using System;");
                usingsPresent = true;
            }

            if (UsingCollections())
            {
                sb.AppendLine("using System.Collections.Generic;");
                usingsPresent = true;
            }

            if (IsUsingSimpleTypes)
            {
                sb.AppendLine("using dotMailer.Api.Resources.Enums;");
                usingsPresent = true;
            }

            if (usingsPresent)
                sb.AppendLine();

            sb.AppendLineFormat("namespace dotMailer.Api.Resources.Models");
            sb.AppendLineFormat("{");
            if (ShouldBeCollection())
            {
                sb.AppendLineFormat("\tpublic class {0} : List<{1}>", Name, Properties.First().DataType);
                sb.AppendLineFormat("\t{ }");
            }
            else
            {
                sb.AppendLineFormat("\tpublic class {0}", Name);
                sb.AppendLineFormat("\t{");
                foreach (var property in Properties)
                {
                    sb.AppendLineFormat("\t\tpublic {0} {1}", GetClrDataType(property), property.Name);
                    sb.AppendLineFormat("\t\t{ get; set; }");
                    if (property != Properties.Last())
                        sb.AppendLine();
                }
                sb.AppendLineFormat("\t}");
            }

            sb.AppendLineFormat("}");
            return sb.ToString();
        }

        private bool UsingSystem()
        {
            return Properties.Any(x => x.DataType.Equals("DateTime") || x.DataType.Equals("Guid"));
        }

        private bool UsingCollections()
        {
            return Properties.Any(x => x.IsCollection);
        }

        private bool ShouldBeCollection()
        {
            // This is hacky but seems to make sense.
            // If a complex type has a single element that is a collection of T
            // then the class should inherit from List<T> rather than have a
            // List<T> as a property with the singular name
            // i.e. this:
            // public class ApiTemplateList : List<ApiTemplate>
            // {}
            //
            // And not this:
            //
            // public class ApiTemplateList
            // {
            //     public IList<ApiTemplate> ApiTemplate { get; set; }
            // }
            // Makes more sense.
            return (Properties.Count == 1 && Properties.First().IsCollection);
        }

        private string GetClrDataType(Property property)
        {
            if (property.IsCollection)
                return "IList<" + property.DataType + ">";
            return property.DataType;
        }
    }
}
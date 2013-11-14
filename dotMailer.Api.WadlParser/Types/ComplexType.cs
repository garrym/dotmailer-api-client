using System.Collections.Generic;
using System.Linq;

namespace dotMailer.Api.WadlParser.Types
{
    public class ComplexType : CodeBuilder
    {
        public string Name
        { get; set; }

        public readonly IList<Property> Properties = new List<Property>();

        public bool IsUsingSimpleTypes { get; set; }

        public override string ToString()
        {
            var usingsPresent = false;

            var usingSystem = Properties.Any(x => x.DataType.Equals("DateTime") || x.DataType.Equals("Guid"));
            if (usingSystem)
            {
                AddLine("using System;");
                usingsPresent = true;
            }

            //var usingCollections = Properties.Any(x => x.IsCollection);
            //if (usingCollections)
            //{
                AddLine("using System.Collections.Generic;");
                usingsPresent = true;
            //}

            if (IsUsingSimpleTypes)
            {
                AddLine("using dotMailer.Api.Resources.Enums;");
                usingsPresent = true;
            }

            if (usingsPresent)
                AddEmptyLine();

            AddLine("namespace dotMailer.Api.Resources.Models");
            AddLine("{");

            var isCollection = (Properties.Count == 1 && Properties.First().IsCollection);

            if (isCollection)
            {
                AddLine(1, "public class {0} : List<{1}>", Name, Properties.First().DataType);
                AddLine(1, "{ }");
            }
            else
            {
                AddLine(1, "public class {0}", Name);
                AddLine(1, "{");
                foreach (var property in Properties)
                {
                    AddLine(2, "public {0} {1}", GetClrDataType(property), property.Name);
                    AddLine(2, "{ get; set; }");
                    if (property != Properties.Last())
                        AddEmptyLine();
                }
                AddLine(1, "}");
            }

            AddLine("}");

            return base.ToString();
        }

        private string GetClrDataType(Property property)
        {
            if (property.IsCollection)
                return "IList<" + property.DataType + ">";
            return property.DataType;
        }
    }
}
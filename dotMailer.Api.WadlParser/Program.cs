using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using dotMailer.Api.WadlParser.Methods;
using dotMailer.Api.WadlParser.Methods.Abstract;
using dotMailer.Api.WadlParser.Types;

namespace dotMailer.Api.WadlParser
{
    class Program
    {
        private static readonly RestDefinition restDefinition = new RestDefinition();
        private const string outputDirectory = @"C:\Output\";
        private const string url = "https://api.dotmailer.com/v2/help/wadl";

        static void Main()
        {
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine(" dotMailer REST API POCO Generator");
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("This application will generate C# classes from the WADL description for the dotMailer API found at {0}", url);
            Console.WriteLine("");
            Console.WriteLine("These classes will be placed in the directory '{0}'", outputDirectory);
            Console.WriteLine("Press Y to continue, any other key to exit");
            var consoleKey = Console.ReadKey();
            if (consoleKey.Key != ConsoleKey.Y)
                return;

            Console.Clear();

            var document = XDocument.Load(url);

            ProcessTypes(document);
            ProcessMethods(document);

            PostProcessTypes();

            GenerateClasses();

            Console.WriteLine();
            Console.WriteLine("Finished!");
            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.WriteLine();
            Console.ReadKey();
        }

        private static bool IsUsingSimpleTypes(ComplexType complexType)
        {
            var isUsingSimpleTypes = false;
            foreach (var property in complexType.Properties)
            {
                if (!isUsingSimpleTypes)
                    isUsingSimpleTypes = restDefinition.SimpleTypes.Any(x => x.Name.Equals(property.DataType, StringComparison.OrdinalIgnoreCase));
            }
            return isUsingSimpleTypes;
        }

        private static void PostProcessTypes()
        {
            foreach (var complexType in restDefinition.ComplexTypes)
            {
                complexType.IsUsingSimpleTypes = IsUsingSimpleTypes(complexType);

                foreach (var property in complexType.Properties)
                    property.DataType = GetPropertyDataType(property);
            }
        }

        /// <summary>
        /// Check if there's a complex type matching the name, if not assume it's a primitive
        /// </summary>
        private static string GetPropertyDataType(Property property)
        {
            var ct = restDefinition.ComplexTypes.SingleOrDefault(x => x.Name.Equals(property.DataType, StringComparison.OrdinalIgnoreCase));
            if (ct != null)
                return ct.Name;

            var ctCollection = restDefinition.ComplexTypes.SingleOrDefault(x => ("IList<" + x.Name + ">").Equals(property.DataType, StringComparison.OrdinalIgnoreCase));
            if (ctCollection != null)
                return "IList<" + ctCollection.Name + ">";

            var st = restDefinition.SimpleTypes.SingleOrDefault(x => x.Name.Equals(property.DataType, StringComparison.OrdinalIgnoreCase));
            if (st != null)
                return st.Name;

            var stCollection = restDefinition.SimpleTypes.SingleOrDefault(x => ("IList<" + x.Name + ">").Equals(property.DataType, StringComparison.OrdinalIgnoreCase));
            if (stCollection != null)
                return "IList<" + stCollection.Name + ">";

            return property.DataType;
        }

        private static void GenerateClasses()
        {
            Console.Write("Generating classes...");
            GenerateModelClasses();
            CopyClass("Client");
            CopyClass("Request");
            GenerateClientClass();
            CopyClass("ServiceResult");
            Console.Write(" Done!");
            Console.WriteLine("");
        }

        private static void CopyClass(string fileNameWithoutExtension)
        {
            var clientCode = LoadTemplateClass(fileNameWithoutExtension);

            var fileName = string.Format("{0}.cs", fileNameWithoutExtension);
            var filePath = Path.Combine(outputDirectory, fileName);

            CreateFile(filePath, clientCode);
        }

        private static void GenerateClientClass()
        {
            const string templateName = "Client.Generated";
            var clientCode = LoadTemplateClass(templateName);

            clientCode = clientCode.Replace("[[BASE-ADDRESS]]", restDefinition.BaseAddress);
            clientCode = GenerateMethodsCode(clientCode);

            var fileName = string.Format("{0}.cs", templateName);
            var filePath = Path.Combine(outputDirectory, fileName);

            CreateFile(filePath, clientCode);
        }

        private static string GenerateMethodsCode(string clientCode)
        {
            var sb = new StringBuilder();
            foreach (var method in restDefinition.Methods.OrderBy(x => x.Name))
            {
                sb.Append(method);
                if (method != restDefinition.Methods.Last())
                    sb.AppendLine();
            }

            return clientCode.Replace("[[METHODS]]", sb.ToString());
        }

        private static void CreateFile(string path, string data)
        {
            using (var file = File.CreateText(path))
            {
                file.Write(data);
                file.Close();
            }
        }

        private static string LoadTemplateClass(string fileNameWithoutExtension)
        {
            var binDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", ""); ;
            var codePath = Path.Combine(binDirectory, string.Format(@"Resources\{0}.txt", fileNameWithoutExtension));
            return File.ReadAllText(codePath);
        }

        private static void GenerateModelClasses()
        {
            foreach (var complexType in restDefinition.ComplexTypes)
            {
                var fileName = complexType.Name + ".cs";
                var directory = Path.Combine(outputDirectory, "Resources", "Models");
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var filePath = Path.Combine(directory, fileName);
                using (var file = File.CreateText(filePath))
                {
                    file.Write(complexType.ToString());
                    file.Close();
                }
            }

            foreach (var simpleType in restDefinition.SimpleTypes)
            {
                var fileName = simpleType.Name + ".cs";
                var directory = Path.Combine(outputDirectory, "Resources", "Enums");
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var filePath = Path.Combine(directory, fileName);
                using (var file = File.CreateText(filePath))
                {
                    file.Write(simpleType.ToString());
                    file.Close();
                }
            }
        }

        private static void ProcessTypes(XDocument document)
        {
            if (document.Root == null)
                return;

            var grammarsNode = document.Root.Elements().Single(x => x.Name.LocalName.Equals("grammars"));
            var schemaNode = grammarsNode.Elements().First();
            var elementNodes = schemaNode.Elements().Where(x => x.Name.LocalName.Equals("element")).ToList();
            var complexTypeNodes = schemaNode.Elements().Where(x => x.Name.LocalName.Equals("complexType")).ToList();
            var simpleTypeNodes = schemaNode.Elements().Where(x => x.Name.LocalName.Equals("simpleType")).ToList();

            foreach (var elementNode in elementNodes)
            {
                var name = elementNode.Attribute("name").Value;
                var elementType = elementNode.Attribute("type").Value;

                var complexTypeNode = complexTypeNodes.SingleOrDefault(x => x.Attribute("name").Value.Equals(elementType));
                if (complexTypeNode != null)
                    ProcessComplexType(name, complexTypeNode);

                var simpleTypeNode = simpleTypeNodes.SingleOrDefault(x => x.Attribute("name").Value.Equals(elementType));
                if (simpleTypeNode != null)
                    ProcessSimpleType(name, simpleTypeNode);
            }
        }

        private static string PascalCase(string value)
        {
            return char.ToUpper(value[0]) + value.Substring(1);
        }

        private static void ProcessComplexType(string name, XElement element)
        {
            var complexType = new ComplexType { Name = name };
            foreach (var propertyNode in element.Elements().Where(x => x.Name.LocalName.Equals("sequence")).Elements().ToList())
            {
                var property = new Property
                {
                    Name = PascalCase(propertyNode.Attribute("name").Value)
                };

                var typeAttribute = propertyNode.Attribute("type");
                if (typeAttribute == null)
                    continue;

                property.DataType = FormatClrType(typeAttribute.Value);
                property.IsCollection = !(propertyNode.Attribute("minOccurs") == null && propertyNode.Attribute("maxOccurs") == null);

                complexType.Properties.Add(property);
            }

            restDefinition.ComplexTypes.Add(complexType);
        }

        private static void ProcessSimpleType(string name, XElement element)
        {
            if (name.Equals("guid", StringComparison.OrdinalIgnoreCase))
                return;

            var simpleType = new SimpleType { Name = name };
            foreach (var value in element.Elements().Where(x => x.Name.LocalName.Equals("restriction")).Elements().Select(x => x.Attribute("value").Value))
            {
                simpleType.Values.Add(value);
            }
            restDefinition.SimpleTypes.Add(simpleType);
        }

        private static void ProcessMethods(XDocument document)
        {
            var resourcesElement = document.Root.Elements().First(x => x.Name.LocalName.Equals("resources"));
            restDefinition.BaseAddress = resourcesElement.Attribute("base").Value;
            var methodElements = resourcesElement.Elements().ToList();
            foreach (var element in methodElements)
            {
                ProcessMethodNode(element);
            }
        }

        private static void ProcessMethodNode(XElement element)
        {
            var path = element.Attribute("path").Value;
            var id = element.Attribute("id").Value;

            foreach (var methodNode in element.Elements())
            {
                var method = GetMethod(methodNode);
                method.Path = path;
                method.Id = id;

                var documentationNode = methodNode.Elements().First(x => x.Name.LocalName.Equals("doc"));
                method.Description = documentationNode.Value;

                var requestNode = methodNode.Elements().First(x => x.Name.LocalName.Equals("request"));
                foreach (var parameterNode in requestNode.Elements())
                    method.Parameters.Add(ProcessParameterNode(parameterNode));

                var responseNodes = methodNode.Elements().Where(x => x.Name.LocalName.Equals("response"));
                foreach (var responseNode in responseNodes)
                {
                    method.Responses.Add(ProcessResponseNode(responseNode));
                }

                restDefinition.Methods.Add(method);
            }
        }

        private static Method GetMethod(XElement element)
        {
            Method method;
            var httpMethod = element.Attribute("name").Value.ToLower();
            switch (httpMethod)
            {
                case "put":
                    method = new PutMethod();
                    break;
                case "get":
                    method = new GetMethod();
                    break;
                case "delete":
                    method = new DeleteMethod();
                    break;
                case "post":
                    method = new PostMethod();
                    break;
                default:
                    throw new Exception("Unknown method type");

            }
            method.Name = element.Attribute("id").Value;
            return method;
        }

        private static Parameter ProcessParameterNode(XElement element)
        {
            var parameter = new Parameter
            {
                Name = element.Attribute("name").Value,
                DataType = FormatClrType(element.Attribute("type").Value),
                Required = bool.Parse(element.Attribute("required").Value)
            };
            return parameter;
        }

        private static Response ProcessResponseNode(XElement responseNode)
        {
            var response = new Response();
            var statusCode = int.Parse(responseNode.Attribute("status").Value);
            response.StatusCode = statusCode;
            var docNode = responseNode.Elements().FirstOrDefault(x => x.Name.LocalName.Equals("doc"));
            if (docNode != null)
                response.Message = docNode.Value;

            var representationNodes = responseNode.Elements().Where(x => x.Name.LocalName.Equals("representation", StringComparison.OrdinalIgnoreCase)).ToList();
            if (representationNodes.Any())
            {
                var typeAttribute = representationNodes.First().Attribute("element");
                if (typeAttribute != null)
                    response.ReturnType = FormatClrType(typeAttribute.Value);
            }

            return response;
        }

        private static string FormatClrType(string value)
        {
            value = value.Replace("xs:", "");
            switch (value)
            {
                case "dateTime":
                    return "DateTime";
                case "boolean":
                    return "bool";
                case "guid":
                    return "Guid";
            }
            return value;
        }
    }
}

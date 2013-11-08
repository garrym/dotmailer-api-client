using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using dotMailer.Api.WadlParser.Factories;
using dotMailer.Api.WadlParser.Types;

namespace dotMailer.Api.WadlParser
{
    class Program
    {
        private static readonly RestDefinition restDefinition = new RestDefinition();
        private const string outputDirectory = @"C:\Output\";
        private const string url = "https://api.dotmailer.com/v2/help/wadl";

        private static readonly IMethodFactory methodFactory = new MethodFactory();
        private static readonly IComplexTypeFactory complexTypeFactory = new ComplexTypeFactory();
        private static readonly ISimpleTypeFactory simpleTypeFactory = new SimpleTypeFactory();

        static void Main()
        {
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine(" dotMailer REST API Client Generator");
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
                { 
                    // TODO: Assign name in complexTypeFactory instead
                    var complexType = complexTypeFactory.Build(complexTypeNode);
                    complexType.Name = name;
                    restDefinition.ComplexTypes.Add(complexType);
                }

                var simpleTypeNode = simpleTypeNodes.SingleOrDefault(x => x.Attribute("name").Value.Equals(elementType));
                if (simpleTypeNode != null)
                {
                    if (name.Equals("guid", StringComparison.OrdinalIgnoreCase))
                        continue;

                    // TODO: Assign name in simpleTypeFactory instead
                    var simpleType = simpleTypeFactory.Build(simpleTypeNode);
                    simpleType.Name = name;
                    restDefinition.SimpleTypes.Add(simpleType);
                }
            }
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
            foreach (var methodNode in element.Elements())
            {
                var method = methodFactory.Build(methodNode);
                restDefinition.Methods.Add(method);
            }
        }
    }
}

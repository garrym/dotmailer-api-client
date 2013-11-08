using System.Xml.Linq;

namespace dotMailer.Api.WadlParser.Factories
{
    public interface IFactory<T>
    {
        T Build(XElement element);
    }
}
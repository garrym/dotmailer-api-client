using System.Xml.Linq;

namespace dotMailer.Api.WadlParser.Factories.Abstract
{
    public interface IFactory<T>
    {
        T Build(XElement element);
    }
}
namespace dotMailer.Api.WadlParser
{
    public class Parameter
    {
        public string Name
        { get; set; }

        public string DataType
        { get; set; }

        public bool Required
        { get; set; }

        public override string ToString()
        {
            return string.Format(Required ? "{0} {1}, " : "{0}? {1} = null, ", DataType, Name);
        }
    }
}
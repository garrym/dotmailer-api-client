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
            if (Required)
                return string.Format("{0} {1}, ", DataType, Name);

            // Check if it's nullable
            return string.Format("{0}? {1} = null, ", DataType, Name);
        }
    }
}
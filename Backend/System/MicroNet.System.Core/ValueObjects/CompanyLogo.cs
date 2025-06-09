namespace MicroNet.System.Core.ValueObjects
{
    public class CompanyLogo
    {
        public string FileName { get; private set; }
        public byte[] Content { get; private set; }

        public CompanyLogo(string fileName, byte[] content)
        {
            FileName = fileName;
            Content = content;
        }
    }
}

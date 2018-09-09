using System.Text;

namespace libTask1
{
    public class TextFile : File
    {
        public TextFile(string path) : base(path)
        {
        }

        public void Append(string text)
        {
            AppendBytes(Encoding.UTF8.GetBytes(text));
        }

        public string GetContent()
        {
            return Encoding.UTF8.GetString(GetBytes());
        }
    }
}

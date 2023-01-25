using TatterFitness.App.Enums;

namespace TatterFitness.App.Models
{
    public class PatchOperation
    {
        public string Value { get; private set; }
        public string Path { get; private set; }
        public string Op { get; private set; }

        public PatchOperation(PatchOpCommand operation, string path, object value)
        {
            Op = operation.ToString("g");
            Path = path;
            Value = value.ToString();
        }
    }
}

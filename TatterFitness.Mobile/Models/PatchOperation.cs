using Ardalis.GuardClauses;
using TatterFitness.Mobile.Enums;

namespace TatterFitness.Mobile.Models
{
    public class PatchOperation
    {
        public string Value { get; private set; }
        public string Path { get; private set; }
        public string Op { get; private set; }

        public PatchOperation(PatchOpCommand operation, string path, object value)
        {
            path = Guard.Against.Null(path, nameof(path));
            value = Guard.Against.Null(value, nameof(value));

            Op = operation.ToString("g");
            Path = path;
            Value = value?.ToString();
        }
    }
}

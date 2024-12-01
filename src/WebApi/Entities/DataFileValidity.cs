namespace WebApi.Entities
{
    public class DataFileValidity
    {
        public bool FileValid { get; set; } = true;

        public List<string> InvalidLines { get; set; } = new List<string>();
    }
}

namespace WebApi.Entities
{
    public class UserDataValidity
    {
        public bool IsValid => Errors.Count == 0;
        public List<string> Errors { get; set; } = new List<string>();
        public required UserData UserData { get; set; }
    }
}

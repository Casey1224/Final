namespace Final.Models
{
    public class keep
    {
        public int Id { get; set; }
        public string creatorId { get; set; }
        public string img { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int views { get; set; }
        public int kept { get; set; }
        public Account creator { get; set; }
    }
    public class keepvm : keep
    {

        public int vaultKeepId { get; set; }
    }



}
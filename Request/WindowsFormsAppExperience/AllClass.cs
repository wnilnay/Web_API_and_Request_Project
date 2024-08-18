namespace WindowsFormsAppExperience
{
    public class PostClass
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LoginAccount { get; set; }
        public string Password { get; set; }
    }

    public class PutClass
    {
        public string LoginAccount { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
    public class DeleteClass
    {
        public string LoginAccount { get; set; }
        public string Password { get; set; }
    }
}

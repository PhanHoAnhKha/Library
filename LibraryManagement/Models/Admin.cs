namespace LibraryManagement.Models
{
    public class Admin
    {
        public int AdminID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; } // Lưu mật khẩu đã băm
    }
}

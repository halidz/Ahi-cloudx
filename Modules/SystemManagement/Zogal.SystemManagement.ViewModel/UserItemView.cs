using Zogal.SystemManagement.Core;

namespace Zogal.SystemManagement.ViewModel
{
    public class UserItemView
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public UserStatus Status { get; set; }
    }
}

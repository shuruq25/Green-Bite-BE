using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Entity
{
    public class Users
    {
        public int UserID { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? EmailAddress { get; set; }
        public int PaymentID { get; set; }
        public string? Phone { get; set; }
        public Role UserRole { get; set; }
        public byte[]? Salt { get; set; }
        public enum Role
        {
            Admin,
            User,
            Guest,
        }
    }
}

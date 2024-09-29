using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;

namespace src.Entity
{
    public class User
    {
        public Guid UserID { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? EmailAddress { get; set; }
        public int AddressID { get; set; }
        public string? Phone { get; set; }
        public Role UserRole { get; set; }
        public byte[]? Salt { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Role
        {
            Admin,
            Customer,
            Guest,
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Username { get; set; }

        [Column(TypeName = "varbinary(MAX)")]
        public byte[] PasswordHash { get; set; }

        [Column(TypeName = "varbinary(MAX)")]
        public byte[] PasswordSalt { get; set; }
    }
}

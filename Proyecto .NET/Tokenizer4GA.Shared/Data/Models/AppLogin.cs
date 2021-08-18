using System;
using SQLite;

namespace Tokenizer4GA.Shared.Data.Models
{
    public class AppLogin : TrackEntity
    {
        [PrimaryKey]
        [AutoIncrement]
        public override int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime LastLogin { get; set; }
    }
}

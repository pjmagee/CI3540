using System;
using System.Collections.Generic;
using System.Data.Objects;

namespace CI3540.Core.Entities
{
    public abstract class User
    {
        public int Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
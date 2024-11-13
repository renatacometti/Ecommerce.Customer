﻿using Domain.Common;

namespace Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateTime DateBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Active { get; set; }
        public virtual ICollection<AddressEntity> Addresses { get; set; }
        public int Id_Profile { get; set; }
        public virtual ProfileEntity Profiles { get; set; }
    }
}
﻿using Domain.Common;

namespace Domain.DTOs.Profile
{
    public class ProfileDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }

    }
}

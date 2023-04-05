﻿namespace ModelHouse.Security.Resources
{
    public class GetUserResource
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string RegistrationDate { get; set; }
        public string LastLogin { get; set; }
        public long AccountStatus { get; set; }
        public long AccountId { get; set; }
    }
}

namespace CattleMgmApi.Dtos.Farmer
{
    public class FarmerCreateDto
    {
        public string PersonalNumber { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }
    }
}

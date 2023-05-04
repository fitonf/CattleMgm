namespace CattleMgmApi.Dtos
{
    public class CattleCreateDto
    {
        public Guid UniqueIdentifier { get; set; }

        public int FarmId { get; set; }

        public int BreedId { get; set; }

        public int Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public double Weight { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

    }
}

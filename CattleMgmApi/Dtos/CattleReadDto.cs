namespace CattleMgmApi.Dtos
{
    public class CattleReadDto
    {
        public int Id { get; set; }

        public Guid UniqueIdentifier { get; set; }

        public string? Name { get; set; }

        public double Weight { get; set; }
    }
}

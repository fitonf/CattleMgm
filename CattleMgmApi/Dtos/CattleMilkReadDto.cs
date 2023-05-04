namespace CattleMgmApi.Profiles
{
    internal class CattleMilkReadDto
    {
        public int Id { get; set; }
        public Guid Identifier { get; set; }

        public int CattleId { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; } = null!;

        public double LitersCollected { get; set; }
    }
}
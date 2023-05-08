namespace CattleMgmApi.Dtos
{
    public class MediaCreateDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public Guid Identifier { get; set; }

        public string Path { get; set; } = null!;

        public int Type { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; } = null!;
    }
}
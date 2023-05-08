namespace CattleMgmApi.Dtos
{
    public class MediaReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Path { get; set; } = null!;
        public int Type { get; set; }
    }
}
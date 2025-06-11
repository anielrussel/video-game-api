namespace VideoGameApi.Models
{
    public class VideoGameDetails
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public int VideoGameId { get; set; }
    }
}

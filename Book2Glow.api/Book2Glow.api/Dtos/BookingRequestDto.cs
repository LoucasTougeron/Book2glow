namespace Book2Glow.Api.Dtos
{
    public class BookingRequestDto
    {
        public DateOnly Date { get; set; }
        public int StartTime { get; set; }
        public Guid ServiceId { get; set; }
    }
}

namespace TaskManagement.Application.DTOs
{
    public record TaskDTO
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string? Description { get; init; }
        public DateTime CreatedDate { get; init; }
        public DateTime? DueDate { get; init; }
        public DateTime? CompletedDate { get; init; }
        public int StatusId { get; init; }
    }
}

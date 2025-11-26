public class UpdateBookRequest
{
    public string? Title { get; set; }  // Nullable string
    public string? Author { get; set; }
    public string? Publisher { get; set; }
    public string? Category { get; set; }
    public int? PublicationYear { get; set; }  // Nullable int
    public decimal? Price { get; set; }  // Nullable decimal
    public int? TotalCopies { get; set; }  // Nullable int
    public int? AvailableCopies { get; set; }  // Nullable int
}
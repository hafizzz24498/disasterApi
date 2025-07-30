namespace disasterApi.Core.Dtos
{
    public record RegionDto
    {
        public Guid RegionId { get; set; }
        public Location LocationCoordinates { get; set; } = new();
        public List<string> DisasterTypes { get; set; } = new();    
    }

    public record Location
    {
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
    }
}

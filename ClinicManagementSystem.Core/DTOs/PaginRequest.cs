namespace ClinicManagementSystem.Core.DTOs
{
    public class PaginRequest
    {
        public int Take { get; set; } = 10;
        public int Skip { get; set; }
    }
}

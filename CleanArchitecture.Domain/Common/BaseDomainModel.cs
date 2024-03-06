namespace CleanArchitecture.Domain.Common
{
    public abstract class BaseDomainModel
    {
        public int Id { get; set; }
        public DateTime? CreateAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdateAt { get; set;}
        public string? UpdatedBy { get; set;}
    }
}

namespace ESourcing.Sourcing.Settings
{
    public interface ISourcingDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

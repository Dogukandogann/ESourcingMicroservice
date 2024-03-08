namespace ESourcing.Sourcing.Settings
{
    public class SourcingDbSettings : ISourcingDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

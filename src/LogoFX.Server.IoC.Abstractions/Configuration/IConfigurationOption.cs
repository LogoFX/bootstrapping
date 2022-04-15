namespace LogoFX.Server.IoC.Abstractions.Configuration
{
    /// <summary>
    /// Implement to register the configuration option automatically.
    /// </summary>
    public interface IConfigurationOption
    {
        /// <summary>
        /// Gets the section name.
        /// </summary>
        string SectionName { get; }
    }
}

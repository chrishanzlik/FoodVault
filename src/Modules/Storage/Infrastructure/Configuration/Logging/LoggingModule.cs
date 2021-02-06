using Autofac;
using Microsoft.Extensions.Logging;

namespace FoodVault.Modules.Storage.Infrastructure.Configuration.Logging
{
    /// <summary>
    /// Logging autofac module
    /// </summary>
    internal class LoggingModule : Module
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingModule" /> class.
        /// </summary>
        /// <param name="logger"></param>
        public LoggingModule(ILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_logger)
                   .As<ILogger>()
                   .SingleInstance();
        }
    }
}

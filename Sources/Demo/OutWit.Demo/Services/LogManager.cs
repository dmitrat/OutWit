using Serilog.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Serilog.Exceptions;
using OutWit.Common.Logging.Interfaces;
using OutWit.Common.Utils;

namespace OutWit.Demo.Services
{
    public class LogManager : ILogManager
    {
        #region Constants

        private const string LOG_FOLDER = "Log";
        private const string LOG_FILE_NAME = "log.txt";

        #endregion

        #region Constructors

        public LogManager()
        {
            var level = ServiceLocator.Get.Settings.LogMinimumLevel;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(level)
                .Enrich.WithExceptionDetails()
                .WriteTo.File(Path.Combine(Application.ResourceAssembly.ApplicationDataPath(2, LOG_FOLDER), LOG_FILE_NAME),
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true, fileSizeLimitBytes: 524288)
                .CreateLogger();

            LoggerFactory = new SerilogLoggerFactory();
        }

        #endregion

        #region Properties

        public Microsoft.Extensions.Logging.ILoggerFactory LoggerFactory { get; }

        #endregion
    }
}

﻿using CV19.Models.CV19;
using CV19.Services;
using CV19.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Linq;
using System.Windows;

namespace CV19
{
    public partial class App : Application
    {
        public static bool IsDesignMode { get; private set; } = true;

        private static IHost __Host;
        public static IHost Host => __Host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
        protected override async void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;
            var host = Host;
            base.OnStartup(e);

            await host.StartAsync().ConfigureAwait(false);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            var host = Host;
            await host.StopAsync().ConfigureAwait(false);
            host.Dispose();
            __Host = null;
        }

        internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<DataCountriesService>();
            services.AddSingleton<CountriesStatisticsView>();
        }
    }
}

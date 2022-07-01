using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EmployeeManager.Services;
using EmployeeManager.ViewModels;
using EmployeeManager.Repositories;
using EmployeeManager.Views;
using EmployeeManager.Models.Options;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using EmployeeService.Client;

namespace EmployeeManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider serviceProvider;

        public App()
        {
            InitializeComponent();
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        } 
        private  void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>((sp) =>
            {
                ConfigurationBuilder configurationBuilder = (ConfigurationBuilder)new ConfigurationBuilder().SetBasePath(System.IO.Directory.GetCurrentDirectory()).AddJsonFile("AppSettings.Json", false, true);
                IConfiguration configuration = configurationBuilder.Build();
                return configuration;
            });
            services.AddSingleton<IEmployeeServiceClient>((sp) =>
            {
                IConfiguration configuration = sp.GetRequiredService<IConfiguration>();
                ServiceOptions serviceOptions = new ServiceOptions();
                configuration.GetSection(serviceOptions.Service).Bind(serviceOptions);
                return new EmployeeServiceClient(serviceOptions.BaseUrl, serviceOptions.Token);
            });
            services.AddSingleton<IEmployeeDataRepository, EmployeeDataRepository>();
            services.AddSingleton<IEmployeeManagerState, EmployeeManagerState>();
            services.AddTransient<EmployeeAdd>();
            services.AddTransient<EmployeeEdit>();
            services.AddTransient<AddEmployeeViewModel>();
            services.AddTransient<EditEmployeeViewModel>();


            services.AddSingleton<IEmployeeUIService, EmployeeUIService>((sp) =>
            {
                return new EmployeeUIService(sp);
            });
            services.AddTransient<EmployeeListViewModel>();
            services.AddSingleton<MainWindow>(sp =>
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.DataContext = sp.GetRequiredService<EmployeeListViewModel>();
                return mainWindow;
            });
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var mWIndow = serviceProvider.GetRequiredService<MainWindow>();
            mWIndow.Show();
        }
    }
}

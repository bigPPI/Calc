using Caliburn.Micro;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TASK.Logging;
using TASK.Models;
using TASK.ViewModels;

namespace TASK
{
	public class Bootstrapper : BootstrapperBase
	{
		private IWindsorContainer _container;
		private static readonly log4net.ILog _log = LogHelper.GetLogger();

		public Bootstrapper()
		{
			_log.Debug("Application started, initialising Caliburn.Micro...");
			Initialize();
		}

		/// <summary>
		/// Configuring Castle.Windsor DI-container
		/// </summary>
		protected override void Configure()
		{
			_log.Debug("Configuring Bootsrapper started");

			_container = new WindsorContainer();

			_container.Register(Component.For<IWindsorContainer>().Instance(_container));

			_container.Register(Component.For<IWindowManager>().ImplementedBy<WindowManager>().LifestyleSingleton());
			_container.Register(Component.For<IEventAggregator>().ImplementedBy<EventAggregator>().LifestyleSingleton());

			_container.Register(Component.For<ICalculations>().ImplementedBy<Calculations>().LifestyleTransient()); 
			_container.Register(Component.For<IRPNCalculation>().ImplementedBy<RPNCalculation>().LifestyleTransient());

			//Register all ViewModels
			GetType().Assembly.GetTypes()
				.Where(type => type.IsClass)
				.Where(type => type.Name.EndsWith("ViewModel"))
				.ToList()
				.ForEach(viewModelType => _container.Register(Component.For(viewModelType).LifestyleTransient()));
		}

		protected override object GetInstance(Type service, string key)
		{
			var instance = _container.Resolve(service);
			if(instance != null)
			{
				return instance;
			}

			throw new InvalidOperationException("Could not locate any instances.");
		}

		protected override IEnumerable<object> GetAllInstances(Type service)
		{
			var array = _container.ResolveAll(service);
			object[] trgArray = new object[array.Length];
			Array.Copy(array, trgArray, array.Length);

			return trgArray;
		}

		protected override void OnStartup(object sender, StartupEventArgs e)
		{
			DisplayRootViewFor<ShellViewModel>();
			_log.Debug("ShallViev is shown");
		}

		protected override void OnExit(object sender, EventArgs e)
		{
			_log.Debug("Exiting the App...");
		}
	}
}

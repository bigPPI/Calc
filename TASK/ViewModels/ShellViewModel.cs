using Caliburn.Micro;
using TASK.Logging;
using TASK.Models;

namespace TASK.ViewModels
{
	public class ShellViewModel : Conductor<object>
	{
		IWindowManager _manager;
		SimpleCalcViewModel _firstCalculator;
		RPNCalcViewModel _secondCalculator;

		private static readonly log4net.ILog _log = LogHelper.GetLogger();

		public ShellViewModel(IWindowManager manager, ICalculations calc1, IRPNCalculation calc2)
		{
			_manager = manager;
			_firstCalculator = new SimpleCalcViewModel(calc1, manager);
			_secondCalculator = new RPNCalcViewModel(calc2, manager);

			ChangeCalculators();
		}

		private byte _activeCalculator = 2;

		/// <summary>
		/// Handlin Click Event of ChangeCalculators button
		/// </summary>
		public void ChangeCalculators()
		{
			switch (_activeCalculator)
			{
				case 1:
					_activeCalculator = 2;
					ActivateItem(_secondCalculator);

					_log.Info("Changed to RPN calculator");
					break;
				default:
					_activeCalculator = 1;
					ActivateItem(_firstCalculator);

					_log.Info("Changed to Simple calculator");
					break;
			}
		}
	}
}

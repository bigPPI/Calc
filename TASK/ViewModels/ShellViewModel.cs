using Caliburn.Micro;
using System.Collections.Generic;
using System.Windows.Input;
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

		/// <summary>
		/// KeyPressed event handler
		/// </summary>
		/// <param name="keyArgs"></param>
		public void KeyPressed(KeyEventArgs keyArgs)
		{
			string keyValue = keyArgs.Key.ToString();
			var operators = new Dictionary<Key, string>
			{
				{ Key.Add, "+"},
				{ Key.Subtract, "-"},
				{ Key.Divide, "/"},
				{ Key.Multiply, "*"}
			};

			switch (keyArgs.Key)
			{
				case Key.NumPad0:
				case Key.NumPad1:
				case Key.NumPad2:
				case Key.NumPad3:
				case Key.NumPad4:
				case Key.NumPad5:
				case Key.NumPad6:
				case Key.NumPad7:
				case Key.NumPad8:
				case Key.NumPad9:
				case Key.D0:
				case Key.D1:
				case Key.D2:
				case Key.D3:
				case Key.D4:
				case Key.D5:
				case Key.D6:
				case Key.D7:
				case Key.D8:
				case Key.D9:
					if (_activeCalculator == 1)
					{
						_firstCalculator.NumericButton(keyValue.Substring(keyValue.Length - 1));
					}
					else
					{
						_secondCalculator.OrdinaryButtonClicked(keyValue.Substring(keyValue.Length - 1));
					}
					break;
				case Key.Add:
				case Key.Subtract:
				case Key.Divide:
				case Key.Multiply:
					if (_activeCalculator == 1)
					{
						_firstCalculator.ActionButton(operators[keyArgs.Key]);
					}
					else
					{
						_secondCalculator.OrdinaryButtonClicked(operators[keyArgs.Key]);
					}
					break;
				case Key.Decimal:
					if (_activeCalculator == 1)
					{
						_firstCalculator.Dot();
					}
					else
					{
						_secondCalculator.OrdinaryButtonClicked(",");
					}
					break;
				case Key.Delete:
					if (_activeCalculator == 1)
					{
						_firstCalculator.Clear();
					}
					else
					{
						_secondCalculator.Clear();
					}
					break;
				case Key.Back:
					if (_activeCalculator == 1)
					{
						_firstCalculator.BackSpace();
					}
					else
					{
						_secondCalculator.BackSpace();
					}
					break;
				case Key.Enter:
					if (_activeCalculator == 1)
					{
						_firstCalculator.IsEqual();
					}
					else
					{
						_secondCalculator.IsEqual();
					}
					break;
				case Key.Tab:
					ChangeCalculators();
					break;
				default:
					break;
			}
		}
	}
}

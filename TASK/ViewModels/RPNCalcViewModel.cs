using Caliburn.Micro;
using System;
using TASK.Logging;
using TASK.Models;

namespace TASK.ViewModels
{
	public class RPNCalcViewModel :  Conductor<object>
	{
		IWindowManager _manager;
		IRPNCalculation _calculator;

		private static readonly log4net.ILog _log = LogHelper.GetLogger();

		public RPNCalcViewModel(IRPNCalculation calculator, IWindowManager manager)
		{
			_calculator = calculator;
			_manager = manager;
		}

		#region Properties
		private string _expression = string.Empty;
		private string _inputString = "0";

		public string Expression
		{
			get { return _expression; }
			set
			{
				_expression = value;
				NotifyOfPropertyChange(() => Expression);
			}
		}

		public string InputString
		{
			get { return _inputString; }
			set
			{
				_inputString = value;
				NotifyOfPropertyChange(() => InputString);
			}
		}
		#endregion

		#region Buttons' OnClick handlers
		public void One()
		{
			OrdinaryButtonClicked("1");
		}

		public void Two()
		{
			OrdinaryButtonClicked("2");
		}

		public void Three()
		{
			OrdinaryButtonClicked("3");
		}

		public void Four()
		{
			OrdinaryButtonClicked("4");
		}

		public void Five()
		{
			OrdinaryButtonClicked("5");
		}

		public void Six()
		{
			OrdinaryButtonClicked("6");
		}

		public void Seven()
		{
			OrdinaryButtonClicked("7");
		}

		public void Eight()
		{
			OrdinaryButtonClicked("8");
		}

		public void Nine()
		{
			OrdinaryButtonClicked("9");
		}

		public void Zero()
		{
			OrdinaryButtonClicked("0");
		}

		public void Plus()
		{
			OrdinaryButtonClicked("+");
		}

		public void Minus()
		{
			OrdinaryButtonClicked("-");
		}

		public void Divide()
		{
			OrdinaryButtonClicked("/");
		}

		public void Multiply()
		{
			OrdinaryButtonClicked("*");
		}

		public void Dot()
		{
			OrdinaryButtonClicked(",");
		}

		public void Clear()
		{
			InputString = "0";
			Expression = string.Empty;
		}

		public void BackSpace()
		{
			if (InputString.Length == 1)
			{
				InputString = "0";
			}
			else
			{
				InputString = InputString.Remove(InputString.Length - 1);
			}

			Expression = string.Empty;
		}

		public void OpenBracket()
		{
			OrdinaryButtonClicked("(");
		}

		public void ClosingBracket()
		{
			OrdinaryButtonClicked(")");
		}

		public void Square()
		{
			OrdinaryButtonClicked("^2");
		}

		public void Pow_n()
		{
			OrdinaryButtonClicked("^");
		}

		public void OrdinaryButtonClicked(string digit)
		{
			if (!string.IsNullOrWhiteSpace(Expression))
			{
				Expression = string.Empty;
			}

			if(InputString == "0" && digit != ",")
			{
				InputString = string.Empty;
			}

			InputString += digit;
		}

		public void IsEqual()
		{
			Expression = string.Concat(InputString, "=");

			InputString = TryCalculate(InputString);
		}
		#endregion

		#region Connection to calculator
		/// <summary>
		/// Trying to calculate expression
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		private string TryCalculate(string expression)
		{
			var result = "0";

			try
			{
				result = Convert.ToString(_calculator.Calculate(expression));

				_log.Info(string.Format("New calculation: {0}{1}", Expression, result));
			}
			catch (ArgumentException e)
			{
				_log.Error(e.Message, e);
				ShowWarning(e.Message);
			}

			return result;
		}

		/// <summary>
		/// Shows a warning/error message to a user
		/// </summary>
		/// <param name="message">Warning/Error message</param>
		private void ShowWarning(string message)
		{
			var warning = new WarnViewModel(message);

			ActivateItem(warning);
			_manager.ShowWindow(warning);
		}
		#endregion
	}
}

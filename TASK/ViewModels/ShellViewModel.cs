using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Logging;
using TASK.Models;

namespace TASK.ViewModels
{
	public class ShellViewModel : Conductor<object>.Collection.OneActive
	{
		IWindowManager _manager;
		ICalculations _calculator;

		private static readonly log4net.ILog _log = LogHelper.GetLogger();

		public ShellViewModel(ICalculations calculator, IWindowManager manager)
		{
			_calculator = calculator;
			_manager = manager;
		}

		#region Properties
		private string _expression = string.Empty;
		private string _inputNumber = "0";

		public string Expression
		{
			get { return _expression; }
			set
			{
				_expression = value;
				NotifyOfPropertyChange(() => Expression);
			}
		}

		/// <summary>
		/// Property, which contains input/output data, user writes/gets
		/// </summary>
		public string InputNumber
		{
			get { return _inputNumber; }
			set
			{
				_inputNumber = value;
				NotifyOfPropertyChange(() => InputNumber);
			}
		}
		#endregion

		#region Technical fields
		private string _secondNumber = string.Empty;
		private string _operator = string.Empty;
		private bool _allowChangeOperators = false;
		#endregion

		#region Buttons' OnClic handlers
		public void One()
		{
			NumericButton("1");
		}

		public void Two()
		{
			NumericButton("2");
		}

		public void Three()
		{
			NumericButton("3");
		}

		public void Four()
		{
			NumericButton("4");
		}

		public void Five()
		{
			NumericButton("5");
		}

		public void Six()
		{
			NumericButton("6");
		}

		public void Seven()
		{
			NumericButton("7");
		}

		public void Eight()
		{
			NumericButton("8");
		}

		public void Nine()
		{
			NumericButton("9");
		}

		public void Zero()
		{
			NumericButton("0");
		}

		public void Plus()
		{
			ActionButton("+");
		}

		public void Minus()
		{
			ActionButton("-");
		}

		public void Divide()
		{
			ActionButton("/");
		}

		public void Multiply()
		{
			ActionButton("*");
		}

		public void Percent()
		{
			InputNumber = TryCalculate(_secondNumber, InputNumber, "%");
		}

		public void Factorial()
		{
			InputNumber = TryCalculate(InputNumber, "!");
		}

		public void IsEqual()
		{
			InputNumber = TryCalculate(_secondNumber, InputNumber, _operator);
			
			_secondNumber = string.Empty;
			_operator = string.Empty;
		}

		public void Dot()
		{
			if (InputNumber.Contains(","))
			{
				return;
			}

			InputNumber += ",";
		}

		public void Clear()
		{
			_secondNumber = string.Empty;
			InputNumber = "0";
			_operator = string.Empty;
		}

		public void BackSpace()
		{
			if (InputNumber.Length == 1)
			{
				InputNumber = "0";
			}
			else
			{
				InputNumber = InputNumber.Remove(InputNumber.Length - 1);
			}
		}
		#endregion

		#region Connection to calculator

		/// <summary>
		/// Trying to calculate unary operation
		/// </summary>
		/// <param name="a">Operand</param>
		/// <param name="function">Unary operator</param>
		/// <returns></returns>
		private string TryCalculate(string a, string function)
		{
			var result = "0";

			try
			{
				result = _calculator.Calculate(a, function);
			}
			catch (ArgumentException e)
			{
				ShowWarning(e.Message);
			}

			return result;
		}

		/// <summary>
		/// Trying to calculate binary operation
		/// </summary>
		/// <param name="a">1-st operand</param>
		/// <param name="b">2-nd operand</param>
		/// <param name="function">Binary operator</param>
		/// <returns></returns>
		private string TryCalculate(string a, string b, string function)
		{
			var result = "0";

			try
			{
				result = _calculator.Calculate(a, b, function);
			}
			catch (ArgumentException e)
			{
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

		#region Handling logic

		/// <summary>
		/// Handling numeric button's click
		/// </summary>
		/// <param name="digit"></param>
		public void NumericButton(string digit)
		{
			if (_allowChangeOperators || InputNumber == "0")
			{
				_allowChangeOperators = false;
				InputNumber = string.Empty;
			}
			InputNumber += digit;
		}

		/// <summary>
		/// Handling action button's click
		/// </summary>
		/// <param name="digit"></param>
		public void ActionButton(string func)
		{
			if (string.IsNullOrEmpty(_secondNumber))
			{
				_secondNumber = InputNumber;
				_operator = func;
			}
			else
			{
				if (_allowChangeOperators)
				{
					_operator = func;
				}
				else
				{
					InputNumber = TryCalculate(_secondNumber, InputNumber, _operator);

					_secondNumber = InputNumber;
					_operator = func;
				}
			}

			_allowChangeOperators = true;
		}
		#endregion

	}
}

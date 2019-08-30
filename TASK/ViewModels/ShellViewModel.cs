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
	public class ShellViewModel : Screen
	{
		ICalculations _calculator;
		private static readonly log4net.ILog _log = LogHelper.GetLogger();

		public ShellViewModel(ICalculations calculator)
		{
			_calculator = calculator;
		}

		private string _expression = string.Empty;

		public string Expression
		{
			get { return _expression; }
			set
			{
				_expression = value;
				NotifyOfPropertyChange(() => Expression);
			}
		}

		private string _inputNumber = "0";

		public string InputNumber
		{
			get { return _inputNumber; }
			set
			{
				_inputNumber = value;
				NotifyOfPropertyChange(() => InputNumber);
			}
		}

		private string _secondNumber = string.Empty;
		private string _operator = string.Empty;
		private bool _allowChangeOperators = false;

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
			InputNumber = _calculator.Calculate(_secondNumber, InputNumber, "%");
		}

		public void Factorial()
		{
			InputNumber = _calculator.Calculate(InputNumber, "!");
		}

		public void NumericButton(string digit)
		{
			if(_allowChangeOperators || InputNumber == "0")
			{
				_allowChangeOperators = false;
				InputNumber = string.Empty;
			}
			InputNumber += digit;
		}

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
					InputNumber = _calculator.Calculate(_secondNumber, InputNumber, _operator);
					
					_secondNumber = InputNumber;
					_operator = func;
				}
			}

			_allowChangeOperators = true;
		}

		public void Equals()
		{
			InputNumber = _calculator.Calculate(_secondNumber, InputNumber, _operator);
			
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
	}
}

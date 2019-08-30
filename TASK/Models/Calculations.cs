using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Logging;

namespace TASK.Models
{
	/*public class Calculations 
	{
		private List<double> _operands = new List<double>();
		private List<string> _operators = new List<string>();

		public string Calculate(string expression, out bool success)
		{
			var result = string.Empty;

			string operand = string.Empty;

			for (int i = 0; i < expression.Length; i++)
			{
				if(_priorities.Keys.Contains(Convert.ToString(expression[i])))
				{
					if (operand != string.Empty)
					{
						AddOperand(operand);
					}

					AddOperator(Convert.ToString(expression[i]));

					operand = string.Empty;
				}
				else
				{
					operand += expression[i];
				}
			}

			ClosingBracketMet();

			result = "Error";
			success = false;

			if (_operands.Count == 1)
			{
				if (_operators.Count == 1 && _operators.Last() == "-")
				{
					result = "-" + _operands.Last();
					success = true;
				}

				if(_operators.Count == 0)
				{
					result = Convert.ToString(_operands.Last());
					success = true;
				}
			}

			return result;
		}
		
		/// <summary>
		/// Ads new operand to an expression
		/// </summary>
		/// <param name="operand"></param>
		/// <returns>Success sign</returns>
		public bool AddOperand(string operand)
		{
			var success = true;

			try
			{
				_operands.Add(Convert.ToDouble(operand));
			}
			catch (FormatException) { success = false; }
			catch (OverflowException) { success = false; }

			return success;
		}

		private Dictionary<string, int> _priorities
			= new Dictionary<string, int>()
				{
					{ "+", 1 },
					{ "-", 1 },
					{ "*", 2 },
					{ "/", 2 },
					{ "(", 3 },
					{ ")", 0 }
				};

		private Dictionary<string, Func<double, double, double>> _functions
			= new Dictionary<string, Func<double, double, double>>()
				{
					{ "+", (a, b) => { return a + b; } },
					{ "-", (a, b) => { return a - b; } },
					{ "*", (a, b) => { return a * b; } },
					{ "/", (a, b) => { return a / b; } }
				};

		public bool AddOperator(string _operator)
		{
			if (!_priorities.Keys.Contains(_operator))
			{
				return false;
			}

			if ((_operators.Count == 0 && _operator != ")") || _operator == "(")
			{
				_operators.Add(_operator);
				return true;
			}

			if (_operator == ")")
			{
				return ClosingBracketMet();
			}

			if (_priorities[_operator] > _priorities[_operators.Last()])
			{
				_operators.Add(_operator);
				return true;
			}

			double a, b;
			
			if (!TakeTwoLastOperands(out a, out b))
			{
				return false;
			}

			_operands.Add(_functions[_operators.Last()].Invoke(a, b));

			_operators[_operators.Count - 1] = _operator;


			return true;
		}

		private bool ClosingBracketMet()
		{
			double a, b;

			while(_operators.Last() != "(" || _operators.Any())
			{
				if (!TakeTwoLastOperands(out a, out b))
				{
					return  false;
				}

				_operands.Add(_functions[_operators.Last()].Invoke(a, b));

				_operators.RemoveAt(_operators.Count - 1);
			}

			if (_operators.Last() == "(")
			{
				_operators.RemoveAt(_operators.Count - 1);
			}

			return true;
		}

		private bool TakeTwoLastOperands(out double a, out double b)
		{
			if (_operands.Count < 2)
			{
				a = b = 0;
				return false;
			}

			b = _operands[_operands.Count - 1];
			a = _operands[_operands.Count - 2];
			_operands.RemoveRange(_operands.Count - 2, 2);

			return true;
		}
	}*/

	public class Calc : ICalculations
	{
		private static readonly log4net.ILog _log = LogHelper.GetLogger();

		public string Calculate(string a, string b, string function)
		{
			if(!_binaryFunctions.Keys.Contains(function))
			{
				return "0";
			}

			double result = 0;

			try
			{
				result = _binaryFunctions[function].Invoke(Convert.ToDouble(a), Convert.ToDouble(b));
				_log.Info(string.Format("New calculation: {0} {1} {2} = {3}", a, function, b, result));
			}
			catch (DivideByZeroException e)
			{
				_log.Error(e.Message, e);
			}

			return Convert.ToString(result);
		}

		public string Calculate(string a, string function)
		{
			if (!_unaryFunctions.Keys.Contains(function))
			{
				return "0";
			}

			var result = _unaryFunctions[function].Invoke(Convert.ToDouble(a));

			_log.Info(string.Format("New calculation of unary operator: {0}{1} = {2}", a, function, result));
			return Convert.ToString(result);
		}

		private Dictionary<string, Func<double, double, double>> _binaryFunctions
			= new Dictionary<string, Func<double, double, double>>()
				{
					{ "+", (a, b) => { return a + b; } },
					{ "-", (a, b) => { return a - b; } },
					{ "*", (a, b) => { return a * b; } },
					{ "/", (a, b) => 
						{
							if(b == 0)
								throw new DivideByZeroException("Divizion by zero is undefined.");
							else
								return a / b;
						}
					},
					{ "%", (a, b) => { return (a / 100) * b; } }
				};

		private Dictionary<string, Func<double, double>> _unaryFunctions
			= new Dictionary<string, Func<double, double>>()
				{
					{ "!", (a) => 
						{
							double num = 1;

							for (int i = 2; i <= a; ++i)
								num *= i;

							return num;
						}
					}
				};
	}
}

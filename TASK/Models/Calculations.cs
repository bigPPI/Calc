using System;
using System.Collections.Generic;
using System.Linq;
using TASK.Logging;

namespace TASK.Models
{
	public class Calculations : ICalculations
	{
		private static readonly log4net.ILog _log = LogHelper.GetLogger();

		/// <summary>
		/// Calculates a result of binary operation. Precision: 5 decimal places
		/// </summary>
		/// <param name="a">First operand</param>
		/// <param name="b">Second operand</param>
		/// <param name="function">Binary operator</param>
		/// <returns></returns>
		/// <exception cref="">Throws ArgumentException</exception>
		public string Calculate(string a, string b, string function)
		{
			double result = 0;

			try
			{
				if (!_binaryFunctions.Keys.Contains(function))
				{
					throw new NotImplementedException("This operator is not implemented");
				}

				result = _binaryFunctions[function].Invoke(Convert.ToDouble(a), Convert.ToDouble(b));
				_log.Info(string.Format("New calculation: {0} {1} {2} = {3}", a, function, b, result));
			}
			catch (Exception e)
			{
				_log.Error(e.Message, e);
				throw new ArgumentException(e.Message);
			}

			Math.Round(result, 5);

			return Convert.ToString(result);
		}

		/// <summary>
		/// Calculates a result of binary operation. Precision: 5 decimal places
		/// </summary>
		/// <param name="a">First operand</param>
		/// <param name="b">Second operand</param>
		/// <param name="function">Binary operator</param>
		/// <returns></returns>
		/// <exception cref="">Throws ArgumentException</exception>
		public string Calculate(string a, string function)
		{
			double result = 0;

			try
			{

				if (!_unaryFunctions.Keys.Contains(function))
				{
					throw new NotImplementedException("This operator is not implemented");
				}

				result = _unaryFunctions[function].Invoke(Convert.ToDouble(a));
				_log.Info(string.Format("New calculation of unary operator: {0}{1} = {2}", a, function, result));
			}
			catch (Exception e)
			{
				_log.Error(e.Message, e);
				throw new ArgumentException(e.Message);
			}

			Math.Round(result, 5);

			return Convert.ToString(result);
		}

		/// <summary>
		/// Dictionary of Binary operators
		/// </summary>
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

		/// <summary>
		/// Dictionary of Unary operators
		/// </summary>
		private Dictionary<string, Func<double, double>> _unaryFunctions
			= new Dictionary<string, Func<double, double>>()
				{
					{ "!", (a) => 
						{
							if(a<0)
								throw new ArgumentOutOfRangeException("Factorial of negative numbers is undefined");

							double num = 1;

							for (int i = 2; i <= a; ++i)
								num *= i;

							return num;
						}
					}
				};
	}
}

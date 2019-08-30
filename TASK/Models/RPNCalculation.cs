//Original code - habr.com
//Small edits and adaptation - Pavlo Pasieka
using System;
using System.Collections.Generic;

namespace TASK.Models
{
	public class RPNCalculation : IRPNCalculation
	{
		/// <summary>
		/// Калькуляция выражения переданого в виде строки
		/// </summary>
		/// <param name="input">Строка с выражением</param>
		/// <returns></returns>
		/// <exception cref="">Throws ArgumentException</exception>
		public double Calculate(string input)
		{
			double result = 0;

			try
			{
				string output = GetExpression(input);
				result = Counting(output);
			}
			catch(Exception e)
			{
				throw new ArgumentException(e.Message);
			}

			return result;
		}

		/// <summary>
		/// Gives expression in Reverse Polish notation
		/// </summary>
		/// <param name="input">Expression in normal notation</param>
		/// <returns></returns>
		private string GetExpression(string input)
		{
			string output = string.Empty; //Строка для хранения выражения
			Stack<char> operStack = new Stack<char>(); //Стек для хранения операторов

			for (int i = 0; i < input.Length; i++) //Для каждого символа в входной строке
			{
				//Разделители пропускаем
				if (IsDelimeter(input[i]))
					continue; //Переходим к следующему символу

				//Если символ - цифра, то считываем все число
				if (Char.IsDigit(input[i])) //Если цифра
				{
					//Читаем до разделителя или оператора, что бы получить число
					while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
					{
						output += input[i]; //Добавляем каждую цифру числа к нашей строке
						i++; //Переходим к следующему символу

						if (i == input.Length) break; //Если символ - последний, то выходим из цикла
					}

					output += " "; //Дописываем после числа пробел в строку с выражением
					i--; //Возвращаемся на один символ назад, к символу перед разделителем
				}

				//Если символ - оператор
				if (IsOperator(input[i])) //Если оператор
				{
					if (input[i] == '(') //Если символ - открывающая скобка
						operStack.Push(input[i]); //Записываем её в стек
					else if (input[i] == ')') //Если символ - закрывающая скобка
					{
						//Выписываем все операторы до открывающей скобки в строку
						char s = operStack.Pop();

						while (s != '(')
						{
							output = string.Concat(output, s.ToString(), " ");
							s = operStack.Pop();
						}
					}
					else //Если любой другой оператор
					{
						if (operStack.Count > 0) //Если в стеке есть элементы
							if (GetPriority(input[i]) <= GetPriority(operStack.Peek())) //И если приоритет нашего оператора меньше или равен приоритету оператора на вершине стека
								output = string.Concat(output, operStack.Pop().ToString(), " "); //То добавляем последний оператор из стека в строку с выражением

						operStack.Push(char.Parse(input[i].ToString())); //Если стек пуст, или же приоритет оператора выше - добавляем операторов на вершину стека

					}
				}
			}

			//Когда прошли по всем символам, выкидываем из стека все оставшиеся там операторы в строку
			while (operStack.Count > 0)
				output = string.Concat(output, operStack.Pop(), " ");

			return output; //Возвращаем выражение в постфиксной записи
		}

		/// <summary>
		/// Calculating RPN-expression
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		private double Counting(string input)
		{
			double result = 0; //Результат
			Stack<double> temp = new Stack<double>(); //Dhtvtyysq стек для решения

			for (int i = 0; i < input.Length; i++) //Для каждого символа в строке
			{
				//Если символ - цифра, то читаем все число и записываем на вершину стека
				if (Char.IsDigit(input[i]))
				{
					string a = string.Empty;

					while (!IsDelimeter(input[i]) && !IsOperator(input[i])) //Пока не разделитель
					{
						a += input[i]; //Добавляем
						i++;
						if (i == input.Length) break;
					}
					temp.Push(double.Parse(a)); //Записываем в стек
					i--;
				}
				else if (IsOperator(input[i])) //Если символ - оператор
				{
					//Берем два последних значения из стека
					double a = temp.Pop();
					double b = 0;

					if (temp.Count != 0)
					{
						b = temp.Pop();
					}

					switch (input[i]) //И производим над ними действие, согласно оператору
					{
						case '+': result = b + a; break;
						case '-': result = b - a; break;
						case '*': result = b * a; break;
						case '/':
							if (a == 0)
							{
								throw new ArgumentException("Division by zero is undefined");
							}

							result = b / a;
							break;
						case '^': result = double.Parse(Math.Pow(double.Parse(b.ToString()), double.Parse(a.ToString())).ToString()); break;
					}
					temp.Push(result); //Результат вычисления записываем обратно в стек
				}
			}
			return temp.Peek(); //Забираем результат всех вычислений из стека и возвращаем его
		}

		/// <summary>
		/// Looks if a symbol is a Delimiter
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		private bool IsDelimeter(char c)
		{
			if ((" =".IndexOf(c) != -1))
				return true;
			return false;
		}

		/// <summary>
		/// Looks if a symbol is a Operator
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		private bool IsOperator(char с)
		{
			if (("+-/*^()".IndexOf(с) != -1))
				return true;
			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="c"></param>
		/// <returns>Priority of an operator</returns>
		private byte GetPriority(char s)
		{
			switch (s)
			{
				case '(': return 0;
				case ')': return 1;
				case '+': return 2;
				case '-': return 3;
				case '*': return 4;
				case '/': return 4;
				case '^': return 5;
				default: return 6;
			}
		}
	}
}

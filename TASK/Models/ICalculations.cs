namespace TASK.Models
{
	public interface ICalculations
	{
		/// <summary>
		/// Calculates a result of binary operation
		/// </summary>
		/// <param name="a">First operand</param>
		/// <param name="b">Second operand</param>
		/// <param name="function">Binary operator</param>
		/// <returns></returns>
		string Calculate(string a, string b, string function);

		/// <summary>
		/// Calculates a result of unary operation
		/// </summary>
		/// <param name="a">First operand</param>
		/// <param name="b">Second operand</param>
		/// <param name="function">Binary operator</param>
		/// <returns></returns>
		string Calculate(string a, string function);
	}
}
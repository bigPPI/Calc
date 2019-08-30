namespace TASK.Models
{
	public interface IRPNCalculation
	{
		/// <summary>
		/// Калькуляция выражения переданого в виде строки
		/// </summary>
		/// <param name="input">Строка с выражением</param>
		/// <returns></returns>
		double Calculate(string input);
	}
}
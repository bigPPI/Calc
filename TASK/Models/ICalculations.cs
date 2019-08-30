namespace TASK.Models
{
	public interface ICalculations
	{
		string Calculate(string a, string b, string function);
		string Calculate(string a, string function);
	}
}
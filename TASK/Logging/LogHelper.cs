using System.Runtime.CompilerServices;

namespace TASK.Logging
{
	public class LogHelper
	{
		/// <summary>
		/// Retrives or creates a logger, named after caller's file runtime path
		/// </summary>
		/// <param name="filename">Caller's file runtime path</param>
		/// <returns></returns>
		public static log4net.ILog GetLogger([CallerFilePath]string filename = "")
		{
			return log4net.LogManager.GetLogger(filename);
		}
	}
}

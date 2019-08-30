using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TASK.ViewModels;

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

using Caliburn.Micro;
using log4net.Appender;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASK.Logging;

namespace TASK.ViewModels
{
	public class LoggerViewModel : Screen
	{
		private string _logs;

		public string Logs
		{
			get { return _logs; }
			set
			{
				_logs = value;
				NotifyOfPropertyChange(() => Logs);
			}
		}

		private static readonly log4net.ILog _log = LogHelper.GetLogger();

		public LoggerViewModel()
		{
			_log.Info("It works!!!");
		}
	}
}

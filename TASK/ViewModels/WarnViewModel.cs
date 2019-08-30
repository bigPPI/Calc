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
	public class WarnViewModel : Screen
	{
		private static readonly log4net.ILog _log = LogHelper.GetLogger();

		private string _text;

		/// <summary>
		/// Property, which contains Error/Warning message 
		/// </summary>
		public string Text
		{
			get { return _text; }
			set
			{
				_text = value;
				NotifyOfPropertyChange(() => Text);
			}
		}

		public WarnViewModel(string message)
		{
			Text = message;
			_log.Info("WarnView is shown");
		}

		public void Ok()
		{
			this.TryClose();
			_log.Info("WarnView is closed");
		}
	}
}

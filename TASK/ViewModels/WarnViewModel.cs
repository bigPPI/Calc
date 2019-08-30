using Caliburn.Micro;
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
			_log.Debug("WarnView is shown");
		}

		public void Ok()
		{
			this.TryClose();
			_log.Debug("WarnView is closed");
		}
	}
}

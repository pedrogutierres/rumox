using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Core.Infra.Log.ELK.Models
{
	internal class LogEntry
	{
		public virtual string Content { get; set; }
		public virtual string Context { get; set; }
		public virtual long ElapsedMilliseconds { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual LogLevel LogLevel { get; set; }
		public virtual IDictionary<string, object> Tags { get; set; }
		public virtual string ProjectKey { get; set; }
	}
}

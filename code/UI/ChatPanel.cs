using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.UI
{
	[UseTemplate]
	public class ChatPanel : Panel
	{
		public static ChatPanel Instance;

		// @ref
		public Panel mainChat { get; set; }

		// @ref
		public TextEntry textEntry { get; set; }


		public ChatPanel()
		{
			AddChild<Slider2D>().AddClass("slider");
			Instance = this;
		}


	}
}

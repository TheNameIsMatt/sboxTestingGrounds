using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.UI
{
	//Attaches html and css automatically with this attribute
	[UseTemplate]
	public class GameBar : Panel
	{
		//The names of these properties MUST MATCH the names of the @ref classes in the HTML file
		public Label Header { get; set; }
		public Button btn { get; set; }
		public GameBar()
		{
			Log.Info( "Setting Template" );
		}

		public override void Tick()
		{
			base.Tick();
			btn.Text = "BEWTON";
			Header.Text = "Poop";
		}
	}
}

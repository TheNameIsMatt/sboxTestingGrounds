using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;
using Sandbox.UI;

namespace Sandbox.UI
{
	[Library]
	public partial class TanksHUD : HudEntity<RootPanel>
	{
	
		public TanksHUD() { 
			if ( !IsClient )
			{
				return;
			}
			Log.Info( "HUD Intialised" );

			//RootPanel.SetTemplate( "/UI/GameBar.html" );

			RootPanel.AddChild<GameBar>();
			
			
			
		}
	}
}

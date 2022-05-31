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

			//If [UseTemplate] Attribute is applied, you will not need to use the setTemplate function on all these child panels as they will automatically be bound
			RootPanel.AddChild<GameBar>();
			
		}
	}
}

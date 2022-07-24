using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Tanks
{
	[Library]
	[UseTemplate]
	public partial class TanksHUD : HudEntity<RootPanel>
	{
	
		public TanksHUD() { 
			if ( !IsClient )
			{
				return;
			}
			Log.Info( "HUD Intialised" );


			//If [UseTemplate] Attribute is applied, you will not need to use the setTemplate function in all these child panels as they will automatically be bound

			// RootPanel.AddChild<GameBar>(); Old Bar
			RootPanel.AddChild<BattleBar>();


			//RootPanel.AddChild<ChatPanel>();
				
		}

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.UI
{
	[UseTemplate]
	public class BattleBar : Panel
 	{
		
		private TimeSince timeSincePointerActivated;
		public Button FireButton { get; set; }
		public BattleBar()
		{

		}
		public override void Tick()
		{
			if ( Input.Pressed( InputButton.Flashlight ) && timeSincePointerActivated > 0.2f )
			{
				TogglePointer();
			}
			base.Tick();
		}

		public void FireAmmo()
		{
			Log.Info( "Ammo fired" );
		}
		public void TogglePointer()
		{
			//Because I imported my Utilities class in my GameBar.scss, I can use the enablePointer class I set
			timeSincePointerActivated = 0f;

			if ( HasClass( "enablePointer" ) )
			{
				RemoveClass( "enablePointer" );
			}
			else
			{
				AddClass( "enablePointer" );
			}
		}
	}
}

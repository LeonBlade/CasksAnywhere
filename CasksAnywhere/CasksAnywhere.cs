using System;
using System.Collections.Generic;
using StardewValley;
using StardewValley.Objects;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CasksAnywhere
{
	public class CasksAnywhere : Mod
	{
		public static EventHandler<ReleaseCaskEventArgs> ReleaseCask;
		private List<Vector2> casks = new List<Vector2>();

		public override void Entry(IModHelper helper)
		{
			GameEvents.OneSecondTick += GameEvents_OneSecondTick;
			ReleaseCask += CasksAnywhere_ReleaseCask;
			ControlEvents.MouseChanged += ControlEvents_MouseChanged;
			ControlEvents.ControllerButtonPressed += ControlEvents_ControllerButtonPressed;
		}

		void GameEvents_OneSecondTick(object sender, EventArgs e)
		{
			if (casks.Count == 0)
				return;

			foreach (var cask in casks)
			{
				if (!Game1.currentLocation.objects.ContainsKey(cask) || !(Game1.currentLocation.objects[cask] is HijackCask))
					continue;
				Game1.currentLocation.objects[cask] = (Game1.currentLocation.objects[cask] as HijackCask).CaskBack();

			}

			casks.Clear();
		}

		void ControlEvents_MouseChanged(object sender, EventArgsMouseStateChanged e)
		{
			if (e.NewState.LeftButton == ButtonState.Pressed)
				hijackCask();
		}

		void ControlEvents_ControllerButtonPressed(object sender, EventArgsControllerButtonPressed e)
		{
			if (e.ButtonPressed == Buttons.A)
				hijackCask();
		}

		void CasksAnywhere_ReleaseCask(object sender, ReleaseCaskEventArgs e)
		{
			casks.Add(e.location);
		}

		private void hijackCask()
		{
			// get the tile location
			var tileLocation = new Vector2(
				(Game1.viewport.X + Game1.getOldMouseX()) / Game1.tileSize,
				(Game1.viewport.Y + Game1.getOldMouseY()) / Game1.tileSize
			);

			// if our location is valid and we have an object in this location
			if (Game1.currentLocation != null && Game1.currentLocation.objects.ContainsKey(tileLocation))
			{
				// the item in question
				Item item = Game1.currentLocation.objects[tileLocation];
				// if this is a cask then hijack it!
				if (item != null && item is Cask)
					Game1.currentLocation.objects[tileLocation] = new HijackCask(item as Cask);
			}
		}
	}
}

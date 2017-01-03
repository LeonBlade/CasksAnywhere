using StardewValley;
using StardewValley.Objects;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using Microsoft.Xna.Framework;
using StardewModdingAPI.Inheritance;

namespace CasksAnywhere
{
	public class CasksAnywhere : Mod
	{
		public override void Entry(IModHelper helper)
		{
			// add event listener for when inventory changes
			PlayerEvents.InventoryChanged += PlayerEvents_InventoryChanged;
		}

		void PlayerEvents_InventoryChanged(object sender, EventArgsInventoryChanged e)
		{
			// loop through the changes that were removed
			foreach (var change in e.Removed)
				if (change.ChangeType == ChangeType.Removed && change.Item.Name == "Cask")
					hijackCask();
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
					Game1.currentLocation.objects[tileLocation] = new HijackCask(item as Cask, tileLocation);
			}
		}
	}
}

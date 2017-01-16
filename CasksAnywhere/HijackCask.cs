using System;
using StardewValley;
using StardewValley.Objects;
using Microsoft.Xna.Framework;

namespace CasksAnywhere
{
	public class HijackCask : Cask
	{
		public HijackCask() { }

		public HijackCask(Cask b) : base(b.tileLocation)
		{
			this.heldObject = b.heldObject;
			this.agingRate = b.agingRate;
			this.daysToMature = b.daysToMature;
			this.minutesUntilReady = b.minutesUntilReady;
		}

		public override bool performObjectDropInAction(StardewValley.Object dropIn, bool probe, Farmer who)
		{
			if (dropIn != null && dropIn.bigCraftable)
				return false;

			if (this.heldObject != null)
				return false;

			if (this.quality >= 4)
				return false;
			
			bool flag = false;
			float num = 1f;
			int psi = dropIn.parentSheetIndex;

			switch (psi)
			{
				case 346:
					flag = true;
					num = 2f;
					break;
				case 348:
					flag = true;
					num = 1f;
					break;
				default:
					switch (psi - 424)
					{
						case 0:
							flag = true;
							num = 4f;
							break;
						case 2:
							flag = true;
							num = 4f;
							break;
						default:
							if (psi != 303)
							{
								if (psi == 459)
								{
									flag = true;
									num = 2f;
									break;
								}
								break;
							}
							flag = true;
							num = 1.66f;
							break;
					}
					break;
			}

			if (!flag)
				return false;
			
			this.heldObject = dropIn.getOne() as StardewValley.Object;

			if (!probe)
			{
				this.agingRate = num;
				this.daysToMature = 56f;
				this.minutesUntilReady = 999999;
				if (this.heldObject.quality == 1)
					this.daysToMature = 42f;
				else if (this.heldObject.quality == 2)
					this.daysToMature = 28f;
				else if (this.heldObject.quality == 4)
				{
					this.daysToMature = 0.0f;
					this.minutesUntilReady = 1;
				}
				Game1.playSound("Ship");
				Game1.playSound("bubbles");
				who.currentLocation.temporarySprites.Add(new TemporaryAnimatedSprite(Game1.animations, new Rectangle(256, 1856, 64, 128), 80f, 6, 999999, this.tileLocation * (float)Game1.tileSize + new Vector2(0.0f, (float)(-Game1.tileSize * 2)), false, false, (float)(((double)this.tileLocation.Y + 1.0) * (double)Game1.tileSize / 10000.0 + 9.99999974737875E-05), 0.0f, Color.Yellow * 0.75f, 1f, 0.0f, 0.0f, 0.0f, false)
				{
					alphaFade = 0.005f
				});
			}

			return true;
		}
	}
}

using Terraria;
using Terraria.Audio;
using TerrariaOverhaul.Core.ItemOverhauls;

namespace TerrariaOverhaulAddon.Common
{
	public abstract class GunOverhaul : ItemOverhaul
	{
		public SoundStyle? ShootSound { get; set; }

		public override bool CanUseItem(Item item, Player player)
		{
			if (ShootSound != null) item.UseSound = ShootSound;
			return base.CanUseItem(item, player);
		}
	}
}

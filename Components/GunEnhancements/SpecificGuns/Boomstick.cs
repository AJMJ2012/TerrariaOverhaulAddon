using Terraria;
using Terraria.ID;

namespace TerrariaOverhaulAddon.Components.GunEnhancements.SpecificGuns;
internal class Boomstick : BreakShotgun
{
	public override bool ShouldApplyItemOverhaul(Item item)
		=> item.type == ItemID.Boomstick;

	public override void SetDefaults(Item item)
	{
		base.SetDefaults(item);
		CasingCount = 2;
	}
}
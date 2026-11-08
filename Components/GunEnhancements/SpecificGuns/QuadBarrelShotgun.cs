using Terraria;
using Terraria.ID;
using TerrariaOverhaulAddon.Common;

namespace TerrariaOverhaulAddon.Components.GunEnhancements.SpecificGuns;
internal class QuadBarrelShotgun : BreakShotgun
{
	public override bool ShouldApplyItemOverhaul(Item item)
		=> item.type == ItemID.QuadBarrelShotgun;

	public override void SetDefaults(Item item)
	{
		base.SetDefaults(item);
		CasingCount = 4;
		CloseSound = SoundAssets.BreakShotgunCloseLongSound;
	}
}
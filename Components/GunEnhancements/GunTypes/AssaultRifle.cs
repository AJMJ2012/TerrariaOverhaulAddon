using Terraria;

namespace TerrariaOverhaulAddon.Components.GunEnhancements;
internal class AssaultRifle : TerrariaOverhaul.Common.Guns.AssaultRifle
{
	public override bool ShouldApplyItemOverhaul(Item item)
		=> GunChecks.IsAssaultRifle(item);
}

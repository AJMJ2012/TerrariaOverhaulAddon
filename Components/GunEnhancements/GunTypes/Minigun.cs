using Terraria;

namespace TerrariaOverhaulAddon.Components.GunEnhancements;
internal class Minigun : TerrariaOverhaul.Common.Guns.Minigun
{
	public override bool ShouldApplyItemOverhaul(Item item)
		=> GunChecks.IsMinigun(item);
}

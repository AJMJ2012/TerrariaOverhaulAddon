using Terraria;

namespace TerrariaOverhaulAddon.Components.GunEnhancements;
// Initially unused
// Since there is more than 1 revolver, this isn't under Specific Guns
internal class Revolver : TerrariaOverhaul.Common.Guns.Revolver
{
	public override bool ShouldApplyItemOverhaul(Item item)
		=> GunChecks.IsRevolver(item);
}

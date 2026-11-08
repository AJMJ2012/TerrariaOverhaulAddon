using Terraria.DataStructures;
using Terraria.ModLoader;
using TerrariaOverhaul.Common.Dodgerolls;

namespace TerrariaOverhaulAddon.Components;

// PlayerDodgerolls is sealed. Bruh.
internal class DodgeRollHidesWeapon : ModPlayer
{
	public override void HideDrawLayers(PlayerDrawSet drawInfo)
	{
		if (Player.TryGetModPlayer<PlayerDodgerolls>(out var dodgeRollPlayer) && dodgeRollPlayer.IsDodging)
			PlayerDrawLayers.HeldItem.Hide();
	}
}
using System;
using System.Reflection;
using Terraria.ModLoader;
using TerrariaOverhaul.Common.Dodgerolls;
using TerrariaOverhaul.Common.EntityEffects;

namespace TerrariaOverhaulAddon.Compatibility;

[JITWhenModsEnabled("MetroidMod")]
internal class MetroidMod_FixSpinJump : ModSystem
{
	public override void Load()
	{
		if (ModLoader.HasMod("MetroidMod"))
		{
			MonoModHooks.Add(typeof(PlayerBodyRotation).GetMethod("PostUpdate", BindingFlags.Public | BindingFlags.Instance), void (Action<PlayerBodyRotation> orig, PlayerBodyRotation self) =>
			{
				orig(self);
				if (self.Player.TryGetModPlayer<PlayerDodgerolls>(out var dodgeRollPlayer) && dodgeRollPlayer.IsDodging)
					return;
				if (self.Player.TryGetModPlayer<MetroidMod.Common.Players.MPlayer>(out var metroidPlayer))
					self.Player.fullRotation += metroidPlayer.rotation;
				return;
			});
		}
	}
}
using System;
using System.ComponentModel;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using TerrariaOverhaul.Common.EntityEffects;

namespace TerrariaOverhaulAddon.Compatibility;

// New versions of WeaponOut and WeaponOut Lite seem to have Terraria Overhaul compatibility, however the old version does not.
// This is here just in case for the old version.

[JITWhenModsEnabled("WeaponOut", "WeaponOutLite")]
internal class WeaponOut_FixHoldOut : ModSystem
{
	bool overhaulHandled = false;
	static WeaponOut_FixHoldOut_Config Config => WeaponOut_FixHoldOut_Config.Instance;
	public override void Load()
	{
		if (Main.netMode == NetmodeID.Server) return;
		if (ModLoader.TryGetMod("WeaponOut", out var woc) && woc.Version == new Version(1, 4, 4, 9)) // I may need a better than a version check here to determine if it's the old version or not.
		{
			HookWeaponOutLiteClassic(woc);
		}
		else if (ModLoader.TryGetMod("WeaponOutLite", out var wol))
		{
			HookWeaponOutLite(wol);
		}
	}

	public void HookWeaponOutLiteClassic(Mod mod)
	{
		MonoModHooks.Add(typeof(PlayerHoldOutAnimation).GetMethod("ShouldForceUseAnim", BindingFlags.NonPublic | BindingFlags.Static), bool (Func<Player, Item, bool> orig, Player player, Item item) =>
		{
			if (!Config.Enabled)
			{
				return orig(player, item);
			}
			overhaulHandled = false;
			// Held Projectile Weapon
			if (player.heldProj >= 0 && item.noUseGraphic)
				return false;
			bool isHeld = orig(player, item) && PlayerHoldOutAnimation.AlwaysShowAimableWeapons;
			// Prefer WeaponOut's style if available
			if (!Config.PreferOverhaulHoldOut && isHeld && !player.pulley && player.grapCount <= 0)
			{
				player.TryGetModPlayer<WeaponOut.WOPlayer>(out var woPlayer);
				bool canDraw = woPlayer.Show && WeaponOut.DrawTool.DefaultVisibility(new PlayerDrawSet() { drawPlayer = player, heldItem = item });
				if (canDraw)
					return false;
			}
			return overhaulHandled = isHeld;
		});

		MonoModHooks.Add(typeof(WeaponOut.DrawTool).GetMethod("DefaultVisibility", BindingFlags.Public | BindingFlags.Static), bool (Func<PlayerDrawSet, bool> orig, PlayerDrawSet drawInfo) =>
		{
			if (!Config.Enabled)
			{
				return orig(drawInfo);
			}
			// Handled by Overhaul
			// Held Projectile Weapon - Can't do much about that
			if (overhaulHandled || (drawInfo.drawPlayer?.heldProj >= 0 && drawInfo.heldItem?.noUseGraphic == true))
				return false;
			return orig(drawInfo);
		});
	}

	public void HookWeaponOutLite(Mod mod)
	{
		MonoModHooks.Add(typeof(PlayerHoldOutAnimation).GetMethod("ShouldForceUseAnim", BindingFlags.NonPublic | BindingFlags.Static), bool (Func<Player, Item, bool> orig, Player player, Item item) =>
		{
			if (!Config.Enabled)
			{
				return orig(player, item);
			}
			overhaulHandled = false;
			// Held Projectile Weapon
			if (player.heldProj >= 0 && item.noUseGraphic)
				return false;
			bool isHeld = orig(player, item) && PlayerHoldOutAnimation.AlwaysShowAimableWeapons;
			if (!Config.PreferOverhaulHoldOut && isHeld && !player.pulley && player.grapCount <= 0)
			{
				// Prefer WeaponOut's style if available
				player.TryGetModPlayer<WeaponOutLite.Common.Players.WeaponOutPlayerRenderer>(out var woPlayer);
				woPlayer.showHeldItemThisFrame = true;
				bool canDraw = woPlayer.showHeldItemThisFrame && woPlayer.IsShowingHeldItem && WeaponOutLite.Common.WeaponOutLayerRenderer.CanDrawBaseDrawData(new PlayerDrawSet() { drawPlayer = player, heldItem = item }, out _, out _, out _, out _, out _);
				if (canDraw)
					return false;
			}
			return overhaulHandled = isHeld;
		});


		MonoModHooks.Add(typeof(WeaponOutLite.Common.Players.WeaponOutPlayerRenderer).GetMethod("ResetEffects", BindingFlags.Public | BindingFlags.Instance), void (Action<WeaponOutLite.Common.Players.WeaponOutPlayerRenderer> orig, WeaponOutLite.Common.Players.WeaponOutPlayerRenderer self) =>
		{
			orig(self);
			if (!Config.Enabled)
			{
				return;
			}
			self.showHeldItemThisFrame = true;
			// Handled by Overhaul
			// Held Projectile Weapon - Can't do much about that
			if (overhaulHandled || (self.Player.heldProj >= 0 && self.Player.HeldItem.noUseGraphic))
				mod.Call("HidePlayerHeldItem", self.Player.whoAmI);
			return;
		});
	}
}

public class WeaponOut_FixHoldOut_Config : ModConfig
{
	public override ConfigScope Mode => ConfigScope.ClientSide;
	public static WeaponOut_FixHoldOut_Config Instance;

	[DefaultValue(true)]
	public bool Enabled = true;

	[DefaultValue(false)]
	public bool PreferOverhaulHoldOut = false;
}
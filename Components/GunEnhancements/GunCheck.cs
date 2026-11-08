using Terraria;
using Terraria.ID;
using TerrariaOverhaul.Common.Tags;
using TerrariaOverhaulAddon.Utilities;

namespace TerrariaOverhaulAddon.Components.GunEnhancements;

public static class GunChecks
{

	const int InsanelyFast = 8;
	const int VeryFast = 20;
	const int Fast = 25;
	const int Average = 30;
	const int Slow = 35;
	const int VerySlow = 45;
	const int ExtremelySlow = 55;

	public static bool IsShootable(Item item)
	{
		return item.shoot > 0 && item.useStyle > 0;
	}



	public static bool IsGun(Item item)
	{
		if (item.channel)
		{
			Projectile projectile = new Projectile();
			projectile.SetDefaults(item.shoot);
			if (projectile.aiStyle == ProjAIStyleID.HeldProjectile)
			{
				return false;
			}
			if (item.UseSound == null)
			{
				return false;
			}
			if (item.noUseGraphic)
			{
				return false;
			}
		}
		return IsShootable(item) && item.useAmmo == AmmoID.Bullet && item.type != ItemID.Xenopopper;
	}

	public static bool IsAssaultRifle(Item item)
	{
		return IsGun(item) && AssaultRifleCheck(item);
	}
	public static bool IsHandgun(Item item)
	{
		return IsGun(item) && HandgunCheck(item);
	}
	public static bool IsBoltRifle(Item item)
	{
		return IsGun(item) && BoltRifleCheck(item);
	}
	public static bool IsShotgun(Item item)
	{
		return IsGun(item) && ShotgunCheck(item);
	}
	public static bool IsMinigun(Item item)
	{
		return IsGun(item) && MinigunCheck(item);
	}
	public static bool IsRevolver(Item item)
	{
		return IsGun(item) && RevolverCheck(item);
	}
	public static bool IsBreakShotgun(Item item)
	{
		return false; // For now as I can't seem to prioritize the specific gun classes over the main one
		return IsGun(item) && BreakShotgunCheck(item);
	}

	// Work top down. Least specific checks to most, all while checking the lower one.

	public static bool AssaultRifleCheck(Item item)
	{
		return !HandgunCheck(item) && !BoltRifleCheck(item) && !ShotgunCheck(item) && !MinigunCheck(item) && !RevolverCheck(item) && !BreakShotgunCheck(item) && !LaserGunCheck(item);
	}

	public static bool HandgunCheck(Item item)
	{
		// Exclude auto reuse
		if (item.autoReuse)
		{
			return false;
		}
		return  !BoltRifleCheck(item) && !ShotgunCheck(item) && !MinigunCheck(item) && !RevolverCheck(item) && !BreakShotgunCheck(item) && !LaserGunCheck(item);
	}

	public static bool BoltRifleCheck(Item item)
	{
		// Exclude non average and faster weapons
		if (item.useTime < Average)
		{
			return false;
		}

		return !ShotgunCheck(item) && !MinigunCheck(item) && !RevolverCheck(item) && !BreakShotgunCheck(item) && !LaserGunCheck(item);
	}

	// TODO: Multiple Projectile Check somehow...
	public static bool ShotgunCheck(Item item)
	{
		// Include Xenopopper
		/*if (item.type == ItemID.Xenopopper)
		{
			return true;
		}*/

		// Only Shotgun Sounds
		if (item.UseSound != SoundID.Item36 && item.UseSound != SoundID.Item38)
		{
			return false;
		}

		// Exclude insanely fast weapons
		if (item.useTime <= InsanelyFast)
		{
			return false;
		}

		// Not high velocity bullets
		if (item.shoot == ProjectileID.BulletHighVelocity)
		{
			return false;
		}

		return !MinigunCheck(item) && !RevolverCheck(item) && !BreakShotgunCheck(item) && !LaserGunCheck(item);
	}

	public static bool MinigunCheck(Item item)
	{
		// Include vanilla miniguns
		if (item.type == ItemID.Minishark || item.type == ItemID.Megashark || item.type == ItemID.SDMG || item.type == ItemID.Gatligator || item.type == ItemID.ChainGun)
		{
			return true;
		}

		// Exclude non gatling, minigun, or chaingun named weapons
		if (!item.Name.Contains("Gatling") && !item.Name.Contains("Minigun") && !item.Name.Contains("Chaingun") && !item.Name.Contains("Chain Gun"))
		{
			return false;
		}

		// Exclude non auto reuse
		if (!item.autoReuse)
		{
			return false;
		}

		// Exclude very fast and slower weapons
		if (item.useTime > InsanelyFast)
		{
			return false;
		}

		return !RevolverCheck(item) && !BreakShotgunCheck(item);
	}


	// Is there any way to automatically detect these? I may as well put them under Specific Guns.
	public static bool RevolverCheck(Item item)
	{
		if (item.type == ItemID.Revolver || item.type == ItemID.TheUndertaker)
		{
			return true;
		}
		return false;
	}

	// Is there any way to automatically detect these? I may as well put them under Specific Guns.
	public static bool BreakShotgunCheck(Item item)
	{
		if (item.type == ItemID.Boomstick || item.type == ItemID.QuadBarrelShotgun)
		{
			return true;
		}
		return false;
	}


	public static bool IsLauncher(Item item) {
		return IsShootable(item) && item.useAmmo == AmmoID.Rocket || item.useAmmo == AmmoID.JackOLantern;
	}

	public static bool IsGrenadeLauncher(Item item)
	{
		if (!ContentSampleUtils.TryGetProjectile(item.shoot, out var proj))
		{
			return false;
		}

		if (proj.aiStyle != ProjAIStyleID.Explosive || OverhaulProjectileTags.Rocket.Has(proj.type))
		{
			return false;
		}

		return IsLauncher(item);
	}

	public static bool IsRocketLauncher(Item item)
	{
		if (!ContentSampleUtils.TryGetProjectile(item.shoot, out var proj))
		{
			return false;
		}

		if (proj.aiStyle != ProjAIStyleID.Explosive || OverhaulProjectileTags.Grenade.Has(proj.type))
		{
			return false;
		}

		return IsLauncher(item);
	}



	public static bool IsLaserGun(Item item)
	{
		return IsShootable(item) && LaserGunCheck(item);
	}

	public static bool LaserGunCheck(Item item)
	{
		return item.UseSound == SoundID.Item12 || item.UseSound == SoundID.Item33 || item.UseSound == SoundID.Item75 || item.UseSound == SoundID.Item157 || item.UseSound == SoundID.Item158;
	}



	public static bool IsNailgun(Item item)
	{
		return IsShootable(item) && item.useAmmo == AmmoID.NailFriendly;
	}
}

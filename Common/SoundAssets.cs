using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;

namespace TerrariaOverhaulAddon.Common
{
	internal static class SoundAssets
	{
		/* OLD OVERHAUL SOUNDS */
		public static readonly SoundStyle AssaultRifleFireSound = new($"{nameof(TerrariaOverhaul)}/Assets/Sounds/Items/Guns/AssaultRifle/AssaultRifleFire", 3)
		{
			Volume = 0.125f,
			PitchVariance = 0.2f,
		};


		/* MY NEW SOUNDS */
		// TODO: Replace?
		public static readonly SoundStyle SniperRifleShootSound = new($"{nameof(TerrariaOverhaulAddon)}/Assets/Sounds/Items/Guns/SniperRifle/Shoot")
		{
			Volume = 0.3f,
			PitchVariance = 0.2f,
		};
		// TODO: Replace?
		public static readonly SoundStyle RifleRackSound = new($"{nameof(TerrariaOverhaulAddon)}/Assets/Sounds/Items/Guns/Rifle/Rack")
		{
			Volume = 0.2f,
			PitchVariance = 0.2f,
		};

		// TODO: Replace (But muh Noita reference - It's pretty obscure anyway)
		public static readonly SoundStyle LaserGunShootSound = new($"{nameof(TerrariaOverhaulAddon)}/Assets/Sounds/Items/Guns/LaserGun/Shoot")
		{
			Volume = 0.3f,
			PitchVariance = 0.2f,
		};
		// TODO: Replace (But muh Quake reference)
		public static readonly SoundStyle NailgunShootSound = new($"{nameof(TerrariaOverhaulAddon)}/Assets/Sounds/Items/Guns/Nailgun/Shoot")
		{
			Volume = 0.2f,
			PitchVariance = 0.2f,
		};


		public static readonly SoundStyle ShotgunShootSound = new($"{nameof(TerrariaOverhaulAddon)}/Assets/Sounds/Items/Guns/Shotgun/Shoot")
		{
			Volume = 0.4f,
			PitchVariance = 0.2f
		};

		public static readonly SoundStyle ShotgunPumpSound = new($"{nameof(TerrariaOverhaulAddon)}/Assets/Sounds/Items/Guns/Shotgun/Pump")
		{
			Volume = 0.2f,
			PitchVariance = 0.2f
		};

		// Same as the break action shotgun for now
		public static readonly SoundStyle CombatShotgunShootSound = new($"{nameof(TerrariaOverhaulAddon)}/Assets/Sounds/Items/Guns/CombatShotgun/Shoot")
		{
			Volume = 0.4f,
			PitchVariance = 0.2f
		};

		// Wait, combat shotguns don't pump! Ah well, it's used for the heavy shotguns.
		public static readonly SoundStyle CombatShotgunPumpSound = new($"{nameof(TerrariaOverhaulAddon)}/Assets/Sounds/Items/Guns/CombatShotgun/Pump")
		{
			Volume = 0.2f,
			PitchVariance = 0.2f
		};

		public static readonly SoundStyle? BreakShotgunShootSound = new($"{nameof(TerrariaOverhaulAddon)}/Assets/Sounds/Items/Guns/BreakShotgun/Shoot")
		{
			Volume = 0.4f,
			PitchVariance = 0.2f,
		};

		public static readonly SoundStyle? BreakShotgunOpenSound = new($"{nameof(TerrariaOverhaulAddon)}/Assets/Sounds/Items/Guns/BreakShotgun/Open")
		{
			Volume = 0.2f,
			PitchVariance = 0.2f,
		};

		public static readonly SoundStyle? BreakShotgunLoadSound = new($"{nameof(TerrariaOverhaulAddon)}/Assets/Sounds/Items/Guns/BreakShotgun/Load")
		{
			Volume = 0.2f,
			PitchVariance = 0.2f,
		};

		public static readonly SoundStyle? BreakShotgunCloseSound = new($"{nameof(TerrariaOverhaulAddon)}/Assets/Sounds/Items/Guns/BreakShotgun/Close")
		{
			Volume = 0.2f,
			PitchVariance = 0.2f,
		};

		public static readonly SoundStyle? BreakShotgunCloseLongSound = new($"{nameof(TerrariaOverhaulAddon)}/Assets/Sounds/Items/Guns/BreakShotgun/CloseLong")
		{
			Volume = 0.2f,
			PitchVariance = 0.2f,
		};


		/* SPECIFIC WEAPONS */
		public static readonly SoundStyle RedRyderShootSound = new($"{nameof(TerrariaOverhaulAddon)}/Assets/Sounds/Items/Guns/RedRyder/Shoot")
		{
			Volume = 0.4f,
			PitchVariance = 0.1f
		};

		public static readonly SoundStyle RedRyderPumpSound = new($"{nameof(TerrariaOverhaulAddon)}/Assets/Sounds/Items/Guns/RedRyder/Pump")
		{
			Volume = 0.2f,
			PitchVariance = 0.1f
		};

	}
}

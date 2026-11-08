using Microsoft.Xna.Framework.Audio;
using System;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;
using TerrariaOverhaul.Common.Ambience;
using TerrariaOverhaul.Common.AudioEffects;
using TerrariaOverhaul.Common.Footsteps;
using TerrariaOverhaul.Core.AudioEffects;

namespace TerrariaOverhaulAddon.Compatibility;

[JITWhenModsEnabled("TerrariaAmbience")]
internal class TerrariaAmbience_DisableAudioEffects : ModSystem
{
	private delegate void ApplyEffects(SoundEffectInstance instance, in AudioEffectParameters parameters);
	private delegate void OnSoundUpdate(Span<AudioEffectsSystem.SoundData> sounds);
	private delegate void ApplyOcclusionToSounds(Span<AudioEffectsSystem.SoundData> sounds);
	private delegate void PostUpdateEverything(object instance);
	private delegate void PostItemCheck(object instance);
	private delegate void OnModLoad(object instance);
	private delegate void OnKill(object instance, Projectile projectile, int timeLeft);

	// TODO: Re-enable if corrosponding Terraria Ambience config is disabled
	public override void Load()
	{
		if (ModLoader.HasMod("TerrariaAmbience"))
		{
			MonoModHooks.Add(typeof(ReverbSystem).GetMethod("ApplyEffects", BindingFlags.NonPublic | BindingFlags.Static), void (ApplyEffects orig, SoundEffectInstance instance, in AudioEffectParameters parameters) => { return; });
			MonoModHooks.Add(typeof(ReverbSystem).GetMethod("OnModLoad", BindingFlags.Public | BindingFlags.Instance), void (OnModLoad orig, object self) => { return; });

			//MonoModHooks.Add(typeof(LowPassFilteringSystem).GetMethod("ApplyEffects", BindingFlags.NonPublic | BindingFlags.Static), void (ApplyEffects orig, SoundEffectInstance instance, in AudioEffectParameters parameters) => { return; });
			//MonoModHooks.Add(typeof(ProjectileAudioMuffling).GetMethod("OnKill", BindingFlags.Public | BindingFlags.Instance), void (OnKill orig, object instance, Projectile projectile, int timeLeft) => { return; });

			MonoModHooks.Add(typeof(UnderwaterMuffling).GetMethod("OnSoundUpdate", BindingFlags.NonPublic | BindingFlags.Static), void (OnSoundUpdate orig, Span<AudioEffectsSystem.SoundData> sounds) => { return; });

			MonoModHooks.Add(typeof(TileSoundOcclusion).GetMethod("ApplyOcclusionToSounds", BindingFlags.NonPublic | BindingFlags.Static), void (ApplyOcclusionToSounds orig, Span<AudioEffectsSystem.SoundData> sounds) => { return; });
			MonoModHooks.Add(typeof(WallSoundOcclusion).GetMethod("ApplyOcclusionToSounds", BindingFlags.NonPublic | BindingFlags.Static), void (ApplyOcclusionToSounds orig, Span<AudioEffectsSystem.SoundData> sounds) => { return; });
			MonoModHooks.Add(typeof(WallSoundOcclusion).GetMethod("PostUpdateEverything", BindingFlags.Public | BindingFlags.Instance), void (PostUpdateEverything orig, object instance) => { return; });

			MonoModHooks.Add(typeof(AmbienceSystem).GetMethod("PostUpdateEverything", BindingFlags.Public | BindingFlags.Instance), void (PostUpdateEverything orig, object instance) => { return; });

			MonoModHooks.Add(typeof(PlayerFootsteps).GetMethod("PostItemCheck", BindingFlags.Public | BindingFlags.Instance), void (PostItemCheck orig, object instance) => { return; });
		}
	}
}
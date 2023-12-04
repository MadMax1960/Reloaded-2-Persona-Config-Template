using NaoSmiley.Configuration;
using NaoSmiley.Template;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using CriFs.V2.Hook;
using CriFs.V2.Hook.Interfaces;
using PAK.Stream.Emulator;
using PAK.Stream.Emulator.Interfaces;
using Reloaded.Mod.Interfaces.Internal;
using BGME.Framework;
using BGME.Framework.Interfaces;
using BF.File.Emulator;
using BF.File.Emulator.Interfaces;

namespace NaoSmiley
{
	/// <summary>
	/// Your mod logic goes here.
	/// </summary>
	public class Mod : ModBase // <= Do not Remove.
	{
		/// <summary>
		/// Provides access to the mod loader API.
		/// </summary>
		private readonly IModLoader _modLoader;
	
		/// <summary>
		/// Provides access to the Reloaded.Hooks API.
		/// </summary>
		/// <remarks>This is null if you remove dependency on Reloaded.SharedLib.Hooks in your mod.</remarks>
		private readonly IReloadedHooks? _hooks;
	
		/// <summary>
		/// Provides access to the Reloaded logger.
		/// </summary>
		private readonly ILogger _logger;
	
		/// <summary>
		/// Entry point into the mod, instance that created this class.
		/// </summary>
		private readonly IMod _owner;
	
		/// <summary>
		/// Provides access to this mod's configuration.
		/// </summary>
		private Config _configuration;
	
		/// <summary>
		/// The configuration of the currently executing mod.
		/// </summary>
		private readonly IModConfig _modConfig;
	
		public Mod(ModContext context)
		{
			_modLoader = context.ModLoader;
			_hooks = context.Hooks;
			_logger = context.Logger;
			_owner = context.Owner;
			_configuration = context.Configuration;
			_modConfig = context.ModConfig;

			var modDir = _modLoader.GetDirectoryForModId(_modConfig.ModId); // modDir variable for file emulation

			// For more information about this template, please see
			// https://reloaded-project.github.io/Reloaded-II/ModTemplate/

			// If you want to implement e.g. unload support in your mod,
			// and some other neat features, override the methods in ModBase.

			// TODO: Implement some mod logic

			// Define controllers and other variables, set warning messages
	
			var criFsController = _modLoader.GetController<ICriFsRedirectorApi>();
			if (criFsController == null || !criFsController.TryGetTarget(out var criFsApi))
			{
				_logger.WriteLine($"Something in CriFS shit its pants! Normal files will not load properly!", System.Drawing.Color.Red);
				return;
            }

            var PakEmulatorController = _modLoader.GetController<IPakEmulator>();
            if (PakEmulatorController == null || !PakEmulatorController.TryGetTarget(out var _PakEmulator))
            {
                _logger.WriteLine($"Something in PAK Emulator shit its pants! Files requiring bin merging will not load properly!", System.Drawing.Color.Red);
                return;
            }

            var BfEmulatorController = _modLoader.GetController<IBfEmulator>();
            if (BfEmulatorController == null || !BfEmulatorController.TryGetTarget(out var _BfEmulator))
            {
                _logger.WriteLine($"Something in BF Emulator shit its pants! Files requiring bf merging will not load properly!", System.Drawing.Color.Red);
                return;
            }

            var BGMEController = _modLoader.GetController<IBgmeApi>();
			if (BGMEController == null || !BGMEController.TryGetTarget(out var _BGME))
			{
				_logger.WriteLine($"Something in PAK Emulator shit its pants! Files requiring bin merging will not load properly!", System.Drawing.Color.Red);
				return;
            }


            // Set configuration options - obviously you don't need all of these, pick and choose what you want!

            // criFS
            if (_configuration.BoolExample)
			{
				criFsApi.AddProbingPath("CriFS"); // adds "CriFS\(any folder name here)\..." as a probing path for CriFS if BoolExample is set to true
			}

            if (_configuration.EnumExample == Config.TestEnum.Value1)
            {
                criFsApi.AddProbingPath("CriFS"); // adds "CriFS\(any folder name here)\..." as a probing path for CriFS if EnumExample is set to Value1
            }

            // PAK Emulator
            if (_configuration.BoolExample)
			{
				_PakEmulator.AddDirectory(Path.Combine(modDir, "PakEmu")); // adds "PakEmu\..." as a probing path for BIN merging if config option is enabled
            }

            // BF Emulator
            if (_configuration.BoolExample)
            {
                _BfEmulator.AddDirectory(Path.Combine(modDir, "BfEmu/BF")); // adds "PakEmu\BF\..." as a probing path for BF merging if config option is enabled
            }

            // BGME
            if (_configuration.BoolExample)
            {
				_BGME.AddFolder(Path.Combine(modDir, "BGME_Config")); // adds "BGME_Config\..." as a probing path for BF merging if config option is enabled
            }
        }
	
		#region Standard Overrides
	public override void ConfigurationUpdated(Config configuration)
	{
		// Apply settings from configuration.
		// ... your code here.
		_configuration = configuration;
		_logger.WriteLine($"[{_modConfig.ModId}] Config Updated: Applying");
	}
	#endregion
	
		#region For Exports, Serialization etc.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public Mod() { }
#pragma warning restore CS8618
	#endregion
	}
}
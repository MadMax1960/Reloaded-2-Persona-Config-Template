using NaoSmiley.Configuration;
using NaoSmiley.Template;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using CriFs.V2.Hook;
using CriFs.V2.Hook.Interfaces;
using PAK.Stream.Emulator;
using PAK.Stream.Emulator.Interfaces;
using Reloaded.Mod.Interfaces.Internal;
using SPD.File.Emulator.Interfaces;
using SPD.File.Emulator;

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

			var criFsController = _modLoader.GetController<ICriFsRedirectorApi>(); // grab emu
			if (criFsController == null || !criFsController.TryGetTarget(out var criFsApi)) // idr LMAO
			{
				_logger.WriteLine($"Something shit it's pants", System.Drawing.Color.Red);
				return;
			}
	
			if (_configuration.Test1) // should be _configuration.(boolname)
			{
				criFsApi.AddProbingPath("Test"); // folder name
			}
	
			var PakEmulatorController = _modLoader.GetController<IPakEmulator>(); // grab emu
			if (PakEmulatorController == null || !PakEmulatorController.TryGetTarget(out var _PakEmulator)) // idr
			{
				_logger.WriteLine($"Something in pak emu shit it's pants", System.Drawing.Color.Red);
				return;
			}
	
			if (_configuration.Test1) // should be _configuration.(boolname)
			{
				_PakEmulator.AddDirectory(Path.Combine(modDir, "Test")); // folder name
			}

			var SPDEmulatorController = _modLoader.GetController<ISpdEmulator>(); // grab emu
			if (SPDEmulatorController == null || !SPDEmulatorController.TryGetTarget(out var _SPDEmulator)) // idr
			{
				_logger.WriteLine($"Something in spd emu shit it's pants", System.Drawing.Color.Red);
				return;
			}


			if (_configuration.Test1) // should be _configuration.(boolname)
			{
				_PakEmulator.AddDirectory(Path.Combine(modDir, "Test")); // folder name 
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
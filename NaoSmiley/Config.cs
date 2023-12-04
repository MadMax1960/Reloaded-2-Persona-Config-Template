using NaoSmiley.Template.Configuration;
using Reloaded.Mod.Interfaces.Structs;
using System.ComponentModel;
using CriFs.V2.Hook;
using CriFs.V2.Hook.Interfaces;
using System.Reflection;

namespace NaoSmiley.Configuration
{
	public class Config : Configurable<Config>
	{
		/*
            User Properties:
                - Please put all of your configurable properties here.

            By default, configuration saves as "Config.json" in mod user config folder.    
            Need more config files/classes? See Configuration.cs

            Available Attributes:
            - Category
            - DisplayName
            - Description
            - DefaultValue

            // Technically Supported but not Useful
            - Browsable
            - Localizable

            The `DefaultValue` attribute is used as part of the `Reset` button in Reloaded-Launcher.
        */

		public enum TestEnum
		{
			Value1,
			Value2,
            Value3,
            Value4,
        }

        [Category("Category 1")]
        [DisplayName("Bool")]
		[Description("Bool example")]
		[DefaultValue(true)]
		public bool BoolExample { get; set; } = true;

        [Category("Category 2")]
        [DisplayName("Enum")]
        [Description("Enum example")]
        [DefaultValue(TestEnum.Value1)]
        public TestEnum EnumExample { get; set; } = TestEnum.Value1;
    }

    /// <summary>
    /// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
    /// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
    /// </summary>
	public class ConfiguratorMixin : ConfiguratorMixinBase
	{
		// 
	}
}
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace Community.PowerToys.CommandPalette.Extension.CrcLauncher;

public partial class CrcLauncherCommandsProvider : CommandProvider
{
    private readonly ICommandItem[] _commands;

    public CrcLauncherCommandsProvider()
    {
        Id = "crc-launcher";
        DisplayName = "CRC launcher";
        Icon = IconHelpers.FromRelativePath("Assets\\CrcLauncher.svg");
        _commands =
        [
            new CommandItem(new CrcProfilesPage())
            {
                Title = DisplayName,
                Subtitle = "Launch CRC with one of your installed profiles",
                Icon = Icon,
            },
        ];
    }

    public override ICommandItem[] TopLevelCommands() => _commands;
}

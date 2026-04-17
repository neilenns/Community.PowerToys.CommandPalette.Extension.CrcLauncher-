using System;
using System.Diagnostics;
using System.IO;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace Community.PowerToys.CommandPalette.Extension.CrcLauncher;

internal sealed partial class LaunchCrcProfileCommand : InvokableCommand
{
    private readonly CrcProfile _profile;

    public LaunchCrcProfileCommand(CrcProfile profile)
    {
        _profile = profile;
        Name = "Launch CRC profile";
        Icon = IconHelpers.FromRelativePath("Assets\\CrcLauncher.svg");
    }

    public override CommandResult Invoke()
    {
        if (string.IsNullOrWhiteSpace(_profile.Id))
        {
            return ShowError("The selected profile is missing its profile ID.");
        }

        var crcPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "CRC",
            "Application",
            "crc.exe");

        if (!File.Exists(crcPath))
        {
            return ShowError($"Unable to find CRC executable at '{crcPath}'.");
        }

        try
        {
            Process.Start(
                new ProcessStartInfo
                {
                    FileName = crcPath,
                    Arguments = $"--profile={_profile.Id}",
                });

            return CommandResult.Dismiss();
        }
        catch (Exception ex)
        {
            return ShowError($"Unable to launch CRC profile '{_profile.Name}': {ex.Message}");
        }
    }

    private static CommandResult ShowError(string message)
    {
        ExtensionHost.ShowStatus(
            new StatusMessage
            {
                Message = message,
                State = MessageState.Error,
            },
            StatusContext.Page);

        return CommandResult.KeepOpen();
    }
}

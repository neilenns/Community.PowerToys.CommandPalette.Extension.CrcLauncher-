using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.Win32;

namespace Community.PowerToys.CommandPalette.Extension.CrcLauncher;

internal static class ProfileManager
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    public static IReadOnlyList<CrcProfile> GetMatchingProfiles(string? query)
    {
        var profiles = LoadProfilesFromDisk();
        if (string.IsNullOrWhiteSpace(query))
        {
            return profiles;
        }

        return
        [
            .. profiles.Where(profile =>
                !string.IsNullOrWhiteSpace(profile.Name) &&
                profile.Name.Contains(query, StringComparison.OrdinalIgnoreCase)),
        ];
    }

    private static IReadOnlyList<CrcProfile> LoadProfilesFromDisk()
    {
        var profileDirectory = GetCrcProfilePath();
        if (string.IsNullOrEmpty(profileDirectory) || !Directory.Exists(profileDirectory))
        {
            return [];
        }

        List<CrcProfile> profiles = [];
        foreach (var file in Directory.GetFiles(profileDirectory, "*.json"))
        {
            try
            {
                var profile = JsonSerializer.Deserialize<CrcProfile>(File.ReadAllText(file), JsonSerializerOptions);
                if (profile is null)
                {
                    continue;
                }

                profile.FilePath = file;
                if (string.IsNullOrWhiteSpace(profile.Name))
                {
                    profile.Name = Path.GetFileNameWithoutExtension(file);
                }

                profiles.Add(profile);
            }
            catch
            {
                // Skip malformed profile files and continue loading the rest.
            }
        }

        return profiles;
    }

    private static string GetCrcProfilePath()
    {
        var fallback = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CRC", "Profiles");

        try
        {
            using RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"Software\CRC");
            var installDir = key?.GetValue("Install_Dir")?.ToString();
            if (!string.IsNullOrWhiteSpace(installDir))
            {
                var configuredPath = Path.Combine(installDir, "Profiles");
                if (Directory.Exists(configuredPath))
                {
                    return configuredPath;
                }
            }
        }
        catch
        {
            // Fall back to the default LocalAppData path.
        }

        return fallback;
    }
}

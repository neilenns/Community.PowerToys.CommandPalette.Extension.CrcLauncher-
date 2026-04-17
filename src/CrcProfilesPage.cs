using System;
using System.Linq;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace Community.PowerToys.CommandPalette.Extension.CrcLauncher;

internal sealed partial class CrcProfilesPage : DynamicListPage
{
    public CrcProfilesPage()
    {
        Icon = IconHelpers.FromRelativePath("Assets\\CrcLauncher.svg");
        Name = "Open";
        Title = "CRC profiles";
        Id = "crc-launcher.profiles";
        PlaceholderText = "Filter CRC profiles";
    }

    public override void UpdateSearchText(string oldSearch, string newSearch)
    {
        RaiseItemsChanged(0);
    }

    public override IListItem[] GetItems()
    {
        var profiles =
            ProfileManager
                .GetMatchingProfiles(SearchText)
                .OrderBy(profile => profile.Name, StringComparer.OrdinalIgnoreCase)
                .ToArray();

        if (profiles.Length == 0)
        {
            return
            [
                new ListItem(new NoOpCommand())
                {
                    Icon = Icon,
                    Title = "No CRC profiles found",
                    Subtitle = string.IsNullOrWhiteSpace(SearchText)
                        ? "Install CRC and create profiles to launch them from Command Palette."
                        : $"No profiles match '{SearchText}'.",
                },
            ];
        }

        return
        [
            .. profiles.Select(profile => new ListItem(new LaunchCrcProfileCommand(profile))
            {
                Icon = Icon,
                Title = profile.Name,
                Subtitle = $"Profile ID: {profile.Id}",
            }),
        ];
    }
}

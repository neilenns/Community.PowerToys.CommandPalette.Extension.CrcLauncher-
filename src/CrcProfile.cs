using System;

namespace Community.PowerToys.CommandPalette.Extension.CrcLauncher;

internal class CrcProfile
{
    public string Id { get; set; } = string.Empty;

    public int Version { get; set; }

    public string Name { get; set; } = string.Empty;

    public string FilePath { get; set; } = string.Empty;

    public DateTime LastUsedAt { get; set; }

    public string ArtccId { get; set; } = string.Empty;

    public string LastUsedEnvironment { get; set; } = string.Empty;

    public string LastUsedPositionId { get; set; } = string.Empty;

    public string NetworkRating { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public object? ControllerInfo { get; set; }
}

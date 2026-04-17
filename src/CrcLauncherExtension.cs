using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.CommandPalette.Extensions;

namespace Community.PowerToys.CommandPalette.Extension.CrcLauncher;

[Guid("5B2D3EF9-3556-4F58-BFA9-8B8693F6A658")]
public sealed partial class CrcLauncherExtension : IExtension, IDisposable
{
    private readonly ManualResetEvent _extensionDisposedEvent;
    private readonly CrcLauncherCommandsProvider _provider = new();

    public CrcLauncherExtension(ManualResetEvent extensionDisposedEvent)
    {
        _extensionDisposedEvent = extensionDisposedEvent;
    }

    public object? GetProvider(ProviderType providerType)
    {
        return providerType switch
        {
            ProviderType.Commands => _provider,
            _ => null,
        };
    }

    public void Dispose() => _extensionDisposedEvent.Set();
}

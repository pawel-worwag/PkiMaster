using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace PkiMaster.Backend.Components.Layout;

public partial class MainLayout(IJSRuntime js) : IAsyncDisposable
{
    private IJSObjectReference? _module;
    private ElementReference _mainMenuElement;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            try
            {
                _module = await js.InvokeAsync<IJSObjectReference>("import", "./js/ui-helpers.js");
            }
            catch
            {
                // ignored
            }
        }
    }
    
    private async Task OpenMenuAsync()
    {
        if (_module is null) return;
        await _module.InvokeAsync<bool>("openOffcanvasAsync", _mainMenuElement);
    }

    private async Task CloseMenuAsync()
    {
        if (_module is null) return;
        await _module.InvokeAsync<bool>("closeOffcanvasAsync", _mainMenuElement);
    }

    public async ValueTask DisposeAsync()
    {
        var module = _module;
        if (module is null)
        {
            return;
        }
        _module = null;
        
        try
        {
            await module.DisposeAsync();
        }
        catch (JSDisconnectedException)
        {
            // ignored
        }
        GC.SuppressFinalize(this);
    }
}
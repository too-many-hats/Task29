using SDL2;

namespace ReplicaConsole.Windows;

public class LargeButtonRenderer
{
    private List<LargeButton> buttons = [];
    private ButtonTextures ButtonTextures { get; set; }

    public LargeButtonRenderer(ButtonTextures buttonTextures)
    {
        this.ButtonTextures = buttonTextures;
    }

    /// <summary>
    /// Create a row of large buttons starting at coordinate X,Y. One button will be created for each supplied event handler.
    /// </summary>
    public List<LargeButton> CreateMultiple(int x, int y, params Action[] eventHandlers)
    {
        var result = new List<LargeButton>();
        for (int i = 0; i < eventHandlers.Length; i++)
        {
            var destRect = new SDL.SDL_Rect
            {
                x = x + (65 + 28) * i,
                y = y,
                h = 60,
                w = 60,
            };

            buttons.Add(new LargeButton(destRect, eventHandlers[i]));
        }

        return result;
    }

    /// <summary>
    /// Render an all large buttons that are part of this instance of LargeButtonRenderer.
    /// </summary>
    public void RenderAll(nint renderer)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            var hitbox = buttons[i].Hitbox;
            SDL.SDL_RenderCopy(renderer, ButtonTextures.LargeButtonTexture, ref ButtonTextures.SourceRect, ref hitbox);
        }
    }

    public void TestHitsAndFireCallbacks(SDL.SDL_Event e)
    {
        if (e.type != SDL.SDL_EventType.SDL_MOUSEBUTTONUP)
        {
            return;
        }

        if (e.button.button != SDL.SDL_BUTTON_LEFT)
        {
            return;
        }

        foreach (var button in buttons)
        {
            if (e.button.x >= button.Hitbox.x && e.button.x <= button.Hitbox.x + button.Hitbox.w
                && e.button.y >= button.Hitbox.y && e.button.y <= button.Hitbox.y + button.Hitbox.h)
            {
                button.OnClick();
            }
        }
    }
}
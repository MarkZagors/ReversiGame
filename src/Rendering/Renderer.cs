using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ReversiGame
{
    public class Renderer
    {
        private static Renderer? instance;
        public readonly RenderWindow window;
        private List<IRendering> layers = new List<IRendering>();

        public Renderer(RenderWindow _window)
        {
            window = _window;
            instance = this;
        }

        public static Renderer GetInstance()
        {
            if (instance == null)
            {
                RenderWindow renderWindow = new RenderWindow(new VideoMode(), "Not set");
                return new Renderer(renderWindow);
            }
            return instance;
        }

        public void Show()
        {
            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(Color.Black);
                Process(window);
                window.Display();
            }
        }

        public void Process(RenderWindow _window)
        {
            foreach (IRendering layer in layers)
            {
                layer.Render();
            }
        }

        public void AddLayer(IRendering layer)
        {
            layers.Add(layer);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeaBattle
{
    /// <summary>
    /// Codes representing keyboard keys.
    /// </summary>
    /// <remarks>
    /// Key code documentation:
    /// http://msdn.microsoft.com/en-us/library/dd375731%28v=VS.85%29.aspx
    /// </remarks>
    internal enum KeyCode : int
    {
        /// <summary>
        /// The left arrow key.
        /// </summary>
        Left = 0x25,

        /// <summary>
        /// The up arrow key.
        /// </summary>
        Up,

        /// <summary>
        /// The right arrow key.
        /// </summary>
        Right,

        /// <summary>
        /// The down arrow key.
        /// </summary>
        Down
    }

    /// <summary>
    /// Provides keyboard access.
    /// </summary>
    internal class ArrowKeyListener
    {

        public event Action<KeyCode> OnArrowKeyPressed;

        private Task listener;
        private CancellationTokenSource token;

        public void StartListening()
        {
            if (!listener.IsCompleted) return;

            token = new();
            listener = Task.Run(async () =>
            {
                while (true)
                {
                    if (IsKeyDown(KeyCode.Left)) OnArrowKeyPressed(KeyCode.Left);
                    if (IsKeyDown(KeyCode.Down)) OnArrowKeyPressed(KeyCode.Down);
                    if (IsKeyDown(KeyCode.Right)) OnArrowKeyPressed(KeyCode.Right);
                    if (IsKeyDown(KeyCode.Up)) OnArrowKeyPressed(KeyCode.Up);
                    await Task.Delay(200);
                }
            }, token.Token);
        }
        public void StopListening()
        {
            token.Cancel();
            token.Dispose();
        }


        /// <summary>
        /// A positional bit flag indicating the part of a key state denoting
        /// key pressed.
        /// </summary>
        private const int KeyPressed = 0x8000;

        /// <summary>
        /// Returns a value indicating if a given key is pressed.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>
        /// <c>true</c> if the key is pressed, otherwise <c>false</c>.
        /// </returns>
        private static bool IsKeyDown(KeyCode key)
        {
            return (GetKeyState((int)key) & KeyPressed) != 0;
        }

        /// <summary>
        /// Gets the key state of a key.
        /// </summary>
        /// <param name="key">Virtuak-key code for key.</param>
        /// <returns>The state of the key.</returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short GetKeyState(int key);
    }
}

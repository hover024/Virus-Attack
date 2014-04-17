using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VirusAttack
{
    public class Button : Sprite
    {
        private MouseState _previousState;

        private Texture2D _hoverTexture;
        private Texture2D _pressedTexture;

        private Rectangle _bounds;

        private ButtonStatus _state = ButtonStatus.Normal;

        public Button(Texture2D tex, Texture2D hoverTexture, Texture2D pressedTexture, Vector2 position)
            : base(tex, position)
        {
            _hoverTexture = hoverTexture;
            _pressedTexture = pressedTexture;

            _bounds = new Rectangle((int) position.X, (int) position.Y, tex.Width, tex.Height);
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            var mouseX = mouseState.X;
            var mouseY = mouseState.Y;
            var isMouseOver = _bounds.Contains(mouseX, mouseY);
            if (isMouseOver && _state != ButtonStatus.Pressed)
            {
                _state = ButtonStatus.Hover;
            }
            else if (isMouseOver == false && _state != ButtonStatus.Pressed)
            {
                _state = ButtonStatus.Normal;
            }

            if (mouseState.LeftButton == ButtonState.Pressed && _previousState.LeftButton == ButtonState.Released)
            {
                if (isMouseOver)
                {
                    _state = ButtonStatus.Pressed;
                }
            }

            if (mouseState.LeftButton == ButtonState.Released &&
                _previousState.LeftButton == ButtonState.Pressed)
            {
                if (isMouseOver == true)
                {
                    // update the button state.
                    _state = ButtonStatus.Hover;
                }

                else if (_state == ButtonStatus.Pressed)
                {
                    _state = ButtonStatus.Normal;
                }
            }

            _previousState = mouseState;
        }
    }
}

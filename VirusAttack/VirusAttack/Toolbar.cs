using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VirusAttack
{
    public class Toolbar
    {
        private readonly Texture2D _texture;
        private readonly SpriteFont _font;
        private readonly Vector2 _position;
        private readonly Vector2 _textPosition;

        public Toolbar(Texture2D texture, SpriteFont font, Vector2 position)
        {
            _texture = texture;
            _font = font;
            _position = position;
            _textPosition=new Vector2(130,_position.Y+10);
        }

        public void Draw(SpriteBatch spriteBatch, Player player)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
            var text = string.Format("Gold: {0}     Lives: {1}", player.Money, player.Lives);
            spriteBatch.DrawString(_font, text, _textPosition, Color.White);
        }
    }
}

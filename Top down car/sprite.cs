using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Top_down_car
{
    class Sprite
    {
        public Texture2D spriteTexture { get; set; }
        public Vector2 spritePosition { get; set; }
        public Vector2 spriteSize { get; set; }

        public Sprite()
        {

        }

        public Sprite(Texture2D tex, Vector2 pos, Vector2 size)
        {
            this.spriteTexture = tex;
            this.spritePosition = pos;
            this.spriteSize = size;

        }

        public void DrawSprite(SpriteBatch spriteBatch, Texture2D texture, Vector2 origin,float rotation,Color color,MyMatrix matrix)
        {
            spriteTexture = texture;
            spriteBatch.Begin(SpriteSortMode.Deferred,
            BlendState.AlphaBlend,
            SamplerState.PointClamp,
            null, null, null, transformMatrix: matrix.Transform);

            spriteBatch.Draw(spriteTexture, new Rectangle((int)spritePosition.X, (int)spritePosition.Y, (int)spriteSize.X, (int)spriteSize.Y), null, color, rotation, origin, SpriteEffects.None, 0f);
            spriteBatch.End();

        }


    }
}

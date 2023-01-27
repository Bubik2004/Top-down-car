using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;

namespace Top_down_car
{
    class Sprite
    {
        public Texture2D spriteTexture { get; set; }
        public Vector2 spritePosition { get; set; }
        public Vector2 spriteSize { get; set; }

        List<Rectangle> rectan = new List<Rectangle>();
        bool found = false;
        Vector2 COR;
        Vector2 offset;
        int move = 0;

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

        public Vector2 DrawLines(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 end, MyMatrix matrix,float rotation,Vector2 Faxl,Vector2 Rline,Rectangle rearL)
        {

            //while (distance != end)
            //spriteBatch.Draw(texture, start, null, Color.White,rotation,
            //             new Vector2(0f, texture.Height / 2),
            //             new Vector2(Vector2.Distance(start, end), 0.02f),
            //             SpriteEffects.None, 0f);
            //spriteBatch.End();

            if (rotation != 0 || Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.A))
            {

                offset = matrix.transAxis(new Vector2(0 + move, 0), rotation);
                spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null, null, null, transformMatrix: matrix.Transform);

                foreach (Rectangle i in rectan.ToList())
                {
                        if (i.Intersects(rearL))
                        {
                            found = true;
                            COR = new Vector2(i.X, Rline.Y);
                            move = 0;
                            rectan.Clear();


                        }
                    if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.A)) 
                    {
                        //found = true;
                        //move = 0;
                        rectan.Clear();

                        found = false;

                    }
                
                }
                if (found == false)
                {
                    if (rotation < 0.05) 
                    {
                        move += 2000;
                        Rectangle pixel = new Rectangle((int)start.X + (int)offset.X, (int)start.Y + (int)offset.Y, 200, 200);

                        rectan.Add(pixel);
                    }
                    if (rotation < 0.1) 
                    {
                        move += 1000;

                        Rectangle pixel = new Rectangle((int)start.X + (int)offset.X, (int)start.Y + (int)offset.Y, 150, 150);

                        rectan.Add(pixel);
                    }
                    if (rotation < 0.25) 
                    {
                        move += 450;

                        Rectangle pixel = new Rectangle((int)start.X + (int)offset.X, (int)start.Y + (int)offset.Y, 100, 100);

                        rectan.Add(pixel);
                    }
                    if (rotation < 0.40)
                    {
                        move += 225;
                        Rectangle pixel = new Rectangle((int)start.X + (int)offset.X, (int)start.Y + (int)offset.Y, 100, 100);
                        rectan.Add(pixel);
                    }
                    else 
                    {
                        move += 100;

                        Rectangle pixel = new Rectangle((int)start.X + (int)offset.X, (int)start.Y + (int)offset.Y, 75, 75);

                        rectan.Add(pixel);
                    }
                    spriteBatch.Draw(texture, start + offset, null, Color.White, rotation, new Vector2(0f, texture.Height / 2), new Vector2(0.1f, 0.1f), SpriteEffects.None, 0f);
                
                }
                else if(found == true)
                {
                    found = false;
                }

                spriteBatch.End();


            }

            Debug.WriteLine(rotation);

            //Debug.WriteLine("offset = {0} {1}", offset.X, offset.Y);
            return COR;
        }

    }
}

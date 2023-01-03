using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Top_down_car
{
    class MyMatrix
    {
        public Matrix Transform { get; private set; }
        private Vector2[] comp = new Vector2[2];
        private Vector2 Res;
        private float preRot;
        private Vector2 Ret = new Vector2(0, 0);

        public void Follow(Sprite Target, float rotation,Vector2 origin,float scroll)
        {
            //Matrix Position = Matrix.CreateTranslation(-Target.spritePosition.X - (Target.spriteSize.X / 2),-Target.spritePosition.Y - (Target.spriteSize.Y / 2), 0);
            Matrix offset = Matrix.CreateTranslation(Game1.dispX/2,Game1.dispY/2,0);
            //Matrix scale = Matrix.CreateScale(500);
            //Matrix rot = Matrix.CreateRotationZ(rotation);
            Transform = Matrix.CreateTranslation(new Vector3(-Target.spritePosition.X,-Target.spritePosition.Y, 0)) * Matrix.CreateScale(scroll) * Matrix.CreateRotationZ(-rotation);

            

            Transform = Transform * offset;

            

        }
        public Vector2 transAxis(Vector2 W,float rotation) 
        {
            
            //if (rotation != preRot) 
            //{
                double x;
                double y;
                x = W.X * Math.Cos(rotation) - W.Y * Math.Sin(rotation);
                y = W.X * Math.Sin(rotation) + W.Y * Math.Cos(rotation);
                Res = new Vector2((float)x, (float)y);
            //    if (comp[1] == null)
            //    {
            //        if (comp[0] == null)
            //        {
            //            comp[0] = Res;

            //        }
            //        else
            //        {
            //            comp[1] = Res;

            //        }
            //    }
            //    else
            //    {
            //        comp[0] = comp[1];
            //    if (comp[1] != Res) 
            //    {
            //        comp[1] = Res;


            //    }

            //    Ret.X = comp[0].X - comp[1].X;
            //        Ret.Y = comp[0].Y - comp[1].Y;
            //        Debug.WriteLine(Ret.X);

            //    //}


            //    preRot = rotation;


            //}

            return Res;
        }
        public void misctest(Vector2 V) 
        {
       
        }


        
    }
}


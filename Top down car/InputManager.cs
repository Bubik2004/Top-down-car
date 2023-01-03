using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Top_down_car
{
    class InputManager
    {
        public static float steering = 0;
        public static float rotationF;
        float rotation_velocity = 1.5f;
        float scroll = 1;
        public float Steering(GraphicsDeviceManager inGraphics)
        {

            KeyboardState state;

            state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.A))
            {
                rotationF = -rotation_velocity;
                if (steering > -0.782)
                {
                    steering -= MathHelper.ToRadians(rotation_velocity);


                    //Debug.WriteLine(steering);

                }
                else
                {
                    steering = -0.783f;

                    rotationF = 0;


                }


            }
            else 
            {
                rotationF = -0;
            }

            if (state.IsKeyDown(Keys.D))
            {
                rotationF = rotation_velocity;
                if (steering < 0.782)
                {

                    steering += MathHelper.ToRadians(rotation_velocity);
                   



                }
                else
                {
                    steering = 0.783f;

                    rotationF = 0;


                }
            }
            else 
            {
                rotationF = 0;
            }
          
            return steering;
        }
        public float zoom(GraphicsDeviceManager inGraphics) 
        {
            KeyboardState state;
            MouseState state1;
            state1 = Mouse.GetState();
            state = Keyboard.GetState();
            if (state1.ScrollWheelValue != 0) 
            {

                scroll = (float)state1.ScrollWheelValue / 2000;


            }

            return scroll;
        }
    }
   
}


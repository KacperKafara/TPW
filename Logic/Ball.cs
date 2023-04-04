﻿namespace Logic
{
    public class Ball
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float HorizontalSpeed { get; set; }
        public float VerticalSpeed { get; set; }
        public int Radius { get; set; }

        public Ball(float x, float y, int radius)
        {
            X = x;
            Y = y;
            Radius = radius;
            HorizontalSpeed = 1; 
            VerticalSpeed = 0;
        }

        public void updatePosition()
        {
            X += HorizontalSpeed;
            Y += VerticalSpeed;
        }
    }
}
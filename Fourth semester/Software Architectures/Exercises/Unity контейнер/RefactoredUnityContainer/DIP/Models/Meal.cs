﻿namespace DIP.Models
{
    public abstract class Meal
    {
        public int Calories { get; protected set; }

        public string Name { get; protected set; }
    }
}

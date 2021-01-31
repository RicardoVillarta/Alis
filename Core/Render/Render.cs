﻿//-------------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Render.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//-------------------------------------------------------------------------------------------------
namespace Alis.Core
{
    /// <summary>Render define</summary>
    public class Render
    {
        /// <summary>The core</summary>
        private static SfmlCore core;

        /// <summary>Starts this instance.</summary>
        public static void Start()
        {
            Debug.Log("Start the render.");
            core = new SfmlCore();
        }

        /// <summary>Frames the bytes.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public static byte[] FrameBytes() 
        {
            return core.FramePixels();
        }

        /// <summary>Draws this instance.</summary>
        public static void Draw()
        {
            core.Draw();
        }

        /// <summary>Displays this instance.</summary>
        public static void Display() 
        {
            core.Display(); 
        }
    }
}

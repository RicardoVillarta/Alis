﻿//----------------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Program.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//----------------------------------------------------------------------------------------------------
namespace Alis.Editor
{
    using System;

    /// <summary>Run the engine</summary>
    public class Program
    {
        /// <summary>Mains the specified arguments.</summary>
        /// <param name="args">The arguments.</param>
        /// <returns>Return 0 or -1 to indicate the exit value.</returns>
        [STAThread]
        public static int Main(string[] args) => new Engine(args).Start();
    }
}
﻿//-------------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Scene.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//-------------------------------------------------------------------------------------------------
namespace Core
{
    using System;
    using Alis.Core.SFML;
    using NUnit.Framework;

    /// <summary>Define test of scenes.</summary>
    internal class Scene
    {
        #region Variables


        #endregion


        #region Setup

        /// <summary>Setups this instance.</summary>
        [SetUp]
        public void Setup()
        {
        }

        #endregion

        #region Check Generals Cases

        /// <summary>Checks the maximum size.</summary>
        [Test]
        public void Check_The_Max_Size() => Assert.AreEqual(10, 10);

        #endregion
    }
}

﻿//-------------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="DefaultTest.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//-------------------------------------------------------------------------------------------------
namespace Tools
{
    using NUnit.Framework;

    /// <summary>Test this</summary>
    internal class DefaultTest
    {
        /// <summary>Setups this instance.</summary>
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>Trues this instance.</summary>
        [Test]
        public void True()
        {
            Assert.True(true);
        }
    }
}
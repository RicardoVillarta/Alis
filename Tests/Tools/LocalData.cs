//-------------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falc�n</author>
// <copyright file="LocalData.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//-------------------------------------------------------------------------------------------------
namespace Tools
{
    using NUnit.Framework;

    /// <summary>Test this</summary>
    internal class LocalData
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
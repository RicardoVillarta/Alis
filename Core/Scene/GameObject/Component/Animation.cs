﻿// -------------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Animation.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//-------------------------------------------------------------------------------------------------
namespace Alis.Core
{
    using Newtonsoft.Json;
    using SFML.Graphics;
    using System.Collections.Generic;

    /// <summary>Define animation of sprite.</summary>
    public class Animation
    {
        /// <summary>The name</summary>
        private string name;

        /// <summary>The state</summary>
        private int state;

        /// <summary>The images</summary>
        private string[] images;

        /// <summary>The textures</summary>
        private List<Texture> textures;

        /// <summary>The index</summary>
        private int index;

        /// <summary>The speed</summary>
        private float speed;

        /// <summary>Initializes a new instance of the <see cref="Animation" /> class.</summary>
        /// <param name="name">The name.</param>
        /// <param name="state">The state.</param>
        /// <param name="speed">The speed.</param>
        /// <param name="images">The images.</param>
        [JsonConstructor]
        public Animation(string name, int state, float speed, params string[] images)
        {
            this.name = name;
            this.state = state;
            this.speed = speed;
            this.images = images ?? (new string[0]);
            this.textures = new List<Texture>();

            foreach (string image in images)
            {
                textures.Add(new Texture("C:/Users/wwwam/Documents/Repositorios/Alis/Example/bin/Windows/netcoreapp3.1/Assets/" + image));
            }
        }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [JsonProperty]
        public string Name { get => name; set => name = value; }

        /// <summary>Gets or sets the state.</summary>
        /// <value>The state.</value>
        [JsonProperty]
        public int State { get => state; }

        /// <summary>Gets or sets the speed.</summary>
        /// <value>The speed.</value>
        [JsonProperty]
        public float Speed { get => speed; set => speed = value; }

        /// <summary>Gets the texture.</summary>
        /// <value>The texture.</value>
        [JsonProperty]
        public Texture Texture
        {
            get
            {
                index++;
                if (index >= textures.Count)
                {
                    index = 0;
                }
                return textures[index];
            }
        }
    }
}
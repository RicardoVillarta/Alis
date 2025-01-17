﻿//-------------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="Sprite.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//-------------------------------------------------------------------------------------------------
namespace Alis.Core
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Newtonsoft.Json;
   
    /// <summary>Define a component</summary>
    public class Sprite : Component
    {
        /// <summary>The image</summary>
        [NotNull]
        private string image;

        /// <summary>The path image</summary>
        [NotNull]
        private string pathImage;

        /// <summary>The depth</summary>
        [NotNull]
        private int depth;

        /// <summary>The sprite</summary>
        [NotNull]
        [JsonIgnore]
        private SFML.Graphics.Sprite sprite;

        /// <summary>Initializes a new instance of the <see cref="Sprite" /> class.</summary>
        /// <param name="image">The image.</param>
        public Sprite([NotNull] string image)
        {
            this.image = image;
            depth = 0;

            if (!image.Equals(string.Empty))
            {
                pathImage = AssetManager.Load(image);
                sprite = new SFML.Graphics.Sprite(new SFML.Graphics.Texture(pathImage));
            }

            OnDraw += Sprite_OnDraw;
        }

        /// <summary>Initializes a new instance of the <see cref="Sprite" /> class.</summary>
        /// <param name="image">The image.</param>
        /// <param name="depth">The depth.</param>
        [JsonConstructor]
        public Sprite([NotNull] string image, [NotNull] int depth)
        {
            this.image = image;
            this.depth = depth;

            if (!image.Equals(string.Empty))
            {
                pathImage = AssetManager.Load(image);
                sprite = new SFML.Graphics.Sprite(new SFML.Graphics.Texture(pathImage));
            }

            OnDraw += Sprite_OnDraw;
        }

        /// <summary>Occurs when [on draw].</summary>
        public event EventHandler<bool> OnDraw;

        /// <summary>Gets or sets the image.</summary>
        /// <value>The image.</value>
        [NotNull]
        [JsonProperty]
        public string Image
        {
            get 
            { 
                return image; 
            }
            set
            {
                image = value;
                if (!image.Equals(string.Empty)) 
                {
                    if (AssetManager.Load(image) != null) 
                    {
                        pathImage = AssetManager.Load(image);
                        sprite = new SFML.Graphics.Sprite(new SFML.Graphics.Texture(pathImage));
                        if (Render.Current.Sprites.Contains(this)) 
                        {
                            Render.Current.Sprites[Render.Current.Sprites.IndexOf(this)] = this;
                        }
                        else 
                        {
                            Render.Current.Sprites.Add(this);
                        }
                    }
                    else 
                    {
                        if (Render.Current.Sprites.Contains(this))
                        {
                            Render.Current.Sprites.Remove(this);
                        }
                    }
                }
            }
        }

        /// <summary>Gets or sets the depth.</summary>
        /// <value>The depth.</value>
        [NotNull]
        [JsonProperty]
        public int Depth { get => depth; set => depth = value; }

        /// <summary>Starts this instance.</summary>
        public override void Start()
        {
            if (sprite != null)
            {
                var pos = GetGameObject().Transform.Position;
                sprite.Position = new SFML.System.Vector2f(pos.X, pos.Y);

                var rot = GetGameObject().Transform.Rotation;
                sprite.Rotation = rot.Y;

                var size = GetGameObject().Transform.Size;
                sprite.Scale = new SFML.System.Vector2f(size.X, size.Y);

                Render.Current.AddSprite(this);
            }
        }

        /// <summary>Updates this instance.</summary>
        public override void Update()
        {
            if (sprite != null)
            {
                var pos = GetGameObject().Transform.Position;
                sprite.Position = new SFML.System.Vector2f(pos.X, pos.Y);

                var rot = GetGameObject().Transform.Rotation;
                sprite.Rotation = rot.Y;

                var size = GetGameObject().Transform.Size;
                sprite.Scale = new SFML.System.Vector2f(size.X, size.Y);
            }
        }

        public override int Priority()
        {
            return 0;
        }

        /// <summary>Gets the draw.</summary>
        /// <returns>Return none</returns>
        [return: NotNull]
        internal SFML.Graphics.Sprite GetDraw()
        {
            OnDraw.Invoke(this, true);
            return sprite;
        }

        #region DefineEvents

        /// <summary>Sprites the on draw.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">if set to <c>true</c> [e].</param>
        private void Sprite_OnDraw([NotNull] object sender, [NotNull] bool e) => Logger.Info();

        #endregion
    }
}
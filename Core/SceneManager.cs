﻿//-------------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="SceneManager.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//-------------------------------------------------------------------------------------------------
namespace Alis.Core
{
    using System;
    using System.Collections.Generic;
    using Alis.Tools;
    using Newtonsoft.Json;

    /// <summary>Manage the scenes of the videogame.</summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class SceneManager
    {
        /// <summary>The current</summary>
        private static SceneManager current;

        /// <summary>The scenes</summary>
        private List<Scene> scenes;

        /// <summary>The current scene</summary>
        private Scene currentScene;

        /// <summary>Gets or sets the scenes.</summary>
        /// <value>The scenes.</value>
        public List<Scene> Scenes { get => scenes; set => scenes = value; }

        /// <summary>Occurs when [change].</summary>
        public event EventHandler<bool> OnCreate;

        /// <summary>Occurs when [on add scene].</summary>
        public event EventHandler<bool> OnAddScene;

        /// <summary>Occurs when [on delete scene].</summary>
        public event EventHandler<bool> OnDeleteScene;

        /// <summary>Occurs when [on load scene].</summary>
        public event EventHandler<bool> OnLoadScene;

        /// <summary>Occurs when [change].</summary>
        public event EventHandler<bool> OnDestroy;

        /// <summary>Initializes a new instance of the <see cref="SceneManager" /> class.</summary>
        /// <param name="scenes">The scenes.</param>
        [JsonConstructor]
        public SceneManager(List<Scene> scenes)
        {
            this.scenes = scenes ?? new List<Scene> { new Scene("Default") };

            OnCreate += SceneManager_OnCreate;
            OnAddScene += SceneManager_OnAddScene;
            OnDeleteScene += SceneManager_OnDeleteScene;
            OnLoadScene += SceneManager_OnLoadScene;
            OnDestroy += SceneManager_OnDestroy;

            OnCreate.Invoke(null, true);

            currentScene = scenes[0];

            current = this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneManager"/> class.
        /// </summary>
        public SceneManager()
        {
            this.scenes = new List<Scene>{new Scene("Default")};

            OnCreate += SceneManager_OnCreate;
            OnAddScene += SceneManager_OnAddScene;
            OnDeleteScene += SceneManager_OnDeleteScene;
            OnLoadScene += SceneManager_OnLoadScene;
            OnDestroy += SceneManager_OnDestroy;

            OnCreate.Invoke(null, true);

            currentScene = scenes[0];

            current = this;
        }


        /// <summary>Finalizes an instance of the <see cref="SceneManager" /> class.</summary>
        ~SceneManager()
        {
            GC.Collect();
            OnDestroy?.Invoke(null, true);
            Debug.Log("Memory used after full collection: {" + GC.GetTotalMemory(true) + "}");
        }

        /// <summary>Adds the scene.</summary>
        /// <param name="scene">The scene.</param>
        public void Add(Scene scene)
        {
            if (!scenes.Contains(scene))
            {
                scenes.Add(scene);
                OnAddScene.Invoke(null, true);
            }
        }

        /// <summary>Deletes the scene.</summary>
        /// <param name="scene">The scene.</param>
        public void Delete(Scene scene)
        {
            if (scenes.Contains(scene)) 
            {
                scenes.Remove(scene);
                OnDeleteScene.Invoke(null, true);
            }
        }

        /// <summary>Exitses the specified scene.</summary>
        /// <param name="scene">The scene.</param>
        /// <returns>Return true if exits a scene on the videogame.</returns>
        public bool Exits(Scene scene) 
        {
            Debug.Log("Check if exits the scene: " + scene.Name);
            return scenes.Contains(scene);
        }

        /// <summary>Loads the scene.</summary>
        /// <param name="name">The name.</param>
        public static void LoadScene(string name) 
        {
            current.currentScene = current.scenes.Find(i => i.Name.Equals(name));
            current.OnLoadScene.Invoke(null, true);
        }

        /// <summary>Scenes the manager on create.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">if set to <c>true</c> [e].</param>
        private void SceneManager_OnCreate(object sender, bool e) => Debug.EventLog("Create new " + this.GetType() + " instancie. {" + this.GetHashCode() + "}");

        /// <summary>Scenes the manager on destroy.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">if set to <c>true</c> [e].</param>
        private void SceneManager_OnDestroy(object sender, bool e) => Debug.EventLog("Destroy " + this.GetType() + " instancie. {" + this.GetHashCode() + "}");

        /// <summary>Scenes the manager on load scene.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">if set to <c>true</c> [e].</param>
        private void SceneManager_OnLoadScene(object sender, bool e) => Debug.EventLog("Load scene");

        /// <summary>Scenes the manager on delete scene.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">if set to <c>true</c> [e].</param>
        private void SceneManager_OnDeleteScene(object sender, bool e) => Debug.EventLog("Delete scene");

        /// <summary>Scenes the manager on add scene.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">if set to <c>true</c> [e].</param>
        private void SceneManager_OnAddScene(object sender, bool e) => Debug.EventLog("Add scene");
    }
}
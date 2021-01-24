﻿//----------------------------------------------------------------------------------------------------
// <author>Pablo Perdomo Falcón</author>
// <copyright file="ProjectCreator.cs" company="Pabllopf">GNU General Public License v3.0</copyright>
//----------------------------------------------------------------------------------------------------
namespace Alis.Editor.UI.Widgets
{
    using Alis.Tools;
    using ImGuiNET;
    using System;
    using System.IO;
    using System.Numerics;
    using System.Text;

    /// <summary>Create new project. </summary>
    public class ProjectCreator : Widget
    {
        /// <summary>The name</summary>
        private readonly string name = "ProjectCreator";

        /// <summary>The is open</summary>
        private bool isOpen = true;

        /// <summary>The is select2 d</summary>
        private bool isSelect2D = false;

        private string[] modes = new string[] { "2D Videogame -SFML Core- " };

        /// <summary>The item space</summary>
        private Vector2 itemSpace = new Vector2(-10.0f, 3.0f);

        /// <summary>The event handler</summary>
        private EventHandler<EventType> eventHandler;

        private string nameBuffer;

        /// <summary>The buffer</summary>
        private string pathBuffer;

        private Vector2 sizeMasterChild = new Vector2(550, 200);
        private Vector2 sizeChildLeft = new Vector2();
        private Vector2 sizeChildRight = new Vector2();

        private Vector2 createButton = new Vector2();

        /// <summary>Initializes a new instance of the <see cref="ProjectCreator" /> class.</summary>
        public ProjectCreator(EventHandler<EventType> eventHandler) 
        {
            this.eventHandler = eventHandler;

            nameBuffer = "AlysProject";
            pathBuffer = Application.DesktopPath.Replace("\\", "/");

            current_item = modes[0];
        }

        /// <summary>Load this instance.</summary>
        public override void OnLoad()
        {
            
        }

        
        string current_item = null;


        private bool state;

        private string contentDir;


        /// <summary>Draw this instance.</summary>
        public override void Draw()
        {
            if (!isOpen)
            {
                eventHandler?.Invoke(this, EventType.CloseCreatorProject);
                return;
            }

            if (isOpen)
            {
                ImGui.OpenPopup("New Project");
            }

            if (ImGui.BeginPopupModal("New Project", ref isOpen, ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoSavedSettings))
            {
                ImGui.Text("Name: ");
                ImGui.PushItemWidth(ImGui.GetContentRegionAvail().X);
                ImGui.InputText("##Name", ref nameBuffer, 256);
                ImGui.PopItemWidth();

                

                ImGui.Text("Location: ");
                ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(0, 3));
                ImGui.PushItemWidth(ImGui.GetContentRegionAvail().X - 27.0f);
                ImGui.BeginGroup();
                ImGui.InputText("##Path", ref pathBuffer, 256);
                ImGui.SameLine();
                createButton.X = 27.0f;
                createButton.Y = 27.0f;
                if (ImGui.Button("...", createButton))
                {
                }
                ImGui.PopStyleVar();
                ImGui.PopItemWidth();
                ImGui.EndGroup();




                ImGui.Text("Solution: ");
                ImGui.PushItemWidth(ImGui.GetContentRegionAvail().X);
                if (ImGui.BeginCombo("##Mode", current_item))
                {
                    for (int n = 0; n < modes.Length; n++)
                    {
                        bool is_selected = (current_item == modes[n]);
                        if (ImGui.Selectable(modes[n], is_selected))
                        {
                            current_item = modes[n];
                        }

                        if (is_selected)
                        {
                            ImGui.SetItemDefaultFocus();
                        }
                    }
                    ImGui.EndCombo();
                }
                ImGui.PopItemWidth();

                createButton.X = sizeMasterChild.X;
                createButton.Y = 45.0f;
                if (ImGui.Button("Create", createButton))
                {
                    CreateProject(nameBuffer, pathBuffer, current_item);
                    Close();
                }
            }
            ImGui.EndPopup();
        }

        private void CreateProject(string name, string directory, string current_item)
        {
            if (Directory.Exists(directory + "/" + name))
            {
                Debug.Error("The project(" + directory + "/" + name + ") already exists.");
            }
            else 
            {
                Directory.CreateDirectory(directory + "/" + name);
                Directory.CreateDirectory(directory + "/" + name + "/Assets");
                Directory.CreateDirectory(directory + "/" + name + "/Config");

                string content = File.ReadAllText(Application.ProjectPath + "/Resources/DefaultPr.txt", Encoding.UTF8);
                File.WriteAllText(directory + "/" + name + "/" + name + ".csproj", content, Encoding.UTF8);

                content = File.ReadAllText(Application.ProjectPath + "/Resources/DefaultSl.txt", Encoding.UTF8);
                content = content.Replace("Example", name);
                File.WriteAllText(directory + "/" + name + "/" + name + ".sln", content, Encoding.UTF8);
            }
        }

        /// <summary>Opens this instance.</summary>
        public override void Open()
        {
            isOpen = true;
        }

        /// <summary>Close this instance.</summary>
        public override void Close()
        {
            isOpen = false;
        }

        /// <summary>Gets the name.</summary>
        /// <returns>Return name widget</returns>
        public override string GetName()
        {
            return name;
        }
    }
}

using GameEngine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WoutersRevenge
{
    public class LevelImporter
    {
        public Vector2 StartLocation;

        public LevelImporter()
        {

        }

        public Scene Import(string fileName)
        {
            Scene scene = new Scene();

            using (StreamReader sr = new StreamReader(fileName))
            {
                string line = sr.ReadLine();

                while(!string.IsNullOrEmpty(line))
                {
                    string[] data = line.Split(',');

                    string objectName = data[0];
                    Vector2 position = new Vector2(float.Parse(data[1]), float.Parse(data[2]));
                    
                    switch(objectName)
                    {
                        case "BiggerWouter":
                            StartLocation = position;
                            break;
                        case "Enemy1":
                            scene.Add(new Enemy(position));
                            break;
                        case "platformBlock":                           
                            scene.Add(new Platform(position));
                            break;
                    }

                    line = sr.ReadLine();
                }

            }

            return scene;
        }
    }
}

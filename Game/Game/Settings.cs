using System;
using System.Collections.Generic;
using System.IO;

namespace Game
{
    class Settings
    {
        private static string FilePath { get; } = "Settings.txt";
        public int[] WindowSize { get; set; } //Свойство
        public int WindowHeight { get; set; }
        public int WindowWidth { get; set; }
        public static string WindowName { get; set; } = "2 Find";
        public float Scaling { get; set; }
        public bool VSync { get; set; }
        public bool Fulscreen { get; set; }
        public bool Sound { get; set; }
        public bool Scene { get; set; }
        
        public Settings() 
        {
            if (!File.Exists(FilePath))
                ToDefaultSettings();

            StreamReader sr = new StreamReader(FilePath);
            try
            {
                string[] width = sr.ReadLine().Split("Width =");
                WindowWidth = Convert.ToInt32(width[1].Trim(' '));

                string[] height = sr.ReadLine().Split("Height =");
                WindowHeight = Convert.ToInt32(height[1].Trim(' '));

                string[] vsync = sr.ReadLine().Split("VSync =");
                VSync = Convert.ToBoolean(vsync[1].Trim(' '));

                string[] scale = sr.ReadLine().Split("Scale =");
                Scaling = (float)Convert.ToDouble(scale[1].Trim(' '));

                string[] sound = sr.ReadLine().Split("Sound =");
                Sound = Convert.ToBoolean(sound[1].Trim(' '));

                string[] scene = sr.ReadLine().Split("Scene =");
                Scene = Convert.ToBoolean(scene[1].Trim(' '));

                sr.Close();

            }
            catch
            {
                sr.Close();
                ToDefaultSettings(); 
            }
        }
        
        void ToDefaultSettings()
        {
            WindowWidth = 1366;
            WindowHeight = 768;
            VSync = true;
            Scaling = 1;
            Scene = true;
            Sound = true;
            WriteSettingsToFile();
        }
        public void WriteSettingsToFile()
        {
            StreamWriter sw = new StreamWriter(FilePath, false);
            sw.WriteLine($"Width = {WindowWidth}");
            sw.WriteLine($"Height = {WindowHeight}");
            sw.WriteLine($"VSync = {VSync}");
            sw.WriteLine($"Scale = {Scaling}");
            sw.WriteLine($"Sound = {Sound}");
            sw.WriteLine($"Scene = {Scene}");
            sw.Close();
        }
    }
}

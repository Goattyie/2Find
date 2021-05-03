using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2._0
{
    class Settings
    {
        public int[] WindowSize { get; set; } //Свойство
        public int WindowHeight { get; set; }
        public int WindowWidth { get; set; }
        public string WindowName { get; set; }
        public bool Scaling { get; set; }
        public bool VSync { get; set; }
        public bool Fulscreen { get; set; }
        

        private Settings() { }
        public Settings(int window_width, int window_height, string window_name)
        {
            WindowHeight = window_height;
            WindowWidth = window_width;
            WindowSize = new int[] { window_width, window_height };
            WindowName = window_name;
            Scaling = false;
            VSync = false;
        }
        public Settings(int window_width, int window_height, string window_name, bool scaling)
        {
            WindowHeight = window_height;
            WindowWidth = window_width;
            WindowSize = new int[] { window_width, window_height };
            WindowName = window_name;
            Scaling = scaling;
            VSync = false;
        }
        public Settings(int window_width, int window_height, string window_name, bool scaling, bool vsync)
        {
            WindowHeight = window_height;
            WindowWidth = window_width;
            WindowSize = new int[] { window_width, window_height };
            WindowName = window_name;
            Scaling = scaling;
            VSync = vsync;
        }

    }
}

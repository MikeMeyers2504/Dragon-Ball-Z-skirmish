using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SdlDotNet.Core;
using SdlDotNet.Graphics;
using System.Drawing;

namespace Consoleapplicatie_gamedev
{
    public abstract class level
    {
        //Hier word de klasse aangemaakt voor het level
        protected Surface video;
        public level(Surface video)
        {
            this.video = video;
        }

        //In deze array word het level aangemaakt met alle 0 en 1
        public int[,] intTileArray;

        //Deze arrays dienen voor de platformen
        public Cloud[,] spriteCloudArray;
        public gras[,] spriteTileArray;

        //Hier word de wereld daangemaakt
        protected abstract void CreateWorld();

        //Deze methodes dienen om de wereld te tekenen voor de levels
        public void DrawWorld()
        {
            for (int i = 0; i < spriteTileArray.GetLength(0); i++)
            {
                for (int j = 0; j < spriteTileArray.GetLength(1); j++)
                {
                    if (spriteTileArray[i, j] != null)
                    {
                        spriteTileArray[i, j].Draw();
                    }
                }
            }
        }
        public void DrawWorldSecond()
        {
            for (int i = 0; i < spriteCloudArray.GetLength(0); i++)
            {
                for (int j = 0; j < spriteCloudArray.GetLength(1); j++)
                {
                    if (spriteCloudArray[i, j] != null)
                    {
                        spriteCloudArray[i, j].Draw();
                    }
                }
            }
        }
    }
}

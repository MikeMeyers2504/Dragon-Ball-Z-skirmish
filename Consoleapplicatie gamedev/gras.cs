using System;
using System.Collections.Generic;
using System.Linq;
using SdlDotNet.Core;
using SdlDotNet.Graphics;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Consoleapplicatie_gamedev
{
    public class gras
    {
        //Deze klasse is voor het gras van level1
        Surface video;
        Surface image;
        Point position;
        public  Rectangle col;

        public gras(Surface video, Point position)
        {
            //Hier wordt een surface aangeroepen en een collision voor te zien of je er tege bent 
            this.video = video;
            this.position = position;
            image = new Surface("grass.png");
            col = new Rectangle(position.X, position.Y, 20, 10);
        }


        public void Draw()
        {
            //Hier worden ze getekend op het scherm
            video.Blit(image, position);
        }
    }
}

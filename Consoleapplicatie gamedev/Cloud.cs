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
    public class Cloud
    {
        //Deze klasse is voor de wolken van level2
        Surface video;
        Surface image;
        Point position;
        public Rectangle colCloud;

        public Cloud(Surface video, Point position)
        {
            //Hier wordt een surface aangeroepen en een collision voor te zien of je er tege bent 
            this.video = video;
            this.position = position;
            image = new Surface("cloud2.png");
            colCloud = new Rectangle(position.X, position.Y, 20, 10);
        }


        public void Draw()
        {
            //Hier worden ze getekend op het scherm
            video.Blit(image, position);
        }
    }
}

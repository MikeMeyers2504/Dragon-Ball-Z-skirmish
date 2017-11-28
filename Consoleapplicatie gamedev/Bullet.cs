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
    class Bullet : BeweegbaarObject
    {
        //Deze klasse is voor de kogel
        //Hier worden de rechthoeken aangemaakt om ze te laten zien en ook de collision 
        public Rectangle visibleRectangleBall;
        public Rectangle colBullet;

        public Bullet(Surface video, int x, int y) : base(video)
        {
            //Hier wordt een surface aangeroepen en een rechthoek om deze in te laten zien
            position = new Point(x, y);
            bullet = new Surface("Gohan/gohan shot.png");
            visibleRectangleBall = new Rectangle(0, 0, 29, 25);
            colBullet = new Rectangle(0, 0, 29, 25);
        }

        public override void Draw()
        {
            //Hier wordt het getekend op het scherm
            video.Blit(bullet, position, visibleRectangleBall);
        }

        public override void Update()
        {
            //Dit zorgt ervoor dat de kogel telkens naar rechts beweegt, samen met zijn collision
                position.X += 15;
                colBullet.X += 15;
        }
    }
}



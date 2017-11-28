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
    class Health:BeweegbaarObject
    {
        //Deze klasse is voor het leven van de hero
        public Health(Surface video, int x, int y) : base(video)
        {
            //Hier wordt een surface aangeroepen en een rechthoek om deze in te laten zien
            position = new Point(x, y);
            HealthPlayer = new Surface("health enemy.png");
            visibleRectangle = new Rectangle(0, 0, 136, 33);
        }

        public override void Draw()
        {
            //Hier wordt het getekend op het scherm
            video.Blit(HealthPlayer, position, visibleRectangle);
        }

        public override void Update()
        {
            //Hier gaat de rechthoek van het leven telkens met 1 verminderen zodat er leven afgaat
            visibleRectangle.Width -= 1;
        }
    }
}

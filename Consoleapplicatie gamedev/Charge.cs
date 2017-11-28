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
    class Charge:BeweegbaarObject
    {

        public Charge(Surface video, int x, int y) : base(video)
        {
            //Hier wordt een surface aangeroepen en een rechthoek om deze in te laten zien
            position = new Point(x, y);
            ChargePlayer = new Surface("chargebar.png");
            visibleRectangleTen = new Rectangle(0, 0, 50, 40);
        }

        public override void Draw()
        {
            //Hier wordt het getekend
            video.Blit(ChargePlayer, position, visibleRectangleTen);
        }

        public override void Update()
        {
            //Hier wordt er nagegaan welke aanval er actief is en hoeveel er dan af moet van de chargebar en ook nog zien dat er genoeg is om die aanval te kunnen starten
            if (tellerChargePlayer == 1)
            {
                if (visibleRectangleTen.Width <= 399)
                {
                    visibleRectangleTen.Width += 3; 
                }
            }
            else if (tellerChargePlayer == 2 && visibleRectangleTen.Width >= 200)
            {
                visibleRectangleTen.Width -= 200;
            }
            else if (tellerChargePlayer == 3 && visibleRectangleTen.Width >= 150)
            {
                visibleRectangleTen.Width -= 150;
            }
            else if (tellerChargePlayer == 4 && visibleRectangleTen.Width >= 50)
            {
                visibleRectangleTen.Width -= 50;
            }
            else if (tellerChargePlayer == 5 && visibleRectangleTen.Width >= 50)
	        {
                visibleRectangleTen.Width -= 50;
	        }
            else if (tellerChargePlayer == 6 && visibleRectangleTen.Width >= 350)
            {
                visibleRectangleTen.Width -= 350;
            }
        }
    }
}

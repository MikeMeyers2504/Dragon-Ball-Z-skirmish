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
    class HealthEnemies:BeweegbaarObject
    {
        //Deze dient om de afbeelding in te tekenen 
        public Rectangle visibleRectangleOne;

        public HealthEnemies(Surface video, int x, int y) : base(video)
        {
            //Hier wordt een surface aangeroepen en een rechthoek om deze in te laten zien
            position = new Point(x, y);
            HealthEnemy = new Surface("health bar enemy.png");
            visibleRectangleOne = new Rectangle(0, 0, 100, 14);
        }

        public override void Draw()
        {
            //Hier wordt het getekend op het scherm
            video.Blit(HealthEnemy, position, visibleRectangleOne);
        }

        public override void Update()
        {
            //Hier gaat de rechthoek van de levens telkens verminderen zodat er leven afgaat en hier word er ook gekozen hoeveel damage het doet
            if (CounterMeleeOrSuper == 2)
            {
                visibleRectangleOne.Width -= 100;
            }

            if (CounterMeleeOrSuper == 1)
            {
                visibleRectangleOne.Width -= 3;
            }

            if (CounterMeleeOrSuper == 3)
            {
                visibleRectangleOne.Width -= 50;
            }

            if (CounterMeleeOrSuper == 4)
            {
                visibleRectangleOne.Width -= 5;
            }

            if (CounterMeleeOrSuper == 5)
            {
                visibleRectangleOne.Width -= 1;
            }

            if (CounterMeleeOrSuper == 6)
            {
                visibleRectangleOne.Width -= 100;
            }
               
        }
    }
}

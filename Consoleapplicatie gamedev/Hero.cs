using SdlDotNet.Core;
using SdlDotNet.Graphics;
using SdlDotNet.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consoleapplicatie_gamedev
{
    public class Hero: BeweegbaarObject
    {
        //Is de snelheid waarmee je naar beneden valt. 
        private int yVelocity = 10;
        //Hier worden de rechthoeken aangemaakt om de sprite in te laten zien
        private Rectangle visibleRectangleRight;
        private Rectangle visibleRectangleLeft;
        public Rectangle visibleRectangleOne;
        private Rectangle visibleRectangleTwo;
        private Rectangle visibleRectangleThree;
        private Rectangle visibleRectangleCharge;
        private Rectangle visibleRectangleKick;
        public Rectangle visibleRectangleShot;
        //Hier worden de booleans aangemaakt die telkens zeggen of iets waar is of niet
        public bool left;
        public bool right;
        public bool Charge;
        //private bool Guard;
        public bool Shot;
        public bool melee;
        public bool kick;
        public bool SuperpowerSecond;
        public bool hit;
        public bool hitUp;
        public bool hitUnder;
        public bool hitLeft;
        public bool hitRight;

        //Deze waardes dienen voor het springen, G is hoe hoog de hero kan springen
        public bool Jump;
        public bool Air;
        int G = 30;
        int force;
        public bool fall;
        //Hier wordt een surface gemaakt waar niets in zit, dus wanneer een sprite actief is kan ik die in deze surface steken
        private Surface Active;


        public Hero(Surface video):base(video)
        {
            //hero gewoon staan aangemaakt
            image = new Surface("Gohan/gohan stand.png");
            position = new Point(800, 700);

            //collisions aangemaakt van de hero en level 
            colRectangle = new Rectangle(position.X, position.Y, 27, 76);//54, 76
            colRectangleLevel = new Rectangle(position.X, position.Y, 27, 76);
            colLinks = new Rectangle(position.X, position.Y, 27, 60);
            colRechts = new Rectangle(position.X, position.Y, 27, 60);
            colBoven = new Rectangle(position.X, position.Y, 27, 60);

            //collisions aangemaakt van de aanvallen
            colSuperpower = new Rectangle(position.X, position.Y, 53, 80);
            colSuperpowerSecond = new Rectangle(position.X, position.Y, 39, 73);
            colSuperpowerMelee = new Rectangle(position.X, position.Y, 55, 73);
            colSuperpowerKick = new Rectangle(position.X, position.Y, 70, 73);

            //Hier worden alle afbeeldingen ingeladen en de rechthoeken aangemaakt die de sprites laten zien
            WalkRight = new Surface("Gohan/gohan walking 2.png");
            visibleRectangleRight = new Rectangle(0,0,42,77);

            WalkLeft = new Surface("Gohan/gohan walking left 2.png");
            visibleRectangleLeft = new Rectangle(0, 0, 40, 77);

            Charging = new Surface("Gohan/gohan chargings.png");
            visibleRectangleCharge = new Rectangle(0,0,60,70);

            Jumping = new Surface("Gohan/gohan up.png");
            JumpingRight = new Surface("Gohan/gohan jumping right.png");
            JumpingLeft = new Surface("Gohan/gohan jumping left.png");

            Falling = new Surface("Gohan/gohan falling.png");
            FallingRight = new Surface("Gohan/gohan falling right.png");
            FallingLeft = new Surface("Gohan/gohan falling left.png");

            superpower = new Surface("Gohan/gohan superpower.png");
            visibleRectangleOne = new Rectangle(0, 0, 53, 80);

            superpowerSecond = new Surface("Gohan/gohan superpower 2.png");
            visibleRectangleTwo = new Rectangle(0, 0, 39, 73);

            Melee = new Surface("Gohan/gohan melee.png");
            visibleRectangleThree = new Rectangle(0, 0, 55, 73);

            Kick = new Surface("Gohan/gohan kick.png");
            visibleRectangleKick = new Rectangle(0, 0, 70, 73);

            shot = new Surface("Gohan/shot.png");
            visibleRectangleShot = new Rectangle(0, -8, 71, 67);

            //Deze worden aangemaakt om het toestenbord te kunnen gebruiken 
            Events.KeyboardDown += Events_KeyboardDown;
            Events.KeyboardUp += Events_KeyboardUp;

        }

        private void Events_KeyboardDown(object sender, SdlDotNet.Input.KeyboardEventArgs e)
        {
            //Deze dienen voor te checken of dat de knoppen van het toetsenbord zijn ingedrukt
            if (e.Key == SdlDotNet.Input.Key.LeftArrow)
                left = true;
            if (e.Key == SdlDotNet.Input.Key.RightArrow)
                right = true;
            if (e.Key == SdlDotNet.Input.Key.UpArrow)
                Charge = true;
            if (e.Key == SdlDotNet.Input.Key.Keypad1)
            {
                tellerChargePlayer = 0;
            }
            if (e.Key == SdlDotNet.Input.Key.Keypad2)
            {
                tellerChargePlayer = 0;
            }
            if (e.Key == SdlDotNet.Input.Key.Keypad3)
            {
                tellerChargePlayer = 0;
            }
            if (e.Key == SdlDotNet.Input.Key.Keypad4)
            {
                tellerChargePlayer = 0;
            }
            if (e.Key == SdlDotNet.Input.Key.Keypad5)
            {
                tellerChargePlayer = 0;
            } 
            if (Jump != true)
            {
                if (e.Key == SdlDotNet.Input.Key.Space && fall == false)
                {
                    Jump = true;
                    force = G;
                }
            }
        }

        //Deze booleans dienen voor de sprites te laten bewegen in een vloeiende animatie
        public bool AnimSuperpowerSecond = false;
        public bool AnimShot = false;
        public bool Animmelee = false;
        public bool Animkick = false;

        void Events_KeyboardUp(object sender, SdlDotNet.Input.KeyboardEventArgs e)
        {
            //Deze dienen voor te checken of dat de knoppen van het toetsenbord zijn losgelaten en dan zeggen dat die aanvallen true zijn
            Charge = Jump = false;
            if (e.Key == SdlDotNet.Input.Key.LeftArrow)
                left = false;
            if (e.Key == SdlDotNet.Input.Key.RightArrow)
                right = false;
            if (e.Key == SdlDotNet.Input.Key.Keypad1 /*&& SuperpowerShotCharge == true*/)
            {
                Shot = true;
                AnimShot = true;
                Ball = true;
            }
            if (e.Key == SdlDotNet.Input.Key.Keypad4 && SuperpowerCharge == true)
            {
                Superpower = true;
                AnimSuperpower = true;
            }
            if (e.Key == SdlDotNet.Input.Key.Keypad5 && SuperpowerSecondCharge == true)
            {
                SuperpowerSecond = true;
                AnimSuperpowerSecond = true;
            }
            if (e.Key == SdlDotNet.Input.Key.Keypad2 && SuperpowerMeleeCharge == true)
            {
                melee = true;
                Animmelee = true;
            }
            if (e.Key == SdlDotNet.Input.Key.Keypad3 && SuperpowerKickCharge == true)
            {
                kick = true;
                Animkick = true;
            }
        }

        public override void Update()
        {
            //Deze 3 ifs dienen voor het contact met de platformen in het level, dus wat er dan moet gebeuren
            if (hitUp == true || hitUp == true && hitLeft == true || hitUp == true && hitRight == true)
            {
                Jump = false;
            }
            if (hitLeft == true && hitRight == false)
            {
                position.X = position.X + 5;
            }
            if (hitRight == true && hitLeft == false)
            {
                position.X = position.X - 5;
            }

            //De volgende ifs dienen om na te gaan of je naar links of rechts gaat, je aan het chargen bent , welke aanval je aan het doen bent of gewoon niets doet
            //Het laat dan de sprite animatie zien door de rechthoeken telkens op te schuiven zodat het een vloeiende animatie is
            //De framescounter dient voor dat de aanval frame independent is van het spel zelf 
            if (left == true && position.X >= 1 && hitLeft == false)
            {
                Active = WalkLeft;
                position.X -= xVelocity;
                visibleRectangleLeft.X += 40;
                if (visibleRectangleLeft.X >= 120)
                {
                    visibleRectangleLeft.X = 0;
                }
                right = false;
            }
            else if (right == true && position.X < video.Width -40 && hitRight == false)
            {
                Active = WalkRight;
                position.X += xVelocity;
                visibleRectangleRight.X += 42;
                if (visibleRectangleRight.X >= 167)
                {
                    visibleRectangleRight.X = 0;
                }
                left = false;
            }
            else if (Charge == true) 
            {
                Active = Charging;
                visibleRectangleCharge.X += 60;
                if (visibleRectangleCharge.X >= 240)
                {
                    visibleRectangleCharge.X = 0;
                }
            }
            else if (AnimShot == true)
            {
                Active = shot;
                framesCounter += 2;
                if (framesCounter % 20 == 0)
                {
                    AnimShot = false;
                }
            }
            else if (Animmelee == true && position.Y < video.Height - 76)
            {
                Active = Melee;
                framesCounter += 2;
                if (framesCounter % 10 == 0)
                {
                    visibleRectangleThree.X += 55;
                    colSuperpowerMelee.X += 55;
                }
                if (visibleRectangleThree.X >= 273)
                {
                    visibleRectangleThree.X = 0;
                    colSuperpowerMelee.X = 0;
                    melee = false;
                    Animmelee = false;
                }
            }
            else if (Animkick == true && position.Y < video.Height - 76)
            {
                Active = Kick;
                framesCounter += 2;
                if (framesCounter % 10 == 0)
                {
                    visibleRectangleKick.X += 70;
                    colSuperpowerKick.X += 70;
                }
                if (visibleRectangleKick.X >= 420)
                {
                    visibleRectangleKick.X = 0;
                    colSuperpowerKick.X = 0;
                    kick = false;
                    Animkick = false;
                }
            }
            else if (AnimSuperpower == true && position.Y < video.Height - 76)
            {
                Active = superpower;
                framesCounter += 2;
                if (framesCounter %10 == 0)
                {
                    visibleRectangleOne.X += 51;
                    colSuperpower.X += 51;
                }
               
                if (visibleRectangleOne.X == 306)
                {
                    visibleRectangleOne.Width = 314;
                    colSuperpower.Width = 314;
                }
                else if(visibleRectangleOne.X > 306){
                     Superpower = false;
                     visibleRectangleOne.X = 0;
                     colSuperpower.X = 0;
                     AnimSuperpower = false;
                }   
                else
                {
                    visibleRectangleOne.Width = 48;
                    colSuperpower.Width = 48;  
                } 
            }

            else if (AnimSuperpowerSecond == true && position.Y < video.Height - 76)
            {
                Active = superpowerSecond;
                framesCounter += 2;
                if (framesCounter % 10 == 0)
                {
                    visibleRectangleTwo.X += 39;
                    colSuperpowerSecond.X += 39;
                }
                if (visibleRectangleTwo.X == 195)
                {
                    visibleRectangleTwo.Width = 276;
                    colSuperpowerSecond.Width = 276;

                }
                else if (visibleRectangleTwo.X > 195)
                {
                    SuperpowerSecond = false;
                    visibleRectangleTwo.X = 0;
                    colSuperpowerSecond.X = 0;
                    AnimSuperpowerSecond = false;
                }
                else
                {
                    visibleRectangleTwo.Width = 39;
                    colSuperpowerSecond.Width = 39;
                }
            }
            else
            {
                image = new Surface("Gohan/gohan stand.png");
            }

            //De volgende ifs dienen voor te springen
            //Je kan naar links of rechts springen
            //Air controleert of je in de lucht bent
            //Hier gebeurt ook het vallen 
            //En als je niets doet is het gewoon de standaard afbeelding
            if (Jump == true)
            {
                Air = true;
                if (right == true)
                {
                    Active = JumpingRight;
                }
                else if (left == true)
                {
                    Active = JumpingLeft;
                }
                else
                {
                    Active = Jumping;
                }
                if (force <= 0)
                {
                    Jump = false;
                    fall = true;
                }
                position.Y -= force;
                force -= 2;
                //fall = false;
            }
            else if (fall == true && Jump == false)
            {
                //falling
                if (right == true)
                {
                    Active = FallingRight;
                }
                else if (left == true)
                {
                    Active = FallingLeft;
                }
                else
                {
                    Active = Falling;
                }
                Jump = false;
                position.Y += 20;

                if (hit != true)
                {
                    position.Y += yVelocity;
                    colRectangle.Y += yVelocity;
                }
            }
            else
            {
                Active = image;
            }
            if (Jump == false && fall == false && Air == true)
            {
                fall = true;
            }

            //Hier worden alle collisions geupdate zodat ze de hero meevolgen
            colRectangle.X = position.X;
            colRectangle.Y = position.Y;
            colRectangleLevel.X = position.X;
            colRectangleLevel.Y = position.Y;
            colLinks.X = position.X;
            colLinks.Y = position.Y;
            colRechts.X = position.X;
            colRechts.Y = position.Y;
            colBoven.X = position.X;
            colBoven.Y = position.Y - 25;
            colSuperpower.X = position.X;
            colSuperpower.Y = position.Y;
            colSuperpowerSecond.X = position.X;
            colSuperpowerSecond.Y = position.Y;
            colSuperpowerKick.X = position.X;
            colSuperpowerKick.Y = position.Y;
            colSuperpowerMelee.X = position.X;
            colSuperpowerMelee.Y = position.Y;
        }

        public override void Draw()
        {
            //Hier worden alle sprites van de hero getekend met voorwaarden wanneer ze wel of niet mogen getekend worden
            if (AnimSuperpower == false && Animmelee == false && right == false && left == false && Charge == false && Jump == false && fall == false && Animkick == false && AnimShot == false && AnimSuperpowerSecond == false)
            {
                Events.Fps = 20;
                video.Blit(image, position);
            }
            if (AnimSuperpower == true && Animmelee == false && right == false && left == false && Charge == false && Jump == false && fall == false && AnimSuperpowerSecond == false && Animkick == false && AnimShot == false)
            {
                video.Blit(superpower, position, visibleRectangleOne);
            }
            if (AnimSuperpowerSecond == true && Animmelee == false && right == false && left == false && Charge == false && Jump == false && fall == false && Animkick == false && AnimShot == false && AnimSuperpower == false)
            {
                video.Blit(superpowerSecond, position, visibleRectangleTwo);
            }
            if (Animmelee == true && AnimSuperpower == false && right == false && left == false && Charge == false && Jump == false && fall == false && AnimSuperpowerSecond == false && Animkick == false && AnimShot == false)
            {
                video.Blit(Melee, position, visibleRectangleThree);
            }
            if (right == true && Jump == false && fall == false && left == false)
            {
                video.Blit(WalkRight, position, visibleRectangleRight);
            }
            else if (left == true && Jump == false && fall == false && right == false)
            {
                video.Blit(WalkLeft, position, visibleRectangleLeft);
            }
            if (Charge == true && AnimSuperpower == false && Animmelee == false && right == false && left == false && Jump == false && fall == false && AnimSuperpowerSecond == false && Animkick == false && AnimShot == false)
            {
                video.Blit(Charging, position, visibleRectangleCharge);
            }
            if (Jump == true)
            {
                if (right == true)
                {
                    video.Blit(JumpingRight, position);
                }
                else if (left == true)
                {
                    video.Blit(JumpingLeft, position);
                }
                else
                {
                    video.Blit(Jumping, position);
                }
            }
            if (fall == true && Jump == false)
            {
                if (right == true)
                {
                    video.Blit(FallingRight, position);
                }
                else if (left == true)
                {
                    video.Blit(FallingLeft, position);
                }
                else
                {
                    video.Blit(Falling, position);
                }
            }
            if (Animkick == true && AnimSuperpower == false && right == false && left == false && Charge == false && Jump == false && fall == false && Animmelee == false && AnimSuperpowerSecond == false && AnimShot == false)
            {
                video.Blit(Kick, position, visibleRectangleKick);
            }
            if (AnimShot == true && AnimSuperpower == false && right == false && left == false && Charge == false && Jump == false && fall == false && Animmelee == false && Animkick == false && AnimSuperpowerSecond == false)
            {
                video.Blit(shot, position, visibleRectangleShot);
            }
        }
    }
}

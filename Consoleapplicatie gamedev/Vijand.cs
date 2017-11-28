using System;
using SdlDotNet.Core;
using SdlDotNet.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Consoleapplicatie_gamedev
{
    class Vijand : BeweegbaarObject
    {
        //Hier worden de rechthoeken aangemaakt om de sprite in te laten zien
        private Rectangle visibleRectangleEleven;
        private Rectangle visibleRectangleOne;
        //deze booleans dienen voor links, rechts en de aanvallen
        private bool Left;
        private bool Right;
        public bool Psybeam;
        public bool Elektrikschok;
        //Hier wordt een surface gemaakt waar niets in zit, dus wanneer een sprite actief is kan ik die in deze surface steken
        private Surface Active;

        public Vijand(Surface video, int x, int y) : base(video)
        {
            //xVelocity dient voor de snelheid om te stappen
            //collisions worden hier aangemaakt van de aanvallen
            //Hier worden alle afbeeldingen ingeladen en de rechthoeken aangemaakt die de sprites laten zien
            position = new Point(x, y);
            xVelocity = 5;
            image = new Surface("CellJr/CellJr walking.png");
            imageTwo = new Surface("CellJr/CellJr walking left.png");
            visibleRectangleOne = new Rectangle(0, 0, 43, 57);
            visibleRectangleEleven = new Rectangle(0, 0, 43, 57);
            //colVijand1 = new Rectangle(position.X, position.Y, 44, 57);

            PsyBeamCell = new Surface("CellJr/CellJr. psybeam.png");
            visibleRectanglePsyBeamCell = new Rectangle(0, -10, 40, 80);

            elektrikSchokCell = new Surface("CellJr/CellJr elektrik schok.png");
            visibleRectangleElektrikschokCell = new Rectangle(0, 20, 69, 105);

            colPsyBeam = new Rectangle(position.X, position.Y - 10, 40, 80);
            colElektrikShok = new Rectangle(position.X, position.Y + 20, 69, 105);
        }

        public override void Draw()
        {
            //Hier worden alle sprites van de vijanden getekend met voorwaarden wanneer ze wel of niet mogen getekend worden
            if (Elektrikschok == false && Psybeam == false)
            {
                Side(); 
            }
            else if (Elektrikschok == true)
            {
                Elektrik();
            }
            else if (Psybeam == true && Elektrikschok == false)
            {
                Psy();
            }
        }

        public void Side()
        {
            //Deze gaat na welke kant hij stapt en laat dan de juiste afbeelding zien
            if (xVelocity < 0)
            {
                video.Blit(imageTwo, position, visibleRectangleEleven);
            }
            else
            {
                video.Blit(image, position, visibleRectangleOne);
            }
        }

        public void Elektrik()
        {
            video.Blit(elektrikSchokCell, position, visibleRectangleElektrikschokCell);
        }

        public void Psy()
        {
            video.Blit(PsyBeamCell, position, visibleRectanglePsyBeamCell);
        }
        public override void Update()
        {
            //Hier gebruik ik ook een framescounter zodat de sprite animatie niet zo snel gaan
            //Deze if dient voor de elektrische schok van de vijand en deze dan te laten zien
            if (Elektrikschok == true)
            {
                Active = elektrikSchokCell;
                framesCounter += 2;
                if (framesCounter % 30 == 0)
                {
                    visibleRectangleElektrikschokCell.X += 69;
                    colElektrikShok.X += 69;
                }
                else if (visibleRectangleElektrikschokCell.X == 276)
                {
                    visibleRectangleElektrikschokCell.Width = 86;
                    colElektrikShok.Width = 150;

                }
                else if (visibleRectangleElektrikschokCell.X > 276)
                {
                    Elektrikschok = false;
                    visibleRectangleElektrikschokCell.X = 0;
                    colElektrikShok.X = 0;
                }
                else
                {
                    visibleRectangleElektrikschokCell.Width = 69;
                    colElektrikShok.Width = 69;
                } 
            }
            //Deze if dient voor de psybeam van de vijand en deze dan te laten zien
            else if (Psybeam == true)
            {
                Active = PsyBeamCell;
                framesCounter += 2;
                if (framesCounter % 40 == 0)
                {
                    visibleRectanglePsyBeamCell.X += 40;
                    colPsyBeam.X += 40;
                }
                else if (visibleRectanglePsyBeamCell.X == 80)
                {
                    visibleRectanglePsyBeamCell.Width = 257;
                    colPsyBeam.Width = 257;
                }
                else if (visibleRectanglePsyBeamCell.X > 80)
                {
                    Psybeam = false;
                    visibleRectanglePsyBeamCell.X = 0;
                    colPsyBeam.X = 0;
                }
                else
                {
                    visibleRectanglePsyBeamCell.Width = 40;
                    colPsyBeam.Width = 40;
                }
            }
            //Deze if dient voor dat ze stappen naar links of rechts en wanneer ze moeten omdraaien en welke vijand tot waar stapt
            else if (Elektrikschok == false && Psybeam == false)
            {
                if (position.X >= 0 && position.X <= 1460)
                {
                    if (position.Y == 70)
                    {
                        if ((position.X > 1075) || (position.X < 730))
                        {
                            xVelocity = -xVelocity;
                            Left = true;
                        }
                        position.X += xVelocity;
                        Right = true;
                    }
                    else if (position.Y == 340)
                    {
                        if ((position.X > 1175) || (position.X < 635))
                        {
                            xVelocity = -xVelocity;
                            Left = true;
                        }
                        position.X += xVelocity;
                        Right = true;
                    }
                    else if (position.Y == 490)
                    {
                        if ((position.X > 230) || (position.X < 10))
                        {
                            xVelocity = -xVelocity;
                            Left = true;
                        }
                        position.X += xVelocity;
                        Right = true;
                    }
                    else if (position.Y == 610)
                    {
                        if ((position.X > 1450) || (position.X < 850))
                        {
                            xVelocity = -xVelocity;
                            Left = true;
                        }
                        position.X += xVelocity;
                        Right = true;
                    }
                    else if (position.Y == 730)
                    {
                        if ((position.X > 1450) || (position.X < 10))
                        {
                            xVelocity = -xVelocity;
                            Left = true;
                        }
                        position.X += xVelocity;
                        Right = true;
                    }
                    else if (position.Y == 611)
                    {
                        if ((position.X > 1075) || (position.X < 620))
                        {
                            xVelocity = -xVelocity;
                            Left = true;
                        }
                        position.X += xVelocity;
                        Right = true;
                    }
                    else if (position.Y == 491)
                    {
                        if ((position.X > 750) || (position.X < 140))
                        {
                            xVelocity = -xVelocity;
                            Left = true;
                        }
                        position.X += xVelocity;
                        Right = true;
                    }
                    else if (position.Y == 71)
                    {
                        if ((position.X > 1220) || (position.X < 415))
                        {
                            xVelocity = -xVelocity;
                            Left = true;
                        }
                        position.X += xVelocity;
                        Right = true;
                    }
                    else if (position.Y == 250)
                    {
                        if ((position.X > 1200) || (position.X < 800))
                        {
                            xVelocity = -xVelocity;
                            Left = true;
                        }
                        position.X += xVelocity;
                        Right = true;
                    }
                }
                //Deze laat de animatie zien naar links 
                if (position.X >= 0 && Left == true)
                {
                    visibleRectangleEleven.X += 45;
                    if (visibleRectangleEleven.X >= 172)
                    {
                        visibleRectangleEleven.X = 0;
                    }
                }
                //Deze laat de animatie zien naar rechts 
                if (position.X >= 0 && Right == true)
                {
                    visibleRectangleOne.X += 43;
                    if (visibleRectangleOne.X >= 172)
                    {
                        visibleRectangleOne.X = 0;
                    }
                }
            }
            //Hier worden alle collisions geupdate zodat ze de vijanden meevolgen
            colVijand1.X = position.X;
            colVijand1.Y = position.Y;
            colVijand2.X = position.X;
            colVijand2.Y = position.Y;
            colVijand3.X = position.X;
            colVijand3.Y = position.Y;
            colVijand4.X = position.X;
            colVijand4.Y = position.Y;
            colVijand5.X = position.X;
            colVijand5.Y = position.Y;
            colVijand6.X = position.X;
            colVijand6.Y = position.Y;
            colVijand7.X = position.X;
            colVijand7.Y = position.Y;
            colVijand8.X = position.X;
            colVijand8.Y = position.Y;
            colVijand9.X = position.X;
            colVijand9.Y = position.Y;
            colVijand10.X = position.X;
            colVijand10.Y = position.Y;
            colPsyBeam.X = position.X;
            colPsyBeam.Y = position.Y;
            colElektrikShok.X = position.X - 30;
            colElektrikShok.Y = position.Y;
        }
    }
}

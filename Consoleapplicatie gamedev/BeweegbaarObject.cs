using System;
using SdlDotNet.Core;
using SdlDotNet.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Consoleapplicatie_gamedev
{
    public abstract class BeweegbaarObject
    {
        //Deze int zijn voor de tellers en de snelheid
        protected int xVelocity = 5;
        public int tellerHealthPlayer = 0;
        public int tellerChargePlayer = 0;
        public int CounterMeleeOrSuper = 0;
        //Hier worden surfaces gemaakt en een point
        public Point position;
        public Surface video;
        public Surface image;
        //Dit zijn booleand die dienen voor wat de naam zelf zegt
        public bool Ball = false;
        public bool Superpower;
        public bool AnimSuperpower;

        //Dit zijn de booleans om te zien of dat de charge bar zijn breedte genoeg is om de aanval te kunnen starten
        public bool SuperpowerCharge;
        public bool SuperpowerSecondCharge;
        public bool SuperpowerMeleeCharge;
        public bool SuperpowerKickCharge;
        public bool SuperpowerShotCharge;

        //Dit zijn allemaal surfaces van de hero en vijanden
        protected Surface imageTwo;
        protected Surface superpower;
        protected Surface superpowerSecond;
        protected Surface shot;
        protected Surface bullet;
        protected Surface getHit;
        protected Surface Melee;
        protected Surface Kick;
        protected Surface WalkLeft;
        protected Surface WalkRight;
        protected Surface Charging;
        protected Surface ChargingCellJr;
        protected Surface Guarding;
        protected Surface Jumping;
        protected Surface JumpingRight;
        protected Surface JumpingLeft;
        protected Surface Falling;
        protected Surface FallingRight;
        protected Surface FallingLeft;
        protected Surface HealthPlayer;
        protected Surface ChargePlayer;
        protected Surface HealthEnemy;

        //Surface Animaties van de vijand zitten hier
        protected Surface PsyBeamCell;
        protected Surface KickLeft;
        protected Surface elektrikSchokCell;
        protected Surface elektrikSchokCellLeft;
        public Rectangle visibleRectanglePsyBeamCell;
        public Rectangle visibleRectangleKickCellLeft;
        public Rectangle visibleRectangleElektrikschokCell;
        public Rectangle visibleRectangleElektrikschokCellLeft;

        //Alle collisions van de hero, level en vijand zitten hier
        public Rectangle colBoven;
        public Rectangle colLinks;
        public Rectangle colRechts;
        public Rectangle colRectangle;
        public Rectangle colRectangleLevel;
        public Rectangle colVijand1;
        public Rectangle colVijand2;
        public Rectangle colVijand3;
        public Rectangle colVijand4;
        public Rectangle colVijand5;
        public Rectangle colVijand6;
        public Rectangle colVijand7;
        public Rectangle colVijand8;
        public Rectangle colVijand9;
        public Rectangle colVijand10;

        //Collisions van de aanvallen van hero en vijand
        public Rectangle colSuperpower;
        public Rectangle colSuperpowerSecond;
        public Rectangle colSuperpowerMelee;
        public Rectangle colSuperpowerKick;
        public Rectangle colPsyBeam;
        public Rectangle colElektrikShok;

        //teller voor de framerate independant van de animaties, zodat de sprite niet te snel beweegt
        public int framesCounter = 0;
        //Dit zijn de rechthoeken die worden aangemaakt
        public Rectangle visibleRectangleTen;
        public Rectangle visibleRectangle;

        public BeweegbaarObject(Surface video)
        {
            this.video = video;
        }
        //Deze codes kunnen de klasses die overerven telkens overschrijven 
        public abstract void Update();

        public abstract void Draw();
    }
}

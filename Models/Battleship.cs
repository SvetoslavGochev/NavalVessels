namespace NavalVessels.Models
{
    using NavalVessels.Models.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Battleship : Vessel, IBattleship
    {
        private const double defoultArmorThickness = 300;

        private bool sonarMode = false;

        public Battleship(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, defoultArmorThickness)
        {
            this.ArmorThickness = defoultArmorThickness;
        }

        public bool SonarMode
        {
            get => sonarMode;
            private set => sonarMode = value;
        }

        public  void ToggleSonarMode()
        {
            if (sonarMode)
            {
                sonarMode = false;
                this.MainWeaponCaliber -= 40;
                this.Speed += 5;
            }
            else
            {
                sonarMode=true;
                this.MainWeaponCaliber += 40;
                this.Speed -= 5;

            }
        }
        public override void RepairVessel()
        {
            if (this.ArmorThickness < defoultArmorThickness)
            {
                this.ArmorThickness = defoultArmorThickness;
            }
        }

        public override string ToString()
        {
            var sonarMode = this.SonarMode == true ? "On" : "OFF";
            var sonarText = Environment.NewLine + $" *Sonar mode: {sonarMode}";

            return base.ToString() + sonarText.Trim();
        }
    }
}

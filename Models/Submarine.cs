namespace NavalVessels.Models
{
    using NavalVessels.Models.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Submarine : Vessel, ISubmarine
    {
        private const double defoultArmorThickness = 200;
        private bool submergeMode = false;

        public Submarine(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, defoultArmorThickness)
        {
            //this.ArmorThickness = defoultArmorThickness;
        }

        public bool SubmergeMode
        {
            get => this.submergeMode;
            set => submergeMode = value;
        }

        public void ToggleSubmergeMode()
        {
            if (submergeMode)
            {
                this.submergeMode = false;
                this.MainWeaponCaliber += 40;
                this.Speed -= 4;
            }
            else
            {
                this.submergeMode = true;
                this.MainWeaponCaliber -= 40;
                this.Speed += 4;
            }
        }

        public override string ToString()
        {
            var subMerge = this.SubmergeMode == true ? "ON" : "OFF";
            var subtext = $" *Submerge mode: {subMerge}";

            return base.ToString() + subtext.Trim();
        }
    }
}

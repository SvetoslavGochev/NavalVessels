﻿namespace NavalVessels.Models
{
    using NavalVessels.Models.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Utilities.Messages;
    internal abstract class Vessel : IVessel
    {
        private string name;
        private ICaptain captain;
        private double armorThickness;
        private double mainWeaponCaliber;
        private double speed;
        //private ICollection<string> targets;

        protected Vessel(string name, ICaptain captain, double armorThickness, double mainWeaponCaliber, double speed)
        {
            this.Name = name;
            this.Captain = captain;
            this.ArmorThickness = armorThickness;
            this.MainWeaponCaliber = mainWeaponCaliber;
            this.Speed = speed;
            this.Targets = new List<string>();
        }

        public string Name
        {
            get => this.name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(Utilities.Messages.ExceptionMessages.InvalidCaptainName);
                }
                this.name = value;
            }
        }

        public ICaptain Captain
        {
            get => this.captain;
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException(Utilities.Messages.ExceptionMessages.InvalidCaptainToVessel);
                }
                this.captain = value;
            }
        }
        public double ArmorThickness { get; set; }

        public double MainWeaponCaliber { get; }

        public double Speed { get; }

        public ICollection<string> Targets { get; set; }
        public void Attack(IVessel target)
        {
            if (target == null)
            {
                throw new NullReferenceException(Utilities.Messages.ExceptionMessages.InvalidTarget);
            }

            target.ArmorThickness -= this.mainWeaponCaliber;

            if (target.ArmorThickness < 0)
            {
                target.ArmorThickness = 0;
            }

            this.Targets.Add(target.Name);
        }

        public void RepairVessel()
        {
           // armorThickness = max;
        }

        public virtual string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"- {this.Name}");
            sb.AppendLine($"*Type: {typeof(Vessel)}");
            sb.AppendLine($"*Main weapon caliber: {this.mainWeaponCaliber}");
            sb.AppendLine($"Speed: {this.Speed} knots");
            if (this.Targets.Any())
            {
                foreach (var target in this.Targets)
                {

                }
            }
            //sb.AppendLine($"*Targets: " – if there are no targets "None" Otherwise print "{target1}, {target2}, {target3}, {targetN}"");
            
            return sb.ToString().TrimEnd();
        }
    }
}

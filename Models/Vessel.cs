namespace NavalVessels.Models
{
    using NavalVessels.Models.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Utilities.Messages;
    public abstract class Vessel : IVessel
    {
        private string name;
        private ICaptain captain = new Captain("name");
        private double armorThickness;
        private double mainWeaponCaliber;
        private double speed;
        private ICollection<string> targets = new List<string>();   

        public Vessel(string name, double mainWeaponCaliber, double speed, double armorThickness)
        {
            this.Name = name;
            this.Captain = captain;
            this.ArmorThickness = armorThickness;
            this.MainWeaponCaliber = mainWeaponCaliber;
            this.Speed = speed;
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

        public double MainWeaponCaliber { get; protected set; }

        public double Speed { get; protected set; }

        public ICollection<string> Targets { get => this.targets; }

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

        public virtual void RepairVessel()
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
            if (this.Targets.Count == 0)
            {
                sb.AppendLine("*Targets: None");
            }
            else
            {
                sb.AppendLine($"*Targets: {String.Join(", ", this.Targets)}");
            }
            //sb.AppendLine($"*Targets: " – if there are no targets "None" Otherwise print "{target1}, {target2}, {target3}, {targetN}"");

            return sb.ToString().Trim();
        }
    }
}

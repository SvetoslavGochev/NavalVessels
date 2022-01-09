namespace NavalVessels.Models
{
    using NavalVessels.Models.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class Vessel : IVessel
    {
        private string name;
        private ICaptain captain;
        private double armorThickness;
        private double mainWeaponCaliber;
        private double speed;
        private ICollection<string> targets;

        protected Vessel()
        {
            this.Name = Name;
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
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Error");
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
                    throw new NullReferenceException("Captain cannot be null.");
                }
                this.captain = value;
            }
        }
        public double ArmorThickness { get; set; }

        public double MainWeaponCaliber { get; }

        public double Speed { get; }

        public ICollection<string> Targets { get; set; } = new HashSet<string>();
        public void Attack(IVessel target)
        {
            if (target == null)
            {
                throw new NullReferenceException("Target cannot be null");
            }

        }

        public void RepairVessel()
        {
            throw new NotImplementedException();
        }
    }
}

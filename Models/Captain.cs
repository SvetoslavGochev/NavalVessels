namespace NavalVessels.Models
{
    using NavalVessels.Models.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Captain : ICaptain
    {
        private string fullName;
        private int combatExperiance = 0;
        private ICollection<IVessel> vessels = new List<IVessel>();

        public Captain(string fullName)
        {
            this.FullName = fullName;
            this.CombatExperience = combatExperiance;
        }

        public string FullName
        {
            get => this.fullName;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(Utilities.Messages.ExceptionMessages.InvalidCaptainName);
                }
                this.fullName = value;
            }
        }

        public int CombatExperience
        {
            get { return this.combatExperiance;}
            set
            {
                this.combatExperiance = value;
            }
        }

        public ICollection<IVessel> Vessels { get => this.vessels; } 

        public void AddVessel(IVessel vessel)
        {
            if (vessel == null)
            {
                throw new NullReferenceException(Utilities.Messages.ExceptionMessages.InvalidVesselForCaptain);
            }
            this.vessels.Add(vessel);
        }
      

        public void IncreaseCombatExperience()
        {
            
            this.CombatExperience += 10;
        }

        public string Report()
        { 
            var sb = new StringBuilder();
            var result = $"{this.fullName} has {this.combatExperiance} combat experience and commands {this.vessels.Count} vessels.";
            sb.AppendLine(result);
            if (this.vessels.Any())
            {
                foreach (var vessel in this.vessels)
                {
                    var vesselTargets = vessel.Targets.Any() ? String.Join(", ", vessel.Targets) : "None";

                    sb.AppendLine($"- {vessel.Name}");
                    sb.AppendLine($"*Type: {typeof(Vessel)}");
                    sb.AppendLine($"*Armor thickness: {vessel.ArmorThickness}");
                    sb.AppendLine($" *Main weapon caliber: {vessel.MainWeaponCaliber}");
                    sb.AppendLine($"Speed: {vessel.Speed} knots ");
                    sb.AppendLine($"*Targets: {vesselTargets}");
                    sb.AppendLine($"*Sonar/Submerge mode: {"???"}");
                }
            }

            return sb.ToString().Trim();
        }
    }
}

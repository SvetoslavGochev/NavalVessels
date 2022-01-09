namespace NavalVessels.Core.Contracts
{
    using NavalVessels.Models;
    using NavalVessels.Models.Contracts;
    using NavalVessels.Repositories.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Controller : IController
    {
        private IRepository<IVessel> vessels;
        private ICollection<ICaptain> captains = new List<ICaptain>();
        public Controller()
        {
            this.Captains = new List<ICaptain>();
        }

        ICollection<ICaptain> Captains { get; set; }

        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
        {
            var result = string.Empty;

            var vessel = this.vessels.FindByName(selectedVesselName);//null or vessel

            var captain = this.captains.FirstOrDefault(x => x.FullName == selectedCaptainName);//null or captain

            var currCaptainVessel = this.captains.FirstOrDefault(x => x.Vessels.Any(x => x.Name == selectedVesselName));


            if (captain == null)
            {
                result = $"{Utilities.Messages.OutputMessages.CaptainNotFound},{selectedCaptainName}";
            }
            else if (vessel == null)
            {
                result = $"{Utilities.Messages.OutputMessages.VesselNotFound},{selectedVesselName}";
            }
            else if (currCaptainVessel != null)
            {
                result = $"{Utilities.Messages.OutputMessages.VesselOccupied},{selectedVesselName}";

            }

            /*essel.Captain.FullName == selectedCaptainName;*/

            vessel.Captain = captain;

            this.vessels.Add(vessel);

            result = $"{Utilities.Messages.OutputMessages.SuccessfullyAssignCaptain},{selectedCaptainName}{selectedVesselName}";

            return result;
        }

        public string AttackVessels(string attackingVesselName, string defendingVesselName)
        {
            var atackVessel = this.vessels.FindByName(attackingVesselName);
            var defendVessel = this.vessels.FindByName(defendingVesselName);

            var result = string.Empty;

            if (atackVessel == null)
            {
                return $"Vessel {attackingVesselName} could not be found.";

            }
            else if (defendVessel == null)
            {
                return $"Vessel {defendingVesselName} could not be found.";
            }


            if (atackVessel.ArmorThickness == 0)
            {
                return $"Unarmored vessel {atackVessel.Name} cannot attack or be attacked.";

            }
            else if (defendVessel.ArmorThickness == 0)
            {
                return $"Unarmored vessel {defendVessel.Name} cannot attack or be attacked.";

            }

            atackVessel.Attack(defendVessel);

            atackVessel.Captain.IncreaseCombatExperience();
            defendVessel.Captain.IncreaseCombatExperience();

            result = $"Vessel {defendingVesselName} was attacked by vessel {attackingVesselName}-current armor thickness: {defendVessel.ArmorThickness}.";


            return result;
        }


        public string CaptainReport(string captainFullName)
        {
            var currVessel = this.vessels.Models.FirstOrDefault(x => x.Name == captainFullName);

            var currCaptain = currVessel.Captain;

            var result = currCaptain.Report();

            return result;
        }

        public string HireCaptain(string fullName)
        {
            var captain = this.Captains.FirstOrDefault(x => x.FullName == fullName);
            if (captain != null)
            {
                return $"Captain {fullName} is hired.";
            }
            return $"Captain {fullName} is already hired.";

        }

        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
            var result = string.Empty;

            IVessel vessel;
            vessel = this.vessels.FindByName(name);
            if (vessel == null)
            {
                return $"{vesselType} vessel {name} is already manufactured.";
            }
            else if (vesselType == typeof(Submarine).ToString())
            {
                vessel = new Submarine(name, mainWeaponCaliber, speed);

            }
            else if (vesselType == typeof(Battleships).ToString())
            {
                vessel = new Battleships(name, mainWeaponCaliber, speed);
            }
            else
            {
                return Utilities.Messages.OutputMessages.InvalidVesselType;

            }
            result = $"{Utilities.Messages.OutputMessages.SuccessfullyCreateVessel}{vessel.GetType()},{vessel.Name}, {vessel.MainWeaponCaliber}, {vessel.Speed}";

            return result;
        }
        public string ServiceVessel(string vesselName)
        {
            var vesselForRepear = this.vessels.FindByName(vesselName);
            var result = string.Empty;

            if (vesselForRepear != null)
            {
                vesselForRepear.RepairVessel();
                result = $"Vessel {vesselName} was repaired.";
            }
            else
            {
                result = $"Vessel {vesselName} could not be found.";
            }

            return result;
        }

        public string ToggleSpecialMode(string vesselName)
        {
            var result = string.Empty;

            var currVessel = this.vessels.FindByName(vesselName);

            var typeOfVessel = currVessel.GetType().ToString();

            IVessel batleship;

            if (currVessel == null)
            {
                result = $"{Utilities.Messages.OutputMessages.VesselNotFound},{vesselName}";
            }

            if (typeOfVessel == nameof(Battleships))
            {

            }
            else
            {

            }


            return result;
        }

        public string VesselReport(string vesselName)
        {
            var currVessel = this.vessels.FindByName(vesselName);

            var result = currVessel.ToString();

            return result;
        }
    }
}

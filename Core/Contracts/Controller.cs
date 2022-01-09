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
            throw new NotImplementedException();
        }

        public string AttackVessels(string attackingVesselName, string defendingVesselName)
        {
            throw new NotImplementedException();
        }

        public string CaptainReport(string captainFullName)
        {
            throw new NotImplementedException();
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
            else if(vesselType == typeof(Submarine).ToString())
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

        private IVessel GetVessel(string name)
        {
            var vessel = this.vessels.FindByName(name);

            return vessel;

        }



        public string ServiceVessel(string vesselName)
        {
            throw new NotImplementedException();
        }

        public string ToggleSpecialMode(string vesselName)
        {
            throw new NotImplementedException();
        }

        public string VesselReport(string vesselName)
        {
            throw new NotImplementedException();
        }
    }
}

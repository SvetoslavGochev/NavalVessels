namespace NavalVessels.Core.Contracts
{
    using NavalVessels.Models;
    using NavalVessels.Models.Contracts;
    using NavalVessels.Repository;
    using NavalVessels.Repository.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Controller : IController
    {
        private Repository vessels = new Repository();

        private ICollection<ICaptain> captains = new List<ICaptain>();



        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
        {
            var result = string.Empty;

            var vessel = this.vessels.FindByName(selectedVesselName);//null or vessel

            var captain = this.captains.FirstOrDefault(x => x.FullName == selectedCaptainName);//null or captain

            var currCaptainVessel = this.captains.FirstOrDefault(x => x.Vessels.Any(x => x.Name == selectedVesselName));


            if (captain == null)
            {
                return $"Captain {selectedCaptainName} could not be found.";
                return result;
            }
            else if (vessel == null)
            {
                return $"Vessel {selectedVesselName} could not be found.";
                return result;
            }
            else if (currCaptainVessel != null)
            {
                result = $"{Utilities.Messages.OutputMessages.VesselOccupied},{selectedVesselName}";
                return result;

            }

            /*essel.Captain.FullName == selectedCaptainName;*/

            vessel.Captain = captain;

            this.vessels.Add(vessel);

            result = $"{Utilities.Messages.OutputMessages.SuccessfullyAssignCaptain},{selectedCaptainName}{selectedVesselName}";

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
            var captain = this.captains.FirstOrDefault(x => x.FullName == fullName);
            if (captain == null)
            {
                var newCaptain = new Captain(fullName);

                this.captains.Add(newCaptain);

                return $"Captain {fullName} is hired.";
            }

            return $"Captain {fullName} is already hired.";
        }

        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
            var result = string.Empty;

            var vessel = this.vessels.FindByName(name);

            if (vessel != null)
            {
                return $"{vesselType} vessel {name} is already manufactured.";
            }
            else if (vesselType == nameof(Submarine))
            {
                vessel = new Submarine(name, mainWeaponCaliber, speed);
                this.vessels.Add(vessel);

                return $"{Utilities.Messages.OutputMessages.SuccessfullyCreateVessel}{vessel.GetType()},{vessel.Name}, {vessel.MainWeaponCaliber}, {vessel.Speed}";

            }
            else if (vesselType == nameof(Battleship))
            {
                vessel = new Battleship(name, mainWeaponCaliber, speed);
                this.vessels.Add(vessel);

                return $"{Utilities.Messages.OutputMessages.SuccessfullyCreateVessel}{vessel.GetType()},{vessel.Name}, {vessel.MainWeaponCaliber}, {vessel.Speed}";
            }
            else
            {
                return Utilities.Messages.OutputMessages.InvalidVesselType;

            }
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


            Battleship batleship;

            if (currVessel == null)
            {
                result = $"{Utilities.Messages.OutputMessages.VesselNotFound},{vesselName}";
                return result;
            }

            var typeOfVessel = currVessel.GetType().ToString();

            if (typeOfVessel == nameof(Battleship))
            {
                batleship = new Battleship(vesselName, currVessel.MainWeaponCaliber, currVessel.Speed);
                batleship.ToggleSonarMode();
                result = $"Battleship {vesselName} toggled sonar mode.";
                return result;
            }
            else
            {
                var submarine = new Submarine(currVessel.Name, currVessel.MainWeaponCaliber, currVessel.Speed);
                submarine.ToggleSubmergeMode();
                result = $"Submarine {vesselName} toggled submerge mode.";
                return result;
            }

            return result;
        }

        public string VesselReport(string vesselName)
        {
            var currVessel = this.vessels.FindByName(vesselName);

             return currVessel.ToString();

           
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
    }
}

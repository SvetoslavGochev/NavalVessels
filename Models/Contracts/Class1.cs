namespace NavalVessels.Models.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal class Class1 : IVessel
    {
        string IVessel.Name => throw new NotImplementedException();

        ICaptain IVessel.Captain { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        double IVessel.ArmorThickness { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        double IVessel.MainWeaponCaliber => throw new NotImplementedException();

        double IVessel.Speed => throw new NotImplementedException();

        ICollection<string> IVessel.Targets => throw new NotImplementedException();

        void IVessel.Attack(IVessel target)
        {
            throw new NotImplementedException();
        }

        void IVessel.RepairVessel()
        {
            throw new NotImplementedException();
        }

        string IVessel.ToString()
        {
            throw new NotImplementedException();
        }
    }
}

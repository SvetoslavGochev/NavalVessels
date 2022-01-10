namespace NavalVessels.Repository
{
    using NavalVessels.Models.Contracts;
    using NavalVessels.Repository.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class VesselRepository : IRepository<IVessel>
    {
        private List<IVessel> models = new List<IVessel>();

        public VesselRepository()
        {
        }

        public IReadOnlyCollection<IVessel> Models
            => this.models.AsReadOnly();

        public void Add(IVessel model)
        {
            this.models.Add(model);
        }

        public IVessel FindByName(string name)
        {
            var vessel = this.models.FirstOrDefault(x => x.Name == name);

            if (vessel != null)
            {
                return vessel;
            }
            else
            {
                return null;
            }

        }
        public bool Remove(IVessel model)
        {
            if (model == null)
            {
                return false;
            }
            return this.models.Remove(model);
        }
    }


}

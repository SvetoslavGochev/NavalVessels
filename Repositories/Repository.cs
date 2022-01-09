namespace NavalVessels.Repository
{
    using NavalVessels.Models.Contracts;
    using NavalVessels.Repository.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Repository : IRepository<IVessel>
    {
        private ICollection<IVessel> models = new HashSet<IVessel>();

        public Repository()
        {

        }

        public IReadOnlyCollection<IVessel> Models => models.ToList().AsReadOnly();

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
            return this.models.Remove(model);
        }
    }


}

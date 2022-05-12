using DevTrackr.API.Entities;

namespace DevTrackr.API.Persistance
{
    public class DevTrackRContext
    {
        public DevTrackRContext()
        {
            Packages = new List<Package>();
        }

        public List<Package> Packages {get ; set;}
    }
}
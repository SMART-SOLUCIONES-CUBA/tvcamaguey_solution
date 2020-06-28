using SmartSolucionesCuba.SAPRESSC.Syncronization.Api.Common.Models;

namespace SmartSolucionesCuba.TVCamaguey.Cloud.Internet.Helpers.Comparing
{
    public class EntryChangeApiModelComparingHelper : System.Collections.Generic.IComparer<EntryChangeModel>
    {
        public int Compare(EntryChangeModel x, EntryChangeModel y)
        {
            if (x.SerializationTargetClass == y.SerializationTargetClass) return 0;

            if (x.SerializationTargetClass.StartsWith("SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence"))
            {
                return -1;
            }

            if (y.SerializationTargetClass.StartsWith("SmartSolucionesCuba.SAPRESSC.Middleware.Media.Persistence"))
            {
                return 1;
            }

            if (x.SerializationTargetClass.StartsWith("SmartSolucionesCuba.TVCamaguey.Cloud.Persistence"))
            {
                return 1;
            }

            if (y.SerializationTargetClass.StartsWith("SmartSolucionesCuba.TVCamaguey.Cloud.Persistence"))
            {
                return -1;
            }

            return 0;
        }
    }
}

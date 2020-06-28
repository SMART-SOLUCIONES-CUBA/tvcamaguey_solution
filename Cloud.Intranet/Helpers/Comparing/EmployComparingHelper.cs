namespace SmartSolucionesCuba.TVCamaguey.Cloud.Intranet.Helpers.Comparing
{
    public class EmployComparingHelper : System.Collections.Generic.IEqualityComparer<Cloud.Persistence.Entities.Employ>
    {
        public bool Equals(Cloud.Persistence.Entities.Employ x, Cloud.Persistence.Entities.Employ y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Cloud.Persistence.Entities.Employ obj)
        {
            return obj.GetHashCode();
        }
    }
}

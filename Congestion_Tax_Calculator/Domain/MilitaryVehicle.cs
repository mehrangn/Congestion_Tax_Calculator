namespace Congestion_Tax_Calculator.Domain
{
    public class MilitaryVehicle : Vehicle
    {   
        public MilitaryVehicle() : base(VehicleType.MilitaryVehicles)
        { 
        }

        public override string GetVehicleType()
        {
            return VehicleType.MilitaryVehicles.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congestion_Tax_Calculator.Domain;

public class Motorbike : Vehicle
{
    public Motorbike() : base(VehicleType.Motorbike)
    { 
    }

    public override string GetVehicleType()
    {
        return VehicleType.Motorbike.ToString();
    }
}
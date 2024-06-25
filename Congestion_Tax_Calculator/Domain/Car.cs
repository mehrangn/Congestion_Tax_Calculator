using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congestion_Tax_Calculator.Domain;

public class Car : Vehicle
{
    public Car() : base(VehicleType.Car)
    {
    }

    public override string GetVehicleType()
    {
        return VehicleType.ToString();
    }
}
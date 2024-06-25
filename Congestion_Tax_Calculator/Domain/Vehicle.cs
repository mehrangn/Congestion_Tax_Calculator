using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congestion_Tax_Calculator.Domain;
public abstract class Vehicle : IVehicle
{
    public Guid Id { get; set; }
    public VehicleType VehicleType { get; set; }
    public string PlateNumber { get; set; }

    public abstract string GetVehicleType();

    public void SetPlateNumber(string plateNumber)
    {
        PlateNumber = plateNumber.Trim();
    }

    protected Vehicle(VehicleType vehicleType)
    {
        VehicleType = vehicleType;
    }
}
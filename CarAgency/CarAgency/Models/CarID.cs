using System;

namespace CarAgency.Models
{
    public class CarID
    {
        public int CarNumber { get; }

        public CarID(int carNumber)
        {
            CarNumber = carNumber;
        }

        public override string ToString()
        {
            return $"{CarNumber}";
        }

        public override bool Equals(object obj)
        {
            return obj is CarID carID &&
                CarNumber == carID.CarNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CarNumber);
        }

        public static bool operator ==(CarID carID1, CarID carID2)
        {
            if (carID1 is null && carID2 is null)
            {
                return true;
            }

            return !(carID1 is null) && carID1.Equals(carID2);
        }

        public static bool operator !=(CarID carID1, CarID carID2)
        {
            return !(carID1 == carID2);
        }

    }
}

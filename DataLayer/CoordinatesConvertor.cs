using BusinessLayer;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataLayer
{
    internal class CoordinatesConvertor : ValueConverter<Coordinates, byte[]>
    {
        public CoordinatesConvertor()
            : base(
                coordinates => Coordinates.ToBytes(coordinates),
                bytes => Coordinates.FromBytes(bytes)) { }
    }
}

using MapHandling.TerrainData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapHandling.MapTypes
{
    public class MapSquare
    {
        //Private Properties
        //public Properties
        public ITerrain Terrain { get; private set; }
        //Static Properties
        private static Dictionary<char, Func<ITerrain>> CharToTerrain;
        //Static Constructors
        static MapSquare()
        {
            CharToTerrain = new Dictionary<char, Func<ITerrain>>()
            {
                {'P',()=>new PlainTerrain() },
                {'A',()=>new AirportTerrain() },
                {'C',()=>new CityTerrain() },
                {'F',()=>new FactoryTerrain() },
                {'M',()=>new MountainTerrain() },
                {'N',()=>new NoneTerrain() },
                {'R',()=>new RoadTerrain() },
                {'S',()=>new ShipyardTerrain() },
                {'B',()=>new BeachTerrain() },
                {'W',()=>new WaterTerrain() }
            };
        }
        //Static Methods
        public static ITerrain GetTerrain(char repChar) => CharToTerrain[repChar]();
        //Constructors
        public MapSquare()
        {
            Terrain = new NoneTerrain();
        }
        public MapSquare(char repChar)
        {
            Terrain = CharToTerrain[repChar]();
        }
        //Set Methods
        public void SetTerrain(ITerrain terrain)
        {
            Terrain = terrain;
        }
        //Object Override Methods
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != Terrain.GetType())
            {
                return false;
            }
            return true;
        }
        public override int GetHashCode()
        {
            return Terrain.CharRep.GetHashCode();
        }



    }
}

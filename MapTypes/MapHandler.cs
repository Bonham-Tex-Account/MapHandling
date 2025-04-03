using MapHandling.TerrainData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapHandling.MapTypes
{
    public class MapHandler
    {
        //Private Properties
        private const int DEFAULTMAPSIZE = 10;
        private MapSquare[,] map;
        //Public Properties
        public int Size { get; private set; }
        //Constructors
        public MapHandler()
        {
            Size = DEFAULTMAPSIZE;
            map = new MapSquare[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    map[i, j] = new MapSquare();
                }
            }
        }
        public MapHandler(int mapSize)
        {
            if (mapSize <= 0)
            {
                throw new InvalidOperationException();
            }
            this.Size = mapSize;
            map = new MapSquare[mapSize, mapSize];
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    map[i, j] = new MapSquare();
                }
            }
        }
        public MapHandler(int mapSize, ReadOnlySpan<char> loadedData)
        {
            if (mapSize * mapSize != loadedData.Length)
            {
                throw new InvalidDataException();
            }
            this.Size = mapSize;
            map = new MapSquare[mapSize, mapSize];
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    map[i, j] = new MapSquare(loadedData[i * mapSize + j]);
                }
            }
        }
        public MapHandler(int mapSize, string loadedData)
        {
            if (mapSize * mapSize != loadedData.Length)
            {
                throw new InvalidDataException();
            }
            this.Size = mapSize;
            map = new MapSquare[mapSize, mapSize];
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    map[i, j] = new MapSquare(loadedData[i * mapSize + j]);
                }
            }
        }
        public MapHandler(int mapSize, char[] loadedData)
        {
            if (mapSize * mapSize != loadedData.Length)
            {
                throw new InvalidDataException();
            }
            this.Size = mapSize;
            map = new MapSquare[mapSize, mapSize];
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    map[i, j] = new MapSquare(loadedData[i * mapSize + j]);
                }
            }
        }
        //Get Methods
        public ITerrain GetSquareTerrain(int x, int y) => map[x, y].Terrain;
        public ITerrain GetTerrain(char reChar) => MapSquare.GetTerrain(reChar);
        public MapSquare this[int x, int y]
        {
            get => map[x, y];
            set => map[x, y] = value;
        }
        //Manipulation Methods
        public void ChangeRange(ITerrain terrain, int xMin, int xMax, int yMin, int yMax)
        {
            if (xMin < 0 || yMin < 0 || xMax > Size || yMax > Size)
            {
                throw new IndexOutOfRangeException();
            }
            for (int i = xMin; i <= xMax; i++)
            {
                for (int j = yMin; j <= yMax; j++)
                {
                    map[i, j].SetTerrain(terrain);
                }
            }
        }
        public void ChangeRow(ITerrain terrain, int row)
        {
            for (int i = 0; i < Size; i++)
            {
                map[row, i].SetTerrain(terrain);
            }
        }
        public void ChangeColumn(ITerrain terrain, int column)
        {
            for (int i = 0; i < Size; i++)
            {
                map[i, column].SetTerrain(terrain);
            }
        }
        //Object Override Methods
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }
            MapHandler compHandler = (MapHandler)obj;
            if(this.Size!=compHandler.Size)
            {
                return false;
            }
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (!compHandler[i, j].Equals(map[i,j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public override int GetHashCode()
        {
            int code = 0;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    code += map[i, j].GetHashCode()*Size;
                }
            }
            return code;
        }
        public override string ToString()
        {
            char[] output = new char[Size * Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    output[i * Size + j] = map[i, j].Terrain.CharRep;
                }
            }
            return new String(output);
        }

    }
}

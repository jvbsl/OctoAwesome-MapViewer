using OctoAwesome;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapLoader
{
    [Serializable]
    public class Map
    {
        private Chunk2D[] chunks;
        private int seed;
        private string generatorName;
        public Map()
        {

        }
        public Map(IMapGenerator generator,int seed)
        {
            this.seed = seed;
            LoadGenerator(generator);
            Chunks = new Chunk2D[Planet.Size.X * Planet.Size.Y];
        }
        public void ReloadGenerator()
        {
            foreach (IMapGenerator gen in OctoAwesome.Runtime.MapGeneratorManager.GetMapGenerators())
            {
                if (gen.GetType().Name == generatorName)
                {
                    LoadGenerator(gen);
                    return;
                }
            }
            throw new NotSupportedException("MapGenerator not found.");
        }
        private void LoadGenerator(IMapGenerator generator)
        {
            Generator = generator;
            generatorName = generator.GetType().Name;
            Universe = generator.GenerateUniverse(0);
            Planet = generator.GeneratePlanet(0, seed);
        }
        public Chunk2D[] Chunks
        {
            get
            {
                return chunks;
            }
            private set
            {
                chunks = value;
            }
        }

        [NonSerialized]
        private IUniverse universe;
        [NonSerialized]
        private IPlanet planet;
        [NonSerialized]
        private IMapGenerator generator;

        public IUniverse Universe
        {
            get
            {
                return universe;
            }
            private set
            {
                universe = value;
            }
        }
        public IPlanet Planet
        {
            get
            {
                return planet;
            }
            private set
            {
                planet = value;
            }
        }
        public IMapGenerator Generator
        {
            get
            {
                return generator;
            }
            set
            {
                generator = value;
            }
        }

    }
}

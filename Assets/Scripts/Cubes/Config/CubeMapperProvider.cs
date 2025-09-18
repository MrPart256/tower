using System.Linq;

namespace Cubes
{
    public class CubeMapperProvider : Provider<CubeMapper>
    {
        public bool TryGet(int id, out CubeMapper mapper)
        {
            mapper = Get(id);
            return mapper != null;
        }

        public CubeMapper Get(int id)
            => _collection.FirstOrDefault(x => x.ID == id);
    }
}
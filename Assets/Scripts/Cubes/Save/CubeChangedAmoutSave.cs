using Game;
using Saves;
using UniRx;

namespace Cubes.Save
{
    public class CubeChangedAmoutSave : SaveTriggerListener
    {
        private readonly GameRepository _gameRepository;
        
        public CubeChangedAmoutSave(GameRepository gameRepository,SaveSystemFacade saveSystemFacade) : base(saveSystemFacade)
        {
            _gameRepository = gameRepository;
        }

        public override void Initialize()
        {
            _gameRepository.Count.Skip(1).Subscribe(_ => Save())
                .AddTo(_trackStream);
        }
    }
}
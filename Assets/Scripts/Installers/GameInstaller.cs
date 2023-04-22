using SkiFree2.Cursor;
using SkiFree2.UI;
using Zenject;

namespace SkiFree2.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
               .Bind<GameManager>()
               .FromComponentInHierarchy()
               .AsSingle();

            Container
                .Bind<UIManagerBase>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .Bind<Skier>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .Bind<MajinGremlin>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .Bind<Gremlin>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .Bind<WorldDriver>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .Bind<ObstacleManager>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<BoosterManager>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container
                .Bind<CursorTracker>()
                .FromComponentInHierarchy()
                .AsSingle();

            var screens = FindObjectsOfType<UIScreenBase>(true);
            foreach (var screen in screens)
                Container
                    .Bind(screen.GetType())
                    .FromComponentsInHierarchy()
                    .AsSingle();
        }
    }
}

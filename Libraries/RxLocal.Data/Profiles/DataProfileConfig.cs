using AutoMapper;
using RxLocal.Core.Infrastructure;

namespace RxLocal.Data.Profiles
{
    /// <summary>
    /// Registers the AutoMapper profiles
    /// </summary>
    public class DataProfileConfig : IStartupTask
    {
        public void Execute()
        {
            Mapper.Initialize(cfg =>
                cfg.AddProfile<EfProfile>()
            );
        }

        public int Order { get; } = 10;
    }
}

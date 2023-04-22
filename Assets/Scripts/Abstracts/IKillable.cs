using System;

namespace SkiFree2.Abstracts
{
    public interface IKillable
    {
        event Action OnDeath;
        void Kill();
    }
}

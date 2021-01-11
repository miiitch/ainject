using System;

namespace Ainject.Abstractions
{
    public interface IDependencyCall: IDisposable
    {
        void TrackSuccess();
        void TrackFailure();
    }
}
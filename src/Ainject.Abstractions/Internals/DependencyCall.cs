using System;

namespace Ainject.Abstractions.Internals
{
    public class DependencyCall: IDependencyCall
    {
        private readonly ITelemetry _telemetry;
        private readonly string _dependencyTypeName;
        private readonly string _dependencyName;
        private readonly string _data;
        private readonly DependencyCallDefaultStatus _defaultStatus;
        private bool _statusDefined = false;
        private readonly StopwatchBlock _stopwatchBlock;
        public DependencyCall(ITelemetry telemetry, string dependencyTypeName, string dependencyName, string data, DependencyCallDefaultStatus defaultStatus)
        {
            _telemetry = telemetry;
            _dependencyTypeName = dependencyTypeName;
            _dependencyName = dependencyName;
            _data = data;
            _defaultStatus = defaultStatus;
            _stopwatchBlock = new StopwatchBlock();
        }

        public void TrackSuccess()
        {
            TrackDependency(true);
        }

        public void TrackFailure()
        {
            TrackDependency(false);
        }
        private void TrackDependency(bool success)
        {
            if (_statusDefined)
            {
                throw new InvalidOperationException("Dependency status already defined");
            }
            _stopwatchBlock.Stop();
            _telemetry.TrackDependency(_dependencyTypeName, _dependencyName, _data, _stopwatchBlock.StartDate,
                _stopwatchBlock.Elapsed, success);
            _statusDefined = true;
        }
        
        public void Dispose()
        {
            if (_statusDefined) return;
            
            TrackDependency(_defaultStatus == DependencyCallDefaultStatus.Success);
        }
    }
}

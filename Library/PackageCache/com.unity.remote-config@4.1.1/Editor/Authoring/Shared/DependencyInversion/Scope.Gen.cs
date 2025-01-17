// WARNING: Auto generated code. Modifications will be lost!
// Original source 'com.unity.services.shared' @0.0.11.
using System;
using System.Collections.Generic;

namespace Unity.Services.RemoteConfig.Authoring.Editor.Shared.DependencyInversion
{
    sealed class Scope : List<IDisposable>, IDisposable
    {
        public bool Strict { get; }

        public Scope(bool strict = false)
        {
            Strict = strict;
        }

        public void Dispose()
        {
            foreach (var disposable in this)
            {
                disposable.Dispose();
            }
        }
    }
}

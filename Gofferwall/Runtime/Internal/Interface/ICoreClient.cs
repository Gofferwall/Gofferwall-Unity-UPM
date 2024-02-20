using Gofferwall.Model;
using System;

namespace Gofferwall.Internal.Interface
{
    /// <summary>
    /// interface for core client
    /// </summary>
    internal interface ICoreClient
    {
        void Initialize(Action<bool> callback);

        void Initialize(string mediaId, string mediaSecret, Action<bool> callback);

        bool IsInitialized();
        void SetUserId(string userId);
    }
}
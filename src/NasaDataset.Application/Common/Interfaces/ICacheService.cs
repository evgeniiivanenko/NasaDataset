﻿namespace NasaDataset.Application.Common.Interfaces
{
    public interface ICacheService
    {
        T? Get<T>(string key);
        void Set<T>(string key, T value, TimeSpan duration);
        void Remove(string key);
        void ClearMeteoriteCache(); // <— группа: фильтры, группировки и т.д.
    }
}

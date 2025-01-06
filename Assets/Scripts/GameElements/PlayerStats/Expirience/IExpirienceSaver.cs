using System;

public interface IExpirienceSaver
{
    event Action<int> SavedExpirienceChanged;

    int SavedExpirience { get; }

    bool TryChangeSavedExpirience(int exp);
}

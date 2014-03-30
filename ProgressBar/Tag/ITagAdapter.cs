#region



#endregion

namespace ProgressBar.Tag
{
    public interface ITagAdapter
    {
        bool HasPersistedBar();

        ITagContainer GetPersistedBar();

        void PersistContainer(TagContainer containerToPersist);

        void RemoveTagContainer();
    }
}
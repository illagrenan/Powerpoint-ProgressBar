namespace ProgressBar.Tag
{
    public interface ITagWriter
    {
        string GetTagByKey(string key);
        void SaveTag(string key, string value);

        void RemoveTagByKey(string p);
    }
}
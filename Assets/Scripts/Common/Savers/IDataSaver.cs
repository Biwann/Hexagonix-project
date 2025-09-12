public interface IDataSaver
{
    void SetFloat(string key, float value);
    float GetFloat(string key, float defaultValue);

    void SetInt(string key, int value);
    int GetInt(string key, int defaultValue);
}
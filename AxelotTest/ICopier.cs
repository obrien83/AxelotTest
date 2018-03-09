namespace AxelotTest
{
    /// <summary>
    /// Интерфес копировальщика.
    /// </summary>
    internal interface ICopier
    {
        void Copy(string src, string dest, bool isDeleteMode);
    }
}

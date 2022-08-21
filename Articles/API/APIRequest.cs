namespace Articles.API
{
    public abstract record APIRequest(string Url)
    {
        public abstract string GetFullUrl();
    }
}

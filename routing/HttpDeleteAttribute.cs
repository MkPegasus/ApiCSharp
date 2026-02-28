namespace LibraryManagement.routing
{
    /// <summary>
    /// definition de l'attribut HttpDeleteAttribute qui est utilisé pour annoter les méthodes dans les contrôleurs afin d'indiquer que ces méthodes doivent être invoquées en réponse à des requêtes HTTP DELETE.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    class HttpDeleteAttribute:Attribute
    {
        public string Path { get; }
        public HttpDeleteAttribute(string path)
        {
            Path = path;
        }
    }
}
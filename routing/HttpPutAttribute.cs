using System;


namespace LibraryManagement.routing
{
    /// <summary>
    /// definition de l'attribut HttpPutAttribute qui est utilisé pour annoter les méthodes dans les contrôleurs afin d'indiquer que ces méthodes doivent être invoquées en réponse à des requêtes HTTP PUT.
    /// Cet attribut peut également inclure un chemin de route pour spécifier l'URL à laquelle la méthode doit répondre.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    class HttpPutAttribute:Attribute
    {
        public string Path { get; }
        public HttpPutAttribute(string path)
        {
            Path = path;
        }
    }
}
using System;


namespace LibraryManagement.routing
{
    /// <summary>
    /// definition de l'attribut HttpGetAttribute qui est utilisé pour annoter les méthodes dans les contrôleurs
    /// afin d'indiquer que ces méthodes doivent être invoquées en réponse à des requêtes HTTP GET.
    /// Cet attribut peut également inclure un chemin de route pour spécifier l'URL à laquelle la méthode doit répondre.
    /// En utilisant cet attribut, le framework de routage peut associer les requêtes entrantes aux méthodes appropriées dans les contrôleurs,
    /// facilitant ainsi la gestion des différentes opérations liées aux utilisateurs ou à d'autres ressources dans l'application.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    class HttpGetAttribute: Attribute
    {
        public string Path { get; }
        public HttpGetAttribute(string path)
        {
            Path = path;
        }
    }
}
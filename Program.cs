using System.Net;
using System.Reflection;
using System.Text.Json;
using LibraryManagement.controllers;
using LibraryManagement.data;
using LibraryManagement.repositories;
using LibraryManagement.services;
using LibraryManagement.routing;

namespace LibraryManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            // ─────────────────────────────────────────
            // Instanciation manuelle des dépendances
            // ─────────────────────────────────────────
            var dbContext         = new DbContext();
            var bookRepository    = new BookRepository(dbContext);
            var authorRepository  = new AuthorRepository(dbContext);
            var copyRepository    = new BookCopyRepository(dbContext);
            var loanRepository    = new LoanRepository(dbContext);

            var bookService    = new BookService(bookRepository);
            var authorService  = new AuthorService(authorRepository);
            var copyService    = new BookCopyService(copyRepository);
            var loanService    = new LoanService(loanRepository, copyRepository);

            // Liste de tous les contrôleurs à enregistrer dans le routeur
            var controllers = new object[]
            {
                new BookController(bookService),
                new AuthorController(authorService),
                new BookCopyController(copyService),
                new LoanController(loanService)
            };

            // ─────────────────────────────────────────
            // Démarrage du serveur HTTP
            // ─────────────────────────────────────────
            var listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:5000/");
            listener.Start();
            Console.WriteLine("Serveur démarré sur http://localhost:5000/");

            // Boucle infinie pour écouter les requêtes
            while (true)
            {
                // Attendre une requête entrante (bloquant)
                var context  = listener.GetContext();
                var request  = context.Request;
                var response = context.Response;

                string method  = request.HttpMethod;           // GET, POST, PUT, DELETE
                string urlPath = request.Url!.AbsolutePath;   // /api/books, /api/books/1

                Console.WriteLine($"[{method}] {urlPath}");

                string? result = null;

                // ─────────────────────────────────────────
                // Routeur — parcourt tous les contrôleurs
                // et cherche la méthode qui correspond
                // ─────────────────────────────────────────
                foreach (var controller in controllers)
                {
                    var methods = controller.GetType().GetMethods();

                    foreach (var methodInfo in methods)
                    {
                        // Récupérer l'attribut HTTP correspondant à la méthode HTTP reçue
                        Attribute? attr = method switch
                        {
                            "GET"    => methodInfo.GetCustomAttribute<HttpGetAttribute>(),
                            "POST"   => methodInfo.GetCustomAttribute<HttpPostAttribute>(),
                            "PUT"    => methodInfo.GetCustomAttribute<HttpPutAttribute>(),
                            "DELETE" => methodInfo.GetCustomAttribute<HttpDeleteAttribute>(),
                            _        => null
                        };

                        if (attr == null) continue;

                        // Récupérer le path défini dans l'attribut
                        string? attrPath = attr switch
                        {
                            HttpGetAttribute    a => a.Path,
                            HttpPostAttribute   a => a.Path,
                            HttpPutAttribute    a => a.Path,
                            HttpDeleteAttribute a => a.Path,
                            _                    => null
                        };

                        if (attrPath == null) continue;

                        // Vérifier si le path correspond à l'URL reçue
                        // et extraire les paramètres de route (ex: {id})
                        if (!TryMatchRoute(attrPath, urlPath, out var routeParams)) 
                            continue;

                        // Lire le body de la requête si nécessaire (POST, PUT)
                        object? bodyParam = null;
                        var parameters = methodInfo.GetParameters();

                        if (request.HasEntityBody)
                        {
                            using var reader = new StreamReader(request.InputStream);
                            string body = reader.ReadToEnd();

                            // Désérialiser le body vers le type du paramètre attendu
                            var bodyParamInfo = parameters.FirstOrDefault(p =>
                                !routeParams.ContainsKey(p.Name!));

                            if (bodyParamInfo != null)
                                bodyParam = JsonSerializer.Deserialize(body, bodyParamInfo.ParameterType);
                        }

                        // Construire la liste des arguments à passer à la méthode
                        var args2 = new List<object?>();
                        foreach (var param in parameters)
                        {
                            if (routeParams.TryGetValue(param.Name!, out string? routeVal))
                            {
                                // Convertir le paramètre de route vers le bon type (int, string...)
                                args2.Add(Convert.ChangeType(routeVal, param.ParameterType));
                            }
                            else
                            {
                                args2.Add(bodyParam);
                            }
                        }

                        // Appeler la méthode du contrôleur
                        result = (string?)methodInfo.Invoke(controller, args2.ToArray());
                        break;
                    }

                    if (result != null) break;
                }

                // ─────────────────────────────────────────
                // Envoi de la réponse HTTP
                // ─────────────────────────────────────────
                response.ContentType = "application/json";

                if (result == null)
                {
                    response.StatusCode = 404;
                    result = JsonSerializer.Serialize(new { message = "Route introuvable." });
                }
                else
                {
                    response.StatusCode = 200;
                }

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(result);
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();
            }
        }

        /// <summary>
        /// Compare le pattern de route défini dans l'attribut avec l'URL reçue.
        /// Extrait les paramètres dynamiques comme {id}.
        /// Exemple : pattern "/api/books/{id}" + url "/api/books/5" → { "id": "5" }
        /// </summary>
        static bool TryMatchRoute(string pattern, string url, out Dictionary<string, string> routeParams)
        {
            routeParams = new Dictionary<string, string>();

            var patternParts = pattern.Split('/');
            var urlParts     = url.Split('/');

            // Si le nombre de segments ne correspond pas → pas de match
            if (patternParts.Length != urlParts.Length) return false;

            for (int i = 0; i < patternParts.Length; i++)
            {
                if (patternParts[i].StartsWith("{") && patternParts[i].EndsWith("}"))
                {
                    // Segment dynamique → extraire le nom et la valeur
                    string paramName = patternParts[i][1..^1]; // enlève { et }
                    routeParams[paramName] = urlParts[i];
                }
                else if (patternParts[i] != urlParts[i])
                {
                    // Segment statique qui ne correspond pas → pas de match
                    return false;
                }
            }

            return true;
        }
    }
}


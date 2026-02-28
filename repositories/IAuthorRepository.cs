using LibraryManagement.models;

namespace LibraryManagement.repositories
{
    /// <summary>
    /// Définition de l'interface pour le repository de la classe Author qui contient les methodes à implementer 
    /// la ou les classe(s) concrète qui l'implémenterons
    /// </summary>
    public interface IAuthorRepository
    {
        List<Author> GetAll();
        Author? GetById(int id);
        void Add(Author author);
        void Update(Author author);
        void Delete(int id);
    }
}
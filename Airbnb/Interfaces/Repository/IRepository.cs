using Airbnb.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airbnb.Interfaces.Repository
{
    /// <summary>
    /// Rozhraní, které umožňuje implementovat metody ke splnění CRUD
    /// </summary>
    /// <typeparam name="T">Objekt</typeparam>
    public interface IRepository<T> where T : IBaseClass
    {
        /// <summary>
        /// Metoda, která uloží objekt do DB
        /// </summary>
        /// <param name="element">objekt, který chci uložit</param>
        void Save(T element);
        /// <summary>
        /// Metoda, která odstraní objekt z DB
        /// </summary>
        /// <param name="element">objekt, který chci odstranit</param>
        void Delete(T element);
        /// <summary>
        /// Kolekce(SELECT), která obsahuje všechny objekty v DB
        /// </summary>
        /// <returns>Vrací kolekci objektů</returns>
        IEnumerable<T> GetAll();
        /// <summary>
        /// Metoda, která vrací objekt podle zadaného ID
        /// </summary>
        /// <param name="id">ID objektu</param>
        /// <returns>Objekt, s daným ID</returns>
        T GetById(int id);
        /// <summary>
        /// Metoda, která kontroluje zda je uživatel připojen při každém dotazy na databázi
        /// </summary>
        /// <returns></returns>
        bool IsServerConnected();
    }
}

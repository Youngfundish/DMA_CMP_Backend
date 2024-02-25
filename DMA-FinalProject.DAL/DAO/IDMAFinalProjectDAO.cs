namespace DMA_FinalProject.DAL.DAO
{
    /// <summary>
    /// Dette er et generic interface der beskriver metoderne i DAO.
    /// <summary/>
    public interface IDMAFinalProjectDAO<TEntity>
    {
        /// <summary>
        /// Metoden henter alle objekter af den angivet type.
        /// </summary>
        /// <returns>Collection af en bestemt type</returns>
        public IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Henter et specifikt af den efterspurgte type
        /// </summary>
        /// <param name="key">En dynamisk nøgle der bruges som søgeparametre</param>
        /// <returns></returns>
        public TEntity? Get(dynamic key);

        /// <summary>
        /// Metoden der opretter et objekt af den indsatte type
        /// </summary>
        /// <param name="o">Det objekt som ønskes at oprettes</param>
        /// <returns>bool som er afhængig af statussen på den udførte operation</returns>
        public bool Add(TEntity o);

        /// <summary>
        /// Metode til at slette et objekt af den valgte type
        /// </summary>
        /// <param name="key">En dynamisk nøgle som bruges som søgeparametre</param>
        /// <returns>bool som er afhængig af hvad statussen er, på den udførte operation</returns>
        public bool Remove(dynamic key);

        /// <summary>
        /// Metode til at opdatere et objekt af den valgte type
        /// </summary>
        /// <param name="o">Det objekt som skal opdatere med den valgte type</param>
        /// <returns>bool som er afhængig af hvad statussen er, på den udførte operation</returns>
        public bool Update(TEntity o);
    }
}

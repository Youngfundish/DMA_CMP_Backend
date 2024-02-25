namespace DMA_FinalProject.DAL.Authentication
{
    /// <summary>
    ///  BCryptTool klassen er statisk og benyttes til at bruge BCrypts funktionaliteter
    /// </summary>
    public static class BCryptTool
    {
        /// <summary>
        /// Statisk metode som genererer et nyt salt ved hjælp af BCrypt
        /// </summary>
        /// <returns>Returnere string af et random salt</returns>
        private static string GetRandomSalt() => BCrypt.Net.BCrypt.GenerateSalt(12);
        /// <summary>
        /// Statisk metode som hasher det indtastet password
        /// </summary>
        /// <param name="password">En string af det password som skal oprettes</param>
        /// <returns>Returnere string af et det hashet password</returns>
        public static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        /// <summary>
        /// Statisk metode som validere sit password
        /// </summary>
        /// <param name="password">En string af det password som skal valideres</param>
        /// <param name="correctHash">En string af det korrekte hash som passer til passwordet</param>
        /// <returns>Returnere en bool, afhængig af om den blev verifiet eller ikke</returns>
        public static bool ValidatePassword(string password, string correctHash) => BCrypt.Net.BCrypt.Verify(password, correctHash);
    }
}

namespace Core.Auth.Config
{
    /// <summary>
    /// In practice you calibrate these three “cost” knobs (MemoryKb, Iterations, Parallelism) to hit a target wall-clock time (e.g. 200 ms).
    /// If an attacker gains access to your Base64-encoded salts + hashes, they need to run your exact Argon2 setting locally and try password guesses against them.
    /// So this config will slow them down / make it more expensive to brute-force. You can bump these numbers but that will result in more hardware costs / slower processing.
    /// </summary>
    public class PasswordConfig
    {
        // How many parallel lanes (threads) Argon2 will use to fill that memory.
        // E.g. 2: It will split the 64 MB buffer into 2 lanes and compute them concurrently.
        // On a multicore server this speeds up your hash function; on an attacker’s GPU/ASIC it also gives some parallel speed—but overall you’re still bound by memory use.
        public int Parallelism { get; set; }

        // Amount of RAM Argon2 uses during the hash, in kilobytes.
        // E.g. 65536 == 64 MB of memory.
        // Because Argon2 is memory‐hard, forcing an attacker to allocate that much RAM for every guess dramatically raises GPU/ASIC cracking cost.
        public int MemoryKb { get; set; }

        // time cost / number of passes over the memory.
        // E.g. 4: Argon2 will sweep through its memory buffer four times.
        // More iterations = slower hashing (both for legitimate logins and for attackers), so choose a value that balances security vs UX.
        public int Iterations { get; set; }

        // length in bytes.
        public int SaltLength { get; set; }

        // length in bytes.
        public int HashLength { get; set; }

        // Additional secret. Not stored in the db.
        public required string Pepper { get; set; }
    }
}
using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Endava.TechCourse.BankApp.Server.Services
{
    public class WalletService
    {
        private readonly ApplicationDbContext _context;

        public WalletService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateUniqueWalletCode()
        {
            var uniqueId = GenerateRandomNumericString(12);

            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

            var walletCode = $"{timestamp}{uniqueId}".Substring(5, 16);

            while (await WalletCodeExists(walletCode))
            {
                uniqueId = GenerateRandomNumericString(12);
                walletCode = $"{timestamp}{uniqueId}".Substring(5, 16);
            }

            return walletCode;
        }

        private string GenerateRandomNumericString(int length)
        {
            const string digits = "0123456789";
            var random = new Random();
            var result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                result.Append(digits[random.Next(digits.Length)]);
            }

            return result.ToString();
        }

        private async Task<bool> WalletCodeExists(string walletCode)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.Code == walletCode);
            if (wallet == null)
                return false;
            return true;
        }
    }
}
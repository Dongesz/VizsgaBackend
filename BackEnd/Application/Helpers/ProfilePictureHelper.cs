using BackEnd.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;

namespace BackEnd.Application.Helpers
{
    public class ProfilePictureHelper
    {
        private readonly DatabaseContext _context;
        private const string BaseImageUrl = "https://dongesz.com/images/";

        public ProfilePictureHelper(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<string?> GetProfilePictureUrlAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                return null;

            if (!string.IsNullOrEmpty(user.CustomPictureUrl))
            {
                return BaseImageUrl + user.CustomPictureUrl;
            }

            if (user.DefaultPictureUrl.HasValue)
            {
                var pic = await _context.DefaultPictures
                    .FirstOrDefaultAsync(x => x.Id == user.DefaultPictureUrl.Value);

                if (pic != null)
                    return pic.DefaultPictureUrl; 
            }

            return null;
        }
    }
}

using BackEnd.Application.Helpers;
using BackEnd.Domain.Models;
using BackEnd.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Tests;

public class ProfilePictureHelperTests
{
    private static DatabaseContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new DatabaseContext(options);
    }

    [Fact]
    public async Task GetProfilePictureUrlAsync_CustomPicture_ReturnsBaseUrlPlusFileName()
    {
        await using var ctx = CreateInMemoryContext();
        ctx.Users.Add(new User
        {
            Id = 1,
            Email = "a@test.local",
            AuthUserId = Guid.NewGuid().ToString(),
            CustomPictureUrl = "abc-123.png"
        });
        await ctx.SaveChangesAsync();

        var helper = new ProfilePictureHelper(ctx);
        var url = await helper.GetProfilePictureUrlAsync(1);

        Assert.Equal("https://dongesz.com/images/abc-123.png", url);
    }

    [Fact]
    public async Task GetProfilePictureUrlAsync_DefaultPicture_ReturnsUrlFromDefaultPicturesTable()
    {
        await using var ctx = CreateInMemoryContext();
        ctx.DefaultPictures.Add(new DefaultPicture
        {
            Id = 1,
            DefaultPictureUrl = "https://dongesz.com/images/Default_1.png"
        });
        ctx.Users.Add(new User
        {
            Id = 2,
            Email = "b@test.local",
            AuthUserId = Guid.NewGuid().ToString(),
            CustomPictureUrl = null,
            DefaultPictureUrl = 1
        });
        await ctx.SaveChangesAsync();

        var helper = new ProfilePictureHelper(ctx);
        var url = await helper.GetProfilePictureUrlAsync(2);

        Assert.Equal("https://dongesz.com/images/Default_1.png", url);
    }
}

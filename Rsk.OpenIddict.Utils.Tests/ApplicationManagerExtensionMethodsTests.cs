using System.Collections.Immutable;
using System.Text.Json;
using FluentAssertions;
using Moq;
using OpenIddict.Abstractions;
using Rsk.OpenIddict.Utils.Constants;
using Rsk.OpenIddict.Utils.Extensions;
using Rsk.OpenIddict.Utils.Models;

namespace Rsk.OpenIddict.Utils.Tests;

public class ApplicationManagerExtensionMethodsTests
{
    private readonly IOpenIddictApplicationManager openIddictApplicationManager = Mock.Of<IOpenIddictApplicationManager>();

    [Fact]
    public async Task GetClaimsFromProperties_ShouldMapToListOfAdminUIClaimObjects()
    {
        var fakeApplicationObject = new { DisplayName = "Fake Application Object" };

        var fakeProperties = new Dictionary<string, JsonElement>
        {
            { AdminUiConstants.ApplicationPropertyClaims, JsonSerializer.SerializeToElement<object[]>([
                new { Type = "middle_name", Value = "Henry" },
                new { Type = "middle_name", Value = "Adams" },
                new { Type = "birthdate", Value = "19/02/1996" },
                new { Type = "role", Value = "AdminUI Administrator" },
            ])}
        };

        Mock.Get(openIddictApplicationManager)
            .Setup(x => x.GetPropertiesAsync(fakeApplicationObject, It.IsAny<CancellationToken>()))
            .ReturnsAsync(fakeProperties.ToImmutableDictionary());
        
        var actual = await openIddictApplicationManager.GetClaimsFromProperties(fakeApplicationObject);

        actual.Should().NotBeNullOrEmpty();
        actual.Should().Contain(x => x.Type == "middle_name" && x.Value == "Henry");
        actual.Should().Contain(x => x.Type == "middle_name" && x.Value == "Adams");
        actual.Should().Contain(x => x.Type == "birthdate" && x.Value == "19/02/1996");
        actual.Should().Contain(x => x.Type == "role" && x.Value == "AdminUI Administrator");
    }

    [Fact]
    public async Task GetClaimValuesDictionary_ShouldGroupIntoDictionary()
    {
        var fakeApplicationObject = new { DisplayName = "Fake Application Object" };

        var fakeProperties = new Dictionary<string, JsonElement>
        {
            { AdminUiConstants.ApplicationPropertyClaims, JsonSerializer.SerializeToElement<object[]>([
                new { Type = "middle_name", Value = "Henry" },
                new { Type = "middle_name", Value = "Adams" },
                new { Type = "birthdate", Value = "19/02/1996" },
                new { Type = "role", Value = "AdminUI Administrator" },
            ])}
        };

        Mock.Get(openIddictApplicationManager)
            .Setup(x => x.GetPropertiesAsync(fakeApplicationObject, It.IsAny<CancellationToken>()))
            .ReturnsAsync(fakeProperties.ToImmutableDictionary());
        
        var actual = await openIddictApplicationManager.GetClaimValuesDictionary(fakeApplicationObject);

        actual.Should().NotBeNullOrEmpty();
        
        actual["middle_name"].Should().NotBeNullOrEmpty();
        actual["middle_name"].Should().Contain("Henry");
        actual["middle_name"].Should().Contain("Adams");
        
        actual["birthdate"].Should().NotBeNullOrEmpty();
        actual["birthdate"].Should().Contain("19/02/1996");
        
        actual["role"].Should().NotBeNullOrEmpty();
        actual["role"].Should().Contain("AdminUI Administrator");
    }
}
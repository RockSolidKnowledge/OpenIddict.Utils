using System.Collections.Immutable;
using System.Text.Json;
using FluentAssertions;
using Moq;
using OpenIddict.Abstractions;
using Rsk.OpenIddict.Utils.Constants;
using Rsk.OpenIddict.Utils.Extensions;

namespace Rsk.OpenIddict.Utils.Tests;

public class ScopeManagerExtensionMethodsTest
{
    private readonly IOpenIddictScopeManager openIddictScopeManager = Mock.Of<IOpenIddictScopeManager>();
    
    private readonly object[] fakeClaims = ["middle_name", "email", "role", "birthdate"];

    [Fact]
    public async Task GetClaimsFromProperties_MapToListOfClaims()
    {
        var fakeScopeObject = new { DisplayName = "Fake Scope" };

        var fakeProperties = new Dictionary<string, JsonElement>
        {
            { AdminUiConstants.ScopePropertyClaims, JsonSerializer.SerializeToElement(fakeClaims)}
        };
        
        Mock.Get(openIddictScopeManager)
            .Setup(x => x.GetPropertiesAsync(fakeScopeObject, It.IsAny<CancellationToken>()))
            .ReturnsAsync(fakeProperties.ToImmutableDictionary());

        var actual = await openIddictScopeManager.GetClaimsFromProperties(fakeScopeObject);

        actual.Should().NotBeNullOrEmpty();
        actual.Should().BeEquivalentTo(fakeClaims);
    }

    [Fact]
    public async Task GetClaimsFromProperties_WhenLegacy_MapToListOfClaims()
    {
        var fakeScopeObject = new { DisplayName = "Fake Scope" };

        var fakeLegacyProperties = new Dictionary<string, JsonElement>
        {
            { AdminUiConstants.LegacyScopePropertyClaims, JsonSerializer.SerializeToElement(fakeClaims)}
        };
        
        Mock.Get(openIddictScopeManager)
            .Setup(x => x.GetPropertiesAsync(fakeScopeObject, It.IsAny<CancellationToken>()))
            .ReturnsAsync(fakeLegacyProperties.ToImmutableDictionary());

        var actual = await openIddictScopeManager.GetClaimsFromProperties(fakeScopeObject);

        actual.Should().NotBeNullOrEmpty();
        actual.Should().BeEquivalentTo(fakeClaims);
    }
    
    [Fact]
    public async Task ScopesExist_WhenTheyExistOnScope_ShouldBeTrue()
    {
        Mock.Get(openIddictScopeManager)
            .Setup(x => x.FindByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new object());

        var actual = await openIddictScopeManager.ScopesExist(new OpenIddictRequest
        {
            Scope = "middle_name email"
        });

        actual.Should().BeTrue();
    }
    
    [Fact]
    public async Task ScopesExist_WhenOneScopeDoesntExist_ShouldBeFalse()
    {
        Mock.Get(openIddictScopeManager)
            .Setup(x => x.FindByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new object());

        Mock.Get(openIddictScopeManager)
            .Setup(x => x.FindByNameAsync("nonexistant", It.IsAny<CancellationToken>()))
            .ReturnsAsync(null as object);

        var actual = await openIddictScopeManager.ScopesExist(new OpenIddictRequest
        {
            Scope = "middle_name email nonexistant"
        });

        actual.Should().BeFalse();
    }
    
    [Fact]
    public async Task ScopesExist_WhenAllDontExist_ShouldBeFalse()
    {
        var actual = await openIddictScopeManager.ScopesExist(new OpenIddictRequest
        {
            Scope = "nonexistant other_non_existant"
        });

        actual.Should().BeFalse();
    }
}
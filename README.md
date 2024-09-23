# OpenIddict Utils
Contains packages to assist in integrating Rock Solid Knowledge components into OpenIddict.

## Scopes and Client Applications Claims
### Use case
In the OIDC protocol, it is common to assign Claims to both Scopes and Client Applications. Once this binding is in place, the associated Claims should be automatically included in the token when the access request is successful.

### Problem
OpenIddict does not provide a built-in mechanism for binding and assigning these Claims to Scopes or Client Applications by default.

### Solution
In AdminUI, Claims are stored within the Properties bag for each Scope or Client Application under the following keys:

- `urn:com:rocksolidknowledge:adminui:scope:property:claims` in the Scopes
- `urn:com:rocksolidknowledge:adminui:application:property:claims` in the Applications

To easily retrieve the assigned claims, you can use the extension method `GetClaimsFromProperties()` from both `IOpenIddictScopeManager` and `IOpenIddictApplicationManager`.

Aditionally you can call all the Claims grouped by Claim Types from `IOpenIddictApplicationManager` using `GetClaimValuesDictionary()`.